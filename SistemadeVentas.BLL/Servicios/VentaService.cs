﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemadeVentas.BLL.Servicios.Contrato;
using SistemadeVentas.DAL.Repository;
using SistemadeVentas.DAL.Repository.Contratos;
using SistemadeVentas.DTO;
using SistemadeVentas.Model;
namespace SistemadeVentas.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepository;
        private readonly IVentaRepository _ventaRepository;
        private readonly IMapper _mapper;

        public VentaService(IGenericRepository<DetalleVenta> detalleVentaRepository, IVentaRepository ventaRepository, IMapper mapper)
        {
            _detalleVentaRepository = detalleVentaRepository;
            _ventaRepository = ventaRepository;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await _ventaRepository.Registrar(_mapper.Map<Venta>(modelo));
                if(ventaGenerada.IdVenta == 0)
                {
                    throw new TaskCanceledException("No se pudo generar la venta");
                }
                return _mapper.Map<VentaDTO>(ventaGenerada);

            }catch 
            {
                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Venta> query = await _ventaRepository.Consultar();
            var ListaResultado = new List<Venta>();
            try
            {

                if (buscarPor == "fecha")
                {
                    DateTime fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-ECU"));
                    DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ECU"));

                    ListaResultado = await query.Where(v =>
                    v.FechaRegistro.Value.Date >= fechInicio.Date && v.FechaRegistro.Value.Date <= fechFin.Date
                    ).Include(dv => dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                    
                }
                else
                {
                    ListaResultado = await query.Where(v =>
                        v.NumeroDocumento == numeroVenta)
                        .Include(dv => dv.DetalleVenta).ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }
            }
            catch 
            {
                throw;
            }
            return _mapper.Map<List<VentaDTO>>(ListaResultado);
        }

        

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepository.Consultar();
            var ListaResultado = new List<DetalleVenta>();
            try
            {
                DateTime fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-ECU"));
                DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-ECU"));

                ListaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv =>
                    dv.IdVentaNavigation.FechaRegistro.Value.Date >= fechInicio.Date &&
                    dv.IdVentaNavigation.FechaRegistro.Value.Date <= fechFin.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ReporteDTO>>(ListaResultado);
        }
    }
}

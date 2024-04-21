﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemadeVentas.DAL;
using SistemadeVentas.DAL.Repository.Contratos;
using SistemadeVentas.Model;

namespace SistemadeVentas.DAL.Repository
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository 
    {
        private readonly DbventasContext _dbventasContext;

        public VentaRepository(DbventasContext dbventasContext) : base(dbventasContext) 
        {
            _dbventasContext = dbventasContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using(var transaction = _dbventasContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbventasContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - dv.Cantidad;

                        _dbventasContext.Productos.Update(producto_encontrado);

                    }

                    await _dbventasContext.SaveChangesAsync();

                    NumeroDocumento correlativo = _dbventasContext.NumeroDocumentos.First();

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;

                    correlativo.FechaRegistro = DateTime.Now;

                    await _dbventasContext.SaveChangesAsync();

                    _dbventasContext.NumeroDocumentos.Update(correlativo);

                    await _dbventasContext.SaveChangesAsync();

                    int CantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));

                    string numeroVenta = ceros + correlativo.UltimoNumero.ToString();

                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - CantidadDigitos, CantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await _dbventasContext.AddAsync(modelo);
                    await _dbventasContext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaction.Commit();

                }catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemadeVentas.BLL.Servicios.Contrato;
using SistemadeVentas.DAL.Repository.Contratos;
using SistemadeVentas.DTO;
using SistemadeVentas.Model;
namespace SistemadeVentas.BLL.Servicios
{
    public class DashboardService : IDashBoardService
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IVentaRepository _ventaRepository;
        private readonly IMapper _mapper;

        public DashboardService(IGenericRepository<Producto> productoRepository, IVentaRepository ventaRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _ventaRepository = ventaRepository;
            _mapper = mapper;
        }

       

        private IQueryable<Venta> RetornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);

        }
        private async Task<int> TotalVenta_UltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await _ventaRepository.Consultar();

            if (_ventaQuery.Count() > 0)
            {
                var tablaVenta = RetornarVentas(_ventaQuery, -7);
                total = tablaVenta.Count();

            }
            return total;
        }

        private async Task<string> TotalIngresos_UltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaquery = await _ventaRepository.Consultar();
            if (_ventaquery.Count() > 0)
            {
                var tablaVenta = RetornarVentas(_ventaquery, 7);

                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-ECU"));

        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _queryProducto = await _productoRepository.Consultar();
            int total = _queryProducto.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> Ventas_UltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            IQueryable<Venta> _queryVenta = await _ventaRepository.Consultar();

            if (_queryVenta.Count() > 0)
            {
                var tablaVenta = RetornarVentas(_queryVenta, -7);

                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date).OrderBy(g => g.Key)
                    .Select(dv => new {fecha = dv.Key.ToString("dd/MM/yyyy"), total =  dv.Count()})
                    .ToDictionary(keySelector : r => r.fecha, elementSelector :  r => r.total);
            }
            return resultado;
        }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashboard = new DashboardDTO();

            try
            {
                vmDashboard.TotalVentas = await TotalVenta_UltimaSemana();
                vmDashboard.TotalIngresos = await TotalIngresos_UltimaSemana();
                vmDashboard.TotalProductos = await TotalProductos();
                
                List<VentaSemanaDTO> listaVentaSemana = new List<VentaSemanaDTO>();

                foreach (KeyValuePair<string,int> item in await Ventas_UltimaSemana())
                {
                    listaVentaSemana.Add(new VentaSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value

                    });
                }

                vmDashboard.Ventas_UltimaSemana = listaVentaSemana;

                return vmDashboard;
                
            }
            catch
            {
                throw;
            }
        }
    }
}

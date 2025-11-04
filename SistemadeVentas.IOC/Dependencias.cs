using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemadeVentas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemadeVentas.DAL.Repository.Contratos;
using SistemadeVentas.DAL.Repository;
using SistemadeVentas.Utility;
using SistemadeVentas.BLL.Servicios.Contrato;
using SistemadeVentas.BLL.Servicios;

namespace SistemadeVentas.IOC
{
    public static class Dependencias
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbventasContext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("cadenaSQL"),
                        b => b.MigrationsAssembly("SistemadeVentas.DAL")
                    )
            );
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDashBoardService, DashboardService>();
            services.AddScoped<IMenuService, MenuService>(); 


        }

         
    }
}

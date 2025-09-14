using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemadeVentas.DTO;
using SistemadeVentas.Model;
using System.Globalization;
<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
>>>>>>> 87524d8 (Actualizaciones)
namespace SistemadeVentas.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Rol

            CreateMap<Rol, RolDTO>().ReverseMap();

            #endregion

            #region Menu

            CreateMap<Menu,  MenuDTO>().ReverseMap();

            #endregion

            #region Usuario

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                destino.RolDescripcion,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)

            );

            #endregion

            #region SesionDTO

            CreateMap<Usuario, SesionDTO>().
                ForMember(destino =>
                destino.RolDescripcion, 
                opt => 
                opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                );

            CreateMap<UsuarioDTO, Usuario>().
                ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore())
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false));

            #endregion

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            #endregion

            #region Producto
            CreateMap<Producto, ProductoDTO>()
            .ForMember(destino =>
            destino.DescripcionCategoria,
            opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre))
            .ForMember(destino =>
            destino.Precio,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-ECU"))))
            .ForMember(destino => 
            destino.EsActivo, 
            opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0));


            CreateMap<ProductoDTO, Producto>()
            .ForMember(destino =>
                destino.IdCategoriaNavigation,
                opt => opt.Ignore())
            .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio, new CultureInfo("es-ECU"))))
            .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)


         );
            #endregion

            #region Venta

<<<<<<< HEAD
=======

>>>>>>> 87524d8 (Actualizaciones)
            CreateMap<Venta, VentaDTO>()
            .ForMember(destino =>
            destino.TotalTexto,
            opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-ECU"))))
<<<<<<< HEAD
            .ForMember(destino =>
            destino.FechaRegistro,
            opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy")));

            CreateMap<VentaDTO, Venta>()
            .ForMember(destino =>
            destino.Total,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-ECU")))

            );
=======

            .ForMember(destino =>
            destino.FechaRegistro,
            opt => opt.MapFrom(origen => origen.FechaRegistro!.Value.ToString("dd/MM/yyyy")));

            CreateMap<VentaDTO, Venta>()

            .ForMember(destino =>
            destino.Total,
            opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-ECU"))))
            .ForMember(destino => destino.FechaRegistro,
            opt => opt.MapFrom(origen => origen.FechaRegistro ?? DateTime.Now));
            /*
            .ForMember(destino => destino.FechaRegistro,
            opt => opt.MapFrom(origen =>
            DateTime.ParseExact(origen.FechaRegistro, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
            */


>>>>>>> 87524d8 (Actualizaciones)

            #endregion

            #region DetalleVenta

            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ForMember(destino =>
                destino.DescripcionProducto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                .ForMember(destino =>
                destino.PrecioTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-ECU"))))
                .ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-ECU"))));

            CreateMap<DetalleVentaDTO, DetalleVenta>()
                .ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto)))
                .ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto)));

<<<<<<< HEAD
=======
           

>>>>>>> 87524d8 (Actualizaciones)


            #endregion

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>()
                .ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy")))
                .ForMember(destino =>
                destino.NumeroDocumento,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento))
                .ForMember(destino =>
                destino.TipoPago,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago))
                .ForMember(destino =>
                destino.TotalVenta,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdVentaNavigation.Total.Value, new CultureInfo("es-ECU"))))
                 .ForMember(destino =>
                 destino.Producto,
                 opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre))
                 .ForMember(destino =>
                 destino.Precio,
                 opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-ECU"))))
                 .ForMember(destino =>
                 destino.Total,
                 opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-ECU"))));                                                                                         
                                                                                                                  
            #endregion

        }
    }
}

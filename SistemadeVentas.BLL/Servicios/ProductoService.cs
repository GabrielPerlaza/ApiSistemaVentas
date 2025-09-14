using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemadeVentas.BLL.Servicios.Contrato;
using SistemadeVentas.DAL.Repository.Contratos;
using SistemadeVentas.DTO;
using SistemadeVentas.Model;
namespace SistemadeVentas.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Producto> _productoRepository;

        public ProductoService(IMapper mapper, IGenericRepository<Producto> productoRepository)
        {
            _mapper = mapper;
            _productoRepository = productoRepository;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productoRepository.Consultar();
                var listaProducto = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();

                return _mapper.Map<List<ProductoDTO>>(listaProducto.ToList());

            }
            catch 
            {
                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await _productoRepository.Crear(_mapper.Map<Producto>(modelo));

                if (productoCreado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }

                return _mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Producto>(modelo);

                var productoEncontrado = await _productoRepository.Obtener(
                    u => u.IdProducto == productoModelo.IdProducto
                    ) ;
                if(productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe");
                }

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                bool respuesta = await _productoRepository.Editar(productoEncontrado);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar el producto");
                }

                return respuesta;

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
<<<<<<< HEAD
                var productoEncontrado = _productoRepository.Obtener(
=======
                var productoEncontrado = await _productoRepository.Obtener(
>>>>>>> 87524d8 (Actualizaciones)
                    p => p.IdProducto == id
                    );
                if(productoEncontrado == null)
                {
                    throw new TaskCanceledException("No se logro encontrar el producto");
                }
<<<<<<< HEAD
                bool respuesta = await _productoRepository.Eliminar(_mapper.Map<Producto>(productoEncontrado));
=======
                bool respuesta = await _productoRepository.Eliminar(productoEncontrado);
>>>>>>> 87524d8 (Actualizaciones)

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo eliminar el producto");
                }
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

      
    }
}

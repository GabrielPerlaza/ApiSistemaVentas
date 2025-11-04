using System;
using System.Collections.Generic;
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
    public class CategoriaService : ICategoriaService 
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Categoria> _categoriaRepository;

        public CategoriaService(IMapper mapper, IGenericRepository<Categoria> categoriaRepository)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            try
            {
                var listaCategoria = await _categoriaRepository.Consultar();
                return _mapper.Map<List<CategoriaDTO>>(listaCategoria.ToList());

            }
            catch 
            {
                throw;
            }
        }
    }
}

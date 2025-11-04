using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemadeVentas.DAL.Repository.Contratos;
using SistemadeVentas.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SistemadeVentas.DAL.Repository
{
    public class GenericRepository<Tmodelo> : IGenericRepository<Tmodelo> where Tmodelo : class
    {
        private readonly DbventasContext _dbventasContext;

        public GenericRepository(DbventasContext dbventasContext)
        {
            _dbventasContext = dbventasContext; 
        }

        public async Task<Tmodelo> Obtener(Expression<Func<Tmodelo, bool>> Filtro)
        {
            try
            {
                Tmodelo modelo = await _dbventasContext.Set<Tmodelo>().FirstOrDefaultAsync(Filtro);
                return modelo;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Tmodelo> Crear(Tmodelo modelo)
        {
            try
            {
                _dbventasContext.Set<Tmodelo>().AddAsync(modelo);
                await _dbventasContext.SaveChangesAsync();
                return modelo;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Editar(Tmodelo modelo)
        {
            try
            {
                _dbventasContext.Set<Tmodelo>().Update(modelo);
                await _dbventasContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Tmodelo modelo)
        {
            try
            {
                _dbventasContext.Set<Tmodelo>().Remove(modelo);
                await _dbventasContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IQueryable<Tmodelo>> Consultar(Expression<Func<Tmodelo, bool>> Filtro = null)
        {
            try
            {
                IQueryable<Tmodelo> queryModelo = Filtro == null ? _dbventasContext.Set<Tmodelo>() : _dbventasContext.Set<Tmodelo>().Where(Filtro);
                return queryModelo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       

      
    }

      

       

      
    }


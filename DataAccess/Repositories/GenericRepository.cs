using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    /// <summary>
    /// Repositorio genérico para operaciones CRUD sobre una entidad del dominio.
    /// </summary>
    /// <typeparam name="TEntity">Tipo de entidad del dominio</typeparam>
    public class GenericRepository<TEntity> : IGenericRepositoriy<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Contexto de base de datos
        /// </summary>
        private readonly JujuTestContext _context;

        /// <summary>
        /// Conjunto de datos (tabla) asociado a la entidad
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor del repositorio
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado</param>
        public GenericRepository(JujuTestContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        /// <returns>Lista de entidades</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Busca una entidad por su ID
        /// </summary>
        /// <param name="id">Identificador único</param>
        /// <returns>Entidad encontrada o null</returns>
        public async Task<TEntity> FindByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(false);
        }

        /// <summary>
        /// Crea una nueva entidad en la base de datos
        /// </summary>
        /// <param name="entity">Entidad a crear</param>
        /// <returns>True si se guardó correctamente</returns>
        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity).ConfigureAwait(false);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// Crea múltiples entidades en una sola operación
        /// </summary>
        /// <param name="entities">Colección de entidades</param>
        /// <returns>True si se guardaron correctamente</returns>
        public async Task<bool> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities).ConfigureAwait(false);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        /// <param name="editedEntity">Entidad con los nuevos datos</param>
        /// <returns>True si se actualizó correctamente</returns>
        public async Task<bool> UpdateAsync(TEntity editedEntity)
        {
            _dbSet.Update(editedEntity);
            int changes = await _context.SaveChangesAsync().ConfigureAwait(false);
            return changes > 0;
        }

        /// <summary>
        /// Elimina una entidad por su ID
        /// </summary>
        /// <param name="id">Identificador de la entidad</param>
        /// <returns>True si se eliminó correctamente</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await FindByIdAsync(id).ConfigureAwait(false);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        /// <summary>
        /// Verifica si existe alguna entidad que cumpla una condición
        /// </summary>
        /// <param name="predicate">Expresión booleana</param>
        /// <returns>True si existe al menos una coincidencia</returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate).ConfigureAwait(false);
        }

        /// <summary>
        /// Obtiene una lista de entidades que cumplen una condición específica.
        /// </summary>
        /// <param name="predicate">Expresión para filtrar las entidades</param>
        /// <returns>Lista de entidades que cumplen la condición</returns>
        public async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Elimina un conjunto de entidades de la base de datos.
        /// </summary>
        /// <param name="entities">Colección de entidades a eliminar</param>
        /// <returns>True si se eliminaron correctamente</returns>
        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

    }
}

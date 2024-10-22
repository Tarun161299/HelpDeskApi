

namespace HelpDeskAPI.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using EF.Models;
    using Interfaces;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Query.Internal;

    /// <inheritdoc/>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        private readonly HelpDeskDBContext objHelpDeskDBContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="exclusionDrugsContext">ExclusionDrugs DbContext.</param>
        public GenericRepository(HelpDeskDBContext dbContext)
        {
            this.objHelpDeskDBContext = dbContext;
            if (dbContext != null)
            {
                dbSet = this.objHelpDeskDBContext.Set<TEntity>();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await dbSet
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet
                    .AsNoTracking();
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return dbSet
                    .Where(predicate)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await dbSet
                    .Where(predicate)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet
                    .Where(predicate)
                    .AsNoTracking();
        }

        /// <inheritdoc/>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return dbSet.AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return this.dbSet.AddAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var entityToDelete = await FindByAsync(predicate, cancellationToken).ConfigureAwait(true);

            await DeleteAsync(entityToDelete, cancellationToken).ConfigureAwait(true);
        }

        /// <inheritdoc/>
        public virtual Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken)
        {
            return Task.Run(
                () =>
                {
                    if (objHelpDeskDBContext.Entry(entityToDelete).State == EntityState.Detached)
                    {
                        dbSet.Remove(entityToDelete);
                    }

                    dbSet.Remove(entityToDelete);
                }, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual Task UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken)
        {
            return Task.Run(
                () =>
                {
                    //dbSet.Attach(entityToUpdate);
                    dbSet.Update(entityToUpdate);
                }, cancellationToken);
        }
       
        ///// <inheritdoc/>
        //public virtual Task UpdatewithIDAsync(TEntity entityToUpdate, TEntity currentEntity, CancellationToken cancellationToken)
        //{
        //    return Task.Run(
        //        () =>
        //        {
        //            objHelpDeskDBContext.Entry(entityToUpdate).CurrentValues.SetValues(currentEntity);
        //        }, cancellationToken);
        //}

        /// <inheritdoc/>
        public virtual Task<int> ExecuteSqlRawAsync(string storedProcedureName, ref SqlParameter[] sqlParameters, CancellationToken cancellationToken)
        {
            if (sqlParameters == null)
            {
                return Task.FromResult(0);
            }

            return objHelpDeskDBContext.Database.ExecuteSqlRawAsync(storedProcedureName, sqlParameters, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual Task<int> ExecuteSqlRawAsync(string storedProcedureName, SqlParameter[] sqlParameters, CancellationToken cancellationToken)
        {
            if (sqlParameters == null)
            {
                return Task.FromResult(0);
            }

            return objHelpDeskDBContext.Database.ExecuteSqlRawAsync(storedProcedureName, sqlParameters, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> FromSqlRawAsync(string storedProcedureName, SqlParameter[] sqlParameters, CancellationToken cancellationToken)
        {
            return await dbSet.FromSqlRaw(storedProcedureName, sqlParameters)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken)
                        .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<List<TEntity>> FromSqlRawAsync(string storedProcedureName, ref SqlParameter[] sqlParameters, CancellationToken cancellationToken)
        {
            return dbSet.FromSqlRaw(storedProcedureName, sqlParameters)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return this.objHelpDeskDBContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Return objHelpDeskDBContext objects.
        /// </summary>
        /// <returns> Return objHelpDeskDBContext. </returns>
        public HelpDeskDBContext OBSDBContextOut()
        {
            return objHelpDeskDBContext;
        }
    }
}

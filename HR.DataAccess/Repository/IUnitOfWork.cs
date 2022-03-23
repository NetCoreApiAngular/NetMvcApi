using System;

namespace HR.DataAccess.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Function us to Get instance of a Object on Database
        /// </summary>
        /// <typeparam name="TEntity">Object is target</typeparam>
        /// <returns></returns>
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Function use to Save all Object is changed into Database 
        /// </summary>
        void SaveChanges();
        System.Threading.Tasks.Task SaveChangesAsync();
        void TransactionSaveChanges();
    }
}

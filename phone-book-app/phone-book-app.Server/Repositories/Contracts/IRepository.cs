using System.Linq.Expressions;

namespace phone_book_app.Server.Repositories.Contracts
{
    public interface IRepository<TEntity>
       where TEntity : class
    {
        bool IsExisting(Expression<Func<TEntity, bool>> predicate);
        Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate);
        bool IsEmpty();
        Task<TEntity> GetAsync(dynamic id);
        TEntity Get(dynamic id);
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> RemoveAll();
    }
}

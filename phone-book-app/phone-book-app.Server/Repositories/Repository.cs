using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.UnitOfWorks.Contracts;
using System.Linq.Expressions;

namespace phone_book_app.Server.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
      where TEntity : class
    {
        protected readonly DbSet<TEntity> Entities;

        public Repository(IUnitOfWork<DbContext> unitOfWork)
        {
            Entities = unitOfWork.GetDbContext().Set<TEntity>();
        }

        public bool IsExisting(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate) != null;
        }

        public async Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.FirstOrDefaultAsync(predicate) != null;
        }

        public bool IsEmpty()
        {
            return !Entities.Any();
        }

        public async Task<TEntity> GetAsync(dynamic id)
        {
            return await Entities.FindAsync(id);
        }

        public TEntity Get(dynamic id)
        {
            return Entities.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Entities;
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.Where(predicate).ToListAsync();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.FirstOrDefaultAsync(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            var result = Entities.Add(entity);
            return result.Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await Entities.AddAsync(entity);
            return result.Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
        }

        public TEntity Update(TEntity entity)
        {
            var result = Entities.Update(entity);
            return result.Entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            var removeRange = entities as TEntity[] ?? entities.ToArray();
            Entities.UpdateRange(removeRange);
            return removeRange;
        }

        public TEntity Remove(TEntity entity)
        {
            Entities.Remove(entity);
            return entity;
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            var removeRange = entities as TEntity[] ?? entities.ToArray();
            Entities.RemoveRange(removeRange);
            return removeRange;
        }

        public IEnumerable<TEntity> RemoveAll()
        {
            var removeRange = Entities.ToArray();
            Entities.RemoveRange(removeRange);
            return removeRange;
        }
    }
}

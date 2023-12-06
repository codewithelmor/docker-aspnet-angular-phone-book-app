using Microsoft.EntityFrameworkCore;

namespace phone_book_app.Server.UnitOfWorks.Contracts
{
    public interface IUnitOfWork<out TContext> : IDisposable where TContext : DbContext
    {
        TContext GetDbContext();
        Task Commit(CancellationToken cancellationToken = default);
        void Rollback();
    }
}

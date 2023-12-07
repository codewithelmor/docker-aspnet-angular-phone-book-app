using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Models;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.UnitOfWorks.Contracts;
using System.Linq.Expressions;

namespace phone_book_app.Server.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(IPhoneBookAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IEnumerable<Contact>> FindAsync(Expression<Func<Contact, bool>> predicate)
        {
            return await Entities.Include(x => x.Label).Where(predicate).ToListAsync();
        }

        public override Contact FirstOrDefault(Expression<Func<Contact, bool>> predicate)
        {
            return Entities.Include(x => x.Label).FirstOrDefault(predicate);
        }

        public override Task<Contact> FirstOrDefaultAsync(Expression<Func<Contact, bool>> predicate)
        {
            return Entities.Include(x => x.Label).FirstOrDefaultAsync(predicate);
        }
    }
}

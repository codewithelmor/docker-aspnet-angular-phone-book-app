using phone_book_app.Server.Models;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.UnitOfWorks.Contracts;

namespace phone_book_app.Server.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(IPhoneBookAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

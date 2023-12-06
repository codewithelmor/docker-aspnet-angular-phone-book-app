using phone_book_app.Server.Data;
using phone_book_app.Server.UnitOfWorks.Contracts;

namespace phone_book_app.Server.UnitOfWorks
{
    public class PhoneBookAppUnitOfWork : UnitOfWork<PhoneBookAppContext>, IPhoneBookAppUnitOfWork
    {
        public PhoneBookAppUnitOfWork(PhoneBookAppContext context) : base(context)
        {
        }
    }
}

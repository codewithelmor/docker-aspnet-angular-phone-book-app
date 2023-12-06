using phone_book_app.Server.Models;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.UnitOfWorks.Contracts;

namespace phone_book_app.Server.Repositories
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(IPhoneBookAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

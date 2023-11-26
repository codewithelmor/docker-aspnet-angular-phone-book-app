using phone_book_app.Server.ViewModels;

namespace phone_book_app.Server.Services.Contracts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactViewModel>> ListAsync();
    }
}

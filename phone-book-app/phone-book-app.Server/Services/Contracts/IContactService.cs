using phone_book_app.Server.InputModels;
using phone_book_app.Server.ViewModels;

namespace phone_book_app.Server.Services.Contracts
{
    public interface IContactService
    {
        Task<IEnumerable<ContactViewModel>> ListAsync();
        Task<ContactViewModel> CreateAsync(ContactInputModel model);
        Task<ContactViewModel> UpdateAsync(ContactInputModel model);
        Task DeleteAsync(BaseInputModel model);
    }
}

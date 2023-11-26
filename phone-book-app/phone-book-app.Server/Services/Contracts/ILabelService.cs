using Microsoft.AspNetCore.Mvc.Rendering;

namespace phone_book_app.Server.Services.Contracts
{
    public interface ILabelService
    {
        Task<SelectList> AsSelectList();
        Task<SelectListItem> CreateLabelIfExisting(string name);
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace phone_book_app.Server.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public string GivenName { get; set; } = null!;

        public string FamilyName { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        public string BirthDate { get; set; } = null!;

        public SelectListItem Label { get; set; } = null!;
    }
}

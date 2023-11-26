namespace phone_book_app.Server.InputModels
{
    public class ContactInputModel : BaseInputModel
    {
        public string GivenName { get; set; } = null!;

        public string FamilyName { get; set; } = null!;

        public string MobileNumber { get; set; } = null!;

        public string? BirthDate { get; set; }

        public string Label { get; set; } = null!;
    }
}

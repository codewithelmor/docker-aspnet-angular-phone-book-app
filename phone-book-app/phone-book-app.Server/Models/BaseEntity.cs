namespace phone_book_app.Server.Models
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }

        public DateTimeOffset? DeletedDate { get; set; }
    }
}

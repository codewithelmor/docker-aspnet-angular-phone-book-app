using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using phone_book_app.Server.Models;

namespace phone_book_app.Server.Data.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.FamilyName).HasMaxLength(20);
            builder.Property(e => e.GivenName).HasMaxLength(20);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.MobileNumber).HasMaxLength(20);

            builder.HasOne(d => d.Label).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.LabelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contact_Label");
        }
    }
}

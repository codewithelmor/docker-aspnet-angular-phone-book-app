using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using phone_book_app.Server.Models;

namespace phone_book_app.Server.Data.Configurations
{
    public class LabelConfiguration : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> builder)
        {
            builder.ToTable("Label");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())");
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.Name).HasMaxLength(20);
        }
    }
}

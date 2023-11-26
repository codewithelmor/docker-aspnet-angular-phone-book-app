using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Data.Configurations;
using phone_book_app.Server.Models;

namespace phone_book_app.Server.Data;

public partial class PhoneBookAppContext : DbContext
{
    public PhoneBookAppContext()
    {
    }

    public PhoneBookAppContext(DbContextOptions<PhoneBookAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new LabelConfiguration());

    }
}

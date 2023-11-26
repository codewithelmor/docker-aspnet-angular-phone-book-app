using System;
using System.Collections.Generic;

namespace phone_book_app.Server.Models;

public partial class Label : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}

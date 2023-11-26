using System;
using System.Collections.Generic;

namespace phone_book_app.Server.Models;

public partial class Contact : BaseEntity
{
    public int Id { get; set; }

    public string GivenName { get; set; } = null!;

    public string FamilyName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public int LabelId { get; set; }

    public virtual Label Label { get; set; } = null!;
}

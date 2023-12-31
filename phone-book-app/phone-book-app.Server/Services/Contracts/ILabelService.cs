﻿using Microsoft.AspNetCore.Mvc.Rendering;
using phone_book_app.Server.Models;

namespace phone_book_app.Server.Services.Contracts
{
    public interface ILabelService
    {
        Task<SelectList> AsSelectListAsync();
        Task<Label> CreateLabelIfExistingAsync(string name);
    }
}

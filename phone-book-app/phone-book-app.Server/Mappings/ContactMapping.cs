using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using phone_book_app.Server.InputModels;
using phone_book_app.Server.Models;
using phone_book_app.Server.ViewModels;

namespace phone_book_app.Server.Mappings
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<Contact, ContactViewModel>()
                .ForMember(dest => dest.BirthDate,
                    opt => opt.MapFrom(src => src.BirthDate.HasValue ? src.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty))
                .ForMember(dest => dest.Label,
                    opt => opt.MapFrom(src => new SelectListItem
                    {
                        Text = src.Label.Name,
                        Value = src.Label.Id.ToString(),
                        Disabled = src.Label.IsActive
                    }));
        }
    }
}

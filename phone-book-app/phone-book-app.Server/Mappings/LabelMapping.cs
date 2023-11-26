using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using phone_book_app.Server.Models;

namespace phone_book_app.Server.Mappings
{
    public class LabelMapping : Profile
    {
        public LabelMapping()
        {
            CreateMap<Label, SelectListItem>()
                .ForMember(dest => dest.Text,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value,
                    opt => opt.MapFrom(src => src.Id));
        }
    }
}

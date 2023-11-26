using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Data;
using phone_book_app.Server.Models;
using phone_book_app.Server.Services.Contracts;

namespace phone_book_app.Server.Services
{
    public class LabelService : ILabelService
    {
        private readonly PhoneBookAppContext _context;
        private readonly IMapper _mapper;

        public LabelService(
            PhoneBookAppContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SelectList> AsSelectList()
        {
            try
            {
                var labels = await _context.Labels
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                var items = _mapper.Map<List<SelectListItem>>(labels);

                return new SelectList(items, "Value", "Text", null, null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SelectListItem> CreateLabelIfExisting(string name)
        {
            try
            {
                var existingLabel = await _context.Labels.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                if (existingLabel != null)
                {
                    return _mapper.Map<SelectListItem>(existingLabel);
                }

                var newLabel = await _context.Labels.AddAsync(new Label { Name = name });
                await _context.SaveChangesAsync();
                return _mapper.Map<SelectListItem>(newLabel.Entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

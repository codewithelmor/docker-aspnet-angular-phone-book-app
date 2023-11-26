using AutoMapper;
using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Data;
using phone_book_app.Server.Services.Contracts;
using phone_book_app.Server.ViewModels;

namespace phone_book_app.Server.Services
{
    public class ContactService : IContactService
    {
        private readonly PhoneBookAppContext _context;
        private readonly IMapper _mapper;

        public ContactService(
            PhoneBookAppContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactViewModel>> ListAsync()
        {
            try
            {
                var contacts = await _context.Contacts
                    .Include(x => x.Label)
                    .Where(x => !x.IsDeleted)
                    .ToListAsync();

                return _mapper.Map<List<ContactViewModel>>(contacts);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

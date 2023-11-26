using AutoMapper;
using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Data;
using phone_book_app.Server.InputModels;
using phone_book_app.Server.Models;
using phone_book_app.Server.Services.Contracts;
using phone_book_app.Server.ViewModels;

namespace phone_book_app.Server.Services
{
    public class ContactService : IContactService
    {
        private readonly PhoneBookAppContext _context;
        private readonly IMapper _mapper;
        private readonly ILabelService _labelService;

        public ContactService(
            PhoneBookAppContext context,
            IMapper mapper,
            ILabelService labelService)
        {
            _context = context;
            _mapper = mapper;
            _labelService = labelService;
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

        public async Task<ContactViewModel> CreateAsync(ContactInputModel model)
        {
            try
            {
                var label = await _labelService.CreateLabelIfExisting(model.Label);
                var contact = new Contact();
                contact.GivenName = model.GivenName;
                contact.FamilyName = model.FamilyName;
                contact.MobileNumber = model.MobileNumber;
                contact.IsActive = true;
                contact.Label = label;
                if (!string.IsNullOrWhiteSpace(model.BirthDate))
                {
                    contact.BirthDate = DateOnly.Parse(model.BirthDate);
                }
                contact = (await _context.Contacts.AddAsync(contact)).Entity;
                await _context.SaveChangesAsync();
                return _mapper.Map<ContactViewModel>(contact);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ContactViewModel> UpdateAsync(ContactInputModel model)
        {
            try
            {
                var contact = await _context.Contacts
                    .Include(x => x.Label)
                    .FirstOrDefaultAsync(x => x.Id == model.GetId());
                if (contact != null)
                {
                    contact.GivenName = model.GivenName;
                    contact.FamilyName = model.FamilyName;
                    contact.MobileNumber = model.MobileNumber;
                    var label = await _labelService.CreateLabelIfExisting(model.Label);
                    contact.Label = label;
                    if (!string.IsNullOrWhiteSpace(model.BirthDate))
                    {
                        contact.BirthDate = DateOnly.Parse(model.BirthDate);
                    }
                    contact.UpdatedDate = DateTimeOffset.UtcNow;
                    contact = (_context.Contacts.Update(contact)).Entity;
                    await _context.SaveChangesAsync();
                    return _mapper.Map<ContactViewModel>(contact);
                }
                throw new KeyNotFoundException();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(BaseInputModel model)
        {
            try
            {
                var contact = await _context.Contacts
                    .Include(x => x.Label)
                    .FirstOrDefaultAsync(x => x.Id == model.GetId());
                if (contact != null)
                {
                    contact.IsDeleted = true;
                    contact.DeletedDate = DateTimeOffset.UtcNow;
                    contact = (_context.Contacts.Update(contact)).Entity;
                    await _context.SaveChangesAsync();
                    return;
                }
                throw new KeyNotFoundException();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

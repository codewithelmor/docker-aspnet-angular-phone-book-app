﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Data;
using phone_book_app.Server.InputModels;
using phone_book_app.Server.Models;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.Services.Contracts;
using phone_book_app.Server.UnitOfWorks.Contracts;
using phone_book_app.Server.ViewModels;

namespace phone_book_app.Server.Services
{
    public class ContactService : IContactService
    {
        private readonly IPhoneBookAppUnitOfWork _unitOfWork;
        private readonly IContactRepository _repository;
        private readonly ILabelService _labelService;
        private readonly IMapper _mapper;

        public ContactService(
            IPhoneBookAppUnitOfWork unitOfWork,
            IContactRepository repository,
            ILabelService labelService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _labelService = labelService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContactViewModel>> ListAsync()
        {
            try
            {
                var contacts = await _repository.Find(x => !x.IsDeleted)
                    .Include(x => x.Label)
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
                contact.LabelId = label.Id;
                contact.Label = label;
                if (!string.IsNullOrWhiteSpace(model.BirthDate))
                {
                    contact.BirthDate = DateOnly.Parse(model.BirthDate);
                }
                contact = (await _repository.AddAsync(contact));
                await _unitOfWork.Commit();
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
                var contact = await _repository.Find(x => x.Id == model.GetId())
                    .Include(x => x.Label)
                    .FirstOrDefaultAsync();
                if (contact != null)
                {
                    contact.GivenName = model.GivenName;
                    contact.FamilyName = model.FamilyName;
                    contact.MobileNumber = model.MobileNumber;
                    var label = await _labelService.CreateLabelIfExisting(model.Label);
                    contact.LabelId = label.Id;
                    contact.Label = label;
                    if (!string.IsNullOrWhiteSpace(model.BirthDate))
                    {
                        contact.BirthDate = DateOnly.Parse(model.BirthDate);
                    }
                    contact.UpdatedDate = DateTimeOffset.UtcNow;
                    contact = (_repository.Update(contact));
                    await _unitOfWork.Commit();
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
                var contact = await _repository.Find(x => x.Id == model.GetId())
                    .Include(x => x.Label)
                    .FirstOrDefaultAsync();
                if (contact != null)
                {
                    contact.IsDeleted = true;
                    contact.DeletedDate = DateTimeOffset.UtcNow;
                    contact = (_repository.Update(contact));
                    await _unitOfWork.Commit();
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

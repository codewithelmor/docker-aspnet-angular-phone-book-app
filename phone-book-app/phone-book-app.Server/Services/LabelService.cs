using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using phone_book_app.Server.Data;
using phone_book_app.Server.Models;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.Services.Contracts;
using phone_book_app.Server.UnitOfWorks.Contracts;

namespace phone_book_app.Server.Services
{
    public class LabelService : ILabelService
    {
        private readonly IPhoneBookAppUnitOfWork _unitOfWork;
        private readonly ILabelRepository _repository;
        private readonly IMapper _mapper;

        public LabelService(
            IPhoneBookAppUnitOfWork unitOfWork,
            ILabelRepository repository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SelectList> AsSelectList()
        {
            try
            {
                var labels = await _repository.FindAsync(x => !x.IsDeleted);

                var items = _mapper.Map<List<SelectListItem>>(labels);

                return new SelectList(items, "Value", "Text", null, null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Label> CreateLabelIfExistingAsync(string name)
        {
            try
            {
                var existingLabel = await _repository.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
                if (existingLabel != null)
                {
                    return existingLabel;
                }
                else
                {
                    var newLabel = await _repository.AddAsync(new Label { Name = name, IsActive = true });
                    await _unitOfWork.Commit();
                    return newLabel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

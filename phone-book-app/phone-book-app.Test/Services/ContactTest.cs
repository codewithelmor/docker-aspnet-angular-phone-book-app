using AutoMapper;
using FluentAssertions;
using NSubstitute;
using phone_book_app.Server.InputModels;
using phone_book_app.Server.Mappings;
using phone_book_app.Server.Models;
using phone_book_app.Server.Repositories.Contracts;
using phone_book_app.Server.Services;
using phone_book_app.Server.Services.Contracts;
using phone_book_app.Server.UnitOfWorks.Contracts;
using System.Linq;
using System.Linq.Expressions;

namespace phone_book_app.Test.Services
{
    public class ContactTest
    {
        private readonly IPhoneBookAppUnitOfWork _unitOfWorkMock;
        private readonly IContactRepository _repositoryMock;
        private readonly ILabelService _labelServiceMock;
        private readonly IMapper _mapperMock;

        private readonly ContactService _sut;

        private static readonly ContactInputModel ContactInputModel = new()
        {
            Label = "Test",
            GivenName = "John",
            FamilyName = "Doe",
            BirthDate = "2000-01-01",
            MobileNumber = "+639171234567"
        };

        private static readonly Label LabelEntity = new()
        {
            Id = 3,
            Name = "Test",
            IsActive = true,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null,
            IsDeleted = false,
            DeletedDate = null
        };

        private static readonly Contact ContactInputEntity = new()
        {
            GivenName = "John",
            FamilyName = "Doe",
            BirthDate = DateOnly.Parse("2000-01-01"),
            MobileNumber = "+639171234567",
            LabelId = LabelEntity.Id,
            Label = LabelEntity,
            IsActive = true,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null,
            IsDeleted = false,
            DeletedDate = null
        };

        private static readonly Contact ContactOutputEntity = new()
        {
            Id = 3,
            GivenName = "John",
            FamilyName = "Doe",
            BirthDate = DateOnly.Parse("2000-01-01"),
            MobileNumber = "+639171234567",
            LabelId = LabelEntity.Id,
            Label = LabelEntity,
            IsActive = true,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null,
            IsDeleted = false,
            DeletedDate = null
        };

        private static readonly IEnumerable<Contact> ContactOutputEntities = new List<Contact>()
        {
            ContactOutputEntity
        };

        public ContactTest()
        {
            _unitOfWorkMock = Substitute.For<IPhoneBookAppUnitOfWork>();
            _repositoryMock = Substitute.For<IContactRepository>();
            _labelServiceMock = Substitute.For<ILabelService>();
            
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new ContactMapping());
            });
            _mapperMock = new Mapper(mapperConfig);

            _sut = new ContactService(
                _unitOfWorkMock,
                _repositoryMock,
                _labelServiceMock,
                _mapperMock);

            ContactInputModel.SetId(3);
        }

        [Fact]
        public async Task ListAsync_ReturnContactViewModels()
        {
            // Arrange
            _repositoryMock.FindAsync(Arg.Any<Expression<Func<Contact, bool>>>()).Returns(ContactOutputEntities);

            // Act
            var result = await _sut.ListAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateAsync_WithValidContactInputModel_ReturnContactViewModel()
        {
            // Arrange
            _labelServiceMock.CreateLabelIfExistingAsync(ContactInputModel.Label).Returns(LabelEntity);
            _repositoryMock.AddAsync(Arg.Any<Contact>()).Returns(ContactOutputEntity);

            // Act
            var result = await _sut.CreateAsync(ContactInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.GivenName.Should().NotBeNullOrWhiteSpace();
            result.FamilyName.Should().NotBeNullOrWhiteSpace();
            result.MobileNumber.Should().MatchRegex("^\\+639[0-9]{9}$");
            result.BirthDate.Should().MatchRegex("^[0-9]{4}\\-[0-9]{2}\\-[0-9]{2}$");
        }

        [Fact]
        public async Task UpdateAsync_WithValidContactInputModel_ReturnContactViewModel()
        {
            // Arrange
            _labelServiceMock.CreateLabelIfExistingAsync(ContactInputModel.Label).Returns(LabelEntity);
            _repositoryMock.FirstOrDefaultAsync(Arg.Any<Expression<Func<Contact, bool>>>()).Returns(ContactOutputEntity);
            _repositoryMock.Update(Arg.Any<Contact>()).Returns(ContactOutputEntity);

            // Act
            var result = await _sut.UpdateAsync(ContactInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Id.Should().Match(x => x == ContactOutputEntity.Id);
            result.GivenName.Should().NotBeNullOrWhiteSpace();
            result.GivenName.Should().Match(x => x == ContactOutputEntity.GivenName);
            result.FamilyName.Should().NotBeNullOrWhiteSpace();
            result.FamilyName.Should().Match(x => x == ContactOutputEntity.FamilyName);
            result.MobileNumber.Should().MatchRegex("^\\+639[0-9]{9}$");
            result.MobileNumber.Should().Match(x => x == ContactOutputEntity.MobileNumber);
            result.BirthDate.Should().MatchRegex("^[0-9]{4}\\-[0-9]{2}\\-[0-9]{2}$");
        }

        [Fact]
        public async Task DeleteAsync_WithValidBaseInputModel_ReturnContact()
        {
            // Arrange
            _repositoryMock.FirstOrDefaultAsync(Arg.Any<Expression<Func<Contact, bool>>>()).Returns(ContactOutputEntity);
            ContactOutputEntity.IsDeleted = true;
            _repositoryMock.Update(Arg.Any<Contact>()).Returns(ContactOutputEntity);

            // Act
            var result = await _sut.DeleteAsync(ContactInputModel);

            // Assert
            result.Should().NotBeNull();
            result.IsDeleted.Should().BeTrue();
            result.DeletedDate.Should().NotBeNull();
        }
    }
}

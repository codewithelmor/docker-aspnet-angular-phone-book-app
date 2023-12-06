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

namespace phone_book_app.Test.Services
{
    public class ContactTest
    {
        private readonly IPhoneBookAppUnitOfWork _unitOfWorkMock;
        private readonly IContactRepository _repositoryMock;
        private readonly ILabelService _labelServiceMock;
        private readonly IMapper _mapperMock;

        private readonly ContactService _sut;

        private static string _label = "Test";

        private static readonly ContactInputModel ContactInputModel = new()
        {
            Label = _label,
            GivenName = "John",
            FamilyName = "Doe",
            BirthDate = null,
            MobileNumber = "+639171234567"
        };

        private static readonly Label LabelEntity = new()
        {
            Id = 3,
            Name = _label,
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
            BirthDate = null,
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
            BirthDate = null,
            MobileNumber = "+639171234567",
            LabelId = LabelEntity.Id,
            Label = LabelEntity,
            IsActive = true,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null,
            IsDeleted = false,
            DeletedDate = null
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
        }

        [Fact]
        public async Task Handle_Should_ReturnContactViewModel_WhenContactInputModelIsValid()
        {
            // Arrange
            _labelServiceMock.CreateLabelIfExisting(ContactInputModel.Label).Returns(LabelEntity);
            _repositoryMock.AddAsync(Arg.Any<Contact>()).Returns(ContactOutputEntity);

            // Act
            var result = await _sut.CreateAsync(ContactInputModel);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.GivenName.Should().NotBeNullOrWhiteSpace();
            result.FamilyName.Should().NotBeNullOrWhiteSpace();
            result.MobileNumber.Should().MatchRegex("^\\+639[0-9]{9}$");
        }
    }
}

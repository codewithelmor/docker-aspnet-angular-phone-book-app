using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class LabelServiceTest
    {
        private readonly IPhoneBookAppUnitOfWork _unitOfWorkMock;
        private readonly ILabelRepository _repositoryMock;
        private readonly IMapper _mapperMock;

        private readonly LabelService _sut;

        private static readonly Label LabelOutputEntity = new()
        {
            Id = 3,
            Name = "Test",
            IsActive = true,
            CreatedDate = DateTimeOffset.UtcNow,
            UpdatedDate = null,
            IsDeleted = false,
            DeletedDate = null
        };

        private static readonly IEnumerable<Label> LabelOutputEntities = new List<Label>()
        {
            LabelOutputEntity
        };

        public LabelServiceTest()
        {
            _unitOfWorkMock = Substitute.For<IPhoneBookAppUnitOfWork>();
            _repositoryMock = Substitute.For<ILabelRepository>();
            
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new LabelMapping());
            });
            _mapperMock = new Mapper(mapperConfig);

            _sut = new LabelService(
                _unitOfWorkMock,
                _repositoryMock,
                _mapperMock);
        }

        [Fact]
        public async Task AsSelectListAsync_ReturnSelectList()
        {
            // Arrange
            _repositoryMock.FindAsync(Arg.Any<Expression<Func<Label, bool>>>()).Returns(LabelOutputEntities);

            // Act
            var result = await _sut.AsSelectListAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.First().Value.Should().Be(LabelOutputEntity.Id.ToString());
        }

        [Fact]
        public async Task CreateLabelIfExistingAsync_WithExistingLabel_ReturnLabel()
        {
            // Arrange
            _repositoryMock.FirstOrDefaultAsync(Arg.Any<Expression<Func<Label, bool>>>()).Returns(LabelOutputEntity);

            // Act
            var result = await _sut.CreateLabelIfExistingAsync(LabelOutputEntity.Name);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(LabelOutputEntity.Id);
        }

        [Fact]
        public async Task CreateLabelIfExistingAsync_WithNewLabel_ReturnLabel()
        {
            // Arrange
            _repositoryMock.FirstOrDefaultAsync(Arg.Any<Expression<Func<Label, bool>>>()).Returns(null as Label);
            _repositoryMock.AddAsync(Arg.Any<Label>()).Returns(LabelOutputEntity);

            // Act
            var result = await _sut.CreateLabelIfExistingAsync(LabelOutputEntity.Name);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(LabelOutputEntity.Id);
        }
    }
}

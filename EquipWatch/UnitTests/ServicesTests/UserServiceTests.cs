using Domain.User.Models;
using DTO.UserDTOs;
using Moq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using webapi.Services;
using AutoMapper;
using webapi.uow;

namespace EquipWatch.UnitTests.ServicesTests;

[TestFixture]
public class UserServiceTests
{
    private Mock<IConfiguration> _configurationMock;
    private UserServices _userServices;
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;
    private IConfiguration _configuration;

    [SetUp]
    public void Setup()
    {
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["JwtSettings:SecretKey"]).Returns("fakeSecretKeyForTesting");
        _userServices = new UserServices(_configurationMock.Object, _mapper, _unitOfWork);

        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"JwtSettings:SecretKey", "fakeSecretKeyForTesting"}
        });
        _configuration = configurationBuilder.Build();
    }

    [Test]
    public void MatchModelToNewUser_ShouldMatchCorrectly()
    {
        // Arrange
        var model = new CreateUserDTO
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe"
        };

        // Act
        var result = _userServices.MatchModelToNewUser(model);

        // Assert
        Assert.AreEqual(model.Email, result.Email);
        Assert.AreEqual(model.FirstName, result.FirstName);
        Assert.AreEqual(model.LastName, result.LastName);
    }

    [Test]
    public void GenerateClaims_ShouldGenerateCorrectClaims()
    {
        // Arrange
        var user = new User
        {
            Id = "123",
            UserName = "testuser",
            Email = "test@example.com"
        };

        // Act
        var result = _userServices.GenerateClaims(user);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result, Has.Some.Matches<Claim>(c => c.Type == ClaimTypes.Email && c.Value == user.Email));
        Assert.That(result, Has.Some.Matches<Claim>(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id));
        Assert.That(result, Has.Some.Matches<Claim>(c => c.Type == ClaimTypes.Name && c.Value == user.UserName));
    }

    [Test]
    public void MatchModelToExistingUser_ShouldMatchCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = "123",
            UserName = "testuser",
            Email = "test@example.com"
        };

        var model = new UpdateUserCredentialsDTO
        {
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            Email = "updated@example.com"
        };

        // Act
        _userServices.MatchModelToExistingUser(user, model);

        // Assert
        Assert.AreEqual(model.FirstName, user.FirstName);
        Assert.AreEqual(model.LastName, user.LastName);
        Assert.AreEqual(model.Email, user.Email);
    }
    [Test]
    public void GenerateJwtToken_ShouldGenerateCorrectToken()
    {
        // Arrange
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, "test@example.com"),
            new Claim(ClaimTypes.NameIdentifier, "123"),
            new Claim(ClaimTypes.Name, "testuser"),
        };

        // Act
        var result = _userServices.GenerateJwtToken(claims);

        // Assert
        Assert.IsNotNull(result);
    }
}
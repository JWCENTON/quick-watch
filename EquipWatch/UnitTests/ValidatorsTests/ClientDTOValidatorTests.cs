using FluentValidation.TestHelper;
using webapi.Validators;
using DTO.ClientDTOs;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class ClientDTOValidatorTests
    {
        private CreateClientDTOValidator _createValidator;
        private FullClientDTOValidator _fullValidator;
        private PartialClientDTOValidator _partialValidator;
        private UpdateClientDTOValidator _updateValidator;

        [SetUp]
        public void Setup()
        {
            _createValidator = new CreateClientDTOValidator();
            _fullValidator = new FullClientDTOValidator();
            _partialValidator = new PartialClientDTOValidator();
            _updateValidator = new UpdateClientDTOValidator();
        }

        [Test]
        public void CreateValidator_ShouldHaveNoErrors()
        {
            var dto = new CreateClientDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123456789",
                ContactAddress = "123 Main St",
                CompanyId = Guid.NewGuid().ToString()
            };

            var result = _createValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void FullValidator_ShouldHaveNoErrors()
        {
            var dto = new FullClientDTO
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123456789",
                ContactAddress = "123 Main St",
                CompanyId = Guid.NewGuid().ToString()
            };

            var result = _fullValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void PartialValidator_ShouldHaveNoErrors()
        {
            var dto = new PartialClientDTO
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123456789",
                ContactAddress = "123 Main St"
            };

            var result = _partialValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void UpdateValidator_ShouldHaveNoErrors()
        {
            var dto = new UpdateClientDTO
            {
                CompanyId = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123456789",
                ContactAddress = "123 Main St"
            };

            var result = _updateValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void CreateValidator_ShouldFail_WhenFirstNameIsEmpty()
        {
            var dto = new CreateClientDTO { FirstName = string.Empty };
            var result = _createValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("First name cannot be empty.", result.Errors.First().ErrorMessage);
        }

        [Test]
        public void CreateValidator_ShouldFail_WhenEmailIsInvalid()
        {
            var dto = new CreateClientDTO { Email = "invalid-email" };
            var result = _createValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid email format.", result.Errors.First().ErrorMessage);
        }

        [Test]
        public void CreateValidator_ShouldFail_WhenLastNameIsEmpty()
        {
            var dto = new CreateClientDTO { LastName = string.Empty };
            var result = _createValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Last name cannot be empty.", result.Errors.First().ErrorMessage);
        }

        [Test]
        public void CreateValidator_ShouldFail_WhenPhoneNumberIsInvalid()
        {
            var dto = new CreateClientDTO { PhoneNumber = "12345" };
            var result = _createValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid phone number format. Phone number must be 9 digits.", result.Errors.First().ErrorMessage);
        }

        [Test]
        public void CreateValidator_ShouldFail_WhenContactAddressIsTooLong()
        {
            var dto = new CreateClientDTO { ContactAddress = new string('A', 51) };
            var result = _createValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Contact address cannot exceed 50 characters.", result.Errors.First().ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldPass_WhenOptionalFieldsAreNull()
        {
            var dto = new UpdateClientDTO
            {
                FirstName = null,
                LastName = null,
                Email = null,
                PhoneNumber = null,
                ContactAddress = null
            };
            var result = _updateValidator.Validate(dto);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void FullValidator_ShouldFail_WhenIdIsInvalid()
        {
            var dto = new FullClientDTO { Id = "invalid-guid" };
            var result = _fullValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid Client ID.", result.Errors.First().ErrorMessage);
        }
    }
}
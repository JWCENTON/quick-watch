using NUnit.Framework;
using webapi.Validators;
using DTO.UserDTOs;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    public class UserDTOValidatorTests
    {
        [Test]
        public void FullUserValidator_ShouldFail_WhenIdIsInvalid()
        {
            var validator = new FullUserDTOValidator();
            var dto = new FullUserDTO
            {
                Id = "InvalidId",
                Email = "test@example.com",
                Password = "Password1!",
                UserName = "UserName"
            };

            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid User ID format.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void CreateUserValidator_ShouldFail_WhenFirstNameIsEmpty()
        {
            var validator = new CreateUserDTOValidator();
            var dto = new CreateUserDTO
            {
                FirstName = "",
                LastName = "LastName",
                Email = "test@example.com",
                Password = "Password1!"
            };

            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("First name cannot be empty.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void LoginUserValidator_ShouldPass_WhenAllFieldsAreValid()
        {
            var validator = new LoginUserDTOValidator();
            var dto = new LoginUserDTO
            {
                Email = "test@example.com",
                Password = "Password1!"
            };

            var result = validator.Validate(dto);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void UpdateUserValidator_ShouldFail_WhenEmailIsInvalid()
        {
            var validator = new UpdateUserDTOValidator();
            var dto = new UpdateUserDTO
            {
                Email = "invalid-email",
                UserName = "UserName"
            };

            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Email must contain @.", result.Errors[0].ErrorMessage);
        }
    }
}

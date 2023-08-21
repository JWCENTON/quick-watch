using FluentValidation.TestHelper;
using NUnit.Framework;
using webapi.Validators;
using DTO.CheckInDTOs;
using System;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class CheckInDTOValidatorTests
    {
        private CreateCheckInDTOValidator _createValidator;
        private FullCheckInDTOValidator _fullValidator;
        private UpdateCheckInDTOValidator _updateValidator;

        [SetUp]
        public void Setup()
        {
            _createValidator = new CreateCheckInDTOValidator();
            _fullValidator = new FullCheckInDTOValidator();
            _updateValidator = new UpdateCheckInDTOValidator();
        }

        [Test]
        public void CreateValidator_ShouldHaveNoErrors()
        {
            var dto = new CreateCheckInDTO
            {
                UserId = Guid.NewGuid().ToString(),
                EquipmentId = Guid.NewGuid().ToString(),
                Time = DateTime.Now
            };

            var result = _createValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void FullValidator_ShouldHaveNoErrors()
        {
            var dto = new FullCheckInDTO
            {
                UserId = Guid.NewGuid().ToString(),
                EquipmentId = Guid.NewGuid().ToString(),
                Time = DateTime.Now,
                Id = Guid.NewGuid().ToString()
            };

            var result = _fullValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void UpdateValidator_ShouldHaveNoErrors()
        {
            var dto = new UpdateCheckInDTO
            {
                UserId = Guid.NewGuid().ToString(),
                EquipmentId = Guid.NewGuid().ToString()
            };

            var result = _updateValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}

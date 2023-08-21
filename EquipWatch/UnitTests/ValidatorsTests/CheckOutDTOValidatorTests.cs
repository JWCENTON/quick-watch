using FluentValidation.TestHelper;
using NUnit.Framework;
using webapi.Validators;
using DTO.CheckOutDTOs;
using System;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class CheckOutDTOValidatorTests
    {
        private CreateCheckOutDTOValidator _createValidator;
        private FullCheckOutDTOValidator _fullValidator;
        private UpdateCheckOutDTOValidator _updateValidator;

        [SetUp]
        public void Setup()
        {
            _createValidator = new CreateCheckOutDTOValidator();
            _fullValidator = new FullCheckOutDTOValidator();
            _updateValidator = new UpdateCheckOutDTOValidator();
        }

        [Test]
        public void CreateValidator_ShouldHaveNoErrors()
        {
            var dto = new CreateCheckOutDTO
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
            var dto = new FullCheckOutDTO
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
            var dto = new UpdateCheckOutDTO
            {
                UserId = Guid.NewGuid().ToString(),
                EquipmentId = Guid.NewGuid().ToString()
            };

            var result = _updateValidator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }
        
    }
}

using NUnit.Framework;
using webapi.Validators;
using DTO.BookedEquipmentDTOs;
using FluentValidation;
using System;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class BookedEquipmentDTOValidatorTests
    {
        private UpdateBookedEquipmentDTOValidator _updateValidator;

        [SetUp]
        public void Setup()
        {
            _updateValidator = new UpdateBookedEquipmentDTOValidator();
        }

        [Test]
        public void UpdateBookedEquipmentDTOValidator_NullCommissionIdAndEquipmentId_ShouldPass()
        {
            // Arrange
            var dto = new UpdateBookedEquipmentDTO
            {
                CommissionId = null,
                EquipmentId = null
            };

            // Act
            var result = _updateValidator.Validate(dto);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void UpdateBookedEquipmentDTOValidator_ValidCommissionIdAndEquipmentId_ShouldPass()
        {
            // Arrange
            var dto = new UpdateBookedEquipmentDTO
            {
                CommissionId = Guid.NewGuid().ToString(),
                EquipmentId = Guid.NewGuid().ToString()
            };

            // Act
            var result = _updateValidator.Validate(dto);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void UpdateBookedEquipmentDTOValidator_InvalidCommissionId_ShouldFail()
        {
            // Arrange
            var dto = new UpdateBookedEquipmentDTO
            {
                CommissionId = "InvalidCommissionId",
                EquipmentId = Guid.NewGuid().ToString()
            };

            // Act
            var result = _updateValidator.Validate(dto);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void UpdateBookedEquipmentDTOValidator_InvalidEquipmentId_ShouldFail()
        {
            // Arrange
            var dto = new UpdateBookedEquipmentDTO
            {
                CommissionId = Guid.NewGuid().ToString(),
                EquipmentId = "InvalidEquipmentId"
            };

            // Act
            var result = _updateValidator.Validate(dto);

            // Assert
            Assert.IsFalse(result.IsValid);
        }
    }
}

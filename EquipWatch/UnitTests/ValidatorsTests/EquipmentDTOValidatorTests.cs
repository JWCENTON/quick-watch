using webapi.Validators;
using DTO.EquipmentDTOs;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class EquipmentDTOValidatorTests
    {
        private CreateEquipmentDTOValidator _createValidator;
        private FullEquipmentDTOValidator _fullValidator;
        private UpdateEquipmentDTOValidator _updateValidator;

        [SetUp]
        public void Setup()
        {
            _createValidator = new CreateEquipmentDTOValidator();
            _fullValidator = new FullEquipmentDTOValidator();
            _updateValidator = new UpdateEquipmentDTOValidator();
        }

        [Test]
        public void CreateValidator_ShouldFail_WhenSerialNumberIsEmpty()
        {
            var dto = new CreateEquipmentDTO { SerialNumber = string.Empty };
            var result = _createValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Serial number cannot be empty.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void FullValidator_ShouldFail_WhenIdIsInvalid()
        {
            var dto = new FullEquipmentDTO { Id = "invalid-id", Condition = 3 };
            var result = _fullValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid Equipment ID format.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldFail_WhenSerialNumberIsTooLong()
        {
            var dto = new UpdateEquipmentDTO { SerialNumber = new string('a', 31) };
            var result = _updateValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Serial number cannot exceed 30 characters.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldFail_WhenCategoryIsTooLong()
        {
            var dto = new UpdateEquipmentDTO { Category = new string('a', 51) };
            var result = _updateValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Category cannot exceed 50 characters.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldFail_WhenLocationIsTooLong()
        {
            var dto = new UpdateEquipmentDTO { Location = new string('a', 51) };
            var result = _updateValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Location cannot exceed 50 characters.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldFail_WhenConditionIsLessThanOne()
        {
            var dto = new UpdateEquipmentDTO { Condition = 0 };
            var result = _updateValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Condition cannot be less than 1.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldFail_WhenConditionIsGreaterThanFive()
        {
            var dto = new UpdateEquipmentDTO { Condition = 6 };
            var result = _updateValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Condition cannot be higher than 5.", result.Errors[0].ErrorMessage);
        }
    }
}

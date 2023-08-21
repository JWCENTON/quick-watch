using webapi.Validators;
using DTO.CommissionDTOs;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    [TestFixture]
    public class CommissionDTOValidatorTests
    {
        private CreateCommissionDTOValidator _createValidator;
        private FullCommissionDTOValidator _fullValidator;
        private UpdateCommissionDTOValidator _updateValidator;

        [SetUp]
        public void Setup()
        {
            _createValidator = new CreateCommissionDTOValidator();
            _fullValidator = new FullCommissionDTOValidator();
            _updateValidator = new UpdateCommissionDTOValidator();
        }

        [Test]
        public void CreateValidator_ShouldFail_WhenLocationIsEmpty()
        {
            var dto = new CreateCommissionDTO { Location = string.Empty };
            var result = _createValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Location cannot be empty.", result.Errors.First().ErrorMessage);
        }

        [Test]
        public void FullValidator_ShouldFail_WhenEndTimeIsBeforeStartTime()
        {
            var dto = new FullCommissionDTO { StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(-1) };
            var result = _fullValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("End time must be same or after start time.", result.Errors.First().ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldPass_WhenOptionalFieldsAreNull()
        {
            var dto = new UpdateCommissionDTO
            {
                CompanyId = null,
                ClientId = null,
                Location = null,
                Description = null,
                Scope = null,
                StartTime = null,
                EndTime = null
            };
            var result = _updateValidator.Validate(dto);
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void FullValidator_ShouldFail_WhenIdIsInvalid()
        {
            var dto = new FullCommissionDTO
            {
                Id = "invalid-guid",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };
            var result = _fullValidator.Validate(dto);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Invalid Commission ID.", result.Errors.First(e => e.PropertyName == "Id").ErrorMessage);
        }
    }
}
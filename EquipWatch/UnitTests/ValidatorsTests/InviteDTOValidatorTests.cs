using NUnit.Framework;
using webapi.Validators;
using DTO.InviteDTOs;
using System;
using Domain.Invite;

namespace EquipWatch.UnitTests.ValidatorsTests
{
    public class InviteDTOValidatorTests
    {
        [Test]
        public void FullValidator_ShouldFail_WhenCreatedAtIsInFuture()
        {
            var validator = new FullInviteDTOValidator();
            var dto = new FullInviteDTO
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Status = Status.Sent,
                CreatedAt = DateTime.UtcNow.AddMinutes(1)
            };

            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Created at date cannot be in the future.", result.Errors[0].ErrorMessage);
        }

        [Test]
        public void UpdateValidator_ShouldFail_WhenStatusIsInvalid()
        {
            var validator = new UpdateInviteDTOValidator();
            var dto = new UpdateInviteDTO
            {
                Status = (Status)999
            };

            var result = validator.Validate(dto);

            Assert.IsFalse(result.IsValid);
            Assert.AreEqual($"Invalid status. Status must be one of the following values: {string.Join(", ", Enum.GetNames(typeof(Status)))}", result.Errors[0].ErrorMessage);
        }
    }
}

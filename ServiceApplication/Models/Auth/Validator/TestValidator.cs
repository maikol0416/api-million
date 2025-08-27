using Domain.Entities;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Validator
{
    public class TestValidator : AbstractValidator<TestDto>
    {
        public TestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode($"NameEmpty")
            .WithMessage(x => x.Name)
                 .WithName(nameof(Test.Name));
        }
    }
}

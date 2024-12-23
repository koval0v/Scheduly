using FluentValidation;
using TokenService.Models;

namespace Services.Validation.FluentValidation
{
    public class UserModelValidator : AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(100)
                .EmailAddress()
                .WithMessage("Maximum 100 characters.");
        }
    }
}

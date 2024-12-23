
using FluentValidation;
using TokenService.Models;

namespace Services.Validation.FluentValidation
{
    public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$")
                .WithMessage("Password must contain 8-16 characters, at least one letter and one number.");

            RuleFor(x => x.PasswordRepeat)
                .Equal(x => x.Password)
                .WithMessage("Passwords should be equal.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(100)
                .WithMessage("Maximum 100 characters.");
        }
    }
}

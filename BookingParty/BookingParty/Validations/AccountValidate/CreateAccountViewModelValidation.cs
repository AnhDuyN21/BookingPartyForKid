using Application.ViewModel.AccountDTO;
using Application.ViewModel.RegisterAccountDTO;
using Domain.Entities;
using FluentValidation;

namespace WebAPI.Validations.AccountValidate
{
    public class CreateAccountViewModelValidation : AbstractValidator<CreatedAccountDTO> 
    {
        public CreateAccountViewModelValidation()
        {
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Must(email => email.EndsWith("@gmail.com"))
                .WithMessage("Email must have the extension @gmail.com");
            RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^0[0-9]{9}$")
                .WithMessage("The phone number must have 10 digits and start with 0");
            RuleFor(x => x.PasswordHash).NotEmpty().MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long");
        }
    }
}

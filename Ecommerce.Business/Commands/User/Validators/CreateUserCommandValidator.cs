using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.User.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName Cannot be empty")
                .NotNull()
                .WithMessage("FirstName Cannot be null value");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName Cannot be empty")
                .NotNull()
                .WithMessage("LastName Cannot be null value");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username Cannot be empty")
                .NotNull()
                .WithMessage("Username Cannot be null value")
                .MaximumLength(30)
                .WithMessage("Username cannot exceed 30 characters.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password Cannot be empty")
                .NotNull()
                .WithMessage("Password Cannot be null value")
                .MaximumLength(30)
                .WithMessage("Password cannot exceed 30 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email Cannot be empty")
                .NotNull()
                .WithMessage("Email Cannot be null value")
                .EmailAddress()
                .WithMessage("Invalid email format");
        }
    }
}

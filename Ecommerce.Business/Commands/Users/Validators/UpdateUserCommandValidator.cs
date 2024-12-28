using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business.Commands.Users.Validators
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .MaximumLength(30)
                .WithMessage("FirstName cannot exceed 30 characters.");

            RuleFor(x => x.LastName)
                .MaximumLength(30)
                .WithMessage("LastName cannot exceed 30 characters.");

            RuleFor(x => x.Password)
                .MaximumLength(30)
                .WithMessage("Password cannot exceed 30 characters.");

        }
    }
}

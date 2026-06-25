using Domain.Models.Errors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Register
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(x => x.Email)
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
                .WithMessage(ErrorModel.InvalidEmail);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ErrorModel.EmailRequired);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ErrorModel.FirstNameRequired);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(ErrorModel.LastNameRequired);

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage(ErrorModel.PasswordMinLength)
                .NotEmpty()
                .WithMessage(ErrorModel.PasswordRequired);
        }
    }
}

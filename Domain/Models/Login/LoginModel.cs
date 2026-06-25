using Domain.Models.Errors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Login
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Username)
         .NotEmpty()
         .WithMessage(ErrorModel.UsernameRequired);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ErrorModel.PasswordRequired);
        }
    }
}

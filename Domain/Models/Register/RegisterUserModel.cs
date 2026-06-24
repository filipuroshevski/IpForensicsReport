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
            RuleFor(x => x.Email).Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").WithMessage("The Email is not valid");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The Email is required field");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("The First Name is required field");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("The Last Name is required field");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("The Password must be greather than 6 characters")
               .NotEmpty().WithMessage("The Password is required field");
        }
    }
}

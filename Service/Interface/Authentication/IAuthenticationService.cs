using Domain.Models.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.Authentication
{
    public interface IAuthenticationService
    {
        Task RegisterUser(RegisterUserModel model);
    }
}

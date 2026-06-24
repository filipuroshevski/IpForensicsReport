using Data.Entities.Users;
using Domain.Models.Register;
using Microsoft.AspNetCore.Identity;
using Service.Interface.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Service.Implementation.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        #endregion

        #region Ctor

        public AuthenticationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method for registering a new user. It checks if the user already exists by email, and if not, creates a new user with the provided details.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public async Task RegisterUser(RegisterUserModel model)
        {
            var userExist = (await _userManager.FindByEmailAsync(model.Email));
            if (userExist != null)
            {
                throw new ApplicationException("The user with this email already exists");
            }

            User user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
            };

            await _userManager.CreateAsync(user, model.Password);
        }
        #endregion
    }
}

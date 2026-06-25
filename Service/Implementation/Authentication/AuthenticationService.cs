using Data.Entities.Users;
using Domain.Models.Errors;
using Domain.Models.Login;
using Domain.Models.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interface.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Implementation.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        #endregion

        #region Ctor

        public AuthenticationService(UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
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
                throw new ApplicationException(ErrorModel.UserAlreadyExist);
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

        /// <summary>
        /// Method for logging in a user. 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);

            if (user == null)
            {
                throw new ApplicationException(ErrorModel.UserNotFound);
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                throw new ApplicationException(ErrorModel.InvalidPassword);
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = GenerateToken(
                user.Id,
                user.Email!,
                user.UserName!,
                roles
            );

            return token;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generate token for the authenticated user with claims including userId, email, username, and roles. The token is signed using HMAC SHA256 algorithm and has an expiration time of 2 hours.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        private string GenerateToken(string userId, string email, string username, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
                new Claim("username", username)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(
                    Convert.ToDouble(_configuration["MS:TokenExpiredHours"])
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}

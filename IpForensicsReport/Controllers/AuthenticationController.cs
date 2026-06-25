using Data.Entities.Users;
using Domain.Models.Errors;
using Domain.Models.Helper.ModalState;
using Domain.Models.Login;
using Domain.Models.Register;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interface.Authentication;

namespace IpForensicsReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Ctor
        public AuthenticationController(UserManager<User> userManager, IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }
        #endregion

        #region Public Methods

        [AllowAnonymous]
        [HttpPost]
        [Route("registeruser")]
        public async Task<IActionResult> RegisterUser(RegisterUserModel model)
        {
            try
            {
                var validator = new RegisterUserModelValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                await _authenticationService.RegisterUser(model);


                return new JsonResult(true);

            }
            catch (Exception ex)
            {
                var error = new ValidationResult();

                switch (ex.Message)
                {
                    case ErrorModel.UserAlreadyExist:
                        error.Errors.Add(new FluentValidation.Results.ValidationFailure("UserAlreadyExist", ErrorModel.UserAlreadyExist));
                        break;
                    default:
                        error.Errors.Add(new ValidationFailure(
                            "SystemErrorOccured",
                            "A system error has occurred"));
                        break;
                }

                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var validator = new LoginModelValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var token=await _authenticationService.Login(model);


                return new JsonResult(new { token });

            }
            catch (Exception ex)
            {
                var error = new ValidationResult();

                switch (ex.Message)
                {
                    case ErrorModel.UserNotFound:
                        error.Errors.Add(new FluentValidation.Results.ValidationFailure("UserNotFound", ErrorModel.UserNotFound));
                        break;
                    case ErrorModel.InvalidPassword:
                        error.Errors.Add(new FluentValidation.Results.ValidationFailure("InvalidPassword", ErrorModel.InvalidPassword));
                        break;
                    default:
                        error.Errors.Add(new ValidationFailure(
                            "SystemErrorOccured",
                            "A system error has occurred"));
                        break;
                }

                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        #endregion
    }
}

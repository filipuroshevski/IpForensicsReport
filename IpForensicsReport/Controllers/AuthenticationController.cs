using Data.Entities.Users;
using Domain.Models.Helper.ModalState;
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
                    case "TheUserWithThisEmailExist":
                        error.Errors.Add(new ValidationFailure(
                            "Email",
                            "The user with this email already exists"));
                        break;

                    default:
                        error.Errors.Add(new ValidationFailure(
                            "SystemErrorOccured",
                            "A system error has occurred"));
                        break;
                }

                return BadRequest(ModalStateHelper.ModalStateException(error));
            }
        }

        #endregion
    }
}

using Domain.Models.Errors;
using Domain.Models.Helper.ModalState;
using Domain.Models.IpForensicsReports.GenerateReport;
using Domain.Models.IpForensicsReports.ReportList;
using Domain.Models.Register;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface.IpForensicsReport;
using System.Security.Claims;

namespace IpForensicsReport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpForensicsReportController : ControllerBase
    {
        #region Fields
        private readonly IIpForensicsReportService _ipForensicsReportService;
        #endregion

        #region Ctor
        public IpForensicsReportController(IIpForensicsReportService ipForensicsReportService)
        {
            _ipForensicsReportService = ipForensicsReportService;
        }
        #endregion

        #region Public Methods
        [Authorize]
        [HttpPost]
        [Route("generatereport")]
        public async Task<IActionResult> GenerateReport(GenerateReportModel model)
        {
            try
            {
                var validator = new GenerateReportModelValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _ipForensicsReportService.GenerateReport(model, userId);


                return new JsonResult(true);

            }
            catch (Exception ex)
            {
                var error = new ValidationResult();

                switch (ex.Message)
                {
                    case ErrorModel.IpAddressInvalid:
                        error.Errors.Add(new FluentValidation.Results.ValidationFailure("IpAddressInvalid", ErrorModel.IpAddressInvalid));
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


        [Authorize]
        [HttpPost]
        [Route("getallreports")]
        public async Task<IActionResult> GetAllReports(SearchReportModel searchModel)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
               
                var viewModel = await _ipForensicsReportService.GetAllReports(searchModel, userId);
                return new JsonResult(viewModel);

            }
            catch (Exception ex)
            {
                var error = new ValidationResult();

                switch (ex.Message)
                {
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

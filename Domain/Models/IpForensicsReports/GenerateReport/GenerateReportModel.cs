using Domain.Models.Errors;
using Domain.Models.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Models.IpForensicsReports.GenerateReport
{
    public class GenerateReportModel
    {
        public string IpAddress { get; set; }
    }

    public class GenerateReportModelValidator : AbstractValidator<GenerateReportModel>
    {
        public GenerateReportModelValidator()
        {
            RuleFor(x => x.IpAddress)
         .NotEmpty()
         .WithMessage(ErrorModel.IpAddressRequired).Must(BeValidIpAddress)
            .WithMessage(ErrorModel.IpAddressInvalid); ;
        }

        private bool BeValidIpAddress(string ipAddress)
        {
            return !string.IsNullOrWhiteSpace(ipAddress)
                && IPAddress.TryParse(ipAddress, out _);
        }
    }
}

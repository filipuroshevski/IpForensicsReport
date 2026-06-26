using Domain.Models.IpForensicsReports.GenerateReport;
using Domain.Models.IpForensicsReports.ReportList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.IpForensicsReport
{
    public interface IIpForensicsReportService
    {
        Task GenerateReport(GenerateReportModel model, string userId);
        Task<ReportViewModel> GetAllReports(SearchReportModel searchReportModel, string userId);
    }
}

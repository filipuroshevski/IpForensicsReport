using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.IpForensicsReports.ReportList
{
    public class ReportViewModel
    {
        public SearchReportModel SearchReportModel { get; set; }
        public List<ReportBaseModel> ReportBaseModels { get; set; }
    }
    public class ReportBaseModel
    {
        public int Id { get; set; }

        public string IpAddress { get; set; }
        public string AbuseConfidenceScore { get; set; }
        public string TotalReports { get; set; }
        public string LastReportedDate { get; set; }

        public string Continent { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }

        public string Mobile { get; set; }
        public string Proxy { get; set; }
        public string Hosting { get; set; }
        public string Tor { get; set; }

        public string CreatedDate { get; set; }
    }
}

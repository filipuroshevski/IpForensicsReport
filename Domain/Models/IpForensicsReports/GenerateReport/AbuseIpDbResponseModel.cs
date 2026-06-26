using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.IpForensicsReports.GenerateReport
{
    public class AbuseIpDbResponseModel
    {
        public AbuseIpDbDataModel Data { get; set; }
    }

    public class AbuseIpDbDataModel
    {
        public int AbuseConfidenceScore { get; set; }
        public int TotalReports { get; set; }
        public DateTime? LastReportedAt { get; set; }
    }
}

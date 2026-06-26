using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.IpForensicsReports.GenerateReport
{
    public class GenerateReportResultModel
    {
        public string IpAddress { get; set; }

        // Reputation
        public int AbuseConfidenceScore { get; set; }
        public int TotalReports { get; set; }
        public DateTime? LastReportedDate { get; set; }

        // Geo data
        public string Continent { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }

        // Network attributes
        public bool Mobile { get; set; }
        public bool Proxy { get; set; }
        public bool Hosting { get; set; }
        public bool Tor { get; set; }
    }
}

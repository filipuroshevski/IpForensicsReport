using Data.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.IpForensicsReports
{
    public class IpForensicsReport : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string IpAddress { get; set; } = null!;
        public string AbuseConfidenceScore { get; set; } = null!;
        public string TotalReports { get; set; } = null!;
        public string LastReportedDate { get; set; } = null!;
        public string Continent { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Mobile { get; set; } = null!;
        public string Proxy { get; set; } = null!;
        public string Hosting { get; set; } = null!;
        public string Tor { get; set; } = null!;
        public string CreatedDate { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.IpForensicsReports.GenerateReport
{
    public class IpApiResponseModel
    {
        public string Status { get; set; }
        public string Continent { get; set; }
        public string Country { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public bool Mobile { get; set; }
        public bool Proxy { get; set; }
        public bool Hosting { get; set; }
    }
}

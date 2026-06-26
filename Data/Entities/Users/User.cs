
using Data.Entities.IpForensicsReports;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities.Users
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public IList<IpForensicsReport> IpForensicsReports { get; set; } = new List<IpForensicsReport>();
    }
}

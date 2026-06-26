using Microsoft.AspNetCore.DataProtection;
using Service.Interface.Common.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Implementation.Common.DataProtection
{
    public class DataProtectionService : IDataProtectionService
    {
        #region Fields
        private readonly IDataProtector _protector;
        #endregion

        #region Ctor
        public DataProtectionService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("IpForensicsReportProtector");
        }
        #endregion

        #region Public Methods
        public string Protect(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return _protector.Protect(input);
        }

        public string Unprotect(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return _protector.Unprotect(input);
        }
        #endregion
    }
}

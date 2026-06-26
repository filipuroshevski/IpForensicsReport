using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.Common.DataProtection
{
    public interface IDataProtectionService
    {
        string Protect(string input);
        string Unprotect(string input);
    }
}

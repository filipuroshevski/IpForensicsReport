using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface.Common
{
    public interface ICommonService
    {
        int CalculateTotalPages(int numberOfRecords, int pageSize);
    }
}

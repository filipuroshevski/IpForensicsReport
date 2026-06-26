
using Service.Interface.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Implementation.Common
{
    public class CommonService : ICommonService
    {
        #region Fields
       
        #endregion

        #region Ctor

        public CommonService()
        {

        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Calculates the total pages.
        /// </summary>
        /// <param name="numberOfRecords">The number of records.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public int CalculateTotalPages(int numberOfRecords, int pageSize)
        {
            int result;
            int totalPages;

            Math.DivRem(numberOfRecords, pageSize, out result);

            if (result > 0)
                totalPages = (int)((numberOfRecords / pageSize)) + 1;
            else
                totalPages = (int)(numberOfRecords / pageSize);

            return totalPages;

        }
        #endregion
    }
}


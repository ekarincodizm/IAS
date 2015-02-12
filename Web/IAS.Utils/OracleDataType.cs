using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public static class OracleDataType
    {
        public static string ToOracleDateType(this DateTime date)
        {
            string oracleDate = date.Year.ToString("0000") + date.ToString("MMdd");
            return string.Format("to_date('{0}','yyyy/mm/dd')", oracleDate);
        }
    }
}
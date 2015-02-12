using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class PhaseCurrencyAmount
    {
        public static Decimal Phase(String source)
        {
            Decimal result;
            if (Decimal.TryParse(source, out result))
            {
                return result;
            }
            else return 0m;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class EducationCodeHelper
    {
        public static String Phase(String source) {
            Int32 result;
            if (Int32.TryParse(source, out result))
            {
                return result.ToString("00");
            }
            else {
                return "0";
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class PhaseAppliantCodeHelper
    {
        public static Int16 Phase(String source)
        {
            Int16 result;
            if (Int16.TryParse(source, out result))
            {
                return result;
            }
            else return 0;

        }
    }
}
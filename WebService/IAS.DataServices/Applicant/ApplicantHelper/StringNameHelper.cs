using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class StringNameHelper
    {
        public static Boolean Validate(String source) {
            Regex reg = new Regex(@"^[ก-ฮ| |a-zA-Z|\p{L}|\p{M}]{1,50}?$");

            return reg.IsMatch(source);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class PhaseDateHelper
    {
        public static DateTime PhaseToDate(String source)
        {
            string[] format = { "dd/M/yyyy" };
            DateTime date;

            if (!DateTime.TryParseExact(source, format, System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"), System.Globalization.DateTimeStyles.None, out date))
            {
                return DateTime.MinValue;
            }
            return date;

        }
        public static DateTime? PhaseToDateNull(String source)
        {
            string[] format = { "dd/M/yyyy" };
            DateTime date;

            if (!DateTime.TryParseExact(source, format, System.Globalization.CultureInfo.CreateSpecificCulture("th-TH"), System.Globalization.DateTimeStyles.None, out date))
            {
                if (String.IsNullOrEmpty(source))
                    return null;
                else return  DateTime.MinValue;
            }
            return date;

        }
    }
}
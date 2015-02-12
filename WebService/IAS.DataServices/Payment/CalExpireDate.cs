using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment
{
    public static class CalExpireDate
    {
        public static DateTime? CalculateExpireDate(this DateTime currentdate) {

            return currentdate.AddDays(1);
        } 
    }
}
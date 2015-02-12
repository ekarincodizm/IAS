using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions; 

namespace IAS.DataServices.Exam.Helpers
{
    public class ExamTypeValue
    {
        public bool IsAlphaNumeric(String str)
        {
            Regex regexAlphaNum = new Regex("[^0-9]");

            return !regexAlphaNum.IsMatch(str);
        }
    }
}
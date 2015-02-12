using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Exam.Helpers
{
    public class ExamSubStringHelper
    {
        public static String Get(String text, Int32 index, Int32 length) 
        {
            return (text.Length > (index + length - 1)) ? text.Substring(index, length) : "";
        }
    }
}
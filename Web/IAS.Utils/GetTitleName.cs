using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class GetTitleName
    {
        public static string GetSex(string titleName)
        {
            List<string> TF = new List<string>() { "หญิง", "นาง", "แม่ชี", "Mrs.", "Ms.", "Mr." };
            string sex = string.Empty;
            try
            {
                if ((titleName != null) && (titleName != ""))
                {
                    if ((titleName.Contains("หญิง")
                        || titleName.Contains("นาง")
                        || titleName.Contains("แม่ชี")
                        || titleName.Equals("Mrs.")
                        || titleName.Equals("Ms.")
                        || titleName.Equals("Miss")) == true)
                    {
                        sex = "F";
                    }
                    else
                    {
                        if (titleName.Contains("หม่อม") == true)
                        {
                            sex = "";
                        }
                        else if (titleName.Contains("ด็อกเตอร์") == true)
                        {
                            sex = "";
                        }
                        else
                        {
                            sex = "M";
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                sex = ex.Message;
            }

            return sex;

        }

        public static string GetTitleNameFromSex(string sex)
        {
            List<string> TF = new List<string>() { "หญิง", "นาง", "แม่ชี", "Mrs.", "Ms.", "Mr." };
            string Title = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                Title = ex.Message;
            }
            return Title;
        }

    }
}

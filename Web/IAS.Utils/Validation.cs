using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IAS.Utils
{
    public class Validation
    {
        /// <summary>
        /// DateTime Compare
        /// Result ได้ -1 If  Time_1 น้อยกว่า  Time_2
        /// Result ได้ 0  If  Time_1 เท่ากับ Time_2
        /// Result ได้ 1  If  Time_1 มากกว่า Time_2
        /// How to use Compare?
        /// 1.เวลาปัจจุบัน เทียบกับ เวลาที่เลือก
        /// Coding BY Natta
        /// </summary>
        //public DTO.ResponseMessage<bool> DateValidation(string birthDay)
        //{
        //    DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

        //    if ((birthDay != null) && (birthDay != ""))
        //    {
        //        DateTime currDate = DateTime.Now;
        //        DateTime dateFromCtrl = Convert.ToDateTime(birthDay);

        //        string currDateFormat = String.Format("{0:dd/MM/yyy}", currDate).ToString();
        //        string birthDateFormat = String.Format("{0:dd/MM/yyy}", dateFromCtrl).ToString();
        //        DateTime currTime = DateTime.Parse(currDateFormat);
        //        DateTime birthTime = DateTime.Parse(birthDateFormat);

        //        int dateCompare = DateTime.Compare(birthTime, currTime);
        //        //BirthDay < CurrentTime
        //        if (dateCompare == -1)
        //        {
        //            res.ResultMessage = true;
        //        }
        //        //BirthDay == CurrentTime
        //        if (dateCompare == 0)
        //        {
        //            res.ErrorMsg = SysMessage.RegBirthDayValidationF;
        //            res.ResultMessage = false;
        //        }
        //        //BirthDay > CurrentTime
        //        if (dateCompare == 1)
        //        {
        //            res.ErrorMsg = SysMessage.RegBirthDayValidationF;
        //            res.ResultMessage = false;
        //        }


        //    }
        //    else
        //    {
        //        res.ErrorMsg = "กรุณาตรวจสอบความถูกต้องวันเกิด";
        //        res.ResultMessage = false;
        //    }

        //    return res;
        //}


        /// <summary>
        /// RequiredFieldValidator & RegularExpressionValidator Validation Function
        /// Added new & Last Update 31/10/2556
        /// by Nattapong
        /// </summary>
        /// <param name="registrationType"></param>
        /// <param name="registration"></param>
        //public DTO.ResponseMessage<bool> ControlValidationBeforeSubmit(Page child)
        //{
        //    DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
        //    StringBuilder strBuild = new StringBuilder();

        //    this.Page.Validate(this.GroupValidation);
        //    if (this.Page.IsValid)
        //    {
        //        res.ResultMessage = true;
        //    }
        //    else
        //    {
        //        if (child.Validators.Count > 0)
        //        {
        //            for (int i = 0; i < child.Validators.Count; i++)
        //            {
        //                string nameType = child.Validators[i].GetType().Name;
        //                if (nameType == "RequiredFieldValidator")
        //                {
        //                    bool validate = child.Validators[i].IsValid;
        //                    if (validate == false)
        //                    {
        //                        strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
        //                    }

        //                }
        //                if (nameType == "RegularExpressionValidator")
        //                {
        //                    bool validate = child.Validators[i].IsValid;
        //                    if (validate == false)
        //                    {
        //                        strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
        //                    }
        //                }
        //                if (nameType == "CompareValidator")
        //                {
        //                    bool validate = child.Validators[i].IsValid;
        //                    if (validate == false)
        //                    {
        //                        strBuild.Append(child.Validators[i].ErrorMessage + "<br/>");
        //                    }
        //                }
        //            }

        //        }

        //    }

        //    if (strBuild.ToString() != string.Empty)
        //    {
        //        res.ErrorMsg = strBuild.ToString();
        //        res.ResultMessage = false;
        //    }
        //    else
        //    {
        //        res.ResultMessage = true;
        //    }

        //    return res;
        //}


        public static string  Validate(Page page, string group)
        {
            string error = "";
            page.Validate(group);
            int count = page.Validators.Count;
            if (page.IsValid)
            {
                return error;
            }
            else
            {
                error += "<table width='100%'><tr><td width='20%'><td><td align='left' style='color:red;'>";
                var listRequiredField = page.Validators.OfType<RequiredFieldValidator>();
                foreach (var item in listRequiredField)
                {
                    if (!item.IsValid)
                    {
                        error += "-" + item.ErrorMessage + "<br/>";
                    }
                }

                var listRegularExpression = page.Validators.OfType<RegularExpressionValidator>();
                foreach (var item in listRegularExpression)
                {
                    if (!item.IsValid)
                    {
                        error += "-" + item.ErrorMessage + "<br/>";
                    }
                }
                error += "</td></table>";
                return error;
            }
        }

    }
}

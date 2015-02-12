using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace IAS.Mockup
{
    public partial class PopUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetDay();

            bool valid1 = this.IsAlpha("");
            bool valid2 = this.IsAlpha("");

        }

        protected void btnSortPopup_Click(object sender, EventArgs e)
        {
            this.mdlLicenseD.Show();
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            mdlLicenseD.Show();
            mdlPop2.Show();
        }

        private void GetDay()
        {
            string date1 = "1/1/2557";
            string date2 = "30/6/2557";
            TimeSpan difference = Convert.ToDateTime(date2) - Convert.ToDateTime(date1);
            int date = difference.Days;
            //int compare = Convert.ToDateTime(date2).Day.CompareTo(Convert.ToDateTime(date2).Day);

        }

        private bool IsAlpha(String strToCheck)
        {
            //Regex objAlphaPattern = new Regex("[a-zA-Z0-9@#$%^&*(){}+?,_]");
            Regex objAlphaPattern = new Regex("^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
    }
}
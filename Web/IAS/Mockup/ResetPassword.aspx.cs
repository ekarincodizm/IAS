using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Properties;
using IAS.Utils;

namespace IAS.Mockup
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GenNewPass();

        }

        private void GenNewPass()
        {
            
            string status = "";

            Int32 statusNo;
            if (Int32.TryParse("2", out statusNo))
            {

                if (statusNo == 1 || statusNo == 2)
                {
                    DTO.RegistrationStatus regstatus = (DTO.RegistrationStatus)statusNo;

                    if(regstatus.GetEnumValue().ToString().Equals(DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString()))
                    {
                        status = "รอการอนุมัติ";
                    }
                    else if (regstatus.GetEnumValue().ToString().Equals(DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString()))
                    {
                        status = "ได้รับการอนุมัติ";
                    }

                    //switch (regstatus.GetEnumValue().ToString())
                    //{
                    //    case DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() : status = "รอการอนุมัติ"; break;
                    //    case DTO.RegistrationStatus.NotApprove : status = "ได้รับการอนุมัติ"; break;

                    //    default:
                    //        break;
                    //}

                }

            }

            RandomPassword ranpass = new RandomPassword();
            string newpass = ranpass.GeneratePassword(true, true, true, true, 8);
            lblNewPass.Text = newpass;
        }
    }

    public class RandomPassword
    {
        public static Random rnd = new Random();
        private const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LOWER = "abcdefghijklmnopqrstuvwxyz";
        private const string NUMBERS = "0123456789";
        private const string SYMBOLS = "@#$%^&*(){}+?,_";

        public string GeneratePassword(bool useUpper, bool userLower, bool userNumbers, bool useSymbols, int passwordLength)
        {
            System.Text.StringBuilder charPool = new System.Text.StringBuilder();
            System.Text.StringBuilder charReq = new System.Text.StringBuilder();
            System.Text.StringBuilder newPass = new System.Text.StringBuilder();
            if (useUpper)
            {
                charPool.Append(UPPER);
                charReq.Append(UPPER[rnd.Next(UPPER.Length)]);
            }

            if (userLower)
            {
                charPool.Append(LOWER);
                charReq.Append(LOWER[rnd.Next(LOWER.Length)]);
            }

            if (userNumbers)
            {
                charPool.Append(NUMBERS);
                charReq.Append(NUMBERS[rnd.Next(NUMBERS.Length)]);
            }

            if (useSymbols)
            {
                charPool.Append(SYMBOLS);
                charReq.Append(SYMBOLS[rnd.Next(SYMBOLS.Length)]);
            }

            int max = charPool.Length;

            newPass.Append(charReq.ToString());
            for (int x = newPass.Length; x < passwordLength; x++)
            {
                newPass.Append(charPool[rnd.Next(max)]);
                
            }
            return newPass.ToString();
        }

    }
}
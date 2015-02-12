using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.DTO;
using IAS.Properties;
using System.Text;
using IAS.Utils;
using System.Security.Cryptography;

namespace IAS.Mockup
{
    public partial class TestUCPersonalSkill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //DateTime dt = DateTime.Now;
                //lblInstalledUICulture.Text = "InstalledUICulture : " + System.Globalization.CultureInfo.InstalledUICulture.Name;
                //lblCurrentCulture.Text = "CurrentCulture : " + System.Globalization.CultureInfo.CurrentCulture.Name;
                //lblCurrentUICulture.Text = "CurrentUICulture : " + System.Globalization.CultureInfo.CurrentUICulture.Name;
                //lblInvariantCulture.Text = "InvariantCulture : " + System.Globalization.CultureInfo.InvariantCulture.Name;
                //if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                //{
                //    var y = dt.Year + 543;
                //}
                //else
                //{
                //    int y = dt.Year;
                //}
                

                GetPersonalSkill();
            }
        }

        private void GetPersonalSkill()
        {
            Func<string, string> resultConvert = delegate(string input)
            {
                if ((input == null) || (input == ""))
                {
                    input = SysMessage.TrainResultF;
                }

                return input;

            };

            this.ucPerSkills.CurrentIDCard = "2251300001059";
            this.ucPerSkills.CurrentRegistrationID = "131113150911794";
            this.ucPerSkills.LicenseTypeCode = "03";
            this.ucPerSkills.PettionTypeCode = "13";
            this.ucPerSkills.CurrentLicenseRenewTime = "0";
            this.ucPerSkills.curResultExam = "ผ่าน";
            this.ucPerSkills.curResultTrain = "ไม่ผ่าน";
            this.ucPerSkills.Rule7 = "1";
            this.ucPerSkills.Rule8 = "1";
            this.ucPerSkills.Rule9 = "1";
            this.ucPerSkills.curResultEducation = "ผ่าน";
            this.ucPerSkills.Mode = DTO.LicensePropMode.General.GetEnumValue().ToString();
            this.ucPerSkills.ucInit();
        }
    }
}
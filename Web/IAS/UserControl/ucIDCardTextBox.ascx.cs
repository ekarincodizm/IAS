using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class ucIDCardTextBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Boolean Validate()
        {
            String idcardNumber = txtIDCardNumber.Text;
            if (String.IsNullOrWhiteSpace(idcardNumber))
                return false;

            if (idcardNumber.Length != 13)
                return false;

            Int64 numidcard;
            if (!Int64.TryParse(idcardNumber, out numidcard))
                return false;

            Int32 sumResult = 0;
            for (int i = 1; i <= 12; i++) // (Char digit in idcardNumber)
            {
                sumResult += (13 + 1 - i) * Convert.ToInt32(idcardNumber.Substring((i - 1), 1));
            }
            if (Convert.ToInt32(idcardNumber.Substring(12, 1)) != (11 - (sumResult % 11)))
                return false;

            return true;
        }
    }
}
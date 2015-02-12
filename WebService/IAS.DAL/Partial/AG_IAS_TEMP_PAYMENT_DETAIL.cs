using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DAL
{
    public partial class AG_IAS_TEMP_PAYMENT_DETAIL
    {
        public string COMPANY_NAME { get; set; }
        public string SERVICE_CODE { get; set; }
        public string EFFECTIVE_DATE { get; set; }
        public string TOTAL_DEBIT_AMOUNT { get; set; }
        public string TOTAL_CREDIT_AMOUNT { get; set; }  
        public string PAYMENT_DATE_SHOW
        {
            get
            {
                string dt = string.Empty;
                string tm = this.PAYMENT_DATE;
                if (tm.Length > 0 && tm.Length == 8)
                {
                    dt = tm.Substring(0, 2) + "/" +
                         tm.Substring(2, 2) + "/" +
                         tm.Substring(4, 4);
                }
                return tm;
            }
        }

        public string EFFECTIVE_DATE_SHOW
        {
            get
            {
                string dt = string.Empty;
                string tm = this.EFFECTIVE_DATE;
                if (tm.Length > 0 && tm.Length == 8)
                {
                    dt = tm.Substring(0, 2) + "/" +
                         tm.Substring(2, 2) + "/" +
                         tm.Substring(4, 4);
                }
                return tm;
            }
        }
    }
}

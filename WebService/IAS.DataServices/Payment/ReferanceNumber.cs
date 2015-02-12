using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment
{
    public class ReferanceNumber
    {
        private String _oicNumber = "none";
        private Int64 _runningNumber = -1;
        private DateTime _createDate;

        public ReferanceNumber(String oicNumber, Int64 runningNumber, DateTime createDate)
        {
            _oicNumber = oicNumber;
            _runningNumber = runningNumber;
            _createDate = createDate;
        }

        public String OICNumber { get { return _oicNumber; }  }
        public DateTime CreateDate { get { return _createDate; } }
        public Int64 RunnigNumber { get { return _runningNumber; } }

        public String FirstNumber
        {
            get
            {
                return String.Format("{0}{1}{2}",
                    OICNumber,
                    CreateDate.ToString("yyMM", new System.Globalization.CultureInfo("th-TH")),
                    RunnigNumber.ToString().PadLeft(8,'0'));
            }
        }
        public String DisplayFirstNumber 
        {
            get 
            {
                return String.Format("{0}-{1}-{2}",
                    OICNumber,
                    CreateDate.ToString("yyMM", new System.Globalization.CultureInfo("th-TH")),
                    RunnigNumber.ToString().PadLeft(8, '0'));
            }
        }
        public String SecondNumber { get { return CreateDate.AddDays(1).ToString("ddMMyyyy", new System.Globalization.CultureInfo("en-US")); } }

        public override string ToString()
        {
            return String.Format("Ref1: {0}, Ref2: {1}", FirstNumber, SecondNumber);
        }
    }
}

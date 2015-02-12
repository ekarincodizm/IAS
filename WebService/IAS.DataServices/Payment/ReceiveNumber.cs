using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment
{
    public class ReceiveNumber
    {
        private String _oicNumber = "XXXXX";
        private Int64 _runningNumber = -1;
        private Int32 _lenght = 8;
        private DateTime _createDate;


        public ReceiveNumber(String oicNumber, Int64 runningNumber, DateTime createDate)
        {
            _oicNumber = oicNumber;
            _runningNumber = runningNumber;
            _createDate = createDate;
        }

        public String OICNumber { get { return _oicNumber; } }
        public DateTime CreateDate { get { return _createDate; } }
        public Int64 RunnigNumber { get { return _runningNumber; } }

        /// <summary>
        /// XXXXX 5 หลักแรก เป็นข้อมูลของคปภ. จะเก็บไว้ใน web Config
        /// XXXX 4  ปีปีเดือนเดือน
        /// XXXXXXXX 8 หลัก เป็น Running Number
        /// </summary>
        public String ReceiveNo
        {
            get
            {           
                return String.Format("{0}{1}{2}",
                    OICNumber,
                    CreateDate.ToString("yyMM", new System.Globalization.CultureInfo("th-TH")),
                    RunnigNumber.ToString().PadLeft(8, '0'));
            }
        }
        public String DisplayReceiveNo
        {
            get 
            {
                return String.Format("{0} {1} {2}",
                    OICNumber,
                    CreateDate.ToString("yyMM", new System.Globalization.CultureInfo("th-TH")),
                    RunnigNumber.ToString().PadLeft(8, '0'));
            }
        }


    }
}
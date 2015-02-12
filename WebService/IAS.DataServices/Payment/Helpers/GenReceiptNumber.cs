using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using IAS.DAL;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Payment.Helpers
{
    public class GenReceiptNumber
    {
        // update format: 20131001  '12122 e5 56 XXXXXXXXXXX'
        // 12122 = fix section code 
        // e5 = type ค่าปรับตาม PetitionCode
        // 56 = ปีปัจจุบัน พ.ศ.
        // 01 = เดือน
        // XXXXXXXXX = runningNo
        //private static Int64 _runnigNumber;
        //private static Int64 _runnigNumber;
        private static IAS.DAL.Interfaces.IIASPersonEntities ctx;
        private static readonly String _oicNumber = "12122";
        private static String _typeCode = "RECV" ;
        public static Int64 RUNNINGNO
        {
            get
            {
                return Convert.ToInt64(ctx.AG_IAS_PAYMENT_RUNNINGNO.Single(a => a.ID == _typeCode).LAST_RUNNO);
            }
        }
        public static ReceiveNumber CreateReceiveNumber()
        {
            ctx = DAL.DALFactory.GetPersonContext();

            ReceiveNumber referanceNumber = new ReceiveNumber(_oicNumber, RunningNumber, DateTime.Now);
            return referanceNumber;
        }

        public static Int64 RunningNumber
        {
            get
            {
                if (RUNNINGNO == 0)
                    throw new ApplicationException(Resources.errorGenQRcode_001);

                Int64 currentNumber = RUNNINGNO;
                Running();
                return currentNumber;
            }
        }

        /// <summary>
        ///   Impliment Number Of Bank No 
        /// </summary>
        private static void Running()
        {
            ctx.AG_IAS_PAYMENT_RUNNINGNO.Single(p => p.ID == _typeCode).LAST_RUNNO++;
            ctx.SaveChanges();
        }
    }
}
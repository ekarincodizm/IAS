using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using IAS.DAL;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Payment
{
    public class GenReferanceNumber
    {

        private static readonly String _oicNumber = "999999";
        private static readonly String _bankCode =  "KTB";
        private static IAS.DAL.Interfaces.IIASPersonEntities ctx;

        public static IObjectSet<AG_IAS_PAYMENT_RUNNINGNO> PAYMENT_RUNNINGNO 
        {
            get { return ctx.AG_IAS_PAYMENT_RUNNINGNO; }
        }
        public static Int64 RUNNINGNO { 
            get {

                AG_IAS_PAYMENT_RUNNINGNO runningNo = ctx.AG_IAS_PAYMENT_RUNNINGNO.Single(a => a.ID == _bankCode);
                if(runningNo==null) {
                     throw new ApplicationException("ไม่พบข้อมูลเลขที่ใบสั่งจ่าย");
                }


                return Convert.ToInt64(runningNo.LAST_RUNNO);
            } 
        }
        public static ReferanceNumber CreateReferanceNumber() 
        {
            ctx = DAL.DALFactory.GetPersonContext();

            ReferanceNumber referanceNumber = new ReferanceNumber
                                                    (_oicNumber, RunningNumber, DateTime.Now);
            return referanceNumber;
        }

        public static Int64 RunningNumber 
        {
            get {
                if (RUNNINGNO == 0)
                    throw new ApplicationException(Resources.errorGenReferanceNumber_001);

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
            ctx.AG_IAS_PAYMENT_RUNNINGNO.Single(p => p.ID == _bankCode).LAST_RUNNO++;
            ctx.SaveChanges();
        }
    }
}
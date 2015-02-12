using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using IAS.DAL;
using IAS.DTO;
using IAS.Utils;

using Oracle.DataAccess.Client;

using System.Configuration;
using System.IO;

using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using IAS.DataServices;
using IAS.DataServices.Payment;
using IAS.DataServices.Payment.Helpers;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class InsertEntityTest
    {
        protected IAS.DAL.Interfaces.IIASPersonEntities ctx;

        [TestInitialize]
        public void Setup()
        {


            this.ctx = DAL.DALFactory.GetPersonContext();
        }

        [TestMethod]
        public void TestMethod1()
        {
              DateTime? expireDate;
            PaymentService paymentService = new PaymentService();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            var LicenseNo = GenLicenseNumber.AG_LICENSE_RUNNING(ctx,Convert.ToDateTime("22/10/2013"), "04");
            string aaa = LicenseNo.ToString();

            if ("18/10/2013".ToString().Substring(0, 4) == "2902")
            {

                expireDate = Convert.ToDateTime("22/10/2013".ToString()).AddMonths(12);
            }
            else
            {
                expireDate = Convert.ToDateTime("22/10/2013".ToString()).AddMonths(12).AddDays(-1);
            }

            var License = new DAL.AG_LICENSE_T
            {
                LICENSE_NO = LicenseNo.ToString(),
                LICENSE_DATE = Convert.ToDateTime("22/10/2013"),
                EXPIRE_DATE = Convert.ToDateTime(Convert.ToString(expireDate)),
                LICENSE_TYPE_CODE = "04",
                NEW_LICENSE_NO = null,
                LICENSE_ACTOR = null,
                DATE_LICENSE_ACT = Convert.ToDateTime("22/10/2013"),
                REMARK = null,
                UNIT_LINK_RENEW = null,
                START_UL_DATE = null,
                EXPIRE_UL_DATE = null,
                UNIT_LINK_STATUS = null
            };
            ctx.AG_LICENSE_T.AddObject(License);
            var entLicenseD = ctx.AG_IAS_LICENSE_D.SingleOrDefault(a => a.SEQ_NO == "0001" &&
                a.UPLOAD_GROUP_NO == "131022094549687");
            entLicenseD.LICENSE_NO = LicenseNo;
            var entSubpaymentD = ctx.AG_IAS_SUBPAYMENT_D_T.SingleOrDefault(a => a.SEQ_NO == "0001" &&
           a.UPLOAD_GROUP_NO == "131022094549687");
            entSubpaymentD.LICENSE_NO = LicenseNo;

            ctx.SaveChanges();

            //using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
            //{
            //    OracleCommand objCmd = new OracleCommand();

            //    objCmd.Connection = objConn;

            //    objCmd.CommandText = "IAS_UPDATE_11";

            //    objCmd.CommandType = CommandType.StoredProcedure;

            //    objCmd.Parameters.Add("P_id_card_no", OracleDbType.Varchar2).Value = "6239694333650";
            //    objCmd.Parameters.Add("P_license_type_code", OracleDbType.Varchar2).Value = "04";
            //    objCmd.Parameters.Add("P_license_no", OracleDbType.Varchar2).Value = "5601023392";
            //    objCmd.Parameters.Add("COMP_CODE", OracleDbType.Varchar2).Value = "3139";
            //    objCmd.Parameters.Add("P_RECEIPT_DATE", OracleDbType.Date).Value = Convert.ToDateTime("11/10/2013");
            //    objCmd.Parameters.Add("P_REQUEST_NO", OracleDbType.Varchar2).Value = "";
            //    objCmd.Parameters.Add("P_PAYMENT_NO", OracleDbType.Varchar2).Value = "0001";
            //    // รอการjoin data from ag_applicant_t
            //    //  objCmd.Parameters.Add("P_testing_no", OracleDbType.Varchar2).Value = dt.Rows[i]["testing_no"].ToString();
            //    objCmd.Parameters.Add("P_RECEIPT_NO", OracleDbType.Varchar2).Value = "12122e41300027";
            //    try
            //    {
            //        objConn.Open();
            //        objCmd.ExecuteNonQuery();
                 
            //        //if (objCmd.ExecuteNonQuery() == -1)
            //        //{
            //        //    res.ResultMessage = false;
            //        //}
            //        //else
            //        //{
            //        //    res.ResultMessage = true;
            //        //}
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ArgumentException(ex.Message);
            //    }
            //    finally
            //    {
            //        objConn.Close();
            //    }



           

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Oracle.DataAccess.Client;
using System.Data;
using IAS.DataServices.Payment.Messages;
using IAS.DAL;

namespace IAS.DataServices.Payment.Helpers
{
    public class UpdateAfterUploadPayment
    {

        public static void LicensePetitionType(IAS.DAL.Interfaces.IIASPersonEntities ctx, OracleConnection Connection, LicensePetitionType11Request licenseRequest)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            String LicenseNo = GenLicenseNumber.AG_LICENSE_RUNNING(ctx, (DateTime)licenseRequest.RECEIPT_DATE, licenseRequest.LICENSE_TYPE_CODE); // Convert.ToDateTime(dt.Rows[i]["receipt_date"]), dt.Rows[i]["license_type_code"].ToString());


            var License = new DAL.AG_LICENSE_T
            {
                LICENSE_NO = LicenseNo,
                LICENSE_DATE = licenseRequest.RECEIPT_DATE,
                EXPIRE_DATE = licenseRequest.EXPIRATION_DATE,
                LICENSE_TYPE_CODE = licenseRequest.LICENSE_TYPE_CODE,
                NEW_LICENSE_NO = null,
                LICENSE_ACTOR = null,
                DATE_LICENSE_ACT = licenseRequest.RECEIPT_DATE,
                REMARK = null,
                UNIT_LINK_RENEW = null,
                START_UL_DATE = null,
                EXPIRE_UL_DATE = null,
                UNIT_LINK_STATUS = null 
            };
            ctx.AG_LICENSE_T.AddObject(License);

            //Check Null
            AG_IAS_LICENSE_D entLicenseD = ctx.AG_IAS_LICENSE_D.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO && a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            if (entLicenseD != null)
            {
                entLicenseD.LICENSE_NO = LicenseNo;
                entLicenseD.LICENSE_DATE = DateTime.Now;
                entLicenseD.LICENSE_EXPIRE_DATE = licenseRequest.EXPIRATION_DATE;
            }

            //Check Null
            AG_IAS_SUBPAYMENT_D_T entSubpaymentD = ctx.AG_IAS_SUBPAYMENT_D_T.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO && a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            if (entSubpaymentD != null)
            {
                entSubpaymentD.LICENSE_NO = LicenseNo;
            }


            OracleCommand objCmd = new OracleCommand()
            {
                Connection = Connection,
                CommandText = "IAS_UPDATE_11",
                CommandType = CommandType.StoredProcedure
            };

            objCmd.Parameters.Add("P_id_card_no", OracleDbType.Varchar2).Value = SetValue(licenseRequest.ID_CARD_NO);
            objCmd.Parameters.Add("P_license_type_code", OracleDbType.Varchar2).Value = SetValue(licenseRequest.LICENSE_TYPE_CODE);
            objCmd.Parameters.Add("P_license_no", OracleDbType.Varchar2).Value = SetValue( LicenseNo);
            objCmd.Parameters.Add("COMP_CODE", OracleDbType.Varchar2).Value = SetValue( licenseRequest.COMP_CODE);
            objCmd.Parameters.Add("P_RECEIPT_DATE", OracleDbType.Date).Value = licenseRequest.RECEIPT_DATE;
            objCmd.Parameters.Add("P_REQUEST_NO", OracleDbType.Varchar2).Value = SetValue(entLicenseD.REQUEST_NO);
            objCmd.Parameters.Add("P_PAYMENT_NO", OracleDbType.Varchar2).Value = SetValue( licenseRequest.PAYMENT_NO);
            objCmd.Parameters.Add("P_testing_no", OracleDbType.Varchar2).Value = SetValue( licenseRequest.TESTING_NO);
            objCmd.Parameters.Add("P_RECEIPT_NO", OracleDbType.Varchar2).Value = SetValue( licenseRequest.RECEIPT_NO);
            objCmd.ExecuteNonQuery();

        }

        private static String SetValue(String source) {
            if (source == null) return "";
            else return  source;
        }

        public static void LicensePetitionType(IAS.DAL.IASPersonEntities ctx, OracleConnection Connection, LicensePetitionType1314Request licenseRequest)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

            var entLicenseD = ctx.AG_IAS_LICENSE_D.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO && a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            // entLicenseD.LICENSE_NO = LicenseNo;
            entLicenseD.LICENSE_DATE = DateTime.Now;

            entLicenseD.LICENSE_EXPIRE_DATE = licenseRequest.EXPIRATION_DATE;

            OracleCommand objCmd = new OracleCommand() { Connection = Connection, CommandText = "IAS_UPDATE_1314", CommandType = CommandType.StoredProcedure };

            objCmd.Parameters.Add("P_LICENSE_NO", OracleDbType.Varchar2).Value = licenseRequest.LICENSE_NO;
            objCmd.Parameters.Add("P_EXPIRE_DATE", OracleDbType.Date).Value = licenseRequest.RECEIPT_DATE; // Convert.ToDateTime(ReceiveDate);
            objCmd.Parameters.Add("P_RECEIPT_DATE", OracleDbType.Date).Value = licenseRequest.RECEIPT_DATE; // Convert.ToDateTime(ReceiveDate);
            objCmd.Parameters.Add("P_RECEIPT_NO", OracleDbType.Varchar2).Value = licenseRequest.RECEIPT_NO; // requestNo;
            objCmd.Parameters.Add("P_PAYMENT_NO", OracleDbType.Varchar2).Value = licenseRequest.PAYMENT_NO;
            objCmd.Parameters.Add("P_REQUEST_NO", OracleDbType.Varchar2).Value = SetValue( licenseRequest.REQUEST_NO);
            objCmd.Parameters.Add("PV_CODE", OracleDbType.Varchar2).Value = licenseRequest.AREA; // area;
            objCmd.ExecuteNonQuery();

        }

        public static void LicensePetitionType(IAS.DAL.Interfaces.IIASPersonEntities ctx, OracleConnection Connection, LicensePetitionType15Request licenseRequest)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            string LicenseNo = GenLicenseNumber.AG_LICENSE_RUNNING(ctx, licenseRequest.RECEIPT_DATE, licenseRequest.LICENSE_TYPE_CODE);

            var License = new DAL.AG_LICENSE_T
            {
                LICENSE_NO = LicenseNo,
                LICENSE_DATE = licenseRequest.RECEIPT_DATE, // Convert.ToDateTime(ReceiveDate),
                EXPIRE_DATE = licenseRequest.EXPIRATION_DATE, // Convert.ToDateTime(Convert.ToString(expireDate)),
                LICENSE_TYPE_CODE = licenseRequest.LICENSE_TYPE_CODE, // licenseT,
                NEW_LICENSE_NO = null,
                LICENSE_ACTOR = null,
                DATE_LICENSE_ACT = licenseRequest.RECEIPT_DATE, // Convert.ToDateTime(ReceiveDate),
                REMARK = null,
                UNIT_LINK_RENEW = null,
                START_UL_DATE = null,
                EXPIRE_UL_DATE = null,
                UNIT_LINK_STATUS = null
            };
            ctx.AG_LICENSE_T.AddObject(License);
            var entLicenseD = ctx.AG_IAS_LICENSE_D.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO &&
                                                        a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            entLicenseD.LICENSE_NO = LicenseNo;
            entLicenseD.LICENSE_DATE = DateTime.Now;

            entLicenseD.LICENSE_EXPIRE_DATE = licenseRequest.EXPIRATION_DATE; // Convert.ToDateTime(Convert.ToString(expireDate));
            var entSubpaymentD = ctx.AG_IAS_SUBPAYMENT_D_T.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO &&
                                                                            a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            //string OldLicense = entSubpaymentD.LICENSE_NO.ToString();
            entSubpaymentD.OLD_LICENSE_NO = entSubpaymentD.LICENSE_NO;
            entSubpaymentD.LICENSE_NO = LicenseNo;

            OracleCommand objCmd = new OracleCommand() { Connection = Connection, CommandText = "IAS_UPDATE_15", CommandType = CommandType.StoredProcedure };

            objCmd.Parameters.Add("P_ID_CARD_NO", OracleDbType.Varchar2).Value = licenseRequest.ID_CARD_NO;
            objCmd.Parameters.Add("P_LICENSE_TYPE_CODE", OracleDbType.Varchar2).Value = licenseRequest.LICENSE_TYPE_CODE;
            objCmd.Parameters.Add("P_LICENSE_NO", OracleDbType.Varchar2).Value = LicenseNo;
            objCmd.Parameters.Add("P_COMP_CODE", OracleDbType.Varchar2).Value = licenseRequest.COMP_CODE;
            objCmd.Parameters.Add("P_RECEIPT_DATE", OracleDbType.Date).Value = licenseRequest.RECEIPT_DATE;
            objCmd.Parameters.Add("P_REQUEST_NO", OracleDbType.Varchar2).Value = licenseRequest.RECEIPT_NO; // requestNo;
            objCmd.Parameters.Add("P_PAYMENT_NO", OracleDbType.Varchar2).Value = licenseRequest.PAYMENT_NO; // payment_no;
            objCmd.Parameters.Add("P_RECEIPT_NO", OracleDbType.Varchar2).Value = licenseRequest.RECEIPT_NO; // receiptNo;

            objCmd.ExecuteNonQuery();

        }

        public static void LicensePetitionType(IAS.DAL.Interfaces.IIASPersonEntities ctx, OracleConnection Connection, LicensePetitionType17Request licenseRequest)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            String LicenseNo = GenLicenseNumber.AG_LICENSE_RUNNING(ctx, licenseRequest.RECEIPT_DATE, licenseRequest.LICENSE_TYPE_CODE);

            var License = new DAL.AG_LICENSE_T
            {
                LICENSE_NO = LicenseNo,
                LICENSE_DATE = licenseRequest.RECEIPT_DATE,// Convert.ToDateTime(ReceiveDate),
                EXPIRE_DATE = licenseRequest.EXPIRATION_DATE,
                LICENSE_TYPE_CODE = licenseRequest.LICENSE_TYPE_CODE, // licenseT,
                NEW_LICENSE_NO = null,
                LICENSE_ACTOR = null,
                DATE_LICENSE_ACT = licenseRequest.RECEIPT_DATE, // Convert.ToDateTime(ReceiveDate),
                REMARK = null,
                UNIT_LINK_RENEW = null,
                START_UL_DATE = null,
                EXPIRE_UL_DATE = null,
                UNIT_LINK_STATUS = null
            };
            ctx.AG_LICENSE_T.AddObject(License);

            var entLicenseD = ctx.AG_IAS_LICENSE_D.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO &&
                                            a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            entLicenseD.LICENSE_NO = LicenseNo;
            entLicenseD.LICENSE_DATE = DateTime.Now;
            entLicenseD.LICENSE_EXPIRE_DATE = licenseRequest.EXPIRATION_DATE;

            var entSubpaymentD = ctx.AG_IAS_SUBPAYMENT_D_T.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO &&
                                                                            a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            entSubpaymentD.OLD_LICENSE_NO = entSubpaymentD.LICENSE_NO;
            entSubpaymentD.LICENSE_NO = LicenseNo;

            OracleCommand objCmd = new OracleCommand() { Connection = Connection, CommandText = "IAS_UPDATE_17", CommandType = CommandType.StoredProcedure };

            objCmd.Parameters.Add("P_ID_CARD_NO", OracleDbType.Varchar2).Value = licenseRequest.ID_CARD_NO;
            objCmd.Parameters.Add("P_LICENSE_NO", OracleDbType.Varchar2).Value = LicenseNo;
            objCmd.Parameters.Add("P_COMP_CODE", OracleDbType.Varchar2).Value = licenseRequest.COMP_CODE;
            objCmd.Parameters.Add("P_RECEIPT_DATE", OracleDbType.Date).Value = licenseRequest.RECEIPT_DATE;
            objCmd.Parameters.Add("P_REQUEST_NO", OracleDbType.Varchar2).Value = licenseRequest.REQUEST_NO; // requestNo;
            objCmd.Parameters.Add("P_PAYMENT_NO", OracleDbType.Varchar2).Value = licenseRequest.PAYMENT_NO; // payment_no;
            objCmd.Parameters.Add("P_RECEIPT_NO", OracleDbType.Varchar2).Value = licenseRequest.RECEIPT_NO; // receiptNo;
            objCmd.ExecuteNonQuery();

        }

        public static void LicensePetitionType(IAS.DAL.Interfaces.IIASPersonEntities ctx, OracleConnection Connection, LicensePetitionType18Request licenseRequest)
        {

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            String LicenseNo = GenLicenseNumber.AG_LICENSE_RUNNING(ctx, licenseRequest.RECEIPT_DATE, licenseRequest.LICENSE_TYPE_CODE);


            var License = new DAL.AG_LICENSE_T
            {
                LICENSE_NO = LicenseNo.ToString(),
                LICENSE_DATE = licenseRequest.RECEIPT_DATE, // Convert.ToDateTime(ReceiveDate),
                EXPIRE_DATE = licenseRequest.EXPIRATION_DATE, //  Convert.ToDateTime(Convert.ToString(expireDate)),
                LICENSE_TYPE_CODE = licenseRequest.LICENSE_TYPE_CODE,
                NEW_LICENSE_NO = null,
                LICENSE_ACTOR = null,
                DATE_LICENSE_ACT = licenseRequest.RECEIPT_DATE, // Convert.ToDateTime(ReceiveDate),
                REMARK = null,
                UNIT_LINK_RENEW = null,
                START_UL_DATE = null,
                EXPIRE_UL_DATE = null,
                UNIT_LINK_STATUS = null
            };
            ctx.AG_LICENSE_T.AddObject(License);

            var entLicenseD = ctx.AG_IAS_LICENSE_D.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO &&
                                       a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            entLicenseD.LICENSE_NO = LicenseNo;
            entLicenseD.LICENSE_DATE = DateTime.Now;
            entLicenseD.LICENSE_EXPIRE_DATE = licenseRequest.EXPIRATION_DATE; // Convert.ToDateTime(Convert.ToString(expireDate));
            var entSubpaymentD = ctx.AG_IAS_SUBPAYMENT_D_T.FirstOrDefault(a => a.SEQ_NO == licenseRequest.SEQ_NO &&
                                                                            a.UPLOAD_GROUP_NO == licenseRequest.UPLOAD_GROUP_NO);
            entSubpaymentD.OLD_LICENSE_NO = entSubpaymentD.LICENSE_NO;
            entSubpaymentD.LICENSE_NO = LicenseNo;

            OracleCommand objCmd = new OracleCommand() { Connection = Connection, CommandText = "IAS_UPDATE_18", CommandType = CommandType.StoredProcedure };

            objCmd.Parameters.Add("P_ID_CARD_NO", OracleDbType.Varchar2).Value = licenseRequest.ID_CARD_NO;// IdCard;
            objCmd.Parameters.Add("P_LICENSE_NO", OracleDbType.Varchar2).Value = LicenseNo;
            objCmd.Parameters.Add("P_COMP_CODE", OracleDbType.Varchar2).Value = licenseRequest.COMP_CODE; // ComCode;
            objCmd.Parameters.Add("P_RECEIPT_DATE", OracleDbType.Date).Value = licenseRequest.RECEIPT_DATE; // Convert.ToDateTime(ReceiveDate);
            objCmd.Parameters.Add("P_REQUEST_NO", OracleDbType.Varchar2).Value = licenseRequest.RECEIPT_NO; // requestNo;
            objCmd.Parameters.Add("P_PAYMENT_NO", OracleDbType.Varchar2).Value = licenseRequest.PAYMENT_NO; // payment_no;
            objCmd.Parameters.Add("P_RECEIPT_NO", OracleDbType.Varchar2).Value = licenseRequest.RECEIPT_NO; // receiptNo;

            objCmd.ExecuteNonQuery();

        }     
    }
}
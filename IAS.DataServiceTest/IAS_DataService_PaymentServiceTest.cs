using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DAL;
using System.Data;
using IAS.DataServices.Payment.Mapper;
using IAS.DataServices.Payment.Messages;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class IAS_DataService_PaymentServiceTest
    {
        [TestMethod]
        public void CreateReceiptMapper_CanMap_DataRowTest()
        {
            string IdWhenCareteInDetail = "";

            String G_req_no = "999999561000000091";
            String receiptById = "130923130455931";
        
            string IDcon = string.Empty;
            if (IdWhenCareteInDetail != "") //สำหรับกรณีเก็บตกที่มีการerrorในการสร้างเอกสาร และต้องมากำหนดการสร้างรายคน
            {
                IDcon = String.Format(" and d.ID_CARD_NO = '{0}' ", IdWhenCareteInDetail);
            }

            string tmp = string.Empty;
            tmp = " select * from ag_ias_subpayment_d_t d,ag_ias_subpayment_h_t h "
                    + " where d.head_request_no = h.head_request_no and h.group_request_no =  '" + G_req_no + "' and "
                    + " d.record_status = '" + DTO.SubPayment_D_T_Status.A.ToString() + "' and  d.receipt_by_id = '" + receiptById + "' " ;


            string H_req_no = "131010194915174";
            string IDcard = "3909900148564";
            string PayNo = "0004";
            string crit = String.Format(" and d.head_request_no = '{0}' and d.ID_CARD_NO = '{1}' and d.payment_no = '{2}' ", H_req_no, IDcard, PayNo);
            tmp = "SELECT	d.head_request_no,d.id_card_no  ,p.petition_type_name  "
                + ",d.payment_date,FN.TITLE_NAME ||' '|| FN.NAMES || ' ' || FN.LASTNAME FirstName , "
                + " NN.NAME || ' ' || ipt.names || ' ' || ipt.lastname LASTNAME, "
                + "g.group_request_no,g.group_date,d.receipt_no,d.PAYMENT_NO,d.RECEIPT_DATE "
                + " ,d.AMOUNT,g.CREATED_DATE, ipt.signature_img , " //----> ipt.signature_img , add by milk
                + "ROW_NUMBER() OVER (ORDER BY g.group_request_no) RUN_NO  , g.CREATED_BY "
                + "from ag_ias_payment_g_t g,ag_ias_subpayment_h_t h,ag_ias_subpayment_d_t d, "
                + "ag_petition_type_r p,AG_IAS_LICENSE_D FN ,AG_IAS_PERSONAL_T ipt " //,AG_IAS_PERSONAL_T ipt ---> add by milk
                + ",VW_IAS_TITLE_NAME NN " //milk
                + "where g.group_request_no = h.group_request_no "
                + "and d.head_request_no = h.head_request_no "
                + "and d.RECEIPT_BY_ID = ipt.ID  "  //->>>> this line add by Milk
                + "and d.petition_type_code = p.petition_type_code "
                + "and FN.ID_CARD_NO = d.id_card_no and NN.ID = ipt.pre_name_code "
                + "and d.petition_type_code in ('01','11','13','14','15','16','17','18') " + crit
                + "union "
                + "SELECT	d.head_request_no,d.id_card_no ,p.petition_type_name  "
                + ",d.payment_date,TT.NAME ||' '|| FN.NAMES || ' ' || FN.LASTNAME FirstName, "
                + " NN.NAME || ' ' || ipt.names || ' ' || ipt.lastname LASTNAME, "
                + "g.group_request_no,g.group_date,d.receipt_no,d.PAYMENT_NO,d.RECEIPT_DATE,d.AMOUNT,g.CREATED_DATE, ipt.signature_img , " //----> ipt.signature_img , add by milk
                + "ROW_NUMBER() OVER (ORDER BY g.group_request_no) RUN_NO   , g.CREATED_BY "
                + "from ag_ias_payment_g_t g,ag_ias_subpayment_h_t h,ag_ias_subpayment_d_t d, "
                + "ag_petition_type_r p,AG_APPLICANT_T FN,VW_IAS_TITLE_NAME TT ,AG_IAS_PERSONAL_T ipt " //,AG_IAS_PERSONAL_T ipt ---> add by milk
                + ",VW_IAS_TITLE_NAME NN " //milk
                + "where g.group_request_no = h.group_request_no "
                + "and d.head_request_no = h.head_request_no "
                + "and d.RECEIPT_BY_ID = ipt.ID  "  //->>>> this line add by Milk
                + "and d.petition_type_code = p.petition_type_code "
                + "and TT.ID = fn.pre_name_code "
                + "and FN.ID_CARD_NO = d.id_card_no and NN.ID = ipt.pre_name_code "
                + "and fn.testing_no = d.TESTING_NO "
                + "and d.petition_type_code in ('01','11','13','14','15','16','17','18') " + crit;


            OracleDB ora = new OracleDB();
            DataSet DS_D_T = ora.GetDataSet(tmp);


            CreateReceiptReqeust receipt = DS_D_T.Tables[0].Rows[0].ToCreateReceiptRequest();

            Assert.AreEqual(receipt.GROUP_REQUEST_NO, G_req_no);
        }
    }
}

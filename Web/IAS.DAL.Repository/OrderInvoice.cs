using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class OrderInvoice
    {
        public string UPLOAD_BY_SESSION { get; set; }
   
        public string UPLOAD_GROUP_NO { get; set; }
        public string RUN_NO { get; set; }
        public Int32 IndexOfGroup { get; set; }

        //สมัครสอบ
        public string CountPerson { get; set; }
        public string PETITION_TYPE_NAME { get; set; }
        public string testing_date { get; set; }
        public string TESTING_NO { get; set; }
        public string EXAM_PLACE_CODE { get; set; }
        public string PaymentType { get; set; }
        public Int32 ApplicantCode { get; set; }

        // License
        public string LOTS { get; set; }
        public string LICENSE_TYPE_NAME { get; set; }
        public string LICENSE_NO { get; set; }
        public string RENEW_TIMES { get; set; }

        public string SEQ_NO { get; set; }


        //ใบสั่งจ่าย
        public string HeadrequestNo { get; set; }
        public string Amount { get; set; }
        public string SubPaymentDate { get; set; }


        //สมัครสอบรายเดี่ยว
        public string comcode { get; set; }
    }
}

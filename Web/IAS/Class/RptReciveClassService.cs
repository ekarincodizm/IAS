using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Class
{
    public class RptReciveClassService
    {
        public string BillNumber { get; set; }
        public string PaymentType { get; set; }
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TestingNo { get; set; }
        public string CompanyCode { get; set; }
        public string ExamPlaceCode { get; set; }
        public string ReceiptDate { get; set; }
        public string AMOUNT { get; set; }
        public string HEAD_REQUEST_NO { get; set; }
        public string PAYMENT_NO { get; set; }
        public string BathThai { get; set; }
        public string SigImgPath { get; set; } //milk
        public byte[] SigImgPathArray { get; set; } //milk
        public string QRcordPath { get; set; } //milk
        public byte[] QRcordPathArray { get; set; } //milk
        public string GUID { get; set; } //milk
        public string POSITION { get; set; }//milk
        public byte[] BG_copy_pathArray { get; set; }//milk
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ReceiveNo
    {
        public string ReceiptNumber { get; set; }
        public string ReceiptDate { get; set; }
        public string PaymentType { get; set; }
        public string Amt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HEAD_REQUEST_NO { get; set; }
        public string PAYMENT_NO { get; set; }
        public string SigImgPath { get; set; } //milk
        public byte[] SigImgPathArray { get; set; } //milk
        public string QRcordPath { get; set; } //milk
        public byte[] QRcordPathArray { get; set; } //milk
        public string GUID { get; set; } //milk
        public string POSITION { get; set; }//milk
        // add by Por 
        public string rcv_path { get; set; }
        public string id_card { get; set; }
        public string GroupRequestNo  { get; set; }
        public string RunNo { get; set; }
        public string  groupDate { get; set; }
      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using IAS.DAL;
using IAS.Utils;

namespace IAS.DataServices.Payment.Messages
{
    public class CreateReceiptReqeust
    {
        public String HEAD_REQUEST_NO { get; set; }
        public String ID_CARD_NO { get; set; }
        public String PETITION_TYPE_NAME { get; set; }
        public DateTime PAYMENT_DATE { get; set; }
        public String FIRSTNAME { get; set; }
        public String LASTNAME { get; set; }
        public String GROUP_REQUEST_NO { get; set; }
        public DateTime GROUP_DATE { get; set; }
        public String RECEIPT_NO { get; set; }
        public String PAYMENT_NO { get; set; }
        public DateTime RECEIPT_DATE { get; set; }
        public Decimal AMOUNT { get; set; }                      
        public DateTime CREATED_DATE { get; set; }
        public String CREATE_BY { get; set; }
        public String SIGNATURE_IMG { get; set; }
        public Int32 RUN_NO { get; set; }
        public byte[] IMG_SIGN { get; set; } //milk

        public String LICENSE_TYPE_CODE { get; set; }

        public String LinkRecipt(IAS.DAL.Interfaces.IIASPersonEntities ctx)
        {
            AG_IAS_USERS user = ctx.AG_IAS_USERS.FirstOrDefault(a => a.USER_ID == CREATE_BY);
            if (user != null)
            {
                String urlroot = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();
                String requestdata = CryptoBase64.Encryption(String.Format("{0}||{1}", user.USER_NAME, FilePath));

                return String.Format("{0}UserControl/ViewFile.aspx?PostReceipt={1}", urlroot, requestdata);
            }
            else
            {
                return "ไม่พบข้อมูลของเจ้าของใบสั่งจ่าย กรุณาติดต่อผู้ดูแลระบบ";
            }

        }

        public String FullFilePath {                                           
            get {
                string mapDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"].ToString();
                string PDF_Receipt = ConfigurationManager.AppSettings["FS_RECEIVE"].ToString(); // "ReceiptFile";

                string FileNameInput = String.Format("{0}_{1}.pdf", ID_CARD_NO, RECEIPT_NO);

                return Path.Combine(mapDrive, PDF_Receipt, ID_CARD_NO, FileNameInput); 

            }
        }

        public String DirectoryPath {
            get {
                string mapDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"].ToString();
                string PDF_Receipt = ConfigurationManager.AppSettings["FS_RECEIVE"].ToString(); // "ReceiptFile";

                return Path.Combine(mapDrive, PDF_Receipt, ID_CARD_NO);
                                   
            }
        }

        public String FilePath { 
            get {
              
                string PDF_Receipt = ConfigurationManager.AppSettings["FS_RECEIVE"].ToString(); // "ReceiptFile";

                string FileNameInput = String.Format("{0}_{1}.pdf", ID_CARD_NO, RECEIPT_NO);

                return Path.Combine(PDF_Receipt, ID_CARD_NO, FileNameInput);
            }

        }
    }                                                                      
}
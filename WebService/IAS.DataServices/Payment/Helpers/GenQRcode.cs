using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects;
using IAS.DAL;
using System.Configuration;
using IAS.Utils;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using IAS.DataServices.Payment.Messages;
using IAS.DataServices.Class;
using System.Text;
using System.Drawing;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Payment.Helpers
{
    public class GenQRcode
    {
       
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
        public static byte[] CreateQRcode(string PDF_path,string UID)
        {
            ctx = DAL.DALFactory.GetPersonContext();

           string ImgPath= @"OIC\QRCodeIASWebSite.jpg";

            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            byte[] buffer = null;
            using (NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive))
            {
                Stream fileStream = new FileStream(_netDrive + "" + ImgPath, FileMode.Open);
                buffer = new Byte[fileStream.Length + 1];
                BinaryReader br = new BinaryReader(fileStream);
                buffer = br.ReadBytes(Convert.ToInt32((fileStream.Length)));
                br.Close();
            }
            return buffer;


            //ReceiveNumber referanceNumber = new ReceiveNumber(_oicNumber, RunningNumber, DateTime.Now);
            //return referanceNumber;
            
        }
      
        public static byte[] CreateQRcode(IAS.DAL.Interfaces.IIASPersonEntities ctx, CreateReceiptReqeust receipt)
        {
            byte[] qrcode = null;
    
            QRCodeEncoder encoder = new QRCodeEncoder();
            StringBuilder data = new StringBuilder("");
            data.AppendLine(String.Format("เลขที่ใบเสร็จ: {0}", receipt.RECEIPT_NO));
            data.AppendLine(String.Format("ชื่อ: {0}", receipt.FIRSTNAME));
            data.AppendLine(String.Format("จำนวนเงิน: {0}", receipt.AMOUNT.ToString("#,##0.00")));
            data.AppendLine(String.Format("Code: {0}", receipt.LICENSE_TYPE_CODE));

            string qrres = receipt.LinkRecipt(ctx);
            if (qrres.Contains("ไม่พบข้อมูล"))
            {
                return null;
            }
            else
            {
                data.AppendLine(receipt.LinkRecipt(ctx));
                Bitmap img = encoder.Encode(data.ToString(), System.Text.Encoding.UTF8);
                ImageConverter converter = new ImageConverter();

                qrcode = (byte[])converter.ConvertTo(img, typeof(byte[]));
            }


            return qrcode;
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
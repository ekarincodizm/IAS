using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.TransactionBanking.Citi;
using IAS.DataServices.Payment.TransactionBanking.KTB;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class BankFileFactory
    {

        //public static BankFileHeader ConcreateBankFileTranfer(IAS.DAL.IASPersonEntities ctx, String fileName, DTO.UploadData data) 
        //{
        //    if (data.Body.FirstOrDefault().Substring(0,1)=="H") 
        //    { 
        //        return ConcreateKTBFileTransfer(ctx, fileName, data);
        //    } 
        //}

        #region CityBank File Upload
        public static CityFileHeader ConcreateCityBankFileTransfer(IAS.DAL.Interfaces.IIASPersonEntities ctx, String fileName, DTO.UploadData data)
        {
            String id = OracleDB.GetGenAutoId();
            CityFileHeader header = new CityFileHeader(ctx, id, fileName);
            Int32 numrow = 0;

            foreach (String record in data.Body)
            {
                numrow++;
                switch (record.Substring(0, 1))
                {
                    case "1": CreateCityBankHeader(header, record, numrow);
                        break;
                    case "2": header.AddBoxHeader(new CityFileBoxHeader()); 
                        break;
                    case "4": header.AddOverFlow(new CityFileOverFlow()); 
                        break;
                    case "5": header.AddBatchHeader(new CityFileBatchHeader()); 
                        break;
                    case "6": header.AddDetail(CreateCityBankDetail(ctx, record, numrow));
                        break;
                    case "7": header.AddBatchTotal(new CityFileBatchTotal());
                        break;
                    case "8": header.SetTotal(CreateCityBankTotal(record, numrow));
                        break;
                    case "9":
                        header.RowCount = Convert.ToInt32(record.Substring(1, 6));
                        break;
                    default:
                        break;
                }
            }

            return header;
        }


        public static CityFileHeader CreateCityBankHeader(CityFileHeader header, String lineData, Int32 rownum)
        {

            header.RECORD_TYPE = getSubstring(lineData ,0, 1);
            header.SEQUENCE_NO = rownum.ToString();
            header.BANK_CODE = getSubstring(lineData ,7, 3);
            header.COMPANY_ACCOUNT = getSubstring(lineData ,10, 10);
            header.COMPANY_NAME = getSubstring(lineData ,77, 10);
            header.EFFECTIVE_DATE = getSubstring(lineData ,63, 8);
            header.SERVICE_CODE = getSubstring(lineData ,68, 8);


            header.RECORD_TYPE = getSubstring(lineData ,0, 1);
            header.SEQUENCE_NO = rownum.ToString();
            header.BANK_CODE = getSubstring(lineData ,33, 3);
            header.COMPANY_ACCOUNT = getSubstring(lineData ,3, 10);
            //header.COMPANY_NAME = "";
            //header.EFFECTIVE_DATE = "";
            //header.SERVICE_CODE = "";
            return header;
        }

        private static CityFileTotal CreateCityBankTotal(String line, Int32 rownum)
        {
            return new CityFileTotal()
            {
                RECORD_TYPE = getSubstring(line ,0, 1),
                SEQUENCE_NO = rownum.ToString(),
                BANK_CODE = getSubstring(line, 34, 3),
                COMPANY_ACCOUNT = getSubstring(line ,3, 10),
                TOTAL_DEBIT_AMOUNT = "",
                TOTAL_DEBIT_TRANSACTION = "",
                TOTAL_CREDIT_AMOUNT = getSubstring(line ,14, 16),
                TOTAL_CREDIT_TRANSACTION = ""
            };
        }

        public static CityFileDetail CreateCityBankDetail(IAS.DAL.Interfaces.IIASPersonEntities ctx, String line, Int32 rownum)   
        {
            string detailId = OracleDB.GetGenAutoId();
            return new CityFileDetail()
            {
                ID = detailId,
                RECORD_TYPE = getSubstring(line ,0, 1),
                SequenceNo = rownum.ToString(),
                BANK_CODE = getSubstring(line ,144, 3),
                COMPANY_ACCOUNT = getSubstring(line ,3, 10),
                PAYMENT_DATE = getSubstring(line ,109, 8),
                PAYMENT_TIME = "",
                CUSTOMER_NAME = getSubstring(line ,209, 70),
                CUSTOMER_NO_REF1 = getSubstring(line ,389, 20),
                REF2 = getSubstring(line ,529, 20),
                REF3 = "",
                BRANCH_NO = getSubstring(line ,152, 4),
                TELLER_NO = "",
                KIND_OF_TRANSACTION = "",
                TRANSACTION_CODE = getSubstring(line ,51, 3),
                CHEQUE_NO = getSubstring(line , 31, 20),
                AMOUNT = getSubstring(line ,120, 13),
                CHEQUE_BANK_CODE = ""
            };
        }

        #endregion End CityBank File Upload


        #region KTB Bank File Upload
        public static BankFileHeader ConcreateKTBFileTransfer(IAS.DAL.Interfaces.IIASPersonEntities ctx, String fileName, DTO.UploadData data)
        {
            String id = OracleDB.GetGenAutoId();
            BankFileHeader header = new BankFileHeader(ctx, id, fileName);

            foreach (String record in data.Body)
            {
                switch (record.Substring(0, 1))
                {
                    case "H": CreateKTBHeader(header, record);
                        break;
                    case "D": header.AddDetail(CreateKTBDetail( record));
                        break;
                    case "T": header.SetTotal(CreateKTBTotal(record));
                        break;
                    default:
                        break;
                }
            }

            return header;
        }

        private static  String getSubstring(String text, Int32 index, Int32 length) {
            return (text.Length > (index + length-1)) ? text.Substring(index, length) : "";
        }

        private static BankFileTotal CreateKTBTotal(String line)
        {
            return new BankFileTotal()
            {
                RECORD_TYPE = getSubstring(line ,0, 1),
                SEQUENCE_NO = getSubstring(line ,1, 6),
                BANK_CODE = getSubstring(line ,7, 3),
                COMPANY_ACCOUNT = getSubstring(line ,10, 10),
                TOTAL_DEBIT_AMOUNT = getSubstring(line ,20, 13),
                TOTAL_DEBIT_TRANSACTION = getSubstring(line ,33, 6),
                TOTAL_CREDIT_AMOUNT = getSubstring(line ,39, 13),
                TOTAL_CREDIT_TRANSACTION = getSubstring(line ,52, 6),
            };
        }

        public static BankFileHeader CreateKTBHeader(BankFileHeader header, String lineData)
        {

            header.RECORD_TYPE = getSubstring(lineData ,0, 1);
            header.SEQUENCE_NO = getSubstring(lineData ,1, 6);
            header.BANK_CODE = getSubstring(lineData ,7, 3);
            header.COMPANY_ACCOUNT = getSubstring(lineData ,10, 10);
            header.COMPANY_NAME = getSubstring(lineData ,20, 40);
            header.EFFECTIVE_DATE = getSubstring(lineData ,60, 8);
            header.SERVICE_CODE = getSubstring(lineData ,68, 8);

            return header;
        }

        public static BankFileDetail CreateKTBDetail(String line)
        {
            string detailId = OracleDB.GetGenAutoId();
            return new BankFileDetail()
            {
                ID = detailId,
                RECORD_TYPE = getSubstring(line ,0, 1),
                SequenceNo = getSubstring(line ,1, 6),
                BANK_CODE = getSubstring(line ,7, 3),
                COMPANY_ACCOUNT = getSubstring(line ,10, 10),
                PAYMENT_DATE = getSubstring(line ,20, 8),
                PAYMENT_TIME = getSubstring(line ,28, 6),
                CUSTOMER_NAME = getSubstring(line ,34, 50),
                CUSTOMER_NO_REF1 = getSubstring(line ,84, 20),
                REF2 = getSubstring(line ,104, 20),
                REF3 = getSubstring(line ,124, 20),
                BRANCH_NO = getSubstring(line ,144, 4),
                TELLER_NO = getSubstring(line ,148, 4),
                KIND_OF_TRANSACTION = getSubstring(line ,152, 1),
                TRANSACTION_CODE = getSubstring(line ,153, 3),
                CHEQUE_NO = getSubstring(line ,156, 7),
                AMOUNT = getSubstring(line ,163, 13),
                CHEQUE_BANK_CODE = getSubstring(line ,176, 3),

            };
        }

        #endregion End KTB File Upload
    }
}
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment;
using IAS.DAL;
using System.Data;
using System.IO;
using IAS.DataServices.Payment.Helpers;
using System.Globalization;
using IAS.DataServices.Payment.Receipts;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class BillBizTests
    {


        [TestMethod]
        public void The_Finance_BillBiz_Can_GetBillNo_By_pass_parameter() 
        {
            String user_id = "50-1-147"; // user id ของ คปภ. ที่มีใน Table APPS_CONFIG_INPUT.USER_ID
            String doc_date = "01/09/2013";

            BillBiz billBiz = new BillBiz();

            String billNo = GenBillCodeFactory.GetBillNo(user_id, doc_date, "", "e1", "");

            String strqry = "select DOCNO from APPS_TABLE_YM_DOCNO where  branch_no=1 AND type='e1' AND year_month_break = '734869' AND section = '60600'";
            OracleDB ora = new OracleDB(ConnectionFor.OraDB_Finance);
            String doc_no = ora.GetDataTable(strqry).Rows[0][0].ToString();
            Assert.AreEqual(billNo, doc_no);
        }

        [TestMethod]
        public void The_Finance_BillBiz_Can_GetBudgetYear_By_OIC_user() 
        {
            String user_id = "50-1-147"; // user id ของ คปภ. ที่มีใน Table APPS_CONFIG_INPUT.USER_ID
            String doc_date = "01/09/2013";

            BillBiz billBiz = new BillBiz();

            string BudCode = string.Empty;
            BudCode = fnBKOFFGetUserConfigByField(user_id, "default_budget");
            doc_date = GenBillCodeFactory.fnBKOFFGetBudgetYear(doc_date, BudCode);

            Assert.AreNotEqual(doc_date, String.Empty);
        }


        private string fnBKOFFGetUserConfigByField(string user_id, string fieldName)
        {
            string sql = "Select " + fieldName + " From (" +
                         "SELECT a.*,b.description default_budget_desc,c.description default_project_desc" +
                         "       ,d.description default_activity_desc,e.description default_bud_account_desc" +
                         "       , f.description default_receive_group_desc, g.description default_payment_group_desc" +
                         "       , h.cus_name default_customer_desc,i.users_section,i.users_institute " +
                         "FROM apps_config_input a, appm_budget_code b,appm_project c, appm_activity d,appm_bud_account e,appm_receive_group f" +
                         "      ,appm_payment_group g,appm_customer h,apps_slc_users i 	" +
                         "WHERE a.user_id = '" + user_id + "' and a.menu_code = '73050' AND a.default_budget = b.budget_code(+) " +
                         "      AND a.default_project = c.project_code(+)  AND a.default_activity = d.activity(+) AND a.user_id=i.user_id(+) " +
                         "      AND a.default_bud_account = e.bud_account_code(+)  AND a.default_receive_group=f.receive_group(+) " +
                         "      AND a.default_payment_group=g.payment_group(+) AND a.default_customer_code=h.customer_code(+))";
            DataTable dt = new DataTable();
            using (var ora = new OracleDB(ConnectionFor.OraDB_Finance))
            {
                dt = ora.GetDataTable(sql);
            }

            return dt.Rows.Count > 0 ? dt.Rows[0][0].ToString() : string.Empty;
        }

        [TestMethod]
        public void The_Finance_BillBiz_Can_SubmtPaymentBankUpload() 
        {
            // แก้ ดึงข้อมูล Detail ให้ใช้ HEADER_ID
            //BillBiz billBiz = new BillBiz();
            //String user_id = "52-2-034";
            //String batch = "131001100254311";   // id temp ที่บันทึกไว้ที่ table AG_IAS_TEMP_PAYMENT_HEADER

            //bool isTrue = billBiz.SubmitPaymentBankUpload(_ctx, user_id, batch).ResultMessage;
             
            //Assert.IsTrue(isTrue);

        }

        [TestMethod]
        public void Test_Method_GenBankImportFile_From_Datebase() {
            String path = @"D:\OIC\Branch\IAS-branch-finance\IAS.DataServiceTest\SimpleFile\KTB_payment.txt";
            String[] ids = new String[4] { "999999561000000032", "999999561000000032", "999999561000000034", "999999561000000035" };
            //GenBankImportFileFromDataBase(path, ids);
            FileInfo fileInfo = new FileInfo(path);

            Assert.IsTrue(fileInfo.Exists);
        }


        private void GenBankImportFileFromDataBase(String path, List<String> ids) 
        {
            IAS.DAL.Interfaces.IIASPersonEntities ctx = DAL.DALFactory.GetPersonContext();
            IEnumerable<AG_IAS_PAYMENT_G_T> billPayments = ctx.AG_IAS_PAYMENT_G_T
                .Where(a => a.GROUP_REQUEST_NO == "999999561000000032"
                      || a.GROUP_REQUEST_NO == "999999561000000033"
                      || a.GROUP_REQUEST_NO == "999999561000000034"
                      || a.GROUP_REQUEST_NO == "999999561000000035").ToList();

          
            FileKTBBank fileKTBBank = new FileKTBBank(ctx);
            fileKTBBank.AddHeader();
            foreach (AG_IAS_PAYMENT_G_T item in billPayments)
	        {
                fileKTBBank.AddDetail(item);
	        }
            fileKTBBank.AddTotal();

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                file.WriteLine(convetANSI(fileKTBBank.LineKTBBankHeader.ToString()));

                foreach (LineKTBBankDetail line in fileKTBBank.LineKTBBankDetails)
                {
                    file.WriteLine( convetANSI( line.ToString()));
                }

                file.WriteLine(convetANSI(fileKTBBank.LineKTBBankTotal.ToString()));
            }

        }
        private String convetANSI(String str) {
            return str;
          //return  System.Text.Encoding.ASCII.GetString(Convert.ToByte( str.GetEnumerator()));
           
        }

    }


    public class FileKTBBank
    {
        private IAS.DAL.Interfaces.IIASPersonEntities _ctx;

        private LineKTBBankHeader _lineKTBBankHeader;
        private LineKTBBankTotal _lineKTBBankTotal;
        private IList<LineKTBBankDetail> _lineKTBBankDetails = new List<LineKTBBankDetail>();

        private Int32 _currentRow = 0;
        private String _bankCode = "006";
        private String _companyAccount = "3856001476";
        private DateTime _effactiveDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9,0,0);
        public String EffactiveDateString { get { return _effactiveDate.ToString("ddMMyyyy", CultureInfo.CreateSpecificCulture("en-US"));} }
        public FileKTBBank(IAS.DAL.Interfaces.IIASPersonEntities ctx)
        {
            _ctx = ctx;
            AddHeader();
        }


        public LineKTBBankHeader LineKTBBankHeader { get { return _lineKTBBankHeader; } set { _lineKTBBankHeader = value; } }
        public LineKTBBankTotal LineKTBBankTotal { get { return _lineKTBBankTotal; } set { _lineKTBBankTotal = value; } }
        public IList<LineKTBBankDetail> LineKTBBankDetails { get { return _lineKTBBankDetails; } }

        public void AddHeader() {
            _currentRow++;

            LineKTBBankHeader lineKTBBankHeader = new LineKTBBankHeader();
            lineKTBBankHeader.SequenceNo = _currentRow.ToString();
            lineKTBBankHeader.BankCode = _bankCode;
            lineKTBBankHeader.CompanyAccount = _companyAccount;
            lineKTBBankHeader.CompanyName = "สำนักงาน คปภ. รับสมัครสอบนายหน้าประกันชี";
            lineKTBBankHeader.EffectiveDate = EffactiveDateString;
            lineKTBBankHeader.ServiceCode = "9281";
            lineKTBBankHeader.Space = "";

            _lineKTBBankHeader = lineKTBBankHeader;
        }
        public void AddDetail(AG_IAS_PAYMENT_G_T payment) 
        {
            _currentRow++;
   
            AG_IAS_PERSONAL_T person = _ctx.AG_IAS_PERSONAL_T.Where(p=>p.ID==payment.CREATED_BY).FirstOrDefault();
            LineKTBBankDetail detail = new LineKTBBankDetail()
            {
                SequenceNo = _currentRow.ToString(),
                BankCode = _bankCode,
                CompanyAccount = _companyAccount,
                PaymentDate = EffactiveDateString,
                PaymentTime = _effactiveDate.AddMinutes(30).AddSeconds(8).ToString("HHmmss"),
                CustomerName = (person == null) ? "" : String.Format("{0} {1}", person.NAMES, person.LASTNAME),
                Customer_No_Ref1 = payment.GROUP_REQUEST_NO,
                Ref2 = (payment.REF2==null)?"":payment.REF2,
                Ref3 = "",
                BranchNo = "0602",
                TellerNo = "9953",
                KindOfTransaction = "C",
                TransactionCode = "CSH",
                ChequeNo = "",
                Amount = ((payment.GROUP_AMOUNT==null)?"": ((Decimal)payment.GROUP_AMOUNT).ToString("0.00").Replace(".","") ),
                ChequeBankCode = "",
                Spece = ""


            };
            _lineKTBBankDetails.Add(detail);
        }

        public void AddTotal() {
            _currentRow++;
            LineKTBBankTotal lineKTBBankTotal = new LineKTBBankTotal() { 
                SequenceNo=_currentRow.ToString(),
                BankCode = _bankCode,
                CompanyAccount = _companyAccount,
                TotalDebitAmount = LineKTBBankDetails.Where(l=>l.KindOfTransaction=="D").Sum(s=> Convert.ToDecimal(s.Amount)).ToString(),
                TotalDebitTransaction = LineKTBBankDetails.Where(l=>l.KindOfTransaction=="D").Count().ToString(),
                TotalCreditAmount = LineKTBBankDetails.Where(l=>l.KindOfTransaction=="C").Sum(s=> Convert.ToDecimal(s.Amount)).ToString(),
                TotalCreditTransaction = LineKTBBankDetails.Where(l=>l.KindOfTransaction=="C").Count().ToString(),
                Space = ""
            };

            LineKTBBankTotal = lineKTBBankTotal;
        }
    }

    public class LineKTBBankHeader
    {
        private String _lineHeader;

        private String _recordType = "H";
        private String _sequenceNo;
        private String _bankCode;
        private String _companyAccount;
        private String _companyName;
        private String _effectiveDate;
        private String _serviceCode;
        private String _space;

        public LineKTBBankHeader ()
	    {

	    }
        public LineKTBBankHeader(String lineHeader)
        {
            _lineHeader = lineHeader;
            ExtractHeaderLine();
        }

        private void ExtractHeaderLine()
        {
            _recordType = _lineHeader.Substring(0, 1);
            _sequenceNo = _lineHeader.Substring(1, 6);
            _bankCode = _lineHeader.Substring(7, 3);
            _companyAccount = _lineHeader.Substring(10, 10);
            _companyName = _lineHeader.Substring(20, 40);
            _effectiveDate = _lineHeader.Substring(60, 8);
            _serviceCode = _lineHeader.Substring(68, 8);
            _space = _lineHeader.Substring(76, 180);
        }

        public String RecordType { get { return _recordType.Trim(); } set { _recordType = value.Trim().PadRight(1, ' '); } } // length 1
        public String SequenceNo { get { return _serviceCode.Trim(); } set { _sequenceNo = value.Trim().PadLeft(6, '0'); } } // length 6
        public String BankCode { get { return _bankCode.Trim(); } set { _bankCode = value.Trim().PadRight(3, ' '); } } // length 3
        public String CompanyAccount { get { return _companyAccount.Trim(); } set { _companyAccount = value.Trim().PadRight(10, ' '); } } // length 10
        public String CompanyName { get { return _companyName.Trim(); } set { _companyName = value.Trim().PadRight(40, ' '); } } // length 40
        public String EffectiveDate { get { return _effectiveDate.Trim(); } set { _effectiveDate = value.Trim().PadRight(8, ' '); } } // length 8
        public String ServiceCode { get { return _serviceCode.Trim(); } set { _serviceCode = value.Trim().PadRight(8, ' '); } } // length 8
        public String Space { get { return _space.Trim(); } set { _space = value.Trim().PadRight(180, ' '); } } // length 180

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", _recordType, _sequenceNo, _bankCode, _companyAccount, _companyName, _effectiveDate, _serviceCode, _space);
        }
    }
    public class LineKTBBankDetail 
    {
        private String _lineDetail;

        private String _recordType = "D";     
        private String _sequenceNo;
        private String _bankCode;
        private String _companyAccount;
        private String _paymentDate;
        private String _paymentTime;
        private String _customerName;
        private String _customer_No_Ref1;
        private String _ref2;
        private String _ref3 = "";
        private String _branchNo;
        private String _tellerNo;
        private String _kindOfTransaction;
        private String _transactionCode;
        private String _chequeNo;
        private String _amount;
        private String _chequeBankCode;
        private String _space;

        public LineKTBBankDetail()
        {

        }
        public LineKTBBankDetail(String lineDetail)
        {
            _lineDetail = lineDetail;

            ExtractLineDetail();
        }

        private void ExtractLineDetail()
        {
            _recordType = _lineDetail.Substring(0, 1);
            _sequenceNo = _lineDetail.Substring(1, 6);
            _bankCode = _lineDetail.Substring(7, 3);
            _companyAccount = _lineDetail.Substring(10, 10);
            _paymentDate = _lineDetail.Substring(20, 8);
            _paymentTime = _lineDetail.Substring(28, 6);
            _customerName = _lineDetail.Substring(34, 50);
            _customer_No_Ref1 = _lineDetail.Substring(84, 20);
            _ref2 = _lineDetail.Substring(104, 20);
            _ref3 = _lineDetail.Substring(124, 20);
            _branchNo = _lineDetail.Substring(144, 4);
            _tellerNo = _lineDetail.Substring(148, 4);
            _kindOfTransaction = _lineDetail.Substring(152, 1);
            _transactionCode = _lineDetail.Substring(153, 3);
            _chequeNo = _lineDetail.Substring(156, 7);
            _amount = _lineDetail.Substring(163, 13);
            _chequeBankCode = _lineDetail.Substring(176, 3);
            _space = _lineDetail.Substring(179, 77);
        }

        public String RecordType { get { return _recordType.Trim(); } set { _recordType = value.Trim().PadRight(1, ' '); } } // len 1  "D"=Detail
        public String SequenceNo { get { return _sequenceNo.Trim(); } set { _sequenceNo = value.Trim().PadLeft(6, '0'); } } // len 6  Running Sequence No.
        public String BankCode { get { return _bankCode.Trim(); } set { _bankCode = value.Trim().PadRight(3, ' '); } }   // len 3
        public String CompanyAccount { get { return _companyAccount.Trim(); } set { _companyAccount = value.Trim().PadRight(10, ' '); } } // len 10
        public String PaymentDate { get { return _paymentDate.Trim(); } set { _paymentDate = value.Trim().PadRight(8, ' '); } }  // len 8       DDMMYYYY (วันที่รับชำระ)
        public String PaymentTime { get { return _paymentTime.Trim(); } set { _paymentTime = value.Trim().PadRight(6, ' '); } }  // len 6       HHMMSS (เวลาที่รับชำระ)
        public String CustomerName { get { return _customerName.Trim(); } set { _customerName = value.Trim().PadRight(50, ' '); } }  // len 50
        public String Customer_No_Ref1 { get { return _customer_No_Ref1.Trim(); } set { _customer_No_Ref1 = value.Trim().PadRight(20,' '); } }  // len 20
        public String Ref2 { get { return _ref2.Trim(); } set { _ref2 = value.Trim().PadRight(20, ' '); } }     // len 20
        public String Ref3 { get { return _ref3.Trim(); } set { _ref3 = value.Trim().PadRight(20, ' '); } }    // len 20
        public String BranchNo { get { return _branchNo.Trim(); } set { _branchNo = value.Trim().PadRight(4, ' '); } }  // len 4
        public String TellerNo { get { return _tellerNo.Trim(); } set { _tellerNo = value.Trim().PadRight(4, ' '); } }   // len 4
        public String KindOfTransaction { get { return _kindOfTransaction.Trim(); } set { _kindOfTransaction = value.Trim().PadRight(1, ' '); } }  // len 1   "C"=Credit, "D"=Debit
        public String TransactionCode { get { return _transactionCode.Trim(); } set { _transactionCode = value.Trim().PadRight(3, ' '); } }  // len 3
        public String ChequeNo { get { return _chequeNo.Trim(); } set { _chequeNo = value.Trim().PadRight(7, ' '); } }   // len 7
        public String Amount { get { return _amount.Trim(); } set { _amount = value.Trim().PadLeft(13,'0'); } }    // len 13
        public String ChequeBankCode { get { return _chequeBankCode.PadRight(3, ' '); } set { _chequeBankCode = value.Trim().PadRight(3, '0'); } }   // len 3
        public String Spece { get { return _space.Trim(); } set { _space = value.Trim().PadRight(77, ' '); } }     // len 77

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}",
                                _recordType,
                                _sequenceNo,
                                _bankCode,
                                _companyAccount,
                                _paymentDate,
                                _paymentTime,
                                _customerName,
                                _customer_No_Ref1,
                                _ref2,
                                _ref3,
                                _branchNo,
                                _tellerNo,
                                _kindOfTransaction,
                                _transactionCode,
                                _chequeNo,
                                _amount,
                                _chequeBankCode,
                                _space);
        }
    }

    public class LineKTBBankTotal
    {
        private String _lineTotal ;

        private String _recordType = "T";
        private String _sequenceNo;
        private String _bankCode;
        private String _companyAccount;
        private String _totalDebitAmount;
        private String _totalDebitTransaction;
        private String _totalCreditAmount;
        private String _totalCreditTransaction;
        private String _space;

        public LineKTBBankTotal()
        {

        }
        public LineKTBBankTotal(String lineTotal)
        {
            _lineTotal = lineTotal;

            ExtractLineTotal();
        }

        private void ExtractLineTotal()
        {
            _recordType = _lineTotal.PadRight(1, ' ');
            _sequenceNo = _lineTotal.PadLeft(6, '0');
            _bankCode = _lineTotal.PadRight(3, ' ');
            _companyAccount = _lineTotal.PadRight(10, ' ');
            _totalDebitAmount = _lineTotal.PadRight(13, ' ');
            _totalDebitTransaction = _lineTotal.PadRight(6, ' ');
            _totalCreditAmount = _lineTotal.PadRight(13, ' ');
            _totalCreditTransaction = _lineTotal.PadRight(6, ' ');
            _space = _lineTotal.PadRight(198, ' ');
        }

        public String RecordType { get { return _recordType.Trim().Trim(); } set { _recordType = value.Trim().PadRight(1, ' '); } }   // len 1   "T"=Total
        public String SequenceNo { get { return _sequenceNo.Trim(); } set { _sequenceNo = value.Trim().PadLeft(6, '0'); } }     // len 6  Last Sequence No.
        public String BankCode { get { return _bankCode.Trim(); } set { _bankCode = value.Trim().PadRight(3, ' '); } }        // len 3
        public String CompanyAccount { get { return _companyAccount.Trim(); } set { _companyAccount = value.Trim().PadRight(10, ' '); } }   // len 10
        public String TotalDebitAmount { get { return _totalDebitAmount.Trim(); } set { _totalDebitAmount = value.Trim().PadLeft(13, '0'); } }   // len 13
        public String TotalDebitTransaction { get { return _totalDebitTransaction.Trim(); } set { _totalDebitTransaction = value.Trim().PadLeft(6, '0'); } }    // len 6
        public String TotalCreditAmount { get { return _totalCreditAmount.Trim(); } set { _totalCreditAmount = value.Trim().PadLeft(13, '0'); } }        // len 13
        public String TotalCreditTransaction { get { return _totalCreditTransaction.Trim(); } set { _totalCreditTransaction = value.Trim().PadLeft(6, '0'); } }   // len 6
        public String Space { get { return _space.Trim(); } set { _space = value.Trim().PadRight(198, ' '); } }          // len 198

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", _recordType, _sequenceNo, _bankCode, _companyAccount,
                _totalDebitAmount, _totalDebitTransaction, _totalCreditAmount, _totalCreditTransaction, _space); 
        }




    }

    

}
                                                      
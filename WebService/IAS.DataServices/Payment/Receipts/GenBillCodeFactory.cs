using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using System.Data;
using System.Configuration;
using System.Globalization;

namespace IAS.DataServices.Payment.Receipts
{
    public class GenBillCodeFactory
    {
        public static string GetBillNo(string user_id, string doc_date,
                               string doc_code, string doc_type,
                               string date_mode)
        {
            //user_id = "52-2-034";
            OracleDB ora = new OracleDB(ConnectionFor.OraDB_Finance);


            //หา doc_code

            if (string.IsNullOrEmpty(doc_code))
            {
                string sqlDocCode = String.Format("select doc_code from apps_table_document where doc_type='{0}'", doc_type);
                DataTable dtDocCode = ora.GetDataTable(sqlDocCode);
                if (dtDocCode != null && dtDocCode.Rows.Count > 0)
                {
                    doc_code = dtDocCode.Rows[0][0].ToString();
                }
            }

            string BudYear = string.Empty;
            string SQLDocRunning = "SELECT DOC_RUNNING FROM apps_table_ym_docno";
            string SQLDocRunningWhere = string.Empty;
            string BranchNo = fnBKOFFGetUserConfigByField(user_id, "USERS_INSTITUTE");


            SQLDocRunningWhere = BranchNo.Length > 0
                                        ? " WHERE branch_no=" + BranchNo + " AND type='" + doc_type + "'"
                                        : " WHERE type='" + doc_type + "'";


            if (doc_date.Trim().Length == 0)
            {
                doc_date = doc_date.Trim().Length == 0
                                ? DateTime.Today.ToString("dd/MM/") +
                                  DateTime.Today.Year.ToString("0000")
                                : doc_date;
            }
            else
            {
                if (doc_date.IndexOf('/') == -1)
                {
                    doc_date = "01/01/" + DateTime.Today.Year.ToString("0000");
                }
                else
                {
                    if (doc_date.Length != 10)
                    {
                        string docNo = "0000000000";
                        return docNo;
                    }
                }
                BudYear = doc_date;
            }

            doc_date = doc_date.Trim().Length == 0 ? string.Empty : doc_date;

            string SQL = "SELECT doc_code,doc_type,doc_desc,doc_runby,year_type,section_digits,doc_usesection" +
                         "       ,doc_usebranch,seq_docno,doc_usetype,doc_rundigit FROM apps_table_document " +
                         "WHERE doc_code='" + doc_code + "' AND doc_type='" + doc_type + "'";

            string DocDesc = "";
            string DocRunBy = "";
            string YearType = "";
            string SectionDigit = "";
            string DocUseSection = "";
            string DocUseBranch = "";
            string SeqDocNo = "";
            string DocUseType = "";
            string DocRunDigit = "";
            string SectionNo = "";
            string DocNo = "";

            DataTable dt = ora.GetDataTable(SQL);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                doc_code = dr[0].ToString();
                doc_type = dr[1].ToString();
                DocDesc = dr[2].ToString();
                DocRunBy = dr[3].ToString();
                YearType = dr[4].ToString();
                SectionDigit = dr[5].ToString();
                DocUseSection = dr[6].ToString();
                DocUseBranch = dr[7].ToString();
                SeqDocNo = dr[8].ToString();
                DocUseType = dr[9].ToString();
                DocRunDigit = dr[10].ToString();
            }

            int YearBreak = 0;
            if (DocRunBy.Length == 0)
            {
                return "0000000000";
            }
            else
            {
                if (DocRunBy == "2")
                {
                    if (YearType.Trim().Length == 0)
                    {
                        DateTime tmp = new DateTime(Convert.ToInt32(doc_date.Substring(6, 4)), 1, 1);
                        YearBreak = fnBKOFFGetNumDate(tmp);
                    }
                    else if (YearType == "1" || YearType == "3")
                    {
                        string tmp = "01/01/" + doc_date.Substring(6, 4);
                        YearBreak = fnBKOFFGetNumDate(tmp);
                    }
                    else if (YearType == "2")
                    {
                        string BudCode = string.Empty;
                        BudCode = fnBKOFFGetUserConfigByField(user_id, "default_budget");
                        doc_date = fnBKOFFGetBudgetYear(doc_date, BudCode);
                        YearBreak = fnBKOFFGetNumDate(doc_date);
                    }
                }
                else if (DocRunBy == "3")
                {

                    string tmp = "01/" + doc_date.Substring(3, 7);
                    YearBreak = fnBKOFFGetNumDate(tmp);

       
                }
                else if (DocRunBy == "4")
                {
                    YearBreak = fnBKOFFGetNumDate(doc_date);
                }
            }

            if (YearBreak > 0 && BranchNo.Length > 0)
            {
                SQLDocRunningWhere += " AND year_month_break = '" + YearBreak.ToString() + "'";
            }

            if (DocUseSection == "1")
            {
                // 2013-10-11 ปรับ ให้ใช้ ข้อมูล รหัสฝ่าย จาก Web.config  tik
                SectionNo = ConfigurationManager.AppSettings["OIC_SECTION"].ToString();  //fnBKOFFGetUserConfigByField(user_id, "USERS_SECTION");

                if (SectionNo.Length == 0) SectionNo = " ";

                SQLDocRunningWhere = String.Format("{0} AND section = '{1}'", SQLDocRunningWhere, SectionNo);
            }

            if (SectionDigit == "0") SectionDigit = "10";

            //SectionNo = (SectionNo.Length > Convert.ToInt32( SectionDigit)) ? SectionNo.Substring(0, Convert.ToInt32(SectionDigit)) : SectionNo ;

            string MM = doc_date.Substring(3, 2);
            string DD = doc_date.Substring(0, 2);

            MM = fnBKOFFPadStr(MM, "0", 2, "B");
            DD = fnBKOFFPadStr(DD, "0", 2, "B");

            if (date_mode == "T")
            {
                doc_date = doc_date.Substring(0, 6) + (Convert.ToInt32(doc_date.Substring(6, 4)) + 543).ToString("0000");
            }

            string Seq1;
            string Seq2;
            string Seq3;
            string Seq4;
            GenerateReceiptNumber(doc_date,
                                    doc_type,
                                    BranchNo,
                                    DocRunBy,
                                    SectionDigit,
                                    DocUseSection,
                                    DocUseBranch,
                                    SeqDocNo,
                                    DocUseType,
                                    SectionNo,
                                    MM,
                                    DD,
                                    out Seq1,
                                    out Seq2,
                                    out Seq3,
                                    out Seq4);

            long doc_runing = 0;
            string doc_runing_text = string.Empty;

            SQL = SQLDocRunning + SQLDocRunningWhere +
                  " AND DOC_RUNNING = (SELECT MAX(DOC_RUNNING) FROM apps_table_ym_docno " +
                  SQLDocRunningWhere + ")";
            dt = ora.GetDataTable(SQL);
            doc_runing = dt.Rows.Count > 0 ? Convert.ToInt64(dt.Rows[0][0]) : 0;
            doc_runing += 1;
            doc_runing_text = fnBKOFFPadStr(doc_runing.ToString(), "0", Convert.ToInt32(DocRunDigit), "B");

            if (Convert.ToInt32(SeqDocNo) == 5)
            {
                DocNo = Seq2 + "-" + Seq3 + "-" + doc_runing_text + "/" + Seq4;
                DataTable dt_Do = ora.GetDataTable("select doc_no from APPR_DO_HEADER where doc_no='" + DocNo + "'");
                while (dt_Do != null && dt_Do.Rows.Count > 0)
                {
                    doc_runing += 1;
                    doc_runing_text = fnBKOFFPadStr(doc_runing.ToString(), "0", Convert.ToInt32(DocRunDigit), "B");
                    DocNo = Seq2 + "-" + Seq3 + "-" + doc_runing_text + "/" + Seq4;
                    dt_Do = ora.GetDataTable("select doc_no from APPR_DO_HEADER where doc_no='" + DocNo + "'");


                } ;
                
            }
            else
            {
                DocNo = Seq1 + Seq2 + Seq3 + Seq4 + doc_runing_text;
                DataTable dt_Do = ora.GetDataTable("select doc_no from APPR_DO_HEADER where doc_no='" + DocNo + "'");
                while (dt_Do != null && dt_Do.Rows.Count > 0)
                {
                    doc_runing += 1;
                    doc_runing_text = fnBKOFFPadStr(doc_runing.ToString(), "0", Convert.ToInt32(DocRunDigit), "B");
                    DocNo = Seq1 + Seq2 + Seq3 + Seq4 + doc_runing_text;
                    dt_Do = ora.GetDataTable("select doc_no from APPR_DO_HEADER where doc_no='" + DocNo + "'");


                } ;
            }

            if (doc_runing == 1)
            {
                string TypeCalCost = " ";
                SQL = "Insert Into APPS_TABLE_YM_DOCNO " +
                      "(Branch_No,InternalCode,Type,Year_Month_Break,Doc_Running,DocNo,Type_Cal_Cost,Section) " +
                      "VALUES('" + BranchNo + "','" + doc_code + "','" + doc_type + "','" +
                              YearBreak.ToString() + "','" + doc_runing.ToString() + "','" +
                              DocNo + "','" + TypeCalCost + "','" + SectionNo + "')";
            }
            else
            {
                
                
                SQL = "Update apps_table_ym_docno " +
                      "SET DOCNO = '" + DocNo + "',DOC_RUNNING = '" + doc_runing.ToString() + "' " +
                      SQLDocRunningWhere;
            }

            ora.ExecuteCommand(SQL);
            ora.Dispose();

            return DocNo;
        }

        private static void GenerateReceiptNumber(string doc_date, string doc_type, string BranchNo, string DocRunBy, string SectionDigit, string DocUseSection, string DocUseBranch, string SeqDocNo, string DocUseType, string SectionNo, string MM, string DD, out string Seq1, out string Seq2, out string Seq3, out string Seq4)
        {
            string YY = doc_date.Substring(8, 2);

            Seq1 = string.Empty;
            Seq2 = string.Empty;
            Seq3 = string.Empty;
            Seq4 = string.Empty;

            if (SeqDocNo.Length == 0 || SeqDocNo == " ") //Defaul format doc_no Type/Institute/Section/YY/Running
            {
                if (DocUseType == "1") Seq1 = doc_type;
                if (DocUseBranch == "1") Seq2 = BranchNo;
                if (DocUseSection == "1") Seq3 = SectionNo;// fnBKOFFPadStr(SectionNo, "0", Convert.ToInt32(SectionDigit), "B");
                if (DocRunBy == "2") Seq4 = YY;  // YY (2555 / 2012)
                else if (DocRunBy == "3") Seq4 = YY + MM;
                else if (DocRunBy == "4") Seq4 = YY + MM + DD;
                else Seq4 = "";
            }
            else if (SeqDocNo == "1")
            {
                if (DocUseBranch == "1") Seq1 = BranchNo;
                if (DocUseSection == "1") Seq2 = SectionNo; // fnBKOFFPadStr(SectionNo, "0", Convert.ToInt32(SectionDigit), "B");
                if (DocUseType == "1") Seq3 = doc_type;
                if (DocRunBy == "2") Seq4 = YY; //(2555 / 2012)
                else if (DocRunBy == "3") Seq4 = YY + MM;
                else if (DocRunBy == "4") Seq4 = YY + MM + DD;
                else Seq4 = "";
            }
            else if (SeqDocNo == "2")
            {
                if (DocUseBranch == "1") Seq1 = BranchNo;
                if (DocUseSection == "1") Seq2 = SectionNo; // fnBKOFFPadStr(SectionNo, "0", Convert.ToInt32(SectionDigit), "B");
                if (DocRunBy == "2") Seq3 = YY; //(2555 / 2012)
                else if (DocRunBy == "3") Seq3 = YY + MM;
                else if (DocRunBy == "4") Seq3 = YY + MM + DD;
                else Seq3 = "";

                if (DocUseType == "1") Seq4 = doc_type;
            }
            else if (SeqDocNo == "3")
            {
                if (DocUseType == "1") Seq1 = doc_type;
                if (DocUseBranch == "1") Seq2 = BranchNo;
                if (DocUseSection == "1") Seq3 = SectionNo; // fnBKOFFPadStr(SectionNo, "0", Convert.ToInt32(SectionDigit), "B");

                if (DocRunBy == "2") Seq4 = YY; //(2555 / 2012)
                else if (DocRunBy == "3") Seq4 = YY + MM;
                else if (DocRunBy == "4") Seq4 = YY + MM + DD;
                else Seq4 = "";
            }
            else if (SeqDocNo == "4")
            {
                if (DocRunBy == "2") Seq1 = YY; //(2555 / 2012)
                else if (DocRunBy == "3") Seq1 = YY + MM;
                else if (DocRunBy == "4") Seq1 = YY + MM + DD;
                else Seq1 = "";

                if (DocUseBranch == "1") Seq2 = BranchNo;
                if (DocUseSection == "1") Seq3 = SectionNo;// fnBKOFFPadStr(SectionNo, "0", Convert.ToInt32(SectionDigit), "B");
                if (DocUseType == "1") Seq4 = doc_type;
            }
            else if (SeqDocNo == "5")
            {
                if (DocUseBranch == "1") Seq1 = BranchNo;
                if (DocUseType == "1") Seq2 = doc_type;
                if (DocUseSection == "1") Seq3 = SectionNo;// fnBKOFFPadStr(SectionNo, "0", Convert.ToInt32(SectionDigit), "B");

                if (DocRunBy == "2") Seq4 = YY; //(2555 / 2012)
                else if (DocRunBy == "3") Seq4 = YY + MM;
                else if (DocRunBy == "4") Seq4 = YY + MM + DD;
                else Seq1 = "";
            }
        }
        private static string fnBKOFFPadStr(string str_input, string str_pad, int str_len, string pad_direction)
        {
            if (str_input.Length > str_len) str_input = str_input.Substring(0, str_len);
            while (str_input.Length < str_len)
            {
                if (pad_direction == "B") //BEFORE
                {
                    str_input = str_pad + str_input;
                }
                else if (pad_direction == "A") //AFTER
                {
                    str_input += str_pad;
                }
            }

            return str_input;
        }

        private static string fnBKOFFGetUserConfigByField(string user_id, string fieldName)
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

        private static int fnBKOFFGetNumDate(string ParamDate)
        {
            if (ParamDate.Length != 10) return 0;

            int dd = Convert.ToInt32(ParamDate.Substring(0, 2));
            int mm = Convert.ToInt32(ParamDate.Substring(3, 2));
            int yy = Convert.ToInt32(ParamDate.Substring(6, 4));
            DateTime tmpDate = new DateTime(yy, mm, dd);
            return fnBKOFFGetNumDate(tmpDate);
        }

        private static int fnBKOFFGetNumDate(DateTime ParamDate)
        {
            DateTime startDate = new DateTime(1, 1, 1);
            return ParamDate.Subtract(startDate).Days + 1;
        }

        public static string fnBKOFFGetBudgetYear(string doc_date, string bud_code)
        {
            OracleDB ora = new OracleDB(ConnectionFor.OraDB_Finance);
            string sql = string.Empty;
            DataTable dt = new DataTable();
            string bud_year = string.Empty;

            if (doc_date.IndexOf('/') == -1)
            {
                string ddMM = DateTime.Today.ToString("dd/MM/");
                doc_date = doc_date.Length == 4
                                ? ddMM + doc_date
                                : ddMM + DateTime.Today.Year.ToString("0000", CultureInfo.CreateSpecificCulture("en-US"));
            }

            if (bud_code.Length == 0)
            {
                sql = "SELECT BUDGET_CODE FROM APPM_BUDGET_CODE ORDER BY BUDGET_CODE ASC";
                dt = ora.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    bud_code = dt.Rows[0][0].ToString();
                }
            }

            Func<string, string> GetBudYear = strSQL =>
            {
                dt = ora.GetDataTable(sql);
                return dt.Rows.Count > 0
                            ? ((DateTime)dt.Rows[0][0]).ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"))
                            : string.Empty;
            };

            sql = "SELECT  BUD_YEAR FROM appm_budget_carlendar WHERE budget_code = '" + bud_code + "'" +
                                "AND TO_DATE('" + doc_date + "', 'DD/MM/YYYY') >= date_start  " +
                                "AND TO_DATE('" + doc_date + "', 'DD/MM/YYYY') <= date_end " +
                                "ORDER BY budget_code DESC";
            bud_year = GetBudYear(sql);

            if (bud_year.Length == 0)
            {
                sql = "SELECT  BUD_YEAR FROM appm_budget_carlendar " +
                      "WHERE TO_DATE('" + doc_date + "', 'DD/MM/YYYY') >= date_start  " +
                      "      AND TO_DATE('" + doc_date + "', 'DD/MM/YYYY') <= date_end " +
                      "ORDER BY budget_code DESC";

                bud_year = GetBudYear(sql);
            }

            return bud_year;
        }
     
    }
}
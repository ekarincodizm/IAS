using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using IAS.Utils;
using System.Reflection;
using System.Text.RegularExpressions;
using IAS.Properties;

namespace IAS.Mockup
{
    public partial class webTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string[] file = Request.CurrentExecutionFilePath.Split('/');
                string fileName = file[file.Length - 1];


                var a = new A();
                gv1.DataSource = a.GetData();
                gv1.DataBind();
            }
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetApproveConfig();
            gv.DataSource = res.DataResponse;
            gv.DataBind();
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            //BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //var res = biz.GetApproveConfig();
            //res.DataResponse[0].ITEM_VALUE = "N";
            //res.DataResponse[1].ITEM_VALUE = "N";
            //var rm = biz.SetApproveConfig(res.DataResponse);
            //Response.Write(rm.DataResponse.ResultMessage);
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            //DAL.IASPersonEntities ctx = new DAL.IASPersonEntities();
            //var ent = ctx.AG_EDUCATION_R.Where(w => w.EDUCATION_CODE == "03").FirstOrDefault();
            //ent.EDUCATION_NAME = "ป.เอก";
            //ctx.SaveChanges();
            ////ctx.AG_EDUCATION_R.AddObject(new DAL.AG_EDUCATION_R
            ////{
            ////    EDUCATION_CODE = "03",
            ////    EDUCATION_NAME = "ทดสอบ"
            ////});
            ////ctx.SaveChanges();
            //Response.Write(Resources.errorwebTestUpload_002);
        }

        protected void btnInsert0_Click(object sender, EventArgs e)
        {
            //BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            //var res = biz.GetRegistrationsByCriteria("", "", "", "", "", new DateTime(2003, 8, 17));

            var biz = new BLL.ExamScheduleBiz();
            var res = biz.GetExamByYearMonth("201301");

            gv.DataSource = res.DataResponse;
            gv.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connStr = "Data Source=OIC;User Id=AGDOI;Password=password;";
            Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connStr);
            if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            conn.Open();
            Oracle.DataAccess.Client.OracleCommand comm = new Oracle.DataAccess.Client.OracleCommand();

            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "AG_GET_PETNAME"; // "AG_TESTING_RUNNING";
            comm.Connection = conn;
            //comm.BindByName = true;

            //comm.CommandType = System.Data.CommandType.Text;
            //comm.CommandText = "Select * From ag_app_running_no_t;";
            //comm.Connection = conn;


            //comm.Parameters.Add("I_Date", OracleDbType.Date).Value = DateTime.Now.Date;
            //comm.Parameters.Add("i_license_type",  OracleDbType.Varchar2).Value = "";
            //comm.Parameters.Add("I_Exam_Status", OracleDbType.Varchar2).Value = "";
            //comm.Parameters.Add("V_Year", OracleDbType.Varchar2).Direction = System.Data.ParameterDirection.ReturnValue;
            //comm.Parameters.Add("V_Last_No", OracleDbType.Varchar2).Direction = System.Data.ParameterDirection.ReturnValue;

            //OracleDataAdapter da = new OracleDataAdapter(comm);
            //System.Data.DataTable dt = new System.Data.DataTable();
            //da.Fill(dt);

            comm.Parameters.Add("I_Code", OracleDbType.Varchar2, 2, "11", System.Data.ParameterDirection.Input);

            comm.Parameters.Add("V_Name", OracleDbType.Varchar2, 120).Direction = System.Data.ParameterDirection.Output;


            comm.ExecuteNonQuery();


            //string vName = comm.Parameters["V_Name"].Value.ToString();

            //string vYear = comm.Parameters["V_Year"].Value.ToString();
            //string vLastNo = comm.Parameters["V_Last_No"].Value.ToString();


            //Response.Write(string.Format("vYear = {0}, vLastNo = {1}", vYear, vLastNo));


            //Oracle.DataAccess.Client.OracleParameter p1 = new Oracle.DataAccess.Client.OracleParameter("I_Date", OracleDbType.Date);
            //p1.Value = DateTime.Now.Date;

            //Oracle.DataAccess.Client.OracleParameter p2 = new Oracle.DataAccess.Client.OracleParameter("I_License_Type", OracleDbType.Varchar2);
            //p2.Value = string.Empty;

            //Oracle.DataAccess.Client.OracleParameter p3 = new Oracle.DataAccess.Client.OracleParameter("I_Exam_Status", OracleDbType.Varchar2);
            //p3.Value = string.Empty;

            //comm.Parameters.Add(p1);
            //comm.Parameters.Add(p2);
            //comm.Parameters.Add(p3);

            //Oracle.DataAccess.Client.OracleDataAdapter da = new Oracle.DataAccess.Client.OracleDataAdapter(comm);
            //System.Data.DataSet ds = new System.Data.DataSet();
            //da.Fill(ds);

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DTO.ExamSchedule ent = new DTO.ExamSchedule();
            ent.TESTING_NO = "561444";
            ent.EXAM_PLACE_CODE = "42000";
            ent.TESTING_DATE = DateTime.Now.Date;
            ent.TEST_TIME_CODE = "05";
            ent.LICENSE_TYPE_CODE = "02";
            ent.USER_ID = "AGDOI2";
            ent.USER_DATE = DateTime.Now;
            ent.EXAM_STATUS = "E";

            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            //var res =  biz.UpdateExam(ent);

            //Response.Write(res.IsError);
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            var list = IAS.Utils.DateUtil.GetMonthList("");
            gv.DataSource = list;
            gv.DataBind();
        }

        protected void btnGetData_Click(object sender, EventArgs e)
        {
            var biz = new BLL.ApplicantBiz();
            //var res = biz.GetApplicantByCriteria("", "","", "1", "", "", null,null, "", "");
            //var res = biz.PersonGetApplicantByCriteria("1111111111122");
            //var res = biz.GetApplicantByLicenseType("");
            //gv.DataSource = res.DataResponse;
            //gv.DataBind();
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            //DAL.AG_IAS_APPLICANT_SCORE_D_TEMP ent = new DAL.AG_IAS_APPLICANT_SCORE_D_TEMP
            //{
            //    SCORE_1 = "1",
            //    SCORE_2 = "2",
            //    SCORE_3 = "3"
            //};
            //PropertyInfo[] prop = ent.GetType().GetProperties();
            //int iCount = prop.Count();
            //List<DTO.DataItem> list = new List<DTO.DataItem>();
            //for (int i = 0; i < iCount; i++)
            //{
            //    list.Add(new DTO.DataItem
            //    {
            //        Id = prop[i].Name,
            //        Name = ent.GetType().GetProperty(prop[i].Name).GetValue(ent, null).ToString()
            //    });
            //}

            //gv.DataSource = list;
            //gv.DataBind();
        }

        private void GetDataToGrid(DTO.RegistrationType regType, string compCode, string idCard)
        {
            //var biz = new BLL.ApplicantBiz();
            //var res = biz.GetApplicantByCriteria(regType, compCode, idCard, "", "", "", null, null, "", "", 0, 0, false, "", "", "", "","","","","");
            //gv.DataSource = res.DataResponse;
            //gv.DataBind();
        }

        protected void btnTest1_Click(object sender, EventArgs e)
        {
            GetDataToGrid(DTO.RegistrationType.General, "", "3101400554580");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            GetDataToGrid(DTO.RegistrationType.Insurance, "2016", "");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //สมาคมชีวิต
            //GetDataToGrid(DTO.RegistrationType.Association, "999", "");

            //สมาคมวินาศ
            GetDataToGrid(DTO.RegistrationType.Association, "222", "");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            //DateTime startDate = new DateTime(2013, 3, 1);
            //DateTime toDate = new DateTime(2013, 3, 31);
            //var biz = new BLL.ApplicantBiz();
            //var res = biz.GetApplicantByCriteria(DTO.RegistrationType.OIC, "", "", "", "", "", startDate, toDate, "", "", 0, 0, false, "", "","","");
            //gv.DataSource = res.DataResponse;
            //gv.DataBind();
        }

        protected void btnTest7_Click(object sender, EventArgs e)
        {
            var biz = new BLL.ApplicantBiz();
            var data = biz.GetApplicantInfo("1", "561178", "10003", 0, 0, false);
            gv.DataSource = data.DataResponse.Subjects;
            gv.DataBind();
        }

        protected void btnTestExam1_Click(object sender, EventArgs e)
        {
            var biz = new BLL.ExamResultBiz();
            var res = biz.GetExamResultTempEdit("130423165507881", "0002");
            List<DTO.ExamHeaderResultTemp> lH = new List<DTO.ExamHeaderResultTemp>();
            List<DTO.ExamResultTemp> lD = new List<DTO.ExamResultTemp>();
            lH.Add(res.DataResponse.Header);
            lD.Add(res.DataResponse.Detail);
            gv.DataSource = lH;
            gv.DataBind();
            gv1.DataSource = lD;
            gv1.DataBind();
        }

        protected void btnTestExam2_Click(object sender, EventArgs e)
        {
            var biz = new BLL.ExamResultBiz();
            var res = biz.GetExamResultTempEdit("130423165507881", "0002");
            res.DataResponse.Header.TESTING_DATE = "11/12/2013";
            res.DataResponse.Detail.PRE_NAME_CODE = "3";
            biz.UpdateExamResultEdit(res.DataResponse);

            Response.Write(Resources.errorwebTestUpload_002);
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            //3100100779607
            //BLL.LicenseBiz biz = new BLL.LicenseBiz();
            //var res =  biz.GetPersonalHistoryByIdCard("3100100779607");
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetRequestLicenseType("--เลือก--");
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = res.DataResponse;
            ddl.DataBind();
        }

        protected void btnTest8_Click(object sender, EventArgs e)
        {
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["OraDB_Person"].ToString();
            OracleConnection ora = new OracleConnection(strConn);
            ora.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = ora;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "prdTest";

            OracleParameter v1 = new OracleParameter { OracleDbType = OracleDbType.Decimal, Direction = System.Data.ParameterDirection.Input };
            v1.Value = 1;

            OracleParameter v2 = new OracleParameter { OracleDbType = OracleDbType.Decimal, Direction = System.Data.ParameterDirection.Input };
            v1.Value = 2;

            OracleParameter v3 = new OracleParameter { OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Input };
            v3.Value = "ทดสอบ";

            OracleParameter o1 = new OracleParameter { OracleDbType = OracleDbType.Decimal, Direction = System.Data.ParameterDirection.Output };
            OracleParameter o2 = new OracleParameter { OracleDbType = OracleDbType.Decimal, Direction = System.Data.ParameterDirection.Output };
            OracleParameter o3 = new OracleParameter { OracleDbType = OracleDbType.Varchar2, Direction = System.Data.ParameterDirection.Output };

            cmd.Parameters.Add(v1);
            cmd.Parameters.Add(v2);
            cmd.Parameters.Add(v3);
            cmd.Parameters.Add(o1);
            cmd.Parameters.Add(o2);
            cmd.Parameters.Add(o3);

            cmd.ExecuteNonQuery();

            Response.Write(o1);
            Response.Write(o2);
            Response.Write(o3);

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.NamingContainer;

        }

        public class A
        {
            public bool Value { get; set; }
            public string Id { get; set; }


            public List<A> GetData()
            {
                var list = new List<A>();
                for (int i = 0; i < 1; i++)
                {
                    list.Add(new A
                    {
                        Value = false,
                        Id = i.ToString()
                    });
                }
                return list;

            }
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            string userName = "999999";
            bool isIdCard = Regex.IsMatch(userName, @"^[0-9]*$");

        }
    }
}
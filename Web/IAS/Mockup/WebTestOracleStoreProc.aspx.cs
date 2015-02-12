using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;

namespace IAS.Mockup
{
    public partial class WebTestOracleStoreProc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            OracleConnection conn = new OracleConnection("Data Source=OIC;User Id=ptmdoi;Password=password;");
            if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            conn.Open();
            OracleCommand comm = new OracleCommand();
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "PT_CHK_POL_BARCODE_F";
            
        }

        protected void btnTest2_Click(object sender, EventArgs e)
        {
            var biz = new BLL.DataCenterBiz();
            var res =  biz.GetExamPlaceGroup("เลือก");
            GridView1.DataSource = res.DataResponse;
            GridView1.DataBind();
        }
    }
}
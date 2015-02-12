using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using IAS.Utils;
using System.Data;
using GenCode128;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace IAS.Mockup
{
    public partial class TestReport : System.Web.UI.Page
    {
        private List<Repository.TestBarCodeEntity> ListBarCode
        {
            get
            {
                if (Session["listBarCode"] == null)
                    Session["listBarCode"] = new List<Repository.TestBarCodeEntity>();
                return (List<Repository.TestBarCodeEntity>)Session["listBarCode"];
            }
            set { Session["listBarCode"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            //string weight = "1";
            //string code = "1234567890123";
            //using (Font fnt = new Font("Arial", 16))
            //{
            //    string caption = string.Format("Code128 barcode weight={0}", weight );
            //    g.DrawString(caption, fnt, System.Drawing.Brushes.Black, 50, 50);
            //    caption = string.Format("message='{0}'", code);
            //    g.DrawString(caption, fnt, System.Drawing.Brushes.Black, 50, 75);
            //    g.DrawImage(pictBarcode.Image, 50, 110);
            //}



            string code = txt.Text;

            //DataTable dt = new DataTable();

            //DataColumn dc1 = new DataColumn("BarCodeImage", typeof(System.Byte[]));
            //DataColumn dc2 = new DataColumn("BarCode", typeof(System.String));

            //dt.Columns.Add(dc1);
            //dt.Columns.Add(dc2);

            //DataRow dr;
            //dr = dt.NewRow();
            //dr["BarCodeImage"] = BarCode.GenBarCodeToImage(code);
            //dr["BarCode"] = code;
            //dt.Rows.Add(dr);

            //this.ListBarCode.Add(
            //    new Repository.TestBarCodeEntity
            //    {
            //        BarCode = code,
            //        BarCodeImage = BarCode.GenBarCodeToImage(code)
            //    }
            //);

            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("~/reports/rptPayment.rpt"));
            //rpt.SetDataSource(this.ListBarCode);
            this.CrystalReportViewer1.ReportSource = rpt;
            this.CrystalReportViewer1.DataBind();
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "");

        }

        protected void vIdCard_Click(object sender, EventArgs e)
        {
            lblResult.Text = Utils.IdCard.Verify(txtIdCard.Text).ToString();
        }

        protected void btnEncrypt_Click(object sender, EventArgs e)
        {
            lblEncrypt.Text = Utils.EncryptSHA256.Encrypt(txtPassword.Text);
            lblNumber.Text = lblEncrypt.Text.Length.ToString();
        }


        enum MyTest
        {
            Apple = 1,
            Orange,
            Banana
        }

        protected void btnTestEnum_Click(object sender, EventArgs e)
        {
            MyTest ts = MyTest.Banana;

            string em = Enum.GetName(typeof(MyTest), 2);

            
        }
    }
}
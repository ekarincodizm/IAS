using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{

    class Register
    {
        public DateTime RegisterDate { get; set; }
        public String RegisterCode { get; set; }
        public int Id { get; set; }
    }

    private List<Register> lstRegister;

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Globalization.CultureInfo myCulture = System.Globalization.CultureInfo.CreateSpecificCulture("th-TH");

        System.Threading.Thread.CurrentThread.CurrentCulture = myCulture;
        System.Threading.Thread.CurrentThread.CurrentUICulture = myCulture;

        lstRegister = new List<Register>();
        lstRegister.Add(new Register { RegisterCode = "TR0155", RegisterDate = Convert.ToDateTime("8 มกราคม 2556"), Id = 1 });
        lstRegister.Add(new Register { RegisterCode = "TR0156", RegisterDate = Convert.ToDateTime("8 มกราคม 2556"), Id = 1 });
        lstRegister.Add(new Register { RegisterCode = "TR1422", RegisterDate = Convert.ToDateTime("16 มกราคม 2556"), Id = 2 });
        lstRegister.Add(new Register { RegisterCode = "TR2002", RegisterDate = Convert.ToDateTime("24 มกราคม 2556"), Id = 3 });
        lstRegister.Add(new Register { RegisterCode = "TR2002", RegisterDate = Convert.ToDateTime("14 กุมภาพันธ์ 2556"), Id = 4 });
        lstRegister.Add(new Register { RegisterCode = "TR2005", RegisterDate = Convert.ToDateTime("15 กุมภาพันธ์ 2556"), Id = 5 });

    }

    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        if (lstRegister != null)
        {
            foreach (Register item in lstRegister)
            {
                if (e.Day.Date == item.RegisterDate)
                {

                    string lnkid = string.Empty;

                    e.Cell.Controls.Add(new LiteralControl("</br>"));

                    LinkButton lnkbtn = new LinkButton();
                    lnkbtn.ID = "lnk" + item.RegisterCode;
                    lnkbtn.Text = item.RegisterCode;
                    lnkid = lnkbtn.ID;

                    int registerID = item.Id;
                    string registerDate = item.RegisterDate.ToString("dd MMM yyyy");


                    string script = string.Format(@"openDialog('{0}', '{1}')", registerID, registerDate);

                    lnkbtn.Attributes.Add("onclick", script);
                    e.Cell.Controls.Add(lnkbtn);

                }
            }
        }
    }

    private void ClearValue()
    {
        txtExam.Text = string.Empty;
        txtTimeExam.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        txtTestingLocations.Text = string.Empty;
        txtFee.Text = string.Empty;
        txtNumberOfCandidates.Text = string.Empty;
    }

    protected void Calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
    {
        if (lstRegister != null)
        {
            foreach (Register item in lstRegister)
            {
                if (e.NewDate.Date == item.RegisterDate)
                {
                    Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;</br>"));
                    HyperLink hpl = new HyperLink();
                    hpl.Text = item.RegisterCode;
                    hpl.NavigateUrl = "~/Default.aspx?id=" + item.Id;
                    Controls.Add(hpl);
                }
            }
        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        //string script = string.Format(@"ValidData('{0}')", 1);
        //btnRegister.Attributes.Add("onclick", script);
        ClearValue();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }


}
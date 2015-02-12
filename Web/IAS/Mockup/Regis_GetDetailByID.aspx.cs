using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.DTO;


namespace IAS.Mockup
{
    public partial class Regis_GetDetailByID : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //Main();
                //GetIDList();
            }

        }

        private void GetIDList()
        {
            //txtIDCard.Text = "4759340007081";

            RegistrationBiz biz = new RegistrationBiz();
            ResponseService<DTO.Person> res = biz.GetPersonalDetailByIDCard(txtIDCard.Text);

            if (res.DataResponse != null)
            {
                List<DTO.Person> ls = new List<DTO.Person>
                {
                    res.DataResponse
                };

                gvIDCard.DataSource = ls;
                gvIDCard.DataBind();


                gvAll.DataSource = ls;
                gvAll.DataBind();
            }
            else
            {
                string Alert = "alert('"+ res.ErrorMsg +"')";
                AjaxControlToolkit.ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "alert", Alert, true);


            }
        }

        protected void btnValid_Click(object sender, EventArgs e)
        {
            GetIDList();
        }


        //For Test
        private void Main()
        {
            string[] a = { "a", "b", "c", "d" };
            string[] b = { "d", "e", "f", "g" };

            var UnResult = a.Union(b);
            Console.WriteLine("Union Result");
            foreach (string s in UnResult)
            {
                Console.WriteLine(s);
            }

            var ExResult = a.Except(b);
            Console.WriteLine("Except Result");
            foreach (string s in ExResult)
            {
                Console.WriteLine(s);
            }

            var InResult = a.Intersect(b);
            Console.WriteLine("Intersect Result");
            foreach (string s in InResult)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();

        }
    }
}
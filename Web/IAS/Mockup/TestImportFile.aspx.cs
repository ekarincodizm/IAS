using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace IAS.Mockup
{
    public class csvItem
    {
        public string IDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDay { get; set; }
        public string ExamID { get; set; }
    }


    public partial class TestImportFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            var reader = new StreamReader(File.OpenRead(@"D:\Test_03.csv"), Encoding.UTF8);
            
            //List<string> listA = new List<string>();
            //List<string> listB = new List<string>();

            var csvLisItems = new List<csvItem>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                string lineString = line.ToString().Replace("\"",string.Empty);
                var values = lineString.Split(',');

             
                csvLisItems.Add(new csvItem
                {
                    IDNumber = values[0],
                    FirstName = values[1],
                    LastName = values[2],
                    BirthDay = values[3],
                    ExamID = values[4]
                });


            }

            gvList.DataSource = csvLisItems;
            gvList.DataBind();
        }
    }
}
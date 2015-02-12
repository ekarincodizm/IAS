using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.DirectoryServices;

namespace IAS.Mockup
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //private void GetAD()
        //{

        //    ADUtil ad = new ADUtil("LDAP://ar.co.th", "phornnarongl", "gsoj'riIi'8N6");

        //    ////DirectoryEntry dir = new DirectoryEntry("LDAP://oic.or.th", "artest","artest123"); //45678");
        //    //DirectoryEntry dir = new DirectoryEntry("LDAP://ar.co.th", "phornnarongl", ""); //45678");
        //    ////dir.Path = "LDAP://192.168.110.91";
        //    //DirectorySearcher sea = new DirectorySearcher(dir);
        //    ////sea.Filter = "(sAMAccountName=kositc)"; // +txtUsername.Text + ")";
        //    ////sea.Filter = "(useraccountcontrol=66048)";
        //    //sea.Filter = "(sAMAccountName=phornnarongl)"; // +txtUsername.Text + ")";
        //    //SearchResult seares = sea.FindOne();



        //    //DirectoryEntry dsresult = sresult.GetDirectoryEntry();


        //    //sea.PropertiesToLoad.Add("givenName");
        //    //sea.PropertiesToLoad.Add("sn");
        //    //sea.PropertiesToLoad.Add("mail");
        //    //SearchResult result = sea.FindOne();

        //    var result = ad.searchResult;

        //    if (null == result)
        //    {
        //        return; // "ไม่พบข้อมูลในระบบ";
        //    }

        //    if (null != result.Properties["givenName"][0])
        //    {
        //        lblName.Text = (String)result.Properties["givenName"][0];
        //    }
        //    if (null != result.Properties["sn"][0])
        //    {
        //        lblLastName.Text = (String)result.Properties["sn"][0];
        //    }
        //    try
        //    {
        //        if (null != result.Properties["mail"][0])
        //        {
        //            lblMail.Text = (String)result.Properties["mail"][0];
        //        }
        //    }
        //    catch { }

            
        //}

        protected void btn_Click(object sender, EventArgs e)
        {
            //GetAD();

            //IAS.BLL.PersonBiz biz = new BLL.PersonBiz();
            //biz.IsRightUserOIC("phornnarongl");

        }


    }
}
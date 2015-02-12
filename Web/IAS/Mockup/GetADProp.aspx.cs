using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.DirectoryServices;
using System.Collections;
using IAS.BLL;
using IAS.Common.Logging;

namespace IAS.Mockup
{
    public partial class GetADProp : System.Web.UI.Page
    {

        private Dictionary<string, string> ADProperties = new Dictionary<string, string>();

        private DirectoryEntry directory { get; set; }
        private DirectorySearcher dirSearcher { get; set; }
        public SearchResult searchResult { get; set; }

        //private string ADuserPath = "LDAP://192.168.110.91";
        //private string ADuserName = "ar01";
        //private string ADuserPass = "ar01@1234";
        private string ADuserPath = "LDAP://192.168.110.91";
        private string ADuserName = "it-prapatu";
        private string ADuserPass = "abcd12345";
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //this.ADUtil(ADuserPath, ADuserName, ADuserPass);
                //this.SetFilter(ADuserName);
                //this.GetADProperties(this.searchResult);

                //Dictionary<string, string> ls = ADProperties;

                //gvADProperties.DataSource = ls;
                //gvADProperties.DataBind();
            }
        }

        public void ADUtil(string adPath, string userName, string passWord)
        {
            this.directory = new DirectoryEntry(adPath,userName,passWord);
            this.dirSearcher = new DirectorySearcher(this.directory);
            SetFilter(userName);
        }

        public void SetFilter(string userName)
        {
            this.dirSearcher.Filter = "(sAMAccountName=" + userName + ")";
            this.searchResult = dirSearcher.FindOne();
        }

        public Dictionary<string,string> FindUserByProperties(string[] param)
        {
            var dic = new Dictionary<string,string>();
            for(int i=0; i<param.Length; i++)
            {
                dic.Add(param[i],"");
                this.dirSearcher.PropertiesToLoad.Add(param[i]);
            }
            SearchResult result = this.dirSearcher.FindOne();

            if(result!=null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    var property =  result.Properties[param[i]];
                    dic[param[i]] =  property.Count > 0 ? property[0].ToString() : string.Empty;
                }
            }

            return dic;
        }

        private void GetADProperties(System.DirectoryServices.SearchResult result)
        {

            if (result.Properties.Count > 0)
            {
                //System.Collections.ICollection nameList = this.searchResult.Properties.PropertyNames;
                //System.Collections.ICollection valueList = this.searchResult.Properties.Values;

                foreach (System.Collections.DictionaryEntry item in this.searchResult.Properties)
                {
                    //var itemKey = item.Key;
                    //var itemValue = item.Value;
                    System.Collections.ReadOnlyCollectionBase valueList = ((System.Collections.ReadOnlyCollectionBase)(item.Value));
                    foreach (var valuekey in valueList)
                    {
                        //Assign prop to List<string, string>
                        if( ADProperties.Where(x => x.Key == item.Key).Count() == 0)
                        {
                            ADProperties.Add(Convert.ToString(item.Key), Convert.ToString(valuekey));
                        }
                        
                    }

                }

            }
        }

        protected void btnAuth_Click(object sender, EventArgs e)
        {
            //string LoginName = "it-prapatu";
            //string PassWord = "abcd12345";
            //List<DTO.OICADProperties> lsProp = new List<DTO.OICADProperties>();

            //IAS.BLL.OICActiveDirectoryBiz biz = new IAS.BLL.OICActiveDirectoryBiz();
            //IAS.BLL.ADService.LoginResult res = biz.OICAuthentication(ADuserName, ADuserPass);
            //if ((res.Result != null) && (res.Result.Equals("SUCCESS")))
            //{
            //    DTO.OICADProperties ent = new DTO.OICADProperties();
            //    ent.DepartmentCode = res.DepartmentCode;
            //    ent.DepartmentName = res.DepartmentName;
            //    ent.EmployeeCode = res.EmployeeCode;
            //    ent.EmployeeName = res.EmployeeName;
            //    ent.PositionCode = res.PositionCode;
            //    ent.PositionName = res.PositionName;
            //    lsProp.Add(ent);

            //    lblResult.Text = res.Result;
            //    lblResult.Font.Bold = true;
            //    lblResult.ForeColor = System.Drawing.Color.BlueViolet;

            //    gvADProperties.DataSource = lsProp;
            //    gvADProperties.DataBind();
            //    udpMain.Update();
            //}
            //else
            //{
            //    lblResult.Text = res.Result;
            //    lblResult.Font.Bold = true;
            //    lblResult.ForeColor = System.Drawing.Color.Red;
            //}

            PersonBiz biz = new PersonBiz();
            List<DTO.OICADProperties> lsProp = new List<DTO.OICADProperties>();

            DTO.ResponseService<DTO.OICADProperties> res = biz.OICAuthenWithADService(ADuserName, ADuserPass);

            if ((res.DataResponse.Result != null) && (res.DataResponse.Result.Equals("SUCCESS")))
            {
                DTO.OICADProperties ent = new DTO.OICADProperties();
                ent.DepartmentCode = res.DataResponse.DepartmentCode;
                ent.DepartmentName = res.DataResponse.DepartmentName;
                ent.EmployeeCode = res.DataResponse.EmployeeCode;
                ent.EmployeeName = res.DataResponse.EmployeeName;
                ent.PositionCode = res.DataResponse.PositionCode;
                ent.PositionName = res.DataResponse.PositionName;
                lsProp.Add(ent);

                lblResult.Text = res.DataResponse.Result;
                lblResult.Font.Bold = true;
                lblResult.ForeColor = System.Drawing.Color.BlueViolet;

                gvADProperties.DataSource = lsProp;
                gvADProperties.DataBind();
                udpMain.Update();
            }
            else
            {
                lblResult.Text = res.DataResponse.Result;
                lblResult.Font.Bold = true;
                lblResult.ForeColor = System.Drawing.Color.Red;
            }


        }

        protected void btnAuthByNewService_Click(object sender, EventArgs e)
        {
            PersonBiz biz = new PersonBiz();
            List<DTO.OICADProperties> lsProp = new List<DTO.OICADProperties>();

            //ตรวยสอบ Key APP_FOR_OIC ใน Web.config ถ้าไม่มีให้ Default = No
            //string appForOIC = System.Configuration.ConfigurationManager.AppSettings["APP_FOR_OIC"] == null
            //                        ? "Yes"
            //                        : System.Configuration.ConfigurationManager.AppSettings["APP_FOR_OIC"].ToUpper();

            DTO.ResponseService<DTO.UserProfile> res = biz.UserAuthen(txtUserName.Text.Trim(), txtPassword.Text, true, HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);

            if (res.DataResponse != null)
            {
                Session[PageList.UserProfile] = res.DataResponse;
                Session["LoginUser"] = res.DataResponse.LoginName;
                SetSessionLogin(res.DataResponse);
                LoggerFactory.CreateLog().LogInfo(String.Format("[{0}] User Login: {1} ", HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], res.DataResponse.LoginName));

                lblResult.Text = "SUCCESS";
                lblResult.Font.Bold = true;
                lblResult.ForeColor = System.Drawing.Color.BlueViolet;

                gvADProperties.DataSource = res.DataResponse.CanAccessSystem;
                gvADProperties.DataBind();
                udpMain.Update();
            }
            else
            {
                lblResult.Text = "FAIL";
                lblResult.Font.Bold = true;
                lblResult.ForeColor = System.Drawing.Color.Red;

                gvADProperties.DataSource = null;
                gvADProperties.DataBind();
                udpMain.Update();
            }
        }


        private void SetSessionLogin(DTO.UserProfile profile)
        {
            HttpContext.Current.Session["LogingName"] = profile.LoginName;
            HttpContext.Current.Session["OICUserId"] = profile.OIC_User_Id;
            HttpContext.Current.Session["DeptCode"] = profile.DeptCode;
            HttpContext.Current.Session["CompanyCode"] = profile.CompCode;
            HttpContext.Current.Session["CreatedBy"] = profile.Id;
        }

    }

}
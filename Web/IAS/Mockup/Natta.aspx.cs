using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

using IAS.Utils;
using System.Text.RegularExpressions;
using Oracle.DataAccess.Client;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Threading;

using IAS.DTO;
using IAS.BLL;
using CrystalDecisions.CrystalReports.Engine;
using IAS.MasterPage;
using IAS.Class;
using CrystalDecisions.Shared;
using IAS.Properties;

namespace IAS.Mockup
{
    public partial class Natta : basepage
    {
        private string IdCard = "1111111111119";
        private string id = "130923134423693";
        string ReportFolder = "../Reports/";
        string outPutFile = "Agreement.pdf";
        //string ReportFolder = @"D:\[-- AR Soft --]\[-- Project --]\IAS_PROJECT10\Web\IAS\Reports\";

        //public MasterLicense MasterLicense
        //{
        //    get
        //    {
        //        return (this.Page.Master as MasterLicense);
        //    }

        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load();
            }
        }

        private void BindReport()
        {
            LicenseBiz biz = new LicenseBiz();
            PersonBiz perBiz = new PersonBiz();
            ReportDocument rpt = new ReportDocument();
            CrystalReport1 cr = new CrystalReport1();
            var res = perBiz.GetById(id);

            
            string ReportFolder = @"D:\[-- AR Soft --]\[-- Project --]\IAS_PROJECT10\Web\IAS\Reports\";
            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_1.rpt"));
            //rpt.SetDataSource(res.DataResponse);

            cr.Load(ReportFolder + "RptAgreement_1.rpt");
            cr.SetDataSource(new [] {res});
            CrystalReportViewer.ReportSource = cr;
            
        }

        private void Load()
        {

            PersonBiz biz = new PersonBiz();
            var res = biz.GetById(id);

            var ls = new List<PersonLicenseAgreement>();

            if ((!res.IsError) && (res.DataResponse != null))
            {
                PersonLicenseAgreement ent = new PersonLicenseAgreement();
                ent.SEX = res.DataResponse.SEX;
                ent.NAMES = res.DataResponse.NAMES;
                ent.LASTNAME = res.DataResponse.LASTNAME;
                ent.MEMBER_TYPE = res.DataResponse.MEMBER_TYPE;
                ls.Add(ent);

            }

            string path = base.AgreementFilePath_Key;
            ReportDocument rpt = new ReportDocument();
            rpt.Load(Server.MapPath("~/Reports/" + "RptAgreement_1.rpt"));
            rpt.SetDataSource(ls);
            //rpt.SetDataSource(new[] { res });
            BindReport(rpt);
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(path + outPutFile));

            BindReport(rpt);
            //rpt.Refresh();
        }

        private void BindReport(ReportDocument rpt)
        {
            this.CrystalReportViewer.ReportSource = rpt;
            //this.LicenseCrystalReportViewer.DataBind();
        }
        private void Gen()
        {
            Func<string, string> ConverCodeToString = delegate(string code)
            {
                if (code.Length.Equals(1))
                {
                    string x = code.Replace(code, "0" + code);
                    code = x;
                }
                return code;

            };

            Func<string, string> ConvertLicense = delegate(string license)
            {

                string newlicense = ConverCodeToString(license);

                string type1 = ConverCodeToString(Convert.ToString(DTO.LicenseType.Type01.GetEnumValue()));
                string type2 = ConverCodeToString(Convert.ToString(DTO.LicenseType.Type02.GetEnumValueString()));

                if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License001;
                }
                else if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License002;
                }
                else if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License003;
                }
                else if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License004;
                }
                else if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License005;
                }
                else if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License006;
                }
                else if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License007;
                }
                else if (license.Equals(newlicense))
                {

                    license = Resources.propNatta_License008;
                }
                else
                {
                    license = Resources.propNatta_License009;
                }
                return license;
            };

            Func<string, string> ConvertPettion = delegate(string pettion)
            {
                string newpettion = ConverCodeToString(pettion);

                string type1 = ConverCodeToString(Convert.ToString(DTO.PettionCode.NewLicense.GetEnumValue()));
                string type2 = ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense1Y.GetEnumValue()));

                if (pettion.Equals(newpettion))
                {
                    pettion = Resources.propNatta_Pettion001;
                }
                else if (pettion.Equals(newpettion))
                {
                    pettion = Resources.propNatta_Pettion002;
                }
                else if (pettion.Equals(newpettion))
                {
                    pettion = Resources.propNatta_Pettion003;
                }
                else if (pettion.Equals(newpettion))
                {
                    pettion = Resources.propNatta_Pettion004;
                }
                else
                {
                    pettion = Resources.propNatta_License009;
                }
                return pettion;
            };

            string license1 = ConvertLicense("4");
            string license2 = ConvertLicense("01");
            string license3 = ConvertLicense("13");


            string pettion1 = ConvertPettion("2");
            string pettion2 = ConvertPettion("01");
            string pettion3 = ConvertPettion("11");
        }


    }
}
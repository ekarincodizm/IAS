using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Utils;
using System.Data;

namespace IAS.BLL
{
    public class RegisterLicenseBiz
    {
        DataCenterService.DataCenterServiceClient svcCenter;
        RegistrationService.RegistrationServiceClient svcRegis;
        public RegisterLicenseBiz()
        {
            svcCenter = new DataCenterService.DataCenterServiceClient();
            svcRegis = new RegistrationService.RegistrationServiceClient();
        }

        public List<DTO.DataItem> GetLicenseType()
        {
            var list_table = svcCenter.GetLicenseType("").DataResponse;
            List<DTO.DataItem> list = new List<DTO.DataItem>();
            list.Add(new DTO.DataItem { Id = "", Name = "ทั้งหมด" });
            foreach (var item in list_table)
            {
                if (item.Id.ToInt() <= 8)
                    list.Add(item);
            }
            list.Add(new DTO.DataItem { Id="09",Name="รวมประเภทตัวแทนประกันชีวิต " });
            list.Add(new DTO.DataItem { Id = "10", Name = "รวมประเภทตัวแทนประกันวินาศภัย " });
            return list;
        }

        public DTO.DataItem[] GetCompany()
        {
            return svcCenter.GetCompanyCode("ทั้งหมด").DataResponse;
            //return null;
        }

        public DataSet ReportRegisterLicense(string licensetype, string comcode, string startdate, string enddate)
        {
            DataSet tb = svcRegis.ReportRegisterLicense(licensetype, comcode, startdate, enddate).DataResponse;
            int count = tb.Tables[0].Rows.Count;
            return tb;
        }

        public DTO.ResponseService<DataSet> ReportRegisterLicenseStaticRatio(string lincensetype, string startdateone, string enddateone, string startdatetwo, string enddatetwo)
        {
            return svcRegis.ReportRegisterLicenseStaticRatio(lincensetype, startdateone, enddateone, startdatetwo, enddatetwo);
        }
    }
}

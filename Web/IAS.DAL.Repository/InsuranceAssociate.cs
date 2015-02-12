using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class InsuranceAssociate
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] LicenseTypeCode { get; set; }

        //Associate
        public string EXAM_PLACE_GROUP_CODE { get; set; }
        public string EXAM_PLACE_GROUP_NAME { get; set; }

        public List<InsuranceAssociate> GetList()
        {
            var list = new List<InsuranceAssociate>();

            list.Add(new InsuranceAssociate { Id = "098", Name = "สมาคมประกันวินาศภัย", LicenseTypeCode = new string[] { "02", "04" } });
            list.Add(new InsuranceAssociate { Id = "222", Name = "สถาบันประกันภัยไทย", LicenseTypeCode = new string[] { "02", "04" } });
            list.Add(new InsuranceAssociate { Id = "333", Name = "สมาคมประกันวินาศภัย (ตจว.)", LicenseTypeCode = new string[] { "02", "04" } });
            list.Add(new InsuranceAssociate { Id = "999", Name = "สมาคมประกันชีวิตไทย", LicenseTypeCode = new string[] { "01", "03" } });
            return list;
        }
    }
}

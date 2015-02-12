using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Class
{
    public class RptLicenseStatisticsService
    {
        public string LicenseType { get; set; }
        public double CountLicense1 { get; set; }
        public double Share1 { get; set; }
        public double CountLicense2 { get; set; }
        public double Share2 { get; set; }
        public double CompareLicense { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;
using System.Configuration;
using IAS.Utils;
using IAS.DataServices.Properties;

namespace IAS.DataServices.License.LicenseHelpers
{
    public class DataLicenseRequestReaderHelper
    {
        public static DTO.ResponseService<DTO.UploadData> ReadDataFromFile(String filename)
        {
            DTO.ResponseService<DTO.UploadData> res = new DTO.ResponseService<DTO.UploadData>();

            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];


            using (NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive))
            {
                FileInfo fileLicense = new FileInfo(Path.Combine(_netDrive, filename));

                if (!fileLicense.Exists)
                {
                    res.ErrorMsg = Resources.errorDataLicenseRequestReaderHelper_001;
                    return res;
                }

                //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

                DTO.UploadData data = new DTO.UploadData
                {
                    Body = new List<string>()
                };


                FileStream filestream = new FileStream(fileLicense.FullName, FileMode.Open);
                using (StreamReader sr =
                       new StreamReader(filestream,
                                           System.Text.Encoding.GetEncoding("TIS-620")))
                {
                    string line = sr.ReadLine();


                    if (line != null && line.Length > 0)
                    {
                        if (line.Substring(0, 1) == "H")
                        {

                            data.Header = line;
                        }
                        // else
                        // {
                        //res.ErrorMsg = Resources.errorDataLicenseRequestReaderHelper_002;
                        //return res;
                        //}
                        // }
                        // else
                        // {
                        //  res.ErrorMsg = Resources.errorExamService_046;
                        // return res;

                    }

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != null && line.Length > 0 && !String.IsNullOrEmpty(line))
                        {


                            if (line.Substring(0, 1) == "H")
                            {

                                data.Header = line;
                            }



                            else if (line.Trim().Length > 0 && line.Substring(0, 11) != "ลำดับข้อมูล" && !line.Substring(0, 11).StartsWith("ลำดับ"))
                            {
                                data.Body.Add(line.Trim());
                            }
                        }
                    }

                    if (data.Body.Count == 0)
                    {
                        res.ErrorMsg = Resources.errorDataLicenseRequestReaderHelper_003;
                        return res;
                    }
                }

                res.DataResponse = data;
            }



            return res;
        }
    }
}
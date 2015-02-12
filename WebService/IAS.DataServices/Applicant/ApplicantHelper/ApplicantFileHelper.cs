using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class ApplicantFileHelper
    {
        public static DTO.UploadData ReadFileUpload(String rawData)
        {


            DTO.UploadData data = new DTO.UploadData
            {
                Body = new List<string>()
            };
            //Stream rawData = FileUpload1.PostedFile.InputStream;
            using (StreamReader sr = new StreamReader(rawData, System.Text.Encoding.GetEncoding("TIS-620")))
            {
                string line = sr.ReadLine().Trim();
                if (line != null && line.Length > 0)
                {
                    if (line.Substring(0, 1) == "H")
                    {
                        data.Header = line;
                    }
                    else
                    {
                        throw new ApplicationException(Resources.errorApplicantFileHelper_001);
                    }

                }
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Trim().Length > 0)
                    {
                        data.Body.Add(line.Trim());
                    }
                }
            }

            return data;
        }
    }
}
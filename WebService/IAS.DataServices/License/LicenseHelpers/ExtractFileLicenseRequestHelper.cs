using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using IAS.DataServices;
using System.IO;
using IAS.DTO;
using IAS.DataServices.Properties;

namespace IAS.DataServices.License.LicenseHelpers
{
    public class ExtractFileLicenseRequestHelper
    {
        public static DTO.ResponseService<DTO.CompressFileDetail> ExtractFile(String compressFile)
        {
            DTO.ResponseService<DTO.CompressFileDetail> res = new DTO.ResponseService<DTO.CompressFileDetail>();
            res.DataResponse = new CompressFileDetail();
            res.DataResponse.AttatchFiles =  new List<DTO.AttachFileDetail>();

            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];


            using (NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive))
            {
                

                //******************** ******* Check FileExist************************************/
                FileInfo compressFileInfo = new FileInfo(Path.Combine(_netDrive, compressFile));

                if (!compressFileInfo.Exists)
                {
                    res.ErrorMsg = Resources.errorExtractFileLicenseRequestHelper_001;
                    return res;
                }

                //ถ้าไม่ใช่ไฟล์ .ZIP หรือ .RAR 
                if (!".ZIP_.RAR".Contains(compressFileInfo.Extension.ToUpper()))
                {
                    res.ErrorMsg = Resources.errorExtractFileLicenseRequestHelper_002;
                    return res;
                }
                //****************************************************************/


                String _targetFullPath = Path.GetDirectoryName(compressFileInfo.FullName);

                DirectoryInfo targetDirectory = new DirectoryInfo(_targetFullPath);
                if (!targetDirectory.Exists)
                {
                    targetDirectory.Create();
                }

                Utils.CompressFile cf = new Utils.CompressFile();

                bool result = false;
                var fileInRAR_Zip = new List<string>();

                if (compressFileInfo.Extension.ToUpper() == ".ZIP")
                {
                    fileInRAR_Zip = cf.GetFilesInZip(compressFileInfo.FullName);
                    result = cf.ZipExtract(compressFileInfo.FullName, targetDirectory.FullName);
                }
                else if (compressFileInfo.Extension.ToUpper() == ".RAR")
                {
                    fileInRAR_Zip = cf.GetFilesInRar(compressFileInfo.FullName);
                    result = cf.RarExtract(compressFileInfo.FullName, targetDirectory.FullName);
                }

                //ถ้าผลการ Extract File เกิดข้อผิดพลาด
                if (!result)
                {
                    res.ErrorMsg = Resources.errorExtractFileLicenseRequestHelper_003;
                    return res;
                }


                if (fileInRAR_Zip.Count > 0)
                {
          
                    long count = 0;
                    for (int i = 0; i < fileInRAR_Zip.Count; i++)
                    {
                        string file = fileInRAR_Zip[i].Replace(@"/", @"\");

                        FileInfo fInfo = new FileInfo(Path.GetFullPath(file));

                        if (fInfo.Extension.ToUpper() == ".CSV")
                        {
                            count += 1;
                        }

                    }

                    if (count > 1)
                    {

                        res.ErrorMsg = Resources.errorExamService_046;
                        return res;
                    
                    }

                    //วนเก็บรายการใน Zip File
                    for (int i = 0; i < fileInRAR_Zip.Count; i++)
                    {
                        //เก็บรายการ Path จริงแปะเข้าไฟล์
                        string file = fileInRAR_Zip[i].Replace(@"/", @"\");

                        //string fullFilePath = tempFolder + @"\" + file;

                        FileInfo fInfo = new FileInfo(Path.GetFullPath(file));
                        if (fInfo.Extension.ToUpper() == ".CSV")
                        {
                            res.DataResponse.TextFilePath = Path.Combine(_targetFullPath, fInfo.Name).Replace(_netDrive, "");
                            res.DataResponse.ExtFile = fInfo.Extension.ToUpper();
                        }else {
                            string[] ary = fInfo.Name.Split('.');
                            string fileExt = fInfo.Extension;
                            string fName = ary.Length > 0 ? ary[0] : string.Empty;

                            if (!string.IsNullOrEmpty(fName))
                            {
                                res.DataResponse.AttatchFiles.Add(new DTO.AttachFileDetail
                                {
                                    FileName = fName,
                                    Extension = fInfo.Extension,
                                    FullFileName = Path.Combine(_targetFullPath, fInfo.Name),
                                    MapFileName = Path.Combine(_targetFullPath, fInfo.Name).Replace(_netDrive, "") ,
                                    FileTypeCode = fName.Substring(fName.Length-2)
                                });
                            }
                        }
                       
                    }

                }

            }

            return res;
        }
    }
}
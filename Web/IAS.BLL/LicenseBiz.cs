using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading;
using IAS.Utils;
using Ionic.Zip;
using System.Configuration;
using IAS.DTO.FileService;
using IAS.BLL.Properties;


namespace IAS.BLL
{
    public class LicenseBiz : IDisposable
    {

        private LicenseService.LicenseServiceClient svc;
        private static String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString();
        private static String TempFileContainer = ConfigurationManager.AppSettings["FS_TEMP"].ToString();
        public LicenseBiz()
        {
            svc = new LicenseService.LicenseServiceClient();
        }

        /// <summary>
        /// แสดงข้อมูลใบอนุญาตตามเงื่อนไข
        /// </summary>
        /// <param name="licenseNo">เลขที่ใบอนุญาต</param>
        /// <param name="licenseType">ประเภทใบอนุญาต</param>
        /// <param name="startDate">วันที่เริ่มเพื่อหา (วันที่รับใบอนุญาต)</param>
        /// <param name="toDate">วันที่สิ้นสุดเพื่อหา (วันที่รับใบอนุญาต)</param>
        /// <param name="paymentNo">เลขที่ใบสั่งจ่าย</param>
        /// <param name="licenseTypeReceive">ประเภทขอรับใบอนุญา</param>
        /// <param name="pageNo">หน้าที่</param>
        /// <param name="recordPerPage">รายการแสดงต่อหน้า</param>
        /// <returns>DataSet</returns>
        public DTO.ResponseService<DataSet>
            GetLicenseByCriteria(string licenseNo, string licenseType,
                                 DateTime? startDate, DateTime? toDate,
                                 string paymentNo, string licenseTypeReceive,
                                 DTO.UserProfile userProfile,
                                 int pageNo, int recordPerPage)
        {
            return svc.GetLicenseByCriteria(licenseNo, licenseType,
                                 startDate, toDate,
                                 paymentNo, licenseTypeReceive,
                                 userProfile,
                                 pageNo, recordPerPage);
        }

        /// <summary>
        /// ดึงประวัติด้วยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<PersonHistory></returns>
        public DTO.ResponseService<DTO.PersonalHistory> GetPersonalHistoryByIdCard(string idCard)
        {
            return svc.GetPersonalHistoryByIdCard(idCard);
        }


        /// <summary>
        /// ดึงข้อมูลประวัติการสอบ
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetExamHistoryByIdCard(string idCard)
        {
            return svc.GetExamHistoryByIdCard(idCard);
        }


        /// <summary>
        /// ดึงข้อมูลประวัติการอบรมด้วยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetTrainingHistoryBy(string idCard)
        {
            return svc.GetTrainingHistoryBy(idCard);
        }



        /// <summary>
        /// ดึงข้อมูลประวัติการขอรับใบอนุญาต
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetObtainLicenseByIdCard(string idCard)
        {
            return svc.GetObtainLicenseByIdCard(idCard);

        }







        /// <summary>
        /// ดึงข้อมูลการอบรม 1-4 โดยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetTrain_1_To_4_ByIdCard(string idCard)
        {
            return svc.GetTrain_1_To_4_ByIdCard(idCard);
        }


        /// <summary>
        /// ดึงข้อมูล Unit Link โดยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>DTO.ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetUnitLinkByIdCard(string idCard)
        {
            return svc.GetUnitLinkByIdCard(idCard);
        }

        /// <summary>
        /// ดึงข้อมูลขอรับใบอนุญาต โดยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetRequestLicenseByIdCard(string idCard)
        {
            return svc.GetRequestLicenseByIdCard(idCard);
        }

        /// <summary>
        /// ดึงข้อมูลการขอรับใบอนุญาตตามเงื่อนไข
        /// </summary>
        /// <param name="licenseNo">เลขที่ใบอนุญาต</param>
        /// <param name="licenseTypeCode">รหัสประเภทใบอนุญาต</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetReceiveLicenseByCriteria(string licenseNo, string licenseTypeCode,
                                        DateTime? startDate, DateTime? toDate)
        {
            return svc.GetReceiveLicenseByCriteria(licenseNo, licenseTypeCode, startDate, toDate);
        }

        //วนเก็บรายการไฟล์ใน Compress Files
        //public List<DTO.AttatchFileDetail> ExtractFile(string autoId, string compressFile,
        //                                               string tempPath, string mapTempPath)
        //{
        //    var list = new List<DTO.AttatchFileDetail>();
        //    FileInfo fi = new FileInfo(compressFile);
        //    string ext = fi.Extension.ToUpper();

        //    //ถ้าไม่ใช่ไฟล์ .ZIP หรือ .RAR 
        //    if (!".ZIP_.RAR".Contains(ext))
        //    {
        //        return list;
        //    }

        //    string tempFolder = tempPath + autoId;
        //    //Directory.CreateDirectory(tempFolder);

        //    Utils.CompressFile cf = new Utils.CompressFile();
        //    bool result = false;
        //    var fileInRAR_Zip = new List<string>();

        //    if (ext == ".ZIP")
        //    {
        //        fileInRAR_Zip = cf.GetFilesInZip(compressFile);
        //        result = cf.ZipExtract(compressFile, tempFolder);
        //    }
        //    else if (ext == ".RAR")
        //    {
        //        fileInRAR_Zip = cf.GetFilesInRar(compressFile);
        //        result = cf.RarExtract(compressFile, tempFolder);
        //    }

        //    //ถ้าผลการ Extract File เกิดข้อผิดพลาด
        //    if (!result)
        //    {
        //        return list;
        //    }

        //    //วนเก็บรายการใน Zip File
        //    for (int i = 0; i < fileInRAR_Zip.Count; i++)
        //    {
        //        //เก็บรายการ Path จริงแปะเข้าไฟล์
        //        string file = fileInRAR_Zip[i].Replace(@"/", @"\");

        //        string fullFilePath = tempFolder + @"\" + file;

        //        FileInfo fInfo = new FileInfo(fullFilePath);

        //        string[] ary = fInfo.Name.Split('.');
        //        string fileExt = fInfo.Extension;
        //        string fName = ary.Length > 0 ? ary[0] : string.Empty;

        //        if (!string.IsNullOrEmpty(fName))
        //        {
        //            list.Add(new DTO.AttatchFileDetail
        //            {
        //                FileName = fName,
        //                Extension = fileExt,
        //                FullFileName = fullFilePath,
        //                MapFileName = mapTempPath + autoId + @"/" + file.Replace(@"\", @"/")
        //            });
        //        }
        //    }

        //    return list;
        //}

        /// <summary>
        /// Gen เลขที่อัตโนมัติ
        /// </summary>
        /// <returns>string</returns>
        public string GetAutoId()
        {
            return IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
        }

        /// <summary>
        /// สำหรับ Upload การขอรับใบอนุญาตแบบกลุ่ม
        /// </summary>
        /// <param name="fileName">ชื่อไฟล์รวม Path</param>
        /// <param name="rawData">ข้อมูลที่ Upload (Stream)</param>
        /// <param name="testingNo">เลขที่สอบ</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        //public DTO.ResponseService<DTO.UploadLicenseResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail, DTO.AttachFileDetail>>
        //UploadData(string pathFileName, DTO.UserProfile userProfile, string petitionTypeCode, string licenseTypeCode, Stream fileSteam)
        //{
        public DTO.ResponseService<DTO.SummaryReceiveLicense>
        UploadData(string pathFileName, DTO.UserProfile userProfile, string petitionTypeCode, string licenseTypeCode, Stream fileSteam, string replaceType, string approveCom)
        {
            var res = new DTO.ResponseService<DTO.SummaryReceiveLicense>();

            res.DataResponse = new DTO.SummaryReceiveLicense();
            UploadFileResponse response = new UploadFileResponse();
            try
            {
                //String attachFilePath = "";
                using (FileService.FileTransferServiceClient svcFile = new FileService.FileTransferServiceClient())
                {

                    String fileName = String.Format(@"{0}.{1}", IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(), pathFileName.Split('.')[pathFileName.Split('.').Length - 1]); ;
                    String container = String.Format(@"{0}\{1}", TempFileContainer, IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId());

                    response = svcFile.UploadFile(new UploadFileRequest()
                    {
                        TargetContainer = container,
                        TargetFileName = fileName,
                        FileStream = fileSteam
                    });


                    if (response.Code != "0000")
                    {
                        res.ErrorMsg = response.Message;
                        return res;
                    }
                    //FileService.UploadFileResponse resUpload = svcFile.UploadFile(new UploadFileRequest()
                    //{
                    //    TargetContainer = container,
                    //    TargetFileName = fileName,
                    //    FileStream = fileSteam
                    //});

                    //if (resUpload.Code != "0000")
                    //{
                    //    res.ErrorMsg = resUpload.Message;
                    //    return res;
                    //}
                    //pathFileName = resUpload.TargetFullName;
                    pathFileName = response.TargetFullName;
                }

                DTO.DataLicenseRequest request = new DTO.DataLicenseRequest()
                {
                    FileName = pathFileName,
                    LicenseTypeCode = licenseTypeCode,
                    PettitionTypeCode = petitionTypeCode,
                    UserProfile = userProfile,
                    ReplaceType = replaceType,
                    ApproveCom = approveCom
                };


                res = svc.UploadDataLicense(request);


            }
            catch (IOException ioEx)
            {
                res.ErrorMsg = Resources.errorLicenseBiz_001;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorLicenseBiz_001;
            }
            return res;
        }


        /// <summary>
        /// ดึงรายการข้อมูลการขอใบอนุญาต
        /// </summary>
        /// <param name="uploadGroupNo">เลขที่กลุ่ม</param>
        /// <returns>ResponseService<LicenseVerifyImgDetail></returns>
        public DTO.ResponseService<IAS.DTO.AttatchFileLicense[]>
            GetLicenseVerifyImgDetail(string uploadGroupNo, string idCard, string CountPage, Int32 pageIndex, Int32 pageSize)
        {
            return svc.GetLicenseVerifyImgDetail(uploadGroupNo, idCard, CountPage, pageIndex, pageSize);
        }

        private void ValidateAttatchFiles(DTO.ReceiveLicenseDetail detail,
                                         List<DTO.AttachFileDetail> attachFiles)
        {
            try
            {
                int iCount = attachFiles.Where(w => w.FileName.Split('_')[0] == detail.CITIZEN_ID).Count(); //.ID_CARD_NO).Count();
                if (iCount == 0)
                {
                    //detail.ERR_DESC += "\nไม่พบเอกสารแนบ";
                }
            }
            catch (Exception ex)
            {
                //detail.ERR_DESC += ex.Message;
            }
        }

        public DTO.ResponseMessage<bool> SubmitReceiveLicenseGroupUpload(string groupId,
                                                    List<DTO.AttachFileDetail> attachFiles,
                                                    string targetAttatchFilePath, string userId)
        {
            var res = new DTO.ResponseMessage<bool>();


            try
            {
                if (!res.IsError)
                {
                    List<DTO.AttatchFileLicense> attchList = new List<DTO.AttatchFileLicense>();
                    DTO.ResponseService<DTO.AttatchFileLicense[]> resmove = svc.MoveExtachFile(userId, groupId, attachFiles.ToArray());
                    //res = svc.SubmitSingleOrGroupReceiveLicense(groupId, attchList.ToArray());
                    if (resmove.IsError)
                    {
                        res.ErrorMsg = resmove.ErrorMsg;
                        return res;
                    }

                    //res = svc.SubmitSingleOrGroupReceiveLicense(groupId, resmove.DataResponse);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }



        /// <summary>
        /// ดึงข้อมูลที่รออนุมัติออกใบอนุญาต
        /// </summary>
        /// <param name="petitionTypeCode">รหัสประเภทการออกใบอนุญาต</param>
        /// <param name="startDate">วันทำรายการ (เริ่ม)</param>
        /// <param name="toDate">วันทำรายการ (สิ้นสุด)</param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet>
            GetLicenseVerify(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string CompCode, string requestCompCode)
        {
            return svc.GetLicenseVerify(petitionTypeCode, startDate, toDate, CompCode, requestCompCode);
        }

        /// <summary>
        /// ดึงข้อมูลที่รออนุมัติออกใบอนุญาต
        /// </summary>
        /// <param name="petitionTypeCode">รหัสประเภทการออกใบอนุญาต</param>
        /// <param name="startDate">วันทำรายการ (เริ่ม)</param>
        /// <param name="toDate">วันทำรายการ (สิ้นสุด)</param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet>
            GetLicenseVerifyByRequest(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string CompCode)
        {
            return svc.GetLicenseVerifyByRequest(petitionTypeCode, startDate, toDate, CompCode);
        }

        /// <summary>
        /// ดึงรายการข้อมูลการขอใบอนุญาต
        /// </summary>
        /// <param name="uploadGroupNo">เลขที่กลุ่ม</param>
        /// <param name="seqNo">ลำดับที่</param>
        /// <returns>ResponseService<LicenseVerifyDetail></returns>
        public DTO.ResponseService<DTO.LicenseVerifyDetail>
            GetLicenseVerifyDetail(string uploadGroupNo, string seqNo)
        {
            return svc.GetLicenseVerifyDetail(uploadGroupNo, seqNo);
        }


        /// <summary>
        /// อนุมัติการตรวจสอบเอกสารขอรับใบอนุญาต
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public DTO.ResponseService<string> ApproveLicenseVerify(List<DTO.SubmitLicenseVerify> list, string flgApprove, string ApproveID)
        {
            return svc.ApproveLicenseVerify(list.ToArray(), flgApprove, ApproveID);
        }

        /// <summary>
        /// ส่งข้อมูลการขอรับใบอนุญาตแบบเดี่ยว
        /// </summary>
        /// <param name="header">ส่วน header</param>
        /// <param name="detail">ส่วน detail</param>
        /// <param name="attachs">รายการเอกสารแนบ</param>
        /// <param name="userProfile">user profile</param>
        /// <returns></returns>
        public DTO.ResponseMessage<bool> InsertSingleReceiveLicense(DTO.ReceiveLicenseHeader header,
                                                                    DTO.ReceiveLicenseDetail detail,
                                                                    DTO.UserProfile userProfile)
        {
            return svc.InsertSingleReceiveLicense(header, detail, userProfile);
        }

        public DTO.ResponseMessage<bool> SubmitSingleReceiveLicense(string groupId, DTO.AttatchFileLicense[] attachs)
        {
            return svc.SubmitSingleOrGroupReceiveLicense(groupId, attachs);
        }

        //วนใน Folder และ SubFolder เพื่อเก็บเข้า List res
        private void GetAllFiles(List<string> res, string dir)
        {
            foreach (string d in Directory.GetDirectories(dir))
            {
                res.Add(d);
                GetAllFiles(res, d);
            }
        }

        //สำหรับเตรียม Directory และ File ที่ต้อง Zip
        private string InitRequestLicenseFileToDownload(string tempFolder, DateTime reqDate)
        {
            string result = tempFolder + "\\" + reqDate.ToString("yyyy_MM_dd");

            if (Directory.Exists(result))
            {
                Directory.Delete(result);
            }

            Directory.CreateDirectory(result);

            //TODO: Call Service สำหรับ Collection ที่จะเขียนลง TextFile และ เอกสารแนบ (ภาพ)

            //var res = svc.GetRequestLicenseDownload(reqDate);


            //TODO: สร้าง Text File

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@""))
            //{
            //    foreach (string line in lines)
            //    {
            //        file.WriteLine(line);
            //    }
            //}

            //TODO: Copy ภาพจากมาไว้ใน Folder

            //foreach(string f in files)
            //{
            //    string targetFile = string.Empty;
            //    if(File.Exists(targetFile))
            //    {
            //        File.Delete(targetFile);
            //    }
            //    File.Copy(f, targetFile);
            //}


            return result;
        }

        /// <summary>
        /// สำหรับ Zip ไฟล์และ Download
        /// </summary>
        /// <param name="httpResponse">Response ของ Page</param>
        /// <param name="reqDate">วันที่ต้องการ Download</param>
        /// <param name="tempFolder">Temp Folder</param>
        public void DownloadLicenseZip(System.Web.HttpResponse httpResponse, DateTime reqDate, string tempFolder)
        {
            //เตรียม Directory และ ไฟล์ที่ต้องการ Download
            string dir = InitRequestLicenseFileToDownload(tempFolder, reqDate);

            //สำหรับเก็บ Folder ที่ต้อง Zip เพื่อวนหาไฟล์ใน Folder
            List<string> list = new List<string>();
            list.Add(dir);

            //วนหา Folder ที่ต้อง Zip และ SubFolder ที่อยู่ภายใน
            GetAllFiles(list, dir);

            string folderName = string.Empty;

            //สร้าง  Instantce Zip
            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                //กำหนด Option Endcode
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;

                //วนเ Folder และ SubFolder
                for (int i = 0; i < list.Count; i++)
                {
                    //Folder ปัจจุบัน
                    DirectoryInfo dInfo = new DirectoryInfo(list[i]);
                    folderName += (i == 0 ? "" : "\\") + dInfo.Name;

                    //สร้าง Folder ใน Zip
                    zip.AddDirectoryByName(folderName);

                    //วน Add File ใน Folder
                    foreach (string f in Directory.GetFiles(list[i]))
                    {
                        zip.AddFile(f, folderName);
                    }
                }


                httpResponse.Clear();
                httpResponse.BufferOutput = false;
                string zipName = String.Format("{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                httpResponse.ContentType = "application/zip";
                httpResponse.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(httpResponse.OutputStream);
                httpResponse.End();
            }
        }

        public void Dispose()
        {
            svc = null;
        }

        #region Person License
        public DTO.ResponseService<DTO.ExamHistory[]> GetExamHistoryByID(string idCard)
        {
            return svc.GetExamHistoryByID(idCard);
        }

        public DTO.ResponseService<DTO.ExamHistory[]> GetExamHistoryByIDWithCondition(string idCard, string licenseTypeCode)
        {
            return svc.GetExamHistoryByIDWithCondition(idCard, licenseTypeCode);
        }


        public DTO.ResponseService<DTO.TrainPersonHistory[]> GetTrainingHistoryByID(string idCard)
        {
            return svc.GetTrainingHistoryByID(idCard);

        }

        public DTO.ResponseService<DTO.TrainPersonHistory[]> GetTrainingHistoryByIDWithCondition(string idCard, string licenseTypeCode)
        {
            return svc.GetTrainingHistoryByIDWithCondition(idCard, licenseTypeCode);

        }

        public DTO.ResponseService<DTO.Tran3To7[]> GetTrain_3_To_7_ByID(string idCard)
        {
            return svc.GetTrain_3_To_7_ByID(idCard);

        }

        public DTO.ResponseService<DTO.UnitLink[]> GetUnitLinkByID(string idCard)
        {
            return svc.GetUnitLinkByID(idCard);

        }

        public DTO.ResponseService<DTO.UnitLink[]> GetUnitLinkByIDWithCondition(string idCard, string licenseTypeCode)
        {
            return svc.GetUnitLinkByIDWithCondition(idCard, licenseTypeCode);

        }


        public DTO.ResponseService<DTO.PersonAttatchFile[]> GetAttatchFileLicenseByPersonId(string personID)
        {
            return svc.GetAttatchFileLicenseByPersonId(personID);

        }

        public DTO.ResponseMessage<bool> InsertPersonLicense(DTO.PersonLicenseHead[] header, DTO.PersonLicenseDetail[] detail, DTO.UserProfile userProfile, DTO.AttatchFileLicense[] files)
        {
            return svc.InsertPersonLicense(header, detail, userProfile, files);

        }


        public DTO.ResponseService<DTO.ApproverDoctype[]> GetApprocerByDocType(string appdocType)
        {
            return svc.GetApprocerByDocType(appdocType);
        }


        public DTO.ResponseService<DTO.PersonLicenseDetail[]> GenSEQLicenseDetail(DTO.PersonLicenseHead uploadGroupNo)
        {

            return svc.GenSEQLicenseDetail(uploadGroupNo);
        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> GetLicenseTransaction(DTO.PersonLicenseHead[] head, DTO.PersonLicenseDetail[] detail)
        {
            return svc.GetLicenseTransaction(head, detail);

        }

        public DTO.ResponseMessage<bool> SingleLicenseValidation(DTO.PersonLicenseHead head, DTO.PersonLicenseDetail detail)
        {

            return svc.SingleLicenseValidation(head, detail);

        }


        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> GetPaymentLicenseTransaction(DTO.PersonLicenseHead[] head, DTO.PersonLicenseDetail[] detail)
        {

            return svc.GetPaymentLicenseTransaction(head, detail);
        }

        public DTO.ResponseService<DataSet> GetRequesPersontLicenseByIdCard(string idCard)
        {
            return svc.GetRequesPersontLicenseByIdCard(idCard);
        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> GetRenewLicneseByIdCard(string idCard)
        {

            return svc.GetRenewLicneseByIdCard(idCard);
        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction> GetRenewLiceneEntityByLicenseNo(string licenseNo)
        {

            return svc.GetRenewLiceneEntityByLicenseNo(licenseNo);
        }

        public DTO.ResponseMessage<bool> updateRenewLicense(DTO.PersonLicenseHead h, DTO.PersonLicenseDetail d)
        {

            return svc.updateRenewLicense(h, d);
        }


        public DTO.ResponseMessage<bool> updateLicenseDetail(DTO.PersonLicenseDetail d)
        {
            return svc.updateLicenseDetail(d);
        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> GetExpiredLicneseByIdCard(string idCard)
        {

            return svc.GetExpiredLicneseByIdCard(idCard);
        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> getViewPersonLicense(string idCard, string status)
        {
            return svc.getViewPersonLicense(idCard, status);

        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction> GetLicenseDetailByUploadGroupNo(string upGroupNo)
        {

            return svc.GetLicenseDetailByUploadGroupNo(upGroupNo);
        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction[]> GetAllLicenseByIDCard(string idCard, string mode, int feemode)
        {
            return svc.GetAllLicenseByIDCard(idCard, mode, feemode);
        }

        #endregion


        public DTO.ResponseService<DataSet> GetLicenseVerifyHead(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string Compcode, string requestCompCode, string CountPage, int pageNo, int recordPerPage, string StatusApprove)
        {
            return svc.GetLicenseVerifyHead(petitionTypeCode, startDate, toDate, Compcode, requestCompCode, CountPage, pageNo, recordPerPage, StatusApprove);
        }

        public DTO.ResponseService<DataSet> GetListLicenseDetailVerify(string uploadGroupNo, string CountPage, int pageNo, int recordPerPage)
        {
            return svc.GetListLicenseDetailVerify(uploadGroupNo, CountPage, pageNo, recordPerPage);
        }

        public DTO.ResponseService<DTO.VerifyDocumentHeader> GetVerifyHeadByUploadGroupNo(string uploadGroupNo)
        {
            return svc.GetVerifyHeadByUploadGroupNo(uploadGroupNo);
        }

        public bool CheckLicenseDetailVerifyHasNotApprove(string uploadGroupNo)
        {
            var res = svc.CheckLicenseDetailVerifyHasNotApprove(uploadGroupNo);
            return res.ResultMessage;
        }

        public bool ValidateDetail(string groupId)
        {
            var res = svc.ValidateDetail(groupId);
            return res.ResultMessage;


        }


        public DTO.ResponseService<DTO.DetailTemp[]> GetDetail(string groupId)
        {
            return svc.GetDetail(groupId);
        }


        public DTO.ResponseService<String> GenZipFileLicenseRequest(DateTime findDate, String username)
        {

            return svc.GenZipFileLicenseRequest(findDate, username);
        }


        public DTO.ResponseService<DataSet> GetListLicenseDetailByCriteria(string licenseNo, string licenseType,
                                DateTime? startDate, DateTime? toDate,
                                string paymentNo, string licenseTypeReceive,
                                DTO.UserProfile userProfile,
                                int pageNo, int recordPerPage, string PageCount)
        {
            return svc.GetListLicenseDetailByCriteria(licenseNo, licenseType,
                                startDate, toDate,
                               paymentNo, licenseTypeReceive,
                                userProfile,
                               pageNo, recordPerPage, PageCount);
        }


        public DTO.ResponseService<DataSet> GetResultLicenseVerifyHead(string petitionTypeCode,
                     DateTime? startDate,
                     DateTime? toDate, string Compcode, string CountPage, int pageNo, int recordPerPage)
        {
            return svc.GetResultLicenseVerifyHead(petitionTypeCode,
                startDate, toDate,
                Compcode, CountPage, pageNo, recordPerPage);
        }


        public DTO.ResponseService<DataSet> GetEditLicenseHead(string petitionTypeCode,
            DateTime? startDate,
            DateTime? toDate, string Compcode, string CountPage, int pageNo, int recordPerPage)
        {
            return svc.GetEditLicenseHead(petitionTypeCode,
                startDate, toDate,
                Compcode, CountPage, pageNo, recordPerPage);
        }










        public DTO.ResponseService<DataSet> GetListLicenseDetailByPersonal(string licenseNo, string licenseType,
                              DateTime? startDate, DateTime? toDate,
                              string paymentNo, string licenseTypeReceive,
                              DTO.UserProfile userProfile,
                              int pageNo, int recordPerPage, Boolean CountAgain)
        {
            return svc.GetListLicenseDetailByPersonal(licenseNo, licenseType,
                                startDate, toDate,
                               paymentNo, licenseTypeReceive,
                                userProfile,
                               pageNo, recordPerPage, CountAgain);
        }

        public DTO.ResponseService<DataSet> GetLicenseVerifyHeadByOIC(string petitionTypeCode,
                            DateTime? startDate,
                            DateTime? toDate, string requestCompCode, string CountPage, int pageNo, int recordPerPage, string StatusApprove)
        {
            return svc.GetLicenseVerifyHeadByOIC(petitionTypeCode, startDate, toDate, requestCompCode, CountPage, pageNo, recordPerPage, StatusApprove);
        }

        public DTO.ResponseService<DataSet> GetListLicenseDetailAdminByCriteria(string licenseNo, string licenseType,
                               DateTime? startDate, DateTime? toDate,
                               string paymentNo, string licenseTypeReceive,
                               DTO.UserProfile userProfile,
                               int pageNo, int recordPerPage, string PageCount)
        {
            return svc.GetListLicenseDetailAdminByCriteria(licenseNo, licenseType,
                                startDate, toDate,
                               paymentNo, licenseTypeReceive,
                                userProfile,
                               pageNo, recordPerPage, PageCount);
        }

        public DTO.ResponseService<IAS.DTO.ValidateLicense[]> GetPropLiecense(string licenseType, string pettionType, Int32 renewTime, Int32 groupId)
        {
            return svc.GetPropLiecense(licenseType, pettionType, renewTime, groupId);
        }



        public DTO.ResponseMessage<bool> ChkSpecialExam(List<string> filesType, string licenseType)
        {
            return svc.ChkSpecialExam(filesType.ToArray(), licenseType);
        }

        public DTO.ResponseMessage<int> GetRenewTimebyLicenseNo(string licenseNo)
        {
            return svc.GetRenewTimebyLicenseNo(licenseNo);
        }


        public DTO.ResponseMessage<bool> ValidateProp(string groupId)
        {
            return svc.ValidateProp(groupId);
        }

        public DTO.ResponseMessage<bool> CheckSpecial(string idCard)
        {
            return svc.CheckSpecial(idCard);
        }

        public DTO.ResponseService<DTO.TrainSpecial[]> GetSpecialTempTrainById(string idCard)
        {
            return svc.GetSpecialTempTrainById(idCard);
        }


        public DTO.ResponseService<DTO.ExamSpecial[]> GetSpecialTempExamById(string idCard)
        {
            return svc.GetSpecialTempExamById(idCard);
        }

        public DTO.ResponseMessage<bool> LicenseRevokedValidation(string[] license, string licenseTypeCode)
        {
            return svc.LicenseRevokedValidation(license, licenseTypeCode);
        }

        public DTO.ResponseService<DataSet> GetRenewLicenseQuick(string PetitionType, DateTime? DateStart, DateTime? DateEnd, string CompCode, string Days)
        {
            return svc.GetRenewLicenseQuick(PetitionType, DateStart, DateEnd, CompCode, Days);
        }

        public DTO.ResponseService<DTO.PersonLicenseApprover[]> GetPersonalLicenseApprover(string licenseType)
        {

            return svc.GetPersonalLicenseApprover(licenseType);
        }

        public DTO.ResponseService<DTO.TrainPersonHistory> GetPersonalTrainByCriteria(string licenseTypeCode, string pettitionTypeCode, string renewTime, string idCard, string licenseNo, string specialTrainCode)
        {
            return svc.GetPersonalTrainByCriteria(licenseTypeCode, pettitionTypeCode, renewTime, idCard, licenseNo, specialTrainCode);
        }

        public DTO.ResponseService<DateTime[]> GetLicenseRequestOicApprove(DTO.RangeDateRequest request)
        {
            return svc.GetLicenseRequestOicApprove(request);
        }

        public DTO.ResponseService<DataSet> GetTopCompanyMoveOut(string LicenseType, DateTime? DateStart, DateTime? DateEnd)
        {
            return svc.GetTopCompanyMoveOut(LicenseType, DateStart, DateEnd);
        }

        public DTO.ResponseService<DataSet> GetLicenseStatisticsReport(string LicenseTypeCode, string StartDate1, string EndDate1, string StartDate2, string EndDate2)
        {
            return svc.GetLicenseStatisticsReport(LicenseTypeCode, StartDate1, EndDate1, StartDate2, EndDate2);
        }

        public DTO.ResponseService<DataSet> GetSumLicenseStatisticsReport(string StartDate1, string EndDate1)
        {
            return svc.GetSumLicenseStatisticsReport(StartDate1, EndDate1);
        }

        public DTO.ResponseService<DataSet> GetReplacementReport(string LicenseTypeCode, string Compcode, string Replacement, string StartDate, string EndDate)
        {
            return svc.GetReplacementReport(LicenseTypeCode, Compcode, Replacement, StartDate, EndDate);
        }


        public DTO.ResponseMessage<bool> GetAgentCheckTrain(string id)
        {
            return svc.GetAgentCheckTrain(id);
        }

        public DTO.ResponseService<DTO.ConfigDocument[]> GetLicenseConfigByPetition(string petitionType, string licenseType)
        {
            return svc.GetLicenseConfigByPetition(petitionType, licenseType);
        }

        public DTO.ResponseService<DataSet> GetLicenseDetailByCriteria(DateTime dateStart, DateTime dateEnd, string IdCardNo, string Names, string Lastname, string LicenseType, string CompCode, int Page, int RowPerPage, bool isCount)
        {
            return svc.GetLicenseDetailByCriteria(dateStart, dateEnd, IdCardNo, Names, Lastname, LicenseType, CompCode, Page, RowPerPage, isCount);
        }

        public DTO.ResponseService<string> GenZipFileLicenseByIdCardNo(List<DTO.GenLicenseDetail> LicenseDetail, string username)
        {
            return svc.GenZipFileLicenseByIdCardNo(LicenseDetail.ToArray(), username);
        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction> GetLicenseRenewDateByLicenseNo(string licenseNo)
        {
            return svc.GetLicenseRenewDateByLicenseNo(licenseNo);
        }

        public DTO.ResponseMessage<bool> ChkLicenseAboutActive(string idCard, string licenseType)
        {
            return svc.ChkLicenseAboutActive(idCard, licenseType);
        }
    }
}

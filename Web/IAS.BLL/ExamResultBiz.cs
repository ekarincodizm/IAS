using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data;
using IAS.BLL.Properties;
namespace IAS.BLL
{
    public class ExamResultBiz
    {
        ExamService.ExamServiceClient svc;

        public ExamResultBiz()
        {
            svc = new ExamService.ExamServiceClient();
        }


        private static String UploadFileApplicantToTemp(Stream rawData)
        {
            String filePath = "";
            using (FileService.FileTransferServiceClient serviceFile = new FileService.FileTransferServiceClient())
            {
                String container = Path.Combine("Applicant", DateTime.Now.ToString("yyyyMMdd"));
                String upfilename = String.Format("{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmss"));
                DTO.FileService.UploadFileResponse resFile = serviceFile.UploadFile(new DTO.FileService.UploadFileRequest() { FileStream = rawData, TargetFileName = upfilename, TargetContainer = container });
                if (resFile.Code != "0000")
                {
                    throw new ApplicationException("ไม่สามารถ นำเข้าเอกสารได้");
                }

                filePath = resFile.TargetFullName;
            }
            return filePath;
        }


        /// <summary>
        /// Upload ผลการสอบ
        /// </summary>
        /// <param name="fileName">ชื่อไฟล์</param>
        /// <param name="rawData">ข้อมูลผลการสอบ</param>
        /// <param name="userId">user id</param>
        /// <returns>ResponseService<UploadResult<UploadHeader, ExamResultTemp>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>>
            UploadData(string fileName, Stream rawData, string userId,string TestNo)
        {
            var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>>();
            if (rawData == null)
            {
                res.ErrorMsg = Resources.errorExamResultBiz_003;
                return res;
            }

           
            //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

            String filePath = UploadFileApplicantToTemp(rawData);

          /*  DTO.UploadData data = new DTO.UploadData
            {
                Body = new List<string>()
            };

            try
            {
                try
                {
                    //Stream rawData = FileUpload1.PostedFile.InputStream;
                    using (StreamReader sr = new StreamReader(rawData, System.Text.Encoding.GetEncoding("TIS-620")))
                    {
                        int i=0;
                        string line = string.Empty; // sr.ReadLine().Trim();
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (i == 0)
                            {
                                if (line != null && line.Length > 0)
                                {
                                    if (line.Substring(0, 1) == "H")
                                    {
                                        data.Header = line;
                                    }
                                    else
                                    {
                                        res.ErrorMsg = Resources.errorExamResultBiz_001;
                                        return res;
                                    }
                                }
                            }
                            else
                            {
                                if (line.Trim().Length > 0)
                                {
                                    data.Body.Add(line.Trim());
                                }
                            }
                            i++;
                        }

                        if (i == 0)//แสดงว่าไม่ได้เข้าไปวนลูป while เลย
                        {
                            res.ErrorMsg = Resources.errorExamResultBiz_002;
                            return res;
                        }
                    }

                    res = svc.InsertAndCheckExamResultUpload(fileName, userId, TestNo);

                }
                catch (Exception ex)
                {
                    res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                }
            }
            catch (IOException ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
           * */

            res = svc.InsertAndCheckExamResultUpload(filePath, userId, TestNo);
            return res;
        }
        

        /// <summary>
        /// ดึงผลการสอบเพื่อแก้ไข
        /// </summary>
        /// <param name="uploadGroupNo">เลขที่กลุ่ม Upload</param>
        /// <param name="seqNo">ลำดับที่</param>
        /// <returns>ResponseService<ExamResultTempEdit></returns>
        public DTO.ResponseService<DTO.ExamResultTempEdit>
            GetExamResultTempEdit(string uploadGroupNo, string seqNo)
        {
            return svc.GetExamResultTempEdit(uploadGroupNo, seqNo);
        }


        public DTO.ResponseService<DataSet> Subject_List(string lic_type_code)
        {
            return svc.GetSubject_List(lic_type_code);
        }


        /// <summary>
        /// Update ผลการสอบที่แก้ไข
        /// </summary>
        /// <param name="exam">ข้อมูลผลการสอบ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateExamResultEdit(DTO.ExamResultTempEdit exam)
        {
            return svc.UpdateExamResultEdit(exam);
        }


        /// <summary>
        /// ดึงข้อมูลผลการสอบทั้งกลุ่ม
        /// </summary>
        /// <param name="groupId">เลขที่กลุ่ม Upload</param>
        /// <returns>ResponseService<UploadResult<UploadHeader, ExamResultTemp>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ExamResultTemp>>
            GetExamResultUploadByGroupId(string groupId)
        {
            return svc.GetExamResultUploadByGroupId(groupId);
        }


        /// <summary>
        /// นำผลการสอบจาก Temp เข้าระบบ
        /// </summary>
        /// <param name="groupId">เลขที่กลุ่ม Upload</param>
        /// <param name="userId">user id</param>
        /// <param name="expireDate">วันที่หมดอายุ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> ExamResultUploadToSubmitNew(string groupId, string userId, DateTime? expireDate,string testingNo)
        {
            try
            {
                return svc.ExamResultUploadToSubmitNew(groupId, userId, expireDate, testingNo);
            }
            catch (Exception ex)
            {
                DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
                res.ErrorMsg = ex.Message;
                return res;
            }
        }

        //public DTO.ResponseMessage<bool> ExamResultUploadToSubmit(string groupId, string userId, DateTime? expireDate)
        //{
        //    return svc.ExamResultUploadToSubmit(groupId, userId, expireDate);
        //}

        /// <summary>
        /// ดึงข้อมูล TestingNo จากไฟล์นำเข้าผลการสอบ
        /// </summary>
        public string GetTestingNoFrom_fileImport(DTO.ExamHeaderResultTemp head) 
        {
            return svc.GetTestingNoFrom_fileImport(head);
        }

        public DTO.ResponseService<DataSet> GetExamStatistic(string LicenseType, DateTime? DateStart, DateTime? DateEnd)
        {
            return svc.GetExamStatistic(LicenseType, DateStart, DateEnd);
        }
    
       
    }
}

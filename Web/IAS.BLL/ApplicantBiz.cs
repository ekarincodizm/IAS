using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DTO;
using IAS.Utils;
using System.Data;
using System.IO;
using System.Threading;
using IAS.BLL.Properties;

namespace IAS.BLL
{
    public class ApplicantBiz : IDisposable
    {
        private ApplicantService.ApplicantServiceClient svc;

        public ApplicantBiz()
        {
            svc = new ApplicantService.ApplicantServiceClient();
        }

        public DTO.ResponseMessage<bool> Update(DTO.Applicant app)
        {
            ResponseMessage<bool> res = svc.Update(app);
            return res;
        }

        public DTO.ResponseMessage<bool> UpdateExamGroupUpload(DTO.ApplicantTemp temp)
        {
            ResponseMessage<bool> res = svc.UpdateApplicantGroupUpload(temp);
            return res;
        }

        ////สำหรับบุคคลทั่วไป
        //public DTO.ResponseService<DataSet> PersonGetApplicantByCriteria(string idCard)
        //{
        //    ResponseService<DataSet> res = new ResponseService<DataSet>();
        //    if (!string.IsNullOrEmpty(idCard))
        //    {
        //        res = svc.PersonGetApplicantByCriteria(idCard);
        //    }
        //    return res;
        //}

        ////สำหรับสมาคม 
        //public DTO.ResponseService<DataSet> GetApplicantByLicenseType(string licenseType)
        //{
        //    ResponseService<DataSet> res = svc.GetApplicantByLicenseType(licenseType);
        //    return res;
        //}

        //สำหรับบริษัทประกันภัย และเจ้าหน้าที่ คปภ.

        public DTO.ResponseService<DataSet>
            GetApplicantById(string applicantCode, string testingNo, string examPlaceCode)
        {
            return svc.GetApplicantById(applicantCode, testingNo, examPlaceCode);
        }

        public DTO.ResponseService<DataSet> GetApplicantByCriteria(DTO.RegistrationType userRegType, string compCode,
                                                                   string idCard, string testingNo,
                                                                   string firstName, string lastName,
                                                                   DateTime? startDate, DateTime? toDate,
                                                                   string paymentNo, string billNo,
                                                                   int RowPerPage, int num_page, Boolean Count, string license, string time, string examPlaceGroupCode, string examPlaceCode, string chequeNo, string examResult, DateTime? startCandidates, DateTime? endCandidates)
        {
            ResponseService<DataSet> res = svc.GetApplicantByCriteria(userRegType, compCode,
                                                 idCard, testingNo,
                                                 firstName, lastName,
                                                 startDate, toDate,
                                                 paymentNo, billNo,
                                                 RowPerPage, num_page, Count, license, time, examPlaceGroupCode, examPlaceCode, chequeNo, examResult, startCandidates, endCandidates);

            return res;
        }


        public DTO.ResponseService<string> GetApplicantByCriteriaSendMail(DTO.RegistrationType userRegType, string compCode,
                                                             string idCard, string testingNo,
                                                             string firstName, string lastName,
                                                             DateTime? startDate, DateTime? toDate,
                                                             string paymentNo, string billNo,
                                                             int RowPerPage, int pageNum, Boolean Count, string license, string time, string examPlaceGroupCode, string examPlaceCode, string chequeNo, string examResult, DateTime? startCandidates, DateTime? endCandidates, string address, string name, string email)
        {
            return svc.GetApplicantByCriteriaSendMail(userRegType, compCode,
                                                 idCard, testingNo,
                                                 firstName, lastName,
                                                 startDate, toDate,
                                                 paymentNo, billNo,
                                                 RowPerPage, pageNum, Count, license, time, examPlaceGroupCode, examPlaceCode, chequeNo, examResult, startCandidates, endCandidates, address, name, email);
        }

        /// <summary>
        /// สำหรับ Upload การสมัครสอบแบบกลุ่ม
        /// </summary>
        /// <param name="fileName">ชื่อไฟล์ที่ Upload</param>
        /// <param name="rawData">ข้อมูลที่ Upload (Stream)</param>
        /// <param name="testingNo">เลขที่สอบ</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        public DTO.ResponseService<DTO.SummaryReceiveApplicant>
            UploadData(string fileName, Stream rawData, string testingNo, string examPlaceCode, string userId, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseService<DTO.SummaryReceiveApplicant>();
            if (rawData == null)
            {
                res.ErrorMsg = Resources.errorApplicantBiz_001;
                return res;
            }
            try
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                //DTO.UploadData data = ReadFileUpload(rawData);
                //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
                DTO.RegistrationType memberType = (DTO.RegistrationType)userProfile.MemberType;
                // FileManagement.FileServiceClient file = new FileManagement.FileServiceClient();
                String filePath = UploadFileApplicantToTemp(rawData);


                InsertAndCheckApplicantGroupUploadRequest request = new InsertAndCheckApplicantGroupUploadRequest()
                {
                    FilePath = filePath,
                    FileName = fileName,
                    RegistrationType = memberType,
                    TestingNo = testingNo,
                    ExamPlaceCode = examPlaceCode,
                    UserProfile = userProfile
                };
                res = svc.InsertAndCheckApplicantGroupUpload(request);
                // res = file.InsertAndCheckApplicantGroupUpload(data, fileName, memberType, testingNo, examPlaceCode, userProfile);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        private static String UploadFileApplicantToTemp(Stream rawData)
        {
            String filePath = "";
            using (FileService.FileTransferServiceClient serviceFile = new FileService.FileTransferServiceClient())
            {
                String container = Path.Combine("Applicant", DateTime.Now.ToString("yyyyMMdd"));
                String upfilename = String.Format("{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss"));
                DTO.FileService.UploadFileResponse resFile = serviceFile.UploadFile(new DTO.FileService.UploadFileRequest() { FileStream = rawData, TargetFileName = upfilename, TargetContainer = container });
                if (resFile.Code != "0000")
                {
                    throw new ApplicationException("ไม่สามารถ นำเข้าเอกสารได้");
                }

                filePath = resFile.TargetFullName;
            }
            return filePath;
        }

        private ResponseService<DTO.SummaryReceiveApplicant> UploadFileApplicant
                        (string fileName, Stream fileApplicate, string testingNo, string examPlaceCode, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseService<DTO.SummaryReceiveApplicant>();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

            try
            {
                //DTO.UploadData data = ReadFileUpload(fileApplicate);

                try
                {

                    BLL.PersonBiz biz = new PersonBiz();
                    int memberType = 0;

                    var user = biz.GetUserProfileById(userProfile.Id);
                    if (!string.IsNullOrEmpty(user.DataResponse.ID))
                    {
                        memberType = Convert.ToInt32(user.DataResponse.MEMBER_TYPE);

                        if (memberType == DTO.RegistrationType.Insurance.GetEnumValue())
                        {

                            String filePath = UploadFileApplicantToTemp(fileApplicate);

                            InsertAndCheckApplicantGroupUploadRequest request = new InsertAndCheckApplicantGroupUploadRequest()
                            {
                                FilePath = filePath,
                                FileName = fileName,
                                RegistrationType = DTO.RegistrationType.Insurance,
                                TestingNo = testingNo,
                                ExamPlaceCode = examPlaceCode,
                                UserProfile = userProfile
                            };
                            res = svc.InsertAndCheckApplicantGroupUpload(request);

                        }
                        else if (memberType == DTO.RegistrationType.Association.GetEnumValue())
                        {
                            String filePath = UploadFileApplicantToTemp(fileApplicate);

                            InsertAndCheckApplicantGroupUploadRequest request = new InsertAndCheckApplicantGroupUploadRequest()
                            {
                                FilePath = filePath,
                                FileName = fileName,
                                RegistrationType = DTO.RegistrationType.Association,
                                TestingNo = testingNo,
                                ExamPlaceCode = examPlaceCode,
                                UserProfile = userProfile
                            };
                            res = svc.InsertAndCheckApplicantGroupUpload(request);
                        }
                    }
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
            return res;
        }



        /// <summary>
        /// ดึงข้อมูลรายการที่ Upload เข้า Temp แล้ว
        /// </summary>
        /// <param name="uploadGroupNo">เลขที่กลุ่ม Upload</param>
        /// <param name="seqNo">ลำดับที่ของข้อมูล</param>
        /// <returns>ข้อมูลที่ Upload เข้า Temp</returns>
        public DTO.ResponseService<DTO.ApplicantTemp>
            GetApplicantUploadTempById(string uploadGroupNo, string seqNo)
        {
            return svc.GetApplicantUploadTempById(uploadGroupNo, seqNo);
        }

        /// <summary>
        /// Update ข้อมูลที่แก้ไขแล้วเข้า Temp
        /// </summary>
        /// <param name="exam">Class ข้อมูล</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateApplicantGroupUpload(DTO.ApplicantTemp exam)
        {
            return svc.UpdateApplicantGroupUpload(exam);
        }

        /// <summary>
        /// Update ข้อมูลการสมัครเดี่ยวที่แก้ไขแล้วเข้า Temp
        /// </summary>
        /// <param name="exam">Class ข้อมูล</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseService<string> InsertSingleApplicant(List<DTO.ApplicantTemp> exam, string userId)
        {
            return svc.InsertSingleApplicant(exam.ToArray(), userId);
        }

        /// <summary>
        /// สำหรับดึงข้อมูลที่ Upload การสมัครสอบกลุ่ม
        /// </summary>
        /// <param name="groupUploadNo">เลขที่กลุ่ม Upload</param>
        /// <returns>ResponseService<UploadResult<UploadHeader, ApplicantTemp>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>
            GetApplicantGroupUploadByGroupUploadNo(string groupUploadNo)
        {
            return svc.GetApplicantGroupUploadByGroupUploadNo(groupUploadNo);
        }

        /// <summary>
        /// นำข้อมูลจาก Temp มาใช้จริง
        /// </summary>
        /// <param name="groupId">เลขที่กลุ่ม Upload</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseService<string> ApplicantGroupUploadToSubmit(string groupId, DTO.UserProfile userProfile)
        {
            return svc.ApplicantGroupUploadToSubmit(groupId, userProfile);
        }

        public DTO.ResponseService<DTO.ApplicantInfo>
            GetApplicantInfo(string applicantCode, string testingNo, string examPlaceCode, int RowPerPage, int num_page, Boolean Count)
        {
            var res = new DTO.ResponseService<DTO.ApplicantInfo>();
            try
            {

                res = svc.GetApplicantInfo(applicantCode, testingNo, examPlaceCode, RowPerPage, num_page, Count);

                //res.DataResponse = new ApplicantInfo
                //{
                //    Absent = false,
                //    AcceptOfficeName = "สำนักงานทดสอบ",
                //    ApplicantCode = "1",
                //    ApplyDate = DateTime.Now.Date,
                //    ExamForce = false,
                //    ExamPlace = "ธนาคารไทยณิชย์",
                //    ExamResult = "result",
                //    ExpireDate = DateTime.Now.Date,
                //    FirstName = "ปรมะ",
                //    IdCard = "1100400363191",
                //    InsuranceCompanyName = "ทริป เมเนจเม้นท์ โบรกเกอร์ จำกัด",
                //    LastName = "พงษ์คณาธร",
                //    LicenseApprove = false,
                //    PaymentNo = "11/22",
                //    Subjects = new List<ExamScoreResult> { new ExamScoreResult { LicenseType = "licenseType", Score = 50, SubjectCode = "วิชา 1" } },
                //    TestingDate = DateTime.Now.Date,
                //    TestingNo = "561449",
                //    Title = "นาย"
                //};
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public void Dispose()
        {
            if (svc != null) svc.Close();
            GC.SuppressFinalize(this);
        }


        public DTO.ResponseService<DataSet> GetRequestEditApplicant(DTO.RegistrationType userRegType,
                                                                   string idCard, string testingNo, string CompCode)
        {
            return svc.GetRequestEditApplicant(userRegType, idCard, testingNo, CompCode);


        }

        public DTO.ResponseService<DataSet> GetApplicantChangeMaxID()
        {
            return svc.GetApplicantChangeMaxID();


        }
        public DTO.ResponseMessage<bool> InsertApplicantChange(DTO.ApplicantChange appChange)
        {
            return svc.InsertApplicantChange(appChange);
        }
        public DTO.ResponseService<DataSet> GetHistoryApplicant(DTO.RegistrationType userRegType,
                                                                  string idCard, string testingNo, string CompCode, string ExamPlaceCode, string Status, int pageNo, int recordPerPage, Boolean Count, string Asso, string oic)
        {


            return svc.GetHistoryApplicant(userRegType, idCard, testingNo, CompCode, ExamPlaceCode, Status, pageNo, recordPerPage, Count, Asso, oic);
        }

        public DTO.ResponseService<DataSet> GetApproveEditApplicant(DTO.RegistrationType userRegType,
                                                                string idCard, string testingNo, string CompCode, string ExamPlaceCode, string Status, int pageNo, int recordPerPage, Boolean Count, string membertype, string Asso, string oic)
        {
            return svc.GetApproveEditApplicant(userRegType, idCard, testingNo, CompCode, ExamPlaceCode, Status, pageNo, recordPerPage, Count, membertype, Asso, oic);
        }

        public DTO.ResponseService<DataSet> GetApplicantTLogMaxID()
        {
            return svc.GetApplicantTLogMaxID();
        }

        public DTO.ResponseService<DataSet> GetApplicantTtoLog(DTO.RegistrationType userRegType,
                                                                 string idCard, string testingNo, string CompCode)
        {
            return svc.GetApplicantTtoLog(userRegType, idCard, testingNo, CompCode);
        }

        public DTO.ResponseMessage<bool> InsertApplicantTLog(DTO.ApplicantTLog appTLog)
        {
            return svc.InsertApplicantTLog(appTLog);
        }

        public DTO.ResponseService<DTO.Applicant> GetApplicantDetail(int applicantCode, string testingNo, string examPlaceCode)
        {
            return svc.GetApplicantDetail(applicantCode, testingNo, examPlaceCode);
        }

        public DTO.ResponseMessage<bool> InsertAttrachFileApplicantChange(DTO.AttachFileApplicantChange[] appAttachFileChange, DTO.UserProfile userProfile, DTO.ApplicantChange appChange)
        {
            return svc.InsertAttrachFileApplicantChange(appAttachFileChange, userProfile, appChange);
        }

        public DTO.ResponseService<DataSet> GetAttachFileAppChange(DTO.RegistrationType userRegType, string changeid)
        {
            return svc.GetAttachFileAppChange(userRegType, changeid);
        }

        public DTO.ResponseService<DTO.AttachFileApplicantChangeEntity[]> GetAttatchFilesAppChangeByIDCard(string idcard, int changeid)
        {
            return svc.GetAttatchFilesAppChangeByIDCard(idcard, changeid);
        }

        //public IList<BLL.AttachFilesIAS.AttachFile> ConvertToAttachFilesApplicantView(IList<DTO.AttachFileApplicantChangeEntity> attachFiles)
        //{
        //    IList<BLL.AttachFilesIAS.AttachFile> resAttachFiles = new List<BLL.AttachFilesIAS.AttachFile>();
        //    foreach (DTO.AttachFileApplicantChangeEntity attachFile in attachFiles)
        //    {
        //        resAttachFiles.Add(new BLL.AttachFilesIAS.AttachFile()
        //        {
        //            ID = attachFile.ID,
        //            REGISTRATION_ID = attachFile.REGISTRATION_ID,
        //            ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
        //            ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
        //            REMARK = attachFile.REMARK,
        //            CREATED_BY = attachFile.CREATED_BY,
        //            CREATED_DATE = (attachFile.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.CREATED_DATE,
        //            UPDATED_BY = attachFile.UPDATED_BY,
        //            UPDATED_DATE = (attachFile.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.UPDATED_DATE,
        //            FILE_STATUS = attachFile.FILE_STATUS,
        //            REQUEST_STATUS = attachFile.REQUEST_STATUS
        //        });
        //    }
        //    return resAttachFiles;
        //}

        public DTO.ResponseService<DataSet> GetApproveAppForStatus(DTO.RegistrationType userRegType,
                                                                string idcard, string status, string asso, string oic)
        {
            return svc.GetApproveAppForStatus(userRegType, idcard, status, asso, oic);
        }

        public DTO.ResponseMessage<bool> SendMailAppChange(string idcard, string TestingNo, string CompCode)
        {
            return svc.SendMailAppChange(idcard, TestingNo, CompCode);
        }

        public DTO.ResponseService<DataSet> GetCheckIDAppT(string idcard, string TestingNo, string CompCode)
        {
            return svc.GetCheckIDAppT(idcard, TestingNo, CompCode);
        }

        public DTO.ResponseService<DataSet> getManageApplicantCourse(string LicenseType, string StartExamDate, string EndExamDate, string Place, string PlaceName, string TimeExam, string TestingNO, int resultPage, int PAGE_SIZE, Boolean Count)
        {
            return svc.getManageApplicantCourse(LicenseType, StartExamDate, EndExamDate, Place, PlaceName, TimeExam, TestingNO, resultPage, PAGE_SIZE, Count);

        }

        public DTO.ResponseService<DataSet> GetExamPlaceByLicenseAndOwner(string owner, string license)
        {
            return svc.GetExamPlaceByLicenseAneOwner(owner, license);
        }


        public DTO.ResponseService<DataSet> GetApplicantFromTestingNoForManageApplicant(string testingNo, string ConSQL, int resultPage, int PAGE_SIZE, Boolean Count)
        {
            return svc.GetApplicantFromTestingNoForManageApplicant(testingNo, ConSQL, resultPage, PAGE_SIZE, Count);
        }


        #region ManageApplicantPage
        public DTO.ResponseService<DTO.DataItem[]> GetExamRoomByTestingNoforManage(string testingNo, string PlaceCode)
        {
            return svc.GetExamRoomByTestingNoforManage(testingNo, PlaceCode);
        }


        public DTO.ResponseMessage<bool> SaveExamAppRoom(string[] Manage_App, string room, string testingNo, string PaymentNo, Boolean AutoManage, string UserId)
        {
            return svc.SaveExamAppRoom(Manage_App, room, testingNo, PaymentNo, AutoManage, UserId);
        }

        public DTO.ResponseMessage<bool> CancleExamApplicantManage(string[] Manage_App, string testingNo)
        {
            return svc.CancleExamApplicantManage(Manage_App, testingNo);
        }
        #endregion ManageApplicantPage




        public string GetQuantityBillPerPageByConfig()
        {
            return svc.GetQuantityBillPerPageByConfig();
            //return svc.getq
        }

        public DTO.ResponseMessage<bool> CheckApplicantIsDuplicate(string TestingNo, string idcard, DateTime testTingDate, string testTimeCode, string examPlaceCode)
        {
            return svc.CheckApplicantIsDuplicate(TestingNo, idcard, testTingDate, testTimeCode, examPlaceCode);
        }

        public DTO.ResponseMessage<bool> ValidateApplicantTestCenter(string TestingNo, string idcard, DateTime testTingDate, string testTimeCode, string examPlaceCode)
        {
            return svc.ValidateApplicantTestCenter(TestingNo, idcard, testTingDate, testTimeCode, examPlaceCode);
        }

        public List<string> CheckApplicantExamDup(string idcard)
        {
            var res = svc.CheckApplicantExamDup(idcard);

            return res.DataResponse == null ? null : res.DataResponse.ToList();
        }

        public DTO.ResponseMessage<bool> ValidateApplicantSingleBeforeSubmit(IEnumerable<DTO.ApplicantTemp> app)
        {
            DTO.ResponseMessage<bool> response = new DTO.ResponseMessage<bool>();

            //ValidateApplicantSingleBeforeSubmitRequest applicants = new ValidateApplicantSingleBeforeSubmitRequest() { Applicants = app };
            //return svc.ValidateApplicantSingleBeforeSubmit(applicants);
            //return response;
            FileManagement.FileServiceClient file = new FileManagement.FileServiceClient();

            return file.ValidateApplicantSingleBeforeSubmit(app.ToArray());
        }



        public bool IsPersonCanApplicant(IsPersonCanApplicantRequest request)
        {
            ResponseMessage<bool> response = svc.IsPersonCanApplicant(request);
            return response.ResultMessage;
        }

        public DTO.ResultValidateApplicant ValidateApplicantBeforeSaveList(string TestingNo, string idcard, DateTime testTingDate, string testTimeCode,
            string examPlaceCode, string time, List<DTO.AddApplicant> lstApplicant)
        {
            ValidateApplicantBeforeSaveListRequest request = new ValidateApplicantBeforeSaveListRequest()
            {
                TestingNo = TestingNo,
                IdCard = idcard,
                TestingDate = testTingDate,
                TestTimeCode = testTimeCode,
                ExamPlaceCode = examPlaceCode,
                Time = time,
                AddApplicants = lstApplicant
            };
            return svc.ValidateApplicantBeforeSaveList(request);

        }

        public DTO.ResponseMessage<bool> GeneralValidateApplicantSingleBeforeSubmit(IEnumerable<DTO.ApplicantTemp> app)
        {
            ValidateApplicantSingleBeforeSubmitRequest applicants = new ValidateApplicantSingleBeforeSubmitRequest() { Applicants = app };
            return svc.ValidateApplicantSingleBeforeSubmit(applicants);
        }
    }


}

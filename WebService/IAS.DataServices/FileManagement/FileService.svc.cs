using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using IAS.DTO;
using IAS.Utils;
using System.ServiceModel.Channels;
using System.Configuration;
using IAS.DataServices.Properties;
using System.ServiceModel.Activation;
using IAS.DAL;
using IAS.Common.Logging;
using IAS.DataServices.Applicant.ApplicantRequestUploads;
using System.Transactions;

namespace IAS.DataServices.FileManagement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FileService : IFileService
    {
        private NASDrive nasDrive;
        string netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
        string userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
        string passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];

        
        private void ConnectNetDrive()
        {
            this.nasDrive = new NASDrive(netDrive, userNetDrive, passNetDrive);
        }

        private void DisConnectNetDrive()
        {
            if (this.nasDrive != null)
            {
                this.nasDrive.Dispose();
            }
        }

        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FileUploadMessage UploadFile(FileUploadMessage request)
        {
            ConnectNetDrive();

            string filePath = string.Empty;
            try
            {
                //ตอน Upload ต้องระบุ Folder มาที่  request.Metadata.TargetFolder ด้วย
                filePath = netDrive + request.Metadata.TargetFolder  + @"\";

                DirectoryInfo di = null;
                if(! Directory.Exists(filePath))
                {
                    di = Directory.CreateDirectory(filePath);    
                }

                string targetFileName = Path.Combine(filePath, request.Metadata.RemoteFileName);

                using (FileStream outfile = new FileStream(targetFileName, FileMode.Create))
                {
                    //const int bufferSize = 65536; // 64K
                    const int bufferSize = 2147483647; // 64K
                    Byte[] buffer = new Byte[bufferSize];
                    int bytesRead = request.FileByteStream.Read(buffer, 0, bufferSize);

                    while (bytesRead > 0)
                    {
                        outfile.Write(buffer, 0, bytesRead);
                        bytesRead = request.FileByteStream.Read(buffer, 0, bufferSize);
                    }
                    request.Metadata.ResponseMessage = "Success";
                    //request.Metadata.ResponseMessage = Resources.infoFileService_001; // "Success Save at " + serverFileName;
                }
            }
            catch (IOException e)
            {
                request.Metadata.ResponseMessage = e.Message; // FilePath + " " + e.Message;
            }
            finally
            {
                DisConnectNetDrive();    
            }
            return request;
        }

        /// <summary>
        /// Download File
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FileDownloadReturnMessage DownloadFile(FileDownloadMessage request)
        {
            ConnectNetDrive();
            Stream fs = null;
            string localFileName = request.FileMetaData.LocalFileName;
            try
            {
                string serverFileName = request.FileMetaData.RemoteFileName;
                fs = new FileStream(serverFileName, FileMode.Open);
                var fdMsg = new FileDownloadReturnMessage(new FileMetaData(localFileName, serverFileName), fs);
                return fdMsg;
            }
            catch (IOException e)
            {
                throw new FaultException<IOException>(e);
            }
            finally
            {
                if (fs != null) fs.Close();
                DisConnectNetDrive();    
            }
        }


        public RemoteFileInfo DownloadFileSign(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            try
            {
                
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public void UpdateOIC(RemoteFileInfo request)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var ctx = new IASPersonEntities();
                var entExist = ctx.AG_IAS_USERS
                                      .Where(w => w.USER_ID == request.userId)
                                      .FirstOrDefault();
                if (entExist == null)
                {
                    res.ErrorMsg = Resources.errorPersonService_014 + request.oicUserName + Resources.errorPersonService_015;
                   // return res;
                }


                var personExist = ctx.AG_IAS_PERSONAL_T.Where(w => w.ID == request.userId).FirstOrDefault();
                if (personExist == null)
                {
                    res.ErrorMsg = Resources.errorPersonService_014 + request.oicUserName + Resources.errorPersonService_015;
                   // return res;
                }

                //บันทึกข้อมูลลง HIS
                AG_IAS_HIST_PERSONAL_T HisPerson = new AG_IAS_HIST_PERSONAL_T();
                HisPerson.TRANS_ID = OracleDB.GetGenAutoId();
                HisPerson.PRE_NAME_CODE = personExist.PRE_NAME_CODE;
                HisPerson.NAMES = personExist.NAMES;
                HisPerson.LASTNAME = personExist.LASTNAME;
                HisPerson.SEX = personExist.SEX;
                HisPerson.MEMBER_TYPE = personExist.MEMBER_TYPE;
                HisPerson.IMG_SIGN = personExist.IMG_SIGN;
                ctx.AG_IAS_HIST_PERSONAL_T.AddObject(HisPerson);

                personExist.PRE_NAME_CODE = request.preNameCode;
                personExist.NAMES = request.firstName;
                personExist.LASTNAME = request.lastName;
                personExist.SEX = request.sex;

                if (personExist.MEMBER_TYPE == "5" && request.FileByteStream != null)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        request.FileByteStream.CopyTo(ms);
                       personExist.IMG_SIGN = ms.ToArray();
                    }   

                }             

                ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_UpdateOIC", ex);
            }
        }

        public void InsertOIC(RemoteFileInfoAddOic request)
        {
           // var res = new DTO.ResponseMessage<bool>();
            try
            {
                var ctx = new IASPersonEntities();
                string memberType = "";
                if (request.oicTypeCode == "1")
                {
                    memberType = DTO.RegistrationType.OICAgent.GetEnumValue().ToString();
                }
                if (request.oicTypeCode == "2")
                {
                    memberType = DTO.RegistrationType.OICFinace.GetEnumValue().ToString();
                }
                if (request.oicTypeCode == "0")
                {
                    memberType = DTO.RegistrationType.OIC.GetEnumValue().ToString();
                }

               
                var entExist = ctx.AG_IAS_USERS
                                      .Where(w => w.USER_NAME == request.oicUserName)
                                      .FirstOrDefault();
                if (entExist != null)
                {
                   // res.ErrorMsg = Resources.errorPersonService_012 + oicUserName + Resources.errorPersonService_013;
                   // return res;
                }

                var per = new AG_IAS_PERSONAL_T();

                using (MemoryStream ms = new MemoryStream())
                {
                    request.FileByteStream.CopyTo(ms);
                    per.IMG_SIGN = ms.ToArray();
                }  
                
                per.ID = OracleDB.GetGenAutoId();
                per.EMPLOYEE_NO = request.oicEmpNo;
                per.PRE_NAME_CODE = request.preNameCode;
                per.NAMES = request.firstName;
                per.LASTNAME = request.lastName;
                per.MEMBER_TYPE = memberType;
                per.SEX = request.sex;
                ctx.AG_IAS_PERSONAL_T.AddObject(per);

                var user = new AG_IAS_USERS();
                user.USER_ID = per.ID;
                user.USER_NAME = request.oicUserName;
                user.MEMBER_TYPE = memberType;

                if (request.oicTypeCode == "1")
                {
                    user.USER_TYPE = user.USER_RIGHT = DTO.RegistrationType.OICAgent.GetEnumValue().ToString();
                }
                if (request.oicTypeCode == "2")
                {
                    user.USER_TYPE = user.USER_RIGHT = DTO.RegistrationType.OICFinace.GetEnumValue().ToString();
                }
                if (request.oicTypeCode == "0")
                {
                    user.USER_TYPE = user.USER_RIGHT = DTO.RegistrationType.OIC.GetEnumValue().ToString();
                }
                user.OIC_TYPE = request.oicTypeCode;
                user.OIC_EMP_NO = request.oicEmpNo;
                user.CREATED_BY = user.UPDATED_BY = "AGDOI";
                user.CREATED_DATE = user.UPDATED_DATE = DateTime.Now;
                user.IS_ACTIVE = "A";
                user.IS_APPROVE = "Y";
                ctx.AG_IAS_USERS.AddObject(user);
                ctx.SaveChanges();
                //res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                //res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_InsertOIC", ex);
            }
           // return res;
        }


        public DTO.ResponseMessage<bool> ValidateApplicantSingleBeforeSubmit(List<DTO.ApplicantTemp> app)
        {
            var ctx = new IASPersonEntities();
            var res = new DTO.ResponseMessage<bool>();
            res.ResultMessage = false;

            //IEnumerable<string> appTestingNo = app.Select(tsn => tsn.TESTING_NO);

            try
            {
                app.ForEach(x =>
                {
                    DateTime dtToday = DateTime.Now.Date;

                    var examLicense = ctx.AG_EXAM_LICENSE_R.SingleOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE);


                    var ent1 = ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.APPLY_DATE != x.APPLY_DATE && w.ID_CARD_NO == x.ID_CARD_NO);
                    if (ent1 != null)
                    {
                        res.ResultMessage = true;
                    }

                    var ent2 = from a in ctx.AG_APPLICANT_T
                               join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                               where e.TESTING_DATE == x.TESTING_DATE && e.TEST_TIME_CODE == examLicense.TEST_TIME_CODE
                               && a.APPLY_DATE != x.APPLY_DATE
                               && a.ID_CARD_NO == x.ID_CARD_NO
                               && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                               select a;
                    if (ent2.ToList().Count() > 0)
                    {
                        res.ResultMessage = true;
                    }


                    var ent3 = from a in ctx.AG_APPLICANT_T
                               join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                               where e.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE && e.TEST_TIME_CODE == examLicense.TEST_TIME_CODE
                               && a.APPLY_DATE != x.APPLY_DATE
                               && a.ID_CARD_NO == x.ID_CARD_NO
                               && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                               select a;
                    if (ent3.ToList().Count() > 0)
                    {
                        res.ResultMessage = true;
                    }

                    var ent4 = from a in ctx.AG_APPLICANT_T
                               join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                               where e.TESTING_DATE == x.TESTING_DATE && a.APPLY_DATE == dtToday
                               && a.ID_CARD_NO == x.ID_CARD_NO
                               && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                               select a;
                    if (ent4.ToList().Count > 0)
                    {
                        res.ResultMessage = true;
                    }

                    var ent5 = from a in ctx.AG_APPLICANT_T
                               join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                               where e.TESTING_DATE == x.TESTING_DATE && a.APPLY_DATE != x.TESTING_DATE
                               && a.ID_CARD_NO == x.ID_CARD_NO
                               && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                               select a;
                    if (ent5.ToList().Count > 0)
                    {
                        res.ResultMessage = true;
                    }


                    //var ent6 = from a in ctx.AG_APPLICANT_T
                    //           join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                    //           join etr in ctx.AG_EXAM_TIME_R on e.TEST_TIME_CODE equals etr.TEST_TIME_CODE
                    //           where e.TESTING_DATE == x.TESTING_DATE
                    //           && a.ID_CARD_NO == x.ID_CARD_NO
                    //           select a;
                    //if (ent6.ToList().Count > 0)
                    //{
                    //    ent6.ToList().ForEach(chk =>
                    //    {
                    //        AG_EXAM_TIME_R entTime =ctx.AG_EXAM_TIME_R.FirstOrDefault(s => s.TEST_TIME_CODE == examLicense.TEST_TIME_CODE);
                    //        if (entTime != null)
                    //        {
                    //            int startTime1 =Convert.ToInt32(entTime.START_TIME);
                    //             int endTime1 =Convert.ToInt32(entTime.END_TIME);
                    //            int startTime2 = Convert.ToInt32(examLicense
                    //             if (startTime)
                    //            {

                    //            }
                    //        }
                    //        //apply = chk.EXAM_APPLY == null ? "0".ToShort() : chk.EXAM_APPLY.Value;
                    //        //admission = entSeat.SEAT_AMOUNT == null ? "0".ToShort() : entSeat.SEAT_AMOUNT.Value;

                    //        //int remain = admission - apply;
                    //        //lsRemain.Add(remain);
                    //    });
                    //}


                });

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_ValidateApplicantSingleBeforeSubmit", ex);
            }
            
            return res;

        }

        public DTO.ResponseService<DTO.SummaryReceiveApplicant>
            InsertAndCheckApplicantGroupUpload(DTO.UploadData data, string fileName,
                                               DTO.RegistrationType regType,
                                               string testingNo, string examPlaceCode, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseService<DTO.SummaryReceiveApplicant>();
            var ctx = new IASPersonEntities();
            res.DataResponse = new DTO.SummaryReceiveApplicant();

            //res.DataResponse.Header = new List<DTO.UploadHeader>();
            //res.DataResponse.Detail = new List<DTO.ApplicantTemp>();



            try
            {
                DTO.ApplicantUploadRequest request = new DTO.ApplicantUploadRequest()
                {
                    FileName = fileName,
                    TestingNo = testingNo,
                    UploadData = data,
                    UserProfile = userProfile,
                    ExamPlaceCode = examPlaceCode
                };
                IAS.DAL.Interfaces.IIASPersonEntities ctx2 = DAL.DALFactory.GetPersonContext();
                DTO.ResponseService<ApplicantFileHeader> response = ApplicantFileFactory.ConcreateApplicantFileRequest(ctx2, request);

                if (response.IsError)
                {
                    res.ErrorMsg = response.ErrorMsg;
                    LoggerFactory.CreateLog().Fatal("ApplicantService_InsertAndCheckApplicantGroupUpload", response.ErrorMsg);
                }
                if (response.DataResponse == null)
                {
                    res.ErrorMsg = Resources.errorApplicantFileHeader_001;
                }

                AG_IAS_APPLICANT_HEADER_TEMP applicantHeadTemp = new AG_IAS_APPLICANT_HEADER_TEMP();
                response.DataResponse.MappingToEntity<ApplicantFileHeader, AG_IAS_APPLICANT_HEADER_TEMP>(applicantHeadTemp);
                ctx.AG_IAS_APPLICANT_HEADER_TEMP.AddObject(applicantHeadTemp);

                var examLicense = ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == request.TestingNo).FirstOrDefault();
                foreach (ApplicantFileDetail detail in response.DataResponse.ApplicantFileDetails)
                {
                    AG_IAS_APPLICANT_DETAIL_TEMP detailTemp = new AG_IAS_APPLICANT_DETAIL_TEMP();
                    detail.EXAM_PLACE_CODE = examLicense.EXAM_PLACE_CODE;
                    detail.MappingToEntity<ApplicantFileDetail, AG_IAS_APPLICANT_DETAIL_TEMP>(detailTemp);
                    ctx.AG_IAS_APPLICANT_DETAIL_TEMP.AddObject(detailTemp);
                }
                using (TransactionScope ts = new TransactionScope())
                {
                   ctx.SaveChanges();
                    ts.Complete();
                }

                DTO.SummaryReceiveApplicant summarize = response.DataResponse.ValidateDataOfData();
                res.DataResponse = summarize;

             

            }
            catch (Exception)
            {
                res.ErrorMsg = Resources.errorApplicantFileHeader_001;
                LoggerFactory.CreateLog().Fatal("ApplicantService_InsertAndCheckApplicantGroupUpload", res.ErrorMsg);
            }

            return res;
        }
       

    }
}

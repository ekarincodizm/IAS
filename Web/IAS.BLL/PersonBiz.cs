using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using IAS.BLL.Properties;

namespace IAS.BLL
{
    public class PersonBiz : IDisposable
    {

        private PersonService.PersonServiceClient svc = null;

        public PersonBiz()
        {
            svc = new PersonService.PersonServiceClient();
        }

        public DTO.ResponseService<DTO.Person> GetUserProfile(string Id, string memType)
        {
            return svc.GetUserProfile(Id, memType);
        }

        public DTO.ResponseService<DTO.UserProfile> RefeshUserProfileStatus(DTO.UserProfile userProfile)
        {
            DTO.ResponseService<DTO.UserProfile> response = new DTO.ResponseService<DTO.UserProfile>();

            if (userProfile == null)
            {
                response.ErrorMsg = Resources.errorPersonBiz_001;
                return response;
            }

            var res = svc.GetUserProfile(userProfile.Id, userProfile.MemberType.ToString());
            if (res.IsError)
            {
                response.ErrorMsg = res.ErrorMsg;
                return response;
            }

            userProfile.STATUS = res.DataResponse.STATUS;

            response.DataResponse = userProfile;
            return response;

        }

        public DTO.ResponseService<DTO.Person> GetById(string Id)
        {

            DTO.ResponseService<DTO.Person> res = svc.GetById(Id);
            //if( !res.IsError && svc.GetById(Id).DataResponse != null)
            //{
            //    DTO.ResponseService<DTO.SignatureImg> resImg = svc.GetOicPersonSignImg(Id);  

            //    if((!resImg.IsError) && resImg.DataResponse !=null)
            //    {
            //        byte[] result = ByteArrayHelper.ConvertStringToByte(resImg.DataResponse.Signture);
            //        res.DataResponse.IMG_SIGN = result;
            //    } 
            //}

            return res;
        }

        /// <summary>
        /// ดึงข้อมูล ลายเซ็น
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DTO.ResponseService<DTO.SignatureImg> GetOicPersonSignImg(string id)
        {

            return svc.GetOicPersonSignImg(id); ;
        }

        public DTO.ResponseService<DataSet> GetDataTo8Report(string ID, string license_code)
        {
            return svc.GetDataTo8Report(ID, license_code);
        }

        public DTO.ResponseService<DataSet> GetDataRenewReport(string Id, string license_code, string license_no)
        {
            return svc.GetDataRenewReport(Id,license_code,license_no);
        }




        public DTO.ResponseService<DTO.Person> GetUserProfileById(string Id)
        {
            return svc.GetUserProfileById(Id);
        }

        public DTO.ResponseService<DTO.PersonTemp> GetPersonTemp(string Id)
        {
            return svc.GetPersonTemp(Id);
        }

        public DTO.ResponseMessage<bool> SetPersonTemp(DTO.PersonTemp tmpPerson, DTO.PersonAttatchFile[] tmpFiles)
        {
            return svc.SetPersonTemp(tmpPerson, tmpFiles);
        }

        public DTO.ResponseMessage<bool> EditPerson(DTO.PersonTemp tmpPerson)
        {
            return svc.EditPerson(tmpPerson);
        }

        public DTO.ResponseService<DTO.PersonAttatchFile[]> GetUserProfileAttatchFileByPersonId(string personId)
        {
            return svc.GetUserProfileAttatchFileByPersonId(personId);
        }

        public DTO.ResponseService<DTO.PersonAttatchFile[]> GetAttatchFileByPersonId(string personId)
        {
            return svc.GetAttatchFileByPersonId(personId);
        }
        public DTO.ResponseService<DTO.PersonAttatchFile[]> GetAttatchFileEditByPersonId(string personId)
        {
            IList<DTO.PersonAttatchFile> attachFiles = svc.GetAttatchFileByPersonId(personId).DataResponse.ToList();
            IList<DTO.PersonAttatchFile> tempAttachFiles = svc.GetTempAttatchFileByPersonId(personId).DataResponse.ToList();
            foreach (DTO.PersonAttatchFile item in tempAttachFiles)
            {
                attachFiles.Add(item);
            }
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = new DTO.ResponseService<DTO.PersonAttatchFile[]>();
            res.DataResponse = attachFiles.ToArray();
            return res;
        }
        public DTO.ResponseMessage<bool> PersonApprove(List<string> listId)
        {
            return svc.PersonApprove(listId.ToArray());
        }



        public DTO.ResponseMessage<bool> PersonEditApprove(List<string> listId, string appresult, string userid)
        {
            return svc.PersonEditApprove(listId.ToArray(), appresult, userid);
        }


        public DTO.ResponseMessage<bool> PersonNotApprove(List<string> listId)
        {
            return svc.PersonNotApprove(listId.ToArray());
        }

        public DTO.ResponseService<DTO.UserProfile> UserAuthen(string userName, string password, bool IsOIC, string Ip)
        {
            return svc.Authentication(userName, password, IsOIC, Ip);
        }

        /// <summary>
        /// ดึงข้อมูลสถิติการ Reset Password ตามเงื่อนไข
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
                GetStatisticResetPassword(string idCard,
                                          string firstName, string lastName)
        {
            return svc.GetStatisticResetPassword(idCard, firstName, lastName);
        }

        /// <summary>
        /// เพิ่มข้อมูล User ที่เป็น คปภ.
        /// </summary>
        /// <param name="employeeNo">รหัสพนักงาน คปภ.</param>
        /// <param name="preNameCode">รหัสคำนำ</param>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <param name="sex">เพศ M=ชาย, F=หญิง</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> InsertOIC(string oicEmpNo, string oicUserName, string preNameCode,
                                                   string firstName, string lastName,
                                                   string sex, string oicTypeCode, byte[] sign)
        {
            //ไม่ต้องเซ็ค รหัส AD ของเจ้าหน้าที่ คปภ.
            if (svc.IsRightUserOIC(oicUserName).ResultMessage)
            {
                // if(true)
                if (oicTypeCode == "2")
                {
                    DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
                    FileManagement.FileServiceClient svc_file = new FileManagement.FileServiceClient();
                    Stream stream = new MemoryStream(sign);
                    try
                    {
                        svc_file.InsertOIC(firstName, lastName, oicEmpNo, oicTypeCode, oicUserName, preNameCode, sex, stream);
                    }
                    catch (Exception ex)
                    {
                        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                    }
                    return res;
                }
                else
                {
                    return svc.InsertOIC(oicEmpNo, oicUserName, preNameCode, firstName, lastName, sex, oicTypeCode, sign);
                }
            }
            else
            {
                DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
                res.ResultMessage = false;
                res.ErrorMsg = Resources.errorPersonBiz_002 + oicUserName + Resources.errorPersonBiz_003;
                return res;
            }
        }


        public DTO.ResponseMessage<bool> IsRightUserOIC(string employeeNo)
        {
            return svc.IsRightUserOIC(employeeNo);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// ดึงข้อมูลจาก username และ email
        /// </summary>
        /// <param name="userName">ชื่อผู้ใช้ระบบ</param>
        /// <param name="email">อีเมล</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetUserProfileByUsername(string userName, string email)
        {
            return svc.GetUserProfileByUsername(userName, email);
        }
        //milk
        public Boolean ChangePasswordTime(string userName)
        {
            return svc.ChangePasswordTime(userName);
        }

        public DTO.ResponseMessage<Boolean> ForgetPasswordRequest(String username, String email)
        {
            return svc.ForgetPasswordRequest(username.Trim(), email.Trim());
        }
        //milk
        public DTO.ResponseService<DataSet> GetPersonByCriteria(string firstName, string lastName,
                                                                DateTime? startDate, DateTime? toDate,
                                                                  string idCard, string memberTypeCode,
                                                                  string email, string compCode,
                                                                  string status, int pageNo, int recordPerPage, string para)
        {
            DTO.ResponseService<DataSet> res = svc.GetPersonByCriteria(firstName, lastName,
                                                                        startDate, toDate,
                                                                        idCard, memberTypeCode,
                                                                              email, compCode,
                                                                              status, pageNo, recordPerPage, para);
            return res;
        }

        public DTO.ResponseService<DataSet> GetPersonTempEditByCriteria(string firstName, string lastName,
                                                                 DateTime? startDate, DateTime? toDate,
                                                                 string idCard, string memberTypeCode,
                                                                 string email, string compCode,
                                                                 string status, int pageNo, int recordPerPage, string para)
        {
            DTO.ResponseService<DataSet> res = svc.GetPersonTempEditByCriteria(firstName, lastName,
                                                                              startDate, toDate,
                                                                              idCard, memberTypeCode,
                                                                              email, compCode,
                                                                              status, pageNo, recordPerPage, para);
            return res;
        }


        public DTO.ResponseMessage<bool> UpdateOIC(string userId, string oicUserName, string preNameCode,
                                                   string firstName, string lastName,
                                                   string sex, string memberType, byte[] imgSign)
        {

            if (memberType == "5")
            {
                DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
                FileManagement.RemoteFileInfo remote = new FileManagement.RemoteFileInfo();
                FileManagement.FileServiceClient svc_file = new FileManagement.FileServiceClient();
                Stream stream = new MemoryStream(imgSign);
                remote.FileByteStream = stream;
                remote.firstName = firstName;
                remote.lastName = lastName;
                remote.memberType = memberType;
                remote.oicUserName = oicUserName;
                remote.preNameCode = preNameCode;
                remote.sex = sex;
                remote.userId = userId;

                svc_file.UpdateOIC(firstName, lastName, memberType, oicUserName, preNameCode, sex, userId, stream);
                return res;
            }
            else
            {
                return svc.UpdateOIC(userId, oicUserName, preNameCode, firstName, lastName, sex, memberType, imgSign);
            }
        }
        public DTO.ResponseService<DataSet> GetPersonByCriteriaAtPage(string firstName, string lastName,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage)
        {
            return svc.GetPersonByCriteriaAtPage(firstName, lastName,
                                                                        IdCard, memberTypeCode,
                                                                        email, compCode,
                                                                        status, pageNo, recordPerPage);
        }


        public DTO.ResponseMessage<Boolean> RenewPassword(String username, String email, String oldpassword, String newpassword)
        {
            return svc.RenewPassword(username, email, oldpassword, newpassword);
        }

        public DTO.ResponseService<DTO.Registration> getPDetailByIDCard(string idCard)
        {
            return svc.getPDetailByIDCard(idCard);
        }

        public DTO.ResponseMessage<bool> SetOffLineStatus(string userName)
        {
            return svc.SetOffLineStatus(userName);
        }

        public DTO.ResponseMessage<bool> SetOffLineAllStatus(string userName)
        {
            return svc.SetOffLineAllStatus(userName);
        }
        public DTO.ResponseService<DataSet> GetOnLineUser(string userName)
        {
            return svc.GetOnLineUser(userName);
        }

        public DTO.ResponseService<DTO.Person> GetPersonalDetail(string idCard)
        {
            return svc.GetPersonalDetail(idCard);
        }

        public DTO.ResponseMessage<bool> CheckAuthorityEditExam(DTO.UserProfile userProfile, string testingNo, string testingDate)
        {
            return svc.CheckAuthorityEditExam(userProfile, testingNo, testingDate);
        }

        public List<string> GetEmailMoveExam(string testingNo) 
        {
            return svc.GetEmailMoveExam(testingNo).ToList();
        }

        #region OIC AD Service
        /// <summary>
        /// OIC AD Service : dont modify
        /// </summary>
        /// <param name="ADUserName"></param>
        /// <param name="ADPassword"></param>
        /// <returns>DTO.ResponseService<DTO.OICADProperties></returns>
        /// <AUTHOR>NATTA</AUTHOR>
        public DTO.ResponseService<DTO.OICADProperties> OICAuthenWithADService(string ADUserName, string ADPassword)
        {
            return svc.OICAuthenWithADService(ADUserName, ADPassword);
        }
        #endregion

    }
}

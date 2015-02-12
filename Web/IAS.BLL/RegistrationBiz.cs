using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using IAS.DTO.FileService;
using System.Configuration;
using IAS.BLL.Properties;
using IAS.DTO;


namespace IAS.BLL
{
    public class RegistrationBiz : IDisposable
    {
        protected String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString(); 

        private RegistrationService.RegistrationServiceClient svc;

        public RegistrationBiz()
        {
            svc = new RegistrationService.RegistrationServiceClient();
        }

        public DTO.ResponseService<DTO.Registration> GetById(string Id)
        {
            DTO.ResponseService<DTO.Registration> res = svc.GetById(Id);
            return res;
        }

        public DTO.ResponseService<DTO.Registration> Insert(DTO.Registration ent,
                                                            DTO.RegistrationType registrationType)
        {
            DTO.ResponseService<DTO.Registration> res = svc.Insert(ent, registrationType);
            return res;
        }

        public DTO.ResponseService<DTO.Registration> InsertWithAttatchFile(DTO.RegistrationType regType,
                                                                            DTO.Registration ent,
                                                                            List<DTO.RegistrationAttatchFile> listAttatchFile)
        {
            
            if (ent.REG_PASS == null)
                throw new ApplicationException(Resources.errorRegistrationBiz_001);

            String pass = Utils.EncryptSHA256.Encrypt(ent.REG_PASS);
            ent.REG_PASS = pass;

            DTO.ResponseService<DTO.Registration> res = svc.InsertWithAttatchFile(regType, ent, listAttatchFile.ToArray());
          

            return res;
        }

        public DTO.ResponseService<DTO.Registration> Update(DTO.Registration ent)
        {
            DTO.ResponseService<DTO.Registration> res = svc.Update(ent);
            return res;
        }
        public DTO.ResponseService<DTO.Registration> Update(DTO.Registration ent, List<DTO.RegistrationAttatchFile> attachFiles)
        {
            DTO.ResponseService<DTO.Registration> res = svc.UpdateWithAttachFiles(ent, attachFiles.ToArray());
            return res; 
        }
        public DTO.ResponseService<DTO.RegistrationAttatchFile> UpdateAttachFile(DTO.RegistrationAttatchFile entity)
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> res = svc.UpdateAttachFile(entity);
            return res;
        }

        public DTO.ResponseService<DTO.RegistrationAttatchFile> DeleteAttatchFile(string Id)
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> res = svc.DeleteAttatchFile(Id);
            if (!res.IsError) {
                using (FileService.FileTransferServiceClient svcFile = new FileService.FileTransferServiceClient())
                {
                    //DTO.FileService.DeleteFileResponse resDelete = svcFile.DeleteFile(new DTO.FileService.DeleteFileRequest() { TargetFileName = res.DataResponse.ATTACH_FILE_PATH });
                    //if (resDelete.Code != "0000")
                    //    throw new ApplicationException(resDelete.Message);
                }
            }
           
            return res;
        }

        public DTO.ResponseService<DTO.RegistrationAttatchFile> GetAttatchFileById(string Id)
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> res = svc.GetAttatchFileById(Id);
            return res;
        }

        public DTO.ResponseService<DTO.RegistrationAttatchFile[]> GetAttatchFilesByRegisterationID(string registerationId)  
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile[]> res = svc.GetAttatchFilesByRegisterationNo(registerationId);
            return res;
        }

        public DTO.ResponseService<DataSet> GetRegistrationsByCriteria(string firstName, string lastName,
                                                                       DateTime? startDate, DateTime? toDate,
                                                                       string idCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage, string para)
        {
            DTO.GetReistrationByCriteriaRequest request = new DTO.GetReistrationByCriteriaRequest(){
                FirstName = firstName, 
                LastName = lastName,
                StartDate = startDate,
                ToDate = toDate,
                IdCard = idCard,
                MemberTypeCode = memberTypeCode,
                Email = email,
                CompCode = compCode,
                Status = status,
                PageNo = pageNo,
                RecordPerPage = recordPerPage,
                Para = para
            };
            DTO.ResponseService<DataSet> res = svc.GetRegistrationsByCriteria(request );
            return res;
        }
        public DTO.ResponseService<DTO.PagingResponse<DataSet>> GetRegistrationsByCriteriaAtPage(string firstName, string lastName,
                                                                      string idCard, string memberTypeCode,
                                                                      string email, string compCode,
                                                                      string status, Int32 indexPage, Int32 pageSize)
        {
            DTO.ResponseService<DTO.PagingResponse<DataSet>> res = svc.GetRegistrationsByCriteriaAtPage(firstName, lastName,
                                                                              idCard, memberTypeCode,
                                                                              email, compCode,
                                                                              status, indexPage, pageSize);
            return res;
        }
        public DTO.ResponseService<DTO.Registration> GetByIdCard(string idCard)
        {
            DTO.ResponseService<DTO.Registration> res = svc.GetByIdCard(idCard);
            return res;
        }
        public DTO.ResponseService<DTO.Registration> GetByEmail(string idCard) 
        {
            DTO.ResponseService<DTO.Registration> res = svc.GetByIdCard(idCard);
            return res;
        }
        public DTO.ResponseService<DTO.Registration> GetByFirstLastName(string firstName, string lastName)
        {
            DTO.GetByFirstLastNameRequest request = new DTO.GetByFirstLastNameRequest() { FirstName = firstName, LastName = lastName };
            DTO.ResponseService<DTO.Registration> res = svc.GetByFirstLastName(request);
            return res;
        }

        public DTO.ResponseMessage<bool> RegistrationApprove(List<string> listId, string approveresult,string userid, string memType)
        {
            DTO.RegistrationApproveRequest request = new DTO.RegistrationApproveRequest() { MemberType = memType, UserId = userid, AppreResult = approveresult, ListId = listId };
            return svc.RegistrationApprove(request);
        }

        //Addedby Nattapong @26/8/2556
        public DTO.ResponseMessage<bool> RegistrationNotApprove(List<string> listId, string approveresult,string userid)
        {
            DTO.RegistrationNotApproveRequest request = new DTO.RegistrationNotApproveRequest() { AppreResult = approveresult, UserId = userid, ListId = listId };
            return svc.RegistrationNotApprove(request);
        }

        public DTO.ResponseService<DTO.RegistrationAttatchFile> InsertAttachFileToRegistration
                    (DTO.Registration registration, DTO.RegistrationAttatchFile registrationAttachFile) 
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> response = svc.InsertAttachFileToRegistration(registration.ID, registrationAttachFile);

            return response;
        }

        public DTO.ResponseMessage<bool> SendEmail(string msg)
        {
            return null;
        }

        public DTO.ResponseService<DTO.Registration> IsGeneralUserRegistered(string idCard)
        {
            return svc.IsGeneralUserRegistered(idCard);
        }

        public DTO.ResponseService<DTO.Registration> IsCompAssoUserRegistered(string email, string name, string lastName, string compCode)
        {
            return svc.IsCompAssoUserRegistered(email, name, lastName, compCode);
        }

        public DTO.ResponseMessage<bool> IsGeneralUserRegistrationNotApprove(string idCard)
        {
            return null;
        }

        public DTO.ResponseMessage<bool> IsCompAssoUserRegistrationNotApprove(string email)
        {
            return null;
        }

        public DTO.ResponseMessage<bool> VerifyIdCard(string idCard)
        {
            return svc.VerifyIdCard(idCard);
        }

        public DTO.ResponseMessage<bool> ValidateBeforeSubmit(DTO.RegistrationType registerType,
                                                      DTO.Registration entity)
        {
            return svc.ValidateBeforeSubmit(registerType, entity);
        }

        public void Dispose()
        {
            if (svc != null) svc.Close();
            GC.SuppressFinalize(this);
        }

        private String GetExtensionFile(String fileName)
        {
            String[] files = fileName.Split('.');
            return files[files.Length - 1];
        }

        #region New Regis Func
        public DTO.ResponseMessage<bool> EntityValidation(DTO.RegistrationType registerType, DTO.Registration entity)
        {
            return svc.EntityValidation(registerType, entity);
        }

        public DTO.ResponseService<DTO.AgentTypeEntity[]> GetAgentType(string agentType)
        {
            return svc.GetAgentType(agentType);
            //DTO.ResponseService<DTO.AgentTypeEntity[]> res = svc.GetAgentType();
            //return res;
        }

        public DTO.ResponseService<DTO.Person> GetPersonalDetailByIDCard(string idCard)
        {
            return svc.GetPersonalDetailByIDCard(idCard);

        }

        public DTO.ResponseService<DataSet> GetLicenseReport(string LicenseTypeCode, string CompCode, string StartDate, string EndDate)
        {
            return svc.GetLicenseReport(LicenseTypeCode, CompCode, StartDate, EndDate);
        }


        
        #endregion

       
    }
}

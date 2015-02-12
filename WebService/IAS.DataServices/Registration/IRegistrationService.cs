using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IAS.DTO;
using IAS.DAL;
using System.Data;

namespace IAS.DataServices.Registration
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        DTO.ResponseService<DTO.Registration> Insert(DTO.Registration entity,
                                                     DTO.RegistrationType registerType);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> InsertWithAttatchFile(DTO.RegistrationType registerType, 
                                                                    DTO.Registration entity, 
                                                                    List<DTO.RegistrationAttatchFile> listAttatchFile);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> Update(DTO.Registration entity);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> UpdateWithAttachFiles(DTO.Registration entity,  List<DTO.RegistrationAttatchFile> listAttatchFile);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> Delete(string Id);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> GetById(string Id);

        [OperationContract]
        DTO.ResponseService<DTO.RegistrationAttatchFile> UpdateAttachFile(DTO.RegistrationAttatchFile entity);

        [OperationContract]
        DTO.ResponseService<DTO.RegistrationAttatchFile> DeleteAttatchFile(string Id);

        [OperationContract]
        DTO.ResponseService<DTO.RegistrationAttatchFile> GetAttatchFileById(string Id);

        [OperationContract]
        DTO.ResponseService<IList<DTO.RegistrationAttatchFile>> GetAttatchFilesByRegisterationNo(string registerationNo); 

        [OperationContract]
        DTO.ResponseService<DataSet> GetRegistrationsByCriteria( DTO.GetReistrationByCriteriaRequest request);

     

        [OperationContract]
        DTO.ResponseService<DTO.Registration> GetByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> GetByFirstLastName(DTO.GetByFirstLastNameRequest request);

        [OperationContract]
        DTO.ResponseMessage<bool> RegistrationApprove(DTO.RegistrationApproveRequest request);// List<string> listId, string appresult,string userid, string memType);

        [OperationContract]
        DTO.ResponseMessage<bool> RegistrationNotApprove(DTO.RegistrationNotApproveRequest request);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> IsGeneralUserRegistered(string idCard);

        [OperationContract]
        DTO.ResponseService<DTO.Registration> IsCompAssoUserRegistered(string email, string name, string lastName, string compCode);

        [OperationContract]
        DTO.ResponseMessage<bool> VerifyIdCard(string idCard);

        [OperationContract]
        DTO.ResponseMessage<bool> ValidateBeforeSubmit(DTO.RegistrationType registerType,
                                                      DTO.Registration entity);

        [OperationContract]
        DTO.ResponseService<DTO.RegistrationAttatchFile> InsertAttachFileToRegistration(string registrationId, DTO.RegistrationAttatchFile attachFile);


        [OperationContract]
        DTO.ResponseService<DTO.PagingResponse<DataSet>> GetRegistrationsByCriteriaAtPage(string firstName, string lastName,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, Int32 pageIndex, Int32 pageSize);

        #region New Regis Func
        [OperationContract]
        DTO.ResponseMessage<bool> EntityValidation(DTO.RegistrationType registerType, DTO.Registration entity);

        [OperationContract]
        DTO.ResponseService<List<DTO.AgentTypeEntity>> GetAgentType(string agentType);

        [OperationContract]
        DTO.ResponseService<DTO.Person> GetPersonalDetailByIDCard(string idCard);

        #endregion

        #region
        [OperationContract]
        DTO.ResponseService<DataSet> ReportRegisterLicense(string licensetype, string comcode, string startdate, string enddate);


        [OperationContract]
        DTO.ResponseService<DataSet> GetLicenseReport(string LicenseTypeCode, string CompCode, string StartDate, string EndDate);


      
        [OperationContract]
        DTO.ResponseService<DataSet> ReportRegisterLicenseStaticRatio(string lincensetype, string startdateone, string enddateone, string startdatetwo, string enddatetwo);
        
        
        #endregion
    }
}

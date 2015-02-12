﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IAS.RegistrationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RegistrationService.IRegistrationService")]
    public interface IRegistrationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/Insert", ReplyAction="http://tempuri.org/IRegistrationService/InsertResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> Insert(IAS.DTO.Registration entity, IAS.DTO.RegistrationType registerType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/InsertWithAttatchFile", ReplyAction="http://tempuri.org/IRegistrationService/InsertWithAttatchFileResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> InsertWithAttatchFile(IAS.DTO.RegistrationType registerType, IAS.DTO.Registration entity, IAS.DTO.RegistrationAttatchFile[] listAttatchFile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/Update", ReplyAction="http://tempuri.org/IRegistrationService/UpdateResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> Update(IAS.DTO.Registration entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/UpdateWithAttachFiles", ReplyAction="http://tempuri.org/IRegistrationService/UpdateWithAttachFilesResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> UpdateWithAttachFiles(IAS.DTO.Registration entity, IAS.DTO.RegistrationAttatchFile[] listAttatchFile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/Delete", ReplyAction="http://tempuri.org/IRegistrationService/DeleteResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> Delete(string Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetById", ReplyAction="http://tempuri.org/IRegistrationService/GetByIdResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> GetById(string Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/UpdateAttachFile", ReplyAction="http://tempuri.org/IRegistrationService/UpdateAttachFileResponse")]
        IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> UpdateAttachFile(IAS.DTO.RegistrationAttatchFile entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/DeleteAttatchFile", ReplyAction="http://tempuri.org/IRegistrationService/DeleteAttatchFileResponse")]
        IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> DeleteAttatchFile(string Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetAttatchFileById", ReplyAction="http://tempuri.org/IRegistrationService/GetAttatchFileByIdResponse")]
        IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> GetAttatchFileById(string Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetAttatchFilesByRegisterationNo", ReplyAction="http://tempuri.org/IRegistrationService/GetAttatchFilesByRegisterationNoResponse")]
        IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile[]> GetAttatchFilesByRegisterationNo(string registerationNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetRegistrationsByCriteria", ReplyAction="http://tempuri.org/IRegistrationService/GetRegistrationsByCriteriaResponse")]
        IAS.DTO.ResponseService<System.Data.DataSet> GetRegistrationsByCriteria(IAS.DTO.GetReistrationByCriteriaRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetByIdCard", ReplyAction="http://tempuri.org/IRegistrationService/GetByIdCardResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> GetByIdCard(string idCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetByFirstLastName", ReplyAction="http://tempuri.org/IRegistrationService/GetByFirstLastNameResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> GetByFirstLastName(IAS.DTO.GetByFirstLastNameRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/RegistrationApprove", ReplyAction="http://tempuri.org/IRegistrationService/RegistrationApproveResponse")]
        IAS.DTO.ResponseMessage<bool> RegistrationApprove(IAS.DTO.RegistrationApproveRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/RegistrationNotApprove", ReplyAction="http://tempuri.org/IRegistrationService/RegistrationNotApproveResponse")]
        IAS.DTO.ResponseMessage<bool> RegistrationNotApprove(IAS.DTO.RegistrationNotApproveRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/IsGeneralUserRegistered", ReplyAction="http://tempuri.org/IRegistrationService/IsGeneralUserRegisteredResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> IsGeneralUserRegistered(string idCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/IsCompAssoUserRegistered", ReplyAction="http://tempuri.org/IRegistrationService/IsCompAssoUserRegisteredResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Registration> IsCompAssoUserRegistered(string email, string name, string lastName, string compCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/VerifyIdCard", ReplyAction="http://tempuri.org/IRegistrationService/VerifyIdCardResponse")]
        IAS.DTO.ResponseMessage<bool> VerifyIdCard(string idCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/ValidateBeforeSubmit", ReplyAction="http://tempuri.org/IRegistrationService/ValidateBeforeSubmitResponse")]
        IAS.DTO.ResponseMessage<bool> ValidateBeforeSubmit(IAS.DTO.RegistrationType registerType, IAS.DTO.Registration entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/InsertAttachFileToRegistration", ReplyAction="http://tempuri.org/IRegistrationService/InsertAttachFileToRegistrationResponse")]
        IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> InsertAttachFileToRegistration(string registrationId, IAS.DTO.RegistrationAttatchFile attachFile);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetRegistrationsByCriteriaAtPage", ReplyAction="http://tempuri.org/IRegistrationService/GetRegistrationsByCriteriaAtPageResponse")]
        IAS.DTO.ResponseService<IAS.DTO.PagingResponse<System.Data.DataSet>> GetRegistrationsByCriteriaAtPage(string firstName, string lastName, string IdCard, string memberTypeCode, string email, string compCode, string status, int pageIndex, int pageSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/EntityValidation", ReplyAction="http://tempuri.org/IRegistrationService/EntityValidationResponse")]
        IAS.DTO.ResponseMessage<bool> EntityValidation(IAS.DTO.RegistrationType registerType, IAS.DTO.Registration entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetAgentType", ReplyAction="http://tempuri.org/IRegistrationService/GetAgentTypeResponse")]
        IAS.DTO.ResponseService<IAS.DTO.AgentTypeEntity[]> GetAgentType(string agentType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetPersonalDetailByIDCard", ReplyAction="http://tempuri.org/IRegistrationService/GetPersonalDetailByIDCardResponse")]
        IAS.DTO.ResponseService<IAS.DTO.Person> GetPersonalDetailByIDCard(string idCard);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/ReportRegisterLicense", ReplyAction="http://tempuri.org/IRegistrationService/ReportRegisterLicenseResponse")]
        IAS.DTO.ResponseService<System.Data.DataSet> ReportRegisterLicense(string licensetype, string comcode, string startdate, string enddate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistrationService/GetLicenseReport", ReplyAction="http://tempuri.org/IRegistrationService/GetLicenseReportResponse")]
        IAS.DTO.ResponseService<System.Data.DataSet> GetLicenseReport(string LicenseTypeCode, string CompCode, string StartDate, string EndDate);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRegistrationServiceChannel : IAS.RegistrationService.IRegistrationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RegistrationServiceClient : System.ServiceModel.ClientBase<IAS.RegistrationService.IRegistrationService>, IAS.RegistrationService.IRegistrationService {
        
        public RegistrationServiceClient() {
        }
        
        public RegistrationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RegistrationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RegistrationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RegistrationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> Insert(IAS.DTO.Registration entity, IAS.DTO.RegistrationType registerType) {
            return base.Channel.Insert(entity, registerType);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> InsertWithAttatchFile(IAS.DTO.RegistrationType registerType, IAS.DTO.Registration entity, IAS.DTO.RegistrationAttatchFile[] listAttatchFile) {
            return base.Channel.InsertWithAttatchFile(registerType, entity, listAttatchFile);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> Update(IAS.DTO.Registration entity) {
            return base.Channel.Update(entity);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> UpdateWithAttachFiles(IAS.DTO.Registration entity, IAS.DTO.RegistrationAttatchFile[] listAttatchFile) {
            return base.Channel.UpdateWithAttachFiles(entity, listAttatchFile);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> Delete(string Id) {
            return base.Channel.Delete(Id);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> GetById(string Id) {
            return base.Channel.GetById(Id);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> UpdateAttachFile(IAS.DTO.RegistrationAttatchFile entity) {
            return base.Channel.UpdateAttachFile(entity);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> DeleteAttatchFile(string Id) {
            return base.Channel.DeleteAttatchFile(Id);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> GetAttatchFileById(string Id) {
            return base.Channel.GetAttatchFileById(Id);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile[]> GetAttatchFilesByRegisterationNo(string registerationNo) {
            return base.Channel.GetAttatchFilesByRegisterationNo(registerationNo);
        }
        
        public IAS.DTO.ResponseService<System.Data.DataSet> GetRegistrationsByCriteria(IAS.DTO.GetReistrationByCriteriaRequest request) {
            return base.Channel.GetRegistrationsByCriteria(request);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> GetByIdCard(string idCard) {
            return base.Channel.GetByIdCard(idCard);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> GetByFirstLastName(IAS.DTO.GetByFirstLastNameRequest request) {
            return base.Channel.GetByFirstLastName(request);
        }
        
        public IAS.DTO.ResponseMessage<bool> RegistrationApprove(IAS.DTO.RegistrationApproveRequest request) {
            return base.Channel.RegistrationApprove(request);
        }
        
        public IAS.DTO.ResponseMessage<bool> RegistrationNotApprove(IAS.DTO.RegistrationNotApproveRequest request) {
            return base.Channel.RegistrationNotApprove(request);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> IsGeneralUserRegistered(string idCard) {
            return base.Channel.IsGeneralUserRegistered(idCard);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Registration> IsCompAssoUserRegistered(string email, string name, string lastName, string compCode) {
            return base.Channel.IsCompAssoUserRegistered(email, name, lastName, compCode);
        }
        
        public IAS.DTO.ResponseMessage<bool> VerifyIdCard(string idCard) {
            return base.Channel.VerifyIdCard(idCard);
        }
        
        public IAS.DTO.ResponseMessage<bool> ValidateBeforeSubmit(IAS.DTO.RegistrationType registerType, IAS.DTO.Registration entity) {
            return base.Channel.ValidateBeforeSubmit(registerType, entity);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.RegistrationAttatchFile> InsertAttachFileToRegistration(string registrationId, IAS.DTO.RegistrationAttatchFile attachFile) {
            return base.Channel.InsertAttachFileToRegistration(registrationId, attachFile);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.PagingResponse<System.Data.DataSet>> GetRegistrationsByCriteriaAtPage(string firstName, string lastName, string IdCard, string memberTypeCode, string email, string compCode, string status, int pageIndex, int pageSize) {
            return base.Channel.GetRegistrationsByCriteriaAtPage(firstName, lastName, IdCard, memberTypeCode, email, compCode, status, pageIndex, pageSize);
        }
        
        public IAS.DTO.ResponseMessage<bool> EntityValidation(IAS.DTO.RegistrationType registerType, IAS.DTO.Registration entity) {
            return base.Channel.EntityValidation(registerType, entity);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.AgentTypeEntity[]> GetAgentType(string agentType) {
            return base.Channel.GetAgentType(agentType);
        }
        
        public IAS.DTO.ResponseService<IAS.DTO.Person> GetPersonalDetailByIDCard(string idCard) {
            return base.Channel.GetPersonalDetailByIDCard(idCard);
        }
        
        public IAS.DTO.ResponseService<System.Data.DataSet> ReportRegisterLicense(string licensetype, string comcode, string startdate, string enddate) {
            return base.Channel.ReportRegisterLicense(licensetype, comcode, startdate, enddate);
        }
        
        public IAS.DTO.ResponseService<System.Data.DataSet> GetLicenseReport(string LicenseTypeCode, string CompCode, string StartDate, string EndDate) {
            return base.Channel.GetLicenseReport(LicenseTypeCode, CompCode, StartDate, EndDate);
        }
    }
}

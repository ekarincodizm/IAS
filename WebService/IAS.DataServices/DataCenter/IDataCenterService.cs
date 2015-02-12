using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IAS.DTO;
using System.Collections.ObjectModel;
using System.Data;

namespace IAS.DataServices.DataCenter
{
    [ServiceContract]
    public interface IDataCenterService
    {
        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetTitleName(string firstItem);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetMemberTypeById(string Id);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetProvince(string firstItem);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetTitleNameById(int Id);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetAmpur(string firstItem, string provinceCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetTumbon(string firstItem, string provinceCode, string ampurCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetEducation(string firstItem);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetEducationById(string Id);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetDocumentType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetDocumentTypeAll(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetDocumentTypeIsImage();

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetStatus(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetMemberType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetMemberTypeNotOIC(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetMemberTypeNotOIC_for_regSearchOfficerOIC(string firstItem);//milk

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetOICType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetNationality(string firstItem);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetNationalityById(string Id);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetCompanyCode(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<string>> GetCompanyByName(string anyName);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetCompanyCodeById(string Id);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetCompanyNameById(string Id);

        [OperationContract]
        DTO.ResponseService<List<string>> GetCompanyCodeByName(string anyText);

        [OperationContract]
        DTO.ResponseService<List<DTO.ApproveConfig>> GetApproveConfig();

        [OperationContract]
        DTO.ResponseService<DTO.ResponseMessage<bool>> SetApproveConfig(List<DTO.ApproveConfig> cfgList);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetProvinceById(string Id);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetAmpurById(string provinceId, string Id);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetTumbonById(string provinceId, string ampurId, string Id);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceGroup(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamPlace(string firstItem, string groupCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamTime(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetLicenseType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetAssociate(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetPaymentType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetRequestLicenseType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetPettitionTypebyLicenseType(string firstItem, string licenseType);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetRequestLicenseType_NOias(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigApproveMember();

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigGeneral();

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigApproveDocument();

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateConfigApproveMember(List<DTO.ConfigEntity> configs);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateConfigApproveDocument(List<DTO.ConfigEntity> configs);

        [OperationContract]
        DTO.ResponseService<List<string>> GetAssociateCodeByName(string anyText);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetAssociateNameById(string Id);

        [OperationContract]
        DTO.ResponseMessage<decimal> GetPetitionFee(string petitionTypeCode);

        [OperationContract]
        DTO.ResponseMessage<bool> InsertLogOpenMenu(string userId, string functionURI);

        [OperationContract]
        DTO.ResponseService<string> GetPlaceExamNameById(string placeExamCode);

        [OperationContract]
        DTO.ResponseService<string> GetAcceptOffNameById(string acceptOffCode);

        [OperationContract]
        DTO.ResponseService<string> GetCompanyNameByIdToText(string compCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetCompanyByRequest(string compCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetAssociateToSetting(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentConfigApproveMember();

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentConfigApproveMemberByApplicant();

        [OperationContract]
        DTO.ResponseMessage<bool> InsertDocumentType(DTO.DocumentType doc, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseMessage<bool> DeleteDocumentType(string docCode);

        [OperationContract]
        DTO.ResponseMessage<bool> InsertConfigDocument(DTO.ConfigDocument doc, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<string> GetConfigDocumentByDocumentCode(string FuncID, string docCode, string memberCode);

        [OperationContract]
        DTO.ResponseMessage<bool> DeleteConfigDocument(decimal id, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetConfigPetitionLicenseType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetConfigLicenseType(string firstItem);

        [OperationContract]
        DTO.ResponseService<string> GetConfigDocumentLicense(string petitionCode, string licenseCode, string docCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfigByType(string licenseCode, string petitionCode);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateConfigApproveLicense(List<DTO.ConfigDocument> configs, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfigByPetitionType(string petitionCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceGroupByCompCode(string firstItem, string compcode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceByCompCode(string groupCode, string compcode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetDocumentTypeDeleted(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByCompCode(string compcode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetCompanyByLicenseType(string firstItem, string licenseType);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfig(string petitionCode, string licenseTypeCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetPersonLicenseType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.SpecialDocument>> GetSpecialDocType(string docStatus, string trainStatus);

        [OperationContract]
        DTO.ResponseService<List<DTO.SpecialDocument>> GetExamSpecialDocType(string docStatus, string trainStatus,string licenseType);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetDocRequire(string docFunc, string memCode, string licenseType, string pettionType);

        [OperationContract]
        List<DTO.DocumentType> GetDocumentConfigList();

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByCreateTest(DTO.UserProfile user);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetMemberDocumentType(string memCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfigByPetitionTypeName(string petitionName);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetPicByDocumentCode(string documentCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetAgentType(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByAgentType(string agentType);

        [OperationContract]
        DTO.ResponseService<DataSet> GetAssociation(string Asso_Code);

        [OperationContract]
        DTO.ResponseService<DataSet> GetAssociationJoinLicense(string Asso_Code, string license_code);

        [OperationContract]
        DTO.ResponseMessage<bool> InsertAssociation(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateAsscoiation(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license);

        [OperationContract]
        DTO.ResponseMessage<bool> DeleteAsscoiation(string ID);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetQualification(string firstItem);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateCancelApplicant(DTO.Applicant appl, DTO.ExamLicense examl);

        [OperationContract]
        DTO.ResponseService<DataSet> GetConfigPrint(string groupCode);

        [OperationContract]
        DTO.ResponseMessage<bool> SaveConfigPrint(List<DTO.ConfigPrintPayment> ent);

        [OperationContract]
        DTO.ResponseService<List<DTO.AssociationLicense>> GetAssociationLicense(string Association_Code);

        [OperationContract]
        DTO.ResponseService<List<DTO.AssociationLicense>> GetAssociationLicenseByCode(string Association_Code);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceFromProvinceAndGroupCode(string ProCode, string GroupCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceFromProvinceAndAssoCode(string ProCode, string AssoCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetSubjectGroup(string firstItem);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetAssociationLicenseByAssocCode(string AssociationCode);

        //[OperationContract]
        //DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByAssocConfig(string AssociationCode);

        [OperationContract]
        DTO.ResponseService<List<string>> GetCompanyCodeAsCompanyT(string anyText);

        [OperationContract]
        DTO.ResponseService<DataSet> GetDefaultcompanyName(string Id);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigExtraEntity>> GetNewConfigApproveMember();

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateNewConfigApproveMember(List<DTO.ConfigExtraEntity> config, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<DataSet> GetAssByAssCodeAndAssName(string ID, string name, string compType, string aType);
    
        [OperationContract]
        DTO.ResponseService<string> GetExamPerBill();

        [OperationContract]
        DTO.ResponseService<List<DTO.TitleName>> GetTitleNameFromSex(string sex);

        [OperationContract]
        DTO.ResponseService<DataSet> GetAssociationByByCriteria(string Code, string Name, string CompType, string AgentType, int NumPage, int RowPerPage, bool Count, bool? IsActive);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamPlaceGroupR(string ExamPlace_Code);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamPlaceGroupRByIDName(string ID, string name, int pageNo, int recordPerPage, Boolean Count);

        [OperationContract]
        DTO.ResponseMessage<bool> InsertExamPlaceGroupR(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamPlaceGroupRByCheckID(string ID, string name);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamPlace_UnderAssocicate(string firstItem, string groupCode);


        [OperationContract]
        DTO.ResponseService<List<string>> GetInsuranceAssociate(string anyText);

        [OperationContract]
        DTO.ResponseService<DTO.ASSOCIATION> GetInsuranceAssociateNameByID(string AssID);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamPlace_AndProvince(string groupCode);


        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamSpecialDocument(List<string> fileType);


        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetTrainSpecialDocument(List<string> fileType);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetAssociationApprove(string lincenseType);

        [OperationContract]
        DTO.ResponseService<string> GetLicensefromTestingNo(string TestingNo);

        [OperationContract]
        DTO.ResponseService<string> GetConficValueByTypeAndGroupCode(string ID, string GroupCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigCheckExamLicense();

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateConfigCheckExamLicense(List<DTO.ConfigEntity> cfgEnt, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByAsso(string firstItem, string Asso);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetTrainSpecialbyIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetUserVerifyDoc(string compcode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamSpecial(string idCard, string licenseType);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetMemberTypeRegister(string item);
    }
}

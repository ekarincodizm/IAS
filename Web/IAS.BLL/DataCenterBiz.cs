using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;

namespace IAS.BLL
{
    public class DataCenterBiz : IDisposable
    {
        private DataCenterService.DataCenterServiceClient svc;

        public DataCenterBiz()
        {
            svc = new DataCenterService.DataCenterServiceClient();
        }

        public List<DTO.DataItem> GetTitleName(string firstItem)
        {

            var res = svc.GetTitleName(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public DTO.DataItem GetTitleNameById(int Id)
        {
            var res = svc.GetTitleNameById(Id);
            return !res.IsError
                        ? res.DataResponse
                        : null;
        }

        public DTO.ResponseService<DTO.DataItem> GetMemberTypeById(string Id)
        {
            var res = svc.GetMemberTypeById(Id);
            return res;
        }

        public List<DTO.DataItem> GetProvince(string firstItem)
        {

            var res = svc.GetProvince(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public List<DTO.DataItem> GetAmpur(string firstItem, string provinceCode)
        {

            var res = svc.GetAmpur(firstItem, provinceCode);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public List<DTO.DataItem> GetTumbon(string firstItem, string provinceCode, string ampurCode)
        {

            var res = svc.GetTumbon(firstItem, provinceCode, ampurCode);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public List<DTO.DataItem> GetEducation(string firstItem)
        {
            var res = svc.GetEducation(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public DTO.DataItem GetEducationById(string Id)
        {
            var res = svc.GetEducationById(Id);

            return !res.IsError
                        ? res.DataResponse
                        : null;
        }

        public List<DTO.DataItem> GetDocumentType(string firstItem)
        {

            var res = svc.GetDocumentType(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }
        public List<DTO.DataItem> GetDocumentTypeAll(string firstItem)
        {

            var res = svc.GetDocumentTypeAll(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }
        public List<DTO.DataItem> GetDocumentTypeDeleted(string firstItem)
        {

            var res = svc.GetDocumentTypeDeleted(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }
        public List<DTO.DataItem> GetDocumentTypeIsImage()
        {

            var res = svc.GetDocumentTypeIsImage();

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public List<DTO.DataItem> GetStatus(string firstItem)
        {

            var res = svc.GetStatus(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public List<DTO.DataItem> GetMemberType(string firstItem, bool NotOIC)
        {

            var res = (NotOIC ? svc.GetMemberTypeNotOIC(firstItem)
                              : svc.GetMemberType(firstItem));

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }
        public List<DTO.DataItem> GetMemberTypeNotOIC_for_regSearchOfficerOIC(string firstItem, bool NotOIC)
        {

            var res = svc.GetMemberTypeNotOIC_for_regSearchOfficerOIC(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }
        public List<DTO.DataItem> GetOICType(string firstItem)
        {

            var res = svc.GetOICType(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public List<DTO.DataItem> GetNationality(string firstItem)
        {

            var res = svc.GetNationality(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public DTO.DataItem GetNationalityById(string Id)
        {

            var res = svc.GetNationalityById(Id);

            return !res.IsError
                        ? res.DataResponse
                        : null;
        }

        public List<DTO.DataItem> GetCompanyCode(string firstItem)
        {

            var res = svc.GetCompanyCode(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public string[] GetCompanyByName(string anyName)
        {

            var res = svc.GetCompanyByName(anyName);

            return !res.IsError
                        ? res.DataResponse
                        : null;
        }

        public string GetCompanyNameById(string Id)
        {
            var res = svc.GetCompanyNameById(Id);
            return !res.IsError
                        ? res.DataResponse.Name
                        : string.Empty;
        }

        public DTO.DataItem GetCompanyCodeById(string Id)
        {

            var res = svc.GetCompanyCodeById(Id);

            return !res.IsError
                        ? res.DataResponse
                        : null;
        }

        public List<string> GetCompanyCodeByName(string anyText)
        {

            var res = svc.GetCompanyCodeByName(anyText);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetCompanyByRequest(string compCode)
        {
            var res = svc.GetCompanyByRequest(compCode);
            return res;
        }

        public DTO.ResponseService<DTO.ApproveConfig[]> GetApproveConfig()
        {
            var res = svc.GetApproveConfig();
            return res;
        }

        public DTO.ResponseService<DTO.ResponseMessage<bool>> SetApproveConfig(DTO.ApproveConfig[] cfgList)
        {
            var res = svc.SetApproveConfig(cfgList);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetProvinceById(string Id)
        {
            var res = svc.GetProvinceById(Id);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetAmpurById(string provinceId, string Id)
        {
            var res = svc.GetAmpurById(provinceId, Id);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetTumbonById(string provinceId, string ampurId, string Id)
        {
            var res = svc.GetTumbonById(provinceId, ampurId, Id);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetExamPlaceGroup(string firstItem)
        {
            var res = svc.GetExamPlaceGroup(firstItem);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetExamPlace(string firstItem, string groupCode)
        {
            var res = svc.GetExamPlace(firstItem, groupCode);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetExamTime(string firstItem)
        {
            var res = svc.GetExamTime(firstItem);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetLicenseType(string firstItem)
        {
            var res = svc.GetLicenseType(firstItem);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetPaymentType(string firstItem)
        {
            return svc.GetPaymentType(firstItem);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetRequestLicenseType(string firstItem)
        {
            return svc.GetRequestLicenseType(firstItem);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetPettitionTypebyLicenseType(string firstItem, string licenseType)
        {
            return svc.GetPettitionTypebyLicenseType(firstItem,licenseType);
        }


        public DTO.ResponseService<DTO.DataItem[]> GetRequestLicenseType_NOias(string firstItem)
        {
            return svc.GetRequestLicenseType_NOias(firstItem);
        }

        public DTO.ResponseService<DTO.ConfigEntity[]> GetConfigApproveMember()
        {
            return svc.GetConfigApproveMember();
        }

        public DTO.ResponseService<DTO.ConfigEntity[]> GetConfigGeneral()
        {
            return svc.GetConfigGeneral();
        }

        public DTO.ResponseService<DTO.ConfigEntity[]> GetConfigApproveDocument()
        {
            return svc.GetConfigApproveDocument();
        }

        public DTO.ResponseService<DTO.ConfigDocument[]> GetDocumentConfigApproveMember()
        {
            return svc.GetDocumentConfigApproveMember();
        }

        public DTO.ResponseService<DTO.ConfigDocument[]> GetDocumentConfigApproveMemberByApplicant()
        {
            return svc.GetDocumentConfigApproveMemberByApplicant();
        }

        public DTO.ResponseMessage<bool> UpdateConfigApproveMember(List<DTO.ConfigEntity> configs)
        {
            return svc.UpdateConfigApproveMember(configs.ToArray());
        }

        public DTO.ResponseMessage<bool> UpdateConfigApproveDocument(List<DTO.ConfigEntity> configs)
        {
            return svc.UpdateConfigApproveDocument(configs.ToArray());
        }

        public string[] GetAssociate(string anyTest)
        {
            var res = svc.GetAssociateCodeByName(anyTest);

            return !res.IsError
                        ? res.DataResponse
                        : null;
        }

        public string[] GetInsuranceAssociate(string anyTest)
        {
            var res = svc.GetInsuranceAssociate(anyTest);

            return !res.IsError
                        ? res.DataResponse
                        : null;
        }

        public DTO.ResponseService<DTO.ASSOCIATION> GetInsuranceAssociateNameByID(string AssID)
        {
            return svc.GetInsuranceAssociateNameByID(AssID);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetAssociateForConfig(string firstItem)
        {
            return svc.GetAssociate(firstItem);
        }

        public DTO.ResponseMessage<decimal> GetPetitionFee(string petitionTypeCode)
        {
            return svc.GetPetitionFee(petitionTypeCode);
        }

        public DTO.ResponseService<string> GetAssociateNameById(string assoCode)
        {
            var res = new DTO.ResponseService<string>();
            var resAsso = GetAssociateForConfig("");
            var asso = resAsso.DataResponse.Where(w => w.Id == assoCode).FirstOrDefault();
            if (asso != null)
            {
                res.DataResponse = asso.Name;
            }
            return res;
        }

        public DTO.ResponseMessage<bool> InsertLogOpenMenu(string userId, string functionId)
        {
            return svc.InsertLogOpenMenu(userId, functionId);
        }

        public DTO.ResponseService<string> GetPlaceExamNameById(string placeExamCode)
        {
            return svc.GetPlaceExamNameById(placeExamCode);
        }

        public DTO.ResponseService<string> GetAcceptOffNameById(string acceptOffCode)
        {
            return svc.GetAcceptOffNameById(acceptOffCode);
        }

        public DTO.ResponseService<string> GetCompanyNameByIdToText(string compCode)
        {
            return svc.GetCompanyNameByIdToText(compCode);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetAssociateToSetting(string firstItem)
        {
            return svc.GetAssociateToSetting(firstItem);
        }

        public void Dispose()
        {
            if (svc != null) svc.Close();
            GC.SuppressFinalize(this);
        }

        public DTO.ResponseMessage<bool> InsertDocumentType(DTO.DocumentType doc, DTO.UserProfile userProfile)
        {
            return svc.InsertDocumentType(doc, userProfile);
        }

        public DTO.ResponseMessage<bool> InsertConfigDocument(DTO.ConfigDocument doc, DTO.UserProfile userProfile)
        {
            return svc.InsertConfigDocument(doc, userProfile);
        }

        public DTO.ResponseMessage<bool> DeleteDocumentType(string docCode)
        {
            return svc.DeleteDocumentType(docCode);
        }

        public DTO.ResponseService<string> GetConfigDocumentByDocumentCode(string FuncID, string docCode, string memberCode)
        {
            return svc.GetConfigDocumentByDocumentCode(FuncID, docCode, memberCode);
        }

        public DTO.ResponseMessage<bool> DeleteConfigDocument(decimal id, DTO.UserProfile userProfile)
        {
            return svc.DeleteConfigDocument(id, userProfile);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetConfigPetitionLicenseType(string firstItem)
        {
            return svc.GetConfigPetitionLicenseType(firstItem);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetConfigLicenseType(string firstItem)
        {
            return svc.GetConfigLicenseType(firstItem);

        }

        public DTO.ResponseService<string> GetConfigDocumentLicense(string petitionCode, string licenseCode, string docCode)
        {
            return svc.GetConfigDocumentLicense(petitionCode, licenseCode, docCode);
        }

        public DTO.ResponseService<DTO.ConfigDocument[]> GetDocumentLicenseConfigByType(string licenseCode, string petitionCode)
        {
            return svc.GetDocumentLicenseConfigByType(licenseCode, petitionCode);
        }

        public DTO.ResponseMessage<bool> UpdateConfigApproveLicense(List<DTO.ConfigDocument> configs, DTO.UserProfile userProfile)
        {
            return svc.UpdateConfigApproveLicense(configs.ToArray(), userProfile);
        }

        public DTO.ResponseService<DTO.ConfigDocument[]> GetDocumentLicenseConfigByPetitionType(string petitionCode)
        {
            return svc.GetDocumentLicenseConfigByPetitionType(petitionCode);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetExamPlaceGroupByCompCode(string firstItem, string compcode)
        {
            var res = svc.GetExamPlaceGroupByCompCode(firstItem, compcode);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetExamPlaceByCompCode(string groupCode, string compcode)
        {
            var res = svc.GetExamPlaceByCompCode(groupCode, compcode);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetLicenseTypeByCompCode(string compcode)
        {
            var res = svc.GetLicenseTypeByCompCode(compcode);
            return res;
        }

        #region Personal License Func
        public DTO.ResponseService<DTO.DataItem[]> GetCompanyByLicenseType(string firstItem, string licenseType)
        {
            var res = svc.GetCompanyByLicenseType(firstItem, licenseType);
            return res;
        }

        public DTO.ResponseService<DTO.ConfigDocument[]> GetDocumentLicenseConfig(string petitionCode, string licenseTypeCode)
        {
            var res = svc.GetDocumentLicenseConfig(petitionCode, licenseTypeCode);
            return res;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetPersonLicenseType(string firstItem)
        {
            var res = svc.GetPersonLicenseType(firstItem);
            return res;

        }
        #endregion

        #region Document Validation
        public DTO.ResponseService<DTO.ConfigDocument[]> GetDocRequire(string docFunc, string memCode, string licenseType, string pettionType)
        {
            //var res = svc.GetRegisDocRequire(docFunc, memCode, docCode, licenseType , pettionType);
            var res = svc.GetDocRequire(docFunc, memCode, licenseType, pettionType);
            return res;

        }
        DTO.ResponseService<DTO.ConfigDocument[]> GetMemberDocumentType(string memCode)
        {
            var res = svc.GetMemberDocumentType(memCode);
            return res;
        }
        #endregion


        public List<DTO.DocumentType> GetDocumentConfigList()
        {
            var res = svc.GetDocumentConfigList();
            return res.ToList();
        }

        public DTO.ResponseService<DTO.DataItem[]> GetLicenseTypeByCreateTest(DTO.UserProfile user)
        {
            var res = svc.GetLicenseTypeByCreateTest(user);
            return res;
        }

        public DTO.ResponseService<DTO.ConfigDocument[]> GetDocumentLicenseConfigByPetitionTypeName(string petitionName)
        {
            return svc.GetDocumentLicenseConfigByPetitionTypeName(petitionName);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetPicByDocumentCode(string documentCode)
        {
            var res = svc.GetPicByDocumentCode(documentCode);
            return res;
        }

        public List<DTO.DataItem> GetAgentType(string firstItem)
        {

            var res = svc.GetAgentType(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetLicenseTypeByAgentType(string agentType)
        {
            var res = svc.GetLicenseTypeByAgentType(agentType);
            return res;
        }

        public DTO.ResponseService<DataSet> GetAssociation(string Asso_Code)
        {
            return svc.GetAssociation(Asso_Code);
        }

        public DTO.ResponseService<DataSet> GetAssociationJoinLicense(string Asso_Code, string license_code)
        {
            return svc.GetAssociationJoinLicense(Asso_Code, license_code);
        }

        public DTO.ResponseMessage<bool> InsertAssociation(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license)
        {
            return svc.InsertAssociation(ent, userProfile, license.ToArray());
        }

        public DTO.ResponseMessage<bool> UpdateAsscoiation(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license)
        {
            return svc.UpdateAsscoiation(ent, userProfile, license.ToArray());
        }

        public DTO.ResponseMessage<bool> DeleteAsscoiation(string ID)
        {
            return svc.DeleteAsscoiation(ID);
        }


        public List<DTO.DataItem> GetQualification(string firstItem)
        {
            var res = svc.GetQualification(firstItem);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public DTO.ResponseMessage<bool> UpdateCancelApplicant(DTO.Applicant appl, DTO.ExamLicense examl)
        {
            return svc.UpdateCancelApplicant(appl, examl);
        }

        public DTO.ResponseService<DataSet> GetConfigPrint(string groupCode)
        {
            return svc.GetConfigPrint(groupCode);
        }

        public DTO.ResponseMessage<bool> SaveConfigPrint(List<DTO.ConfigPrintPayment> ent)
        {
            return svc.SaveConfigPrint(ent.ToArray());
        }

        public DTO.ResponseService<DTO.AssociationLicense[]> GetAssociationLicense(string Association_Code)
        {
            return svc.GetAssociationLicense(Association_Code);
        }

        public DTO.ResponseService<DTO.AssociationLicense[]> GetAssociationLicenseByCode(string Association_Code)
        {
            return svc.GetAssociationLicenseByCode(Association_Code);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetSubjectGroup(string firstItem)
        {
            return svc.GetSubjectGroup(firstItem);
        }

        public List<DTO.DataItem> GetExamPlaceFromProvinceAndGroupCode(string ProCode, string GroupCode)
        {
            var res = svc.GetExamPlaceFromProvinceAndGroupCode(ProCode, GroupCode);
            return !res.IsError
                       ? res.DataResponse.ToList()
                       : null;
        }

        public List<DTO.DataItem> GetExamPlaceFromProvinceAndAssoCode(string ProCode, string AssoCode)
        {
            var res = svc.GetExamPlaceFromProvinceAndAssoCode(ProCode, AssoCode);
            return !res.IsError
                       ? res.DataResponse.ToList()
                       : null;
        }

        public DTO.ResponseService<DTO.DataItem[]> GetAssociationLicenseByAssocCode(string AssociationCode)
        {
            return svc.GetAssociationLicenseByAssocCode(AssociationCode);


        }

        public List<string> GetCompanyCodeAsCompanyT(string anyText)
        {

            var res = svc.GetCompanyCodeAsCompanyT(anyText);

            return !res.IsError
                        ? res.DataResponse.ToList()
                        : null;
        }

        public DTO.ResponseService<DataSet> GetDefaultcompanyName(string Id)
        {
            return svc.GetDefaultcompanyName(Id);
        }

        public DTO.ResponseService<DTO.ConfigExtraEntity[]> GetNewConfigApproveMember()
        {
            return svc.GetNewConfigApproveMember();
        }

        public DTO.ResponseMessage<bool> UpdateNewConfigApproveMember(DTO.ConfigExtraEntity[] config, DTO.UserProfile userProfile)
        {
            return svc.UpdateNewConfigApproveMember(config, userProfile);
        }

        public DTO.ResponseService<string> GetExamPerBill()
        {
            return svc.GetExamPerBill();
        }



        public DTO.ResponseService<DataSet> GetAssByAssCodeAndAssName(string ID, string name, string compType, string aType)
        {
           return svc.GetAssByAssCodeAndAssName( ID,  name,compType,aType);
        }

        public DTO.ResponseService<DTO.TitleName[]> GetTitleNameFromSex(string sex)
        {
            return svc.GetTitleNameFromSex(sex);
        }

        public DTO.ResponseService<DataSet> GetExamPlaceGroupR(string ExamPlace_Code)
        {
            return svc.GetExamPlaceGroupR(ExamPlace_Code);
        }

        public DTO.ResponseService<DataSet> GetExamPlaceGroupRByIDName(string ID, string name, int pageNo, int recordPerPage, Boolean Count)
        {
            return svc.GetExamPlaceGroupRByIDName(ID, name, pageNo, recordPerPage, Count);
        }

        public DTO.ResponseMessage<bool> InsertExamPlaceGroupR(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license)
        {
            return svc.InsertExamPlaceGroupR(ent, userProfile, license.ToArray());
        }

        public DTO.ResponseService<DataSet> GetExamPlaceGroupRByCheckID(string ID, string name)
        {
            return svc.GetExamPlaceGroupRByCheckID(ID, name);
        }

        public DTO.ResponseService<DataSet> GetExamPlace_UnderAssocicate(string firstItem, string groupCode)
        {
            var res = svc.GetExamPlace_UnderAssocicate(firstItem, groupCode);
            return res;
        }

        public DTO.ResponseService<DataSet> GetAssociationByByCriteria(string Code, string Name, string CompType, string AgentType, int NumPage, int RowPerPage, bool Count, bool? IsActive)
        {
            return svc.GetAssociationByByCriteria(Code, Name, CompType, AgentType, NumPage, RowPerPage, Count, IsActive);
        }

        public DTO.ResponseService<DataSet> GetExamPlace_AndProvince(string groupCode)
        {
            return svc.GetExamPlace_AndProvince(groupCode);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetExamSpecialDocument(List<string> filesType)
        {
            return svc.GetExamSpecialDocument(filesType.ToArray());
        }

        public DTO.ResponseService<DTO.DataItem[]> GetTrainSpecialDocument(List<string> filesType)
        {
            return svc.GetTrainSpecialDocument(filesType.ToArray());
        }

        public DTO.ResponseService<DTO.DataItem[]> GetAssociationApprove(string lincenseType)
        {
            return svc.GetAssociationApprove(lincenseType);
        }


        public DTO.ResponseService<string> GetLicensefromTestingNo(string TestingNo)
        {
            return svc.GetLicensefromTestingNo(TestingNo);
        }

        public DTO.ResponseService<string> GetConficValueByTypeAndGroupCode(string ID, string GroupCode)
        {
            return svc.GetConficValueByTypeAndGroupCode(ID,GroupCode);
        }

        public DTO.ResponseService<DTO.ConfigEntity[]> GetConfigCheckExamLicense()
        {
            return svc.GetConfigCheckExamLicense();
        }

        public DTO.ResponseMessage<bool> UpdateConfigCheckExamLicense(List<DTO.ConfigEntity> cfgEnt, DTO.UserProfile userProfile)
        {
            return svc.UpdateConfigCheckExamLicense(cfgEnt.ToArray(), userProfile);
        }


        public DTO.ResponseService<DTO.DataItem[]> GetLicenseTypeByAsso(string firstItem, string Asso)
        {
            return svc.GetLicenseTypeByAsso(firstItem, Asso);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetTrainSpecialbyIdCard(string idCard)
        {
            return svc.GetTrainSpecialbyIdCard(idCard);
        }

        public DTO.ResponseService<DataSet> GetUserVerifyDoc(string compcode)
        {
            return svc.GetUserVerifyDoc(compcode);
        }


        public DTO.ResponseService<DTO.DataItem[]> GetExamSpecial(string idCard,string licenseType)
        {
            return svc.GetExamSpecial(idCard, licenseType);
        }

        public DTO.ResponseService<DTO.SpecialDocument[]> GetSpecialDocType(string docStatus, string trainStatus)
        {
            return svc.GetSpecialDocType(docStatus, trainStatus);
        }

        public DTO.ResponseService<DTO.SpecialDocument[]> GetExamSpecialDocType(string docStatus, string trainStatus, string licenseType)
        {
            return svc.GetExamSpecialDocType(docStatus, trainStatus, licenseType);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetMemberTypeRegister(string item)
        {
            return svc.GetMemberTypeRegister(item);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetMemberTypeAll(string firstItem)
        {
            return svc.GetMemberType(firstItem);
        }

    }
}

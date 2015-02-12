using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using IAS.DAL;

namespace IAS.DataServices.License
{
    [ServiceContract]
    public interface ILicenseService
    {
        [OperationContract]
        DTO.ResponseService<DataSet>
            GetLicenseByCriteria(string licenseNo, string licenseType,
                                 DateTime? startDate, DateTime? toDate,
                                 string paymentNo, string licenseTypeReceive,
                                 DTO.UserProfile userProfile,
                                 int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<DTO.PersonalHistory> GetPersonalHistoryByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamHistoryByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetTrainingHistoryBy(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetTrain_1_To_4_ByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetUnitLinkByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetRequestLicenseByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetReceiveLicenseByCriteria(string licenseNo, string licenseTypeCode,
                                                                 DateTime? startDate, DateTime? toDate);

        [OperationContract]
        DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>>
            InsertAndCheckReceiveLicenseGroupUpload(DTO.UploadData data,
                                                    DTO.CompressFileDetail compressFile,
                                                    string groupNo,
                                                    string fileName, string userId,
                                                    string petitionTypeCode,
                                                    string licenseTypeCode);

        //[OperationContract]
        //DTO.ResponseService<string> SubmitReceiveLicenseGroupUpload(string groupId, List<DTO.AttatchFileLicense> list);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetLicenseVerify(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string Compcode, string requestCompCode);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetLicenseVerifyByRequest(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string Compcode);

        [OperationContract]
        DTO.ResponseService<DTO.LicenseVerifyDetail>
            GetLicenseVerifyDetail(string groupUploadNo, string seqNo);


        [OperationContract]
        DTO.ResponseService<List<IAS.DTO.AttatchFileLicense>>
            GetLicenseVerifyImgDetail(string groupUploadNo, string idCard, string CountPage, Int32 pageIndex, Int32 pageSize);

        [OperationContract]
        DTO.ResponseService<string> ApproveLicenseVerify(List<DTO.SubmitLicenseVerify> list, string flgApprove, string ApproveID);

        [OperationContract]
        DTO.ResponseMessage<bool> InsertSingleReceiveLicense(DTO.ReceiveLicenseHeader header,
                                                                    DTO.ReceiveLicenseDetail detail,
                                                                    DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseMessage<bool> SubmitSingleOrGroupReceiveLicense(string groupId,
                                                             List<DTO.AttatchFileLicense> attachs);
        //[OperationContract]
        //DTO.ResponseService<List<DTO.AttatchFileDetail>> ExtractFile(string autoId, string compressFile,
        //                                               string tempPath, string mapTempPath);

        [OperationContract]
        DTO.ResponseService<DTO.SummaryReceiveLicense>
              UploadDataLicense(DTO.DataLicenseRequest request);

        [OperationContract]
        DTO.ResponseService<List<DTO.AttatchFileLicense>> MoveExtachFile(String userId, String targetPath, List<DTO.AttachFileDetail> attachFiles);

        #region Person License
        /// <summary>
        /// New Personal License Natta
        /// </summary>
        /// <param name="idCard"></param>
        /// <returns></returns>

        [OperationContract]
        DTO.ResponseService<List<DTO.ExamHistory>> GetExamHistoryByID(string idCard);

        [OperationContract]
        DTO.ResponseService<List<DTO.ExamHistory>> GetExamHistoryByIDWithCondition(string idCard, string licenseTypeCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.TrainPersonHistory>> GetTrainingHistoryByID(string idCard);

        [OperationContract]
        DTO.ResponseService<List<DTO.TrainPersonHistory>> GetTrainingHistoryByIDWithCondition(string idCard, string licenseTypeCode);

        [OperationContract]
        DTO.ResponseService<DTO.TrainPersonHistory> GetPersonalTrainByCriteria(string licenseTypeCode, string pettitionTypeCode, string renewTime, string idCard, string licenseNo, string specialTrainCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.Tran3To7>> GetTrain_3_To_7_ByID(string idCard);
        
        [OperationContract]
        DTO.ResponseService<List<DTO.UnitLink>> GetUnitLinkByID(string idCard);

        [OperationContract]
        DTO.ResponseService<List<DTO.UnitLink>> GetUnitLinkByIDWithCondition(string idCard, string licenseTypeCode);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonAttatchFile>> GetAttatchFileLicenseByPersonId(string personID);

        [OperationContract]
        DTO.ResponseMessage<bool> InsertPersonLicense(List<DTO.PersonLicenseHead> header, List<DTO.PersonLicenseDetail> detail, DTO.UserProfile userProfile, List<DTO.AttatchFileLicense> files);


        [OperationContract]
        DTO.ResponseService<List<DTO.ApproverDoctype>> GetApprocerByDocType(string appdocType);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseDetail>> GenSEQLicenseDetail(DTO.PersonLicenseHead uploadGroupNo);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetLicenseTransaction(List<DTO.PersonLicenseHead> head, List<DTO.PersonLicenseDetail> detail);

        [OperationContract]
        DTO.ResponseMessage<bool> SingleLicenseValidation(DTO.PersonLicenseHead head, DTO.PersonLicenseDetail detail);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetPaymentLicenseTransaction(List<DTO.PersonLicenseHead> head, List<DTO.PersonLicenseDetail> detail);

        [OperationContract]
        DTO.ResponseService<DataSet> GetRequesPersontLicenseByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetRenewLicneseByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DTO.PersonLicenseTransaction> GetRenewLiceneEntityByLicenseNo(string licenseNo);

        [OperationContract]
        DTO.ResponseMessage<bool> updateRenewLicense(DTO.PersonLicenseHead h, DTO.PersonLicenseDetail d);

        [OperationContract]
        DTO.ResponseMessage<bool> updateLicenseDetail(DTO.PersonLicenseDetail d);



        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetExpiredLicneseByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseTransaction>> getViewPersonLicense(string idCard, string status);

        [OperationContract]
        DTO.ResponseService<DTO.PersonLicenseTransaction> GetLicenseDetailByUploadGroupNo(string upGroupNo);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetAllLicenseByIDCard(string idCard, string mode, int feemode);

        [OperationContract]
        DTO.ResponseService<DTO.PersonLicenseTransaction> GetLicenseRenewDateByLicenseNo(string licenseNo);
        #endregion



        [OperationContract]
        DTO.ResponseService<DataSet> GetLicenseVerifyHead(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string Compcode, string requestCompCode, string CountPage, int pageNo, int recordPerPage, string StatusApprove);

        [OperationContract]
        DTO.ResponseService<DataSet> GetListLicenseDetailVerify(string uploadGroupNo, string CountPage, int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<DTO.VerifyDocumentHeader> GetVerifyHeadByUploadGroupNo(string uploadGroupNo);

        [OperationContract]
        DTO.ResponseMessage<bool> CheckLicenseDetailVerifyHasNotApprove(string uploadGroupNo);

        [OperationContract]
        DTO.ResponseMessage<bool> ValidateDetail(string groupId);

        [OperationContract]
        DTO.ResponseService<List<DTO.DetailTemp>> GetDetail(string groupId);

        [OperationContract]
        DTO.ResponseService<String> GenZipFileLicenseRequest(DateTime findDate, String username);

        [OperationContract]
        DTO.ResponseService<DataSet> GetListLicenseDetailByCriteria(string licenseNo, string licenseType,
                                 DateTime? startDate, DateTime? toDate,
                                 string paymentNo, string licenseTypeReceive,
                                 DTO.UserProfile userProfile,
                                 int pageNo, int recordPerPage, string PageCount);

        [OperationContract]
        DTO.ResponseService<DataSet> GetResultLicenseVerifyHead(string petitionTypeCode,
                     DateTime? startDate,
                     DateTime? toDate, string Compcode, string CountPage, int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<DataSet> GetListLicenseDetailByPersonal(string licenseNo, string licenseType,
                               DateTime? startDate, DateTime? toDate,
                               string paymentNo, string licenseTypeReceive,
                               DTO.UserProfile userProfile,
                               int pageNo, int recordPerPage, Boolean CountAgain);

        [OperationContract]
        DTO.ResponseService<DataSet> GetLicenseVerifyHeadByOIC(string petitionTypeCode,
                      DateTime? startDate,
                      DateTime? toDate, string requestCompCode, string CountPage, int pageNo, int recordPerPage, string StatusApprove);


        [OperationContract]
        DTO.ResponseService<DataSet> GetListLicenseDetailAdminByCriteria(string licenseNo, string licenseType,
                             DateTime? startDate, DateTime? toDate,
                             string paymentNo, string licenseTypeReceive,
                             DTO.UserProfile userProfile,
                             int pageNo, int recordPerPage, string PageCount);

        [OperationContract]
        DTO.ResponseService<List<DTO.GBHoliday>> GETGBHoliday(string date);


        [OperationContract]
        DTO.ResponseService<DataSet> GetEditLicenseHead(string petitionTypeCode,
                     DateTime? startDate,
                     DateTime? toDate, string Compcode, string CountPage, int pageNo, int recordPerPage);



        [OperationContract]
        DTO.ResponseService<List<DTO.ValidateLicense>> GetPropLiecense(string licenseType, string pettionType,Int32 renewTime, Int32 groupId);



        [OperationContract]
        DTO.ResponseMessage<bool> ChkSpecialExam(List<string> fileType, string licenseType);


        [OperationContract]
        DTO.ResponseMessage<int> GetRenewTimebyLicenseNo(string licenseNo);

        [OperationContract]
        DTO.ResponseMessage<bool> CheckSpecial(string idCard);

        [OperationContract]
        DTO.ResponseService<List<DTO.TrainSpecial>> GetSpecialTempTrainById(string idCard);

        [OperationContract]
        DTO.ResponseService<List<DTO.ExamSpecial>> GetSpecialTempExamById(string idCard);


        [OperationContract]
        DTO.ResponseMessage<bool> ValidateProp(string groupId);

        [OperationContract]
        DTO.ResponseMessage<bool> LicenseRevokedValidation(List<string> license, string licenseTypeCode);

        [OperationContract]
        DTO.ResponseService<DataSet> GetRenewLicenseQuick(string PetitionType, DateTime? DateStart, DateTime? DateEnd, string CompCode, string Days);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseApprover>> GetPersonalLicenseApprover(string licenseType);

        [OperationContract]
        DTO.ResponseService<IEnumerable<DateTime>> GetLicenseRequestOicApprove(DTO.RangeDateRequest request);

        [OperationContract]
        DTO.ResponseService<DataSet> GetMoveCompanyIn(string LicenseType, string CompCode, DateTime DateStart, DateTime DateEnd);

        [OperationContract]
        DTO.ResponseService<DataSet> GetMoveCompanyOut(string LicenseType, string CompCode, DateTime DateStart, DateTime DateEnd);


        [OperationContract]
        DTO.ResponseService<DataSet> GetLicenseStatisticsReport(string LicenseTypeCode, string StartDate1, string EndDate1, string StartDate2, string EndDate2);


        [OperationContract]
        DTO.ResponseService<DataSet> GetSumLicenseStatisticsReport(string StartDate1, string EndDate1);
        
        [OperationContract]
        DTO.ResponseService<DataSet> GetTopCompanyMoveOut(string LicenseType, DateTime? DateStart, DateTime? DateEnd);

        [OperationContract]
        DTO.ResponseService<List<DTO.SubPaymentHead>> GetIndexSubPaymentH(string groupReqNo);

        [OperationContract]
        DTO.ResponseService<List<DTO.SubPaymentDetail>> GetIndexSubPaymentD(string headReqNo);

        [OperationContract]
        DTO.ResponseService<List<DTO.PersonLicenseDetail>> GetIndexLicenseD(string uploadGroupNo);

        [OperationContract]
        DTO.ResponseService<DataSet> GetReplacementReport(string LicenseTypeCode, string Compcode, string Replacement, string StartDate, string EndDate);

        [OperationContract]
        DTO.ResponseService<DataSet> GetLincse0304(string lincense);

        [OperationContract]
        DTO.ResponseService<string> AddLincse0304(Dictionary<string, string> lincense);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigDocument>> GetLicenseConfigByPetition(string petitionType, string licenseType);

        [OperationContract]
        DTO.ResponseMessage<bool> GetAgentCheckTrain(string id);

        [OperationContract]
        DTO.ResponseService<DataSet> GetObtainLicenseByIdCard(string idCard);

        [OperationContract]
        DTO.ResponseService<DataSet> GetLicenseDetailByCriteria(DateTime dateStart, DateTime dateEnd, string IdCardNo, string Names, string Lastname, string LicenseType, string CompCode, int Page, int RowPerPage, Boolean isCount);

        [OperationContract]
        DTO.ResponseService<string> GenZipFileLicenseByIdCardNo(List<DTO.GenLicenseDetail> LicenseDetail, String username);

        [OperationContract]
        DTO.ResponseMessage<bool> ChkLicenseAboutActive(string idCard, string licenseType);
    }
}

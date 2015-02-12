using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.IO;
using IAS.DTO;

namespace IAS.DataServices.Applicant
{
    [ServiceContract]
    public interface IApplicantService
    {
        [OperationContract]
        DTO.ResponseMessage<bool> Insert(DTO.Applicant appl);

        [OperationContract]
        DTO.ResponseMessage<bool> Update(DTO.Applicant appl);

        [OperationContract]
        DTO.ResponseService<DTO.Applicant> GetById(string Id);

        [OperationContract]
        DTO.ResponseMessage<bool> Delete(string Id);

        [OperationContract]
        DTO.ResponseService<DTO.SummaryReceiveApplicant>
            InsertAndCheckApplicantGroupUpload(InsertAndCheckApplicantGroupUploadRequest request);
        [OperationContract]
        DTO.ResponseMessage<bool> UpdateApplicantGroupUpload(DTO.ApplicantTemp exam);

        [OperationContract]
        DTO.ResponseService<string> ApplicantGroupUploadToSubmit(string groupId, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<DataSet> GetApplicantByCriteria(DTO.RegistrationType userRegType, string compCode,
                                                            string idCard, string testingNo,
                                                            string firstName, string lastName,
                                                            DateTime? startDate, DateTime? toDate,
                                                            string paymentNo, string billNo,
                                                            int RowPerPage, int pageNum, Boolean Count, string license, string time, string examPlaceGroupCode, string examPlaceCode, string chequeNo, string examResult, DateTime? startCandidates, DateTime? endCandidates);

        [OperationContract]
        DTO.ResponseService<string> GetApplicantByCriteriaSendMail(DTO.RegistrationType userRegType, string compCode,
                                                            string idCard, string testingNo,
                                                            string firstName, string lastName,
                                                            DateTime? startDate, DateTime? toDate,
                                                            string paymentNo, string billNo,
                                                            int RowPerPage, int pageNum, Boolean Count, string license, string time, string examPlaceGroupCode, string examPlaceCode, string chequeNo, string examResult, DateTime? startCandidates, DateTime? endCandidates, string address, string name, string email);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetApplicantById(string applicantCode, string testingNo, string examPlaceCode);

        [OperationContract]
        DTO.ResponseService<DTO.ApplicantTemp>
            GetApplicantUploadTempById(string uploadGroupNo, string seqNo);

        [OperationContract]
        DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>
            GetApplicantGroupUploadByGroupUploadNo(string groupUploadNo);

        [OperationContract]
        DTO.ResponseService<string> InsertSingleApplicant(List<DTO.ApplicantTemp> app, string userId);


        [OperationContract]
        DTO.ResponseService<DTO.ApplicantInfo> GetApplicantInfo(string applicantCode,
                                                                       string testingNo,
                                                                       string examPlaceCode, int RowPerPage, int num_page, Boolean Count);

        //[OperationContract]
        //DTO.ResponseService<DataSet> GetApplicantByLicenseType(string licenseType);

        //[OperationContract]
        //DTO.ResponseService<DataSet> PersonGetApplicant(string idCard);


        [OperationContract]
        DTO.ResponseService<DataSet> GetRequestEditApplicant(DTO.RegistrationType userRegType, string idCard, string testingNo, string CompCode);

        [OperationContract]
        DTO.ResponseService<DataSet> GetApplicantChangeMaxID();

        [OperationContract]
        DTO.ResponseMessage<bool> InsertApplicantChange(DTO.ApplicantChange appChange);

        [OperationContract]
        DTO.ResponseService<DataSet> GetHistoryApplicant(DTO.RegistrationType userRegType,
                                                                  string idCard, string testingNo, string CompCode, string ExamPlaceCode, string Status, int pageNo, int recordPerPage, Boolean Count, string Asso, string oic);

        [OperationContract]
        DTO.ResponseService<DTO.Applicant> GetApplicantDetail(int applicantCode, string testingNo, string examPlaceCode);



        [OperationContract]
        DTO.ResponseService<DataSet> GetApproveEditApplicant(DTO.RegistrationType userRegType,
                                                                 string idCard, string testingNo, string CompCode, string ExamPlaceCode, string Status, int pageNo, int recordPerPage, Boolean Count, string membertype, string Asso, string oic);

        [OperationContract]
        DTO.ResponseService<DataSet> GetApplicantTLogMaxID();

        [OperationContract]
        DTO.ResponseService<DataSet> GetApplicantTtoLog(DTO.RegistrationType userRegType,
                                                                 string idCard, string testingNo, string CompCode);


        [OperationContract]
        DTO.ResponseMessage<bool> InsertApplicantTLog(DTO.ApplicantTLog appTLog);


        [OperationContract]
        DTO.ResponseMessage<bool> InsertAttrachFileApplicantChange(List<DTO.AttachFileApplicantChange> appAttachFileChange, DTO.UserProfile userProfile, DTO.ApplicantChange appChange);


        [OperationContract]
        DTO.ResponseService<DataSet> GetAttachFileAppChange(DTO.RegistrationType userRegType, string changeid);


        [OperationContract]
        DTO.ResponseService<IList<DTO.AttachFileApplicantChangeEntity>> GetAttatchFilesAppChangeByIDCard(string idcard, int changeid);

        [OperationContract]
        DTO.ResponseService<DataSet> GetApproveAppForStatus(DTO.RegistrationType userRegType,
                                                                string idcard, string status, string asso, string oic);




        [OperationContract]
        DTO.ResponseMessage<bool> SendMailAppChange(string idcard, string TestingNo, string CompCode);


        [OperationContract]
        DTO.ResponseService<DataSet> GetCheckIDAppT(string idcard, string TestingNo, string CompCode);

        [OperationContract]
        DTO.ResponseService<DataSet> getManageApplicantCourse(string LicenseType, string StartExamDate, string EndExamDate, string Place, string PlaceName, string TimeExam, string TestingNO, int resultPage, int PAGE_SIZE, Boolean Count);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamPlaceByLicenseAneOwner(string owner, string license);


        [OperationContract]
        DTO.ResponseService<DataSet> GetApplicantFromTestingNoForManageApplicant(string TestingNo, string ConSQL, int resultPage, int PAGE_SIZE, Boolean Count);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamRoomByTestingNoforManage(string testingNo, string PlaceCode);

        [OperationContract]
        DTO.ResponseMessage<bool> SaveExamAppRoom(List<string> Manage_App, string room, string testingNo, string PaymentNo, Boolean AutoManage, string UserId);


        [OperationContract]
        DTO.ResponseMessage<bool> CancleExamApplicantManage(List<string> Manage_App, string testingNo);

        [OperationContract]
        string AddtoDBRoom(List<string> Manage_App, string room, string testingNo, string UserId);

        [OperationContract]
        string GetQuantityBillPerPageByConfig();

        [OperationContract]
        DTO.ResponseMessage<bool> CheckApplicantIsDuplicate(string TestingNo, string idcard, DateTime testTingDate, string testTimeCode, string examPlaceCode);

        [OperationContract]
        DTO.ResponseService<List<string>> CheckApplicantExamDup(string idcard);

        [OperationContract]
        DTO.ResponseMessage<bool> ValidateApplicantSingleBeforeSubmit(ValidateApplicantSingleBeforeSubmitRequest request);

        [OperationContract]
        DTO.ResponseMessage<bool> ValidateApplicantTestCenter(string TestingNo, string idcard, DateTime testTingDate, string testTimeCode, string examPlaceCode);

        [OperationContract]
        DTO.ResponseMessage<bool> IsPersonCanApplicant(IsPersonCanApplicantRequest request);

        [OperationContract]
        DTO.ResultValidateApplicant ValidateApplicantBeforeSaveList(ValidateApplicantBeforeSaveListRequest request);

    }
}



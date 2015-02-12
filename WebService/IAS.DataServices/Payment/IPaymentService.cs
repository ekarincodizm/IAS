using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using IAS.DTO;

namespace IAS.DataServices.Payment
{
    [ServiceContract]
    public interface IPaymentService
    {

        [OperationContract]
        string CreatePdf(string[] fileNames);

        [OperationContract]
        string CreateZip(string parthpdf);
        
        [OperationContract]
        DTO.ResponseService<string> GetBillNo(string user_id, string doc_date,
                                              string doc_code, string doc_type,
                                              string date_mode);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetSubGroup(string paymentType, DateTime? startDate, DateTime? toDate,
                        DTO.UserProfile userProfile, string CompanyCode, int pageNo, int recordPerPage, string CountTotalRecord);

        [OperationContract]
        DTO.ResponseMessage<bool> SetSubGroup(List<DTO.OrderInvoice> subGroups,
                                              string userId, string compCodestring,string typeUser);

        [OperationContract]
        DTO.ResponseService<string> SetSubGroupSingleLicense(List<DTO.SubGroupPayment> subGroups,
                                                    string userId, string compCode, out string groupHeaderNo);
        [OperationContract]
        DTO.ResponseService<DataSet> GetGroupPayment(string compCode, DateTime? startDate, DateTime? EndDate, string UserT,
string CompanyCode, int pageNo, int recordPerPage, string Count);

        [OperationContract]
        DTO.ResponseMessage<bool> CreatePayment(List<DTO.OrderInvoice> reqList, string remark,
                                                       string paymentId,
                                                       string userId,
                                                       string compCode, out string groupRequestNo);
        [OperationContract]
        DTO.ResponseMessage<bool> NewCreatePayment(List<DTO.OrderInvoice> Groups, string remark,
                                                     string userId,
                                                     string compCode, string dayExp, out string groupRequestNo);
        [OperationContract]
        DTO.ResponseService<DataSet> GetAllGroupPayment(string compCode,
                                                        DateTime? startDate, DateTime? toDate,
                                                        string paymentCode);
        [OperationContract]
        DTO.ResponseService<DataSet> GetGroupPaymentByCriteria(string compCode,
                                                        DateTime? startDate, DateTime? toDate,
                                                        string paymentCode);

        [OperationContract]
        DTO.ResponseService<DataSet> GetSinglePayment(string compCode,
                                                        DateTime? startDate, DateTime? toDate,
                                                        string paymentCode,DateTime? startExamDate,DateTime? EndExamDate,string licenseType,string testingNo, string para, int pageNo, int recordPerPage, string Totalrecoad);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetSubPaymentByHeaderRequestNo(string hearReqNo, string CountRecord, int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<DataSet> GetApplicantNoPayment(string testingDate, string testing_no, string examPlace, DateTime? startDate, DateTime? toDate, string GroupNo, int resultPage, int PageSizeDetail, Boolean Count);
        
         [OperationContract]
        DTO.ResponseService<DataSet> GetApplicantNoPaymentHeadder(DateTime? startDate, DateTime? toDate, 
                        string testingDate, string testNo, string ExamPlaceCode, 
                        int resultPage, int PageSize, Boolean Count,Boolean IsAuto=false);
                
        [OperationContract]
        DTO.ResponseService<DataSet>
            GetApplicantNoPayById(string applicantCode, string testingNo, string examPlaceCode);

        //[OperationContract]
        //DTO.ResponseMessage<bool> CancelApplicants(List<DTO.CancelApplicant> applicants);

        [OperationContract]
        DTO.ResponseMessage<bool> CancelApplicantsHeader(List<DTO.AppNoPay> GroupNo,Boolean IsAuto =false);

        [OperationContract]
        DTO.ResponseService<DataSet>
                GetReportNumberPrintBill(string idCard, string petitionTypeCode,
                                         string firstName, string lastName, int resultPage, int PageSize, Boolean CountAgain);

        [OperationContract]
        DTO.ResponseService<DataSet>
                GenPaymentNumberTable(string compCode, DateTime? startDate, DateTime? toDate, string paymentCodestring, string CountRecord, int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseMessage<bool> GenPaymentNumber(string paymentCodestring, string UID, string receiptNo);
        
        [OperationContract]
        DTO.ResponseMessage<bool> GenReceiptAll(List<DTO.GenReceipt> GenReceipt);
       // [OperationContract]
       //string
       //         AddGenReceiveNumbertoDB(string HeadNo,string UID,string st_date,string ed_date,string ID);

         [OperationContract]
       string
                AddStatusReceiveCompletetoDB(string HeadNo, string UID, string strPath, string IDcard, string hashing, Guid G, string receiveNo,Int64 FileSize);

         [OperationContract]
         DTO.ResponseService<DataSet>
             GetDataFromSub_D_T(string HeadNo, string UID, string HeadOrDetail);


            [OperationContract]
            byte[] Signature_Img(string imgPath);

           
                [OperationContract]
        DTO.ResponseService<DataSet>
            GetDataPayment_BeforeSentToReport(string H_req_no, string IDcard, string PayNo);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetPaymentByCriteria(DTO.UserProfile userProfile,
                                 string paymentType,
                                 DateTime? startDate, DateTime? toDate,
                                 string idCard, string billNo,
                                 int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetPaymentDetailByGroup(int type,
                                 string Gcode,
                                 string Ccode, DateTime? startDate,
                                 DateTime? toDate, int RowPerPage,
                                 int num_page, Boolean Count,string CompCode);
      
        
        [OperationContract]
        DTO.ResponseService<DataSet>
            GetGroupExam(int type, string code);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetExamCode(string code);

        [OperationContract]
        DTO.ResponseService<DTO.PaymentDetail>
            GetPaymentDetail(string applicantCode, string testingNo, string examPlace_code,
                             string licenseNo, string renewTime, bool isApplicant);

        [OperationContract]
        DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>
            InsertAndCheckPaymentUpload(DTO.UploadData data, string fileName, string userId);

        [OperationContract]
        DTO.ResponseService<DTO.BankTransDetail> GetTempBankTransDetail(string headerId, string Id);

        [OperationContract]
        DTO.ResponseService<DTO.SubPaymentHead> GetSubPaymentHeadByHeadRequestNo(string headReqNo);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetCountPaymentDetailByCriteria(DTO.UserProfile userProfile,
                                    string paymentType,
                                    string order,
                                    DateTime? startDate, DateTime? toDate,
                                    string idCard, string billNo, string ViewYear);
                              

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetPaymentDetailByCriteria(DTO.UserProfile userProfile,
                                    string paymentType,
                                    string order,
                                    DateTime? startDate, DateTime? toDate,
                                    string idCard, string billNo,
                                    int pageNo, int recordPerPage, string ViewYear);

        [OperationContract]
        DTO.ResponseMessage<bool> PlusPrintDownloadCount(List<DTO.SubPaymentDetail> subPaymentDetail);

        [OperationContract]
        DTO.ResponseMessage<bool> PrintDownloadCount(List<DTO.SubPaymentDetail> subPaymentDetail, string id_card, string createby);

        [OperationContract]
        DTO.ResponseMessage<bool> Zip_PrintDownloadCount(string[] rcvPath, string EventZip, string id_card, string createby);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetDetailSubPayment(string hearReqNo, int pageNo, int recordPerPage, string CountRecord);


        [OperationContract]
        DTO.ResponseService<String> SubmitBankTrans(DTO.ImportBankTransferRequest request);

        [OperationContract]
        DTO.ResponseService<DataSet> getGroupDetail(string group_reuest);

        [OperationContract]
        DTO.ResponseService<DataSet> getNamePaymentBy(string group_reuest);

        [OperationContract]
        DTO.ResponseService<DataSet> getBindbillPaymentExam(string groupRequestNo, string testNo, string appCode, string examPlaceCode);

        [OperationContract]
        DTO.ResponseService<DTO.ReferanceNumber> CreateReferanceNumber();

        [OperationContract]
        DTO.ResponseMessage<bool> Insert12T(List<DTO.GenLicense> GenLicense);
   
        [OperationContract]
        DTO.ResponseService<IEnumerable<DateTime>> GetLicenseGroupRequestPaid(RangeDateRequest request);

        [OperationContract]
        DTO.ResponseService<DataSet> GetRcvHisDetail(string RcvId, string EventCode, string st_num, string en_num);

        [OperationContract]
        DTO.ResponseService<DataSet> GetPaymentExpireDay();

        [OperationContract]
        DTO.ResponseMessage<bool> UpdatePaymentExpireDay(List<DTO.ConfigPaymentExpireDay> ls, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateCountDownload(string UserId, object FileName, string Event);

        [OperationContract]
        DTO.ResponseService<DataSet> GetPaymentLicenseAppove(string petitonType, string IdCard, string groupNo, DateTime startDate, DateTime endDate, 
           string FirstName,string LastName , string CountPage, int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<DataSet> GetPaymentExamDetail(string GroupRequestNo);
        
        [OperationContract]
        DTO.ResponseService<DataSet> GetConfigViewFile();

        [OperationContract]
        DTO.ResponseService<DataSet> GetNonPayment(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode,DateTime? startExamDate, DateTime? EndExamDate, string licenseType, string testingNo, string para, int pageNo, int recordPerPage, string Totalrecoad);

        [OperationContract]
        DTO.ResponseService<DataSet> GetConfigViewBillPayment();

        [OperationContract]
        DTO.ResponseService<DTO.GetPaymentByRangeResponse> GetPaymentByRange(DTO.GetPaymentByRangeRequest request);

        [OperationContract]
        DTO.ResponseMessage<bool> CancelGroupRequestNo(string GroupRequestNo);


        [OperationContract]
        DTO.ResponseMessage<bool> UpdatePrintGroupRequestNo(List<string> reqList);

        [OperationContract]
        DTO.ResponseMessage<bool> CheckHolidayDate(string strDate);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetSubPaymentByGenPaymentForm(string hearReqNo, string CountRecord, int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetSubGroupDetail(string paymentType, string UploadGroupNo, int pageNo, int recordPerPage, string CountTotalRecord);



        #region AutoCancleApplicant
        //[OperationContract]
        //DTO.ResponseMessage<bool>Auto_CancleApplicant();

        #endregion AutoCancleApplicant

        [OperationContract]
        DTO.ResponseMessage<bool> SavePaymentNoFile(DateTime Date_Time, int CIT, int KTB, string userID);

        [OperationContract]
        DTO.ResponseService<DataSet>
                GetEndOfPay(string compCode, DateTime? startDate, DateTime? toDate, string typeEnd, string CountRecord, int pageNo, int recordPerPage);

        [OperationContract]
        DTO.ResponseService<string> SetSubGroupSingleApplicant(List<DTO.OrderInvoice> subGroups,
                                                    string userId,  out string groupHeaderNo);

        [OperationContract]
        DTO.ResponseMessage<bool> Auto_CancelAppNoPay(DateTime stDate, DateTime enDate);

        [OperationContract]
        DTO.ResponseService<DataSet> Auto_GetApplicantNoPaymentHeadder(DateTime? startDate, DateTime? toDate);

        [OperationContract]
        DTO.ResponseService<string> GetLastEndDate();

        [OperationContract]
        DTO.ResponseService<DataSet>
                GetReceiptSomePay(string paymentNo, string HeadrequestNo);

        [OperationContract]
        DTO.ResponseService<PaymentNotCompleteResponse> PaymentNotComplete(PaymentNotCompleteRequest request);

        [OperationContract]
        bool ScheduleEvent(DateTime PKdatetime, string Name, DateTime stDate, DateTime enDate, string actionEvent);

        [OperationContract]
        DTO.ResponseService<string> ReSubmitBankTrans(DTO.ImportBankTransferRequest request);

        [OperationContract]
        DTO.ResponseService<DataSet> GetCheckFileSize(string PetitionTypeName, string StartDate, string EndDate);

        [OperationContract]
        DTO.ResponseService<DataSet> GetDownloadReceiptHistory(string idCard, string petitionTypeCode, string firstName, string lastName);

        [OperationContract]
        DTO.ResponseService<DataSet> getGroupDetailLicense(string group_reuest);

        [OperationContract]
        DTO.ResponseService<DataSet> GetRecriptByHeadRequestNoAndPaymentNo(string HeadNo, string PaymentNo);
    }
}  
    

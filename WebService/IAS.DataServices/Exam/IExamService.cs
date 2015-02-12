using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using IAS.DTO.Exams;

namespace IAS.DataServices.Exam
{
    [ServiceContract]
    public interface IExamService
    {
        [OperationContract]
        DTO.ResponseMessage<bool> InsertExam(DTO.ExamSchedule ent);

         
        [OperationContract]
        DTO.ResponseMessage<bool> UpdateExam(DTO.ExamSchedule ent);

        [OperationContract]
        DTO.ResponseMessage<bool> DeleteExam(string testingNo, string examPlaceCode);

        [OperationContract]
        DTO.ResponseService<string> GetSeatAmount(string examPlaceCode);

        [OperationContract]
        DTO.ResponseService<string> GetExamFee();

        [OperationContract]
        DTO.ResponseService<DataSet>
            GetExamByCriteria(string examPlaceGroupCode, string examPlaceCode,
                                       string licenseTypeCode, string agentType, string yearMonth,
                                       string timeCode, string testingDate, int resultPage, int PageSize, Boolean CountAgain, string Owner = "");



        [OperationContract]
        DTO.ResponseService<DTO.ExamSchedule>
            GetExamByTestingNoAndPlaceCode(string testingNo, string placeCode);

        [OperationContract]
        DTO.ResponseService<List<DateTime>> GetExamByYearMonth(string yearMonth);

        [OperationContract]
        DTO.ResponseService<DTO.ExamSchedule> GetExamByTestingNo(string testingNo);

        [OperationContract]
        DTO.ResponseService<DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>>
            InsertAndCheckExamResultUpload(string fileName,
                                           string userId, string Default_TESTING_NO);

        [OperationContract]
        DTO.ResponseService<DTO.ExamResultTempEdit> GetExamResultTempEdit(string uploadGroupNo, string seqNo);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateExamResultEdit(DTO.ExamResultTempEdit exam);

        //[OperationContract]
        //DTO.ResponseMessage<bool> ExamResultUploadToSubmit(string groupId, string userId, DateTime? expireDate);

        [OperationContract]
        DTO.ResponseMessage<bool> ExamResultUploadToSubmitNew(string groupId, string userId, DateTime? expireDate, string TestingNo);


        [OperationContract]
        DTO.ResponseMessage<bool> CanChangeExam(string testingNo, string examPlaceCode);

        [OperationContract]
        DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ExamResultTemp>>
            GetExamResultUploadByGroupId(string groupId);

        [OperationContract]
        DTO.ResponseMessage<bool> IsRightTestingNo(string testingNo);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamByTestCenter(string examPlaceGroupCode, string examPlaceCode,
                                                               string licenseTypeCode, string yearMonth,
                                                               string timeCode, string testingDate, string compcode);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamMonthByCriteria(string examPlaceGroupCode, string examPlaceCode,
                                                                       string licenseTypeCode, string yearMonth,
                                                                       string timeCode, string testingDate, string Owner = "");

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamByCriteriaDefault(string examPlaceGroupCode, string examPlaceCode,
                                                                       string licenseTypeCode, string agentType, string yearMonth,
                                                                       string timeCode, string testingDate, int resultPage, int PageSize, Boolean CountAgain, string Owner = "");

        [OperationContract]
        DTO.ResponseService<DataSet> GetSubject_List(string lic_type_code);

        [OperationContract]
        DTO.ResponseService<List<DTO.GBHoliday>> GetHolidayList(int page, int count);

        [OperationContract]
        DTO.ResponseService<DTO.GBHoliday> GetHoliday(string date);

        [OperationContract]
        DTO.ResponseService<string> AddHoliday(DTO.GBHoliday holiday);

        [OperationContract]
        DTO.ResponseService<string> DeleteHoliday(string date);

        [OperationContract]
        DTO.ResponseService<string> UpdateHoliday(DTO.GBHoliday holidate);

        [OperationContract]
        DTO.ResponseService<List<DTO.GBHoliday>> SearchHoliday(string search, int page, int count);

        [OperationContract]
        DTO.ResponseService<List<DTO.GBHoliday>> GetHolidayListByYearMonth(string yearMonth);

        [OperationContract]
        DTO.ResponseService<List<DTO.LicenseTyperDropDrown>> GetLicensetypeList();

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamRoom();

        [OperationContract]
        DTO.ResponseMessage<bool> InsertExamRoom(DTO.ConfigExamRoom ent, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateExamRoom(DTO.ConfigExamRoom ent, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<List<DTO.Subjectr>> GetSubjectrList(string licensecode);

        [OperationContract]
        DTO.ResponseService<string> AddSubject(DTO.Subjectr subject);

        [OperationContract]
        DTO.ResponseService<string> UpdateSubject(DTO.Subjectr subject);

        [OperationContract]
        DTO.ResponseService<string> DeleteSubject(DTO.Subjectr subject);

        [OperationContract]
        DTO.ResponseService<List<DTO.AgentType>> GetAgentTypeList();

        [OperationContract]
        DTO.ResponseService<List<DTO.LicenseTypet>> GetLicenseList(string agentType);

        [OperationContract]
        DTO.ResponseService<string> UpdateLicenseType(DTO.LicenseTypet licensetype);

        [OperationContract]
        DTO.ResponseService<string> AddLicenseType(DTO.LicenseTypet licensetype);

        [OperationContract]
        DTO.ResponseService<string> DeleteLicensetype(string licensecode);

        [OperationContract]
        DTO.ResponseService<List<DTO.DataItem>> GetExamRoomByPlaceCode(string code);


        [OperationContract]
        DTO.ResponseService<string> GetSeatAmountRoom(string roomcode, string ExamPlace);

        [OperationContract]
        DTO.ResponseService<List<DTO.ExamSubLicense>> GetExamRoomByLicenseNo(string No, string Place);

        [OperationContract]
        DTO.ResponseMessage<bool> InsertExamAndRoom(DTO.ExamSchedule ent, List<DTO.ExamSubLicense> entsub);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateExamAndRoom(DTO.ExamSchedule ent, List<DTO.ExamSubLicense> entsub);


        #region ExamTime

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamRoomByPlaceCodeAndTimeCode(string code, string Timetxt, string dDate, List<DTO.ExamSubLicense> oldCode, Boolean Del, string testingNoo);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamTime(string st_hr, string st_min, string en_hr, string en_min, int pageNo, int recordPerPage, Boolean Count);

        [OperationContract]
        DTO.ResponseService<string> GetCountSearch(string st_hr, string st_min, string en_hr, string en_min);

        [OperationContract]
        DTO.ResponseMessage<bool> Add_Time(string st_hr, string st_min, string en_hr, string en_min, string userID);

        [OperationContract]
        DTO.ResponseMessage<bool> Del_Time(string Key, string UserID);

        [OperationContract]
        DTO.ResponseService<DataSet> getExamTimeShow(string ID);

        [OperationContract]
        DTO.ResponseService<DataSet> GetAssoLicense(string AssoCode);

        [OperationContract]
        DTO.ResponseMessage<bool> DelExamRoom(string Room);

        [OperationContract]
        DTO.ResponseService<DataSet> GetPlaceDetailByPlaceCode_noCheckActive(string PlaceCode);

        #endregion ExamTime

        #region examgroup

        [OperationContract]
        DTO.ResponseService<List<DTO.Subjectr>> GetSubjectGroup(string p);

        [OperationContract]
        DTO.ResponseService<string> AddExamGroup(DTO.ConditionGroup conditiongroup);

        [OperationContract]
        DTO.ResponePage<List<DTO.SubjectGroup>> GetSubjectGroupSearch(string p, int Page, int record);

        [OperationContract]
        DTO.ResponseService<List<DTO.SubjectGroupD>> GetSubjectInGroup(string p);

        #endregion

        #region subjectgroup

        [OperationContract]
        DTO.ResponseService<List<DTO.GroupSubject>> GetSubjectGroupList(string p);

        [OperationContract]
        DTO.ResponseService<string> ActiveConditionGroup(string p, string license);

        [OperationContract]
        DTO.ResponseService<string> AddSubjectGroup(DTO.GroupSubject examgroup);

        [OperationContract]
        DTO.ResponseService<string> DeleteSubjectGroup(string p);

        [OperationContract]
        DTO.ResponseService<string> UpdateSubjectGroup(DTO.GroupSubject examgroup);

        #endregion

        [OperationContract]
        DTO.ResponseService<string> GenLicenseNo(string PlaceCode, string UserId);

        [OperationContract]
        DTO.ResponseMessage<bool> SaveSetApplicantRoom(DTO.SaveSetApplicantRoom lr,string Event);

        [OperationContract]
        DTO.ResponseService<string> GetCourseNumber(string licenseCode);

        [OperationContract]
        DTO.ResponseMessage<bool> SaveDeleteApplicantRoom(string testing_no, string ExampleaceCode, string USERID);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamPlaceAndDetailFromProvinceAndGroupCode(string province, string placeG, string Group, int pageNo, int recordPerPage, Boolean Count);

        [OperationContract]
        DTO.ResponseMessage<bool> SavePlace(string PlaceG, string Province, string Code, string Place, string Seat, bool Free, string UserID, Boolean Addnew, string GroupType);

        [OperationContract]
        DTO.ResponseMessage<bool> DelPlace(string Code, string UserID);

        [OperationContract]
        DTO.ResponseService<DataSet> GetGVExamRoomByPlaceCode(string code, int pageNo, int recordPerPage, Boolean Count);

        [OperationContract]
        DTO.ResponseService<string> SumSeat(string placeCode);

        [OperationContract]
        DTO.ResponseService<string> CountCountSeatUse(string testing_no);

        [OperationContract]
        DTO.ResponseMessage<Boolean> CheckUsedPlaceGroup(string ID);

        [OperationContract]
        DTO.ResponseService<DataSet> GetddlGroupType(string PlaceCode);


        [OperationContract]
        DTO.ResponseService<string> SumSeatFromPlace(string ID, string Room);

        [OperationContract]
        string GetTestingNoFrom_fileImport(DTO.ExamHeaderResultTemp head);

        [OperationContract]
        DTO.ResponseService<List<DTO.ConfigEntity>> ManageApplicantIn_OutRoom();

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateManageApplicantIn_OutRoom(List<DTO.ConfigEntity> configs);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamStatistic(string LicenseType, DateTime? DateStart, DateTime? DateEnd);

        [OperationContract]
        DTO.ResponseMessage<bool> UpdateExamCondition(List<DTO.ExamLicense> exam);

        [OperationContract]
        DTO.ResponseService<DataSet> GetExamLicenseByCriteria(string testingNo, string testingDate,
                                                                 int resultPage, int PageSize, Boolean CountAgain);
        [OperationContract]
        DTO.ResponseMessage<bool> IsCanSeatRemainSingle(string testingNo, string examPlaceCode);

        [OperationContract]
        DTO.ResponseService<DTO.Exams.CarlendarExamInitResponse> CarlendarExamInit(DTO.Exams.CarlendarExamInitRequest request);

        [OperationContract]
        DTO.ResponseService<GetExamByCriteriaResponse> GetExamCarlenderByCriteria(GetExamByCriteriaRequest request);

        [OperationContract]
        DTO.ResponseService<GetExamScheduleByCriteriaResponse> GetExamScheduleByCriteria(GetExamScheduleByCriteriaRequest request);
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using IAS.DTO;
using IAS.Utils;
using IAS.DTO.Exams;

namespace IAS.BLL
{
    public class ExamScheduleBiz
    {
        ExamService.ExamServiceClient svc;

        public ExamScheduleBiz()
        {
            svc = new ExamService.ExamServiceClient();
        }

        public DTO.ResponseService<string> GetSeatAmount(string examPlaceCode)
        {
            return svc.GetSeatAmount(examPlaceCode);
        }

        public DTO.ResponseService<string> GetExamFee()
        {
            return svc.GetExamFee();
        }

        public DTO.ResponseMessage<bool> CanChangeExam(string testingNo, string examPlaceCode)
        {
            return svc.CanChangeExam(testingNo, examPlaceCode);
        }

        public DTO.ResponseService<DataSet>
            GetExamByCriteria(string examPlaceGroupCode, string examPlaceCode,
                                       string licenseTypeCode, string agentType, string yearMonth,
                                       string timeCode, DateTime? testingDate, int resultPage, int PageSize, Boolean CountAgain, string Owner = "")
        {
            string tsDate = (testingDate == null ? string.Empty : Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
            return svc.GetExamByCriteria(examPlaceGroupCode, examPlaceCode,
                                                  licenseTypeCode, agentType, yearMonth,
                                                  timeCode, tsDate, resultPage, PageSize, CountAgain,Owner);
        }


        public DTO.ResponseService<DataSet>
           GetExamByCriteria(string examPlaceGroupCode, string examPlaceCode,
                                      string licenseTypeCode,  string yearMonth,
                                      string timeCode, DateTime? testingDate, int resultPage, int PageSize, Boolean CountAgain,string Owner="")
        {
            string tsDate = (testingDate == null ? string.Empty : Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
            return svc.GetExamByCriteria(examPlaceGroupCode, examPlaceCode,
                                                  licenseTypeCode, "", yearMonth,
                                                  timeCode, tsDate, resultPage, PageSize, CountAgain,Owner);
        }

      
        public DTO.ResponseService<DataSet>
           GetExamByTestCenter(string examPlaceGroupCode, string examPlaceCode,
                                      string licenseTypeCode, string yearMonth,
                                      string timeCode, DateTime? testingDate, string compcode)
        {
            string tsDate = (testingDate == null ? string.Empty : Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
            return svc.GetExamByTestCenter(examPlaceGroupCode, examPlaceCode,
                                                  licenseTypeCode, yearMonth,
                                                  timeCode, tsDate, compcode);
        }

        public DTO.ResponseService<DateTime[]> GetExamByYearMonth(string yearMonth)
        {
            return svc.GetExamByYearMonth(yearMonth);
        }

        public DTO.ResponseMessage<bool> InsertExam(DTO.ExamSchedule ent)
        {
            return svc.InsertExam(ent);
        }

        public DTO.ResponseMessage<bool> UpdateExam(DTO.ExamSchedule ent)
        {
            return svc.UpdateExam(ent);
        }

        public DTO.ResponseMessage<bool> DeleteExam(string testingNo, string examPlaceCode)
        {
            return svc.DeleteExam(testingNo, examPlaceCode);
        }

        public DTO.ResponseService<DTO.ExamSchedule> GetExamByTestingNoAndPlaceCode(string testingNo, string placeCode)
        {
            return svc.GetExamByTestingNoAndPlaceCode(testingNo, placeCode);
        }


        /// <summary>
        /// ตรวจสอบรหัสการสอบว่ามีในระบบหรือไม่
        /// </summary>
        /// <param name="testingNo">รหัสการสอบ</param>
        /// <returns>bool true=มี, false=ไม่มี</returns>
        public bool IsRightTestingNo(string testingNo)
        {
            var res = svc.IsRightTestingNo(testingNo);
            return res.ResultMessage;
        }

        public DTO.ResponseService<DataSet>
           GetExamMonthByCriteria(string examPlaceGroupCode, string examPlaceCode,
                                      string licenseTypeCode, string yearMonth,
                                      string timeCode, DateTime? testingDate,string Owner="")
        {
            string tsDate = (testingDate == null ? string.Empty : Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
            return svc.GetExamMonthByCriteria(examPlaceGroupCode, examPlaceCode,
                                                  licenseTypeCode, yearMonth,
                                                  timeCode, tsDate,Owner);
        }

    
        public DTO.ResponseService<DataSet> GetExamByCriteriaDefault(string examPlaceGroupCode, string examPlaceCode,
                               string licenseTypeCode, string agentType, string yearMonth,
                               string timeCode, DateTime? testingDate, int resultPage, int PageSize, Boolean CountAgain,string Owner="")
        {
            string tsDate = (testingDate == null ? string.Empty : Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
            return svc.GetExamByCriteriaDefault(examPlaceGroupCode, examPlaceCode,
                                                  licenseTypeCode, agentType, yearMonth,
                                                  timeCode, tsDate, resultPage, PageSize, CountAgain,Owner);
        }

       

        public DTO.ResponseMessage<bool> InsertExamAndRoom(DTO.ExamSchedule ent, List<DTO.ExamSubLicense> entsub)
        {
            return svc.InsertExamAndRoom(ent, entsub.ToArray());
        }

        public DTO.ResponseMessage<bool> UpdateExamAndRoom(DTO.ExamSchedule ent, List<DTO.ExamSubLicense> entsub)
        {
            return svc.UpdateExamAndRoom(ent, entsub.ToArray()); 
        }

        public DTO.ResponseService<DataSet>  GetAssoLicense(string ComCode)
        {
            return svc.GetAssoLicense(ComCode);
        }



        public ResponseMessage<bool> SaveSetApplicantRoom(SaveSetApplicantRoom lr, string Event)
        {
            return svc.SaveSetApplicantRoom(lr,Event);
        }



        public DTO.ResponseService<string> GenLicenseNo(string PlaceCode,string UserId)
        {
            return svc.GenLicenseNo(PlaceCode, UserId);
        }

        public DTO.ResponseService<string> GetCourseNumber(string licenseCode)
        {
            return svc.GetCourseNumber(licenseCode);
        }

        public DTO.ResponseMessage<bool> SaveDeleteApplicantRoom(string testing_no,string ExampleaceCode,string USERID)
        {
            return svc.SaveDeleteApplicantRoom(testing_no, ExampleaceCode, USERID);
        }

        public DTO.ResponseService<DataSet> GetExamPlaceAndDetailFromProvinceAndGroupCode(string province, string placeG, string Group, int pageNo, int recordPerPage, Boolean Count)
        {
            return svc.GetExamPlaceAndDetailFromProvinceAndGroupCode(province, placeG,Group, pageNo, recordPerPage, Count);
                
        }

        public DTO.ResponseMessage<bool> SavePlace(string PlaceG, string Province, string Code, string Place, string Seat, bool Free, string UserId, Boolean Addnew,string GroupType)
        {
            return svc.SavePlace(PlaceG, Province, Code, Place, Seat, Free,UserId,Addnew,GroupType);
        }

        public DTO.ResponseMessage<bool> DelPlace(string Code, string UserID)
        {
            return svc.DelPlace( Code, UserID);
        }

        public DTO.ResponseService<DataSet> GetGVExamRoomByPlaceCode(string code, int pageNo, int recordPerPage, Boolean Count)
        {
            return svc.GetGVExamRoomByPlaceCode(code, pageNo, recordPerPage, Count);
        }

        public DTO.ResponseService<string> SumSeat(string placeCode)
        {
            return svc.SumSeat(placeCode);
        }

        public DTO.ResponseService<string> CountCountSeatUse(string testing_no)
        {
            return svc.CountCountSeatUse(testing_no);
        }

        public DTO.ResponseMessage<Boolean> CheckUsedPlaceGroup(string ID)
        {
            return svc.CheckUsedPlaceGroup(ID);
        }

        public DTO.ResponseService<string> SumSeatFromPlace(string ID,string Room)
        {
            return svc.SumSeatFromPlace(ID,Room );
        }

        #region ManageApplicantIn_OutRoom
        public DTO.ResponseService<DTO.ConfigEntity[]> ManageApplicantIn_OutRoom()
        {
            return svc.ManageApplicantIn_OutRoom();
        }

        public DTO.ResponseMessage<bool> UpdateManageApplicantIn_OutRoom(List<DTO.ConfigEntity> configs)
        {
            return svc.UpdateManageApplicantIn_OutRoom(configs.ToArray());
        }
        #endregion ManageApplicantIn_OutRoom

        public DTO.ResponseMessage<bool> UpdateExamCondition(List<DTO.ExamLicense> exam)
        {
            return svc.UpdateExamCondition(exam.ToArray());
        }

        public DTO.ResponseService<DataSet>
          GetExamLicenseByCriteria(string testingNo, string testingDate,
                                                                 int resultPage, int PageSize, Boolean CountAgain)
        {
            //string tsDate = (testingDate == null ? string.Empty : Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
            return svc.GetExamLicenseByCriteria( testingNo, testingDate, resultPage,  PageSize,  CountAgain);
        }

        public DTO.ResponseMessage<bool> IsCanSeatRemainSingle(string testingNo, string examPlaceCode)
        {
            return svc.IsCanSeatRemainSingle(testingNo, examPlaceCode);
        }

        public DTO.ResponseService<DTO.ExamSchedule> GetExamByTestingNo(string testingNo) 
        {
            return svc.GetExamByTestingNo(testingNo);
              
        }


        public DTO.ResponseService<DTO.Exams.CarlendarExamInitResponse> CarlendarExamInit(DTO.Exams.CarlendarExamInitRequest request)
        {
            return svc.CarlendarExamInit(request);
        }

        public DTO.ResponseService<DTO.Exams.GetExamScheduleByCriteriaResponse> GetExamScheduleByCriteria
                               (string examPlaceGroupCode, string examPlaceCode,
                                string licenseTypeCode, string agentType, String year, String month, String day,
                                string timeCode, String currentPage, String pageSize, String totalItem, String userId, string Owner = "")
        {
            //string tsDate = (testingDate == null ? string.Empty : Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
            DTO.Exams.GetExamScheduleByCriteriaRequest request = new GetExamScheduleByCriteriaRequest();
            request.UserId = userId;

            request.ExamCriteria = new ExamCriteriaDTO();
            request.ExamCriteria.ExamPlaceGroupCode = examPlaceGroupCode;
            request.ExamCriteria.ExamPlaceCode = examPlaceCode;

            request.ExamCriteria.LicenseTypeCode = licenseTypeCode;
            request.ExamCriteria.AgentType = agentType;
            Int32 y = 0;
            if (!Int32.TryParse(year, out y))
            {
                throw new ApplicationException("คุณระบุปีไม่ถูกต้อง.");
            }
            Int32 m = 0;
            if (!Int32.TryParse(month, out m))
            {

                throw new ApplicationException("คุณระบุเดือนไม่ถูกต้อง.");
            }
            else
            {
                if (m < 1 || m > 12)
                    throw new ApplicationException("คุณระบุเดือนไม่ถูกต้อง.");
            }
            Int32 d = -1;
            if (!String.IsNullOrWhiteSpace(day))
            {
                if (!Int32.TryParse(day, out d))
                {
                    throw new ApplicationException("คุณระบุวันไม่ถูกต้อง.");
                }
                try
                {
                    DateTime testingdate = new DateTime(y, m, d);
                }
                catch (Exception)
                {
                    throw new ApplicationException(String.Format("คุณระบุวันที่ไม่ถูกต้อง. {0}/{1}/{2}", y.ToString(), m.ToString(), d.ToString()));
                }
            }



            request.ExamCriteria.Year = (y > 2500) ? y - 543 : y;

            request.ExamCriteria.Month = m;

            request.ExamCriteria.Day = d;

            request.ExamCriteria.TimeCode = timeCode;

            request.ExamCriteria.OwnerCode = Owner;


            Int32 curPage = 0;
            Int32.TryParse(currentPage, out curPage);
            Int32 pSize = 0;
            Int32.TryParse(pageSize, out pSize);
            Int32 tItems = 0;
            Int32.TryParse(totalItem, out tItems);
            DTO.PagingInfo pageInfo = new PagingInfo() { CurrentPage = curPage, ItemsPerPage = pSize, TotalItems = tItems };
            request.PageInfo = pageInfo;

            return svc.GetExamScheduleByCriteria(request);
        }
        public DTO.ResponseService<DTO.Exams.GetExamByCriteriaResponse>
             GetExamByCriteria(DTO.Exams.GetExamByCriteriaRequest request)
        {
            //string tsDate = (request.TestingDate == null ? string.Empty : Convert.ToDateTime(request.TestingDate).ToString_yyyyMMdd());
            //return svc.GetExamCarlenderByCriteria(request);

            return svc.GetExamCarlenderByCriteria(request);
        }

    }
}

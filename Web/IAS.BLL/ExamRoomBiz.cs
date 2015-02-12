using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data;


namespace IAS.BLL
{
    public class ExamRoomBiz
    { 
        //lll
        private ExamService.ExamServiceClient svc;

        public ExamRoomBiz()
        {
            svc = new ExamService.ExamServiceClient();
        }

        public DTO.ResponseService<DataSet> GetExamRoom()
        {
            return svc.GetExamRoom();
        }

        public DTO.ResponseMessage<bool> InsertExamRoom(DTO.ConfigExamRoom ent, DTO.UserProfile userProfile)
        {
            return svc.InsertExamRoom(ent, userProfile);
        }

        public DTO.ResponseMessage<bool> UpdateExamRoom(DTO.ConfigExamRoom ent, DTO.UserProfile userProfile)
        {
            return svc.UpdateExamRoom(ent, userProfile);
        }

        public DTO.ResponseService<DTO.DataItem[]> GetExamRoomByPlaceCode(string code)
        {
            return svc.GetExamRoomByPlaceCode(code);
        }

        public DTO.ResponseService<string> GetSeatAmountRoom(string roomcode,string ExamPlace)
        {
            return svc.GetSeatAmountRoom(roomcode,ExamPlace);
        }

        public DTO.ResponseService<DTO.ExamSubLicense[]> GetExamRoomByLicenseNo(string No,string Place)
        {
            return svc.GetExamRoomByLicenseNo(No,Place);
        }

        public DTO.ResponseMessage<bool> Add_Time(string st_hr, string st_min, string en_hr, string en_min, string userID)
        {
            return svc.Add_Time(st_hr, st_min, en_hr, en_min, userID);
        }

        public DTO.ResponseMessage<bool> Del_Time(string Key, string UserID)
        {
            return svc.Del_Time(Key, UserID);
        }

        public DTO.ResponseService<DataSet> GetExamTime(string st_hr, string st_min, string en_hr, string en_min,int pageNo, int recordPerPage, Boolean Count)
        {
            return svc.GetExamTime(st_hr, st_min, en_hr, en_min, pageNo, recordPerPage, Count);
        }

        public DTO.ResponseService<string> GetCountSearch(string st_hr, string st_min, string en_hr, string en_min)
        {
            return svc.GetCountSearch(st_hr, st_min, en_hr, en_min);
        }

        public DTO.ResponseService<DataSet> getExamTimeShow(string ID)
        {
            return svc.getExamTimeShow(ID);
        }


        public DTO.ResponseService<DataSet> GetExamRoomByPlaceCodeAndTimeCode(string code, string timeTxt, string dDate, List<DTO.ExamSubLicense> oldCode, Boolean Del, string testingNoo)
        {
            return svc.GetExamRoomByPlaceCodeAndTimeCode(code, timeTxt, dDate,oldCode.ToArray(),Del,testingNoo);
        }

        public DTO.ResponseMessage<bool> DelExamRoom(string Room)
        {
            return svc.DelExamRoom(Room);
        }

        public DTO.ResponseService<DataSet> GetPlaceDetailByPlaceCode_noCheckActive(string PlaceCode)
        {
            return svc.GetPlaceDetailByPlaceCode_noCheckActive(PlaceCode);
        }

        public DTO.ResponseService<DTO.ExamSchedule> GetExamByTestingNo(string testNo)
        {
            return svc.GetExamByTestingNo(testNo);
        }

        public DTO.ResponseService<DataSet> GetddlGroupType(string PlaceCode)
        {
            return svc.GetddlGroupType(PlaceCode);
        }

    }
}

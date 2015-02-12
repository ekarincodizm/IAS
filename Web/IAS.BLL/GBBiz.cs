using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Data;
using System.Data.Entity;

namespace IAS.BLL
{
    public class GBBiz : IDisposable
    {
        private ExamService.ExamServiceClient svc;
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public GBBiz()
        {
            svc = new ExamService.ExamServiceClient();
        }

        public DTO.ResponseService<DTO.GBHoliday[]> GETGBHoliday(int page,int count)
        {
            return svc.GetHolidayList(page,count);
        }

        public DTO.ResponseService<string> AddHoliday(DTO.GBHoliday holiday)
        {
          return  svc.AddHoliday(holiday);
        }

        public DTO.ResponseService<string> DeleteHoliday(string date)
        {
            return svc.DeleteHoliday(date);
        }

        public DTO.ResponseService<string> UpdateHoliday(DTO.GBHoliday holiday)
        {
           return svc.UpdateHoliday(holiday);
        }

        public DTO.ResponseService<DTO.GBHoliday[]> SearchHoliday(string search,int page,int count)
        {
           return svc.SearchHoliday(search,page,count);
        }

        public DTO.ResponseService<DTO.GBHoliday[]> GetHolidayListByYearMonth(string yearMonth)
        {
            return svc.GetHolidayListByYearMonth(yearMonth);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAS.Utils;
using System.Collections;

namespace IAS.BLL
{
    public class MoveCompanyInBiz
    {
        LicenseService.LicenseServiceClient svc;
        DataCenterService.DataCenterServiceClient svcCenter;
        public MoveCompanyInBiz()
        {
            svc = new LicenseService.LicenseServiceClient();
            svcCenter = new DataCenterService.DataCenterServiceClient();
        }

        public List<DTO.DataItem> GetLicenseType()
        {
            var list_table = svcCenter.GetLicenseType("").DataResponse;
            List<DTO.DataItem> list = new List<DTO.DataItem>();
            list.Add(new DTO.DataItem { Id = "", Name = "ทั้งหมด" });
            foreach (var item in list_table)
            {
                if (item.Id.ToInt() <= 8)
                    list.Add(item);
            }
            list.Add(new DTO.DataItem { Id = "09", Name = "รวมประเภทตัวแทนประกันชีวิต " });
            list.Add(new DTO.DataItem { Id = "10", Name = "รวมประเภทตัวแทนประกันวินาศภัย " });
            return list;
        }

        public DTO.DataItem[] GetCompany()
        {
            return svcCenter.GetCompanyCode("ทั้งหมด").DataResponse;
        }


        public DataTable get()
        {
            DataTable table = new DataTable();
            DateTime startdate = DateTime.Now.AddYears(-10);
            DateTime enddate = DateTime.Now;
            var list = svc.GetMoveCompanyIn("","",startdate, enddate);           

            return list.DataResponse.Tables[0]; 
        }

        public void GetTable(ref List<string> head,ref DataTable table ,ref double sum,string licensetype,string comcode,string startdate,string enddate,ref bool error)
        {
            var list = svc.GetMoveCompanyIn(licensetype, comcode, Convert.ToDateTime(startdate), Convert.ToDateTime(enddate));
            if (list.IsError)
            {
                error = true;
            }
            else if (list.DataResponse == null || list.DataResponse.Tables[0] == null)
            {
                error = true;
            }
            else
            {

                DataTable oldTable = list.DataResponse.Tables[0];

                for (int i = 2; i < oldTable.Columns.Count; i++)
                {
                    if (oldTable.Columns[i].ColumnName != "TOTALS")
                    {
                        head.Add(oldTable.Columns[i].ColumnName.Replace("COMP_", ""));
                    }
                }


                for (int i = 0; i < oldTable.Columns.Count - 2; i++)
                {
                    table.Columns.Add("A" + i, typeof(string));
                }
                table.Columns.Add("A20", typeof(string));
                // double sum = 0;
                for (int i = 0; i < oldTable.Rows.Count; i++)
                {
                    DataRow row = table.NewRow();
                    for (int t = 0; t < table.Columns.Count; t++)
                    {
                        row[t] = oldTable.Rows[i][t + 1].ToString();

                        if (t == table.Columns.Count - 1)
                        {
                            sum += oldTable.Rows[i][t + 1].ToString().ToDouble();
                        }
                    }
                    table.Rows.Add(row);
                }
            }
        }
    }
}

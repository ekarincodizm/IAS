using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Data;
using IAS.DAL;

namespace IAS.DataServices.Payment.Helpers
{
    public class GenLicenseNumber
    {

        public static string AG_LICENSE_RUNNING(IAS.DAL.Interfaces.IIASPersonEntities ctx, DateTime ReceiptDate, string licenseTypeC)
        {
            //DTO.ResponseService<string> resLicenseNo = new ResponseService<string>();
            string LicenseNo = string.Empty;
            var res = new DTO.ResponseService<DataSet>();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            string CutDate2 = string.Empty;
            if (ReceiptDate.Year < 2500)
            {
                CutDate2 = Convert.ToString(ReceiptDate.Year + 543).Substring(2, 2);
            }
            else
            {
                CutDate2 = Convert.ToString(ReceiptDate.Year).Substring(2, 2);
            }
            string CutDate = Convert.ToString(ReceiptDate).Replace("/", "");
            string V_Str = CutDate2 + licenseTypeC;


            try
            {
                string tmp = string.Empty;

                tmp = "Select  lpad(to_char(to_number(nvl(Last_License_No,'0')) + 1),6,'0') as V_Last_No From  Ag_License_Running_No_T "
                    + "Where Lead_Str = '" + V_Str + "' ";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    DataRow dr = dt.Rows[0];
                    //AG_LICENSE_RUNNING_NO_T ent = base.ctx.AG_LICENSE_RUNNING_NO_T.FirstOrDefault(w => w.LEAD_STR == V_Str);

                    IQueryable<AG_LICENSE_RUNNING_NO_T> ent = ctx.AG_LICENSE_RUNNING_NO_T.Where(ww => ww.LEAD_STR == V_Str);

                    if ((ent.Count() > 0) && (ent != null))
                    {
                        var result = (from AGR in ent
                                      where AGR.LEAD_STR == V_Str
                                      select new DTO.LicenseRuningNo
                                      {
                                          LAST_LICENSE_NO = AGR.LAST_LICENSE_NO

                                      }).FirstOrDefault();

                        if (result != null)
                        {
                            result.LAST_LICENSE_NO = dr["V_Last_No"].ToString();
                            LicenseNo = V_Str + dr["V_Last_No"].ToString();
                        }
                    }

                }
                else
                {

                    try
                    {
                        AG_LICENSE_RUNNING_NO_T LicenseRun = new AG_LICENSE_RUNNING_NO_T
                        {
                            LEAD_STR = V_Str,
                            LAST_LICENSE_NO = "000001"
                        };
                        ctx.AG_LICENSE_RUNNING_NO_T.AddObject(LicenseRun);

                    }
                    catch (Exception ex)
                    {
                        var ent = ctx.AG_LICENSE_RUNNING_NO_T
                                     .Where(w => w.LEAD_STR == V_Str)
                                     .SingleOrDefault();
                        ent.LAST_LICENSE_NO = "000001";
                    }
                    LicenseNo = V_Str + "000001";
                }

                ctx.SaveChanges();

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            //return resLicenseNo.DataResponse = LicenseNo.ToString();
            return LicenseNo;
        }
    }
}
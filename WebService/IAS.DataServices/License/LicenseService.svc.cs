using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using IAS.DAL;
using IAS.Utils;
using System.Transactions;
using System.Text.RegularExpressions;
using Oracle.DataAccess.Client;
using System.Globalization;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Threading;

using IAS.DTO;
using IAS.DataServices.License.LicenseHelpers;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Properties;
using IAS.Common.Logging;
using System.ServiceModel.Activation;
using IAS.DTO.FileService;
using IAS.DataServices.FileManager;
using IAS.Common.Email;
using System.Web.Configuration;

namespace IAS.DataServices.License
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LicenseService : AbstractService, ILicenseService, IDisposable
    {
        private static String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString();
        private static String TempFileContainer = ConfigurationManager.AppSettings["FS_TEMP"].ToString();
        private static String DefaultNetDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];

        #region GETDATA



        //สำหรับตรวจสอบเงื่อนไขและรวม String
        private string GetCriteria(string criteria, string value)
        {
            return !string.IsNullOrEmpty(value)
                        ? string.Format(criteria, value)
                        : string.Empty;
        }

        /// <summary>
        /// แสดงข้อมูลใบอนุญาตตามเงื่อนไข
        /// </summary>
        /// <param name="licenseNo">เลขที่ใบอนุญาต</param>
        /// <param name="licenseType">ประเภทใบอนุญาต</param>
        /// <param name="startDate">วันที่เริ่มเพื่อหา (วันที่รับใบอนุญาต)</param>
        /// <param name="toDate">วันที่สิ้นสุดเพื่อหา (วันที่รับใบอนุญาต)</param>
        /// <param name="paymentNo">เลขที่ใบสั่งจ่าย</param>
        /// <param name="licenseTypeReceive">ประเภทขอรับใบอนุญา</param>
        /// <param name="pageNo">หน้าที่</param>
        /// <param name="recordPerPage">รายการแสดงต่อหน้า</param>
        /// <returns>DataSet</returns>
        public DTO.ResponseService<DataSet>
            GetLicenseByCriteria(string licenseNo, string licenseType,
                                 DateTime? startDate, DateTime? toDate,
                                 string paymentNo, string licenseTypeReceive,
                                 DTO.UserProfile userProfile,
                                 int pageNo, int recordPerPage)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string critRecNo = pageNo == 0
                                        ? ""
                                        : "WHERE A.RUN_NO BETWEEN " +
                                                 pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                                 pageNo.ToRowNumber(recordPerPage).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("LT.LICENSE_NO = '{0}' AND ", licenseNo));
                sb.Append(GetCriteria("LT.LICENSE_TYPE_CODE = '{0}' AND ", licenseType));

                sb.Append(GetCriteria("RT.GROUP_REQUEST_NO = '{0}' AND ", paymentNo));
                sb.Append(GetCriteria("RT.PETITION_TYPE_CODE = '{0}' AND ", licenseTypeReceive));

                if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue() ||
                   userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    sb.Append(GetCriteria("RT.UPLOAD_BY_SESSION = '{0}' AND ", userProfile.CompCode));
                }
                else if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                {
                    sb.Append(GetCriteria("LC.ID_CARD_NO = '{0}' AND ", userProfile.IdCard));
                }

                string tmp = string.Empty;
                if (startDate != null && toDate != null)
                {
                    tmp = string.Format(
                              "(LT.LICENSE_DATE BETWEEN " +
                              "    to_date('{0}','yyyymmdd') AND " +
                              "    to_date('{1}','yyyymmdd')) AND ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                    sb.Append(tmp);
                }

                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;

                string sql = "SELECT * " +
                             "FROM ( " +
                             "      SELECT LC.ID_CARD_NO, APT.FIRST_NAME, APT.LASTNAME, LR.LICENSE_TYPE_NAME, LT.LICENSE_NO, LT.LICENSE_DATE, LT.EXPIRE_DATE,  " +
                             "             RT.GROUP_REQUEST_NO, RT.PETITION_TYPE_CODE, RT.UPLOAD_BY_SESSION, LD.LICENSE_EXPIRE_DATE , LD.NAMES , LD.APPROVED ,  " +
                             "             ROW_NUMBER() OVER (ORDER BY LT.LICENSE_NO ASC) RUN_NO " +
                             "      FROM AG_LICENSE_T LT, " +
                             "           AG_LICENSE_TYPE_R LR, AG_IAS_LICENSE_D LD,  " +
                             "           (SELECT * " +
                             "            FROM " +
                             "                  (SELECT LICENSE_NO, RENEW_TIME, PETITION_TYPE_CODE, " +
                             "                          GROUP_REQUEST_NO, UPLOAD_BY_SESSION " +
                    //"                          ,RANK() OVER (PARTITION BY LICENSE_NO ORDER BY RENEW_TIME DESC) RANK " +
                             "                   FROM AG_LICENSE_RENEW_T " +
                             "                  ) " +
                    //"            WHERE RANK=1 " +
                             "            ) RT, " +
                             "           (SELECT LICENSE_NO,ID_CARD_NO FROM AG_AGENT_LICENSE_PERSON_T " +
                             "            UNION " +
                             "            SELECT LICENSE_NO,ID_CARD_NO FROM AG_AGENT_LICENSE_T) LC , " +
                             "            (SELECT TT.NAME ||' '|| AP.NAMES FIRST_NAME, AP.LASTNAME, AP.ID_CARD_NO " +
                             "            FROM AG_IAS_PERSONAL_T AP, " +
                             "            VW_IAS_TITLE_NAME		         TT " +
                             "            WHERE TT.ID = AP.PRE_NAME_CODE) APT " +
                             "      WHERE LT.LICENSE_TYPE_CODE = LR.LICENSE_TYPE_CODE AND " +
                             "            LC.ID_CARD_NO = APT.ID_CARD_NO AND " +
                             "            LT.LICENSE_NO = RT.LICENSE_NO AND LT.LICENSE_NO = LC.LICENSE_NO " + crit +
                             "     ) A " + critRecNo;

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseByCriteria", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงประวัติด้วยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DTO.PersonalHistory> GetPersonalHistoryByIdCard(string idCard)
        {
            var res = new DTO.ResponseService<DTO.PersonalHistory>();
            try
            {
                if (string.IsNullOrEmpty(idCard))
                {
                    res.ErrorMsg = Resources.errorLicenseService_001;
                    return res;
                }




                #region SQL Statement

                string sql = string.Empty;

                #region NEW


                var ed = base.ctx.AG_IAS_PERSONAL_T
                             .Where(w => w.ID_CARD_NO == idCard).FirstOrDefault();

                sql = string.Empty;
                sql += "SELECT TT.PRE_FULL || ' ' || P.NAMES || '  ' || P.LASTNAME FLNAME, ";
                sql += "P.SEX, P.ID_CARD_NO ,";
                sql += "       ( " +
                      "          SELECT NATIONALITY_NAME FROM AG_IAS_NATIONALITY " +
                      "          WHERE NATIONALITY_CODE = P.NATIONALITY " +
                      "       ) NATIONALITY";


                if (ed != null)
                {
                    if (ed.EDUCATION_CODE != null)
                    {
                        sql += ",ED.EDUCATION_NAME ";
                    }
                }

                sql += ",P.BIRTH_DATE,P.ADDRESS_1 ADDRESS1, P.ZIP_CODE ZIPCODE, P.TELEPHONE, " +
                   "       ( " +
                   "          SELECT NAME FROM VW_IAS_PROVINCE " +
                   "          Where ID = P.PROVINCE_CODE " +
                   "       ) PROVINCE, " +
                   "       ( " +
                   "          SELECT NAME FROM VW_IAS_AMPUR " +
                   "          WHERE PROVINCECODE = P.PROVINCE_CODE AND " +
                   "          ID=P.AREA_CODE  " +
                   "       ) AMPUR, " +
                   "       ( " +
                   "          SELECT NAME FROM VW_IAS_TUMBON " +
                   "          WHERE PROVINCECODE = P.PROVINCE_CODE AND " +
                   "                AMPURCODE = P.AREA_CODE AND " +
                   "                ID=P.TUMBON_CODE " +
                   "       ) TAMBON, P.LOCAL_ADDRESS1 , P.LOCAL_ZIPCODE, P.LOCAL_TELEPHONE, " +
                   "       ( " +
                   "           SELECT NAME FROM VW_IAS_PROVINCE " +
                   "           Where ID = P.LOCAL_PROVINCE_CODE " +
                   "       ) LOCAL_PROVINCE, " +
                   "       ( " +
                   "          SELECT NAME FROM VW_IAS_AMPUR " +
                   "          WHERE PROVINCECODE = P.PROVINCE_CODE AND " +
                   "          ID=P.LOCAL_AREA_CODE  " +
                   "        ) LOCAL_AMPUR, " +
                   "        ( " +
                   "          SELECT NAME FROM VW_IAS_TUMBON " +
                   "          WHERE PROVINCECODE = P.LOCAL_PROVINCE_CODE AND " +
                   "                AMPURCODE = P.LOCAL_AREA_CODE AND " +
                   "                ID=P.LOCAL_TUMBON_CODE " +
                   "        ) LOCAL_TAMBON " + "FROM  AG_IAS_PERSONAL_T P ";

                if (ed != null)
                {
                    if (ed.EDUCATION_CODE != null)
                    {
                        sql += ",AG_EDUCATION_R ED";
                    }
                }

                sql += ",GB_PREFIX_R TT " +
                "WHERE P.PRE_NAME_CODE = TT.PRE_CODE AND  P.ID_CARD_NO='" + idCard + "'";

                if (ed != null)
                {
                    if (ed.EDUCATION_CODE != null)
                    {
                        sql += "AND P.EDUCATION_CODE = ED.EDUCATION_CODE  ";
                    }
                }
                #endregion



                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);


                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    res.DataResponse = dr.MapToEntity<DTO.PersonalHistory>();
                }

                else
                {
                    sql = string.Empty;
                    #region New


                    var education = base.ctx.AG_PERSONAL_T
                    .Where(w => w.ID_CARD_NO == idCard).FirstOrDefault();


                    sql += "SELECT TT.PRE_FULL || ' ' || P.NAMES || '  ' || P.LASTNAME FLNAME, ";
                    sql += "P.SEX, P.ID_CARD_NO ,P.NATIONALITY";

                    if (education != null)
                    {
                        if (education.EDUCATION_CODE != null)
                        {
                            sql += ",ED.EDUCATION_NAME ";

                        }
                    }
                    sql += "       ,P.BIRTH_DATE, P.ADDRESS1, P.ZIPCODE, P.TELEPHONE, " +
                           "       ( " +
                           "          SELECT PV_DEST FROM GB_PROVINCE_R " +
                           "          WHERE PV_CODE = SUBSTR(P.AREA_CODE,1,2) AND " +
                           "                PV_AMPUR = '00' AND PV_TUMBON='0000' " +
                           "       ) PROVINCE, " +


                           "       ( " +
                           "          SELECT PV_DEST FROM GB_PROVINCE_R " +
                           "          WHERE PV_CODE = SUBSTR(P.AREA_CODE,1,2) AND " +
                           "          PV_AMPUR = SUBSTR(P.AREA_CODE,3,2) AND " +
                           "          PV_TUMBON='0000' " +
                           "       ) AMPUR, " +
                           "       ( " +
                           "          SELECT PV_DEST FROM GB_PROVINCE_R " +
                           "          WHERE PV_CODE = SUBSTR(P.AREA_CODE,1,2) AND " +
                           "                PV_AMPUR = SUBSTR(P.AREA_CODE,3,2) AND " +
                           "                PV_TUMBON=SUBSTR(P.AREA_CODE,5,4) " +
                           "       ) TAMBON, P.LOCAL_ADDRESS1, P.LOCAL_ZIPCODE, P.LOCAL_TELEPHONE, " +
                           "       ( " +
                           "          SELECT PV_DEST FROM GB_PROVINCE_R " +
                           "          WHERE PV_CODE = SUBSTR(P.LOCAL_AREA_CODE,1,2) AND " +
                           "                PV_AMPUR = '00' AND PV_TUMBON='0000' " +
                           "       ) LOCAL_PROVINCE, " +
                           "       ( " +
                           "          SELECT PV_DEST FROM GB_PROVINCE_R " +
                           "          WHERE PV_CODE = SUBSTR(P.LOCAL_AREA_CODE,1,2) AND " +
                           "                PV_AMPUR = SUBSTR(P.LOCAL_AREA_CODE,3,2) AND " +
                           "                PV_TUMBON='0000' " +
                           "        ) LOCAL_AMPUR, " +
                           "        ( " +
                           "          SELECT PV_DEST FROM GB_PROVINCE_R " +
                           "          WHERE PV_CODE = SUBSTR(P.LOCAL_AREA_CODE,1,2) AND " +
                           "                PV_AMPUR = SUBSTR(P.LOCAL_AREA_CODE,3,2) AND " +
                           "                PV_TUMBON=SUBSTR(P.LOCAL_AREA_CODE,5,4) " +
                           "        ) LOCAL_TAMBON " + "FROM  AG_PERSONAL_T P ";
                    if (education != null)
                    {
                        if (education.EDUCATION_CODE != null)
                        {
                            sql += ",AG_EDUCATION_R ED";
                        }
                    }

                    sql += ",GB_PREFIX_R TT " +
                    "WHERE P.PRE_NAME_CODE = TT.PRE_CODE AND  P.ID_CARD_NO='" + idCard + "'";

                    if (education != null)
                    {
                        if (education.EDUCATION_CODE != null)
                        {
                            sql += "AND P.EDUCATION_CODE = ED.EDUCATION_CODE  ";
                        }
                    }




                    ora = new OracleDB();
                    ds = ora.GetDataSet(sql);


                    #endregion
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        res.DataResponse = dr.MapToEntity<DTO.PersonalHistory>();
                    }

                }



            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetPersonalHistoryByIdCard", ex);
            }
            return res;
        }


        /// <summary>
        /// ดึงข้อมูลประวัติการสอบ
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetExamHistoryByIdCard(string idCard)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                if (string.IsNullOrEmpty(idCard))
                {
                    res.ErrorMsg = Resources.errorLicenseService_001;
                    return res;
                }

                #region SQL Statement

                string sql = "SELECT	AP.ID_CARD_NO, AP.APPLICANT_CODE, AP.TESTING_NO, " +
                             "          LR.TESTING_DATE,TR.TEST_TIME,LT.LICENSE_TYPE_NAME, " +
                             "          PR.EXAM_PLACE_NAME, " +
                             "          NVL((SELECT NAME FROM VW_IAS_COM_CODE CT where AP.INSUR_COMP_CODE = CT.ID),'') INSUR_COMP_NAME, " +
                             "          DECODE(RESULT, 'P', 'ผ่าน', 'F','ไม่ผ่าน', null,' ','B','Black List','M','ขาดสอบ') EXAM_RESULT, " +
                             "          AP.EXPIRE_DATE " +
                             "FROM	AG_APPLICANT_T AP, " +
                             "      AG_EXAM_LICENSE_R LR, " +
                             "      AG_EXAM_TIME_R TR, " +
                             "      AG_EXAM_PLACE_R PR, " +
                             "      AG_IAS_LICENSE_TYPE_R LT " +
                             "WHERE	( " +
                             "          AP.TESTING_NO = LR.TESTING_NO AND " +
                             "          AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE " +
                             "      ) AND " +
                             "      LR.TEST_TIME_CODE = TR.TEST_TIME_CODE AND " +
                             "      AP.EXAM_PLACE_CODE = PR.EXAM_PLACE_CODE AND " +
                             "      LR.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE AND " +
                             "      AP.ID_CARD_NO = '" + idCard + "' " +
                             "ORDER BY AP.TESTING_NO DESC";

                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetExamHistoryByIdCard", ex);
            }
            return res;
        }


        /// <summary>
        /// ดึงข้อมูลประวัติการอบรมด้วยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetTrainingHistoryBy(string idCard)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {


                string sql = "select  a.id_card_no,a.train_times "
                             + ",(SELECT LICENSE_TYPE_NAME FROM AG_IAS_LICENSE_TYPE_R where LICENSE_TYPE_CODE = a.LICENSE_TYPE_CODE) LICENSE_TYPE_CODE "
                             + ",(select SUM(nvl(c.train_period,0)) "
                             + "from ag_train_person_t c "
                             + "where c.id_card_no = '" + idCard + "' "
                             + "and c.train_times = a.train_times "
                             + "and nvl(c.result,0) = nvl(a.result,0) "
                             + "and c.train_type = a.train_type "
                             + "and c.license_type_code = a.license_type_code "
                             + "and c.train_exp_date >=trunc(sysdate) "
                             + "group by c.train_times,c.train_type,c.result,c.license_type_code) HOURS "
                             + ",(select SUM(nvl(c.PILAR_1,0)) "
                             + "from ag_train_person_t c "
                             + "where c.id_card_no = '" + idCard + "' "
                             + "and c.train_times = a.train_times "
                             + "and nvl(c.result,0)=nvl(a.result,0) "
                             + "and c.train_type = a.train_type "
                             + "and c.license_type_code = a.license_type_code "
                             + "and c.train_exp_date >=trunc(sysdate) "
                             + "group by c.train_times,c.train_type,c.result,c.license_type_code) PILAR_1 "
                             + ",(select SUM(nvl(c.PILAR_2,0)) "
                             + "from ag_train_person_t c "
                             + "where c.id_card_no = '" + idCard + "' "
                             + "and c.train_times = a.train_times "
                             + "and nvl(c.result,0)=nvl(a.result,0) "
                             + "and c.train_type = a.train_type "
                             + "and c.license_type_code = a.license_type_code "
                             + "and c.train_exp_date >=trunc(sysdate) "
                             + "group by c.train_times,c.train_type,c.result,c.license_type_code) PILAR_2 "
                             + ",(select SUM(nvl(c.PILAR_3,0)) "
                             + "from ag_train_person_t c "
                             + "where c.id_card_no = '" + idCard + "' "
                             + "and c.train_times = a.train_times "
                             + "and nvl(c.result,0)=nvl(a.result,0) "
                             + "and c.train_type = a.train_type "
                             + "and c.license_type_code = a.license_type_code "
                             + "and c.train_exp_date >=trunc(sysdate) "
                             + "group by c.train_times,c.train_type,c.result,c.license_type_code) PILAR_3 "
                             + ",DECODE(a.RESULT,'P','ผ่าน','F','ไม่ผ่าน',null,' ','B','แบล๊กลิสต์') RESULT  "
                             + ",DECODE(a.train_type,'T','อบรม','S','สัมนา') TRAIN_TYPE "
                             + "from ag_train_person_t a left join ag_train_plan_t b on a.train_code = b.train_code "
                             + "where a.id_card_no = '" + idCard + "' "
                             + "and a.train_exp_date >=trunc(sysdate) "
                             + "group by a.id_card_no,a.train_times,a.license_type_code,a.RESULT,a.train_type ";




                //old code
                //string sql = "SELECT a.ID_CARD_NO,a.TRAIN_TIMES,(SELECT LICENSE_TYPE_NAME FROM AG_IAS_LICENSE_TYPE_R where LICENSE_TYPE_CODE = a.LICENSE_TYPE_CODE) LICENSE_TYPE_CODE,DECODE(a.RESULT,'P','ผ่าน','F','ไม่ผ่าน',null,' ','B','แบล๊กลิสต์') RESULT "
                //            + ",PILAR_1,PILAR_2,PILAR_3 "
                //           + ",DECODE(SUM(PILAR_1+PILAR_2+PILAR_3),0,SUM(a.TRAIN_PERIOD) "
                //            + ",SUM(PILAR_1+PILAR_2+PILAR_3),SUM(a.TRAIN_PERIOD)) HOURS "
                //           //+ ",SUM(a.TRAIN_PERIOD) HOURS "
                //            + ",DECODE(a.TRAIN_TYPE,'T','อบรม','S','สัมนา') TRAIN_TYPE "
                //            + "FROM ag_train_person_t a left join ag_train_plan_t b on a.train_code = b.train_code "
                //            + "WHERE a.id_card_no = '" + idCard + "' "
                //            + "and a.train_exp_date >= trunc(sysdate) "
                //            + "group by a.id_card_no,a.TRAIN_TIMES,a.license_type_code,DECODE(a.RESULT,'P','ผ่าน','F','ไม่ผ่าน',null,' ','B','แบล๊กลิสต์') "
                //            + ",PILAR_1, PILAR_2, PILAR_3, DECODE(a.train_type,'T','อบรม','S','สัมนา') ";

                //string sql = "SELECT	TRAIN.LICENSE_TYPE_CODE, LTR.LICENSE_TYPE_NAME, " +
                //             "          TRAIN.TRAIN_TIMES, TRAIN.TRAIN_DATE, TRAIN.TRAIN_DATE_EXP, " +
                //             "          CASE WHEN TRAIN.TRAIN_DATE_EXP > SYSDATE THEN 'ACTIVE' ELSE 'EXPIRE' END STATUS, " +
                //    //"          --จำนวนชั่วโมง " +
                //             "          ( '0' " +
                //             "          ) HOURS " +
                //             "FROM	AG_TRAIN_T	TRAIN, " +
                //             "      AG_IAS_LICENSE_TYPE_R	LTR " +
                //             "WHERE TRAIN.LICENSE_TYPE_CODE = LTR.LICENSE_TYPE_CODE AND TRAIN.ID_CARD_NO = '" + idCard + "'";


                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;

                //DataTable ressss
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetTrainingHistoryBy", ex);
            }
            return res;
        }


        ///<summary>       
        /// ดึงข้อมูลประวัติข้อมูลผู้ขอรับใบอนุญาต
        /// 
        /// <paramref name=" idCard">รหัสบัตรประชาขน</paramref>
        /// <returns>ResponseService<DataSet></returns>
        /// </summary>

        public DTO.ResponseService<DataSet> GetObtainLicenseByIdCard(string idCard)
        {
            var res = new DTO.ResponseService<DataSet>();
            string sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {
                string dateOldst;
                if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                {
                    dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                }
                else
                {
                    dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                }


                sql = "SELECT lt.LICENSE_NO LICENSE_NO,(SELECT LICENSE_TYPE_NAME FROM AG_IAS_LICENSE_TYPE_R where LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE) LICENSE_TYPE_NAME "
                    + ",ar.RENEW_TIME RENEW_TIME,NVL(ar.RENEW_DATE,'') LICENSE_DATE,ar.EXPIRE_DATE LICENSE_EXPIRE_DATE "
                    + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap,AG_LICENSE_RENEW_T ar "
                    + "WHERE  lt.RECORD_STATUS is  null "
                    + "and lt.LICENSE_NO = ar.LICENSE_NO "
                    + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + idCard.Trim() + "') or "
                    + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + idCard.Trim() + "')) "
                    + "and lt.EXPIRE_DATE >= sysdate "
                    + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                    + "and (lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') or lt.REVOKE_LICENSE_DATE is null) "
                    + "group by lt.LICENSE_NO,lt.LICENSE_TYPE_CODE,ar.RENEW_TIME,ar.RENEW_DATE,ar.EXPIRE_DATE "
                    + "order by lt.LICENSE_NO,ar.RENEW_TIME DESC ";

                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetObtainLicenseByIdCard", ex);
            }
            return res;

        }



        /// <summary>
        /// ดึงข้อมูลการอบรม 1-4 โดยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetTrain_1_To_4_ByIdCard(string idCard)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "SELECT	SPT.SPECIAL_TYPE_CODE, SPR.SPECIAL_TYPE_DESC, " +
                             "          SPT.START_DATE, SPT.END_DATE, SPT.SEND_DATE, " +
                             "          SPT.SEND_BY, SPT.ID_CARD_NO " +
                             "FROM	AG_TRAIN_SPECIAL_T SPT, " +
                             "      AG_TRAIN_SPECIAL_R SPR " +
                             "WHERE	SPT.SPECIAL_TYPE_CODE = SPR.SPECIAL_TYPE_CODE AND " +
                             "      SPT.ID_CARD_NO = '" + idCard + "'";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetTrain_1_To_4_ByIdCard", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูล Unit Link โดยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>DTO.ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetUnitLinkByIdCard(string idCard)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "SELECT	U.LICENSE_TYPE_CODE, LTR.LICENSE_TYPE_NAME, " +
                             "          U.TRAIN_TIMES,U.TRAIN_DATE, U.ID_CARD_NO " +
                             "FROM	    AG_U_TRAIN_T		U, " +
                             "          AG_IAS_LICENSE_TYPE_R	LTR " +
                             "WHERE	U.LICENSE_TYPE_CODE = LTR.LICENSE_TYPE_CODE AND " +
                             "      U.ID_CARD_NO = '" + idCard + "'";
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetUnitLinkByIdCard", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลขอรับใบอนุญาต โดยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>DTO.ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetRequestLicenseByIdCard(string idCard)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = " SELECT L.LICENSE_NO, LR.LICENSE_TYPE_NAME, " +
                             " L.RENEW_DATE, L.EXPIRE_DATE, R.REVOKE_TYPE_NAME, L.REVOKE_LICENSE_DATE,RENEW_TIME,PAYMENT_NO  " +

                             " FROM (SELECT  LT.PAYMENT_NO, LT.LICENSE_NO, LT.RENEW_TIME, " +
                             " LT.RENEW_DATE, LT.EXPIRE_DATE, LT.ID_CARD_NO, T.REVOKE_TYPE_CODE,  T.REVOKE_LICENSE_DATE, " +
                             " LT.LICENSE_TYPE_CODE " +

                             " FROM (SELECT	RT.PAYMENT_NO, RT.LICENSE_NO, RT.RENEW_TIME, " +
                             " RT.RENEW_DATE, RT.EXPIRE_DATE, LC.ID_CARD_NO, ALT.LICENSE_TYPE_CODE " +

                             " FROM	AG_LICENSE_RENEW_T	RT, " +
                             " AG_LICENSE_T ALT, " +

                             " (SELECT LICENSE_NO, ID_CARD_NO " +

                             " FROM AG_AGENT_LICENSE_PERSON_T " +




                             " UNION SELECT LICENSE_NO, ID_CARD_NO " +

                             " FROM AG_AGENT_LICENSE_T) LC " +

                             " WHERE RT.LICENSE_NO = LC.LICENSE_NO AND " +
                             " RT.LICENSE_NO = ALT.LICENSE_NO AND " +
                             " LC.ID_CARD_NO = '" + idCard.ClearQuote() + "') LT " +
                             " , AG_LICENSE_T T " +
                             " WHERE LT.LICENSE_NO = T.LICENSE_NO) L, AG_REVOKE_TYPE_R R, AG_LICENSE_TYPE_R LR " +
                             " WHERE L.REVOKE_TYPE_CODE = R.REVOKE_TYPE_CODE AND L.LICENSE_TYPE_CODE = LR.LICENSE_TYPE_CODE ";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetRequestLicenseByIdCard", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลการขอรับใบอนุญาตตามเงื่อนไข
        /// </summary>
        /// <param name="licenseNo">เลขที่ใบอนุญาต</param>
        /// <param name="licenseTypeCode">รหัสประเภทใบอนุญาต</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetReceiveLicenseByCriteria(string licenseNo, string licenseTypeCode,
                                        DateTime? startDate, DateTime? toDate)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("LT.LICENSE_NO = '{0}' AND ", licenseNo.ClearQuote()));
                sb.Append(GetCriteria("LR.LICENSE_TYPE_CODE = '{0}' AND ", licenseTypeCode.ClearQuote()));

                if (startDate != null && toDate != null)
                {
                    sb.Append("(LT.DATE_LICENSE_ACT BETWEEN TO_DATE('" +
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                }

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                #region SQL Statement

                string sql =
                "SELECT	    LT.LICENSE_NO, LT.DATE_LICENSE_ACT, LT.EXPIRE_DATE, " +
                "           LR.LICENSE_TYPE_NAME, TT.PRE_FULL || ' ' || P.NAMES || '  ' || P.LASTNAME FLNAME, " +
                "           CT.COMP_ABBR_NAMET " +
                "FROM	    AG_LICENSE_T		LT, " +
                "           AG_LICENSE_TYPE_R	LR, " +
                "           (SELECT LICENSE_NO,ID_CARD_NO, NULL COMP_CODE " +
                "            FROM AG_AGENT_LICENSE_PERSON_T " +
                "            UNION " +
                "            SELECT LICENSE_NO,ID_CARD_NO, INSURANCE_COMP_CODE COMP_CODE " +
                "            FROM AG_AGENT_LICENSE_T) LC, " +
                "           GB_PREFIX_R TT, " +
                "           AG_PERSONAL_T P, " +
                "           AS_COMPANY_T CT " +
                "WHERE	    LT.LICENSE_TYPE_CODE = LR.LICENSE_TYPE_CODE AND " +
                "           LC.ID_CARD_NO = P.ID_CARD_NO AND " +
                "           LC.LICENSE_NO = LT.LICENSE_NO AND " +
                "           P.PRE_NAME_CODE = TT.PRE_CODE AND " +
                "           LC.COMP_CODE = CT.COMP_CODE " + crit;

                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetReceiveLicenseByCriteria", ex);
            }
            return res;
        }

        #endregion




        //สำหรับตรวจสอบข้อมูลการโหลดขอรับใบอนุญาต
        private bool ValidateReceiveLicenseTemp(long importId, string agentType, string petitionTypeCode, string licenseTypeCode)
        {

            #region Old Code
            //Regex regDigit = new Regex(@"^\d+$");
            //List<VW_IAS_TITLE_NAME> lsTitle = base.ctx.VW_IAS_TITLE_NAME.ToList();

            ////ตรวจสอบลำดับ
            //if (string.IsNullOrEmpty(rec.ORDERS))
            //{
            //    rec.ERR_DESC += "\nไม่มีข้อมูลลำดับที่";
            //}

            ////ตรวจสอบ E-mail
            //if (!string.IsNullOrEmpty(rec.EMAIL) && Email.IsRightEmailFormat(rec.EMAIL))
            //{
            //    rec.ERR_DESC += "\nรูปแบบเมล์ไม่ถูกต้อง";
            //}

            ////เลขบัตรประชาชนต้องมีค่า
            //if (string.IsNullOrEmpty(rec.ID_CARD_NO))
            //{
            //    rec.ERR_DESC += "\nไม่มีเลขบัตรประชาชน";
            //}
            //else
            //{
            //    //ตรวจสอบเลขที่บัตรต้องเป็นตัวเลขทั้งหมด
            //    if (!regDigit.IsMatch(rec.ID_CARD_NO))
            //    {
            //        rec.ERR_DESC += "\nเลขบัตรประชาชนต้องเป็นตัวเลขล้วน";
            //    }
            //}

            ////ตรวจสอบคำนำหน้าชื่อ
            //string title = rec.TITLE_NAME == "น.ส." ? "นางสาว" : rec.TITLE_NAME;
            //VW_IAS_TITLE_NAME entTitle = lsTitle.FirstOrDefault(s => s.NAME == title);
            //if (entTitle != null)
            //    rec.PRE_NAME_CODE = entTitle.ID.ToString();
            //else
            //{
            //    rec.ERR_DESC += "\nคำนำชื่อผิด";
            //}

            ////ตรวจสอบจำนวนตัวอักษรของชื่อต้องไม่เกิน 50 ตัวอักษร
            //if (rec.NAMES.Length > 50)
            //{
            //    rec.ERR_DESC += "\nชื่อยาวเกิน 50 ตัวอักษร";
            //}

            ////ตรวจสอบจำนวนตัวอักษรของนามสกุลต้องไม่เกิน 40 ตัวอักษร
            //if (rec.LASTNAME.Length > 40)
            //{
            //    rec.ERR_DESC += "\nนามสกุลยาวเกิน 40 ตัวอักษร";
            //}

            ////ตรวจสอบที่อยู่ 1 ต้องไม่เกิน 60 ตัวอักษร
            //if (rec.ADDRESS_1.Length > 60)
            //{
            //    rec.ERR_DESC += "\nที่อยู่ 1 ต้องไม่เกิน 60 ตัวอักษร";
            //}

            ////ตรวจสอบที่อยู่ 2 ต้องไม่เกิน 60 ตัวอักษร
            //if (rec.ADDRESS_2.Length > 60)
            //{
            //    rec.ERR_DESC += "\nที่อยู่ 2 ต้องไม่เกิน 60 ตัวอักษร";
            //}

            ////ตรวจสอบ Area Code
            //if (string.IsNullOrEmpty(rec.AREA_CODE) || rec.AREA_CODE.Length != 8)
            //{
            //    rec.ERR_DESC += "\nรหัสพื้นที่ไม่ถูกต้อง";
            //}

            #endregion Old Code

            using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
            {


                OracleCommand objCmd = new OracleCommand();

                objCmd.Connection = objConn;

                objCmd.CommandText = "AG_IAS_CHK_FILE_TO_DB";

                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("import_id", OracleDbType.Long).Value = importId;
                objCmd.Parameters.Add("agent_type", OracleDbType.Varchar2).Value = agentType;
                objCmd.Parameters.Add("petition", OracleDbType.Varchar2).Value = petitionTypeCode;
                objCmd.Parameters.Add("license_type_code", OracleDbType.Varchar2).Value = licenseTypeCode;

                var errFlag = new OracleParameter("ERR_FLG", OracleDbType.Int32, ParameterDirection.InputOutput);
                errFlag.Value = 0;
                objCmd.Parameters.Add(errFlag);


                var errMess = new OracleParameter("ERR_MESS", OracleDbType.Varchar2, ParameterDirection.InputOutput);


                errMess.Size = 4000;
                errMess.Value = "";

                objCmd.Parameters.Add(errMess);

                objCmd.Parameters.Add("B_FLAG", OracleDbType.Varchar2).Value = "L";

                var isDone = new OracleParameter("IS_DONE", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                isDone.Value = "N";
                objCmd.Parameters.Add(isDone);


                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LoggerFactory.CreateLog().Fatal("LicenseService_ValidateReceiveLicenseTemp", ex);
                    throw new ArgumentException(ex.Message);

                }
                finally
                {
                    objConn.Close();
                }


                return (isDone.Value.ToString() == "Y");
            }

        }


        /// <summary>
        /// บันทึกคำขอใบอนุญาตแบบเดี่ยว
        /// </summary>
        /// <param name="header">รายการ Header</param>
        /// <param name="detail">รายการ Detail</param>
        /// <param name="attachs">เอกสารแนบ</param>
        /// <param name="userProfile">User Profile</param>
        /// <returns></returns>
        public DTO.ResponseMessage<bool> InsertSingleReceiveLicense(DTO.ReceiveLicenseHeader header,
                                                                    DTO.ReceiveLicenseDetail detail,
                                                                    DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseMessage<bool>();

            try
            {

                #region ข้อมูลส่วน Header

                string groupNo = OracleDB.GetGenAutoId();
                header.IMPORT_ID = detail.IMPORT_ID = groupNo.ToLong();

                var entHeader = new AG_IAS_IMPORT_HEADER_TEMP();
                header.MappingToEntity(entHeader);

                #region Old Code

                //var entHeader = new DTO.ReceiveLicenseHeader
                //{
                //    IMPORT_ID = groupNo.ToLong(),
                //    IMPORT_DATETIME = DateTime.Now,
                //    FILE_NAME = groupNo,
                //    PETTITION_TYPE = header.PETTITION_TYPE,
                //    LICENSE_TYPE_CODE = header.LICENSE_TYPE_CODE,
                //    COMP_CODE = header.COMP_CODE,
                //    COMP_NAME = header.COMP_NAME,
                //    LICENSE_TYPE = header.LICENSE_TYPE_CODE
                //};
                ////header.IMPORT_ID = 
                //entHeader.IMPORT_ID = header.IMPORT_ID.ToLong();
                //entHeader.IMPORT_DATETIME = header.IMPORT_DATETIME;
                //entHeader.FILE_NAME = header.FILE_NAME;
                //entHeader.PETTITION_TYPE = header.PETTITION_TYPE;
                //entHeader.LICENSE_TYPE_CODE = header.LICENSE_TYPE_CODE;
                //entHeader.COMP_CODE = header.COMP_CODE;
                //entHeader.COMP_NAME = header.COMP_NAME;
                //entHeader.LICENSE_TYPE = entHeader.LICENSE_TYPE_CODE = header.LICENSE_TYPE_CODE;
                //entHeader.SEND_DATE = header.SEND_DATE;
                //entHeader.TOTAL_AGENT = header.TOTAL_AGENT;

                //entHeader.LOTS = header.TOTAL_AGENT.ToDecimal();
                //entHeader.MONEY = header.TOTAL_FEE;

                //entHeader.FILENAME = header.FILE_NAME;
                //entHeader.PETITION_TYPE_CODE = header.PETTITION_TYPE;
                //entHeader.UPLOAD_BY_SESSION = userProfile.CompCode;

                //base.ctx.AG_IAS_LICENSE_H
                //        .AddObject(entHeader);

                #endregion

                base.ctx.AG_IAS_IMPORT_HEADER_TEMP.AddObject(entHeader);

                #endregion

                #region ข้อมูลส่วน Detail

                //ตรวจสอบประเภทตัวแทน
                AG_IAS_LICENSE_TYPE_R licTypeEnt = base.ctx.AG_IAS_LICENSE_TYPE_R
                                                       .Where(w => w.LICENSE_TYPE_CODE == header.LICENSE_TYPE_CODE)
                                                       .FirstOrDefault();
                string _agentType = string.Empty;
                if (licTypeEnt != null)
                {
                    _agentType = licTypeEnt.AGENT_TYPE;
                }
                else
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_002;
                    return res;
                }

                var entDetail = new AG_IAS_IMPORT_DETAIL_TEMP();

                var person = base.ctx.AG_IAS_PERSONAL_T
                                     .Where(w => w.ID_CARD_NO == userProfile.IdCard)
                                     .FirstOrDefault();
                if (person == null)
                {
                    res.ErrorMsg = Resources.errorLicenseService_003;
                    return res;
                }

                detail.MappingToEntity(entDetail);

                base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.AddObject(entDetail);

                #region Old Code

                //entDetail.UPLOAD_GROUP_NO = detail.IMPORT_ID.ToString();
                //entDetail.SEQ_NO = "00001";
                //detail.SEQ = "0001";


                //detail.UPLOAD_GROUP_NO = header.UPLOAD_GROUP_NO;
                //detail.SEQ_NO = "0001";
                //entDetail.ORDERS = "1";

                //คุณณัฐ บอกว่า เลขที่ใบอนุญาตให้ User กรอกเอง 

                //detail.LICENSE_DATE = issueDate;
                //detail.LICENSE_EXPIRE_DATE = expireDate;
                //detail.FEES = GetByIndex(headData, 4).ToDecimal();

                //entDetail.LICENSE_NO = detail.LICENSE_NO;
                //entDetail.LICENSE_DATE = detail.LICENSE_ACTIVE_DATE;
                //entDetail.LICENSE_EXPIRE_DATE = detail.LICENSE_EXPIRE_DATE;
                //entDetail.FEES = detail.LICENSE_FEE;
                //entDetail.ID_CARD_NO = detail.CITIZEN_ID;
                //entDetail.RENEW_TIMES = detail


                //string areaCode = (person.PROVINCE_CODE != null ? person.PROVINCE_CODE : "") +
                //                  (person.AREA_CODE != null ? person.AREA_CODE : "") +
                //                  (person.TUMBON_CODE != null ? person.TUMBON_CODE : "");

                //string localAreaCode = (person.LOCAL_PROVINCE_CODE != null ? person.LOCAL_PROVINCE_CODE : "") +
                //                       (person.LOCAL_AREA_CODE != null ? person.LOCAL_AREA_CODE : "") +
                //                       (person.LOCAL_TUMBON_CODE != null ? person.LOCAL_TUMBON_CODE : "");

                //detail.ID_CARD_NO = person.ID_CARD_NO;
                //detail.PRE_NAME_CODE = person.PRE_NAME_CODE;
                //detail.NAMES = person.NAMES;
                //detail.LASTNAME = person.LASTNAME;
                //detail.ADDRESS_1 = person.ADDRESS_1;
                //detail.ADDRESS_2 = person.ADDRESS_2;
                //detail.AREA_CODE = areaCode;
                //detail.EMAIL = person.EMAIL;
                //detail.CURRENT_ADDRESS_1 = person.LOCAL_ADDRESS1;
                //detail.CURRENT_ADDRESS_2 = person.LOCAL_ADDRESS2;
                //detail.CURRENT_AREA_CODE = localAreaCode;


                //detail.MappingToEntity(ent);

                // base.ctx.AG_IAS_LICENSE_D.AddObject(ent);

                //foreach (DTO.AttatchFileLicense att in attachs)
                //{
                //    var attach = new AG_IAS_ATTACH_FILE_LICENSE();
                //    att.MappingToEntity(attach);
                //    attach.ID_ATTACH_FILE = OracleDB.GetGenAutoId();
                //    base.ctx.AG_IAS_ATTACH_FILE_LICENSE.AddObject(attach);
                //}

                //using (var ts = new TransactionScope())
                //{
                //    base.ctx.SaveChanges();
                //    ts.Complete();
                //}

                #endregion

                base.ctx.SaveChanges();

                bool result = this.ValidateReceiveLicenseTemp(groupNo.ToLong(), _agentType, header.PETTITION_TYPE, header.LICENSE_TYPE_CODE);

                if (!result)
                {
                    res.ErrorMsg = Resources.errorLicenseService_004;
                }
                else
                {
                    Int64 FiltergroupNo = Convert.ToInt64(groupNo.ToLong());
                    var ent = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                      .Where(w => w.IMPORT_ID == FiltergroupNo)
                                      .FirstOrDefault();

                    if (ent != null)
                    {
                        if (!string.IsNullOrEmpty(ent.ERR_MSG))
                            res.ErrorMsg = ent.ERR_MSG;
                        else
                            res.ResultMessage = true;
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_InsertSingleReceiveLicense", ex);
            }
            return res;
        }



        public DTO.ResponseMessage<bool> SubmitSingleOrGroupReceiveLicense(string groupId,
                                                                    List<DTO.AttatchFileLicense> attachs)
        {
            //var dup = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w=>w.LOAD_STATUS == "C" && w.CITIZEN_ID

            var res = new DTO.ResponseMessage<bool>();

            long lgroupID = Convert.ToInt64(groupId);

            //ValidatePayment(groupId);

            var details = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                  .Where(w => w.IMPORT_ID == lgroupID)
                                  .ToList();

            int hasError = details.Where(delegate(AG_IAS_IMPORT_DETAIL_TEMP temp)
            {
                return !string.IsNullOrEmpty(temp.ERR_MSG);
            }).Count();

            if (hasError > 0)
            {
                res.ErrorMsg = Resources.errorLicenseService_005;
                return res;
            }
            bool flagError = false;
            foreach (var item in details)
            {
                var dup = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w => w.CITIZEN_ID == item.CITIZEN_ID && w.LOAD_STATUS == "C" && w.PETITION_TYPE == item.PETITION_TYPE).FirstOrDefault();
                if (dup != null)
                {
                    item.ERR_MSG = Resources.errorLicenseService_006;
                    flagError = true;
                }
            }
            if (flagError)
            {
                // base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.AddObject();
                base.ctx.SaveChanges();
                return res;
            }


            try
            {

                #region ข้อมูลส่วน Header


                //ตรวจสอบประเภทตัวแทน
                long lgroupId = Convert.ToInt64(groupId);
                AG_IAS_IMPORT_HEADER_TEMP header = base.ctx.AG_IAS_IMPORT_HEADER_TEMP
                                                        .Where(w => w.IMPORT_ID == lgroupId)
                                                        .FirstOrDefault();

                AG_LICENSE_TYPE_R licTypeEnt = base.ctx.AG_LICENSE_TYPE_R
                                                       .Where(w => w.LICENSE_TYPE_CODE == header.LICENSE_TYPE_CODE)
                                                       .FirstOrDefault();
                string _agentType = string.Empty;
                if (licTypeEnt != null)
                {
                    _agentType = licTypeEnt.AGENT_TYPE;

                }


                AG_IAS_IMPORT_DETAIL_TEMP detail = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                                        .Where(w => w.IMPORT_ID == lgroupId)
                                                        .FirstOrDefault();
                string _petition = string.Empty;
                if (detail != null)
                {
                    _petition = detail.PETITION_TYPE;
                }

                bool result = false;

                #region call store


                using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
                {

                    OracleCommand objCmd = new OracleCommand();

                    objCmd.Connection = objConn;

                    objCmd.CommandText = "AG_IAS_UPD_FILE_TO_DB";

                    objCmd.CommandType = CommandType.StoredProcedure;



                    objCmd.Parameters.Add("vimport_id", OracleDbType.Long).Value = groupId.ToLong();
                    objCmd.Parameters.Add("agent_type", OracleDbType.Varchar2).Value = _agentType;
                    objCmd.Parameters.Add("petition", OracleDbType.Varchar2).Value = _petition;

                    var errFlag = new OracleParameter("err_flag", OracleDbType.Int32, ParameterDirection.InputOutput);
                    errFlag.Value = 0;
                    objCmd.Parameters.Add(errFlag);


                    var errMess = new OracleParameter("err_mess", OracleDbType.Varchar2, ParameterDirection.Output);
                    errMess.Size = 4000;
                    errMess.Value = "";
                    objCmd.Parameters.Add(errMess);

                    objCmd.Parameters.Add("flag", OracleDbType.Varchar2, ParameterDirection.Input).Value = "L";

                    var isDone = new OracleParameter("IS_DONE", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                    isDone.Value = "N";
                    objCmd.Parameters.Add(isDone);

                    try
                    {
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        result = (isDone.Value.ToString() == "Y");
                    }
                    catch (Exception ex)
                    {
                        res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                        LoggerFactory.CreateLog().Fatal("LicenseService_SubmitSingleOrGroupReceiveLicense", ex);
                    }
                    objConn.Close();
                }

                #endregion
                base.ctx.SaveChanges();


                //update Head
                // Updaateheadby(lgroupId.ToString());


                #region IMPORT AG_IAS_ATTACH_FILE)LICENSE


                //Import AG_IAS_ATTACH_FILE_LICENSE
                if (result)
                {
                    foreach (DTO.AttatchFileLicense att in attachs)
                    {
                        var attach = new AG_IAS_ATTACH_FILE_LICENSE();
                        att.FILE_STATUS = "A";
                        att.GROUP_LICENSE_ID = groupId;
                        att.MappingToEntity(attach);
                        attach.ID_ATTACH_FILE = OracleDB.GetGenAutoId();
                        base.ctx.AG_IAS_ATTACH_FILE_LICENSE.AddObject(attach);

                    }
                    base.ctx.SaveChanges();


                    // CreatePayment();
                    res.ResultMessage = true;
                }
                #endregion


                #endregion

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_SubmitSingleOrGroupReceiveLicense", ex);
            }
            return res;
        }




        public DTO.ResponseMessage<bool> ValidateDetail(string groupId)
        {
            var res = new DTO.ResponseMessage<bool>();
            long lgroupID = Convert.ToInt64(groupId);

            var head = base.ctx.AG_IAS_IMPORT_HEADER_TEMP
                                  .Where(w => w.IMPORT_ID == lgroupID).SingleOrDefault();

            string pettiontype = head.PETTITION_TYPE;
            string licentype = head.LICENSE_TYPE_CODE;
            string comcode = head.COMP_CODE;


            using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
            {

                OracleCommand objCmd = new OracleCommand();

                objCmd.Connection = objConn;

                objCmd.CommandText = "IAS_CHECK_LICENSE";

                objCmd.CommandType = CommandType.StoredProcedure;


                objCmd.Parameters.Add("VIMPORT_ID", OracleDbType.Long).Value = groupId.ToLong();
                objCmd.Parameters.Add("PETTITION_TYPE_CODE", OracleDbType.Varchar2).Value = pettiontype.ToString();
                objCmd.Parameters.Add("LICENSE_TYPE_CODE", OracleDbType.Varchar2).Value = licentype.ToString();


                try
                {
                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    //result = (isDone.Value.ToString() == "Y");
                }
                catch (Exception ex)
                {
                    LoggerFactory.CreateLog().Fatal("LicenseService_ValidateDetail", ex);
                    throw new ArgumentException(ex.Message);
                }
                finally
                {
                    objConn.Close();
                }
            }
            base.ctx.SaveChanges();



            var details = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                .Where(w => w.IMPORT_ID == lgroupID)
                                .ToList();



            int hasError = details.Where(delegate(AG_IAS_IMPORT_DETAIL_TEMP temp)
            {
                return !string.IsNullOrEmpty(temp.ERR_MSG);
            }).Count();

            if (hasError > 0)
            {
                res.ResultMessage = false;
            }
            else
            {

                res.ResultMessage = true;
            }

            return res;
        }


        public DTO.ResponseService<List<DTO.DetailTemp>> GetDetail(string groupId)
        {

            var res = new DTO.ResponseService<List<DTO.DetailTemp>>();
            try
            {

                Func<string, string> NulltoString = delegate(string input)
                {
                    if ((input == null) || (input == ""))
                    {
                        input = "0";
                    }

                    return input;
                };

                string groupIdNullable = NulltoString(groupId);

                long lgroupID = Convert.ToInt64(groupIdNullable);
                var details = (from dtt in base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                               where dtt.IMPORT_ID == lgroupID
                               select new DTO.DetailTemp
                               {
                                   IMPORT_ID = dtt.IMPORT_ID,
                                   PETITION_TYPE = dtt.PETITION_TYPE,
                                   COMP_CODE = dtt.COMP_CODE,
                                   SEQ = dtt.SEQ,
                                   LICENSE_NO = dtt.LICENSE_NO,
                                   LICENSE_ACTIVE_DATE = dtt.LICENSE_ACTIVE_DATE,
                                   LICENSE_EXPIRE_DATE = dtt.LICENSE_EXPIRE_DATE,
                                   LICENSE_FEE = dtt.LICENSE_FEE,
                                   CITIZEN_ID = dtt.CITIZEN_ID,
                                   TITLE_NAME = dtt.TITLE_NAME,
                                   NAME = dtt.NAME,
                                   SURNAME = dtt.SURNAME,
                                   ADDR1 = dtt.ADDR1,
                                   ADDR2 = dtt.ADDR2,
                                   AREA_CODE = dtt.AREA_CODE,
                                   EMAIL = dtt.EMAIL,
                                   CUR_ADDR = dtt.CUR_ADDR,
                                   TEL_NO = dtt.TEL_NO,
                                   CUR_AREA_CODE = dtt.CUR_AREA_CODE,
                                   AR_ANSWER = dtt.AR_ANSWER,
                                   OLD_COMP_CODE = dtt.OLD_COMP_CODE,
                                   ERR_MSG = dtt.ERR_MSG

                               }).ToList();

                if (details.Count > 0)
                {
                    res.DataResponse = details;
                }
                else
                {
                    res.DataResponse = null;
                }

            }
            catch (Exception ex)
            {
                ex.Source = "Transaction not complete! please contact Administrator";
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetDetail", ex);
                //res.ErrorMsg = "Transaction not complete! please contact Administrator";
            }

            return res;

        }


        private void Updaateheadby(string id, string groupid)
        {


            //AG_IAS_LICENSE_H licHead = base.ctx.AG_IAS_LICENSE_H
            //                                       .Where(w => w.UPLOAD_GROUP_NO == id)
            //                                       .FirstOrDefault();

            AG_IAS_PERSONAL_T licper = base.ctx.AG_IAS_PERSONAL_T
                                                     .Where(w => w.ID == id)
                                                     .FirstOrDefault();

            if (licper != null)
            {
                string sql = "UPDATE AG_IAS_LICENSE_H "
                             + " SET UPLOAD_BY_SESSION = " + "'" + licper.COMP_CODE + "' "
                             + " WHERE UPLOAD_GROUP_NO = " + "'" + groupid + "'";

                OracleDB ora = new OracleDB();
                ora.GetDataSet(sql);
            }




        }

        private bool CreatePayment()
        {
            //var payments = new List<DTO.SubGroupPayment>();

            //payments.Add(new DTO.SubGroupPayment
            //{
            //    ApplicantCode = ent.APPLICANT_CODE,
            //    ExamPlaceCode = ent.EXAM_PLACE_CODE,
            //    TestingNo = ent.TESTING_NO
            //});

            //var paySrv = new IAS.DataServices.Payment.PaymentService();
            //string groupHeaderNo = string.Empty;
            //var resSubGroup = paySrv.SetSubGroupSingle(payments, userId, ent.INSUR_COMP_CODE, out groupHeaderNo);

            //if (resSubGroup.IsError)
            //{
            //    res.ErrorMsg = resSubGroup.ErrorMsg;
            //}
            //else
            //{
            //    string paymentId = (DateTime.Now.Year + 543).ToString("0000").Substring(2, 2) +
            //                        DateTime.Now.ToString("MMddHHmmss");

            //    var lsSubPayment = new List<string>();
            //    lsSubPayment.Add(groupHeaderNo);

            //    var resPayment = paySrv.CreatePayment(lsSubPayment, string.Empty, paymentId, userId, ent.INSUR_COMP_CODE);

            //    if (resPayment.IsError)
            //    {
            //        res.ErrorMsg = resPayment.ErrorMsg;
            //    }
            //    else
            //    {
            //        res.ResultMessage = true;
            //    }
            //}
            return true;
        }


        //สำหรับตรวจสอบข้อมูลกับเอกสารแนบด้วยรหัสบัตรประชาชน
        private void ValidateAttatchFiles(DTO.ReceiveLicenseDetail detail,
                                          List<DTO.AttachFileDetail> attachFiles)
        {
            try
            {
                int iCount = attachFiles.Where(w => w.FileName.Split('_')[0] == detail.CITIZEN_ID).Count(); // .ID_CARD_NO).Count();
                if (iCount == 0)
                {
                    //detail.ERR_DESC += "ไม่พบเอกสารแนบ";
                }
            }
            catch (Exception ex)
            {
                //detail.ERR_DESC += ex.Message;
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateAttatchFiles", ex);
            }
        }

        /// <summary>
        /// เพิ่มข้อมูลการ Upload ขอรับใบอนุญาต
        /// </summary>
        /// <param name="data">Raw Data</param>
        /// <param name="compressFile">รายการไฟล์แนบ</param>
        /// <param name="fileName">ชื่อไฟล์ที่ Upload</param>
        /// <param name="userId">user id</param>
        /// <returns>ResponseService<UploadResult<UploadHeader, ReceiveLicenseDetail>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>>
            InsertAndCheckReceiveLicenseGroupUpload(DTO.UploadData data,
                                                    DTO.CompressFileDetail compressFile,
                                                    string groupNo,
                                                    string fileName, string userId,
                                                    string petitionTypeCode,
                                                    string licenseTypeCode)
        {
            var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>>();
            res.DataResponse = new DTO.UploadResult<DTO.UploadHeader, DTO.ReceiveLicenseDetail>();
            res.DataResponse.Header = new List<DTO.UploadHeader>();
            res.DataResponse.Detail = new List<DTO.ReceiveLicenseDetail>();

            #region Internal Function
            Func<string, bool> IsRightDate = (aryString) =>
            {
                if (string.IsNullOrEmpty(aryString)) return false;

                if (aryString.Trim().Length != 10) return false;

                DateTime _dt;
                //return DateTime.TryParse(aryString, out _dt);
                return DateTime.TryParseExact(aryString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dt);

            };

            Func<string[], int, string> GetByIndex = (aryString, index) =>
            {
                return aryString.Length - 1 >= index
                            ? aryString[index].Trim()
                            : string.Empty;
            };

            Func<string, DateTime?> GetDateValue = strDate =>
            {
                DateTime? dateValue = null;

                dateValue = !string.IsNullOrEmpty(strDate)
                            ? strDate.String_dd_MM_yyyy_ToDate('/', true)
                            : dateValue;
                return dateValue;
            };
            #endregion

            try
            {

                #region เตรียมข้อมูลส่วน Header

                string _headData = data.Header.ClearQuoteInCSV();

                //ตรวจสอบ Header มีหรือไม่
                if (string.IsNullOrEmpty(_headData))
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_007;
                    return res;
                }

                string[] headData = _headData.Split(',');

                string _licenseTypeCode = GetByIndex(headData, 3).Trim().ToInt().ToString("00");

                //ตรวจสอบรหัสประเภทใบอนุญาตที่ User เลือกจาก UI ต้องตรงกับ ในไฟล์ที่ส่งมา
                if (_licenseTypeCode != licenseTypeCode)
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_008;
                    return res;
                }



                //ตรวจสอบประเภทตัวแทน
                AG_LICENSE_TYPE_R licTypeEnt = base.ctx.AG_LICENSE_TYPE_R
                                                       .Where(w => w.LICENSE_TYPE_CODE == _licenseTypeCode)
                                                       .FirstOrDefault();
                string _agentType = string.Empty;
                if (licTypeEnt != null)
                {
                    _agentType = licTypeEnt.AGENT_TYPE;
                }
                else
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_002;
                    return res;
                }

                string _compCode = GetByIndex(headData, 1).Trim();

                //ตรวจสอบประเภทตัวแทน

                if (_agentType == "A")
                {
                    //ถ้าไม่มีค่า
                    if (string.IsNullOrEmpty(_compCode))
                    {
                        res.ErrorMsg = "\n" + Resources.errorLicenseService_009;
                        return res;
                    }
                    else
                    {

                        if (_compCode.Length > 3)
                        {
                            VW_IAS_COM_CODE compEnt = base.ctx.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == _compCode);
                            if (compEnt == null)
                            {
                                res.ErrorMsg = "\n" + Resources.errorApplicantService_014;
                                return res;
                            }
                        }
                        else
                        {
                            AG_EXAM_PLACE_GROUP_R compEnt = base.ctx.AG_EXAM_PLACE_GROUP_R.SingleOrDefault(s => s.EXAM_PLACE_GROUP_CODE == _compCode);
                            if (compEnt == null)
                            {
                                res.ErrorMsg = "\n" + Resources.errorLicenseService_010;
                                return res;
                            }
                        }
                    }
                }
                else if (_agentType == "B") //ถ้าเป็น Broker
                {
                    if (!string.IsNullOrEmpty(_compCode))
                    {

                        if (_compCode.Length > 3)
                        {
                            VW_IAS_COM_CODE compEnt = base.ctx.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == _compCode);
                            if (compEnt == null)
                            {
                                res.ErrorMsg = "\n" + Resources.errorApplicantService_014;
                                return res;
                            }
                        }
                        else
                        {
                            AG_EXAM_PLACE_GROUP_R compEnt = base.ctx.AG_EXAM_PLACE_GROUP_R.SingleOrDefault(s => s.EXAM_PLACE_GROUP_CODE == _compCode);
                            if (compEnt == null)
                            {
                                res.ErrorMsg = "\n" + Resources.errorLicenseService_010;
                                return res;
                            }
                        }

                    }
                }

                //ตรวจวันที่นำส่ง
                DateTime sendDate = DateTime.MinValue;
                string strSendgDate = GetByIndex(headData, 4);

                if (!IsRightDate(strSendgDate))
                {
                    res.ErrorMsg = Resources.errorLicenseService_011 + strSendgDate;
                    return res;
                }
                else
                {
                    if (strSendgDate.Substring(6, 2) != "25")
                    {
                        res.ErrorMsg = Resources.errorLicenseService_012 + strSendgDate;
                        return res;
                    }
                }

                sendDate = GetByIndex(headData, 4).String_dd_MM_yyyy_ToDate('/', true);

                //วันที่นำส่งต้องน้อยกว่าวันที่ปัจจุบัน
                if (sendDate > DateTime.Today)
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_013;
                    return res;
                }

                //ตรวจสอบจำนวนคนที่ต่ออายุ
                string _lots = GetByIndex(headData, 5);
                if (string.IsNullOrEmpty(_lots))
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_014;
                    return res;
                }


                //ตรวจสอบจำนวนคนนำเข้า
                int losnumber = Convert.ToInt16(_lots);
                int countdetail = data.Body.Count;
                if (countdetail != losnumber)
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_015;
                    return res;

                }

                //ตรวจสอบการระบุจำนวนเงิน
                string _money = GetByIndex(headData, 6);
                if (string.IsNullOrEmpty(_money))
                {
                    res.ErrorMsg = "\n" + Resources.errorLicenseService_016;
                    return res;
                }

                var entfree = base.ctx.AG_PETITION_TYPE_R.Where(w => w.PETITION_TYPE_CODE == petitionTypeCode).FirstOrDefault();



                decimal sumFree = 0;
                //ตรวจสอบจำนวนเงินรวมที่ระบุ
                if (entfree != null)
                {
                    int countbody = data.Body.Count;
                    sumFree = entfree.FEE.ToDecimal() * countbody;

                    decimal numMoney = Convert.ToDecimal(_money);

                    if (numMoney != sumFree)
                    {
                        res.ErrorMsg = "\n" + Resources.errorLicenseService_017;
                        return res;
                    }
                }




                //ตรวจสอบรหัสตัวแรกของ Comp Code
                //if ("1_2_3_4".Contains(_compCode.Substring(1, 1)))
                //{
                //    res.ErrorMsg = "\nเลือกย้ายรหัสบริษัทต้องเป็นตัวแทนชีวิตเท่านั้น";
                //    return res;
                //}


                var comp = base.ctx.AG_IAS_APPROVE_DOC_TYPE.Where(w => w.APPROVE_DOC_TYPE == licenseTypeCode && w.ITEM_VALUE == "Y").FirstOrDefault();


                //Add Header
                res.DataResponse.GroupId = groupNo;

                var recHead = new DAL.AG_IAS_IMPORT_HEADER_TEMP
                {
                    IMPORT_ID = groupNo.ToLong(),
                    IMPORT_DATETIME = DateTime.Now,
                    FILE_NAME = fileName,
                    PETTITION_TYPE = petitionTypeCode,
                    LICENSE_TYPE_CODE = licenseTypeCode,
                    COMP_CODE = GetByIndex(headData, 1).Trim(),
                    COMP_NAME = GetByIndex(headData, 2).Trim(),
                    LICENSE_TYPE = _licenseTypeCode,
                    SEND_DATE = sendDate,
                    TOTAL_AGENT = _lots.ToInt(),
                    TOTAL_FEE = _money.ToDecimal(),
                    ERR_MSG = "",
                    APPROVE_COMPCODE = comp == null ? null : comp.APPROVER

                };

                base.ctx.AG_IAS_IMPORT_HEADER_TEMP.AddObject(recHead);

                #endregion

                #region เตรียมข้อมูลส่วน Details



                for (int i = 0; i < data.Body.Count; i++)
                {
                    var errMsg = string.Empty;
                    string d = data.Body[i];

                    string[] rawData = d.ClearQuoteInCSV().Split(',');

                    string licenseIssueDate = GetByIndex(rawData, 2).Trim();
                    string licenseExpireDate = GetByIndex(rawData, 3).Trim();

                    string citizenID = GetByIndex(rawData, 5);

                    var dupid = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w => w.CITIZEN_ID == citizenID && w.IMPORT_ID == recHead.IMPORT_ID).FirstOrDefault();
                    if (dupid != null)
                    {
                        errMsg = Resources.errorLicenseService_018 + Environment.NewLine;

                    }


                    DateTime? issueDate = GetDateValue(licenseIssueDate);
                    DateTime? expireDate = GetDateValue(licenseExpireDate);

                    var rec = new DAL.AG_IAS_IMPORT_DETAIL_TEMP();
                    rec.IMPORT_ID = recHead.IMPORT_ID;
                    rec.PETITION_TYPE = petitionTypeCode;
                    rec.COMP_CODE = recHead.COMP_CODE;
                    rec.SEQ = (i + 1).ToString("0000");
                    rec.LICENSE_NO = GetByIndex(rawData, 1);
                    rec.LICENSE_ACTIVE_DATE = issueDate;
                    rec.LICENSE_EXPIRE_DATE = expireDate;
                    rec.LICENSE_FEE = GetByIndex(rawData, 4).ToDecimal();
                    rec.CITIZEN_ID = GetByIndex(rawData, 5);
                    rec.TITLE_NAME = GetByIndex(rawData, 6);
                    rec.NAME = GetByIndex(rawData, 7);
                    rec.SURNAME = GetByIndex(rawData, 8);
                    rec.ADDR1 = GetByIndex(rawData, 9);
                    rec.ADDR2 = GetByIndex(rawData, 10);
                    rec.AREA_CODE = GetByIndex(rawData, 11);
                    rec.EMAIL = GetByIndex(rawData, 12);
                    rec.CUR_ADDR = GetByIndex(rawData, 13);
                    rec.TEL_NO = GetByIndex(rawData, 14);
                    rec.CUR_AREA_CODE = GetByIndex(rawData, 15);
                    rec.AR_ANSWER = rawData.Length > 16 ? GetByIndex(rawData, 16) : "";
                    rec.OLD_COMP_CODE = rawData.Length > 17 ? GetByIndex(rawData, 17) : "";
                    rec.ERR_MSG = errMsg;
                    base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.AddObject(rec);
                    base.ctx.SaveChanges();
                }

                #endregion

                #region Validate  by Store

                //Call Store Proc  ตรวจสอบโดยเรียก Store
                bool valid = this.ValidateReceiveLicenseTemp(groupNo.ToLong(), _agentType, petitionTypeCode, licenseTypeCode);

                long impId = groupNo.ToLong();
                if (valid)
                {
                    res.DataResponse.Detail = GetDetailbyGroupNo(impId);

                }
                #endregion



                res.DataResponse.Detail = GetDetailbyGroupNo(impId);
                #region Validate Attach File

                //ตรวจสอบเอกสารแนบ 
                List<DTO.ReceiveLicenseDetail> ListDetail = res.DataResponse.Detail;

                for (int i = 0; i < ListDetail.Count; i++)
                {
                    var errMsg = string.Empty;

                    //DateTime? expireDate = GetDateValue(licenseExpireDate);
                    var citizenID = ListDetail[i].CITIZEN_ID.ToString();
                    decimal money = ListDetail[i].LICENSE_FEE.ToDecimal();

                    //  var dup = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w => w.CITIZEN_ID == citizenID && w.LOAD_STATUS == "C" && w.PETITION_TYPE == petitionTypeCode ).FirstOrDefault();
                    // if (dup != null)
                    // {
                    //   errMsg = "เลขบัตรประจำตัวประชาชนเลขนี้ได้ทำการขอใบอนุญาตไปแล้ว " + Environment.NewLine;
                    //}

                    var duplicense = (from hl in base.ctx.AG_IAS_IMPORT_HEADER_TEMP
                                      join dl in base.ctx.AG_IAS_IMPORT_DETAIL_TEMP on hl.IMPORT_ID equals dl.IMPORT_ID
                                      join lt in base.ctx.AG_IAS_LICENSE_TYPE_R on hl.LICENSE_TYPE_CODE equals lt.LICENSE_TYPE_CODE
                                      join ld in base.ctx.AG_IAS_LICENSE_D on dl.CITIZEN_ID equals ld.ID_CARD_NO
                                      join lh in base.ctx.AG_IAS_LICENSE_H on ld.UPLOAD_GROUP_NO equals lh.UPLOAD_GROUP_NO


                                      where dl.CITIZEN_ID == citizenID
                                      && dl.LOAD_STATUS == "C"
                                      && ld.APPROVED == "Y"
                                      && lh.LICENSE_TYPE_CODE == licenseTypeCode
                                      && lh.PETITION_TYPE_CODE == petitionTypeCode
                                      && hl.LICENSE_TYPE_CODE == licenseTypeCode
                                      && hl.PETTITION_TYPE == petitionTypeCode
                                      select new DTO.DataItem
                                      {
                                          Name = lt.LICENSE_TYPE_NAME
                                      }).ToList();

                    if (duplicense != null && duplicense.Count != 0 && !string.IsNullOrEmpty(duplicense[0].Name))
                    {

                        errMsg = Resources.errorLicenseService_019 + duplicense[0].Name + Resources.errorLicenseService_020 + Environment.NewLine;
                    }


                    var waiting = base.ctx.AG_IAS_LICENSE_D.Where(w => w.ID_CARD_NO == citizenID && w.APPROVED == "W").FirstOrDefault();
                    if (waiting != null)
                    {
                        errMsg = Resources.errorLicenseService_021 + Environment.NewLine;
                    }


                    var enfree = base.ctx.AG_PETITION_TYPE_R.Where(w => w.PETITION_TYPE_CODE == petitionTypeCode).FirstOrDefault();
                    if (enfree.FEE.ToDecimal() != money)
                    {
                        errMsg = Resources.errorLicenseService_022 + Environment.NewLine;
                    }


                    //ตรวจสอบเอกสารแนบ
                    errMsg += ChkAttachDocument("41", compressFile, citizenID, licenseTypeCode, petitionTypeCode);


                    if (!string.IsNullOrEmpty(errMsg))
                    {
                        DTO.ReceiveLicenseDetail TempDeatail = new DTO.ReceiveLicenseDetail();
                        TempDeatail.ERR_MSG = errMsg + " " + ListDetail[i].ERR_MSG;
                        TempDeatail.CITIZEN_ID = citizenID;
                        UpdateErrorMessage(TempDeatail);
                    }


                }

                #endregion


                res.DataResponse.Detail = GetDetailbyGroupNo(impId);

                int total = res.DataResponse.Detail.Count();
                int missingTrans = res.DataResponse.Detail.Where(w => !string.IsNullOrEmpty(w.ERR_MSG)).Count();
                int rightTrans = res.DataResponse.Detail.Where(w => string.IsNullOrEmpty(w.ERR_MSG)).Count();

                res.DataResponse.Header.Add(new DTO.UploadHeader
                {
                    Totals = total,
                    MissingTrans = missingTrans,
                    RightTrans = rightTrans,
                    UploadFileName = fileName
                });
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_InsertAndCheckReceiveLicenseGroupUpload", ex);
            }
            return res;
        }





        private List<DTO.ReceiveLicenseDetail> GetDetailbyGroupNo(long impId)
        {
            var res = new List<DTO.ReceiveLicenseDetail>();

            try
            {
                res = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                   .Where(w => w.IMPORT_ID == impId)
                   .Select(s => new DTO.ReceiveLicenseDetail
                   {
                       ADDR1 = s.ADDR1,
                       ADDR2 = s.ADDR2,
                       AR_ANSWER = s.AR_ANSWER,
                       AREA_CODE = s.AREA_CODE,
                       CITIZEN_ID = s.CITIZEN_ID,
                       COMP_CODE = s.COMP_CODE,
                       CUR_ADDR = s.CUR_ADDR,
                       CUR_AREA_CODE = s.CUR_AREA_CODE,
                       EMAIL = s.EMAIL,
                       ERR_MSG = s.ERR_MSG,
                       IMPORT_ID = s.IMPORT_ID,
                       LICENSE_ACTIVE_DATE = s.LICENSE_ACTIVE_DATE,
                       LICENSE_EXPIRE_DATE = s.LICENSE_EXPIRE_DATE,
                       LICENSE_FEE = s.LICENSE_FEE,
                       LICENSE_NO = s.LICENSE_NO,
                       NAME = s.NAME,
                       OLD_COMP_CODE = s.OLD_COMP_CODE,
                       PETITION_TYPE = s.PETITION_TYPE,
                       REMARK = s.REMARK,
                       SEQ = s.SEQ,
                       SURNAME = s.SURNAME,
                       TEL_NO = s.TEL_NO,
                       TITLE_NAME = s.TITLE_NAME
                   }).ToList();
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_GetDetailbyGroupNo", ex);
            }
            return res;
        }

        private void UpdateErrorMessage(DTO.ReceiveLicenseDetail list)
        {


            try
            {

                string sql = "UPDATE AG_IAS_IMPORT_DETAIL_TEMP "
                             + " SET ERR_MSG = " + "'" + list.ERR_MSG + "' "
                             + " WHERE CITIZEN_ID = " + "'" + list.CITIZEN_ID + "'";

                OracleDB ora = new OracleDB();
                ora.GetDataSet(sql);



            }
            catch (Exception ex)
            {
                //res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_UpdateErrorMessage", ex);
            }


        }


        //ตรวจสอบเอกสารแนบในไฟล์ Compress Files 
        private string ChkAttachDocument(string function, DTO.CompressFileDetail compressFile, string citizenID, string licenseTypeCode, string petitionTypeCode)
        {
            string errMsg = "";

            var configFile = base.ctx.AG_IAS_DOCUMENT_TYPE_T.Where(w => w.FUNCTION_ID == function && w.LICENSE_TYPE_CODE == licenseTypeCode && w.STATUS == "A" && w.DOCUMENT_REQUIRE == "Y" && w.PETITION_TYPE_CODE == petitionTypeCode).ToList();

            List<DTO.AttachFileDetail> atFile = compressFile.AttatchFiles;



            foreach (var item in configFile)
            {
                var result = atFile.SingleOrDefault(r => r.FileName == citizenID + "_" + item.DOCUMENT_CODE);

                if (result == null)
                {
                    var err = base.ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(r => r.DOCUMENT_CODE == item.DOCUMENT_CODE);

                    errMsg += Resources.errorLicenseService_023 + err.DOCUMENT_NAME + "." + Environment.NewLine;

                }

            }


            return errMsg;
        }

        //วนเก็บรายการไฟล์ใน Compress Files
        public DTO.ResponseService<SummaryReceiveLicense>
                UploadDataLicense(DTO.DataLicenseRequest request)
        {

            var res = new DTO.ResponseService<SummaryReceiveLicense>();

            res.DataResponse = new SummaryReceiveLicense();

            try
            {

                DTO.ResponseService<LicenseFileHeader> licenseFileUpload = LicenseFileFactory.ConcreateLicenseRequest(ctx, request);
                if (licenseFileUpload.IsError)
                {
                    res.ErrorMsg = licenseFileUpload.ErrorMsg;
                    return res;
                }

                SummaryReceiveLicense sumary = licenseFileUpload.DataResponse.ValidateDataOfData();

                AG_IAS_IMPORT_HEADER_TEMP importHeadTemp = new AG_IAS_IMPORT_HEADER_TEMP();
                licenseFileUpload.DataResponse.MappingToEntity<LicenseFileHeader, AG_IAS_IMPORT_HEADER_TEMP>(importHeadTemp);


                ctx.AG_IAS_IMPORT_HEADER_TEMP.AddObject(importHeadTemp);

                //Insert to TEMP
                DateTime curdate = DateTime.Now;
                foreach (LicenseFileDetail item in licenseFileUpload.DataResponse.LicenseFileDetails)
                {
                    AG_IAS_IMPORT_DETAIL_TEMP detail = new AG_IAS_IMPORT_DETAIL_TEMP();
                    item.MappingToEntity<LicenseFileDetail, AG_IAS_IMPORT_DETAIL_TEMP>(detail);
                    ctx.AG_IAS_IMPORT_DETAIL_TEMP.AddObject(detail);


                    String defaultPath = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"].ToString();

                    foreach (AttachFileDetail attach in item.AttachFileDetails)
                    {

                        String attachpath = attach.FullFileName.Replace(defaultPath, "");
                        AG_IAS_ATTACH_FILE_LICENSE attachFile = new AG_IAS_ATTACH_FILE_LICENSE()
                        {
                            ID_ATTACH_FILE = OracleDB.GetGenAutoId(),
                            ID_CARD_NO = item.CITIZEN_ID,
                            ATTACH_FILE_TYPE = attach.FileTypeCode,
                            ATTACH_FILE_PATH = attachpath,
                            CREATED_BY = request.UserProfile.Id,
                            CREATED_DATE = curdate,
                            UPDATED_BY = request.UserProfile.Id,
                            UPDATED_DATE = curdate,
                            GROUP_LICENSE_ID = item.IMPORT_ID.ToString(),
                            FILE_STATUS = "W"
                        };

                        ctx.AG_IAS_ATTACH_FILE_LICENSE.AddObject(attachFile);
                    }
                }


                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                //#region varidateBystore
                //string _agentType = string.Empty;
                //_agentType = GetAgentype(importHeadTemp.LICENSE_TYPE_CODE);



                // ValidateReceiveLicenseTemp(importHeadTemp.IMPORT_ID, _agentType, importHeadTemp.PETTITION_TYPE, importHeadTemp.LICENSE_TYPE_CODE);


                //var detailTemp = GetDetailbyGroupNo(importHeadTemp.IMPORT_ID);

                //if (detailTemp != null)
                //{

                //    foreach (var item in detailTemp)
                //    {
                //        DTO.ReceiveLicenseDetail detail = sumary.ReceiveLicenseDetails.SingleOrDefault(r => r.IMPORT_ID == item.IMPORT_ID && r.SEQ == item.SEQ);

                //        if (String.IsNullOrEmpty(item.ERR_MSG))
                //        {
                //            detail.LOAD_STATUS = "T";
                //        }
                //        else
                //        {
                //            detail.LOAD_STATUS = "F";
                //            detail.ERR_MSG = item.ERR_MSG;
                //        }
                //    }


                //    Int32 errorAmount = sumary.ReceiveLicenseDetails.Count(a => a.LOAD_STATUS == "F");
                //    Int32 passAmount = sumary.ReceiveLicenseDetails.Count(a => a.LOAD_STATUS == "T");

                //    sumary.Header.RightTrans = passAmount;
                //    sumary.Header.MissingTrans = errorAmount;
                //    sumary.Header.Totals = detailTemp.Count();



                //    using (var ts = new TransactionScope())
                //    {
                //        base.ctx.SaveChanges();
                //        ts.Complete();
                //    }
                //}

                ////   AG_IAS_IMPORT_HEADER_TEMP headTemp = ctx.AG_IAS_IMPORT_HEADER_TEMP.SingleOrDefault(a => a.IMPORT_ID == importHeadTemp.IMPORT_ID);
                ////   if (headTemp != null)
                ////  {
                //// sumary.MessageError += headTemp.ERR_MSG;
                //// }

                //#endregion


                res.DataResponse = sumary;

            }
            catch (IOException ioEx)
            {
                res.ErrorMsg = Resources.errorLicenseService_024;
                LoggerFactory.CreateLog().Fatal("LicenseService_UploadDataLicense", ioEx);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorLicenseService_024;
                LoggerFactory.CreateLog().Fatal("LicenseService_UploadDataLicense", ex);
            }

            return res;

        }


        private string GetAgentype(string lincenseTypecode)
        {
            var res = new DTO.ResponseService<DataSet>();
            string agent = string.Empty;
            try
            {



                string sql = "Select AGENT_TYPE "
                        + " From Ag_License_Type_R A "
                        + "Where A.License_Type_Code=" + "'" + lincenseTypecode + "'";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;

                if (res != null)
                {
                    DataTable dt = res.DataResponse.Tables[0];
                    DataRow dr = dt.Rows[0];
                    agent = dr[0].ToString();
                }
                else
                {
                    agent = string.Empty;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetAgentype", ex);
            }
            return agent;

        }

        public DTO.ResponseService<List<DTO.AttatchFileLicense>> MoveExtachFile(String userId, String groupid, List<DTO.AttachFileDetail> attachFiles)
        {

            DTO.ResponseService<List<DTO.AttatchFileLicense>> res = new DTO.ResponseService<List<DTO.AttatchFileLicense>>();

            try
            {
                List<DTO.AttatchFileLicense> attchList = new List<DTO.AttatchFileLicense>();




                IList<AG_IAS_ATTACH_FILE_LICENSE> attachFileLicenses = ctx.AG_IAS_ATTACH_FILE_LICENSE.Where(a => a.GROUP_LICENSE_ID == groupid).ToList();


                //AG_IAS_IMPORT_HEADER_TEMP header = base.ctx.AG_IAS_IMPORT_HEADER_TEMP
                //.Where(w => w.IMPORT_ID == lgroupId)
                //.FirstOrDefault();



                if (attachFileLicenses != null || attachFileLicenses.Count > 0)
                {

                    string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
                    string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
                    string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
                    using (NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive))
                    {
                        foreach (AG_IAS_ATTACH_FILE_LICENSE d in attachFileLicenses)
                        {

                            String targetFileName = "";
                            FileInfo file = new FileInfo(Path.Combine(_netDrive, d.ATTACH_FILE_PATH));
                            if (file.Exists)
                            {
                                targetFileName = file.Name;
                            }
                            else
                            {
                                res.ErrorMsg = Resources.errorLicenseService_025;
                            }

                            String targetContainer = String.Format(@"{0}\{1}", AttachFileContainer, userId);

                            MoveFileResponse moveResponse = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                            {
                                CurrentContainer = ""
                                ,
                                CurrentFileName = d.ATTACH_FILE_PATH
                                ,
                                TargetContainer = targetContainer
                                ,
                                TargetFileName = targetFileName
                            }).Action();

                            if (moveResponse.Code != "0000")
                                throw new IOException(moveResponse.Message);

                            string[] aryFileName = d.ATTACH_FILE_PATH.Split('.');

                            d.ATTACH_FILE_PATH = moveResponse.TargetFullName;
                            d.UPDATED_BY = userId;
                            d.UPDATED_DATE = DateTime.Now;









                            //attchList.Add(new DTO.AttatchFileLicense
                            //{
                            //    ATTACH_FILE_PATH = moveResponse.TargetFullName,
                            //    ATTACH_FILE_TYPE = aryFileName.Length > 1
                            //                          ? aryFileName[aryFileName.Length - 1]
                            //                          : string.Empty,
                            //    CREATED_BY = userId,
                            //    CREATED_DATE = DateTime.Now,
                            //    UPDATED_BY = userId,
                            //    UPDATED_DATE = DateTime.Now,
                            //    ID_CARD_NO = aryFileName[0].Substring(0, 13)
                            //});
                        }
                    }

                    using (var ts = new TransactionScope())
                    {
                        base.ctx.SaveChanges();
                        ts.Complete();
                    }

                }

                DTO.ResponseMessage<bool> resSubmit = new DTO.ResponseMessage<bool>();

                resSubmit = SubmitGroupReceiveLicense(groupid, attachFileLicenses.ToList(), userId.ToString());
                if (resSubmit.IsError)
                {
                    res.ErrorMsg = resSubmit.ErrorMsg;
                    return res;
                }
                //res.DataResponse = attchList;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorLicenseService_024;
                LoggerFactory.CreateLog().Fatal("LicenseService_MoveExtachFile", ex);

            }

            return res;

        }









        private DTO.ResponseMessage<bool> SubmitGroupReceiveLicense(string groupId,
                                                              List<AG_IAS_ATTACH_FILE_LICENSE> attachs, string userid)
        {
            //var dup = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w=>w.LOAD_STATUS == "C" && w.CITIZEN_ID

            var res = new DTO.ResponseMessage<bool>();

            long lgroupID = Convert.ToInt64(groupId);

            DateTime? startDate = null;
            //ValidatePayment(groupId);

            var details = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                  .Where(w => w.IMPORT_ID == lgroupID)
                                  .ToList();


            int hasError = details.Where(delegate(AG_IAS_IMPORT_DETAIL_TEMP temp)
            {
                return !string.IsNullOrEmpty(temp.ERR_MSG);
            }).Count();

            if (hasError > 0)
            {
                res.ErrorMsg = Resources.errorLicenseService_005;
                return res;
            }
            //bool flagError = false;

            //foreach (var item in details)
            //{
            //    var dup = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w => w.CITIZEN_ID == item.CITIZEN_ID && w.LOAD_STATUS == "C" && w.PETITION_TYPE == item.PETITION_TYPE).FirstOrDefault();
            //    if (dup != null)
            //    {
            //        item.ERR_MSG = "เลขบัตรประชาชนเลขนี้ ได้ทำการขอใบอนุญาตไปแล้ว";
            //        flagError = true;
            //    }
            //}
            //if (flagError)
            //{
            //    // base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.AddObject();
            //    base.ctx.SaveChanges();
            //    return res;
            //}


            try
            {

                #region ข้อมูลส่วน Header


                //ตรวจสอบประเภทตัวแทน
                long lgroupId = Convert.ToInt64(groupId);
                AG_IAS_IMPORT_HEADER_TEMP header = base.ctx.AG_IAS_IMPORT_HEADER_TEMP
                                                        .Where(w => w.IMPORT_ID == lgroupId)
                                                        .FirstOrDefault();

                AG_LICENSE_TYPE_R licTypeEnt = base.ctx.AG_LICENSE_TYPE_R
                                                       .Where(w => w.LICENSE_TYPE_CODE == header.LICENSE_TYPE_CODE)
                                                       .FirstOrDefault();
                string _agentType = string.Empty;
                if (licTypeEnt != null)
                {
                    _agentType = licTypeEnt.AGENT_TYPE;

                }


                //AG_IAS_IMPORT_DETAIL_TEMP detail = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                //.Where(w => w.IMPORT_ID == lgroupId)
                //.FirstOrDefault();
                string _petition = string.Empty;
                if (header != null)
                {
                    _petition = header.PETTITION_TYPE;
                    //detail.PETITION_TYPE;
                }



                bool result = false;


                //chk Appove
                string approved = string.Empty;
                string appoveDoc = string.Empty;
                DateTime? appoveDateHead;
                DateTime? appoveDateDetail;

                if (header.APPROVE_COMPCODE == null)
                {
                    approved = "Y";
                    appoveDoc = "Y";
                    appoveDateHead = DateTime.Now;
                    appoveDateDetail = DateTime.Now;
                }
                else
                {
                    approved = "W";
                    appoveDoc = "W";
                    appoveDateHead = null;
                    appoveDateDetail = null;
                }



                #region InsertDataLicense

                //inset AG_IAS_LICENSE_H
                var licenseHead = base.ctx.AG_IAS_LICENSE_H.Where(w => w.UPLOAD_GROUP_NO == groupId).FirstOrDefault();

                var ComCode = (from a in ctx.AG_IAS_PERSONAL_T
                               where a.ID == userid.Trim()
                               select a.COMP_CODE).FirstOrDefault();

                //insert AG_IAS_LICENSE_H
                InsertLicenseHead(groupId, header, licenseHead, appoveDoc, appoveDateHead, ComCode);





                var licenseDetail = base.ctx.AG_IAS_LICENSE_D.Where(w => w.UPLOAD_GROUP_NO == groupId).FirstOrDefault();
                var detail = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w => w.IMPORT_ID == lgroupId).ToList();


                //insert AG_IAS_LICENSE_D
                InsertLicenseDetail(groupId, detail, licenseDetail, approved, appoveDateDetail, header.PETTITION_TYPE, header.LICENSE_TYPE_CODE);



                #endregion











                //#region call store


                //using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
                //{

                //    OracleCommand objCmd = new OracleCommand();

                //    objCmd.Connection = objConn;

                //    objCmd.CommandText = "AG_IAS_UPD_FILE_TO_DB";

                //    objCmd.CommandType = CommandType.StoredProcedure;



                //    objCmd.Parameters.Add("vimport_id", OracleDbType.Long).Value = groupId.ToLong();
                //    objCmd.Parameters.Add("agent_type", OracleDbType.Varchar2).Value = _agentType;
                //    objCmd.Parameters.Add("petition", OracleDbType.Varchar2).Value = _petition;

                //    var errFlag = new OracleParameter("err_flag", OracleDbType.Int32, ParameterDirection.InputOutput);
                //    errFlag.Value = 0;
                //    objCmd.Parameters.Add(errFlag);


                //    var errMess = new OracleParameter("err_mess", OracleDbType.Varchar2, ParameterDirection.Output);
                //    errMess.Size = 4000;
                //    errMess.Value = "";
                //    objCmd.Parameters.Add(errMess);

                //    objCmd.Parameters.Add("flag", OracleDbType.Varchar2, ParameterDirection.Input).Value = "L";

                //    var isDone = new OracleParameter("IS_DONE", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                //    isDone.Value = "N";
                //    objCmd.Parameters.Add(isDone);

                //    try
                //    {
                //        objConn.Open();
                //        objCmd.ExecuteNonQuery();
                //        result = (isDone.Value.ToString() == "Y");
                //    }
                //    catch (Exception ex)
                //    {
                //        res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                //        LoggerFactory.CreateLog().Fatal("LicenseService_SubmitGroupReceiveLicense", ex);
                //    }
                //    objConn.Close();
                //}

                //#endregion
                //base.ctx.SaveChanges();


                //update Head
                //Updaateheadby(userid.ToString(), groupId.ToString());


                #region IMPORT AG_IAS_ATTACH_FILE_LICENSE

                AG_IAS_SPECIAL_T_TEMP EntTemp = new AG_IAS_SPECIAL_T_TEMP();
                AG_TRAIN_SPECIAL_USED_T ciaEnt;


                var TarSpecialExam = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                                      from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                                      where
                                      dt.SPECIAL_TYPE_CODE_EXAM == ex.SPECIAL_TYPE_CODE
                                      && dt.EXAM_DISCOUNT_STATUS == "Y"
                                      && dt.STATUS == "A"
                                      select dt.DOCUMENT_CODE).ToList();


                var TarSpecialTrain = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                                       from ex in ctx.AG_TRAIN_SPECIAL_R
                                       where
                                       dt.SPECIAL_TYPE_CODE_TRAIN == ex.SPECIAL_TYPE_CODE
                                       && dt.TRAIN_DISCOUNT_STATUS == "Y"
                                       && dt.STATUS == "A"
                                       select dt.DOCUMENT_CODE).ToList();

                //Import AG_IAS_ATTACH_FILE_LICENSE

                foreach (AG_IAS_ATTACH_FILE_LICENSE att in attachs)
                {
                    //att.FILE_STATUS = "A";

                    if (TarSpecialExam.Contains(att.ATTACH_FILE_TYPE))
                    {
                        var specialCode = ctx.AG_IAS_DOCUMENT_TYPE.FirstOrDefault(a => a.DOCUMENT_CODE == att.ATTACH_FILE_TYPE);



                        string year = DateTime.Now.AddYears(543).Year.ToString();



                        var specialExam = ctx.AG_IAS_SPECIAL_T_TEMP.FirstOrDefault(s => s.ID_CARD_NO == att.ID_CARD_NO
                        && s.SPECIAL_TYPE_CODE == specialCode.SPECIAL_TYPE_CODE_EXAM);




                        if (detail != null)
                        {
                            var stdetail = detail.FirstOrDefault(w => w.CITIZEN_ID.Trim() == att.ID_CARD_NO.Trim()).START_DATE;
                            if (stdetail != null || stdetail != DateTime.MinValue)
                            {
                                startDate = stdetail;
                            }
                            else
                            {
                                startDate = DateTime.Now;
                            }


                        }

                        if (specialExam != null)
                        {
                            if (specialExam.STATUS != "Y")
                            {
                                specialExam.STATUS = "W";
                                specialExam.START_DATE = startDate;
                                specialExam.SEND_DATE = DateTime.Now;
                                specialExam.SEND_YEAR = year;
                                specialExam.USER_DATE = DateTime.Now;
                                specialExam.SEND_BY = "IAS";
                                specialExam.USER_ID = userid;
                            }
                        }
                        else
                        {

                            EntTemp = new AG_IAS_SPECIAL_T_TEMP();

                            EntTemp.ID_CARD_NO = att.ID_CARD_NO;
                            EntTemp.SPECIAL_TYPE_CODE = specialCode.SPECIAL_TYPE_CODE_EXAM;
                            EntTemp.START_DATE = startDate;
                            EntTemp.SEND_DATE = DateTime.Now;
                            EntTemp.SEND_YEAR = year;
                            EntTemp.USER_DATE = DateTime.Now;
                            EntTemp.SEND_BY = "IAS";
                            EntTemp.USER_ID = userid;
                            EntTemp.STATUS = "W";
                            EntTemp.EXAM_DISCOUNT_STATUS = "Y";

                            this.ctx.AG_IAS_SPECIAL_T_TEMP.AddObject(EntTemp);
                        }



                    }
                    else if (TarSpecialTrain.Contains(att.ATTACH_FILE_TYPE))
                    {
                        var specialCode = ctx.AG_IAS_DOCUMENT_TYPE.FirstOrDefault(a => a.DOCUMENT_CODE == att.ATTACH_FILE_TYPE);

                        string year = DateTime.Now.AddYears(543).Year.ToString();


                        var specialTrain = ctx.AG_IAS_SPECIAL_T_TEMP.FirstOrDefault(s => s.ID_CARD_NO.Trim() == att.ID_CARD_NO.Trim()
                        && s.SPECIAL_TYPE_CODE.Trim() == specialCode.SPECIAL_TYPE_CODE_TRAIN.Trim());



                        if (detail != null)
                        {
                            var stdetail = detail.FirstOrDefault(w => w.CITIZEN_ID.Trim() == att.ID_CARD_NO.Trim()).START_DATE;
                            if (stdetail != null || stdetail != DateTime.MinValue)
                            {
                                startDate = stdetail;
                            }
                            else
                            {
                                startDate = DateTime.Now;
                            }

                        }



                        if (specialTrain != null)
                        {
                            if (specialTrain.STATUS != "Y")
                            {
                                specialTrain.STATUS = "W";
                                specialTrain.START_DATE = startDate;
                                specialTrain.SEND_DATE = DateTime.Now;
                                specialTrain.SEND_YEAR = year;
                                specialTrain.USER_DATE = DateTime.Now;
                                specialTrain.SEND_BY = "IAS";
                                specialTrain.USER_ID = userid;
                            }

                            if (specialCode.SPECIAL_TYPE_CODE_TRAIN == "1006")
                            {
                                //licenseno
                                var detailLicense = detail.Where(w => w.CITIZEN_ID.Trim() == att.ID_CARD_NO.Trim()).FirstOrDefault();

                                //Insert AG_TRAIN_SPECIAL_USED_T
                                AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => id.ID_CARD_NO.Trim() == att.ID_CARD_NO.Trim()
                                && id.SPECIAL_TYPE_CODE == "1006" && id.LICENSE_NO == detailLicense.LICENSE_NO);

                                if (resCIAUsed == null)
                                {
                                    ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                    ciaEnt.ID_CARD_NO = att.ID_CARD_NO;
                                    ciaEnt.LICENSE_NO = detailLicense.LICENSE_NO;
                                    ciaEnt.SPECIAL_TYPE_CODE = "1006";
                                    ciaEnt.START_DATE = DateTime.Now;
                                    ciaEnt.USED_DATE = DateTime.Now;
                                    ciaEnt.USER_ID = userid;
                                    ciaEnt.USER_DATE = DateTime.Now;

                                    base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                }

                            }
                        }
                        else
                        {
                            EntTemp = new AG_IAS_SPECIAL_T_TEMP();
                            EntTemp.ID_CARD_NO = att.ID_CARD_NO;
                            EntTemp.SPECIAL_TYPE_CODE = specialCode.SPECIAL_TYPE_CODE_TRAIN;
                            EntTemp.START_DATE = startDate;
                            EntTemp.SEND_DATE = DateTime.Now;
                            EntTemp.SEND_YEAR = year;
                            EntTemp.USER_DATE = DateTime.Now;
                            EntTemp.SEND_BY = "IAS";
                            EntTemp.USER_ID = userid;
                            EntTemp.STATUS = "W";
                            EntTemp.TRAIN_DISCOUNT_STATUS = "Y";
                            if (specialCode.SPECIAL_TYPE_CODE_TRAIN.StartsWith("3"))
                            {
                                EntTemp.ID_ATTACH_FILE = att.ID_ATTACH_FILE;

                                if (startDate != null || startDate == DateTime.MinValue)
                                {
                                    DateTime stDateYear = Convert.ToDateTime(startDate).AddYears(5);
                                    EntTemp.END_DATE = stDateYear;
                                }

                            }
                            this.ctx.AG_IAS_SPECIAL_T_TEMP.AddObject(EntTemp);

                            if (specialCode.SPECIAL_TYPE_CODE_TRAIN == "1006")
                            {
                                //licenseno
                                var detailLicense = detail.Where(w => w.CITIZEN_ID.Trim() == att.ID_CARD_NO.Trim()).FirstOrDefault();

                                //Insert AG_TRAIN_SPECIAL_USED_T
                                AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => id.ID_CARD_NO.Trim() == att.ID_CARD_NO.Trim()
                                && id.SPECIAL_TYPE_CODE == "1006" && id.LICENSE_NO == detailLicense.LICENSE_NO);

                                if (resCIAUsed == null)
                                {
                                    ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                    ciaEnt.ID_CARD_NO = att.ID_CARD_NO;
                                    ciaEnt.LICENSE_NO = detailLicense.LICENSE_NO;
                                    ciaEnt.SPECIAL_TYPE_CODE = "1006";
                                    ciaEnt.START_DATE = DateTime.Now;
                                    ciaEnt.USED_DATE = DateTime.Now;
                                    ciaEnt.USER_ID = userid;
                                    ciaEnt.USER_DATE = DateTime.Now;

                                    base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                }

                            }
                        }





                    }

                    using (var ts = new TransactionScope())
                    {
                        base.ctx.SaveChanges();
                        ts.Complete();
                    }


                    // CreatePayment();
                    res.ResultMessage = true;
                }
                #endregion


                #endregion

            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorLicenseService_024;
                LoggerFactory.CreateLog().Fatal("LicenseService_MoveExtachFile", ex);
            }
            return res;
        }

        private void InsertLicenseDetail(string groupId, List<AG_IAS_IMPORT_DETAIL_TEMP> detail, AG_IAS_LICENSE_D licenseDetail, string approved, DateTime? appoveDateDetail, string pettionType, string licenseType)
        {
            try
            {
                if (licenseDetail == null)
                {
                    foreach (var dt in detail)
                    {
                        licenseDetail = new AG_IAS_LICENSE_D();
                        licenseDetail.UPLOAD_GROUP_NO = groupId;
                        licenseDetail.SEQ_NO = dt.SEQ;
                        licenseDetail.ORDERS = null;
                        licenseDetail.LICENSE_NO = dt.LICENSE_NO;
                        licenseDetail.LICENSE_DATE = dt.LICENSE_ACTIVE_DATE;
                        licenseDetail.LICENSE_EXPIRE_DATE = dt.LICENSE_EXPIRE_DATE;
                        licenseDetail.FEES = dt.LICENSE_FEE;
                        licenseDetail.ID_CARD_NO = dt.CITIZEN_ID;
                        licenseDetail.RENEW_TIMES = null;
                        licenseDetail.PRE_NAME_CODE = null;
                        licenseDetail.TITLE_NAME = dt.TITLE_NAME;
                        licenseDetail.NAMES = dt.NAME;
                        licenseDetail.LASTNAME = dt.SURNAME;
                        licenseDetail.ADDRESS_1 = dt.ADDR1;
                        licenseDetail.ADDRESS_2 = dt.ADDR2;
                        licenseDetail.AREA_CODE = dt.AREA_CODE;
                        licenseDetail.CURRENT_ADDRESS_1 = dt.CUR_ADDR;
                        licenseDetail.CURRENT_ADDRESS_2 = null;
                        licenseDetail.CURRENT_AREA_CODE = dt.CUR_AREA_CODE;
                        licenseDetail.EMAIL = dt.EMAIL;
                        if (string.IsNullOrEmpty(dt.AR_ANSWER))
                        {
                            licenseDetail.AR_DATE = null;
                        }
                        else
                        {
                            licenseDetail.AR_DATE = Convert.ToDateTime(dt.AR_ANSWER);
                        }
                        licenseDetail.OLD_COMP_CODE = dt.OLD_COMP_CODE;
                        licenseDetail.APPROVED = approved;
                        licenseDetail.APPROVED_DATE = appoveDateDetail;

                        //FindPayExpire
                        DateTime? payExpire = FindDateExpire(licenseDetail, pettionType, licenseType);
                        licenseDetail.PAY_EXPIRE = payExpire;


                        this.ctx.AG_IAS_LICENSE_D.AddObject(licenseDetail);

                        InsertLicensePersonal(licenseDetail);

                    }

                    UpdateImportDetailTemp(groupId);
                }
                else
                {
                    foreach (var dt in detail)
                    {
                        licenseDetail = base.ctx.AG_IAS_LICENSE_D
                                       .Where(w => w.UPLOAD_GROUP_NO == groupId && w.SEQ_NO == dt.SEQ).FirstOrDefault();


                        licenseDetail.LICENSE_NO = dt.LICENSE_NO;
                        licenseDetail.LICENSE_DATE = dt.LICENSE_ACTIVE_DATE;
                        licenseDetail.LICENSE_EXPIRE_DATE = dt.LICENSE_EXPIRE_DATE;
                        licenseDetail.FEES = dt.LICENSE_FEE;
                        licenseDetail.ID_CARD_NO = dt.CITIZEN_ID;
                        licenseDetail.TITLE_NAME = dt.TITLE_NAME;
                        licenseDetail.NAMES = dt.NAME;
                        licenseDetail.LASTNAME = dt.SURNAME;
                        licenseDetail.ADDRESS_1 = dt.ADDR1;
                        licenseDetail.ADDRESS_2 = dt.ADDR2;
                        licenseDetail.AREA_CODE = dt.AREA_CODE;
                        licenseDetail.CURRENT_ADDRESS_1 = dt.CUR_ADDR;
                        licenseDetail.CURRENT_AREA_CODE = dt.CUR_AREA_CODE;
                        licenseDetail.EMAIL = dt.EMAIL;
                        if (string.IsNullOrEmpty(dt.AR_ANSWER))
                        {
                            licenseDetail.AR_DATE = null;
                        }
                        else
                        {
                            licenseDetail.AR_DATE = Convert.ToDateTime(dt.AR_ANSWER);
                        }
                        licenseDetail.OLD_COMP_CODE = dt.OLD_COMP_CODE;

                        //FindPayExpire
                        DateTime? payExpire = FindDateExpire(licenseDetail, pettionType, licenseType);
                        licenseDetail.PAY_EXPIRE = payExpire;

                        InsertLicensePersonal(licenseDetail);
                    }

                    UpdateImportDetailTemp(groupId);
                }

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_InsertLicenseDetail", ex);
                throw ex;
            }


        }

        private DateTime? FindDateExpire(AG_IAS_LICENSE_D licenseDetail, string pettionType, string licenseType)
        {
            DateTime? payDateEx = null;
            var sql = string.Empty;
            OracleDB ora = new OracleDB();
            DateTime dateYear;

            if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
            {
                dateYear = Convert.ToDateTime(DateTime.Now).AddYears(-543).AddYears(1);
            }
            else
            {
                dateYear = Convert.ToDateTime(DateTime.Now).AddYears(1);
            }


            try
            {


                if ((new[] { "11", "15" }).Contains(pettionType))
                {
                    if ((new[] { "01", "02", "05", "06", "07", "08" }).Contains(licenseType))
                    {
                        sql = "SELECT a.TRAIN_EXP_DATE "
                        + "FROM AG_TRAIN_PERSON_T a "
                        + "WHERE a.ID_CARD_NO = '" + licenseDetail.ID_CARD_NO.Trim() + "' "
                        + "AND a.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                        + "AND a.TRAIN_EXP_DATE >= sysdate "
                        + "order by a.TRAIN_TIMES ASC ";

                        DataTable dt = ora.GetDataTable(sql);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            string trainDate = dr[0].ToString();
                            DateTime train_Date = Convert.ToDateTime(trainDate);
                            payDateEx = train_Date;

                        }
                        else
                        {
                            payDateEx = dateYear;
                        }


                    }
                    else if ((new[] { "11", "12" }).Contains(licenseType))
                    {
                        payDateEx = null;
                    }
                    else if ((new[] { "03", "04" }).Contains(licenseType))
                    {
                        string idValue = string.Empty;
                        if (licenseType == "03")
                        {
                            idValue = DTO.ConfigAgenType.AgentLife.GetEnumValue().ToString();
                        }
                        else if (licenseType == "04")
                        {
                            idValue = DTO.ConfigAgenType.AgentCasualty.GetEnumValue().ToString();
                        }

                        var result = GetAgentCheckTrain(idValue);

                        if (result.ResultMessage == true)
                        {


                            sql = "SELECT a.TRAIN_EXP_DATE "
                            + "FROM AG_TRAIN_PERSON_T a "
                            + "WHERE a.ID_CARD_NO = '" + licenseDetail.ID_CARD_NO.Trim() + "' "
                            + "AND a.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                            + "AND a.TRAIN_EXP_DATE >= sysdate "
                            + "order by a.TRAIN_TIMES ASC ";

                            DataTable dt = ora.GetDataTable(sql);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                string trainDate = dr[0].ToString();
                                DateTime train_Date = Convert.ToDateTime(trainDate);

                                payDateEx = train_Date;

                            }
                            else
                            {
                                payDateEx = dateYear;
                            }
                        }
                        else
                        {
                            payDateEx = dateYear;
                        }

                    }

                }
                else if (pettionType == "18")
                {
                    if ((new[] { "01", "02", "05", "06", "07", "08" }).Contains(licenseType))
                    {
                        sql = "SELECT a.EXPIRE_DATE "
                          + "FROM AG_LICENSE_T a "
                          + "WHERE a.LICENSE_NO = '" + licenseDetail.LICENSE_NO.Trim() + "' ";

                        DataTable dt = ora.GetDataTable(sql);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            string expireDate = dr[0].ToString();

                            DateTime date_Date = Convert.ToDateTime(expireDate);


                            payDateEx = date_Date;
                        }
                        else
                        {
                            payDateEx = dateYear;
                        }
                    }
                }
                else if (pettionType == "17")
                {
                    if ((new[] { "01", "02", "05", "06", "07", "08" }).Contains(licenseType))
                    {

                        sql = "SELECT a.EXPIRE_DATE "
                         + "FROM AG_LICENSE_T a "
                         + "WHERE a.LICENSE_NO = '" + licenseDetail.LICENSE_NO.Trim() + "' ";

                        DataTable dtLicense = ora.GetDataTable(sql);

                        if (dtLicense != null && dtLicense.Rows.Count > 0)
                        {
                            DataRow drLicense = dtLicense.Rows[0];
                            string expireDate = String.Format("{0:dd/MM/yyy}", drLicense[0].ToString());


                            DateTime date_Expire = DateTime.Parse(expireDate);



                            sql = "SELECT a.TRAIN_EXP_DATE "
                            + "FROM AG_TRAIN_PERSON_T a "
                            + "WHERE a.ID_CARD_NO = '" + licenseDetail.ID_CARD_NO.Trim() + "' "
                            + "AND a.LICENSE_TYPE_CODE ='" + licenseType.Trim() + "' "
                            + "AND a.TRAIN_EXP_DATE >= sysdate "
                            + "order by a.TRAIN_TIMES ASC ";

                            DataTable dtTrain = ora.GetDataTable(sql);

                            if (dtTrain != null && dtTrain.Rows.Count > 0)
                            {

                                DataRow drTrain = dtTrain.Rows[0];
                                string trainDate = String.Format("{0:dd/MM/yyy}", drTrain[0].ToString());

                                DateTime train_Date = DateTime.Parse(trainDate);


                                int dateCompare = DateTime.Compare(train_Date, date_Expire);


                                if (dateCompare == -1)
                                {
                                    payDateEx = train_Date;
                                }
                                else
                                {
                                    payDateEx = date_Expire;
                                }
                            }
                            else
                            {
                                payDateEx = date_Expire;
                            }


                        }
                        else
                        {
                            sql = "SELECT a.TRAIN_EXP_DATE "
                            + "FROM AG_TRAIN_PERSON_T a "
                            + "WHERE a.ID_CARD_NO = '" + licenseDetail.ID_CARD_NO.Trim() + "' "
                            + "AND a.LICENSE_TYPE_CODE ='" + licenseType.Trim() + "' "
                            + "AND a.TRAIN_EXP_DATE >= sysdate "
                            + "order by a.TRAIN_TIMES ASC ";

                            DataTable dtTrain = ora.GetDataTable(sql);

                            if (dtTrain != null && dtTrain.Rows.Count > 0)
                            {
                                DataRow drTrain = dtTrain.Rows[0];
                                string trainDate = drTrain[0].ToString();
                                DateTime train_Date = Convert.ToDateTime(trainDate);
                                payDateEx = train_Date;

                            }
                            else
                            {
                                payDateEx = dateYear;
                            }
                        }
                    }
                }
                else if (pettionType == "16")
                {
                    sql = "SELECT a.EXPIRE_DATE "
                          + "FROM AG_LICENSE_T a "
                          + "WHERE a.LICENSE_NO = '" + licenseDetail.LICENSE_NO.Trim() + "' ";

                    DataTable dt = ora.GetDataTable(sql);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        string expireDate = dr[0].ToString();

                        DateTime date_Date = Convert.ToDateTime(expireDate);


                        payDateEx = date_Date;
                    }
                    else
                    {
                        payDateEx = dateYear;
                    }

                }
                else if ((new[] { "13", "14" }).Contains(pettionType))
                {
                    sql = "SELECT a.EXPIRE_DATE "
                           + "FROM AG_LICENSE_T a "
                           + "WHERE a.LICENSE_NO = '" + licenseDetail.LICENSE_NO.Trim() + "' ";

                    DataTable dt = ora.GetDataTable(sql);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        string expireDate = dr[0].ToString();

                        Boolean pass = false;
                        DateTime date_Date = Convert.ToDateTime(expireDate);

                        while (!pass)
                        {
                            if ((date_Date.DayOfWeek == DayOfWeek.Saturday) || (date_Date.DayOfWeek == DayOfWeek.Sunday))
                            {
                                date_Date = date_Date.AddDays(1);
                            }
                            else
                            {
                                if (checkEventDate(Convert.ToString(date_Date)))// false = stop date true = work date
                                {
                                    pass = true;
                                }
                                else
                                {
                                    date_Date = date_Date.AddDays(1);
                                }
                            }

                        }

                        payDateEx = date_Date;


                    }
                    else
                    {
                        payDateEx = dateYear;
                    }
                }


                if (payDateEx == DateTime.MinValue)
                {
                    payDateEx = dateYear;
                }
            }
            catch (Exception ex)
            {
                // res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_FindDateExpire", ex);
            }



            return payDateEx;
        }


        private bool checkEventDate(string strDate) //01/01/2004
        {
            Boolean pass = false;
            DateTime tempDate = Convert.ToDateTime(strDate);
            string D = Convert.ToString(tempDate.Day);
            string M = Convert.ToString(tempDate.Month);
            string Y = Convert.ToString(tempDate.Year);
            strDate = (D + "/" + M + "/" + Y);
            //int Iyear = Convert.ToInt16(strDate.Substring(6, 4));
            //Iyear = Iyear - 543;
            try
            {
                //strDate = strDate.Substring(0, 6)+Convert.ToString(Iyear);

                string sql = "select * from GBDOI.gb_holiday_r where hl_date = to_date('" + strDate + "','dd/mm/yyyy')";
                OracleDB ora = new OracleDB();
                DataSet DS = ora.GetDataSet(sql);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    pass = true;
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_checkEventDate", ex);
            }
            return pass;
        }





        private void InsertLicensePersonal(AG_IAS_LICENSE_D licenseDetail)
        {
            AG_PERSONAL_T per = new AG_PERSONAL_T();
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            var perDetail = new AG_PERSONAL_T();
            int preNameCode = 0;
            try
            {
                // var perDetail = base.ctx.AG_PERSONAL_T.FirstOrDefault(w => w.ID_CARD_NO == licenseDetail.ID_CARD_NO);

                sql = "SELECT a.ID_CARD_NO "
                       + "FROM AG_PERSONAL_T a "
                       + "WHERE a.ID_CARD_NO = '" + licenseDetail.ID_CARD_NO.Trim() + "' ";

                DataTable dtPersonal = ora.GetDataTable(sql);


                //var appDetail = base.ctx.AG_APPLICANT_T.Where(w => w.ID_CARD_NO == licenseDetail.ID_CARD_NO
                //                                                    && w.RESULT == "P"
                //                                                    && (w.LICENSE == "N" || w.LICENSE == null)
                //                                                    && w.RECORD_STATUS == null).OrderBy(w => w.APPLY_DATE).FirstOrDefault();


                // if (perDetail != null)
                //{
                //if (perDetail.BIRTH_DATE != null && appDetail != null)
                //{
                //    perDetail.BIRTH_DATE = appDetail.BIRTH_DATE;
                //}
                //if (string.IsNullOrEmpty(perDetail.EDUCATION_CODE) && appDetail != null)
                //{
                //    perDetail.EDUCATION_CODE = appDetail.EDUCATION_CODE;
                //}
                // if (string.IsNullOrEmpty(perDetail.TELEPHONE) && appDetail != null)
                //{
                //perDetail.TELEPHONE = appDetail.TELEPHONE;
                //}
                //if (string.IsNullOrEmpty(perDetail.SEX) && appDetail != null)
                //{
                //    perDetail.SEX = appDetail.SEX;
                //}


                //remark กรณีเปลี่ยนชื่อ
                //short preNameCodeInt = Convert.ToInt16(perDetail.PRE_NAME_CODE);

                //string titleName = (from a in ctx.VW_IAS_TITLE_NAME_PRIORITY
                //                    where a.ID == preNameCodeInt
                //                    select a.NAME).FirstOrDefault();


                //string oldName = titleName.Trim() + " " + perDetail.NAMES.Trim() + " " + perDetail.LASTNAME.Trim();
                //string newName = licenseDetail.TITLE_NAME.Trim() + " " + licenseDetail.NAMES.Trim() + " " + licenseDetail.LASTNAME.Trim();

                //if (oldName == newName)
                //{
                //    perDetail.REMARK = null;
                //}
                //else
                //{
                //    perDetail.REMARK = "เปลี่ยนชื่อจาก " + oldName + " เมื่อวันที่ " + string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                //    perDetail.NAME_CHG_DATE = DateTime.Now;
                //}

                //}




                if (dtPersonal.Rows.Count == 0)
                {
                    perDetail = new AG_PERSONAL_T();

                    sql = "SELECT PRE_NAME_CODE,NAMES,LASTNAME,BIRTH_DATE,SEX,EDUCATION_CODE,ADDRESS1,ADDRESS2,AREA_CODE,TELEPHONE,ZIPCODE  "
                       + "FROM AG_APPLICANT_T a "
                       + "WHERE a.ID_CARD_NO = '" + licenseDetail.ID_CARD_NO.Trim() + "' "
                       + "AND a.RESULT = 'P' "
                       + "AND (a.LICENSE = 'N' or a.LICENSE is null) "
                       + "order by a.APPLY_DATE ";

                    DataTable dtExam = ora.GetDataTable(sql);



                    if (dtExam.Rows.Count != 0)
                    {
                        DataRow drExam = dtExam.Rows[0];


                        if (!string.IsNullOrEmpty(drExam["PRE_NAME_CODE"].ToString()))
                        {
                            perDetail.PRE_NAME_CODE = drExam["PRE_NAME_CODE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["NAMES"].ToString()))
                        {
                            perDetail.NAMES = drExam["NAMES"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["LASTNAME"].ToString()))
                        {
                            perDetail.LASTNAME = drExam["LASTNAME"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["BIRTH_DATE"].ToString()))
                        {
                            perDetail.BIRTH_DATE = Convert.ToDateTime(drExam["BIRTH_DATE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(drExam["SEX"].ToString()))
                        {
                            perDetail.SEX = drExam["SEX"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["EDUCATION_CODE"].ToString()))
                        {
                            perDetail.EDUCATION_CODE = drExam["EDUCATION_CODE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["ADDRESS1"].ToString()))
                        {
                            perDetail.ADDRESS1 = drExam["ADDRESS1"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["ADDRESS2"].ToString()))
                        {
                            perDetail.ADDRESS2 = drExam["ADDRESS2"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["AREA_CODE"].ToString()))
                        {
                            perDetail.AREA_CODE = drExam["AREA_CODE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["TELEPHONE"].ToString()))
                        {
                            perDetail.TELEPHONE = drExam["TELEPHONE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(drExam["ZIPCODE"].ToString()))
                        {
                            perDetail.ZIPCODE = drExam["ZIPCODE"].ToString();
                        }
                    }
                    else
                    {
                        sql = "SELECT a.ID "
                        + "FROM VW_IAS_TITLE_NAME_PRIORITY a "
                        + "WHERE a.NAME = '" + licenseDetail.TITLE_NAME + "' ";

                        DataTable dtTitleName = ora.GetDataTable(sql);

                        if (dtTitleName != null && dtTitleName.Rows.Count > 0)
                        {
                            DataRow drTitleName = dtTitleName.Rows[0];
                            preNameCode = drTitleName[0].ToInt();
                        }

                        perDetail.ID_CARD_NO = licenseDetail.ID_CARD_NO;
                        if (preNameCode != 0)
                        {
                            perDetail.PRE_NAME_CODE = preNameCode.ToString();
                        }

                        perDetail.NAMES = licenseDetail.NAMES;
                        perDetail.LASTNAME = licenseDetail.LASTNAME;
                    }

                    string zipCode = GetZipCode(licenseDetail.AREA_CODE);

                    perDetail.ID_CARD_NO = licenseDetail.ID_CARD_NO;
                    perDetail.NATIONALITY = "ไทย";
                    perDetail.LOCAL_AREA_CODE = licenseDetail.AREA_CODE;
                    perDetail.LOCAL_AREA_CODE = licenseDetail.AREA_CODE.Substring(0, 2);
                    perDetail.ADDRESS1 = licenseDetail.ADDRESS_1;
                    perDetail.ADDRESS2 = licenseDetail.ADDRESS_2;
                    perDetail.LOCAL_ZIPCODE = zipCode;
                    perDetail.REMARK = null;
                    perDetail.NAME_CHG_DATE = DateTime.Now;

                    this.ctx.AG_PERSONAL_T.AddObject(perDetail);
                }


                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }




            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_InsertLicensePersonal", ex);
                throw ex;
            }
        }



        private string GetZipCode(string areaCode)
        {
            string zipCode = string.Empty;

            try
            {

                string sql = "SELECT PV_ZIPCODE "
                             + "FROM GB_PROVINCE_R "
                             + "WHERE PV_CODE = SUBSTR('" + areaCode + "',1,2) "
                             + "AND PV_AMPUR = SUBSTR('" + areaCode + "',3,2) "
                             + "AND PV_TUMBON =SUBSTR('" + areaCode + "',5,4) ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    zipCode = dr["PV_ZIPCODE"].ToString();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


            return zipCode;
        }

        private void UpdateImportDetailTemp(string groupId)
        {
            try
            {
                int impID = groupId.Trim().ToInt();

                AG_IAS_IMPORT_DETAIL_TEMP licdetail = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                                         .Where(w => w.IMPORT_ID == impID)
                                                         .FirstOrDefault();

                if (licdetail != null)
                {
                    string sql = "UPDATE AG_IAS_IMPORT_DETAIL_TEMP "
                                 + " SET LOAD_STATUS = 'C' "
                                 + " WHERE IMPORT_ID = " + groupId.Trim() + " ";

                    OracleDB ora = new OracleDB();
                    ora.ExecuteCommand(sql);
                }


            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_UpdateImportDetailTemp", ex);
                throw ex;
            }
        }

        private void InsertLicenseHead(string groupId, AG_IAS_IMPORT_HEADER_TEMP header, AG_IAS_LICENSE_H licenseHead, string appoveDoc, DateTime? appoveDateHead, string comCode)
        {
            try
            {

                if (licenseHead == null)
                {
                    licenseHead = new AG_IAS_LICENSE_H();
                    licenseHead.UPLOAD_GROUP_NO = groupId;
                    licenseHead.COMP_CODE = header.COMP_CODE;
                    licenseHead.COMP_NAME = header.COMP_NAME;
                    licenseHead.TRAN_DATE = header.SEND_DATE;
                    licenseHead.LOTS = header.TOTAL_AGENT;
                    licenseHead.MONEY = header.TOTAL_FEE;
                    licenseHead.LICENSE_TYPE_CODE = header.LICENSE_TYPE_CODE;
                    licenseHead.FILENAME = header.FILE_NAME;
                    licenseHead.PETITION_TYPE_CODE = header.PETTITION_TYPE;
                    licenseHead.APPROVE_COMPCODE = header.APPROVE_COMPCODE;
                    licenseHead.APPROVED_DOC = appoveDoc;
                    licenseHead.UPLOAD_BY_SESSION = comCode.Trim();
                    licenseHead.APPROVED_DATE = appoveDateHead;
                    this.ctx.AG_IAS_LICENSE_H.AddObject(licenseHead);
                }
                else
                {
                    licenseHead.COMP_CODE = header.COMP_CODE;
                    licenseHead.COMP_NAME = header.COMP_NAME;
                    licenseHead.TRAN_DATE = header.SEND_DATE;
                    licenseHead.LOTS = header.TOTAL_AGENT;
                    licenseHead.MONEY = header.TOTAL_FEE;
                    licenseHead.FILENAME = header.FILE_NAME;
                    licenseHead.PETITION_TYPE_CODE = header.LICENSE_TYPE_CODE;
                    licenseHead.APPROVE_COMPCODE = header.APPROVE_COMPCODE;
                    licenseHead.UPLOAD_BY_SESSION = comCode.Trim();
                }

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_InsertLicenseHead", ex);
                throw ex;
            }

        }
        private DTO.ResponseService<List<DTO.AttachFileDetail>> ExtractFile(String compressFile)
        {
            DTO.ResponseService<List<DTO.AttachFileDetail>> res = new DTO.ResponseService<List<DTO.AttachFileDetail>>();
            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];


            using (NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive))
            {
                var list = new List<DTO.AttachFileDetail>();

                //******************** ******* Check FileExist************************************/
                FileInfo compressFileInfo = new FileInfo(Path.Combine(DefaultNetDrive, compressFile));

                if (!compressFileInfo.Exists)
                {
                    res.ErrorMsg = Resources.errorExtractFileLicenseRequestHelper_001;
                    return res;
                }

                //ถ้าไม่ใช่ไฟล์ .ZIP หรือ .RAR 
                if (!".ZIP_.RAR".Contains(compressFileInfo.Extension.ToUpper()))
                {
                    res.ErrorMsg = Resources.errorExtractFileLicenseRequestHelper_002;
                    return res;
                }
                //****************************************************************/


                String _targetFullPath = Path.GetDirectoryName(compressFileInfo.FullName);

                DirectoryInfo targetDirectory = new DirectoryInfo(_targetFullPath);
                if (!targetDirectory.Exists)
                {
                    targetDirectory.Create();
                }

                Utils.CompressFile cf = new Utils.CompressFile();

                bool result = false;
                var fileInRAR_Zip = new List<string>();

                if (compressFileInfo.Extension == ".ZIP")
                {
                    fileInRAR_Zip = cf.GetFilesInZip(compressFileInfo.FullName);
                    result = cf.ZipExtract(compressFileInfo.FullName, targetDirectory.FullName);
                }
                else if (compressFileInfo.Extension == ".RAR")
                {
                    fileInRAR_Zip = cf.GetFilesInRar(compressFileInfo.FullName);
                    result = cf.RarExtract(compressFileInfo.FullName, targetDirectory.FullName);
                }

                //ถ้าผลการ Extract File เกิดข้อผิดพลาด
                if (!result)
                {
                    res.ErrorMsg = Resources.errorExtractFileLicenseRequestHelper_003;
                    return res;
                }


                if (fileInRAR_Zip.Count > 0)
                {
                    res.DataResponse = new List<DTO.AttachFileDetail>();
                    //วนเก็บรายการใน Zip File
                    for (int i = 0; i < fileInRAR_Zip.Count; i++)
                    {
                        //เก็บรายการ Path จริงแปะเข้าไฟล์
                        string file = fileInRAR_Zip[i].Replace(@"/", @"\");

                        //string fullFilePath = tempFolder + @"\" + file;

                        FileInfo fInfo = new FileInfo(Path.GetFullPath(file));

                        string[] ary = fInfo.Name.Split('.');
                        string fileExt = fInfo.Extension;
                        string fName = ary.Length > 0 ? ary[0] : string.Empty;

                        if (!string.IsNullOrEmpty(fName))
                        {
                            res.DataResponse.Add(new DTO.AttachFileDetail
                            {
                                FileName = fName,
                                Extension = fInfo.Extension,
                                FullFileName = Path.Combine(_targetFullPath, fInfo.Name),
                                MapFileName = Path.Combine(_targetFullPath, fInfo.Name).Replace(DefaultNetDrive, "")
                            });
                        }
                    }

                }
            }

            return res;
        }

        //private DTO.

        /// <summary>
        /// Submit ข้อมูลที่ Upload เข้าระบบ
        /// </summary>
        /// <param name="groupNo">รหัสกลุ่ม Upload</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseService<string> SubmitReceiveLicenseGroupUpload(string groupId, List<DTO.AttatchFileLicense> list)
        {
            var res = new DTO.ResponseService<string>();
            try
            {
                long lgroupID = Convert.ToInt64(groupId);
                var header = base.ctx.AG_IAS_IMPORT_HEADER_TEMP
                                     .SingleOrDefault(w => w.IMPORT_ID == lgroupID);

                var entHeader = new AG_IAS_LICENSE_H();

                header.MappingToEntity(entHeader);

                base.ctx.AG_IAS_LICENSE_H
                        .AddObject(entHeader);

                var details = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                      .Where(w => w.IMPORT_ID == lgroupID)
                                      .ToList();

                foreach (AG_IAS_IMPORT_DETAIL_TEMP d in details)
                {
                    //this.ValidateReceiveLicenseTemp(groupId.ToLong(),
                }

                int hasError = details.Where(delegate(AG_IAS_IMPORT_DETAIL_TEMP temp)
                                            {
                                                return !string.IsNullOrEmpty(temp.ERR_MSG);
                                            })
                                       .Count();

                if (hasError > 0)
                {
                    res.ErrorMsg = Resources.errorLicenseService_005;
                    return res;
                }

                int rowAffected = 0;
                foreach (AG_IAS_IMPORT_DETAIL_TEMP d in details)
                {
                    var ent = new AG_IAS_LICENSE_D();
                    d.MappingToEntity(ent);
                    base.ctx.AG_IAS_LICENSE_D.AddObject(ent);
                    rowAffected += 1;
                }
                foreach (DTO.AttatchFileLicense l in list)
                {
                    var attach = new AG_IAS_ATTACH_FILE_LICENSE();
                    l.MappingToEntity(attach);
                    attach.ID_ATTACH_FILE = OracleDB.GetGenAutoId();
                    base.ctx.AG_IAS_ATTACH_FILE_LICENSE.AddObject(attach);
                }
                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }
                res.DataResponse = string.Format("บันทึกข้อมูลเรียบร้อย จำนวน {0} รายการ", rowAffected);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_SubmitReceiveLicenseGroupUpload", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetLicenseVerifyHead(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string Compcode, string requestCompCode, string CountPage, int pageNo, int recordPerPage, string StatusApprove)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();

                sb.Append(GetCriteria("LH.PETITION_TYPE_CODE = '{0}' AND ", petitionTypeCode));

                if (!string.IsNullOrEmpty(requestCompCode))
                {
                    if (requestCompCode.Length == 15)
                    {
                        sb.Append(" LENGTH(LH.upload_by_session) = 15  AND ");
                    }
                    else if (requestCompCode == "ทั้งหมด")
                    {

                    }
                    else
                    {
                        sb.Append(" LH.upload_by_session = " + requestCompCode + " AND ");
                    }
                }
                if (!string.IsNullOrEmpty(StatusApprove))
                {
                    if (StatusApprove == "0")//ทั้งหมด
                    {

                    }
                    else
                    {
                        if (requestCompCode == "ทั้งหมด")
                        {
                            sb.Append("  LH.APPROVED_DOC = '" + StatusApprove + "' and");
                        }
                        else
                        {
                            sb.Append(" LH.APPROVED_DOC = '" + StatusApprove + "' and");
                        }
                    }
                }
                if (startDate != null && toDate != null)
                {
                    sb.Append("(to_char(LH.TRAN_DATE) BETWEEN TO_DATE('" +
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                }
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                string sql = string.Empty;

                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;
                if (CountPage == "Y")
                {
                    sql = "SELECT	(SELECT COUNT(*) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE AND LH.APPROVE_COMPCODE = '" + Compcode + "'  " + crit + ") AS TOTAL , " +
                                  "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                                  "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                                  "         LH.UPLOAD_GROUP_NO ,substr(substr(LH.FILENAME,Instr(LH.FILENAME,'\\')+1),Instr(substr(LH.FILENAME,Instr(LH.FILENAME,'\\')+1),'\\')+1) FILENAME  , PT.PETITION_TYPE_NAME,case LENGTH(lh.upload_by_session) when 15  then T.NAMES ||'  '|| T.LASTNAME  when 4 then com.name else asso.association_name  end as names " +
                                  "FROM	    AG_IAS_PETITION_TYPE_R PT , " +
                                  "         AG_LICENSE_TYPE_R	    LT, " +
                                  "         AG_IAS_LICENSE_H	    LH " +
                                  "left join AG_IAS_PERSONAL_T T on lh.upload_by_session=T.ID " +
                                  "left join VW_IAS_COM_CODE COM on LH.upload_by_session =COM.ID " +
                                  "left join AG_IAS_ASSOCIATION ASSO on LH.upload_by_session=ASSO.association_code " +
                                  "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	AND " +
                                  "         LH.APPROVE_COMPCODE = '" + Compcode + "' "
                                  + crit
                                  + " order by LH.TRAN_DATE";
                }
                else
                {
                    sql = "Select * from ( " +
                        "SELECT	(SELECT COUNT(*) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE AND LH.APPROVE_COMPCODE = '" + Compcode + "'  " + crit + ") AS TOTAL , " +
                      "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                      "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                      "         LH.UPLOAD_GROUP_NO , substr(substr(LH.FILENAME,Instr(LH.FILENAME,'\\')+1),Instr(substr(LH.FILENAME,Instr(LH.FILENAME,'\\')+1),'\\')+1) FILENAME  , pt.petition_type_code,PT.PETITION_TYPE_NAME,ROW_NUMBER() OVER (ORDER BY LH.TRAN_DATE) RUN_NO ,case LENGTH(lh.upload_by_session) when 15  then T.NAMES ||'  '|| T.LASTNAME  when 4 then com.name else asso.association_name  end as names " +
                      "FROM	    AG_IAS_PETITION_TYPE_R PT , " +
                      "         AG_LICENSE_TYPE_R	    LT, " +
                      "         AG_IAS_LICENSE_H	    LH " +
                      "left join AG_IAS_PERSONAL_T T on lh.upload_by_session=T.ID " +
                      "left join VW_IAS_COM_CODE COM on LH.upload_by_session =COM.ID " +
                      "left join AG_IAS_ASSOCIATION ASSO on LH.upload_by_session=ASSO.association_code " +
                      "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	AND " +
                      "         LH.APPROVE_COMPCODE = '" + Compcode + "' "
                      + crit + " order by LH.TRAN_DATE )A " + critRecNo;

                }
                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);


                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseVerifyHead", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลที่รออนุมัติออกใบอนุญาต
        /// </summary>
        /// <param name="petitionTypeCode">รหัสประเภทการออกใบอนุญาต</param>
        /// <param name="startDate">วันทำรายการ (เริ่ม)</param>
        /// <param name="toDate">วันทำรายการ (สิ้นสุด)</param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet>
            GetLicenseVerify(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string Compcode, string requestCompCode)
        {
            var res = new DTO.ResponseService<DataSet>();

            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();

                sb.Append(GetCriteria("LH.PETITION_TYPE_CODE = '{0}' AND ", petitionTypeCode));

                if (!string.IsNullOrEmpty(requestCompCode))
                {
                    sb.Append(" LH.COMP_CODE = " + requestCompCode + " AND ");
                }

                if (startDate != null && toDate != null)
                {
                    sb.Append("(LH.TRAN_DATE BETWEEN TO_DATE('" +
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                }


                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;

                string sql = "SELECT	(SELECT PTT.NAMES FROM AG_IAS_PERSONAL_T PTT WHERE PTT.ID = LD.APPROVED_BY ) AS NAME , " +
                              "         LD.ID_CARD_NO , LD.TITLE_NAME || ' ' || LD.NAMES || ' ' || LD.LASTNAME FLNAME, LH.COMP_CODE , LH.COMP_NAME , LD.APPROVED_DATE ,LD.APPROVED_By, " +
                              "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, " +
                              "         LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED  " +
                              "FROM	    AG_IAS_LICENSE_D	    LD, " +
                              "         AG_IAS_LICENSE_H	    LH, " +
                              "         AG_LICENSE_TYPE_R	    LT " +
                              "WHERE	LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	AND " +
                              "         LD.UPLOAD_GROUP_NO		=	LH.UPLOAD_GROUP_NO      AND LH.APPROVE_COMPCODE = " + Compcode + "  " + crit;

                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseVerify", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลที่รออนุมัติออกใบอนุญาต
        /// </summary>
        /// <param name="petitionTypeCode">รหัสประเภทการออกใบอนุญาต</param>
        /// <param name="startDate">วันทำรายการ (เริ่ม)</param>
        /// <param name="toDate">วันทำรายการ (สิ้นสุด)</param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet>
            GetLicenseVerifyByRequest(string petitionTypeCode,
                             DateTime? startDate,
                             DateTime? toDate, string Compcode)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();

                sb.Append(GetCriteria("LH.PETITION_TYPE_CODE = '{0}' AND ", petitionTypeCode));

                if (startDate != null && toDate != null)
                {
                    sb.Append("(LH.TRAN_DATE BETWEEN TO_DATE('" +
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                }


                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;

                string sql = "SELECT	(SELECT PTT.NAMES FROM AG_IAS_PERSONAL_T PTT WHERE PTT.ID = LD.APPROVED_BY ) AS NAME , " +
                              "         LD.ID_CARD_NO , LD.TITLE_NAME || ' ' || LD.NAMES || ' ' || LD.LASTNAME FLNAME, LH.COMP_CODE , LH.COMP_NAME , LD.APPROVED_DATE ,LD.APPROVED_By, " +
                              "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, " +
                              "         LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED  " +
                              "FROM	    AG_IAS_LICENSE_D	    LD, " +
                              "         AG_IAS_LICENSE_H	    LH, " +
                              "         AG_LICENSE_TYPE_R	    LT " +
                              "WHERE	LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	AND " +
                              "         LD.UPLOAD_GROUP_NO		=	LH.UPLOAD_GROUP_NO      AND LH.COMP_CODE = " + Compcode + "  " + crit;

                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseVerifyByRequest", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงรายการข้อมูลการขอใบอนุญาต
        /// </summary>
        /// <param name="groupUploadNo">เลขที่กลุ่ม</param>
        /// <param name="seqNo">ลำดับที่</param>
        /// <returns>ResponseService<LicenseVerifyDetail></returns>
        public DTO.ResponseService<DTO.LicenseVerifyDetail>
            GetLicenseVerifyDetail(string groupUploadNo, string seqNo)
        {
            var res = new DTO.ResponseService<DTO.LicenseVerifyDetail>();
            res.DataResponse = new DTO.LicenseVerifyDetail();
            try
            {
                #region SQL Statement

                string sql = "SELECT	LD.PRE_NAME_CODE, LD.NAMES, LD.LASTNAME, LD.TITLE_NAME , " +
                             "          LD.ID_CARD_NO, LD.LICENSE_DATE, " +
                             "          LD.LICENSE_EXPIRE_DATE, LD.LICENSE_NO, LD.EMAIL, " +
                             "          LD.RENEW_TIMES, LD.OLD_COMP_CODE,OLD_COMP_CODE || ' ' || COM.NAME as COMP_NAME, LD.AR_DATE, " +
                             "          LD.FEES, LD.ADDRESS_1 CURRENT_ADDRESS, " +
                             "          SUBSTR(LD.AREA_CODE,1,2) CURRENT_PROVINCE_CODE, " +
                             "          SUBSTR(LD.AREA_CODE,3,2) CURRENT_AMPUR_CODE, " +
                             "          SUBSTR(LD.AREA_CODE,5,4) CURRENT_TUMBON_CODE, " +
                             "          LD.CURRENT_ADDRESS_1 LOCAL_ADDRESS,  " +
                             "          SUBSTR(LD.CURRENT_AREA_CODE,1,2) LOCAL_PROVINCE_CODE,  " +
                             "          SUBSTR(LD.CURRENT_AREA_CODE,3,2) LOCAL_AMPUR_CODE, " +
                             "          SUBSTR(LD.CURRENT_AREA_CODE,5,4) LOCAL_TUMBON_CODE, " +
                             "          LD.UPLOAD_GROUP_NO, LD.SEQ_NO ,LD.APPROVED " +
                             "FROM	    AG_IAS_LICENSE_D	LD " +
                             "left join VW_IAS_Com_code com on ld.old_comp_code=COM.ID " +
                             "WHERE	    LD.UPLOAD_GROUP_NO	=	'" + groupUploadNo + "'	AND " +
                             "          LD.SEQ_NO			=	'" + seqNo + "'";

                #endregion

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    res.DataResponse = dr.MapToEntity<DTO.LicenseVerifyDetail>();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseVerifyDetail", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงรายการข้อมูลการขอใบอนุญาต
        /// </summary>
        /// <param name="groupUploadNo">เลขที่กลุ่ม</param>
        /// <returns>ResponseService<LicenseVerifyImgDetail></returns>
        public DTO.ResponseService<List<IAS.DTO.AttatchFileLicense>>
            GetLicenseVerifyImgDetail(string groupID, string idCard, string CountPage, Int32 pageIndex, Int32 pageSize)
        {
            var res = new DTO.ResponseService<List<IAS.DTO.AttatchFileLicense>>();
            var res2 = new DTO.ResponseService<List<IAS.DTO.AttatchFileLicense>>();

            try
            {
                if (CountPage == "Y")
                {
                    List<AG_IAS_ATTACH_FILE_LICENSE> att = base.ctx.AG_IAS_ATTACH_FILE_LICENSE.Where(W => W.GROUP_LICENSE_ID == groupID &&
                     W.ID_CARD_NO == idCard).ToList();
                    List<DTO.AttatchFileLicense> attl = new List<DTO.AttatchFileLicense>();
                    res.DataResponse = att.ConvertToAttachFileLicense();
                }
                else
                {
                    List<AG_IAS_ATTACH_FILE_LICENSE> att = base.ctx.AG_IAS_ATTACH_FILE_LICENSE.Where(W => W.GROUP_LICENSE_ID == groupID &&
                        W.ID_CARD_NO == idCard).ToList();
                    List<DTO.AttatchFileLicense> attl = new List<DTO.AttatchFileLicense>();
                    res.DataResponse = att.ConvertToAttachFileLicense().OrderBy(c => c.ID_ATTACH_FILE).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();


                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseVerifyImgDetail", ex);
            }
            return res;
        }


        private string GenNewLicenseNo()
        {
            return "";
        }

        /// <summary>
        /// อนุมัติการตรวจสอบรายการขอใบอนุญาต
        /// </summary>
        /// <param name="list">Collection ที่ Submit</param>
        /// <returns>ResponseService<string></returns>
        public DTO.ResponseService<string> ApproveLicenseVerify(List<DTO.SubmitLicenseVerify> list, string flgApprove, string ApproveID)
        {
            var res = new DTO.ResponseService<string>();
            AG_TRAIN_SPECIAL_T entTrainSpecial = new AG_TRAIN_SPECIAL_T();
            AG_IAS_EXAM_SPECIAL_T entExamSpecial = new AG_IAS_EXAM_SPECIAL_T();

            try
            {
                int ListCount = 0;
                foreach (DTO.SubmitLicenseVerify l in list)
                {
                    ListCount++;
                    var lic = base.ctx.AG_IAS_LICENSE_D
                                      .FirstOrDefault(s => s.UPLOAD_GROUP_NO == l.UploadGroupNo &&
                                                           s.SEQ_NO == l.SeqNo);

                    lic.APPROVED = flgApprove;
                    lic.APPROVED_DATE = DateTime.Now;
                    lic.APPROVED_BY = ApproveID;



                    if (flgApprove == "Y")
                    {

                        var head = base.ctx.AG_IAS_LICENSE_H.Where(w => w.UPLOAD_GROUP_NO == l.UploadGroupNo).FirstOrDefault();
                        decimal lot = head.LOTS.ToDecimal();
                        decimal ent = base.ctx.AG_IAS_LICENSE_D.Where(w => w.APPROVED == "Y" && w.UPLOAD_GROUP_NO == l.UploadGroupNo).Count();


                        // เช็คอนุมัติทั้งใบ
                        if (ent + ListCount == lot)
                        {
                            head.APPROVED_DOC = "Y";
                            head.APPROVED_DATE = DateTime.Now;
                            head.APPROVED_BY = ApproveID;

                            #region เช็คคุณสมบัติและเพิ่มข้อมูลคุณสมบัติ
                            var specialExam = base.ctx.AG_IAS_SPECIAL_T_TEMP
                                     .FirstOrDefault(s => s.ID_CARD_NO == lic.ID_CARD_NO &&
                                                          s.STATUS == "W" && s.EXAM_DISCOUNT_STATUS == "Y");
                            var specialTrain = base.ctx.AG_IAS_SPECIAL_T_TEMP
                                 .FirstOrDefault(s => s.ID_CARD_NO == lic.ID_CARD_NO &&
                                                          s.STATUS == "W" && s.TRAIN_DISCOUNT_STATUS == "Y");


                            if (specialExam != null)
                            {
                                entExamSpecial = new AG_IAS_EXAM_SPECIAL_T();

                                entExamSpecial.ID_CARD_NO = specialExam.ID_CARD_NO;
                                entExamSpecial.SPECIAL_TYPE_CODE = specialExam.SPECIAL_TYPE_CODE;
                                entExamSpecial.START_DATE = specialExam.START_DATE;
                                entExamSpecial.END_DATE = specialExam.END_DATE;
                                entExamSpecial.SEND_DATE = specialExam.SEND_DATE;
                                entExamSpecial.SEND_BY = specialExam.SEND_BY;
                                entExamSpecial.USER_ID = specialExam.USER_ID;
                                entExamSpecial.USER_DATE = specialExam.USER_DATE;
                                entExamSpecial.SEND_YEAR = specialExam.SEND_YEAR;
                                entExamSpecial.UNI_CODE = specialExam.UNI_CODE;
                                entExamSpecial.UNI_NAME = specialExam.UNI_NAME;

                                specialExam.STATUS = "Y";

                                base.ctx.AG_IAS_EXAM_SPECIAL_T.AddObject(entExamSpecial);
                            }

                            if (specialTrain != null)
                            {
                                entTrainSpecial = new AG_TRAIN_SPECIAL_T();

                                entTrainSpecial.ID_CARD_NO = specialTrain.ID_CARD_NO;
                                entTrainSpecial.SPECIAL_TYPE_CODE = specialTrain.SPECIAL_TYPE_CODE;
                                entTrainSpecial.START_DATE = specialTrain.START_DATE;
                                entTrainSpecial.END_DATE = specialTrain.END_DATE;
                                entTrainSpecial.SEND_DATE = specialTrain.SEND_DATE;
                                entTrainSpecial.SEND_BY = specialTrain.SEND_BY;
                                entTrainSpecial.USER_ID = specialTrain.USER_ID;
                                entTrainSpecial.USER_DATE = specialTrain.USER_DATE;
                                entTrainSpecial.SEND_YEAR = specialTrain.SEND_YEAR;
                                entTrainSpecial.UNI_CODE = specialTrain.UNI_CODE;
                                entTrainSpecial.UNI_NAME = specialTrain.UNI_NAME;

                                specialTrain.STATUS = "Y";

                                base.ctx.AG_TRAIN_SPECIAL_T.AddObject(entTrainSpecial);
                            }
                            #endregion

                            if (!string.IsNullOrEmpty(head.PETITION_TYPE_CODE) && head.PETITION_TYPE_CODE == "11")
                            {
                                InsertPersonalTByUploadGroupNO(l.UploadGroupNo);
                            }

                            if (!string.IsNullOrEmpty(head.UPLOAD_BY_SESSION))
                            {
                                //กรณีเข้าอนุญาตแบบเดี่ยวและมีใบสั่งจ่ายแล้ว
                                var single = base.ctx.AG_IAS_SUBPAYMENT_H_T.Where(w => w.UPLOAD_GROUP_NO == l.UploadGroupNo && w.UPLOAD_BY_SESSION == head.UPLOAD_BY_SESSION).FirstOrDefault();
                                if (single != null)
                                {
                                    single.APPROVED_DOC = "Y";
                                }
                            }

                        }
                        else
                        {
                            decimal detail = base.ctx.AG_IAS_LICENSE_D.Where(w => w.APPROVED == "N" && w.UPLOAD_GROUP_NO == l.UploadGroupNo).Count();
                            if (detail == 1)
                            {
                                head.APPROVED_DOC = "W";
                                head.APPROVED_DATE = DateTime.Now;
                                head.APPROVED_BY = ApproveID;


                            }

                            if (!string.IsNullOrEmpty(head.UPLOAD_BY_SESSION))
                            {
                                //กรณีเข้าอนุญาตแบบเดี่ยวและมีใบสั่งจ่ายแล้ว
                                var single = base.ctx.AG_IAS_SUBPAYMENT_H_T.Where(w => w.UPLOAD_GROUP_NO == l.UploadGroupNo && w.UPLOAD_BY_SESSION == head.UPLOAD_BY_SESSION).FirstOrDefault();
                                if (single != null)
                                {
                                    single.APPROVED_DOC = "N";
                                }
                            }
                        }
                        Int64 iUploadGroupNo = Convert.ToInt64(l.UploadGroupNo);

                        #region Old Code
                        //เพิ่มข้อมูลเข้า AG_PERSONAL_T
                        //if (!string.IsNullOrEmpty(head.PETITION_TYPE_CODE) && head.PETITION_TYPE_CODE == "11")
                        //{
                        //    var per = base.ctx.AG_PERSONAL_T
                        //                      .SingleOrDefault(s => s.ID_CARD_NO == lic.ID_CARD_NO);

                        //    if (per == null)
                        //    {
                        //        AG_PERSONAL_T p = new AG_PERSONAL_T
                        //        {
                        //            ID_CARD_NO = lic.ID_CARD_NO,
                        //            PRE_NAME_CODE = lic.PRE_NAME_CODE,
                        //            NAMES = lic.NAMES,
                        //            LASTNAME = lic.LASTNAME
                        //        };
                        //        base.ctx.AG_PERSONAL_T.AddObject(p);
                        //    }
                        //}
                        #endregion
                    }
                    else if (flgApprove == "N")
                    {
                        Int64 iUploadGroupNo = Convert.ToInt64(l.UploadGroupNo);
                        base.ctx.AG_IAS_IMPORT_DETAIL_TEMP.Where(w => w.IMPORT_ID == iUploadGroupNo).ToList().ForEach(d => d.LOAD_STATUS = null);

                        var ent = base.ctx.AG_IAS_LICENSE_H
                                      .FirstOrDefault(s => s.UPLOAD_GROUP_NO == l.UploadGroupNo);
                        ent.APPROVED_DOC = "N";
                        ent.APPROVED_DATE = DateTime.Now;
                        ent.APPROVED_BY = ApproveID;

                        #region เช็คคุณสมบัติและเพิ่มข้อมูลคุณสมบัติ
                        var specialExam = base.ctx.AG_IAS_SPECIAL_T_TEMP
                                 .FirstOrDefault(s => s.ID_CARD_NO == lic.ID_CARD_NO &&
                                                      s.STATUS == "W" && s.EXAM_DISCOUNT_STATUS == "Y");
                        var specialTrain = base.ctx.AG_IAS_SPECIAL_T_TEMP
                             .FirstOrDefault(s => s.ID_CARD_NO == lic.ID_CARD_NO &&
                                                      s.STATUS == "W" && s.TRAIN_DISCOUNT_STATUS == "Y");


                        if (specialExam != null)
                        {
                            specialExam.STATUS = "N";
                        }

                        if (specialTrain != null)
                        {
                            specialTrain.STATUS = "N";
                        }
                        #endregion
                    }
                }
                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                List<String> emails = new List<String>();
                //ส่งอีเมล
                if (!res.IsError)
                {
                    foreach (DTO.SubmitLicenseVerify ls in list)
                    {

                        emails = new List<String>();
                        var head = base.ctx.AG_IAS_LICENSE_H.Where(w => w.UPLOAD_GROUP_NO == ls.UploadGroupNo).FirstOrDefault();

                        string licenseName = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE.Trim() == head.LICENSE_TYPE_CODE.Trim()).FirstOrDefault().LICENSE_TYPE_NAME;

                        string pettitionName = base.ctx.AG_IAS_PETITION_TYPE_R.Where(w => w.PETITION_TYPE_CODE.Trim() == head.PETITION_TYPE_CODE.Trim()).FirstOrDefault().PETITION_TYPE_NAME;

                        var lsDetail = base.ctx.AG_IAS_LICENSE_D.Where(s => s.UPLOAD_GROUP_NO == ls.UploadGroupNo).ToList();

                        foreach (var item in lsDetail)
                        {
                            if (emails.Where(a => a == item.EMAIL) != null)
                            {
                                if (!String.IsNullOrEmpty(item.EMAIL))
                                    emails.Add(item.EMAIL);
                            }
                        }


                        if (emails.Count != 0)
                        {
                            MailApproveDocLicenseHelper.SendMailApproveDoc(emails, pettitionName, licenseName, flgApprove);
                        }

                    }

                }


                res.DataResponse = Resources.infoLicenseService_026;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดโปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_ApproveLicenseVerify", ex);
            }
            return res;
        }

        private bool InsertPersonalTByUploadGroupNO(string uploadGroupNo)
        {
            try
            {
                List<AG_IAS_LICENSE_D> attachs = base.ctx.AG_IAS_LICENSE_D.Where(b => b.APPROVED == "Y" && b.UPLOAD_GROUP_NO == uploadGroupNo).ToList();
                foreach (AG_IAS_LICENSE_D ls in attachs)
                {
                    var per = base.ctx.AG_PERSONAL_T
                                                 .FirstOrDefault(s => s.ID_CARD_NO == ls.ID_CARD_NO);

                    if (per == null)
                    {
                        AG_PERSONAL_T p = new AG_PERSONAL_T
                        {
                            ID_CARD_NO = ls.ID_CARD_NO,
                            PRE_NAME_CODE = ls.PRE_NAME_CODE,
                            NAMES = ls.NAMES,
                            LASTNAME = ls.LASTNAME
                        };
                        base.ctx.AG_PERSONAL_T.AddObject(p);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public void Dispose()
        {
            if (ctx != null) ctx.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Person License
        /// <summary>
        /// ดึงข้อมูลประวัติการสอบ แบบมีเงื่อนไข
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<List<DTO.ExamHistory></returns>
        public DTO.ResponseService<List<DTO.ExamHistory>> GetExamHistoryByIDWithCondition(string idCard, string licenseTypeCode)
        {
            var res = new DTO.ResponseService<List<DTO.ExamHistory>>();
            try
            {

                #region Func
                Func<string, string> exresult = delegate(string r)
                {
                    if ((r != null) && (r != ""))
                    {
                        if (r.Equals("P"))
                        {
                            r = Resources.propLicenseService_P;
                        }
                        else if (r.Equals("F"))
                        {
                            r = Resources.propLicenseService_F;
                        }
                        else if (r.Equals("M"))
                        {
                            r = Resources.propLicenseService_M;
                        }
                        else if (r.Equals("B"))
                        {
                            r = Resources.propLicenseService_B;
                        }
                    }
                    else
                    {
                        r = Resources.propLicenseService_N;
                    }

                    return r;
                };
                #endregion

                var result = (from AP in base.ctx.AG_APPLICANT_T
                              join LR in base.ctx.AG_EXAM_LICENSE_R on AP.TESTING_NO equals LR.TESTING_NO
                              join TR in base.ctx.AG_EXAM_TIME_R on LR.TEST_TIME_CODE equals TR.TEST_TIME_CODE
                              join PR in base.ctx.AG_EXAM_PLACE_R on AP.EXAM_PLACE_CODE equals PR.EXAM_PLACE_CODE
                              join LT in base.ctx.AG_IAS_LICENSE_TYPE_R on LR.LICENSE_TYPE_CODE equals LT.LICENSE_TYPE_CODE
                              where AP.TESTING_NO == LR.TESTING_NO &&
                              AP.EXAM_PLACE_CODE == LR.EXAM_PLACE_CODE &&
                              LR.TEST_TIME_CODE == TR.TEST_TIME_CODE &&
                              AP.EXAM_PLACE_CODE == PR.EXAM_PLACE_CODE &&
                              LR.LICENSE_TYPE_CODE == LT.LICENSE_TYPE_CODE &&
                              AP.ID_CARD_NO == idCard &&
                              LR.LICENSE_TYPE_CODE == licenseTypeCode
                              select new DTO.ExamHistory
                              {
                                  ID_CARD_NO = AP.ID_CARD_NO,
                                  APPLICANT_CODE = AP.APPLICANT_CODE,
                                  TESTING_NO = AP.TESTING_NO,
                                  TESTING_DATE = LR.TESTING_DATE,
                                  TEST_TIME = TR.TEST_TIME,
                                  LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE,
                                  LICENSE_TYPE_NAME = LT.LICENSE_TYPE_NAME,
                                  EXAM_PLACE_NAME = PR.EXAM_PLACE_NAME,
                                  EXAM_RESULT = AP.RESULT,
                                  EXPIRE_DATE = AP.EXPIRE_DATE,


                              }).OrderByDescending(app => app.TESTING_DATE).ToList();

                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        result[i].EXAM_RESULT = exresult(result[i].EXAM_RESULT);
                    }
                }

                res.DataResponse = result.OrderByDescending(tdate => tdate.TESTING_NO).ToList();
            }
            catch (Exception ex)
            {
                //error
                LoggerFactory.CreateLog().Fatal("LicenseService_GetExamHistoryByIDWithCondition" + ":" + ex.Message, ex.Message);
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        /// <summary>
        /// ดึงข้อมูลประวัติการสอบ แบบไม่มีเงื่อนไข
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<List<DTO.ExamHistory></returns>
        /// <AUTHOOR>Natta</AUTHOOR>
        /// <LASTUPDATE>29/05/2557</LASTUPDATE>
        public DTO.ResponseService<List<DTO.ExamHistory>> GetExamHistoryByID(string idCard)
        {
            var res = new DTO.ResponseService<List<DTO.ExamHistory>>();
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            try
            {

                #region Func
                Func<string, string> exresult = delegate(string r)
                {
                    if ((r != null) && (r != ""))
                    {
                        if (r.Equals("P"))
                        {
                            r = Resources.propLicenseService_P;
                        }
                        else if (r.Equals("F"))
                        {
                            r = Resources.propLicenseService_F;
                        }
                        else if (r.Equals("M"))
                        {
                            r = Resources.propLicenseService_M;
                        }
                        else if (r.Equals("B"))
                        {
                            r = Resources.propLicenseService_B;
                        }
                    }
                    else
                    {
                        r = Resources.propLicenseService_N;
                    }

                    return r;
                };
                #endregion

                IQueryable<DTO.ExamHistory> result = (from AP in base.ctx.AG_APPLICANT_T
                                                      join LR in base.ctx.AG_EXAM_LICENSE_R on AP.TESTING_NO equals LR.TESTING_NO
                                                      join TR in base.ctx.AG_EXAM_TIME_R on LR.TEST_TIME_CODE equals TR.TEST_TIME_CODE
                                                      join PR in base.ctx.AG_EXAM_PLACE_R on AP.EXAM_PLACE_CODE equals PR.EXAM_PLACE_CODE
                                                      join LT in base.ctx.AG_IAS_LICENSE_TYPE_R on LR.LICENSE_TYPE_CODE equals LT.LICENSE_TYPE_CODE
                                                      where AP.TESTING_NO == LR.TESTING_NO &&
                                                      AP.EXAM_PLACE_CODE == LR.EXAM_PLACE_CODE &&
                                                      LR.TEST_TIME_CODE == TR.TEST_TIME_CODE &&
                                                      AP.EXAM_PLACE_CODE == PR.EXAM_PLACE_CODE &&
                                                      LR.LICENSE_TYPE_CODE == LT.LICENSE_TYPE_CODE &&
                                                      AP.ID_CARD_NO == idCard
                                                      select new DTO.ExamHistory
                                                      {
                                                          ID_CARD_NO = AP.ID_CARD_NO,
                                                          APPLICANT_CODE = AP.APPLICANT_CODE,
                                                          TESTING_NO = AP.TESTING_NO,
                                                          TESTING_DATE = LR.TESTING_DATE,
                                                          TEST_TIME = TR.TEST_TIME,
                                                          LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE,
                                                          LICENSE_TYPE_NAME = LT.LICENSE_TYPE_NAME,
                                                          EXAM_PLACE_NAME = PR.EXAM_PLACE_NAME,
                                                          EXAM_RESULT = AP.RESULT,
                                                          EXPIRE_DATE = AP.EXPIRE_DATE,


                                                      });

                result.ToList().ForEach(s => s.EXAM_RESULT = exresult(s.EXAM_RESULT));

                res.DataResponse = result.OrderByDescending(tdate => tdate.TESTING_NO).ToList();
            }
            catch (Exception ex)
            {
                //error
                LoggerFactory.CreateLog().Fatal("LicenseService_GetExamHistoryByID", ex.Message);
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }

            //sw.Stop();
            //TimeSpan sp = sw.Elapsed;
            //TimeSpan duration = sp.Duration();

            return res;

        }

        /// <summary>
        /// ดึงข้อมูลประวัติการอบรมด้วยรหัสบัตรประชาชน แบบมีเงื่อนไข
        /// </summary> Renew 30/10/2556
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>DTO.ResponseService<List<DTO.TrainPersonHistory>></returns>
        public DTO.ResponseService<List<DTO.TrainPersonHistory>> GetTrainingHistoryByIDWithCondition(string idCard, string licenseTypeCode)
        {
            var res = new DTO.ResponseService<List<DTO.TrainPersonHistory>>();
            try
            {
                #region Func
                Func<string, string> trainresult = delegate(string status)
                {
                    if ((status != null) && (status != ""))
                    {
                        if (status.Equals("P"))
                        {
                            status = Resources.propLicenseService_P;
                        }
                        else
                        {
                            status = Resources.propLicenseService_F;
                        }
                    }
                    if ((status == null) || (status == ""))
                    {
                        status = Resources.propLicenseService_026;
                    }

                    return status;
                };

                Func<string, string> licenseTypeConvert = delegate(string input)
                {
                    if ((input != null) && (input != ""))
                    {

                        string ent = base.ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(w => w.LICENSE_TYPE_CODE == input).LICENSE_TYPE_NAME;
                        if (ent != null)
                        {
                            input = ent;
                        }
                    }

                    return input;
                };

                Func<string, string> traintype = delegate(string type)
                {
                    if ((type != null) && (type != ""))
                    {
                        if (type.Equals("T"))
                        {
                            type = Resources.propLicenseService_027;
                        }
                        else
                        {
                            type = Resources.propLicenseService_028;
                        }
                    }
                    if ((type == null) || (type == ""))
                    {
                        type = Resources.propLicenseService_029;
                    }

                    return type;
                };
                #endregion

                DateTime currentDate = DateTime.Now;

                var result = (from TRAIN in base.ctx.AG_TRAIN_PERSON_T
                              from TRAINPLAN in this.ctx.AG_TRAIN_PLAN_T
                              where TRAIN.TRAIN_CODE == TRAINPLAN.TRAIN_CODE &&
                              TRAIN.ID_CARD_NO == idCard &&
                              TRAIN.LICENSE_TYPE_CODE == licenseTypeCode
                              select new DTO.TrainPersonHistory
                              {
                                  TRAIN_CODE = TRAIN.TRAIN_CODE,
                                  ID_CARD_NO = TRAIN.ID_CARD_NO,
                                  LICENSE_TYPE_CODE = TRAIN.LICENSE_TYPE_CODE,
                                  LICENSE_TYPE_NAME = TRAINPLAN.LICENSE_TYPE_CODE,
                                  TRAIN_TIMES = TRAIN.TRAIN_TIMES,
                                  TRAIN_DATE = TRAIN.TRAIN_DATE,
                                  TRAIN_DATE_EXP = TRAIN.TRAIN_EXP_DATE,
                                  STATUS = TRAIN.RESULT,
                                  PILAR_1 = TRAIN.PILAR_1,
                                  PILAR_2 = TRAIN.PILAR_2,
                                  PILAR_3 = TRAIN.PILAR_3,
                                  TRAIN_TYPE = TRAIN.TRAIN_TYPE,
                                  HOURS = TRAIN.TRAIN_PERIOD,

                              }).OrderByDescending(d => d.TRAIN_DATE_EXP).ToList();


                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        //string dateFilter = Convert.ToString(Convert.ToDateTime(result[i].TRAIN_DATE_EXP).AddYears(-543));
                        result[i].LICENSE_TYPE_NAME = licenseTypeConvert(result[i].LICENSE_TYPE_CODE);
                        result[i].STATUS = trainresult(result[i].STATUS);
                        result[i].TRAIN_TYPE = traintype(result[i].TRAIN_TYPE);
                        result[i].RESULT = result[i].STATUS;
                        result[i].HOURS = result[i].HOURS;
                        //result[i].HOURS = Convert.ToInt16(result[i].PILAR_1 + result[i].PILAR_2 + result[i].PILAR_3);
                    }
                }

                res.DataResponse = result.OrderByDescending(exp => exp.TRAIN_DATE_EXP).ToList();
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_GetTrainingHistoryByIDWithCondition" + ":" + ex.Message, ex.Message);
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลประวัติการอบรมด้วยรหัสบัตรประชาชน แบบมีเงื่อนไข
        /// Renew 26/05/57
        /// </summary>
        /// <Auther>Natta</Auther>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>DTO.ResponseService<DTO.TrainPersonHistory></returns>
        public DTO.ResponseService<DTO.TrainPersonHistory> GetPersonalTrainByCriteria(string licenseTypeCode, string pettitionTypeCode, string renewTime, string idCard, string licenseNo, string specialTrainCode)
        {
            var res = new ResponseService<DTO.TrainPersonHistory>();
            List<string> pilarls = new List<string>();
            DTO.TrainPersonHistory e = new DTO.TrainPersonHistory();
            string TP = "ผ่าน";
            string TF = "ไม่ผ่าน";
            string CIATrainCode = "10006";
            string trainDateBefore = "01/01/2556";
            Int16? PERIOD_HH_TYPE_T = 0;
            Int16? PERIOD_HH_TYPE_S = 0;
            Int16? HOUR_DISCOUNT = 0;
            string curYear = string.Empty;

            try
            {
                #region Func
                Func<string, string, string> dateCompare = delegate(string input, string t)
                {
                    DateTime currDate = DateTime.Now;
                    DateTime expDate = Convert.ToDateTime(input);

                    string currDateFormat = String.Format("{0:dd/MM/yyy}", currDate).ToString();
                    string exphDateFormat = String.Format("{0:dd/MM/yyy}", expDate).ToString();
                    DateTime currTime = DateTime.Parse(currDateFormat);
                    DateTime expTime = DateTime.Parse(exphDateFormat);

                    int resCompare = DateTime.Compare(expTime, currTime);
                    if (t.Equals("train"))
                    {
                        //expTime < CurrentTime
                        if (resCompare == -1)
                        {
                            input = TF;
                        }
                        //expTime == CurrentTime
                        if (resCompare == 0)
                        {
                            input = TF;
                        }
                        //expTime > CurrentTime
                        if (resCompare == 1)
                        {
                            input = TP;
                        }
                    }
                    return input;
                };

                Func<string, string> datePilarCompare = delegate(string input)
                {
                    DateTime expDate = Convert.ToDateTime(input);

                    string currDateFormat = "01/01/2556";
                    string exphDateFormat = String.Format("{0:dd/MM/yyy}", expDate).ToString();
                    DateTime currTime = DateTime.Parse(currDateFormat);
                    DateTime expTime = DateTime.Parse(exphDateFormat);

                    int resCompare = DateTime.Compare(expTime, currTime);
                    //expTime < CurrentTime
                    if (resCompare == -1)
                    {
                        input = TP;
                    }
                    //expTime == CurrentTime
                    if (resCompare == 0)
                    {
                        input = TF;
                    }
                    //expTime > CurrentTime
                    if (resCompare == 1)
                    {
                        input = TF;
                    }
                    return input;
                };

                Func<string, string> dateUnitLinkCompare = delegate(string input)
                {
                    DateTime currDate = DateTime.Now;
                    DateTime expDate = Convert.ToDateTime(input).AddYears(5);

                    string currDateFormat = String.Format("{0:dd/MM/yyy}", currDate).ToString();
                    string exphDateFormat = String.Format("{0:dd/MM/yyy}", expDate).ToString();
                    DateTime currTime = DateTime.Parse(currDateFormat);
                    DateTime expTime = DateTime.Parse(exphDateFormat);

                    int resCompare = DateTime.Compare(expTime, currTime);
                    //expTime < CurrentTime
                    if (resCompare == -1)
                    {
                        input = TF;
                    }
                    //expTime == CurrentTime
                    if (resCompare == 0)
                    {
                        input = TF;
                    }
                    //expTime > CurrentTime
                    if (resCompare == 1)
                    {
                        input = TP;
                    }
                    return input;
                };

                Func<string> GetCurrentYear = delegate()
                {
                    string year = string.Empty;
                    if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                    {
                        year = Convert.ToString(DateTime.Now.AddYears(543).Year);
                    }
                    else
                    {
                        year = Convert.ToString(DateTime.Now.Year);
                    }

                    return year;
                };
                #endregion

                curYear = GetCurrentYear();
                Int32 renewT = Convert.ToInt32(renewTime);
                //การขอต่ออายุใบอนุญาต ครั้งที่ 1,2,3

                if (renewT.Equals(0) || renewT.Equals(1) || renewT.Equals(2) || renewT.Equals(3))
                {

                    //ดึงข้อมูลใบอนุญาติ เพื่อตรวจสอบใบอนุญาติที่ยังมีผลอยู่
                    DTO.ResponseService<List<DTO.PersonLicenseTransaction>> resLicense = this.GetAllLicenseByIDCard(idCard, "1", 1);

                    //ใบอนุุญาตประเภทเดียวกัน .Count() > 1
                    IEnumerable<DTO.PersonLicenseTransaction> currentLic = resLicense.DataResponse.Where(lic => lic.LICENSE_NO != licenseNo && lic.LICENSE_TYPE_CODE == licenseTypeCode);
                    if (currentLic.Count() > 0)
                    {
                        //ตรวจสอบผลอบรม โดยไม่สนใจวันหมดอายุ
                        //LicenseType = "01" ใช้ผลอบรม "01"
                        //LicenseType = "07" สามารถใช้ผลอบรม "07", "01"
                        //LicenseType = "02" ใช้ผลอบรม "02"
                        //LicenseType = "05" สามารถใช้ผลอบรม "05", "02"
                        //LicenseType = "06" สามารถใช้ผลอบรม "06", "02"
                        //LicenseType = "08" สามารถใช้ผลอบรม "08", "02"
                        if (licenseTypeCode.Equals(String.Format("{0:00}", DTO.LicenseType.Type01.GetEnumValue())) || licenseTypeCode.Equals(String.Format("{0:00}", DTO.LicenseType.Type07.GetEnumValue())))
                        {
                            #region LicenseType Layer = 01
                            IQueryable<DTO.TrainPersonHistory> resTrain = (from T in base.ctx.AG_TRAIN_PERSON_T
                                                                           where T.TRAIN_TIMES == renewT &&
                                                                           T.LICENSE_TYPE_CODE.Equals(licenseTypeCode) &&
                                                                           T.RESULT.Equals("P") &&
                                                                           T.ID_CARD_NO.Equals(idCard)
                                                                           select new DTO.TrainPersonHistory
                                                                           {
                                                                               TRAIN_CODE = T.TRAIN_CODE,
                                                                               TRAIN_TIMES = T.TRAIN_TIMES,
                                                                               RESULT = T.RESULT,
                                                                               TRAIN_DATE = T.TRAIN_DATE,
                                                                               TRAIN_DATE_EXP = T.TRAIN_EXP_DATE,
                                                                               LICENSE_TYPE_CODE = T.LICENSE_TYPE_CODE,
                                                                               HOURS = T.TRAIN_PERIOD,
                                                                               PILAR_1 = T.PILAR_1,
                                                                               PILAR_2 = T.PILAR_2,
                                                                               PILAR_3 = T.PILAR_3,
                                                                               LICENSE_NO = T.LICENSE_NO,
                                                                               ID_CARD_NO = T.ID_CARD_NO,
                                                                               TRAIN_TYPE = T.TRAIN_TYPE

                                                                           }).Union(from T in base.ctx.AG_TRAIN_PERSON_T
                                                                                    where T.TRAIN_TIMES == renewT &&
                                                                                    T.LICENSE_TYPE_CODE.Equals("01") &&
                                                                                    T.RESULT.Equals("P") &&
                                                                                    T.ID_CARD_NO.Equals(idCard)
                                                                                    select new DTO.TrainPersonHistory
                                                                                    {
                                                                                        TRAIN_CODE = T.TRAIN_CODE,
                                                                                        TRAIN_TIMES = T.TRAIN_TIMES,
                                                                                        RESULT = T.RESULT,
                                                                                        TRAIN_DATE = T.TRAIN_DATE,
                                                                                        TRAIN_DATE_EXP = T.TRAIN_EXP_DATE,
                                                                                        LICENSE_TYPE_CODE = T.LICENSE_TYPE_CODE,
                                                                                        HOURS = T.TRAIN_PERIOD,
                                                                                        PILAR_1 = T.PILAR_1,
                                                                                        PILAR_2 = T.PILAR_2,
                                                                                        PILAR_3 = T.PILAR_3,
                                                                                        LICENSE_NO = T.LICENSE_NO,
                                                                                        ID_CARD_NO = T.ID_CARD_NO,
                                                                                        TRAIN_TYPE = T.TRAIN_TYPE

                                                                                    });

                            if (resTrain.Count() > 0)
                            {
                                e.RESULT = TP;
                                switch (e.RESULT)
                                {
                                    case "ผ่าน":
                                        res.DataResponse = e;
                                        return res;
                                }
                            }
                            else
                            {
                                e.RESULT = TF;
                                switch (e.RESULT)
                                {
                                    case "ไม่ผ่าน":
                                        res.DataResponse = e;
                                        return res;
                                }
                            }
                            #endregion

                        }
                        else if (licenseTypeCode.Equals(String.Format("{0:00}", DTO.LicenseType.Type02.GetEnumValue()))
                            || licenseTypeCode.Equals(String.Format("{0:00}", DTO.LicenseType.Type05.GetEnumValue()))
                            || licenseTypeCode.Equals(String.Format("{0:00}", DTO.LicenseType.Type06.GetEnumValue()))
                            || licenseTypeCode.Equals(String.Format("{0:00}", DTO.LicenseType.Type08.GetEnumValue())))
                        {
                            #region LicenseType Layer = 02
                            IQueryable<DTO.TrainPersonHistory> resTrain = (from T in base.ctx.AG_TRAIN_PERSON_T
                                                                           join P in base.ctx.AG_TRAIN_PLAN_T on T.TRAIN_CODE equals P.TRAIN_CODE
                                                                           where T.TRAIN_TIMES == renewT &&
                                                                           T.LICENSE_TYPE_CODE.Equals(licenseTypeCode) &&
                                                                           T.RESULT.Equals("P") &&
                                                                           T.ID_CARD_NO.Equals(idCard)
                                                                           select new DTO.TrainPersonHistory
                                                                           {
                                                                               TRAIN_CODE = T.TRAIN_CODE,
                                                                               TRAIN_TIMES = T.TRAIN_TIMES,
                                                                               RESULT = T.RESULT,
                                                                               TRAIN_DATE = T.TRAIN_DATE,
                                                                               TRAIN_DATE_EXP = T.TRAIN_EXP_DATE,
                                                                               LICENSE_TYPE_CODE = T.LICENSE_TYPE_CODE,
                                                                               HOURS = T.TRAIN_PERIOD,
                                                                               PILAR_1 = T.PILAR_1,
                                                                               PILAR_2 = T.PILAR_2,
                                                                               PILAR_3 = T.PILAR_3,
                                                                               LICENSE_NO = T.LICENSE_NO,
                                                                               ID_CARD_NO = T.ID_CARD_NO,
                                                                               TRAIN_TYPE = T.TRAIN_TYPE

                                                                           }).Union(from T in base.ctx.AG_TRAIN_PERSON_T
                                                                                    join P in base.ctx.AG_TRAIN_PLAN_T on T.TRAIN_CODE equals P.TRAIN_CODE
                                                                                    where T.TRAIN_TIMES == renewT &&
                                                                                    T.LICENSE_TYPE_CODE.Equals("02") &&
                                                                                    T.RESULT.Equals("P") &&
                                                                                    T.ID_CARD_NO.Equals(idCard)
                                                                                    select new DTO.TrainPersonHistory
                                                                                    {
                                                                                        TRAIN_CODE = T.TRAIN_CODE,
                                                                                        TRAIN_TIMES = T.TRAIN_TIMES,
                                                                                        RESULT = T.RESULT,
                                                                                        TRAIN_DATE = T.TRAIN_DATE,
                                                                                        TRAIN_DATE_EXP = T.TRAIN_EXP_DATE,
                                                                                        LICENSE_TYPE_CODE = T.LICENSE_TYPE_CODE,
                                                                                        HOURS = T.TRAIN_PERIOD,
                                                                                        PILAR_1 = T.PILAR_1,
                                                                                        PILAR_2 = T.PILAR_2,
                                                                                        PILAR_3 = T.PILAR_3,
                                                                                        LICENSE_NO = T.LICENSE_NO,
                                                                                        ID_CARD_NO = T.ID_CARD_NO,
                                                                                        TRAIN_TYPE = T.TRAIN_TYPE

                                                                                    });

                            if (resTrain.Count() > 0)
                            {
                                e.RESULT = TP;
                                switch (e.RESULT)
                                {
                                    case "ผ่าน":
                                        res.DataResponse = e;
                                        return res;
                                }
                            }
                            else
                            {
                                e.RESULT = TF;
                                switch (e.RESULT)
                                {
                                    case "ไม่ผ่าน":
                                        res.DataResponse = e;
                                        return res;
                                }
                            }
                            #endregion

                        }
                    }
                    else
                    {
                        //ตรวจสอบผลอบรม ตามปกติ
                        var resTrain = (from T in base.ctx.AG_TRAIN_PERSON_T
                                        where T.TRAIN_TIMES == renewT &&
                                        T.LICENSE_TYPE_CODE.Equals(licenseTypeCode) &&
                                        T.RESULT.Equals("P") &&
                                        T.ID_CARD_NO.Equals(idCard)
                                        select new DTO.TrainPersonHistory
                                        {
                                            TRAIN_CODE = T.TRAIN_CODE,
                                            TRAIN_TIMES = T.TRAIN_TIMES,
                                            RESULT = T.RESULT,
                                            TRAIN_DATE = T.TRAIN_DATE,
                                            TRAIN_DATE_EXP = T.TRAIN_EXP_DATE,
                                            LICENSE_TYPE_CODE = T.LICENSE_TYPE_CODE,
                                            HOURS = T.TRAIN_PERIOD,
                                            PILAR_1 = T.PILAR_1,
                                            PILAR_2 = T.PILAR_2,
                                            PILAR_3 = T.PILAR_3,
                                            LICENSE_NO = T.LICENSE_NO,
                                            ID_CARD_NO = T.ID_CARD_NO,
                                            TRAIN_TYPE = T.TRAIN_TYPE

                                        }).OrderBy(t => t.TRAIN_DATE_EXP).ToList();

                        //มีผลอบรม
                        if (resTrain.Count > 0)
                        {
                            //ตรวจสอบวันหมดอายุผลอบรม
                            var dtTranEXPCompare = resTrain.Where(x => DateTime.Compare(DateTime.Parse(String.Format("{0:dd/MM/yyy}", x.TRAIN_DATE_EXP).ToString()),
                            DateTime.Parse(String.Format("{0:dd/MM/yyy}", DateTime.Now).ToString())) != -1).ToList();
                            if (dtTranEXPCompare.Count == 0)
                            {
                                e.RESULT = TF;
                                switch (e.RESULT)
                                {
                                    case "ไม่ผ่าน":
                                        res.DataResponse = e;
                                        return res;
                                }
                            }
                            else
                            {
                                resTrain = dtTranEXPCompare;
                                //ตรวจสอบ RESULT
                                var chkResult = resTrain.Where(s => s.RESULT == "P");
                                if (chkResult.Count() > 0)
                                {
                                    e.RESULT = TP;
                                    switch (e.RESULT)
                                    {
                                        case "ผ่าน":
                                            res.DataResponse = e;
                                            return res;
                                    }
                                }
                                else
                                {
                                    e.RESULT = TF;
                                    switch (e.RESULT)
                                    {
                                        case "ไม่ผ่าน":
                                            res.DataResponse = e;
                                            return res;
                                    }
                                }
                            }

                        }
                        //ไม่มีผลอบรม
                        else
                        {
                            e.RESULT = TF;
                            switch (e.RESULT)
                            {
                                case "ไม่ผ่าน":
                                    res.DataResponse = e;
                                    return res;
                            }
                        }

                    }

                }
                //การขอต่ออายุใบอนุญาต ครั้งที่ 4 เป็นต้นไป				
                if (renewT >= 4)
                {
                    //กำหนดค่า renewT  ให้ใช้ตั้งแต่ครั้งที่ 4 เป็นต้นไป
                    if (renewT > 4)
                    {
                        renewT = 4;
                    }



                    var resTrain = (from T in base.ctx.AG_TRAIN_PERSON_T
                                    where T.TRAIN_TIMES == renewT &&
                                    T.LICENSE_TYPE_CODE.Equals(licenseTypeCode) &&
                                    T.RESULT.Equals("P") &&
                                    T.ID_CARD_NO.Equals(idCard)
                                    select new DTO.TrainPersonHistory
                                    {
                                        TRAIN_CODE = T.TRAIN_CODE,
                                        TRAIN_TIMES = T.TRAIN_TIMES,
                                        RESULT = T.RESULT,
                                        TRAIN_DATE = T.TRAIN_DATE,
                                        TRAIN_DATE_EXP = T.TRAIN_EXP_DATE,
                                        LICENSE_TYPE_CODE = T.LICENSE_TYPE_CODE,
                                        TRAIN_PERIOD = T.TRAIN_PERIOD,
                                        PILAR_1 = T.PILAR_1,
                                        PILAR_2 = T.PILAR_2,
                                        PILAR_3 = T.PILAR_3,
                                        LICENSE_NO = T.LICENSE_NO,
                                        ID_CARD_NO = T.ID_CARD_NO,
                                        TRAIN_TYPE = T.TRAIN_TYPE

                                    }).OrderBy(t => t.TRAIN_DATE_EXP).ToList();

                    if (resTrain.Count > 0)
                    {
                        //STEP 1 : ตรวจสอบ TRAIN_DATE_EXP
                        var dtTranEXPCompare = resTrain.Where(x => DateTime.Compare(DateTime.Parse(String.Format("{0:dd/MM/yyy}", x.TRAIN_DATE_EXP).ToString()),
                            DateTime.Parse(String.Format("{0:dd/MM/yyy}", DateTime.Now).ToString())) != -1).ToList();

                        if (dtTranEXPCompare.Count == 0)
                        {
                            e.RESULT = TF;
                            switch (e.RESULT)
                            {
                                case "ไม่ผ่าน":
                                    res.DataResponse = e;
                                    return res;
                            }
                        }
                        else
                        {
                            resTrain = dtTranEXPCompare;

                        }


                        //STEP 2 :Sum All TRAIN_PERIOD & Get from AG_TRAIN_PERIOD_R.Period_HH & TRAIN_TYPE="T"
                        var resTrainExpT = (from t in base.ctx.AG_TRAIN_PERIOD_R
                                            where t.YEAR_Y == curYear &&
                                            t.LICENSE_TYPE_CODE == licenseTypeCode &&
                                            t.TRAIN_TYPE == "T"
                                            select t).FirstOrDefault();
                        if (resTrainExpT != null)
                        {
                            PERIOD_HH_TYPE_T = resTrainExpT.PERIOD_HH;
                        }

                        //SUM ALL TRAIN_PERIOD WHERE TRAIN_TYPE='T' ตรวจสอบ ผลรวมชั่วโมงการอบรม ของ TRAIN_TYPE='T'
                        //Int32? SumHrs = resTrain.Sum(s => s.TRAIN_PERIOD);
                        Int32? SumHrsT = resTrain.Where(s => s.TRAIN_TYPE.Equals("T")).ToList().Sum(t => t.TRAIN_PERIOD);
                        if (SumHrsT >= PERIOD_HH_TYPE_T)
                        {
                            //ตรวจสอบต้องมีชั่วโมงครบตาม PILAR บุคคลที่อบรมก่อน วันที่ 1/มค/2556 ไม่ต้องตรวจสอบ เงื่อนไข PILAR
                            //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                            string pilarChk = this.PilarValidation(resTrain, "T", curYear);
                            e.RESULT = pilarChk;
                            res.DataResponse = e;
                            return res;
                        }
                        //ผลอบรมน้อยกว่า AG_TRAIN_PERIOD_R.Period_HH
                        else
                        {
                            //STEP 2 :Sum All TRAIN_PERIOD & Get from AG_TRAIN_PERIOD_R.Period_HH & TRAIN_TYPE="T"
                            var resTrainExpS = (from t in base.ctx.AG_TRAIN_PERIOD_R
                                                where t.YEAR_Y == curYear &&
                                                t.LICENSE_TYPE_CODE == licenseTypeCode &&
                                                t.TRAIN_TYPE == "S"
                                                select t).FirstOrDefault();
                            if (resTrainExpS != null)
                            {
                                PERIOD_HH_TYPE_S = resTrainExpS.PERIOD_HH;
                            }


                            //SUM ALL TRAIN_PERIOD WHERE TRAIN_TYPE='S' ตรวจสอบ ผลรวมชั่วโมงการอบรม ของ TRAIN_TYPE='S'
                            //Int32? SumHrs = resTrain.Sum(s => s.TRAIN_PERIOD);
                            Int32? SumHrsS = resTrain.Where(s => s.TRAIN_TYPE.Equals("S")).ToList().Sum(t => t.TRAIN_PERIOD);

                            //Type S + T >= PERIOD_HH_TYPE_T
                            //if ((SumHrsS + SumHrsT) >= PERIOD_HH_TYPE_S)
                            if ((SumHrsS + SumHrsT) >= PERIOD_HH_TYPE_T)
                            {
                                string pilarChk = this.PilarValidation(resTrain, "T", curYear);
                                e.RESULT = pilarChk;
                                res.DataResponse = e;
                                return res;
                            }
                            else
                            {
                                //ตรวจสอบ UNIT LINK
                                AG_U_TRAIN_T resUnitLink = base.ctx.AG_U_TRAIN_T.OrderBy(dt => dt.TRAIN_DATE).FirstOrDefault(id => (id.ID_CARD_NO.Equals(idCard) && id.F_FLAG != "Y") || (id.ID_CARD_NO.Equals(idCard) && id.F_FLAG == null));
                                if (resUnitLink != null)
                                {
                                    //TRAIN_DATE + 5 ปี > dateUnitLinkCompare();
                                    string unitLinkResult = dateUnitLinkCompare(String.Format("{0:dd/MM/yyyy}", resUnitLink.TRAIN_DATE));

                                    switch (unitLinkResult)
                                    {
                                        case "ไม่ผ่าน":
                                            e.RESULT = TF;
                                            res.DataResponse = e;
                                            return res;
                                    }

                                    //ชั่วโมงอบรม TRAIN_TYPE="S" + UNIT LINK(6ชั่วโมง)
                                    if ((SumHrsS + 6) >= PERIOD_HH_TYPE_S)
                                    {
                                        //UPDATE > resUnitLink >  AG_U_TRAIN_T.F_FLAG="Y"
                                        resUnitLink.F_FLAG = "Y";

                                        //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                                        string pilarChk = this.PilarValidation(resTrain, "S", curYear);
                                        e.RESULT = pilarChk;
                                        res.DataResponse = e;
                                        return res;
                                    }
                                    else
                                    {
                                        #region GetTrainspecialdiscount

                                        //Get AG_TRAIN_SPECIAL_DISCOUNT_T.HOUR_DISCOUNT
                                        string convertYear = Convert.ToString(curYear);
                                        AG_TRAIN_SPECIAL_DISCOUNT_T hrdiscount = base.ctx.AG_TRAIN_SPECIAL_DISCOUNT_T.FirstOrDefault(x => x.LICENSE_TYPE_CODE.Equals(licenseTypeCode) && x.P_YEAR == convertYear);
                                        if (hrdiscount != null)
                                        {
                                            HOUR_DISCOUNT = Convert.ToInt16(hrdiscount.HOUR_DISCOUNT);
                                        }


                                        #endregion


                                        #region chkTrainspecialStartwith 3

                                        //ตรวจสอบ การลดชั่วโมงผลอบรม วิทยากร ขึ้นต้นด้วย 3
                                        AG_TRAIN_SPECIAL_T trainspecial = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                            && dtx.SPECIAL_TYPE_CODE.StartsWith("3"));



                                        bool atspecialCheck = false;

                                        if (specialTrainCode.StartsWith("3"))
                                        {
                                            atspecialCheck = true;
                                        }


                                        if (trainspecial != null || atspecialCheck == true)
                                        {
                                            //TRAIN_DATE + 5 ปี > dateUnitLinkCompare();

                                            string trainspecialDT = string.Empty;

                                            if (trainspecial != null)
                                            {
                                                trainspecialDT = dateUnitLinkCompare(String.Format("{0:dd/MM/yyyy}", trainspecial.START_DATE));
                                            }
                                            else
                                            {
                                                trainspecialDT = dateUnitLinkCompare(String.Format("{0:dd/MM/yyyy}", DateTime.Now));
                                            }


                                            if (trainspecialDT.Equals(TP))
                                            {
                                                if ((SumHrsS + 6) >= (PERIOD_HH_TYPE_S - HOUR_DISCOUNT))
                                                {
                                                    //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                                                    string pilarChk = this.PilarValidation(resTrain, "S", curYear);
                                                    e.RESULT = pilarChk;
                                                    res.DataResponse = e;
                                                    return res;
                                                }
                                                else
                                                {
                                                    e.RESULT = TF;
                                                    switch (e.RESULT)
                                                    {
                                                        case "ไม่ผ่าน":
                                                            res.DataResponse = e;
                                                            return res;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                #region chkspecialCIA and chkspecialnotCIA


                                                //ตรวจสอบ การลดชั่วโมงผลอบรม วิทยากร ไม่ขึ้นต้นด้วย 3 และไม่ใช่ CIA = 10006
                                                AG_TRAIN_SPECIAL_T trainciachk = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                                    && !dtx.SPECIAL_TYPE_CODE.StartsWith("3") && dtx.SPECIAL_TYPE_CODE != CIATrainCode);

                                                bool atnotCiaCheck = false;
                                                if (!specialTrainCode.StartsWith("3") && specialTrainCode != CIATrainCode.Trim() && !string.IsNullOrEmpty(specialTrainCode))
                                                {
                                                    atnotCiaCheck = true;
                                                }

                                                if (trainciachk != null || atnotCiaCheck == true)
                                                {
                                                    if ((SumHrsS + 6) >= (PERIOD_HH_TYPE_S - HOUR_DISCOUNT))
                                                    {
                                                        //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                                                        string pilarChk = this.PilarValidation(resTrain, "S", curYear);
                                                        e.RESULT = pilarChk;
                                                        res.DataResponse = e;
                                                        return res;
                                                    }
                                                    else
                                                    {
                                                        e.RESULT = TF;
                                                        switch (e.RESULT)
                                                        {
                                                            case "ไม่ผ่าน":
                                                                res.DataResponse = e;
                                                                return res;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //การลดชั่วโมงผลอบรม  CIA (ลดชั่วโมง 100%) ต่อประเภทธุรกิจ สามารถใช้ได้ 1 ครั้ง

                                                    AG_TRAIN_SPECIAL_T chkCIA = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                                    && dtx.SPECIAL_TYPE_CODE.Equals(CIATrainCode));

                                                    bool atCiaCheck = false;
                                                    if (specialTrainCode == CIATrainCode)
                                                    {
                                                        atCiaCheck = true;
                                                    }

                                                    if (chkCIA != null || atCiaCheck == true)
                                                    {

                                                        //Insert AG_TRAIN_SPECIAL_USED_T
                                                        //AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => (id.ID_CARD_NO.Equals(idCard)
                                                        //    && id.LICENSE_NO.Equals(licenseNo) && id.SPECIAL_TYPE_CODE.Equals(CIATrainCode)));

                                                        //if (resCIAUsed == null)
                                                        //{
                                                        //    AG_TRAIN_SPECIAL_USED_T ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                                        //    ciaEnt.ID_CARD_NO = idCard;
                                                        //    ciaEnt.LICENSE_NO = licenseNo;
                                                        //    ciaEnt.SPECIAL_TYPE_CODE = CIATrainCode;
                                                        //    ciaEnt.START_DATE = DateTime.Now;
                                                        //    ciaEnt.USED_DATE = DateTime.Now;
                                                        //    ciaEnt.USER_ID = idCard;
                                                        //    ciaEnt.USER_DATE = DateTime.Now;
                                                        //    base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                                        //}

                                                        e.RESULT = TP;
                                                        switch (e.RESULT)
                                                        {
                                                            case "ผ่าน":
                                                                e.RESULT = TP;
                                                                res.DataResponse = e;
                                                                return res;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        e.RESULT = TF;
                                                        switch (e.RESULT)
                                                        {
                                                            case "ไม่ผ่าน":
                                                                res.DataResponse = e;
                                                                return res;
                                                        }
                                                    }

                                                }

                                                #endregion
                                            }

                                        #endregion

                                        }
                                        else
                                        {
                                            #region chkspecialCIA and chkspecialnotCIA


                                            //ตรวจสอบ การลดชั่วโมงผลอบรม วิทยากร ไม่ขึ้นต้นด้วย 3 และไม่ใช่ CIA = 10006
                                            AG_TRAIN_SPECIAL_T trainciachk = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                                && !dtx.SPECIAL_TYPE_CODE.StartsWith("3") && dtx.SPECIAL_TYPE_CODE != CIATrainCode);

                                            bool atnotCiaCheck = false;
                                            if (!specialTrainCode.StartsWith("3") && specialTrainCode != CIATrainCode.Trim() && !string.IsNullOrEmpty(specialTrainCode))
                                            {
                                                atnotCiaCheck = true;
                                            }

                                            if (trainciachk != null || atnotCiaCheck == true)
                                            {
                                                if ((SumHrsS + 6) >= (PERIOD_HH_TYPE_S - HOUR_DISCOUNT))
                                                {
                                                    //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                                                    string pilarChk = this.PilarValidation(resTrain, "S", curYear);
                                                    e.RESULT = pilarChk;
                                                    res.DataResponse = e;
                                                    return res;
                                                }
                                                else
                                                {
                                                    e.RESULT = TF;
                                                    switch (e.RESULT)
                                                    {
                                                        case "ไม่ผ่าน":
                                                            res.DataResponse = e;
                                                            return res;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //การลดชั่วโมงผลอบรม  CIA (ลดชั่วโมง 100%) ต่อประเภทธุรกิจ สามารถใช้ได้ 1 ครั้ง

                                                AG_TRAIN_SPECIAL_T chkCIA = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                                && dtx.SPECIAL_TYPE_CODE.Equals(CIATrainCode));

                                                bool atCiaCheck = false;
                                                if (specialTrainCode == CIATrainCode)
                                                {
                                                    atCiaCheck = true;
                                                }

                                                if (chkCIA != null || atCiaCheck == true)
                                                {

                                                    //Insert AG_TRAIN_SPECIAL_USED_T
                                                    //AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => (id.ID_CARD_NO.Equals(idCard)
                                                    //    && id.LICENSE_NO.Equals(licenseNo) && id.SPECIAL_TYPE_CODE.Equals(CIATrainCode)));

                                                    //if (resCIAUsed == null)
                                                    //{
                                                    //    AG_TRAIN_SPECIAL_USED_T ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                                    //    ciaEnt.ID_CARD_NO = idCard;
                                                    //    ciaEnt.LICENSE_NO = licenseNo;
                                                    //    ciaEnt.SPECIAL_TYPE_CODE = CIATrainCode;
                                                    //    ciaEnt.START_DATE = DateTime.Now;
                                                    //    ciaEnt.USED_DATE = DateTime.Now;
                                                    //    ciaEnt.USER_ID = idCard;
                                                    //    ciaEnt.USER_DATE = DateTime.Now;
                                                    //    base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                                    //}

                                                    e.RESULT = TP;
                                                    switch (e.RESULT)
                                                    {
                                                        case "ผ่าน":
                                                            e.RESULT = TP;
                                                            res.DataResponse = e;
                                                            return res;
                                                    }
                                                }
                                                else
                                                {
                                                    e.RESULT = TF;
                                                    switch (e.RESULT)
                                                    {
                                                        case "ไม่ผ่าน":
                                                            res.DataResponse = e;
                                                            return res;
                                                    }
                                                }

                                            }

                                            #endregion
                                        }
                                    }

                                }
                                else
                                {


                                    #region Gettrainspecialdiscount


                                    //Get AG_TRAIN_SPECIAL_DISCOUNT_T.HOUR_DISCOUNT
                                    string convertYear = Convert.ToString(curYear);
                                    AG_TRAIN_SPECIAL_DISCOUNT_T hrdiscount = base.ctx.AG_TRAIN_SPECIAL_DISCOUNT_T.FirstOrDefault(x => x.LICENSE_TYPE_CODE.Equals(licenseTypeCode) && x.P_YEAR == convertYear);
                                    if (hrdiscount != null)
                                    {
                                        HOUR_DISCOUNT = Convert.ToInt16(hrdiscount.HOUR_DISCOUNT);
                                    }

                                    #endregion


                                    #region chkTrainspecialStartwith 3


                                    //ตรวจสอบ การลดชั่วโมงผลอบรม วิทยากร ขึ้นต้นด้วย 3
                                    AG_TRAIN_SPECIAL_T trainspecial = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                        && dtx.SPECIAL_TYPE_CODE.StartsWith("3"));

                                    bool atspecialCheck = false;

                                    if (specialTrainCode.StartsWith("3"))
                                    {
                                        atspecialCheck = true;
                                    }



                                    if (trainspecial != null || atspecialCheck == true)
                                    {
                                        //TRAIN_DATE + 5 ปี > dateUnitLinkCompare();

                                        string trainspecialDT = string.Empty;

                                        if (trainspecial != null)
                                        {
                                            trainspecialDT = dateUnitLinkCompare(String.Format("{0:dd/MM/yyyy}", trainspecial.START_DATE));
                                        }
                                        else
                                        {
                                            trainspecialDT = dateUnitLinkCompare(String.Format("{0:dd/MM/yyyy}", DateTime.Now));
                                        }

                                        if (trainspecialDT.Equals(TP))
                                        {

                                            if ((SumHrsS) >= (PERIOD_HH_TYPE_S - HOUR_DISCOUNT))
                                            {
                                                //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                                                string pilarChk = this.PilarValidation(resTrain, "S", curYear);
                                                e.RESULT = pilarChk;
                                                res.DataResponse = e;
                                                return res;
                                            }
                                            else
                                            {
                                                e.RESULT = TF;
                                                switch (e.RESULT)
                                                {
                                                    case "ไม่ผ่าน":
                                                        res.DataResponse = e;
                                                        return res;
                                                }
                                            }
                                        }
                                        else
                                        {

                                            #region chkCia and NotCIA


                                            //ตรวจสอบ การลดชั่วโมงผลอบรม วิทยากร ไม่ขึ้นต้นด้วย 3 และไม่ใช่ CIA = 10006
                                            AG_TRAIN_SPECIAL_T trainciachk = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                                && !dtx.SPECIAL_TYPE_CODE.StartsWith("3") && dtx.SPECIAL_TYPE_CODE != CIATrainCode);

                                            bool atnotCiaCheck = false;
                                            if (!specialTrainCode.StartsWith("3") && specialTrainCode != CIATrainCode.Trim() && !string.IsNullOrEmpty(specialTrainCode))
                                            {
                                                atnotCiaCheck = true;
                                            }


                                            if (trainciachk != null || atnotCiaCheck == true)
                                            {
                                                if ((SumHrsS) >= (PERIOD_HH_TYPE_S - HOUR_DISCOUNT))
                                                {
                                                    //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                                                    string pilarChk = this.PilarValidation(resTrain, "S", curYear);
                                                    e.RESULT = pilarChk;
                                                    res.DataResponse = e;
                                                    return res;
                                                }
                                                else
                                                {
                                                    e.RESULT = TF;
                                                    switch (e.RESULT)
                                                    {
                                                        case "ไม่ผ่าน":
                                                            res.DataResponse = e;
                                                            return res;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //การลดชั่วโมงผลอบรม  CIA (ลดชั่วโมง 100%) ต่อประเภทธุรกิจ สามารถใช้ได้ 1 ครั้ง

                                                AG_TRAIN_SPECIAL_T chkCIA = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                                && dtx.SPECIAL_TYPE_CODE.Equals(CIATrainCode));

                                                bool atCiaCheck = false;
                                                if (specialTrainCode == CIATrainCode)
                                                {
                                                    atCiaCheck = true;
                                                }

                                                if (chkCIA != null || atCiaCheck == true)
                                                {

                                                    //Insert AG_TRAIN_SPECIAL_USED_T
                                                    //AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => (id.ID_CARD_NO.Equals(idCard)
                                                    //    && id.LICENSE_NO.Equals(licenseNo) && id.SPECIAL_TYPE_CODE.Equals(CIATrainCode)));

                                                    //if (resCIAUsed == null)
                                                    //{
                                                    //    AG_TRAIN_SPECIAL_USED_T ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                                    //    ciaEnt.ID_CARD_NO = idCard;
                                                    //    ciaEnt.LICENSE_NO = licenseNo;
                                                    //    ciaEnt.SPECIAL_TYPE_CODE = CIATrainCode;
                                                    //    ciaEnt.START_DATE = DateTime.Now;
                                                    //    ciaEnt.USED_DATE = DateTime.Now;
                                                    //    ciaEnt.USER_ID = idCard;
                                                    //    ciaEnt.USER_DATE = DateTime.Now;
                                                    //    base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                                    //}

                                                    e.RESULT = TP;
                                                    switch (e.RESULT)
                                                    {
                                                        case "ผ่าน":
                                                            e.RESULT = TP;
                                                            res.DataResponse = e;
                                                            return res;
                                                    }
                                                }
                                                else
                                                {
                                                    e.RESULT = TF;
                                                    switch (e.RESULT)
                                                    {
                                                        case "ไม่ผ่าน":
                                                            res.DataResponse = e;
                                                            return res;
                                                    }
                                                }

                                            }
                                        }

                                            #endregion

                                    }

                                    #endregion

                                    else
                                    {
                                        #region chkCia and NotCIA

                                        //ตรวจสอบ การลดชั่วโมงผลอบรม วิทยากร ไม่ขึ้นต้นด้วย 3 และไม่ใช่ CIA = 10006
                                        AG_TRAIN_SPECIAL_T trainciachk = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                            && !dtx.SPECIAL_TYPE_CODE.StartsWith("3") && dtx.SPECIAL_TYPE_CODE != CIATrainCode);

                                        bool atnotCiaCheck = false;
                                        if (!specialTrainCode.StartsWith("3") && specialTrainCode != CIATrainCode.Trim() && !string.IsNullOrEmpty(specialTrainCode))
                                        {
                                            atnotCiaCheck = true;
                                        }


                                        if (trainciachk != null || atnotCiaCheck == true)
                                        {
                                            if ((SumHrsS) >= (PERIOD_HH_TYPE_S - HOUR_DISCOUNT))
                                            {
                                                //ตรวจสอบเงื่อนไขวันอบรมก่อน 1/1/2556 และ PILAR
                                                string pilarChk = this.PilarValidation(resTrain, "S", curYear);
                                                e.RESULT = pilarChk;
                                                res.DataResponse = e;
                                                return res;
                                            }
                                            else
                                            {
                                                e.RESULT = TF;
                                                switch (e.RESULT)
                                                {
                                                    case "ไม่ผ่าน":
                                                        res.DataResponse = e;
                                                        return res;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //การลดชั่วโมงผลอบรม  CIA (ลดชั่วโมง 100%) ต่อประเภทธุรกิจ สามารถใช้ได้ 1 ครั้ง

                                            AG_TRAIN_SPECIAL_T chkCIA = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard)
                                            && dtx.SPECIAL_TYPE_CODE.Equals(CIATrainCode));

                                            bool atCiaCheck = false;
                                            if (specialTrainCode == CIATrainCode)
                                            {
                                                atCiaCheck = true;
                                            }

                                            if (chkCIA != null || atCiaCheck == true)
                                            {

                                                //Insert AG_TRAIN_SPECIAL_USED_T
                                                //AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => (id.ID_CARD_NO.Equals(idCard)
                                                //    && id.LICENSE_NO.Equals(licenseNo) && id.SPECIAL_TYPE_CODE.Equals(CIATrainCode)));

                                                //if (resCIAUsed == null)
                                                //{
                                                //    AG_TRAIN_SPECIAL_USED_T ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                                //    ciaEnt.ID_CARD_NO = idCard;
                                                //    ciaEnt.LICENSE_NO = licenseNo;
                                                //    ciaEnt.SPECIAL_TYPE_CODE = CIATrainCode;
                                                //    ciaEnt.START_DATE = DateTime.Now;
                                                //    ciaEnt.USED_DATE = DateTime.Now;
                                                //    ciaEnt.USER_ID = idCard;
                                                //    ciaEnt.USER_DATE = DateTime.Now;
                                                //    base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                                //}

                                                e.RESULT = TP;
                                                switch (e.RESULT)
                                                {
                                                    case "ผ่าน":
                                                        e.RESULT = TP;
                                                        res.DataResponse = e;
                                                        return res;
                                                }
                                            }
                                            else
                                            {
                                                e.RESULT = TF;
                                                switch (e.RESULT)
                                                {
                                                    case "ไม่ผ่าน":
                                                        res.DataResponse = e;
                                                        return res;
                                                }
                                            }

                                        }
                                        #endregion
                                    }

                                }

                            }
                        }
                    }
                    //CIA Check Code ="10006"
                    else
                    {
                        //ตรวจสอบ การลดชั่วโมงผลอบรม CIA = 10006
                        //ผ่่าน
                        AG_TRAIN_SPECIAL_T trainciachk = base.ctx.AG_TRAIN_SPECIAL_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(dtx => dtx.ID_CARD_NO.Equals(idCard) && dtx.SPECIAL_TYPE_CODE == CIATrainCode);
                        //TRAIN_DATE + 5 ปี > dateUnitLinkCompare();
                        string trainspecialDT = dateUnitLinkCompare(String.Format("{0:dd/MM/yyyy}", trainciachk.START_DATE));
                        if (trainspecialDT.Equals(TP))
                        {
                            e.RESULT = TP;
                            res.DataResponse = e;
                            return res;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_GetPersonalTrainByCriteria" + ":" + ex.Message, ex.Message);
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }

            if (res.DataResponse == null)
            {
                e.RESULT = TF;
                res.DataResponse = e;
            }

            base.ctx.SaveChanges();
            return res;
        }

        private string PilarValidation(List<DTO.TrainPersonHistory> resTrain, string TrainType, string curYear)
        {
            DTO.TrainPersonHistory e = new DTO.TrainPersonHistory();
            string TP = "ผ่าน";
            string TF = "ไม่ผ่าน";
            string trainDateBefore = "01/01/2556";
            string res = string.Empty;
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {
                var getT = resTrain.Where(s => s.TRAIN_TYPE.Equals(TrainType)).ToList();

                if (getT.Count > 0)
                {
                    //ตรวจสอบต้องมีชั่วโมงครบตาม PILAR บุคคลที่อบรมก่อน วันที่ 1/มค/2556 ไม่ต้องตรวจสอบ เงื่อนไข PILAR > trainDateBefore
                    var dtCompare = getT.Where(x => DateTime.Compare(DateTime.Parse(String.Format("{0:dd/MM/yyy}", x.TRAIN_DATE).ToString()), DateTime.Parse(trainDateBefore)) == -1).ToList();
                    if (dtCompare.Count > 0)
                    {
                        e.RESULT = TP;
                        switch (e.RESULT)
                        {
                            case "ผ่าน":
                                res = e.RESULT;
                                return res;
                        }
                    }
                    else
                    {

                        //ตรวจสอบ PILAR_1, PILAR_2, PILAR_3 > 9/0/6

                        //ตรวจสอบว่า check Pilar ถ้า N ไม่ต้องตรวจสอบ PILAR ถ้าเป็น Y ต้องตรวจสอบ PILAR
                        sql = "select AG_CONFIG_PILAR(dUMMY) as  chkPirar from dual";

                        var dtCheckPilar = ora.GetDataTable(sql);
                        var drCheckPilar = dtCheckPilar.Rows[0];

                        string chkPILAR = drCheckPilar["chkPirar"].ToString();


                       if (chkPILAR != "N")
                        {

                            //slect ค่า PILAR ปีปัจจุบัน
                            sql = string.Empty;
                            sql = "select PILAR_1,PILAR_2,PILAR_3 "
                                  + "from AG_TRAIN_PILAR_T "
                                  + "where P_YEAR = " + curYear + " ";

                            var dtPilar = ora.GetDataTable(sql);
                            var drPilar = dtPilar.Rows[0];


                            var pilar_1 = drPilar["PILAR_1"].ToString();
                            var pilar_2 = drPilar["PILAR_2"].ToString();
                            var pilar_3 = drPilar["PILAR_3"].ToString();


                            //รวม PILAR1,PILAR2,PILAR3
                            Int32? sumPilar1 = getT.Where(s => s.TRAIN_TYPE.Equals(TrainType)).ToList().Sum(t => t.PILAR_1);
                            Int32? sumPilar2 = getT.Where(s => s.TRAIN_TYPE.Equals(TrainType)).ToList().Sum(t => t.PILAR_2);
                            Int32? sumPilar3 = getT.Where(s => s.TRAIN_TYPE.Equals(TrainType)).ToList().Sum(t => t.PILAR_3);





                            //var pilar_1 = getT.Where(p1 => p1.PILAR_1 >= 9).ToList();
                            //var pilar_2 = getT.Where(p1 => p1.PILAR_2 >= 0).ToList();
                            // var pilar_3 = getT.Where(p1 => p1.PILAR_3 >= 6).ToList();
                            //ผ่าน


                            //if ((pilar_1.Count > 0) && (pilar_2.Count > 0) && (pilar_3.Count > 0))

                            if ((sumPilar1 >= pilar_1.ToInt()) && (sumPilar2 >= pilar_2.ToInt()) && (sumPilar3 >= pilar_3.ToInt()))
                            {
                                e.RESULT = TP;
                                switch (e.RESULT)
                                {
                                    case "ผ่าน":
                                        res = e.RESULT;
                                        return res;
                                }
                            }
                            //ไม่ผ่าน
                            else
                            {
                                e.RESULT = TF;
                                switch (e.RESULT)
                                {
                                    case "ไม่ผ่าน":
                                        res = e.RESULT;
                                        return res;
                                }
                            }
                        }
                        else
                        {
                            e.RESULT = TP;
                            switch (e.RESULT)
                            {
                                case "ผ่าน":
                                    res = e.RESULT;
                                    return res;
                            }
                        }
                    }
                 
                }
                else
                {
                    e.RESULT = TF;
                    switch (e.RESULT)
                    {
                        case "ไม่ผ่าน":
                            res = e.RESULT;
                            return res;
                    }
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_GetPersonalTrainByCriteria" + ":" + ex.Message, ex.Message);
                //res = "Transaction not complet. Please contact Administrator";
                res = "โปรดติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        /// <summary>
        /// ดึงข้อมูลประวัติการอบรมด้วยรหัสบัตรประชาชน แบบไม่มีเงื่อนไข
        /// </summary> Renew 30/10/2556
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<List<DTO.TrainPersonHistory>> GetTrainingHistoryByID(string idCard)
        {
            var res = new DTO.ResponseService<List<DTO.TrainPersonHistory>>();
            try
            {
                #region Func
                Func<string, string> trainresult = delegate(string status)
                {
                    if ((status != null) && (status != ""))
                    {
                        if (status.Equals("P"))
                        {
                            status = Resources.propLicenseService_P;
                        }
                        else
                        {
                            status = Resources.propLicenseService_F;
                        }
                    }
                    if ((status == null) || (status == ""))
                    {
                        status = Resources.propLicenseService_026;
                    }

                    return status;
                };

                Func<string, string> licenseTypeConvert = delegate(string input)
                {
                    if ((input != null) && (input != ""))
                    {

                        string ent = base.ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(w => w.LICENSE_TYPE_CODE == input).LICENSE_TYPE_NAME;
                        if (ent != null)
                        {
                            input = ent;
                        }
                    }

                    return input;
                };

                Func<string, string> traintype = delegate(string type)
                {
                    if ((type != null) && (type != ""))
                    {
                        if (type.Equals("T"))
                        {
                            type = Resources.propLicenseService_027;
                        }
                        else
                        {
                            type = Resources.propLicenseService_028;
                        }
                    }
                    if ((type == null) || (type == ""))
                    {
                        type = Resources.propLicenseService_029;
                    }

                    return type;
                };
                #endregion

                DateTime currentDate = DateTime.Now;

                var result = (from TRAIN in base.ctx.AG_TRAIN_PERSON_T
                              from TRAINPLAN in this.ctx.AG_TRAIN_PLAN_T
                              where TRAIN.TRAIN_CODE == TRAINPLAN.TRAIN_CODE &&
                              TRAIN.ID_CARD_NO == idCard
                              select new DTO.TrainPersonHistory
                              {
                                  TRAIN_CODE = TRAIN.TRAIN_CODE,
                                  ID_CARD_NO = TRAIN.ID_CARD_NO,
                                  LICENSE_TYPE_CODE = TRAIN.LICENSE_TYPE_CODE,
                                  LICENSE_TYPE_NAME = TRAINPLAN.LICENSE_TYPE_CODE,
                                  TRAIN_TIMES = TRAIN.TRAIN_TIMES,
                                  TRAIN_DATE = TRAIN.TRAIN_DATE,
                                  TRAIN_DATE_EXP = TRAIN.TRAIN_EXP_DATE,
                                  STATUS = TRAIN.RESULT,
                                  PILAR_1 = TRAIN.PILAR_1,
                                  PILAR_2 = TRAIN.PILAR_2,
                                  PILAR_3 = TRAIN.PILAR_3,
                                  TRAIN_TYPE = TRAIN.TRAIN_TYPE,
                                  HOURS = TRAIN.TRAIN_PERIOD,

                              }).OrderByDescending(d => d.TRAIN_DATE_EXP).ToList();


                if (result.Count > 0)
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        //string dateFilter = Convert.ToString(Convert.ToDateTime(result[i].TRAIN_DATE_EXP).AddYears(-543));
                        result[i].LICENSE_TYPE_NAME = licenseTypeConvert(result[i].LICENSE_TYPE_CODE);
                        result[i].STATUS = trainresult(result[i].STATUS);
                        result[i].TRAIN_TYPE = traintype(result[i].TRAIN_TYPE);
                        result[i].RESULT = result[i].STATUS;
                        result[i].HOURS = result[i].HOURS;
                        //result[i].HOURS = Convert.ToInt16(result[i].PILAR_1 + result[i].PILAR_2 + result[i].PILAR_3);
                    }
                }

                res.DataResponse = result.OrderByDescending(exp => exp.TRAIN_DATE_EXP).ToList();
            }
            catch (Exception ex)
            {
                //res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetTrainingHistoryByID" + ":" + ex.Message, ex.Message);
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลการอบรม 3-7 โดยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<List<DTO.Tran3To7>></returns>
        public DTO.ResponseService<List<DTO.Tran3To7>> GetTrain_3_To_7_ByID(string idCard)
        {
            var res = new DTO.ResponseService<List<DTO.Tran3To7>>();
            try
            {
                var result = (from SPT in base.ctx.AG_TRAIN_SPECIAL_T
                              join SPR in base.ctx.AG_TRAIN_SPECIAL_R on SPT.SPECIAL_TYPE_CODE equals SPR.SPECIAL_TYPE_CODE
                              where SPT.SPECIAL_TYPE_CODE == SPR.SPECIAL_TYPE_CODE &&
                              SPT.ID_CARD_NO == idCard
                              select new DTO.Tran3To7
                              {
                                  SPECIAL_TYPE_CODE = SPT.SPECIAL_TYPE_CODE,
                                  SPECIAL_TYPE_DESC = SPR.SPECIAL_TYPE_DESC,
                                  START_DATE = SPT.START_DATE,
                                  END_DATE = SPT.END_DATE,
                                  SEND_DATE = SPT.SEND_DATE,
                                  SEND_BY = SPT.SEND_BY,
                                  ID_CARD_NO = SPT.ID_CARD_NO

                              }).ToList();

                res.DataResponse = result;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetTrain_3_To_7_ByID" + ":" + ex.Message, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูล Unit Link โดยรหัสบัตรประชาชน แบบไม่มีเงื่อนไข
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>DTO.ResponseService<List<DTO.UnitLink>></returns>
        public DTO.ResponseService<List<DTO.UnitLink>> GetUnitLinkByID(string idCard)
        {
            var res = new DTO.ResponseService<List<DTO.UnitLink>>();
            try
            {
                var result = (from U in base.ctx.AG_U_TRAIN_T
                              join LTR in base.ctx.AG_IAS_LICENSE_TYPE_R on U.LICENSE_TYPE_CODE equals LTR.LICENSE_TYPE_CODE
                              where U.LICENSE_TYPE_CODE == LTR.LICENSE_TYPE_CODE &&
                              U.ID_CARD_NO == idCard
                              select new DTO.UnitLink
                              {
                                  LICENSE_TYPE_CODE = U.LICENSE_TYPE_CODE,
                                  LICENSE_TYPE_NAME = LTR.LICENSE_TYPE_NAME,
                                  TRAIN_TIMES = U.TRAIN_TIMES,
                                  TRAIN_DATE = U.TRAIN_DATE,
                                  ID_CARD_NO = U.ID_CARD_NO,

                              }).ToList();

                res.DataResponse = result;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetUnitLinkByID" + ":" + ex.Message, ex.Message);
            }
            return res;

        }

        /// <summary>
        /// ดึงข้อมูล Unit Link โดยรหัสบัตรประชาชน แบบมีเงื่อนไข
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>DTO.ResponseService<List<DTO.UnitLink>></returns>
        public DTO.ResponseService<List<DTO.UnitLink>> GetUnitLinkByIDWithCondition(string idCard, string licenseTypeCode)
        {
            var res = new DTO.ResponseService<List<DTO.UnitLink>>();
            try
            {
                var result = (from U in base.ctx.AG_U_TRAIN_T
                              join LTR in base.ctx.AG_IAS_LICENSE_TYPE_R on U.LICENSE_TYPE_CODE equals LTR.LICENSE_TYPE_CODE
                              where U.LICENSE_TYPE_CODE == LTR.LICENSE_TYPE_CODE &&
                              U.ID_CARD_NO == idCard &&
                              U.LICENSE_TYPE_CODE == licenseTypeCode
                              select new DTO.UnitLink
                              {
                                  LICENSE_TYPE_CODE = U.LICENSE_TYPE_CODE,
                                  LICENSE_TYPE_NAME = LTR.LICENSE_TYPE_NAME,
                                  TRAIN_TIMES = U.TRAIN_TIMES,
                                  TRAIN_DATE = U.TRAIN_DATE,
                                  ID_CARD_NO = U.ID_CARD_NO,

                              }).ToList();

                res.DataResponse = result;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetUnitLinkByIDWithCondition" + ":" + ex.Message, ex.Message);
            }
            return res;

        }

        /// <summary>
        /// Get Image Files License by idCard
        /// </summary>
        /// <param name="personID">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<List<ddd>></returns>
        public DTO.ResponseService<List<DTO.PersonAttatchFile>> GetAttatchFileLicenseByPersonId(string personID)
        {
            DTO.ResponseService<List<DTO.PersonAttatchFile>> res = new DTO.ResponseService<List<DTO.PersonAttatchFile>>();
            List<DTO.PersonAttatchFile> targetList = new List<DTO.PersonAttatchFile>();
            DTO.PersonAttatchFile pa = new DTO.PersonAttatchFile();

            try
            {
                var person = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(per => per.ID == personID);

                if (person != null)
                {
                    //ข้อมูล User Profile มีการแก้ไข
                    if (person.STATUS == ((int)DTO.PersonDataStatus.WaitForApprove).ToString())
                    {

                        List<AG_IAS_ATTACH_FILE_LICENSE> attachs = base.ctx.AG_IAS_ATTACH_FILE_LICENSE.Where(a => a.ID_ATTACH_FILE == personID).ToList();
                        foreach (AG_IAS_ATTACH_FILE_LICENSE ls in attachs)
                        {
                            var ent = ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(w => w.DOCUMENT_CODE == ls.ATTACH_FILE_TYPE);
                            ls.MappingToEntity(pa);
                            pa.DocumentTypeName = ent.DOCUMENT_NAME;
                            targetList.Add(pa);
                        }
                    }
                    //ข้อมูล User Profile ไม่มีการแก้ไข
                    else
                    {
                        List<AG_IAS_ATTACH_FILE_LICENSE> attachs = base.ctx.AG_IAS_ATTACH_FILE_LICENSE.Where(b => b.ID_ATTACH_FILE == personID).ToList();

                        foreach (AG_IAS_ATTACH_FILE_LICENSE ls in attachs)
                        {
                            var ent = ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(w => w.DOCUMENT_CODE == ls.ATTACH_FILE_TYPE);
                            ls.MappingToEntity(pa);
                            pa.DocumentTypeName = ent.DOCUMENT_NAME;
                            targetList.Add(pa);
                        }
                    }

                    res.DataResponse = targetList;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetAttatchFileLicenseByPersonId" + ":" + ex.Message, ex.Message);
            }

            return res;

        }

        private DTO.ResponseService<List<DTO.SpecialDocument>> GetSpecialDocType(string docStatus, string trainStatus)
        {
            var res = new DTO.ResponseService<List<DTO.SpecialDocument>>();
            try
            {
                IQueryable<DTO.SpecialDocument> result = (from doct in base.ctx.AG_IAS_DOCUMENT_TYPE
                                                          join spe in base.ctx.AG_TRAIN_SPECIAL_R on doct.SPECIAL_TYPE_CODE_TRAIN equals spe.SPECIAL_TYPE_CODE
                                                          where doct.STATUS == docStatus && doct.TRAIN_DISCOUNT_STATUS == trainStatus
                                                          select new DTO.SpecialDocument
                                                          {
                                                              DOCUMENT_CODE = doct.DOCUMENT_CODE,
                                                              DOCUMENT_NAME = doct.DOCUMENT_NAME,
                                                              DOCUMENT_REQUIRE = doct.DOCUMENT_REQUIRE,
                                                              TRAIN_DISCOUNT_STATUS = doct.TRAIN_DISCOUNT_STATUS,
                                                              EXAM_DISCOUNT_STATUS = doct.EXAM_DISCOUNT_STATUS,
                                                              SPECIAL_TYPE_CODE_TRAIN = doct.SPECIAL_TYPE_CODE_TRAIN,
                                                              SPECIAL_TYPE_CODE_EXAM = doct.SPECIAL_TYPE_CODE_EXAM,
                                                              STATUS = doct.STATUS

                                                          });

                res.DataResponse = result.ToList();

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetSpecialDocType" + ":" + ex.Message, ex.Message);
            }
            return res;

        }

        /// <summary>
        /// บันทึกข้อมูลการขอใบอนุญาต + ไฟล์ 
        /// Last update 28/04/2557
        /// </summary>
        /// <param name="header"></param>
        /// <param name="detail"></param>
        /// <param name="userProfile"></param>
        /// <param name="files"></param>
        /// <returns>DTO.ResponseMessage<bool></returns>
        /// <AHTHOR>Natta</AHTHOR>
        /// <LASTUPDATE>28/04/2557</LASTUPDATE>
        public DTO.ResponseMessage<bool> InsertPersonLicense(List<DTO.PersonLicenseHead> header, List<DTO.PersonLicenseDetail> detail, DTO.UserProfile userProfile, List<DTO.AttatchFileLicense> files)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            AG_IAS_SPECIAL_T_TEMP EntTemp = new AG_IAS_SPECIAL_T_TEMP();
            AG_TRAIN_SPECIAL_USED_T ciaEnt = new AG_TRAIN_SPECIAL_USED_T();


            try
            {
                #region Func
                Func<string, string> convertLicenseName = delegate(string input)
                {
                    if ((input != null) && (input != ""))
                    {
                        string ent = base.ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(li => li.LICENSE_TYPE_CODE == input).LICENSE_TYPE_NAME;
                        if (ent != "")
                        {
                            input = ent;
                        }
                    }

                    return input;
                };

                #endregion

                using (TransactionScope trans = new TransactionScope())
                {
                    #region Attach Files

                    //Get Special Type
                    //DTO.ResponseService<List<DTO.SpecialDocument>> lsSpecialType = this.GetSpecialDocType("A", "Y");


                    //getlistExamSpecial
                    var lsSpecialExam = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                                         from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                                         where
                                         dt.SPECIAL_TYPE_CODE_EXAM == ex.SPECIAL_TYPE_CODE
                                         && dt.EXAM_DISCOUNT_STATUS == "Y"
                                         && dt.STATUS == "A"
                                         select dt.DOCUMENT_CODE).ToList();

                    //getlistTrainSpecial
                    var lsSpecialTrain = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                                          from ex in ctx.AG_TRAIN_SPECIAL_R
                                          where
                                          dt.SPECIAL_TYPE_CODE_TRAIN == ex.SPECIAL_TYPE_CODE
                                          && dt.TRAIN_DISCOUNT_STATUS == "Y"
                                          && dt.STATUS == "A"
                                          select dt.DOCUMENT_CODE).ToList();









                    foreach (DTO.AttatchFileLicense l in files)
                    {
                        //Check before add new
                        AG_IAS_ATTACH_FILE_LICENSE ent = base.ctx.AG_IAS_ATTACH_FILE_LICENSE.FirstOrDefault(f => f.ID_ATTACH_FILE == l.ID_ATTACH_FILE);
                        if (ent == null)
                        {
                            AG_IAS_ATTACH_FILE_LICENSE attach = new AG_IAS_ATTACH_FILE_LICENSE();
                            String targetFileName = new FileInfo(l.ATTACH_FILE_PATH).Name;
                            String targetContainer = String.Format(@"{0}\{1}", AttachFileContainer, userProfile.Id);

                            MoveFileResponse moveResponse = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                            {
                                CurrentContainer = ""
                                ,
                                CurrentFileName = l.ATTACH_FILE_PATH
                                ,
                                TargetContainer = targetContainer
                                ,
                                TargetFileName = targetFileName
                            }).Action();

                            l.MappingToEntity(attach);
                            attach.ID_ATTACH_FILE = OracleDB.GetGenAutoId();
                            attach.ATTACH_FILE_PATH = moveResponse.TargetFullName;
                            base.ctx.AG_IAS_ATTACH_FILE_LICENSE.AddObject(attach);

                            DateTime startDate;


                            if (lsSpecialExam.Contains(l.ATTACH_FILE_TYPE))
                            {
                                var specialCode = ctx.AG_IAS_DOCUMENT_TYPE.FirstOrDefault(a => a.DOCUMENT_CODE == l.ATTACH_FILE_TYPE);

                                string year = DateTime.Now.AddYears(543).Year.ToString();

                                var specialExam = ctx.AG_IAS_SPECIAL_T_TEMP.FirstOrDefault(s => s.ID_CARD_NO == l.ID_CARD_NO
                                && s.SPECIAL_TYPE_CODE == specialCode.SPECIAL_TYPE_CODE_EXAM);


                                startDate = DateTime.Now;

                                if (specialExam != null)
                                {
                                    if (specialExam.STATUS != "Y")
                                    {
                                        specialExam.STATUS = "W";
                                        specialExam.START_DATE = startDate;
                                        specialExam.SEND_DATE = DateTime.Now;
                                        specialExam.SEND_YEAR = year;
                                        specialExam.USER_DATE = DateTime.Now;
                                        specialExam.SEND_BY = "IAS";
                                        specialExam.USER_ID = userProfile.IdCard;
                                    }
                                }
                                else
                                {

                                    EntTemp = new AG_IAS_SPECIAL_T_TEMP();

                                    EntTemp.ID_CARD_NO = l.ID_CARD_NO;
                                    EntTemp.SPECIAL_TYPE_CODE = specialCode.SPECIAL_TYPE_CODE_EXAM;
                                    EntTemp.START_DATE = startDate;
                                    EntTemp.SEND_DATE = DateTime.Now;
                                    EntTemp.SEND_YEAR = year;
                                    EntTemp.USER_DATE = DateTime.Now;
                                    EntTemp.SEND_BY = "IAS";
                                    EntTemp.USER_ID = userProfile.IdCard;
                                    EntTemp.STATUS = "W";
                                    EntTemp.EXAM_DISCOUNT_STATUS = "Y";

                                    base.ctx.AG_IAS_SPECIAL_T_TEMP.AddObject(EntTemp);
                                }



                            }

                            else if (lsSpecialTrain.Contains(l.ATTACH_FILE_TYPE))
                            {
                                var specialCode = ctx.AG_IAS_DOCUMENT_TYPE.FirstOrDefault(a => a.DOCUMENT_CODE == l.ATTACH_FILE_TYPE);

                                string year = DateTime.Now.AddYears(543).Year.ToString();


                                var specialTrain = ctx.AG_IAS_SPECIAL_T_TEMP.FirstOrDefault(s => s.ID_CARD_NO.Trim() == l.ID_CARD_NO.Trim()
                                && s.SPECIAL_TYPE_CODE.Trim() == specialCode.SPECIAL_TYPE_CODE_TRAIN.Trim());


                                startDate = DateTime.Now;



                                if (specialTrain != null)
                                {
                                    if (specialTrain.STATUS != "Y")
                                    {
                                        specialTrain.STATUS = "W";
                                        specialTrain.START_DATE = startDate;
                                        specialTrain.SEND_DATE = DateTime.Now;
                                        specialTrain.SEND_YEAR = year;
                                        specialTrain.USER_DATE = DateTime.Now;
                                        specialTrain.SEND_BY = "IAS";
                                        specialTrain.USER_ID = userProfile.IdCard;
                                    }

                                    if (specialCode.SPECIAL_TYPE_CODE_TRAIN == "1006")
                                    {

                                        //licenseno
                                        var detailLicense = detail.Where(w => w.ID_CARD_NO.Trim() == userProfile.IdCard.Trim()).FirstOrDefault();

                                        //Insert AG_TRAIN_SPECIAL_USED_T
                                        AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => id.ID_CARD_NO.Trim() == userProfile.IdCard.Trim()
                                        && id.SPECIAL_TYPE_CODE == "1006" && id.LICENSE_NO == detailLicense.LICENSE_NO);

                                        if (resCIAUsed == null)
                                        {
                                            ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                            ciaEnt.ID_CARD_NO = userProfile.IdCard;
                                            ciaEnt.LICENSE_NO = detailLicense.LICENSE_NO;
                                            ciaEnt.SPECIAL_TYPE_CODE = "1006";
                                            ciaEnt.START_DATE = DateTime.Now;
                                            ciaEnt.USED_DATE = DateTime.Now;
                                            ciaEnt.USER_ID = userProfile.IdCard;
                                            ciaEnt.USER_DATE = DateTime.Now;

                                            base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                        }

                                    }
                                }
                                else
                                {
                                    EntTemp = new AG_IAS_SPECIAL_T_TEMP();
                                    EntTemp.ID_CARD_NO = l.ID_CARD_NO;
                                    EntTemp.SPECIAL_TYPE_CODE = specialCode.SPECIAL_TYPE_CODE_TRAIN;
                                    EntTemp.START_DATE = startDate;
                                    EntTemp.SEND_DATE = DateTime.Now;
                                    EntTemp.SEND_YEAR = year;
                                    EntTemp.USER_DATE = DateTime.Now;
                                    EntTemp.SEND_BY = "IAS";
                                    EntTemp.USER_ID = userProfile.IdCard;
                                    EntTemp.STATUS = "W";
                                    EntTemp.TRAIN_DISCOUNT_STATUS = "Y";
                                    if (specialCode.SPECIAL_TYPE_CODE_TRAIN.StartsWith("3"))
                                    {
                                        EntTemp.ID_ATTACH_FILE = l.ID_ATTACH_FILE;

                                        if (startDate != null || startDate == DateTime.MinValue)
                                        {
                                            DateTime stDateYear = Convert.ToDateTime(startDate).AddYears(5);
                                            EntTemp.END_DATE = stDateYear;
                                        }

                                    }

                                    base.ctx.AG_IAS_SPECIAL_T_TEMP.AddObject(EntTemp);

                                    if (specialCode.SPECIAL_TYPE_CODE_TRAIN == "1006")
                                    {

                                        //licenseno
                                        var detailLicense = detail.Where(w => w.ID_CARD_NO.Trim() == userProfile.IdCard.Trim()).FirstOrDefault();

                                        //Insert AG_TRAIN_SPECIAL_USED_T
                                        AG_TRAIN_SPECIAL_USED_T resCIAUsed = base.ctx.AG_TRAIN_SPECIAL_USED_T.OrderBy(dt => dt.START_DATE).FirstOrDefault(id => id.ID_CARD_NO.Trim() == userProfile.IdCard.Trim()
                                        && id.SPECIAL_TYPE_CODE == "1006" && id.LICENSE_NO == detailLicense.LICENSE_NO);

                                        if (resCIAUsed == null)
                                        {
                                            ciaEnt = new AG_TRAIN_SPECIAL_USED_T();
                                            ciaEnt.ID_CARD_NO = userProfile.IdCard;
                                            ciaEnt.LICENSE_NO = detailLicense.LICENSE_NO;
                                            ciaEnt.SPECIAL_TYPE_CODE = "1006";
                                            ciaEnt.START_DATE = DateTime.Now;
                                            ciaEnt.USED_DATE = DateTime.Now;
                                            ciaEnt.USER_ID = userProfile.IdCard;
                                            ciaEnt.USER_DATE = DateTime.Now;

                                            base.ctx.AG_TRAIN_SPECIAL_USED_T.AddObject(ciaEnt);
                                        }

                                    }
                                }
                            }








                            //old code
                            //var getspt = lsSpecialType.DataResponse.FirstOrDefault(x => x.DOCUMENT_CODE.Equals(l.ATTACH_FILE_TYPE));
                            //if (getspt != null)
                            //{
                            //    string sendY = DateTime.Now.Year.ToString();
                            //    IQueryable<AG_IAS_SPECIAL_T_TEMP> specialT = base.ctx.AG_IAS_SPECIAL_T_TEMP.Where(x => x.ID_CARD_NO == l.ID_CARD_NO &&
                            //        x.SPECIAL_TYPE_CODE == getspt.SPECIAL_TYPE_CODE_TRAIN && x.SEND_YEAR == sendY);

                            //    AG_IAS_SPECIAL_T_TEMP specialTs = base.ctx.AG_IAS_SPECIAL_T_TEMP.FirstOrDefault(x => x.ID_CARD_NO == l.ID_CARD_NO &&
                            //        x.SPECIAL_TYPE_CODE == getspt.SPECIAL_TYPE_CODE_TRAIN && x.SEND_YEAR == sendY);
                            //    if (specialT == null)
                            //    {
                            //        AG_IAS_SPECIAL_T_TEMP st = new AG_IAS_SPECIAL_T_TEMP
                            //        {
                            //            ID_ATTACH_FILE = attach.ID_ATTACH_FILE,
                            //            ID_CARD_NO = l.ID_CARD_NO,
                            //            SPECIAL_TYPE_CODE = getspt.SPECIAL_TYPE_CODE_TRAIN,
                            //            START_DATE = DateTime.Now,
                            //            END_DATE = DateTime.Now,
                            //            SEND_DATE = DateTime.Now,
                            //            SEND_BY = "",
                            //            USER_DATE = DateTime.Now,
                            //            SEND_YEAR = DateTime.Now.Year.ToString(),
                            //            STATUS = "W",
                            //            TRAIN_DISCOUNT_STATUS = getspt.TRAIN_DISCOUNT_STATUS,
                            //            EXAM_DISCOUNT_STATUS = getspt.EXAM_DISCOUNT_STATUS,
                            //            USER_ID = userProfile.Id
                            //        };
                            //        base.ctx.AG_IAS_SPECIAL_T_TEMP.AddObject(st);
                            //    }

                            //}





                            if (moveResponse.Code != "0000")
                                throw new IOException(moveResponse.Message);
                        }

                    }
                    #endregion

                    #region AG_IAS_LICENSE_H
                    foreach (DTO.PersonLicenseHead h in header)
                    {
                        //Check brfore add new
                        AG_IAS_LICENSE_H ent = base.ctx.AG_IAS_LICENSE_H.FirstOrDefault(w => w.LICENSE_TYPE_CODE == h.LICENSE_TYPE_CODE && w.PETITION_TYPE_CODE == h.PETITION_TYPE_CODE
                            && h.APPROVED_DOC == "W" && w.UPLOAD_BY_SESSION == h.UPLOAD_BY_SESSION && w.UPLOAD_GROUP_NO == h.UPLOAD_GROUP_NO);
                        if (ent == null)
                        {
                            AG_IAS_LICENSE_H entHeader = new AG_IAS_LICENSE_H();

                            //ตรวจสอบประเภทตัวแทน
                            AG_IAS_LICENSE_TYPE_R licTypeEnt = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == h.LICENSE_TYPE_CODE).FirstOrDefault();
                            string _agentType = string.Empty;
                            if (licTypeEnt != null)
                            {
                                _agentType = licTypeEnt.AGENT_TYPE;
                            }
                            else
                            {
                                res.ErrorMsg = "\n" + Resources.errorLicenseService_002;
                                return res;
                            }

                            h.MappingToEntity(entHeader);
                            base.ctx.AG_IAS_LICENSE_H.AddObject(entHeader);

                        }
                        else if (ent != null)
                        {

                            res.ErrorMsg = Resources.errorLicenseService_030 + convertLicenseName(ent.LICENSE_TYPE_CODE) + Resources.errorLicenseService_031;
                            return res;

                        }

                    }
                    #endregion

                    #region AG_IAS_LICENSE_D
                    foreach (DTO.PersonLicenseDetail d in detail)
                    {
                        //Check brfore add new
                        //ตรวจสอบLicense สถานะรออการอนุมัติ
                        AG_IAS_LICENSE_D entD = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(w => w.ID_CARD_NO == d.ID_CARD_NO && w.APPROVED == "W");
                        //Yes > Add new
                        if (entD == null)
                        {
                            AG_IAS_LICENSE_D entDetail = new AG_IAS_LICENSE_D();
                            AG_IAS_PERSONAL_T person = base.ctx.AG_IAS_PERSONAL_T.Where(w => w.ID_CARD_NO == userProfile.IdCard).FirstOrDefault();
                            if (person == null)
                            {
                                res.ErrorMsg = Resources.errorLicenseService_003;
                                return res;
                            }

                            d.MappingToEntity(entDetail);
                            base.ctx.AG_IAS_LICENSE_D.AddObject(entDetail);

                            AG_PERSONAL_T entPer = new AG_PERSONAL_T();
                            AG_PERSONAL_T personT = base.ctx.AG_PERSONAL_T.Where(w => w.ID_CARD_NO == userProfile.IdCard).FirstOrDefault();
                            if (personT == null)
                            {
                                d.MappingToEntity(entPer);
                                base.ctx.AG_PERSONAL_T.AddObject(entPer);
                            }

                        }
                        //No
                        if (entD != null)
                        {
                            //ตรวจสอบ เก่า > update
                            AG_IAS_LICENSE_D entOldDetail = new AG_IAS_LICENSE_D();
                            AG_IAS_LICENSE_D entDD = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(w => w.ID_CARD_NO == d.ID_CARD_NO && w.APPROVED == "Y" && w.LICENSE_NO == d.LICENSE_NO);
                            if (entDD == null)
                            {
                                d.MappingToEntity(entOldDetail);
                                base.ctx.AG_IAS_LICENSE_D.AddObject(entOldDetail);


                                AG_PERSONAL_T entPer = new AG_PERSONAL_T();
                                AG_PERSONAL_T personT = base.ctx.AG_PERSONAL_T.Where(w => w.ID_CARD_NO == userProfile.IdCard).FirstOrDefault();
                                if (personT == null)
                                {
                                    d.MappingToEntity(entPer);
                                    base.ctx.AG_PERSONAL_T.AddObject(entPer);
                                }
                            }
                            else if (entDD != null)
                            {
                                d.MappingToEntity(entOldDetail);
                                base.ctx.AG_IAS_LICENSE_D.AddObject(entOldDetail);


                                AG_PERSONAL_T entPer = new AG_PERSONAL_T();
                                AG_PERSONAL_T personT = base.ctx.AG_PERSONAL_T.Where(w => w.ID_CARD_NO == userProfile.IdCard).FirstOrDefault();
                                if (personT == null)
                                {
                                    d.MappingToEntity(entPer);
                                    base.ctx.AG_PERSONAL_T.AddObject(entPer);
                                }

                            }

                        }



                    }
                    #endregion

                    //Submit All Data
                    base.ctx.SaveChanges();
                    trans.Complete();
                    res.ResultMessage = true;
                }



            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_InsertPersonLicense", ex);
            }
            return res;

        }


        /// <summary>
        /// Move Files from Temp > AttachFile
        /// </summary>
        /// <param name="uploadGroupNo"></param>
        /// <returns></returns>
        public DTO.ResponseService<string> SubmitReceiveLicensePersonUpload(string groupId, List<DTO.AttatchFileLicense> list)
        {

            var res = new DTO.ResponseService<string>();
            try
            {
                //AG_IAS_LICENSE_H header = base.ctx.AG_IAS_LICENSE_H.SingleOrDefault(w => w.UPLOAD_GROUP_NO == groupId);


                //AG_IAS_LICENSE_H entHeader = new AG_IAS_LICENSE_H();

                //header.MappingToEntity<AG_IAS_LICENSE_H>(entHeader);

                //base.ctx.AG_IAS_LICENSE_H.AddObject(entHeader);

                //List<AG_IAS_LICENSE_D> details = base.ctx.AG_IAS_LICENSE_D.Where(w => w.UPLOAD_GROUP_NO == groupId).ToList();

                ////foreach (AG_IAS_LICENSE_D d in details)
                ////{
                ////    //this.ValidateReceiveLicenseTemp(groupId.ToLong(),
                ////}

                //int hasError = details.Where(delegate(AG_IAS_LICENSE_D temp)
                //{
                //    return !string.IsNullOrEmpty(temp.ERR_DESC);
                //}).Count();

                //if (hasError > 0)
                //{
                //    res.ErrorMsg = "มีข้อมูลการขอรับใบอนุญาตที่ยังไม่ถูกต้องอยู่โปรดแก้ไขให้ถูกต้องก่อน Submit ข้อมูล";
                //    return res;
                //}

                //int rowAffected = 0;
                //foreach (AG_IAS_LICENSE_D d in details)
                //{
                //    //AG_IAS_LICENSE_D ent = new AG_IAS_LICENSE_D();
                //    //d.MappingToEntity(ent);
                //    base.ctx.AG_IAS_LICENSE_D.AddObject(ent);
                //    rowAffected += 1;
                //}
                //foreach (DTO.AttatchFileLicense l in list)
                //{
                //    var attach = new AG_IAS_ATTACH_FILE_LICENSE();
                //    l.MappingToEntity(attach);
                //    attach.ID_ATTACH_FILE = OracleDB.GetGenAutoId();
                //    base.ctx.AG_IAS_ATTACH_FILE_LICENSE.AddObject(attach);
                //}
                //using (var ts = new TransactionScope())
                //{
                //    base.ctx.SaveChanges();
                //    ts.Complete();
                //}

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_SubmitReceiveLicensePersonUpload", ex);
            }
            return res;
        }


        /// <summary>
        /// Get Approver COMPCODE
        /// </summary>
        /// <param name="approvedoctype"></param>
        /// <returns>DTO.ResponseService<List<DTO.ApproverDoctype>></returns>
        public DTO.ResponseService<List<DTO.ApproverDoctype>> GetApprocerByDocType(string appdocType)
        {
            var res = new DTO.ResponseService<List<DTO.ApproverDoctype>>();
            try
            {
                var result = (from ap in base.ctx.AG_IAS_APPROVE_DOC_TYPE
                              where ap.APPROVE_DOC_TYPE == appdocType
                              select new DTO.ApproverDoctype
                              {
                                  APPROVE_DOC_TYPE = ap.APPROVE_DOC_TYPE,
                                  APPROVE_DOC_NAME = ap.APPROVE_DOC_NAME,
                                  APPROVER = ap.APPROVER,
                                  DESCRIPTION = ap.DESCRIPTION,
                                  CREATED_BY = ap.CREATED_BY,
                                  CREATED_DATE = ap.CREATED_DATE,
                                  UPDATED_BY = ap.UPDATED_BY,
                                  UPDATED_DATE = ap.UPDATED_DATE,
                                  ITEM_VALUE = ap.ITEM_VALUE,

                              }).ToList();

                if (result != null)
                {
                    res.DataResponse = result;

                }
                else
                {

                    res.DataResponse = null;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetApprocerByDocType", ex);
            }

            return res;

        }

        /// <summary>
        /// Get & Gen SEQ_NO
        /// </summary>
        /// <param name="uploadGroupNo"></param>
        /// <returns>DTO.ResponseService<List<DTO.ApproverDoctype>></returns>
        public DTO.ResponseService<List<DTO.PersonLicenseDetail>> GenSEQLicenseDetail(DTO.PersonLicenseHead uploadGroupNo)
        {
            var res = new DTO.ResponseService<List<DTO.PersonLicenseDetail>>();
            List<DTO.PersonLicenseDetail> ls = new List<PersonLicenseDetail>();

            try
            {
                Func<string, string> genSEQ = delegate(string index)
                {
                    if (index.Equals(""))
                    {
                        index += 1;
                    }
                    string x = index.Replace(index, "000" + index);
                    index = x;
                    return index;
                };

                var result = (from ld in base.ctx.AG_IAS_LICENSE_D
                              join lh in base.ctx.AG_IAS_LICENSE_H on ld.UPLOAD_GROUP_NO equals lh.UPLOAD_GROUP_NO
                              where ld.UPLOAD_GROUP_NO == uploadGroupNo.UPLOAD_GROUP_NO &&
                              ld.UPLOAD_GROUP_NO == lh.UPLOAD_GROUP_NO
                              select new DTO.PersonLicenseDetail
                              {
                                  UPLOAD_GROUP_NO = ld.UPLOAD_GROUP_NO,
                                  SEQ_NO = ld.SEQ_NO,
                                  ORDERS = ld.ORDERS,
                                  LICENSE_NO = ld.LICENSE_NO,
                                  LICENSE_DATE = ld.LICENSE_DATE,
                                  LICENSE_EXPIRE_DATE = ld.LICENSE_EXPIRE_DATE,
                                  FEES = ld.FEES,
                                  ID_CARD_NO = ld.ID_CARD_NO,
                                  RENEW_TIMES = ld.RENEW_TIMES,
                                  PRE_NAME_CODE = ld.PRE_NAME_CODE,
                                  TITLE_NAME = ld.TITLE_NAME,
                                  NAMES = ld.NAMES,

                              }).ToList();

                if (result != null)
                {
                    DTO.PersonLicenseDetail ent = new PersonLicenseDetail();

                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            ent.UPLOAD_GROUP_NO = result[i].UPLOAD_GROUP_NO;
                            ent.SEQ_NO = genSEQ(result[i].SEQ_NO);
                        }
                        ls.Add(ent);
                    }
                    else if (result.Count == 0)
                    {
                        ent.UPLOAD_GROUP_NO = "";
                        ent.SEQ_NO = genSEQ("");
                        ls.Add(ent);
                    }

                }

                res.DataResponse = ls;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GenSEQLicenseDetail", ex);
            }
            return res;

        }

        /// <summary>
        /// GetLicenseTransaction
        /// </summary>
        /// <param name="head">List<DTO.PersonLicenseHead></param>
        /// <param name="detail">List<DTO.PersonLicenseDetail></param>
        /// <returns>DTO.ResponseService<List<DTO.PersonLicenseTransaction>></returns>
        public DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetLicenseTransaction(List<DTO.PersonLicenseHead> head, List<DTO.PersonLicenseDetail> detail)
        {

            var res = new ResponseService<List<DTO.PersonLicenseTransaction>>();
            List<DTO.PersonLicenseTransaction> ls = new List<DTO.PersonLicenseTransaction>();

            try
            {

                #region Func
                Func<string, string> ConverCodeToString = delegate(string code)
                {
                    if (code.Length.Equals(1))
                    {
                        string x = code.Replace(code, "0" + code);
                        code = x;
                    }
                    return code;

                };

                Func<string, string> ConvertLicense = delegate(string license)
                {

                    if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type01.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_032;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type02.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_033;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type03.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_034;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type04.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_035;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type05.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_036;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type06.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_037;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type07.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_038;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type08.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_039;
                    }
                    else
                    {
                        license = Resources.propLicenseService_040;
                    }
                    return license;
                };

                Func<string, string> ConvertPettion = delegate(string pettion)
                {
                    if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.NewLicense.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_041;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense1Y.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_042;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense5Y.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_043;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.ExpireRenewLicense.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_044;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.OtherLicense_1.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_045;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.MoveLicense.GetEnumValue()))))
                    {
                        pettion = "ใบอนุญาต(ย้ายบริษัท)";
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.SecondLicense.GetEnumValue()))))
                    {
                        pettion = "ใบอนุญาต(ใบอนุญาตใบที่ 2)";
                    }
                    else
                    {
                        pettion = Resources.propLicenseService_040;
                    }
                    return pettion;
                };


                Func<string, string> getHeadRequestNo = (input) =>
                {
                    if ((input != null) || (input != ""))
                    {
                        var resH = (from h in base.ctx.AG_IAS_SUBPAYMENT_H_T
                                    from d in this.ctx.AG_IAS_SUBPAYMENT_D_T
                                    where h.HEAD_REQUEST_NO == d.HEAD_REQUEST_NO &&
                                    d.UPLOAD_GROUP_NO == input
                                    select new DTO.PersonLicenseTransaction
                                    {
                                        HEAD_REQUEST_NO = d.HEAD_REQUEST_NO

                                    }).FirstOrDefault();

                        input = resH.HEAD_REQUEST_NO;
                    }

                    return input;
                };
                #endregion

                if (head != null)
                {
                    if (head.Count > 0)
                    {
                        for (int i = 0; i < head.Count; i++)
                        {
                            var result = detail.Where(groupno => groupno.UPLOAD_GROUP_NO == head[i].UPLOAD_GROUP_NO).ToList();

                            if (result != null)
                            {
                                for (int j = 0; j < result.Count(); j++)
                                {
                                    DTO.PersonLicenseTransaction ent = new DTO.PersonLicenseTransaction();
                                    ent.UPLOAD_GROUP_NO = result[j].UPLOAD_GROUP_NO;
                                    ent.COMP_CODE = head[i].COMP_CODE;
                                    ent.COMP_NAME = head[i].COMP_NAME;
                                    ent.LICENSE_TYPE_CODE = ConvertLicense(head[i].LICENSE_TYPE_CODE);
                                    ent.PETITION_TYPE_CODE = ConvertPettion(head[i].PETITION_TYPE_CODE);
                                    ent.LICENSE_NO = result[j].LICENSE_NO;
                                    ent.ID_CARD_NO = result[j].ID_CARD_NO;
                                    ent.RENEW_TIMES = result[j].RENEW_TIMES;

                                    //Add New For Payment
                                    ent.FEES = result[j].FEES;
                                    ent.MONEY = head[i].MONEY;
                                    ent.APPROVED_DOC = head[i].APPROVED_DOC;
                                    ent.TRAN_DATE = head[j].TRAN_DATE;

                                    ls.Add(ent);
                                }

                            }

                        }

                    }

                }


                res.DataResponse = ls;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseTransaction" + ":" + ex.Message, ex.Message);
            }
            return res;

        }

        /// <summary>
        /// Single License Validation with SP
        /// </summary>
        /// <param name="SP">IAS_CHECK_LICENSE_SINGLE_PRO</param>
        /// <param name="detail"></param>
        /// <returns>DTO.ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> SingleLicenseValidation(DTO.PersonLicenseHead head, DTO.PersonLicenseDetail detail)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            Func<string, string> chkNullble = delegate(string input)
            {
                if ((input == null) || (input == ""))
                {
                    input = "";
                }
                return input;

            };

            Func<string, string> chkreturnValue = delegate(string input)
            {
                //Success return  == null
                if (input != "null")
                {
                    res.ErrorMsg = input;
                    res.ResultMessage = false;
                }
                ////False return > !null.ToString();
                else
                {
                    res.ResultMessage = true;
                }
                return input;
            };

            //New License
            if (detail.LICENSE_NO == "")
            {
                string returnValue = string.Empty;
                using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
                {
                    OracleCommand objCmd = new OracleCommand();
                    objCmd.Connection = objConn;
                    objCmd.CommandText = "IAS_CHECK_LICENSE_SINGLE_PRO";
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("CITICEN_ID", OracleDbType.Varchar2).Value = chkNullble(detail.ID_CARD_NO);
                    objCmd.Parameters.Add("LICENSE_NO", OracleDbType.Varchar2).Value = "";
                    objCmd.Parameters.Add("OLD_COMP_CODE", OracleDbType.Varchar2).Value = chkNullble(detail.OLD_COMP_CODE);
                    objCmd.Parameters.Add("COMP_CODE", OracleDbType.Varchar2).Value = chkNullble(head.COMP_CODE);
                    objCmd.Parameters.Add("PETTITION_TYPE_CODE", OracleDbType.Varchar2).Value = chkNullble(head.PETITION_TYPE_CODE);
                    objCmd.Parameters.Add("LICENSE_TYPE_CODE", OracleDbType.Varchar2).Value = chkNullble(head.LICENSE_TYPE_CODE);

                    var errMess = new OracleParameter("V_ERR_MSG", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                    errMess.Size = 4000;
                    errMess.Value = "";

                    objCmd.Parameters.Add(errMess);

                    try
                    {
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        chkreturnValue(objCmd.Parameters["V_ERR_MSG"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                        LoggerFactory.CreateLog().Fatal("LicenseService_SingleLicenseValidation", ex);
                    }
                    objConn.Close();
                }
            }
            //Other License
            else
            {
                string returnValue = string.Empty;
                using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
                {
                    OracleCommand objCmd = new OracleCommand();
                    objCmd.Connection = objConn;
                    objCmd.CommandText = "IAS_CHECK_LICENSE_SINGLE_PRO";
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("CITICEN_ID", OracleDbType.Varchar2).Value = chkNullble(detail.ID_CARD_NO);
                    objCmd.Parameters.Add("LICENSE_NO", OracleDbType.Varchar2).Value = chkNullble(detail.LICENSE_NO);
                    objCmd.Parameters.Add("OLD_COMP_CODE", OracleDbType.Varchar2).Value = chkNullble(detail.OLD_COMP_CODE);
                    objCmd.Parameters.Add("COMP_CODE", OracleDbType.Varchar2).Value = chkNullble(head.COMP_CODE);
                    objCmd.Parameters.Add("PETTITION_TYPE_CODE", OracleDbType.Varchar2).Value = chkNullble(head.PETITION_TYPE_CODE);
                    objCmd.Parameters.Add("LICENSE_TYPE_CODE", OracleDbType.Varchar2).Value = chkNullble(head.LICENSE_TYPE_CODE);

                    var errMess = new OracleParameter("V_ERR_MSG", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                    errMess.Size = 4000;
                    errMess.Value = "";

                    objCmd.Parameters.Add(errMess);

                    try
                    {
                        objConn.Open();
                        objCmd.ExecuteNonQuery();
                        chkreturnValue(objCmd.Parameters["V_ERR_MSG"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                        LoggerFactory.CreateLog().Fatal("LicenseService_SingleLicenseValidation" + ":" + ex.Message, ex.Message);
                    }
                    objConn.Close();
                }

            }

            //check License wait  9/12/2556
            if (res.IsError == false)
            {
                var duplicense = (from lh in ctx.AG_IAS_LICENSE_H
                                  join ld in ctx.AG_IAS_LICENSE_D on lh.UPLOAD_GROUP_NO equals ld.UPLOAD_GROUP_NO
                                  join lt in ctx.AG_IAS_LICENSE_TYPE_R on lh.LICENSE_TYPE_CODE equals lt.LICENSE_TYPE_CODE
                                  where lh.LICENSE_TYPE_CODE == head.LICENSE_TYPE_CODE
                                  && ld.ID_CARD_NO == detail.ID_CARD_NO
                                  && lh.APPROVED_DOC == "W"
                                  select new DTO.DataItem
                                  {
                                      Name = lt.LICENSE_TYPE_NAME
                                  }).ToList();

                if (duplicense != null && duplicense.Count != 0 && !string.IsNullOrEmpty(duplicense[0].Name))
                {
                    chkreturnValue(Resources.infoLicenseService_001 + " '" + duplicense[0].Name + "' " + Resources.infoLicenseService_002);
                }

            }



            return res;

        }


        /// <summary>
        /// GetPaymentLicenseTransaction
        /// </summary>
        /// <param name="head">List<DTO.PersonLicenseHead></param>
        /// <param name="detail">List<DTO.PersonLicenseDetail></param>
        /// <returns>DTO.ResponseService<List<DTO.PersonLicenseTransaction>></returns>
        public DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetPaymentLicenseTransaction(List<DTO.PersonLicenseHead> head, List<DTO.PersonLicenseDetail> detail)
        {
            var res = new ResponseService<List<DTO.PersonLicenseTransaction>>();
            List<DTO.PersonLicenseTransaction> ls = new List<DTO.PersonLicenseTransaction>();

            try
            {
                Func<string, string> ConverCodeToString = delegate(string code)
                {
                    if (code.Length.Equals(1))
                    {
                        string x = code.Replace(code, "0" + code);
                        code = x;
                    }
                    return code;

                };

                Func<string, string> ConvertLicense = delegate(string license)
                {

                    if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type01.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_032;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type02.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_033;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type03.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_034;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type04.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_035;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type05.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_036;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type06.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_037;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type07.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_038;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type08.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_039;
                    }
                    else
                    {
                        license = Resources.propLicenseService_040;
                    }
                    return license;
                };

                Func<string, string> ConvertPettion = delegate(string pettion)
                {
                    if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.NewLicense.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_041;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense1Y.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_042;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense5Y.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_043;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.ExpireRenewLicense.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_044;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.OtherLicense_1.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_045;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.MoveLicense.GetEnumValue()))))
                    {
                        pettion = "ใบอนุญาต(ย้ายบริษัท)";
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.SecondLicense.GetEnumValue()))))
                    {
                        pettion = "ใบอนุญาต(ใบอนุญาตใบที่ 2)";
                    }
                    else
                    {
                        pettion = Resources.propLicenseService_040;
                    }
                    return pettion;
                };


                Func<string, string> getHeadRequestNo = (input) =>
                {
                    if ((input != null) || (input != ""))
                    {
                        var resH = (from h in base.ctx.AG_IAS_SUBPAYMENT_H_T
                                    from d in this.ctx.AG_IAS_SUBPAYMENT_D_T
                                    where h.HEAD_REQUEST_NO == d.HEAD_REQUEST_NO &&
                                    d.UPLOAD_GROUP_NO == input
                                    select new DTO.PersonLicenseTransaction
                                    {
                                        HEAD_REQUEST_NO = d.HEAD_REQUEST_NO

                                    }).FirstOrDefault();
                        if (resH != null)
                        {
                            input = resH.HEAD_REQUEST_NO;
                        }
                        else
                        {
                            input = "";
                        }

                    }

                    return input;
                };

                Func<string, string> ConvertApproveDoc = (input) =>
                    {
                        if ((input != null) || (input != ""))
                        {
                            if (input.Equals("W"))
                            {
                                input = Resources.propLicenseService_046;
                            }
                            else if (input.Equals("N"))
                            {
                                input = Resources.propLicenseService_047;
                            }
                            else if (input.Equals("Y"))
                            {
                                input = Resources.propLicenseService_048;
                            }
                        }
                        return input;
                    };

                if (head != null)
                {
                    if (head.Count > 0)
                    {
                        for (int i = 0; i < head.Count; i++)
                        {
                            var result = detail.Where(groupno => groupno.UPLOAD_GROUP_NO == head[i].UPLOAD_GROUP_NO).ToList();

                            if (result != null)
                            {
                                for (int j = 0; j < result.Count(); j++)
                                {
                                    DTO.PersonLicenseTransaction ent = new DTO.PersonLicenseTransaction();

                                    //Check PetitionType if 15 is ExpiredRenew
                                    if (head[i].PETITION_TYPE_CODE == Convert.ToString((int)DTO.PettionCode.ExpireRenewLicense))
                                    {

                                        ent.UPLOAD_GROUP_NO = result[j].UPLOAD_GROUP_NO;
                                        ent.COMP_CODE = head[i].COMP_CODE;
                                        ent.COMP_NAME = head[i].COMP_NAME;
                                        ent.LICENSE_TYPE_CODE = ConvertLicense(head[i].LICENSE_TYPE_CODE);
                                        ent.PETITION_TYPE_CODE = ConvertPettion(head[i].PETITION_TYPE_CODE);
                                        ent.LICENSE_NO = result[j].LICENSE_NO;
                                        ent.ID_CARD_NO = result[j].ID_CARD_NO;
                                        ent.RENEW_TIMES = result[j].RENEW_TIMES;
                                        ent.APPROVED = result[j].APPROVED;

                                        //Add New For Payment
                                        ent.FEES = result[j].FEES;
                                        ent.MONEY = head[i].MONEY;
                                        ent.APPROVED_DOC = ConvertApproveDoc(head[i].APPROVED_DOC);
                                        ent.TRAN_DATE = head[j].TRAN_DATE;

                                        ent.HEAD_REQUEST_NO = "";

                                        ls.Add(ent);

                                    }
                                    else
                                    {
                                        ent.UPLOAD_GROUP_NO = result[j].UPLOAD_GROUP_NO;
                                        ent.COMP_CODE = head[i].COMP_CODE;
                                        ent.COMP_NAME = head[i].COMP_NAME;
                                        ent.LICENSE_TYPE_CODE = ConvertLicense(head[i].LICENSE_TYPE_CODE);
                                        ent.PETITION_TYPE_CODE = ConvertPettion(head[i].PETITION_TYPE_CODE);
                                        ent.LICENSE_NO = result[j].LICENSE_NO;
                                        ent.ID_CARD_NO = result[j].ID_CARD_NO;
                                        ent.RENEW_TIMES = result[j].RENEW_TIMES;

                                        //Add New For Payment
                                        ent.FEES = result[j].FEES;
                                        ent.MONEY = head[i].MONEY;
                                        ent.APPROVED = result[j].APPROVED;
                                        ent.APPROVED_DOC = ConvertApproveDoc(head[i].APPROVED_DOC);
                                        ent.TRAN_DATE = head[j].TRAN_DATE;

                                        ent.HEAD_REQUEST_NO = getHeadRequestNo(result[j].UPLOAD_GROUP_NO);

                                        ls.Add(ent);
                                    }

                                }

                            }

                        }

                    }

                }


                res.DataResponse = ls;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetPaymentLicenseTransaction" + ":" + ex.Message, ex.Message);
            }
            return res;
        }


        /// <summary>
        /// GetRequesPersontLicenseByIdCard
        /// </summary>
        /// <param name="uploadGroupNo"></param>
        /// <returns>DTO.ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetRequesPersontLicenseByIdCard(string idCard)
        {
            //DateTime.Now > (L.EXPIRE_DATE + 60)
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = " SELECT L.LICENSE_NO, LR.LICENSE_TYPE_NAME, " +
                             " L.RENEW_DATE, L.EXPIRE_DATE, R.REVOKE_TYPE_NAME, L.REVOKE_LICENSE_DATE,RENEW_TIME,PAYMENT_NO  " +

                             " FROM (SELECT  LT.PAYMENT_NO, LT.LICENSE_NO, LT.RENEW_TIME, " +
                             " LT.RENEW_DATE, LT.EXPIRE_DATE, LT.ID_CARD_NO, T.REVOKE_TYPE_CODE,  T.REVOKE_LICENSE_DATE, " +
                             " LT.LICENSE_TYPE_CODE " +

                             " FROM (SELECT	RT.PAYMENT_NO, RT.LICENSE_NO, RT.RENEW_TIME, " +
                             " RT.RENEW_DATE, RT.EXPIRE_DATE, LC.ID_CARD_NO, ALT.LICENSE_TYPE_CODE " +

                             " FROM	AG_LICENSE_RENEW_T	RT, " +
                             " AG_LICENSE_T ALT, " +

                             " (SELECT LICENSE_NO, ID_CARD_NO " +

                             " FROM AG_AGENT_LICENSE_PERSON_T " +




                             " UNION SELECT LICENSE_NO, ID_CARD_NO " +

                             " FROM AG_AGENT_LICENSE_T) LC " +

                             " WHERE RT.LICENSE_NO = LC.LICENSE_NO AND " +
                             " RT.LICENSE_NO = ALT.LICENSE_NO AND " +
                             " LC.ID_CARD_NO = '" + idCard.ClearQuote() + "') LT " +
                             " , AG_LICENSE_T T " +
                             " WHERE LT.LICENSE_NO = T.LICENSE_NO) L, AG_REVOKE_TYPE_R R, AG_LICENSE_TYPE_R LR " +
                             " WHERE L.REVOKE_TYPE_CODE = R.REVOKE_TYPE_CODE AND L.LICENSE_TYPE_CODE = LR.LICENSE_TYPE_CODE ";


                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetRequesPersontLicenseByIdCard" + ":" + ex.Message, ex.Message);
            }
            return res;
        }

        /// <summary>
        /// GetRenewLicneseByIdCard // ต่ออายุก่อนหมดอายุ60วัน
        /// </summary> Last edit added > AG_LICENSE_T && AG_AGENT_LICENSE_T
        /// <param name="uploadGroupNo"></param>
        /// <returns>DTO.ResponseService<DTO.PersonLicenseTransaction></returns>
        public DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetRenewLicneseByIdCard(string idCard)
        {
            var res = new DTO.ResponseService<List<DTO.PersonLicenseTransaction>>();
            try
            {
                #region Func
                Func<string, string> dateCompare = delegate(string input)
                {
                    if (input != null)
                    {
                        DateTime expDate = Convert.ToDateTime(input);
                        DateTime curDate = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", DateTime.Now));
                        TimeSpan diff = expDate - curDate;
                        int diffday = diff.TotalDays.ToInt();

                        if (diffday <= 60)
                        {
                            input = Convert.ToString(diffday);
                        }
                        else
                        {
                            int output = diff.Days;
                            input = Convert.ToString(output);
                        }
                    }
                    return input;
                };

                Func<int, string> convertRenewtime = delegate(int input)
                {
                    if (input != null)
                    {
                        return Convert.ToString(input);

                    }

                    return Convert.ToString(input);
                };

                Func<string, string, string> getPTName = delegate(string pettitionCode, string renewTime)
                {
                    if ((pettitionCode != null) && (pettitionCode != ""))
                    {
                        int renewtimes = Convert.ToInt32(renewTime) + 1;
                        if (pettitionCode.Equals("13"))
                        {
                            pettitionCode = Resources.propLicenseService_049 + Resources.propLicenseService_050 + renewtimes;
                        }
                        else if (pettitionCode.Equals("14"))
                        {
                            pettitionCode = Resources.propLicenseService_052 + Resources.propLicenseService_050 + renewtimes;
                        }
                        else
                        {
                            pettitionCode = Resources.propLicenseService_051 + Resources.propLicenseService_050 + renewtimes;
                        }
                    }
                    else
                    {
                        pettitionCode = Resources.propLicenseService_053;
                    }
                    return pettitionCode;
                };

                Func<decimal, decimal> getFee = delegate(decimal input)
                {
                    if (input != null)
                    {
                        string inputFilter = Convert.ToString(input);
                        Nullable<decimal> petitionFee = base.ctx.AG_PETITION_TYPE_R.FirstOrDefault(type => type.PETITION_TYPE_CODE == inputFilter).FEE;
                        if (petitionFee != null)
                        {
                            input = (decimal)petitionFee;
                        }
                        else
                        {
                            input = 0;
                        }
                    }
                    return input;
                };

                Func<string, string> GetLicenseName = delegate(string input)
                {
                    if ((input != null) && (input != ""))
                    {
                        input = base.ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(id => id.LICENSE_TYPE_CODE == input).LICENSE_TYPE_NAME;

                    }

                    return input;
                };

                #endregion

                //Check Old License First
                var oldLicense = (from A in base.ctx.AG_AGENT_LICENSE_T
                                  where A.ID_CARD_NO == idCard
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = A.ID_CARD_NO
                                  }).Union(from B in base.ctx.AG_AGENT_LICENSE_PERSON_T
                                           where B.ID_CARD_NO == idCard
                                           select new DTO.PersonLicenseTransaction
                                           {
                                               LICENSE_NO = B.LICENSE_NO,
                                               ID_CARD_NO = B.ID_CARD_NO
                                           }).ToList();

                //New License
                #region License from New System
                if ((oldLicense.Count == 0) && (oldLicense != null))
                {
                    DateTime currentDate2 = DateTime.Now;
                    var result = (from h in base.ctx.AG_IAS_LICENSE_H
                                  join d in this.ctx.AG_IAS_LICENSE_D on h.UPLOAD_GROUP_NO equals d.UPLOAD_GROUP_NO
                                  where d.ID_CARD_NO == idCard &&
                                  d.LICENSE_NO != null &&
                                  d.APPROVED == "Y" &&
                                  h.APPROVED_DOC == "Y"
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
                                      UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                                      UPLOAD_BY_SESSION = h.UPLOAD_BY_SESSION,
                                      LICENSE_NO = d.LICENSE_NO,
                                      COMP_NAME = h.COMP_NAME,
                                      RENEW_TIMES = d.RENEW_TIMES,
                                      LICENSE_DATE = d.LICENSE_DATE,
                                      LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,
                                      FEES = d.FEES,
                                      LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
                                      PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
                                      APPROVED = d.APPROVED,
                                      APPROVED_DOC = h.APPROVED_DOC

                                  }).OrderByDescending(dt => dt.LICENSE_EXPIRE_DATE).ToList();

                    List<DTO.PersonLicenseTransaction> ls = new List<PersonLicenseTransaction>();
                    if (result.Count > 0)
                    {
                        result.ForEach(x =>
                        {
                            string strDate = x.LICENSE_EXPIRE_DATE.ToString();
                            string diffDate = dateCompare(strDate);
                            //if (System.Data.Objects.SqlClient.SqlFunctions.DateDiff("second", x.LICENSE_EXPIRE_DATE, DateTime.Now) >= 0 || (System.Data.Objects.SqlClient.SqlFunctions.DateDiff("second", x.LICENSE_EXPIRE_DATE, DateTime.Now) <= 60))
                            if ((Convert.ToInt32(diffDate) >= 0) && (Convert.ToInt32(diffDate) <= 60))
                            {
                                //RENEW_TIME Check 1Y || 5Y 
                                if (((Convert.ToInt32(x.RENEW_TIMES) + 1) >= 0) && ((Convert.ToInt32(x.RENEW_TIMES) + 1) < 3))
                                {
                                    x.PETITION_TYPE_CODE = Convert.ToString((int)DTO.PettionCode.RenewLicense1Y);
                                }
                                else if ((Convert.ToInt32(x.RENEW_TIMES) + 1) >= 3)
                                {
                                    x.PETITION_TYPE_CODE = Convert.ToString((int)DTO.PettionCode.RenewLicense5Y);
                                }
                                ls.Add(x);
                            }
                        });

                        //Convert Fee
                        ls.ForEach(f => f.FEES = getFee(Convert.ToDecimal(f.PETITION_TYPE_CODE)));
                        ls.ForEach(licname => licname.LICENSE_TYPE_NAME = GetLicenseName(licname.LICENSE_TYPE_CODE));
                        ls.ForEach(pettiname => pettiname.PETITION_TYPE_NAME = getPTName(pettiname.PETITION_TYPE_CODE, pettiname.RENEW_TIMES));
                    }

                    //Order by LICENSE_EXPIRE_DATE
                    var resls = ls.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();
                    //res.DataResponse = ls.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).Skip(0).Take(1).ToList();
                    res.DataResponse = resls.ToList();

                }
                #endregion
                //Old License
                #region License from Last System
                else if ((oldLicense.Count > 0) && (oldLicense != null))
                {
                    //Step 1 GET License_No from AG_AGENT_T == AG_LICENSE_T
                    var resAGT = (from A in base.ctx.AG_LICENSE_T
                                  join B in base.ctx.AG_AGENT_LICENSE_T on A.LICENSE_NO equals B.LICENSE_NO
                                  where B.ID_CARD_NO == idCard
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = B.ID_CARD_NO,
                                      INSURANCE_COMP_CODE = B.INSURANCE_COMP_CODE,
                                      LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE

                                  }).Union(from A in base.ctx.AG_LICENSE_T
                                           join B in base.ctx.AG_AGENT_LICENSE_PERSON_T on A.LICENSE_NO equals B.LICENSE_NO
                                           where B.ID_CARD_NO == idCard
                                           select new DTO.PersonLicenseTransaction
                                           {
                                               LICENSE_NO = A.LICENSE_NO,
                                               ID_CARD_NO = B.ID_CARD_NO,
                                               INSURANCE_COMP_CODE = "-",
                                               LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE
                                           }).ToList();

                    //Step 2 Check License_No within AG_LICENSE_RENEW_T
                    List<DTO.PersonLicenseTransaction> lsAGLT = new List<PersonLicenseTransaction>();
                    if (resAGT != null)
                    {
                        List<string> licensels = new List<string>();
                        for (int i = 0; i < resAGT.Count; i++)
                        {
                            licensels.Add(resAGT[i].LICENSE_NO);
                        }

                        //Get Max by LICENSE_NO
                        List<AG_LICENSE_RENEW_T> getRenewT = base.ctx.AG_LICENSE_RENEW_T.Where(xx => licensels.Contains(xx.LICENSE_NO)).OrderByDescending(renew => renew.RENEW_TIME).ToList();
                        var filteredRenewT = getRenewT.GroupBy(s => s.LICENSE_NO).Select(group => group.First());

                        var getls = (from A in filteredRenewT.ToList()
                                     from B in resAGT
                                     where A.LICENSE_NO == B.LICENSE_NO
                                     select new DTO.PersonLicenseTransaction
                                     {
                                         LICENSE_NO = A.LICENSE_NO,
                                         RENEW_TIME = A.RENEW_TIME,
                                         LICENSE_DATE = A.RENEW_DATE,
                                         LICENSE_EXPIRE_DATE = A.EXPIRE_DATE,
                                         LICENSE_TYPE_CODE = B.LICENSE_TYPE_CODE,
                                         LICENSE_TYPE_NAME = B.LICENSE_TYPE_NAME,
                                         INSURANCE_COMP_CODE = B.INSURANCE_COMP_CODE,
                                         UPLOAD_BY_SESSION = A.UPLOAD_BY_SESSION,
                                         COMP_NAME = "-",
                                         FEES = 0,
                                         PETITION_TYPE_NAME = B.PETITION_TYPE_NAME,
                                         PETITION_TYPE_CODE = A.PETITION_TYPE_CODE,
                                         APPROVED = A.APPROVE_FLAG,
                                         APPROVED_DOC = A.APPROVE_DOC_TYPE

                                     }).OrderByDescending(order => order.RENEW_TIME).ToList();

                        getls.ForEach(x =>
                        {
                            //string strDate = x.EXPIRE_DATE.ToString();
                            string strDate = x.LICENSE_EXPIRE_DATE.ToString();
                            string diffDate = dateCompare(strDate);
                            //if (System.Data.Objects.SqlClient.SqlFunctions.DateDiff("second", x.LICENSE_EXPIRE_DATE, DateTime.Now) >= 0 || (System.Data.Objects.SqlClient.SqlFunctions.DateDiff("second", x.LICENSE_EXPIRE_DATE, DateTime.Now) <= 60))
                            if ((Convert.ToInt32(diffDate) >= 0) && (Convert.ToInt32(diffDate) <= 60))
                            {
                                //RENEW_TIME Check 1Y || 5Y 
                                if (((Convert.ToInt32(x.RENEW_TIME) + 1) >= 0) && ((Convert.ToInt32(x.RENEW_TIME) + 1) < 3))
                                {
                                    x.PETITION_TYPE_CODE = Convert.ToString((int)DTO.PettionCode.RenewLicense1Y);
                                }
                                else if ((Convert.ToInt32(x.RENEW_TIME) + 1) >= 3)
                                {
                                    x.PETITION_TYPE_CODE = Convert.ToString((int)DTO.PettionCode.RenewLicense5Y);
                                }
                                lsAGLT.Add(x);
                            }
                        });

                        //Convert Fee
                        lsAGLT.ForEach(f => f.FEES = getFee(Convert.ToDecimal(f.PETITION_TYPE_CODE)));
                        lsAGLT.ForEach(licname => licname.LICENSE_TYPE_NAME = GetLicenseName(licname.LICENSE_TYPE_CODE));
                        lsAGLT.ForEach(pettiname => pettiname.PETITION_TYPE_NAME = getPTName(pettiname.PETITION_TYPE_CODE, Convert.ToString(pettiname.RENEW_TIME)));

                    }

                    //Step 3 Validate license within AG_LICENSE_T > renew by type
                    var resls = lsAGLT.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();

                    List<DTO.PersonLicenseTransaction> restype = new List<PersonLicenseTransaction>();
                    var resIn = (from A in base.ctx.VW_IAS_COM_CODE
                                 select new DTO.InsuranceAssociate
                                 {
                                     Id = A.ID,
                                     Name = A.NAME

                                 }).ToList();

                    if (resls.Count > 0)
                    {

                        resls.ForEach(f =>
                        {
                            f.RENEW_TIMES = convertRenewtime(f.RENEW_TIME);
                            var getName = resIn.FirstOrDefault(id => id.Id == f.INSURANCE_COMP_CODE);
                            if (getName != null)
                            {
                                f.COMP_NAME = getName.Name;
                            }

                        });
                    }

                    res.DataResponse = resls.ToList();

                }
                #endregion



            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_restype" + ":" + ex.Message, ex.Message);
            }

            return res;
        }

        /// <summary>
        /// GetExpiredLicneseByIdCard // หมดอายุขอต่อใหม่
        /// </summary>
        /// <param name="uploadGroupNo"></param>
        /// <returns></returns>
        /// <AUTHOR>Natta</AUTHOR>
        /// <LASTUPDATE>27/05/2557</LASTUPDATE>
        public DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetExpiredLicneseByIdCard(string idCard)
        {
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            var res = new DTO.ResponseService<List<DTO.PersonLicenseTransaction>>();
            try
            {
                #region Func
                DateTime currentDate = DateTime.Now;
                Func<string, string> dateCompare = delegate(string input)
                {
                    if (input != null)
                    {
                        DateTime expDate = Convert.ToDateTime(input);
                        DateTime curDate = DateTime.Now;
                        TimeSpan diff = expDate - curDate;
                        int diffday = DateTime.Compare(expDate, curDate);

                        input = Convert.ToString(diffday);
                    }
                    return input;
                };

                Func<string, string> ConverCodeToString = delegate(string code)
                {
                    if (code.Length.Equals(1))
                    {
                        string x = code.Replace(code, "0" + code);
                        code = x;
                    }
                    return code;

                };

                Func<int, string> convertRenewtime = delegate(int input)
                {
                    if (input != null)
                    {
                        return Convert.ToString(input);

                    }

                    return Convert.ToString(input);
                };

                Func<string, string, string> getPTName = delegate(string pettitionCode, string renewTime)
                {
                    if ((pettitionCode != null) && (pettitionCode != ""))
                    {
                        int renewtimes = Convert.ToInt32(renewTime) + 1;
                        if (pettitionCode.Equals("13"))
                        {
                            pettitionCode = Resources.propLicenseService_049 + Resources.propLicenseService_050 + renewtimes;
                        }
                        else if (pettitionCode.Equals("14"))
                        {
                            pettitionCode = Resources.propLicenseService_052 + Resources.propLicenseService_050 + renewtimes;
                        }
                        else
                        {
                            pettitionCode = Resources.propLicenseService_051 + Resources.propLicenseService_050 + renewtimes;
                        }
                    }
                    else
                    {
                        pettitionCode = Resources.propLicenseService_053;
                    }
                    return pettitionCode;
                };

                Func<string, string> getLCName = delegate(string input)
                {
                    if ((input != null) && (input != ""))
                    {
                        input = base.ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(id => id.LICENSE_TYPE_CODE == input).LICENSE_TYPE_NAME;

                    }

                    return input;
                };
                Func<decimal, decimal> getFee = delegate(decimal input)
                {
                    if (input != null)
                    {
                        string inputFilter = Convert.ToString(input);
                        Nullable<decimal> petitionFee = base.ctx.AG_PETITION_TYPE_R.FirstOrDefault(type => type.PETITION_TYPE_CODE == inputFilter).FEE;
                        if (petitionFee != null)
                        {
                            input = (decimal)petitionFee;
                        }
                        else
                        {
                            input = 0;
                        }
                    }
                    return input;
                };

                #endregion

                //Check New License First
                //Check Old License
                var oldLicense = (from A in base.ctx.AG_AGENT_LICENSE_T
                                  from al in ctx.AG_LICENSE_T
                                  where A.ID_CARD_NO == idCard
                                  && al.LICENSE_NO == A.LICENSE_NO
                                  && al.REVOKE_LICENSE_DATE == null
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = A.ID_CARD_NO
                                  }).Union(from B in base.ctx.AG_AGENT_LICENSE_PERSON_T
                                           from al in ctx.AG_LICENSE_T
                                           where B.ID_CARD_NO == idCard
                                           && B.LICENSE_NO == al.LICENSE_NO
                                           && al.REVOKE_LICENSE_DATE == null
                                           select new DTO.PersonLicenseTransaction
                                           {
                                               LICENSE_NO = B.LICENSE_NO,
                                               ID_CARD_NO = B.ID_CARD_NO
                                           }).ToList();

                //New License
                #region License from New system
                if ((oldLicense.Count == 0) && (oldLicense != null))
                {
                    var result = (from h in base.ctx.AG_IAS_LICENSE_H
                                  from d in this.ctx.AG_IAS_LICENSE_D
                                  where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
                                  d.ID_CARD_NO == idCard &&
                                  d.LICENSE_NO != null &&
                                  d.APPROVED == "Y" &&
                                  h.APPROVED_DOC == "Y"
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
                                      UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                                      UPLOAD_BY_SESSION = h.UPLOAD_BY_SESSION,
                                      LICENSE_NO = d.LICENSE_NO,
                                      COMP_NAME = h.COMP_NAME,
                                      RENEW_TIMES = d.RENEW_TIMES,
                                      LICENSE_DATE = d.LICENSE_DATE,
                                      LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,
                                      FEES = d.FEES,
                                      LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
                                      PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,

                                  }).OrderByDescending(dt => dt.LICENSE_EXPIRE_DATE).ToList();


                    //Get License & PettitionName
                    //result.ForEach(ln => ln.LICENSE_TYPE_NAME = getLCName(ln.LICENSE_TYPE_CODE));
                    //result.ForEach(pn => pn.PETITION_TYPE_NAME = getPTName(pn.PETITION_TYPE_CODE, pn.RENEW_TIMES));

                    //Get All License Type
                    List<DTO.PersonLicenseTransaction> ls = new List<PersonLicenseTransaction>();
                    if (result.Count > 0)
                    {
                        result.ForEach(x =>
                            {
                                string strDate = x.LICENSE_EXPIRE_DATE.ToString();
                                string diffDate = dateCompare(strDate);
                                if (!diffDate.Equals("1"))
                                {
                                    ls.Add(x);
                                }

                            });

                        //Convert Fee
                        ls.ForEach(f => f.FEES = getFee(Convert.ToDecimal(f.PETITION_TYPE_CODE)));
                        ls.ForEach(licname => licname.LICENSE_TYPE_NAME = getLCName(licname.LICENSE_TYPE_CODE));
                        ls.ForEach(pettiname => pettiname.PETITION_TYPE_NAME = getPTName(pettiname.PETITION_TYPE_CODE, pettiname.RENEW_TIMES));
                    }

                    //Order by LICENSE_EXPIRE_DATE
                    var resls = ls.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();
                    //if (resls.Count > 0)
                    //{
                    //    resls.ForEach(f =>
                    //    {
                    //        f.RENEW_TIMES = convertRenewtime(f.RENEW_TIME);

                    //    });
                    //}
                    res.DataResponse = resls.ToList();
                }
                #endregion

                #region License from Last system
                //Old License
                else if ((oldLicense.Count > 0) && (oldLicense != null))
                {
                    //Step 1 GET License_No from AG_AGENT_T == AG_LICENSE_T
                    var resAGT = (from A in base.ctx.AG_LICENSE_T
                                  join B in base.ctx.AG_AGENT_LICENSE_T on A.LICENSE_NO equals B.LICENSE_NO
                                  where B.ID_CARD_NO == idCard
                                  && A.REVOKE_LICENSE_DATE == null
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = B.ID_CARD_NO,
                                      INSURANCE_COMP_CODE = B.INSURANCE_COMP_CODE,
                                      LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE

                                  }).Union(from A in base.ctx.AG_LICENSE_T
                                           join B in base.ctx.AG_AGENT_LICENSE_PERSON_T on A.LICENSE_NO equals B.LICENSE_NO
                                           where B.ID_CARD_NO == idCard
                                           && A.REVOKE_LICENSE_DATE == null
                                           select new DTO.PersonLicenseTransaction
                                           {
                                               LICENSE_NO = A.LICENSE_NO,
                                               ID_CARD_NO = B.ID_CARD_NO,
                                               INSURANCE_COMP_CODE = "-",
                                               LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE

                                           }).ToList();

                    //Step 2 Check License_No within AG_LICENSE_RENEW_T where LICENSE_TYPE_CODE = "03" && LICENSE_TYPE_CODE = "04"
                    //Expired Renew License was allow only LICENSE_TYPE_CODE = "03" && LICENSE_TYPE_CODE = "04"
                    List<DTO.PersonLicenseTransaction> lsAGLT = new List<PersonLicenseTransaction>();
                    if (resAGT != null)
                    {
                        List<string> licensels = new List<string>();
                        for (int i = 0; i < resAGT.Count; i++)
                        {
                            licensels.Add(resAGT[i].LICENSE_NO);
                        }

                        IQueryable<AG_LICENSE_RENEW_T> getRenewT = base.ctx.AG_LICENSE_RENEW_T.Where(o => licensels.Contains(o.LICENSE_NO)).OrderByDescending(order => order.RENEW_TIME).Distinct();

                        //Get Max
                        IQueryable<AG_LICENSE_RENEW_T> filteredRenewT = getRenewT.Where(m => m.RENEW_TIME == getRenewT.Max(x => x.RENEW_TIME));
                        var getls = (from A in filteredRenewT.ToList()
                                     from B in resAGT
                                     where A.LICENSE_NO == B.LICENSE_NO
                                     select new DTO.PersonLicenseTransaction
                                     {
                                         LICENSE_NO = A.LICENSE_NO,
                                         RENEW_TIME = A.RENEW_TIME,
                                         LICENSE_DATE = A.RENEW_DATE,
                                         LICENSE_EXPIRE_DATE = A.EXPIRE_DATE,
                                         LICENSE_TYPE_CODE = B.LICENSE_TYPE_CODE,
                                         LICENSE_TYPE_NAME = B.LICENSE_TYPE_NAME,
                                         INSURANCE_COMP_CODE = B.INSURANCE_COMP_CODE,
                                         UPLOAD_BY_SESSION = A.UPLOAD_BY_SESSION,
                                         COMP_NAME = "-",
                                         FEES = 0,
                                         PETITION_TYPE_NAME = B.PETITION_TYPE_NAME,
                                         PETITION_TYPE_CODE = A.PETITION_TYPE_CODE,
                                         APPROVED = A.APPROVE_FLAG,
                                         APPROVED_DOC = A.APPROVE_DOC_TYPE

                                     }).OrderByDescending(order => order.RENEW_TIME).ToList();

                        getls.ForEach(x =>
                            {
                                string strDate = x.LICENSE_EXPIRE_DATE.ToString();
                                string diffDate = dateCompare(strDate);
                                if (!diffDate.Equals("1"))
                                {
                                    x.PETITION_TYPE_CODE = Convert.ToString((int)DTO.PettionCode.ExpireRenewLicense);
                                    lsAGLT.Add(x);
                                }

                            });

                        //Convert Fee
                        lsAGLT.ForEach(f => f.FEES = getFee(Convert.ToDecimal(f.PETITION_TYPE_CODE)));
                        lsAGLT.ForEach(licname => licname.LICENSE_TYPE_NAME = getLCName(licname.LICENSE_TYPE_CODE));
                        lsAGLT.ForEach(pettiname => pettiname.PETITION_TYPE_NAME = getPTName(pettiname.PETITION_TYPE_CODE, Convert.ToString(pettiname.RENEW_TIME)));

                    }

                    //Step 3 Validate license within AG_LICENSE_T > renew by type
                    var resls = lsAGLT.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();
                    var resIn = (from A in base.ctx.VW_IAS_COM_CODE
                                 select new DTO.InsuranceAssociate
                                 {
                                     Id = A.ID,
                                     Name = A.NAME

                                 }).ToList();

                    if (resls.Count > 0)
                    {
                        resls.ForEach(f =>
                        {
                            f.RENEW_TIMES = convertRenewtime(f.RENEW_TIME);
                            var getName = resIn.FirstOrDefault(id => id.Id == f.INSURANCE_COMP_CODE);
                            if (getName != null)
                            {
                                f.COMP_NAME = getName.Name;
                            }
                        });
                    }

                    res.DataResponse = resls.ToList();
                }
                #endregion


            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetExpiredLicneseByIdCard" + ":" + ex.Message, ex.Message);
            }

            //sw.Stop();
            //TimeSpan sp = sw.Elapsed;
            //TimeSpan duration = sp.Duration();
            return res;

        }

        /// <summary>
        /// GetRenewLiceneEntityByLicenseNo
        /// </summary>
        /// <param name="uploadGroupNo"></param>
        /// <returns></returns>
        public DTO.ResponseService<DTO.PersonLicenseTransaction> GetRenewLiceneEntityByLicenseNo(string licenseNo)
        {
            var res = new DTO.ResponseService<DTO.PersonLicenseTransaction>();
            try
            {
                //Check Old License
                var old1 = (from A in base.ctx.AG_AGENT_LICENSE_T
                            where A.LICENSE_NO == licenseNo
                            select new DTO.PersonLicenseTransaction
                            {
                                LICENSE_NO = A.LICENSE_NO,
                                ID_CARD_NO = A.ID_CARD_NO
                            }).ToList();

                var old2 = (from B in base.ctx.AG_AGENT_LICENSE_PERSON_T
                            where B.LICENSE_NO == licenseNo
                            select new DTO.PersonLicenseTransaction
                            {
                                LICENSE_NO = B.LICENSE_NO,
                                ID_CARD_NO = B.ID_CARD_NO
                            }).ToList();

                var oldLicense = old1.Union(old2).ToList();

                //AG_AGENT_LICENSE_T oldlicense = base.ctx.AG_AGENT_LICENSE_T.FirstOrDefault(lino => lino.LICENSE_NO == licenseNo);


                //New License
                if ((oldLicense.Count == 0) && (oldLicense != null))
                {
                    var result = (from h in base.ctx.AG_IAS_LICENSE_H
                                  from d in this.ctx.AG_IAS_LICENSE_D
                                  from l in this.ctx.AG_IAS_LICENSE_TYPE_R
                                  from p in this.ctx.AG_IAS_PETITION_TYPE_R
                                  where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
                                  d.LICENSE_NO == licenseNo &&
                                  h.LICENSE_TYPE_CODE == l.LICENSE_TYPE_CODE &&
                                  h.PETITION_TYPE_CODE == p.PETITION_TYPE_CODE &&
                                  d.LICENSE_NO != null &&
                                  d.APPROVED == "Y" &&
                                  h.APPROVED_DOC == "Y"
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
                                      UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                                      UPLOAD_BY_SESSION = h.UPLOAD_BY_SESSION,
                                      LICENSE_NO = d.LICENSE_NO,
                                      COMP_CODE = h.COMP_CODE,
                                      COMP_NAME = h.COMP_NAME,
                                      RENEW_TIMES = d.RENEW_TIMES,
                                      LICENSE_DATE = d.LICENSE_DATE,
                                      LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,
                                      FEES = d.FEES,
                                      LICENSE_TYPE_NAME = l.LICENSE_TYPE_NAME,
                                      LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
                                      PETITION_TYPE_NAME = p.PETITION_TYPE_NAME,
                                      PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
                                      TRAN_DATE = h.TRAN_DATE,
                                      LOTS = h.LOTS,
                                      MONEY = h.MONEY,
                                      FILENAME = h.FILENAME,
                                      APPROVED_DOC = h.APPROVED_DOC,
                                      APPROVE_COMPCODE = h.APPROVE_COMPCODE,
                                      SEQ_NO = d.SEQ_NO,
                                      ORDERS = d.ORDERS,
                                      ID_CARD_NO = d.ID_CARD_NO,
                                      PRE_NAME_CODE = d.PRE_NAME_CODE,
                                      TITLE_NAME = d.TITLE_NAME,
                                      NAMES = d.NAMES,
                                      APPROVED = d.APPROVED

                                  }).FirstOrDefault();

                    if (result != null)
                    {
                        res.DataResponse = result;
                    }

                }
                //Old License
                else if ((oldLicense.Count > 0) && (oldLicense != null))
                {
                    var resAGT = (from lt in base.ctx.AG_LICENSE_T
                                  join alt in base.ctx.AG_AGENT_LICENSE_T on lt.LICENSE_NO equals alt.LICENSE_NO
                                  join ag in base.ctx.AG_IAS_LICENSE_TYPE_R on lt.LICENSE_TYPE_CODE equals ag.LICENSE_TYPE_CODE
                                  join aglt in base.ctx.AG_LICENSE_RENEW_T on lt.LICENSE_NO equals aglt.LICENSE_NO
                                  where alt.LICENSE_NO == licenseNo
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      ID_CARD_NO = alt.ID_CARD_NO,
                                      INSURANCE_COMP_CODE = alt.INSURANCE_COMP_CODE,
                                      UPLOAD_BY_SESSION = aglt.UPLOAD_BY_SESSION,
                                      LICENSE_NO = lt.LICENSE_NO,
                                      RENEW_TIME = aglt.RENEW_TIME,
                                      LICENSE_DATE = aglt.RENEW_DATE,
                                      LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
                                      LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
                                      LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,

                                  }).ToList();

                    var resAGPT = (from lt in base.ctx.AG_LICENSE_T
                                   join alt in base.ctx.AG_AGENT_LICENSE_PERSON_T on lt.LICENSE_NO equals alt.LICENSE_NO
                                   join ag in base.ctx.AG_IAS_LICENSE_TYPE_R on lt.LICENSE_TYPE_CODE equals ag.LICENSE_TYPE_CODE
                                   join aglt in base.ctx.AG_LICENSE_RENEW_T on lt.LICENSE_NO equals aglt.LICENSE_NO
                                   where alt.LICENSE_NO == licenseNo
                                   select new DTO.PersonLicenseTransaction
                                   {
                                       ID_CARD_NO = alt.ID_CARD_NO,
                                       INSURANCE_COMP_CODE = "",
                                       UPLOAD_BY_SESSION = aglt.UPLOAD_BY_SESSION,
                                       LICENSE_NO = lt.LICENSE_NO,
                                       RENEW_TIME = aglt.RENEW_TIME,
                                       LICENSE_DATE = aglt.RENEW_DATE,
                                       LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
                                       LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
                                       LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,

                                   }).ToList();

                    PersonLicenseTransaction result = resAGT.Union(resAGPT).ToList().Last();

                    if (result != null)
                    {
                        result.RENEW_TIMES = Convert.ToString(result.RENEW_TIME);
                        res.DataResponse = result;
                    }

                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetRenewLiceneEntityByLicenseNo" + ":" + ex.Message, ex.Message);
            }

            return res;
        }


        /// <summary>
        /// GetAllLicenseByIDCard
        /// เมนู ใบแทนใบอนุญาต (ชำรุดสูญหาย)
        /// </summary>
        /// <param name="idCard">idCard</param>
        /// <param name="mode">mode</param>
        /// <param name="mode">1=License Mode > submode 1 = ชำรุดสูญหาย, 2 = เปลี่ยนชื่อ-สกุล</param>
        /// <param name="mode">2=Regis Mode</param>
        /// <returns></returns>
        /// <LASTUPDATE>19/05/2557</LASTUPDATE>
        /// <AUTHOR>Natta</AUTHOR>
        public DTO.ResponseService<List<DTO.PersonLicenseTransaction>> GetAllLicenseByIDCard(string idCard, string mode, int feemode)
        {
            var res = new ResponseService<List<DTO.PersonLicenseTransaction>>();
            string getDT = string.Empty;
            List<DTO.PersonLicenseTransaction> lsAGLT = new List<PersonLicenseTransaction>();

            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();

            try
            {
                #region Func
                Func<string, string> dateCompare = delegate(string input)
                {
                    try
                    {
                        if ((input != null) && (input != ""))
                        {
                            DateTime currDate = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", DateTime.Now));
                            DateTime dateFromCtrl = Convert.ToDateTime(input);
                            int dtCompare = DateTime.Compare(dateFromCtrl, currDate);
                            //BirthDay < CurrentTime
                            if (dtCompare == -1)
                            {
                                input = Convert.ToString(dtCompare);
                            }
                            if (dtCompare == 0)
                            {
                                input = Convert.ToString(dtCompare);
                            }
                            //BirthDay > CurrentTime
                            if (dtCompare == 1)
                            {
                                input = Convert.ToString(dtCompare);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        res.ErrorMsg = ex.Message + "&&" + input;
                        LoggerFactory.CreateLog().Fatal("LicenseService_GetAllLicenseByIDCard", ex);
                    }

                    return input;
                };

                Func<int, string> convertRenewtime = delegate(int input)
                {
                    if (input != null)
                    {
                        return Convert.ToString(input);

                    }

                    return Convert.ToString(input);
                };

                Func<string, string, decimal, string> getPTName = delegate(string pettitionCode, string renewTime, decimal fee)
                {
                    if ((pettitionCode != null) && (pettitionCode != ""))
                    {
                        int renewtimes = Convert.ToInt32(renewTime) + 1;
                        if (pettitionCode.Equals("13"))
                        {
                            pettitionCode = Resources.propLicenseService_049 + Resources.propLicenseService_050 + renewtimes;
                        }
                        else if (pettitionCode.Equals("14"))
                        {
                            pettitionCode = Resources.propLicenseService_052 + Resources.propLicenseService_050 + renewtimes;
                        }
                        else if (pettitionCode.Equals("16"))
                        {
                            if (fee > 0)
                            {
                                pettitionCode = "ขอใบแทนใบอนุญาต(ชำรุดสูญหาย)";
                            }
                            else
                            {
                                pettitionCode = "ขอใบแทนใบอนุญาต(เปลี่ยนชื่อ-นามสกุล)";
                            }

                        }
                        else
                        {
                            pettitionCode = Resources.propLicenseService_051 + Resources.propLicenseService_050 + renewtimes;
                        }
                    }
                    else
                    {
                        pettitionCode = Resources.propLicenseService_053;
                    }
                    return pettitionCode;
                };

                Func<decimal, decimal> getFee = delegate(decimal input)
                {
                    if (input != null)
                    {
                        string inputFilter = Convert.ToString(input);
                        Nullable<decimal> petitionFee = base.ctx.AG_PETITION_TYPE_R.FirstOrDefault(type => type.PETITION_TYPE_CODE == inputFilter).FEE;
                        if (petitionFee != null)
                        {
                            input = (decimal)petitionFee;
                        }
                        else
                        {
                            input = 0;
                        }
                    }
                    return input;
                };


                #endregion

                if (mode.Equals("1"))
                {
                    #region 1.Old License Validation
                    //Step 1 GET License_No from AG_AGENT_T == AG_LICENSE_T
                    var resAG1 = (from A in base.ctx.AG_LICENSE_T
                                  join B in base.ctx.AG_AGENT_LICENSE_T on A.LICENSE_NO equals B.LICENSE_NO
                                  join ALTR in base.ctx.AG_IAS_LICENSE_TYPE_R on A.LICENSE_TYPE_CODE equals ALTR.LICENSE_TYPE_CODE
                                  where B.ID_CARD_NO == idCard
                                  group A by new
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = B.ID_CARD_NO,
                                      INSURANCE_COMP_CODE = B.INSURANCE_COMP_CODE,
                                      LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = ALTR.LICENSE_TYPE_NAME

                                  } into newtb
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = newtb.Key.LICENSE_NO,
                                      ID_CARD_NO = newtb.Key.ID_CARD_NO,
                                      INSURANCE_COMP_CODE = newtb.Key.INSURANCE_COMP_CODE,
                                      LICENSE_TYPE_CODE = newtb.Key.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = newtb.Key.LICENSE_TYPE_NAME

                                  }).ToList();

                    var resAG2 = (from A in base.ctx.AG_LICENSE_T
                                  join B in base.ctx.AG_AGENT_LICENSE_PERSON_T on A.LICENSE_NO equals B.LICENSE_NO
                                  join ALTR in base.ctx.AG_IAS_LICENSE_TYPE_R on A.LICENSE_TYPE_CODE equals ALTR.LICENSE_TYPE_CODE
                                  where B.ID_CARD_NO == idCard
                                  group A by new
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = B.ID_CARD_NO,
                                      LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = ALTR.LICENSE_TYPE_NAME

                                  } into newtb
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = newtb.Key.LICENSE_NO,
                                      ID_CARD_NO = newtb.Key.ID_CARD_NO,
                                      LICENSE_TYPE_CODE = newtb.Key.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = newtb.Key.LICENSE_TYPE_NAME

                                  }).ToList();

                    List<PersonLicenseTransaction> resAGT = resAG1.Union(resAG2).ToList();

                    //Step 2 Check License_No within AG_LICENSE_RENEW_T

                    if (resAGT.Count > 0)
                    {
                        List<string> LIC = new List<string>();
                        for (int i = 0; i < resAGT.Count; i++)
                        {
                            LIC.Add(resAGT[i].LICENSE_NO);
                        }

                        List<AG_LICENSE_RENEW_T> newtb = base.ctx.AG_LICENSE_RENEW_T.Where(xx => LIC.Contains(xx.LICENSE_NO)).OrderByDescending(renew => renew.RENEW_TIME).ToList();
                        //AG_LICENSE_RENEW_T entAGLT = base.ctx.AG_LICENSE_RENEW_T.Where(no => no.LICENSE_NO == licno).OrderByDescending(renew => renew.RENEW_TIME).FirstOrDefault();

                        var filteredRenewT = newtb.GroupBy(s => s.LICENSE_NO).Select(group => group.First());

                        if (filteredRenewT.Count() > 0)
                        {
                            foreach (var entAGLT in filteredRenewT)
                            {
                                //feemode 3 ขอใบอนุญาตใหม่
                                if (feemode.Equals(3))
                                {
                                    string petitionCode = Convert.ToString(DTO.PettionCode.NewLicense.GetEnumValue());
                                    DTO.PersonLicenseTransaction ent = new PersonLicenseTransaction();
                                    ent.LICENSE_NO = entAGLT.LICENSE_NO;
                                    ent.RENEW_TIME = entAGLT.RENEW_TIME;
                                    ent.LICENSE_DATE = entAGLT.RENEW_DATE;
                                    ent.LICENSE_EXPIRE_DATE = entAGLT.EXPIRE_DATE;
                                    ent.LICENSE_TYPE_CODE = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().LICENSE_TYPE_CODE;
                                    ent.LICENSE_TYPE_NAME = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().LICENSE_TYPE_NAME;
                                    ent.INSURANCE_COMP_CODE = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().INSURANCE_COMP_CODE;
                                    ent.UPLOAD_BY_SESSION = entAGLT.UPLOAD_BY_SESSION;
                                    ent.COMP_NAME = "-";
                                    ent.FEES = getFee(Convert.ToDecimal(petitionCode));
                                    ent.PETITION_TYPE_NAME = getPTName(petitionCode, Convert.ToString(entAGLT.RENEW_TIME), Convert.ToInt32(ent.FEES));
                                    ent.PETITION_TYPE_CODE = petitionCode;
                                    ent.APPROVED = entAGLT.APPROVE_FLAG;
                                    ent.APPROVED_DOC = entAGLT.APPROVE_DOC_TYPE;
                                    lsAGLT.Add(ent);
                                }
                                else
                                {
                                    string strDate = Convert.ToString(entAGLT.EXPIRE_DATE);
                                    string diffDate = dateCompare(strDate);
                                    if (!diffDate.Equals("-1"))
                                    {
                                        //PETITION_TYPE_CODE = 16	ค่าใบแทนอนุญาต
                                        string petitionCode = Convert.ToString(DTO.PettionCode.OtherLicense_1.GetEnumValue());
                                        DTO.PersonLicenseTransaction ent = new PersonLicenseTransaction();
                                        ent.LICENSE_NO = entAGLT.LICENSE_NO;
                                        ent.RENEW_TIME = entAGLT.RENEW_TIME;
                                        ent.LICENSE_DATE = entAGLT.RENEW_DATE;
                                        ent.LICENSE_EXPIRE_DATE = entAGLT.EXPIRE_DATE;
                                        ent.LICENSE_TYPE_CODE = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().LICENSE_TYPE_CODE;
                                        ent.LICENSE_TYPE_NAME = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().LICENSE_TYPE_NAME;
                                        ent.INSURANCE_COMP_CODE = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().INSURANCE_COMP_CODE;
                                        ent.UPLOAD_BY_SESSION = entAGLT.UPLOAD_BY_SESSION;
                                        ent.COMP_NAME = "-";
                                        //ent.FEES = entAGLT.LICENSE_FEE;
                                        if (feemode.Equals(1))
                                        {
                                            //FEE = from DB
                                            ent.FEES = getFee(Convert.ToDecimal(petitionCode));
                                        }
                                        else
                                        {
                                            //ใบแทนเปลี่ยนชื่อ-สกุล
                                            ent.FEES = 0;
                                        }
                                        //ent.FEES = getFee(Convert.ToDecimal(petitionCode));
                                        ent.PETITION_TYPE_NAME = getPTName(petitionCode, Convert.ToString(entAGLT.RENEW_TIME), Convert.ToInt32(ent.FEES));
                                        //ent.PETITION_TYPE_CODE = entAGLT.PETITION_TYPE_CODE;
                                        ent.PETITION_TYPE_CODE = petitionCode;
                                        ent.APPROVED = entAGLT.APPROVE_FLAG;
                                        ent.APPROVED_DOC = entAGLT.APPROVE_DOC_TYPE;
                                        lsAGLT.Add(ent);
                                    }
                                }

                            }

                        }

                    }

                    //Step 3 Validate license within AG_LICENSE_T > renew by type
                    var resls = lsAGLT.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();
                    var resIn = (from A in base.ctx.VW_IAS_COM_CODE
                                 select new DTO.InsuranceAssociate
                                 {
                                     Id = A.ID,
                                     Name = A.NAME

                                 }).ToList();

                    if (resls.Count > 0)
                    {

                        resls.ForEach(f =>
                        {
                            f.RENEW_TIMES = convertRenewtime(f.RENEW_TIME);
                            var getName = resIn.FirstOrDefault(id => id.Id == f.INSURANCE_COMP_CODE);
                            if (getName != null)
                            {
                                f.COMP_NAME = getName.Name;
                            }

                        });
                    }

                    res.DataResponse = resls.ToList();

                    #endregion
                }
                else
                {
                    #region 1.Old License Validation
                    //Step 1 GET License_No from AG_AGENT_T == AG_LICENSE_T
                    var resAG1 = (from A in base.ctx.AG_LICENSE_T
                                  join B in base.ctx.AG_AGENT_LICENSE_T on A.LICENSE_NO equals B.LICENSE_NO
                                  join ALTR in base.ctx.AG_IAS_LICENSE_TYPE_R on A.LICENSE_TYPE_CODE equals ALTR.LICENSE_TYPE_CODE
                                  where B.ID_CARD_NO == idCard
                                  group A by new
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = B.ID_CARD_NO,
                                      INSURANCE_COMP_CODE = B.INSURANCE_COMP_CODE,
                                      LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = ALTR.LICENSE_TYPE_NAME

                                  } into newtb
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = newtb.Key.LICENSE_NO,
                                      ID_CARD_NO = newtb.Key.ID_CARD_NO,
                                      INSURANCE_COMP_CODE = newtb.Key.INSURANCE_COMP_CODE,
                                      LICENSE_TYPE_CODE = newtb.Key.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = newtb.Key.LICENSE_TYPE_NAME

                                  }).ToList();

                    var resAG2 = (from A in base.ctx.AG_LICENSE_T
                                  join B in base.ctx.AG_AGENT_LICENSE_PERSON_T on A.LICENSE_NO equals B.LICENSE_NO
                                  join ALTR in base.ctx.AG_IAS_LICENSE_TYPE_R on A.LICENSE_TYPE_CODE equals ALTR.LICENSE_TYPE_CODE
                                  where B.ID_CARD_NO == idCard
                                  group A by new
                                  {
                                      LICENSE_NO = A.LICENSE_NO,
                                      ID_CARD_NO = B.ID_CARD_NO,
                                      LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = ALTR.LICENSE_TYPE_NAME

                                  } into newtb
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      LICENSE_NO = newtb.Key.LICENSE_NO,
                                      ID_CARD_NO = newtb.Key.ID_CARD_NO,
                                      LICENSE_TYPE_CODE = newtb.Key.LICENSE_TYPE_CODE,
                                      LICENSE_TYPE_NAME = newtb.Key.LICENSE_TYPE_NAME

                                  }).ToList();

                    List<PersonLicenseTransaction> resAGT = resAG1.Union(resAG2).ToList();

                    //Step 2 Check License_No within AG_LICENSE_RENEW_T

                    if (resAGT.Count > 0)
                    {
                        List<string> LIC = new List<string>();
                        for (int i = 0; i < resAGT.Count; i++)
                        {
                            LIC.Add(resAGT[i].LICENSE_NO);
                        }

                        //List<AG_LICENSE_RENEW_T> newtb = base.ctx.AG_LICENSE_RENEW_T.Where(xx => LIC.Contains(xx.LICENSE_NO)).OrderByDescending(renew => renew.RENEW_TIME).ToList();

                        #region RenewFunction
                        var qwe = from A in base.ctx.AG_LICENSE_RENEW_T
                                  where LIC.Contains(A.LICENSE_NO)
                                  group A.RENEW_TIME by A.LICENSE_NO into g
                                  select new
                                  {
                                      LICENSE_NO = g.Key,
                                      RENEW_TIME = g.Max()
                                  };

                        List<PersonLicenseTransaction> lsRenew = new List<PersonLicenseTransaction>();
                        foreach (var item in qwe)
                        {
                            PersonLicenseTransaction rt = new PersonLicenseTransaction();
                            rt.LICENSE_NO = item.LICENSE_NO;
                            rt.RENEW_TIME = item.RENEW_TIME;
                            lsRenew.Add(rt);
                        }

                        lsRenew.ForEach(x =>
                            {
                                AG_LICENSE_RENEW_T singleE = base.ctx.AG_LICENSE_RENEW_T.Where(xx => xx.LICENSE_NO == x.LICENSE_NO && xx.RENEW_TIME == x.RENEW_TIME).FirstOrDefault();
                                if (singleE != null)
                                {
                                    x.LICENSE_EXPIRE_DATE = singleE.EXPIRE_DATE;
                                    x.LICENSE_DATE = singleE.RENEW_DATE;
                                    x.UPLOAD_BY_SESSION = singleE.UPLOAD_BY_SESSION;
                                    x.APPROVED = singleE.APPROVE_FLAG;
                                    x.APPROVED_DOC = singleE.APPROVE_DOC_TYPE;
                                }

                            });

                        if (lsRenew.Count > 0)
                        {

                            foreach (var entAGLT in lsRenew)
                            {
                                //PETITION_TYPE_CODE = 16	ค่าใบแทนอนุญาต
                                string petitionCode = Convert.ToString(DTO.PettionCode.OtherLicense_1.GetEnumValue());
                                DTO.PersonLicenseTransaction ent = new PersonLicenseTransaction();
                                ent.LICENSE_NO = entAGLT.LICENSE_NO;
                                ent.RENEW_TIME = entAGLT.RENEW_TIME;
                                ent.LICENSE_DATE = entAGLT.RENEW_DATE;
                                ent.LICENSE_EXPIRE_DATE = entAGLT.LICENSE_EXPIRE_DATE;
                                ent.LICENSE_TYPE_CODE = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().LICENSE_TYPE_CODE;
                                ent.LICENSE_TYPE_NAME = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().LICENSE_TYPE_NAME;
                                ent.INSURANCE_COMP_CODE = resAGT.Where(x => x.LICENSE_NO == entAGLT.LICENSE_NO).FirstOrDefault().INSURANCE_COMP_CODE;
                                ent.UPLOAD_BY_SESSION = entAGLT.UPLOAD_BY_SESSION;
                                ent.COMP_NAME = "-";
                                //ent.FEES = entAGLT.LICENSE_FEE;
                                ent.FEES = getFee(Convert.ToDecimal(petitionCode));
                                ent.PETITION_TYPE_NAME = getPTName(petitionCode, Convert.ToString(entAGLT.RENEW_TIME), Convert.ToInt32(ent.FEES));
                                ent.PETITION_TYPE_CODE = entAGLT.PETITION_TYPE_CODE;
                                ent.PETITION_TYPE_CODE = petitionCode;
                                ent.APPROVED = entAGLT.APPROVED;
                                ent.APPROVED_DOC = entAGLT.APPROVED_DOC;
                                lsAGLT.Add(ent);
                            }
                        }

                        #endregion

                    }

                    //Step 3 Validate license within AG_LICENSE_T > renew by type
                    var resls = lsAGLT.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();
                    var resIn = (from A in base.ctx.VW_IAS_COM_CODE
                                 select new DTO.InsuranceAssociate
                                 {
                                     Id = A.ID,
                                     Name = A.NAME

                                 }).ToList();

                    if (resls.Count > 0)
                    {

                        resls.ForEach(f =>
                        {
                            f.RENEW_TIMES = convertRenewtime(f.RENEW_TIME);
                            var getName = resIn.FirstOrDefault(id => id.Id == f.INSURANCE_COMP_CODE);
                            if (getName != null)
                            {
                                f.COMP_NAME = getName.Name;
                            }

                        });
                    }

                    res.DataResponse = resls.ToList();

                    #endregion
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetAllLicenseByIDCard" + ":" + ex.Message, ex.Message);
            }


            //sw.Stop();
            //TimeSpan sp = sw.Elapsed;
            //TimeSpan duration = sp.Duration();

            return res;
        }

        /// <summary>
        /// updateRenewLicense
        /// </summary>
        /// <param name="uploadGroupNo"></param>
        /// <returns></returns>
        public DTO.ResponseMessage<bool> updateRenewLicense(DTO.PersonLicenseHead h, DTO.PersonLicenseDetail d)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_IAS_LICENSE_H entH = base.ctx.AG_IAS_LICENSE_H.FirstOrDefault(head => head.UPLOAD_GROUP_NO == h.UPLOAD_GROUP_NO);
                AG_IAS_LICENSE_D entD = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(detail => detail.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO && detail.UPLOAD_GROUP_NO == h.UPLOAD_GROUP_NO);


                if ((entH != null) && (entD != null))
                {
                    //entH.MappingToEntity(h);
                    //entD.MappingToEntity(d);
                    h.MappingToEntity<DTO.PersonLicenseHead, AG_IAS_LICENSE_H>(entH);
                    d.MappingToEntity<DTO.PersonLicenseDetail, AG_IAS_LICENSE_D>(entD);
                    base.ctx.SaveChanges();

                    res.ResultMessage = true;
                }
                else
                {

                    res.ResultMessage = false;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_updateRenewLicense" + ":" + ex.Message, ex.Message);
            }

            return res;

        }

        public DTO.ResponseMessage<bool> updateLicenseDetail(DTO.PersonLicenseDetail d)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_IAS_LICENSE_D entD = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(detail => detail.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO && detail.SEQ_NO == d.SEQ_NO);
                AG_PERSONAL_T entP = base.ctx.AG_PERSONAL_T.FirstOrDefault(person => person.ID_CARD_NO == d.ID_CARD_NO);

                if (entD != null)
                {
                    entD.NAMES = d.NAMES;
                    entD.LASTNAME = d.LASTNAME;
                    entD.EMAIL = d.EMAIL;
                    entD.ADDRESS_1 = d.ADDRESS_1;
                    entD.CURRENT_ADDRESS_1 = d.CURRENT_ADDRESS_1;
                    entD.AREA_CODE = d.AREA_CODE;
                    entD.CURRENT_AREA_CODE = d.CURRENT_AREA_CODE;


                    if (entP != null)
                    {
                        entP.PRE_NAME_CODE = entD.PRE_NAME_CODE;
                        entP.NAMES = entD.NAMES;
                        entP.LASTNAME = entD.LASTNAME;
                        entP.REMARK = entD.EMAIL;
                        entP.LOCAL_ADDRESS1 = entD.ADDRESS_1;
                        entP.LOCAL_ADDRESS2 = entD.CURRENT_ADDRESS_1;
                        entP.AREA_CODE = entD.CURRENT_AREA_CODE;
                        entP.LOCAL_AREA_CODE = entD.AREA_CODE;
                    }
                    else
                    {
                        entP = new AG_PERSONAL_T();
                        entP.ID_CARD_NO = d.ID_CARD_NO;

                        VW_IAS_TITLE_NAME_PRIORITY vwTitle = base.ctx.VW_IAS_TITLE_NAME_PRIORITY.FirstOrDefault(title => title.NAME.Trim() == entD.TITLE_NAME.Trim());


                        if (vwTitle != null)
                        {
                            entP.PRE_NAME_CODE = vwTitle.ID.ToString();
                        }

                        entP.NAMES = entD.NAMES;
                        entP.LASTNAME = entD.LASTNAME;
                        entP.REMARK = entD.EMAIL;
                        entP.LOCAL_ADDRESS1 = entD.ADDRESS_1;
                        entP.LOCAL_ADDRESS2 = entD.CURRENT_ADDRESS_1;
                        entP.AREA_CODE = entD.CURRENT_AREA_CODE;
                        entP.LOCAL_AREA_CODE = entD.AREA_CODE;

                        base.ctx.AG_PERSONAL_T.AddObject(entP);
                    }


                    res.ResultMessage = true;
                }
                else
                {

                    res.ResultMessage = false;
                }

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_updateLicenseDetail" + ":" + ex.Message, ex.Message);
            }

            return res;

        }

        /// <summary>
        /// getViewPersonLicense
        /// </summary> เรียกดูข้อมูล License ทั้งหมด ตามเงื่อนไขการอนุมัติ
        /// Nattapong
        /// <param name="uploadGroupNo"></param>
        /// <returns></returns>
        /// <LastUpdate>30/06/2557</LastUpdate>
        public DTO.ResponseService<List<DTO.PersonLicenseTransaction>> getViewPersonLicense(string idCard, string status)
        {
            var res = new DTO.ResponseService<List<DTO.PersonLicenseTransaction>>();
            try
            {
                #region Func
                Func<string, string> ConverCodeToString = delegate(string code)
                {
                    if (code.Length.Equals(1))
                    {
                        string x = code.Replace(code, "0" + code);
                        code = x;
                    }
                    return code;

                };

                Func<string, string> ConvertLicense = delegate(string license)
                {

                    if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type01.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_032;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type02.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_033;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type03.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_034;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type04.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_035;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type05.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_036;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type06.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_037;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type07.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_038;
                    }
                    else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type08.GetEnumValue()))))
                    {

                        license = Resources.propLicenseService_039;
                    }
                    else
                    {
                        license = Resources.propLicenseService_040;
                    }
                    return license;
                };

                Func<string, string> ConvertPettion = delegate(string pettion)
                {
                    if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.NewLicense.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_041;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense1Y.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_042;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense5Y.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_043;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.ExpireRenewLicense.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_044;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.OtherLicense_1.GetEnumValue()))))
                    {
                        pettion = Resources.propLicenseService_045;
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.MoveLicense.GetEnumValue()))))
                    {
                        pettion = "ใบอนุญาต(ย้ายบริษัท)";
                    }
                    else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.SecondLicense.GetEnumValue()))))
                    {
                        pettion = "ใบอนุญาต(ใบอนุญาตใบที่ 2)";
                    }
                    else
                    {
                        pettion = Resources.propLicenseService_040;
                    }
                    return pettion;
                };

                Func<string, string> ConvertApproveDoc = (input) =>
                {
                    if ((input != null) || (input != ""))
                    {
                        if (input.Equals("W"))
                        {
                            input = Resources.propLicenseService_046;
                        }
                        else if (input.Equals("N"))
                        {
                            input = Resources.propLicenseService_047;
                        }
                        else if (input.Equals("Y"))
                        {
                            input = Resources.propLicenseService_048;
                        }
                    }
                    return input;
                };

                Func<int, string> convertRenewtime = delegate(int input)
                {
                    if (input != null)
                    {
                        return Convert.ToString(input);

                    }

                    return Convert.ToString(input);
                };
                #endregion

                #region Lastupdate @14-11-56
                //All Mode
                if (status.Equals("A"))
                {
                    var result1 = (from h in base.ctx.AG_IAS_LICENSE_H
                                   from d in this.ctx.AG_IAS_LICENSE_D
                                   from l in this.ctx.AG_IAS_LICENSE_TYPE_R
                                   from p in this.ctx.AG_IAS_PETITION_TYPE_R
                                   from g in this.ctx.AG_IAS_SUBPAYMENT_H_T
                                   where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
                                   d.ID_CARD_NO == idCard &&
                                   h.LICENSE_TYPE_CODE == l.LICENSE_TYPE_CODE &&
                                   h.PETITION_TYPE_CODE == p.PETITION_TYPE_CODE &&
                                   d.HEAD_REQUEST_NO == g.HEAD_REQUEST_NO
                                   select new DTO.PersonLicenseTransaction
                                   {
                                       HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
                                       UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                                       COMP_CODE = h.COMP_CODE,
                                       COMP_NAME = h.COMP_NAME,
                                       LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
                                       PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
                                       LICENSE_NO = d.LICENSE_NO,
                                       ID_CARD_NO = d.ID_CARD_NO,
                                       RENEW_TIMES = d.RENEW_TIMES,
                                       SEQ_NO = d.SEQ_NO,
                                       GROUP_REQUEST_NO = g.GROUP_REQUEST_NO,
                                       FEES = d.FEES,
                                       MONEY = h.MONEY,
                                       APPROVED_DOC = h.APPROVED_DOC,
                                       TRAN_DATE = h.TRAN_DATE,
                                       LICENSE_DATE = d.LICENSE_DATE,
                                       LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,

                                   }).OrderByDescending(dt => dt.LICENSE_DATE).ToList();

                    var result2 = (from h in base.ctx.AG_IAS_LICENSE_H
                                   from d in this.ctx.AG_IAS_LICENSE_D
                                   from l in this.ctx.AG_IAS_LICENSE_TYPE_R
                                   from p in this.ctx.AG_IAS_PETITION_TYPE_R
                                   where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
                                   d.ID_CARD_NO == idCard &&
                                   h.LICENSE_TYPE_CODE == l.LICENSE_TYPE_CODE &&
                                   h.PETITION_TYPE_CODE == p.PETITION_TYPE_CODE &&
                                   d.HEAD_REQUEST_NO == null &&
                                   d.FEES > 0 && d.APPROVED.Equals("W")
                                   select new DTO.PersonLicenseTransaction
                                   {
                                       HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
                                       UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                                       COMP_CODE = h.COMP_CODE,
                                       COMP_NAME = h.COMP_NAME,
                                       LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
                                       PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
                                       LICENSE_NO = d.LICENSE_NO,
                                       ID_CARD_NO = d.ID_CARD_NO,
                                       RENEW_TIMES = d.RENEW_TIMES,
                                       SEQ_NO = d.SEQ_NO,

                                       FEES = d.FEES,
                                       MONEY = h.MONEY,
                                       APPROVED_DOC = h.APPROVED_DOC,
                                       TRAN_DATE = h.TRAN_DATE,
                                       LICENSE_DATE = d.LICENSE_DATE,
                                       LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,


                                   }).OrderByDescending(dt => dt.LICENSE_DATE).ToList();

                    var result = result1.ToList().Union(result2.ToList()).ToList();

                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].LICENSE_TYPE_CODE = ConvertLicense(result[i].LICENSE_TYPE_CODE);
                            result[i].PETITION_TYPE_CODE = ConvertPettion(result[i].PETITION_TYPE_CODE);
                            result[i].APPROVED_DOC = ConvertApproveDoc(result[i].APPROVED_DOC);
                        }
                    }


                    res.DataResponse = result.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();

                }
                //Approve Y & W
                else
                {
                    var result = (from h in base.ctx.AG_IAS_LICENSE_H
                                  from d in this.ctx.AG_IAS_LICENSE_D
                                  from l in this.ctx.AG_IAS_LICENSE_TYPE_R
                                  from p in this.ctx.AG_IAS_PETITION_TYPE_R
                                  where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
                                  d.ID_CARD_NO == idCard &&
                                  h.LICENSE_TYPE_CODE == l.LICENSE_TYPE_CODE &&
                                  h.PETITION_TYPE_CODE == p.PETITION_TYPE_CODE &&
                                  d.HEAD_REQUEST_NO == null &&
                                  d.FEES > 0 && d.APPROVED.Equals("Y")
                                  select new DTO.PersonLicenseTransaction
                                  {
                                      HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
                                      UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                                      COMP_CODE = h.COMP_CODE,
                                      COMP_NAME = h.COMP_NAME,
                                      LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
                                      PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
                                      LICENSE_NO = d.LICENSE_NO,
                                      ID_CARD_NO = d.ID_CARD_NO,
                                      RENEW_TIMES = d.RENEW_TIMES,
                                      SEQ_NO = d.SEQ_NO,

                                      FEES = d.FEES,
                                      MONEY = h.MONEY,
                                      APPROVED_DOC = h.APPROVED_DOC,
                                      TRAN_DATE = h.TRAN_DATE,
                                      LICENSE_DATE = d.LICENSE_DATE,
                                      LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,


                                  }).OrderByDescending(dt => dt.LICENSE_DATE).ToList();

                    //.OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(1).ToList();

                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            result[i].LICENSE_TYPE_CODE = ConvertLicense(result[i].LICENSE_TYPE_CODE);
                            result[i].PETITION_TYPE_CODE = ConvertPettion(result[i].PETITION_TYPE_CODE);
                            result[i].APPROVED_DOC = ConvertApproveDoc(result[i].APPROVED_DOC);
                        }
                    }

                    res.DataResponse = result.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();

                }
                #endregion

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_getViewPersonLicense" + ":" + ex.Message, ex.Message);
            }

            return res;

        }

        #region Old @14-11-56 >> Do not edit! by NATTAPONG
        //public DTO.ResponseService<List<DTO.PersonLicenseTransaction>> getViewPersonLicense(string idCard, string status)
        //{
        //    var res = new DTO.ResponseService<List<DTO.PersonLicenseTransaction>>();
        //    try
        //    {
        //        #region Func
        //        Func<string, string> ConverCodeToString = delegate(string code)
        //        {
        //            if (code.Length.Equals(1))
        //            {
        //                string x = code.Replace(code, "0" + code);
        //                code = x;
        //            }
        //            return code;

        //        };

        //        Func<string, string> ConvertLicense = delegate(string license)
        //        {

        //            if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type01.GetEnumValue()))))
        //            {

        //                license = "ตัวแทนประกันชีวิต";
        //            }
        //            else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type02.GetEnumValue()))))
        //            {

        //                license = "ตัวแทนประกันวินาศภัย";
        //            }
        //            else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type03.GetEnumValue()))))
        //            {

        //                license = "การจัดการประกันชีวิตโดยตรง";
        //            }
        //            else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type04.GetEnumValue()))))
        //            {

        //                license = "การจัดการประกันวินาศภัยโดยตรง";
        //            }
        //            else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type05.GetEnumValue()))))
        //            {

        //                license = "การประกันภัยอุบัติเหตุส่วนบุคคลและประกันสุขภาพ";
        //            }
        //            else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type06.GetEnumValue()))))
        //            {

        //                license = "พรบ.คุ้มครองผู้ประสบภัยจากรถ";
        //            }
        //            else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type07.GetEnumValue()))))
        //            {

        //                license = "สำหรับการประกันภัยรายย่อย(ชีวิต)";
        //            }
        //            else if (license.Equals(ConverCodeToString(Convert.ToString(DTO.LicenseType.Type08.GetEnumValue()))))
        //            {

        //                license = "สำหรับการประกันภัยรายย่อย(วินาศภัย)";
        //            }
        //            else
        //            {
        //                license = "??";
        //            }
        //            return license;
        //        };

        //        Func<string, string> ConvertPettion = delegate(string pettion)
        //        {
        //            if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.NewLicense.GetEnumValue()))))
        //            {
        //                pettion = "ขอใบอนุญาตใหม่";
        //            }
        //            else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense1Y.GetEnumValue()))))
        //            {
        //                pettion = "ขอต่ออายุใบอนุญาต 1 ปี";
        //            }
        //            else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.RenewLicense5Y.GetEnumValue()))))
        //            {
        //                pettion = "ขอต่ออายุใบอนุญาต 5 ปี";
        //            }
        //            else if (pettion.Equals(ConverCodeToString(Convert.ToString(DTO.PettionCode.ExpireRenewLicense.GetEnumValue()))))
        //            {
        //                pettion = "ขาดต่อขอใหม่";
        //            }
        //            else
        //            {
        //                pettion = "??";
        //            }
        //            return pettion;
        //        };

        //        Func<string, string> ConvertApproveDoc = (input) =>
        //        {
        //            if ((input != null) || (input != ""))
        //            {
        //                if (input.Equals("W"))
        //                {
        //                    input = "รออนุมัติ";
        //                }
        //                else if (input.Equals("N"))
        //                {
        //                    input = "ไม่อนุมัติ";
        //                }
        //                else if (input.Equals("Y"))
        //                {
        //                    input = "อนุมัติ";
        //                }
        //            }
        //            return input;
        //        };

        //        Func<int, string> convertRenewtime = delegate(int input)
        //        {
        //            if (input != null)
        //            {
        //                return Convert.ToString(input);

        //            }

        //            return Convert.ToString(input);
        //        };
        //        #endregion

        //        //All Mode
        //        if (status.Equals("A"))
        //        {

        //            //Check New License First
        //            //AG_IAS_LICENSE_D newlicnese = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(newlicense => newlicense.ID_CARD_NO == idCard);

        //            //Check Old License Secone
        //            var old1 = (from A in base.ctx.AG_AGENT_LICENSE_T
        //                        where A.ID_CARD_NO == idCard
        //                        select new DTO.PersonLicenseTransaction
        //                        {
        //                            LICENSE_NO = A.LICENSE_NO,
        //                            ID_CARD_NO = A.ID_CARD_NO
        //                        }).ToList();

        //            var old2 = (from B in base.ctx.AG_AGENT_LICENSE_PERSON_T
        //                        where B.ID_CARD_NO == idCard
        //                        select new DTO.PersonLicenseTransaction
        //                        {
        //                            LICENSE_NO = B.LICENSE_NO,
        //                            ID_CARD_NO = B.ID_CARD_NO
        //                        }).ToList();

        //            var oldLicense = old1.Union(old2).ToList();
        //            //AG_AGENT_LICENSE_T oldLicense = base.ctx.AG_AGENT_LICENSE_T.FirstOrDefault(oldlicense => oldlicense.ID_CARD_NO == idCard);


        //            //New License Case
        //            if ((oldLicense.Count == 0) && (oldLicense != null))
        //            {
        //                var result = (from h in base.ctx.AG_IAS_LICENSE_H
        //                              from d in this.ctx.AG_IAS_LICENSE_D
        //                              from l in this.ctx.AG_IAS_LICENSE_TYPE_R
        //                              from p in this.ctx.AG_IAS_PETITION_TYPE_R
        //                              where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
        //                              d.ID_CARD_NO == idCard &&
        //                              h.LICENSE_TYPE_CODE == l.LICENSE_TYPE_CODE &&
        //                              h.PETITION_TYPE_CODE == p.PETITION_TYPE_CODE
        //                              select new DTO.PersonLicenseTransaction
        //                              {
        //                                  HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
        //                                  UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
        //                                  COMP_CODE = h.COMP_CODE,
        //                                  COMP_NAME = h.COMP_NAME,
        //                                  LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
        //                                  PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
        //                                  LICENSE_NO = d.LICENSE_NO,
        //                                  ID_CARD_NO = d.ID_CARD_NO,
        //                                  RENEW_TIMES = d.RENEW_TIMES,
        //                                  SEQ_NO = d.SEQ_NO,

        //                                  FEES = d.FEES,
        //                                  MONEY = h.MONEY,
        //                                  APPROVED_DOC = h.APPROVED_DOC,
        //                                  TRAN_DATE = h.TRAN_DATE,
        //                                  LICENSE_DATE = d.LICENSE_DATE,
        //                                  LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,

        //                              }).OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(3).ToList();

        //                if (result.Count > 0)
        //                {
        //                    for (int i = 0; i < result.Count; i++)
        //                    {
        //                        result[i].LICENSE_TYPE_CODE = ConvertLicense(result[i].LICENSE_TYPE_CODE);
        //                        result[i].PETITION_TYPE_CODE = ConvertPettion(result[i].PETITION_TYPE_CODE);
        //                        result[i].APPROVED_DOC = ConvertApproveDoc(result[i].APPROVED_DOC);
        //                    }
        //                }


        //                res.DataResponse = result.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();

        //            }
        //            //Old License Case
        //            else if ((oldLicense.Count > 0) && (oldLicense != null))
        //            {
        //                #region Old
        //                //var resAGT = (from lt in base.ctx.AG_LICENSE_T
        //                //              join alt in base.ctx.AG_AGENT_LICENSE_T on lt.LICENSE_NO equals alt.LICENSE_NO
        //                //              join ag in base.ctx.AG_IAS_LICENSE_TYPE_R on lt.LICENSE_TYPE_CODE equals ag.LICENSE_TYPE_CODE
        //                //              join aglt in base.ctx.AG_LICENSE_RENEW_T on lt.LICENSE_NO equals aglt.LICENSE_NO
        //                //              where alt.ID_CARD_NO == idCard
        //                //              select new DTO.PersonLicenseTransaction
        //                //              {
        //                //                  ID_CARD_NO = alt.ID_CARD_NO,
        //                //                  INSURANCE_COMP_CODE = alt.INSURANCE_COMP_CODE,
        //                //                  UPLOAD_BY_SESSION = aglt.UPLOAD_BY_SESSION,
        //                //                  LICENSE_NO = lt.LICENSE_NO,
        //                //                  RENEW_TIME = aglt.RENEW_TIME,
        //                //                  LICENSE_DATE = aglt.RENEW_DATE,
        //                //                  LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
        //                //                  LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
        //                //                  LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,

        //                //              }).ToList();

        //                //var resAGPT = (from lt in base.ctx.AG_LICENSE_T
        //                //               join alt in base.ctx.AG_AGENT_LICENSE_PERSON_T on lt.LICENSE_NO equals alt.LICENSE_NO
        //                //               join ag in base.ctx.AG_IAS_LICENSE_TYPE_R on lt.LICENSE_TYPE_CODE equals ag.LICENSE_TYPE_CODE
        //                //               join aglt in base.ctx.AG_LICENSE_RENEW_T on lt.LICENSE_NO equals aglt.LICENSE_NO
        //                //               where alt.ID_CARD_NO == idCard
        //                //               select new DTO.PersonLicenseTransaction
        //                //               {
        //                //                   ID_CARD_NO = alt.ID_CARD_NO,
        //                //                   INSURANCE_COMP_CODE = "",
        //                //                   UPLOAD_BY_SESSION = aglt.UPLOAD_BY_SESSION,
        //                //                   LICENSE_NO = lt.LICENSE_NO,
        //                //                   RENEW_TIME = aglt.RENEW_TIME,
        //                //                   LICENSE_DATE = aglt.RENEW_DATE,
        //                //                   LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
        //                //                   LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
        //                //                   LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,

        //                //               }).ToList();

        //                //List<PersonLicenseTransaction> result = resAGT.Union(resAGPT).ToList().OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(3).ToList();
        //                #endregion


        //                var result = (from lt in base.ctx.AG_LICENSE_T
        //                              from alt in this.ctx.AG_AGENT_LICENSE_T
        //                              from ag in this.ctx.AG_IAS_LICENSE_TYPE_R
        //                              from aglt in this.ctx.AG_LICENSE_RENEW_T
        //                              where alt.ID_CARD_NO == idCard &&
        //                              lt.LICENSE_NO == alt.LICENSE_NO &&
        //                              lt.LICENSE_TYPE_CODE == ag.LICENSE_TYPE_CODE &&
        //                              aglt.LICENSE_NO == lt.LICENSE_NO
        //                              select new DTO.PersonLicenseTransaction
        //                              {
        //                                  //HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
        //                                  //UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
        //                                  //UPLOAD_BY_SESSION = h.UPLOAD_BY_SESSION,
        //                                  ID_CARD_NO = alt.ID_CARD_NO,
        //                                  LICENSE_NO = lt.LICENSE_NO,
        //                                  //COMP_NAME = h.COMP_NAME,
        //                                  RENEW_TIME = aglt.RENEW_TIME,
        //                                  LICENSE_DATE = aglt.RENEW_DATE,
        //                                  LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
        //                                  //FEES = d.FEES,
        //                                  LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
        //                                  LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,
        //                                  //PETITION_TYPE_NAME = p.PETITION_TYPE_NAME,
        //                                  //PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
        //                                  //APPROVED = d.APPROVED,
        //                                  //APPROVED_DOC = h.APPROVED_DOC

        //                              }).OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(3).ToList();

        //                if (result.Count > 0)
        //                {
        //                    for (int i = 0; i < result.Count; i++)
        //                    {
        //                        result[i].RENEW_TIMES = convertRenewtime(result[i].RENEW_TIME);
        //                        //result[i].LICENSE_TYPE_CODE = ConvertLicense(result[i].LICENSE_TYPE_CODE);
        //                        //result[i].PETITION_TYPE_CODE = ConvertPettion(result[i].PETITION_TYPE_CODE);
        //                        //result[i].APPROVED_DOC = ConvertApproveDoc(result[i].APPROVED_DOC);
        //                    }
        //                }


        //                res.DataResponse = result.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();

        //            }

        //        }
        //        //Approve Y & W
        //        else
        //        {
        //            //Check New License First
        //            //AG_IAS_LICENSE_D newlicnese = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(newlicense => newlicense.ID_CARD_NO == idCard);

        //            //Check Old License
        //            var old1 = (from A in base.ctx.AG_AGENT_LICENSE_T
        //                        where A.ID_CARD_NO == idCard
        //                        select new DTO.PersonLicenseTransaction
        //                        {
        //                            LICENSE_NO = A.LICENSE_NO,
        //                            ID_CARD_NO = A.ID_CARD_NO
        //                        }).ToList();

        //            var old2 = (from B in base.ctx.AG_AGENT_LICENSE_PERSON_T
        //                        where B.ID_CARD_NO == idCard
        //                        select new DTO.PersonLicenseTransaction
        //                        {
        //                            LICENSE_NO = B.LICENSE_NO,
        //                            ID_CARD_NO = B.ID_CARD_NO
        //                        }).ToList();

        //            var oldLicense = old1.Union(old2).ToList();
        //            //AG_AGENT_LICENSE_T oldLicense = base.ctx.AG_AGENT_LICENSE_T.FirstOrDefault(oldlicense => oldlicense.ID_CARD_NO == idCard);

        //            //New License case
        //            if ((oldLicense.Count == 0) && (oldLicense != null))
        //            {

        //                var result = (from h in base.ctx.AG_IAS_LICENSE_H
        //                              from d in this.ctx.AG_IAS_LICENSE_D
        //                              from l in this.ctx.AG_IAS_LICENSE_TYPE_R
        //                              from p in this.ctx.AG_IAS_PETITION_TYPE_R
        //                              where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
        //                              d.ID_CARD_NO == idCard &&
        //                              h.LICENSE_TYPE_CODE == l.LICENSE_TYPE_CODE &&
        //                              h.PETITION_TYPE_CODE == p.PETITION_TYPE_CODE &&
        //                              d.APPROVED == status &&
        //                              h.APPROVED_DOC == status &&
        //                              d.HEAD_REQUEST_NO == null
        //                              select new DTO.PersonLicenseTransaction
        //                              {
        //                                  HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
        //                                  UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
        //                                  COMP_CODE = h.COMP_CODE,
        //                                  COMP_NAME = h.COMP_NAME,
        //                                  LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
        //                                  PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
        //                                  LICENSE_NO = d.LICENSE_NO,
        //                                  ID_CARD_NO = d.ID_CARD_NO,
        //                                  RENEW_TIMES = d.RENEW_TIMES,
        //                                  SEQ_NO = d.SEQ_NO,

        //                                  FEES = d.FEES,
        //                                  MONEY = h.MONEY,
        //                                  APPROVED_DOC = h.APPROVED_DOC,
        //                                  TRAN_DATE = h.TRAN_DATE,
        //                                  LICENSE_DATE = d.LICENSE_DATE,
        //                                  LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,


        //                              }).OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(3).ToList();

        //                //.OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(1).ToList();

        //                if (result.Count > 0)
        //                {
        //                    for (int i = 0; i < result.Count; i++)
        //                    {
        //                        result[i].LICENSE_TYPE_CODE = ConvertLicense(result[i].LICENSE_TYPE_CODE);
        //                        result[i].PETITION_TYPE_CODE = ConvertPettion(result[i].PETITION_TYPE_CODE);
        //                        result[i].APPROVED_DOC = ConvertApproveDoc(result[i].APPROVED_DOC);
        //                    }
        //                }

        //                res.DataResponse = result.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();
        //            }
        //            //Old License Case
        //            else if ((oldLicense.Count > 0) && (oldLicense != null))
        //            {
        //                #region Old
        //                //var resAGT = (from lt in base.ctx.AG_LICENSE_T
        //                //              join alt in base.ctx.AG_AGENT_LICENSE_T on lt.LICENSE_NO equals alt.LICENSE_NO
        //                //              join ag in base.ctx.AG_IAS_LICENSE_TYPE_R on lt.LICENSE_TYPE_CODE equals ag.LICENSE_TYPE_CODE
        //                //              join aglt in base.ctx.AG_LICENSE_RENEW_T on lt.LICENSE_NO equals aglt.LICENSE_NO
        //                //              where alt.ID_CARD_NO == idCard
        //                //              select new DTO.PersonLicenseTransaction
        //                //              {
        //                //                  ID_CARD_NO = alt.ID_CARD_NO,
        //                //                  INSURANCE_COMP_CODE = alt.INSURANCE_COMP_CODE,
        //                //                  UPLOAD_BY_SESSION = aglt.UPLOAD_BY_SESSION,
        //                //                  LICENSE_NO = lt.LICENSE_NO,
        //                //                  RENEW_TIME = aglt.RENEW_TIME,
        //                //                  LICENSE_DATE = aglt.RENEW_DATE,
        //                //                  LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
        //                //                  LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
        //                //                  LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,

        //                //              }).ToList();

        //                //var resAGPT = (from lt in base.ctx.AG_LICENSE_T
        //                //               join alt in base.ctx.AG_AGENT_LICENSE_PERSON_T on lt.LICENSE_NO equals alt.LICENSE_NO
        //                //               join ag in base.ctx.AG_IAS_LICENSE_TYPE_R on lt.LICENSE_TYPE_CODE equals ag.LICENSE_TYPE_CODE
        //                //               join aglt in base.ctx.AG_LICENSE_RENEW_T on lt.LICENSE_NO equals aglt.LICENSE_NO
        //                //               where alt.ID_CARD_NO == idCard
        //                //               select new DTO.PersonLicenseTransaction
        //                //               {
        //                //                   ID_CARD_NO = alt.ID_CARD_NO,
        //                //                   INSURANCE_COMP_CODE = "",
        //                //                   UPLOAD_BY_SESSION = aglt.UPLOAD_BY_SESSION,
        //                //                   LICENSE_NO = lt.LICENSE_NO,
        //                //                   RENEW_TIME = aglt.RENEW_TIME,
        //                //                   LICENSE_DATE = aglt.RENEW_DATE,
        //                //                   LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
        //                //                   LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
        //                //                   LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,

        //                //               }).ToList();

        //                //List<PersonLicenseTransaction> result = resAGT.Union(resAGPT).ToList().OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(3).ToList();
        //                #endregion

        //                var result = (from lt in base.ctx.AG_LICENSE_T
        //                              from alt in this.ctx.AG_AGENT_LICENSE_T
        //                              from ag in this.ctx.AG_IAS_LICENSE_TYPE_R
        //                              from aglt in this.ctx.AG_LICENSE_RENEW_T
        //                              where alt.ID_CARD_NO == idCard &&
        //                              lt.LICENSE_NO == alt.LICENSE_NO &&
        //                              lt.LICENSE_TYPE_CODE == ag.LICENSE_TYPE_CODE &&
        //                              aglt.LICENSE_NO == lt.LICENSE_NO
        //                              select new DTO.PersonLicenseTransaction
        //                              {
        //                                  //HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
        //                                  //UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
        //                                  //UPLOAD_BY_SESSION = h.UPLOAD_BY_SESSION,
        //                                  ID_CARD_NO = alt.ID_CARD_NO,
        //                                  LICENSE_NO = lt.LICENSE_NO,
        //                                  //COMP_NAME = h.COMP_NAME,
        //                                  RENEW_TIME = aglt.RENEW_TIME,
        //                                  LICENSE_DATE = aglt.RENEW_DATE,
        //                                  LICENSE_EXPIRE_DATE = aglt.EXPIRE_DATE,
        //                                  //FEES = d.FEES,
        //                                  LICENSE_TYPE_NAME = ag.LICENSE_TYPE_NAME,
        //                                  LICENSE_TYPE_CODE = lt.LICENSE_TYPE_CODE,
        //                                  //PETITION_TYPE_NAME = p.PETITION_TYPE_NAME,
        //                                  //PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
        //                                  //APPROVED = d.APPROVED,
        //                                  //APPROVED_DOC = h.APPROVED_DOC

        //                              }).OrderByDescending(dt => dt.LICENSE_DATE).Skip(0).Take(3).ToList();

        //                if (result.Count > 0)
        //                {
        //                    for (int i = 0; i < result.Count; i++)
        //                    {
        //                        result[i].RENEW_TIMES = convertRenewtime(result[i].RENEW_TIME);
        //                        //result[i].LICENSE_TYPE_CODE = ConvertLicense(result[i].LICENSE_TYPE_CODE);
        //                        //result[i].PETITION_TYPE_CODE = ConvertPettion(result[i].PETITION_TYPE_CODE);
        //                        //result[i].APPROVED_DOC = ConvertApproveDoc(result[i].APPROVED_DOC);
        //                    }
        //                }


        //                res.DataResponse = result.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();
        //            }


        //        }


        //        //res.DataResponse = result.OrderByDescending(lastExp => lastExp.LICENSE_EXPIRE_DATE).ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
        //    }

        //    return res;

        //}
        #endregion

        /// <summary>
        /// GetLicenseDetailByUploadGroupNo
        /// </summary>
        /// <param name="uploadGroupNo"></param>
        /// <returns></returns>
        public DTO.ResponseService<DTO.PersonLicenseTransaction> GetLicenseDetailByUploadGroupNo(string upGroupNo)
        {
            var res = new DTO.ResponseService<DTO.PersonLicenseTransaction>();
            try
            {
                var result = (from h in base.ctx.AG_IAS_LICENSE_H
                              from d in this.ctx.AG_IAS_LICENSE_D
                              from l in this.ctx.AG_IAS_LICENSE_TYPE_R
                              from p in this.ctx.AG_IAS_PETITION_TYPE_R
                              where h.UPLOAD_GROUP_NO == d.UPLOAD_GROUP_NO &&
                              d.UPLOAD_GROUP_NO == upGroupNo &&
                              h.LICENSE_TYPE_CODE == l.LICENSE_TYPE_CODE &&
                              h.PETITION_TYPE_CODE == p.PETITION_TYPE_CODE
                              select new DTO.PersonLicenseTransaction
                              {
                                  HEAD_REQUEST_NO = d.HEAD_REQUEST_NO,
                                  UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                                  COMP_CODE = h.COMP_CODE,
                                  COMP_NAME = h.COMP_NAME,
                                  LICENSE_TYPE_CODE = h.LICENSE_TYPE_CODE,
                                  PETITION_TYPE_CODE = h.PETITION_TYPE_CODE,
                                  LICENSE_NO = d.LICENSE_NO,
                                  ID_CARD_NO = d.ID_CARD_NO,
                                  RENEW_TIMES = d.RENEW_TIMES,
                                  SEQ_NO = d.SEQ_NO,

                                  FEES = d.FEES,
                                  MONEY = h.MONEY,
                                  APPROVED_DOC = h.APPROVED_DOC,
                                  TRAN_DATE = h.TRAN_DATE,
                                  LICENSE_DATE = d.LICENSE_DATE,
                                  LICENSE_EXPIRE_DATE = d.LICENSE_EXPIRE_DATE,

                              }).FirstOrDefault();

                if (result != null)
                {
                    res.DataResponse = result;
                }
                else
                {
                    res.DataResponse = null;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseDetailByUploadGroupNo" + ":" + ex.Message, ex.Message);
            }
            return res;

        }

        public DTO.ResponseService<DTO.PersonLicenseTransaction> GetLicenseRenewDateByLicenseNo(string licenseNo)
        {
            var res = new DTO.ResponseService<DTO.PersonLicenseTransaction>();
            Boolean pass = false;

            try
            {
                var resEx = (from A in base.ctx.AG_LICENSE_T
                             where A.LICENSE_NO == licenseNo
                             select new DTO.PersonLicenseTransaction
                             {
                                 LICENSE_NO = A.LICENSE_NO,
                                 LICENSE_DATE = A.LICENSE_DATE,
                                 EXPIRE_DATE = A.EXPIRE_DATE
                             }).FirstOrDefault();

                if (resEx != null)
                {
                    DateTime date_Date = Convert.ToDateTime(resEx.EXPIRE_DATE);

                    while (!pass)
                    {
                        if ((date_Date.DayOfWeek == DayOfWeek.Saturday) || (date_Date.DayOfWeek == DayOfWeek.Sunday))
                        {
                            date_Date = date_Date.AddDays(1);
                        }
                        else
                        {
                            if (checkEventDate(Convert.ToString(date_Date)))// false = stop date true = work date
                            {
                                pass = true;
                            }
                            else
                            {
                                date_Date = date_Date.AddDays(1);
                            }
                        }

                    }

                    resEx.EXPIRE_DATE = date_Date;
                }

                res.DataResponse = resEx;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseExpireDateByLicenseNo" + ":" + ex.Message, ex.Message);
            }

            return res;
        }

        #endregion

        public DTO.ResponseService<DataSet> GetListLicenseDetailVerify(string uploadGroupNo, string CountPage, int pageNo, int recordPerPage)
        {
            var res = new DTO.ResponseService<DataSet>();

            try
            {
                #region SQL Statement
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                string sql = string.Empty;
                if (CountPage == "Y")
                {
                    sql = "select Count(*) rowcount from( " +
                          "SELECT	(SELECT PTT.NAMES FROM AG_IAS_PERSONAL_T PTT WHERE PTT.ID = LD.APPROVED_BY ) AS NAME , " +
                          "         LD.ID_CARD_NO , LD.TITLE_NAME || ' ' || LD.NAMES || ' ' || LD.LASTNAME FLNAME , LD.APPROVED_DATE ,LD.APPROVED_By, " +
                          "         LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED  " +
                          "FROM	    AG_IAS_LICENSE_D LD " +
                          "WHERE	LD.UPLOAD_GROUP_NO = '" + uploadGroupNo + "')  ";
                }
                else
                {
                    sql = "select *  from( " +
                          "SELECT	(SELECT PTT.NAMES || ' ' || PTT.LastName as NAME FROM AG_IAS_PERSONAL_T PTT WHERE PTT.ID = LD.APPROVED_BY ) AS NAME , " +
                          "         LD.ID_CARD_NO , LD.TITLE_NAME || ' ' || LD.NAMES || ' ' || LD.LASTNAME FLNAME , LD.APPROVED_DATE ,LD.APPROVED_By, " +
                          "         LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED,ROW_NUMBER() OVER (ORDER BY LD.ID_CARD_NO) RUN_NO ,LD.RENEW_TIMES,LD.LICENSE_NO " +
                          "FROM	    AG_IAS_LICENSE_D LD " +
                          "WHERE	LD.UPLOAD_GROUP_NO = '" + uploadGroupNo + "' )A   " + critRecNo;
                }
                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetListLicenseDetailVerify", ex);
            }
            return res;
        }

        public DTO.ResponseService<DTO.VerifyDocumentHeader> GetVerifyHeadByUploadGroupNo(string uploadGroupNo)
        {
            DTO.ResponseService<DTO.VerifyDocumentHeader> res = new DTO.ResponseService<DTO.VerifyDocumentHeader>();

            try
            {
                var head = base.ctx.AG_IAS_LICENSE_H
                             .SingleOrDefault(s => s.UPLOAD_GROUP_NO == uploadGroupNo);
                if (head != null)
                {
                    res.DataResponse = new DTO.VerifyDocumentHeader();
                    head.MappingToEntity(res.DataResponse);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetVerifyHeadByUploadGroupNo", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> CheckLicenseDetailVerifyHasNotApprove(string uploadGroupNo)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                int notApprove = base.ctx.AG_IAS_LICENSE_D.Where(w => w.UPLOAD_GROUP_NO == uploadGroupNo && w.APPROVED == "N" || w.APPROVED == "W").Count();
                if (notApprove > 0)
                {
                    res.ResultMessage = true;
                }
                else
                {
                    res.ResultMessage = false;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_CheckLicenseDetailVerifyHasNotApprove", ex);
            }
            return res;

        }

        public DTO.ResponseService<String> GenZipFileLicenseRequest(DateTime findDate, String username)
        {
            DTO.ResponseService<String> response = new ResponseService<string>();
            try
            {
                String fileZip = GenZipLicenseRequest.StartCompressByOicApprove(ctx, findDate, username, String.Empty);
                if (String.IsNullOrEmpty(fileZip))
                {
                    response.ErrorMsg = Resources.errorLicenseService_54;
                }

                response.DataResponse = CryptoBase64.Encryption(fileZip);
            }
            catch (ApplicationException appex)
            {
                response.ErrorMsg = appex.Message;
                LoggerFactory.CreateLog().Fatal("LicenseService_GenZipFileLicenseRequest", appex);
            }
            catch (Exception ex)
            {
                //    response.ErrorMsg = ex.Message;
                response.ErrorMsg = Resources.errorLicenseService_055;
                LoggerFactory.CreateLog().Fatal("LicenseService_GenZipFileLicenseRequest", ex);

            }


            return response;
        }

        public DTO.ResponseService<DataSet> GetListLicenseDetailByCriteria(string licenseNo, string licenseType,
                                 DateTime? startDate, DateTime? toDate,
                                 string paymentNo, string licenseTypeReceive,
                                 DTO.UserProfile userProfile,
                                 int pageNo, int recordPerPage, string PageCount)
        {
            var res = new DTO.ResponseService<DataSet>();

            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();
                // add by milk
                string critLicenseNo = !string.IsNullOrEmpty(licenseNo) ? " AND LD.LICENSE_NO Like '" + licenseNo + "%'" : "";
                string crit = licenseType.Length >= 2 ? " AND LH.LICENSE_TYPE_CODE = '" + licenseType + "'" : "";
                string firstCon = "select * from ( ";
                string midCon = (userProfile.CompCode == "") ? " like '%' " : " = '" + userProfile.CompCode + "' ";

                //var comCode = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(w => w.ID.Trim() == userProfile.Id.Trim()).COMP_CODE;

                string sql = string.Empty;
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                //-------------------------
                if (PageCount == "Y")
                {
                    sql = "select count(*) rowcount from ( " +
                                  "SELECT   LD.ID_CARD_NO , LD.TITLE_NAME , LD.NAMES , LD.LASTNAME , LD.APPROVED_DATE ,LD.LICENSE_NO,LD.LICENSE_DATE,LD.LICENSE_EXPIRE_DATE " +
                                  ",LD.APPROVED_BY, LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED ,LH.TRAN_DATE , LH.LICENSE_TYPE_CODE,LT.LICENSE_TYPE_NAME"
                                  + ",(Select Petition_Type_Name From AG_IAS_PETITION_TYPE_R  where PETITION_TYPE_CODE =  Lh.Petition_Type_Code) Petition_Name "
                                  + ",ROW_NUMBER() OVER (ORDER BY LD.ID_CARD_NO ASC) RUN_NO  " +
                                  "FROM AG_IAS_LICENSE_H LH, AG_IAS_LICENSE_D LD,AG_IAS_LICENSE_TYPE_R LT " +
                                  "WHERE LT.LICENSE_TYPE_CODE = LH.LICENSE_TYPE_CODE AND "
                                  + "LH.UPLOAD_GROUP_NO = LD.UPLOAD_GROUP_NO AND (LH.COMP_CODE  " + midCon + " OR LH.UPLOAD_BY_SESSION = '" + userProfile.CompCode.Trim() + "') "
                                  + " AND " + "To_char(LH.TRAN_DATE) BETWEEN TO_DATE('" + Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                                                       Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd') " + "  " + crit + critLicenseNo + " )A  ORDER BY  A.RUN_NO";
                }
                else
                {
                    sql = firstCon +
                                 "SELECT   LD.ID_CARD_NO , LD.TITLE_NAME , LD.NAMES , LD.LASTNAME , LD.APPROVED_DATE ,LD.LICENSE_NO,LD.LICENSE_DATE,LD.LICENSE_EXPIRE_DATE " +
                                 ",LD.APPROVED_BY, LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED ,LH.TRAN_DATE , LH.LICENSE_TYPE_CODE,LT.LICENSE_TYPE_NAME "
                                 + ",(Select Petition_Type_Name From AG_IAS_PETITION_TYPE_R  where PETITION_TYPE_CODE =  Lh.Petition_Type_Code) Petition_Name "
                                + ",ROW_NUMBER() OVER (ORDER BY LD.ID_CARD_NO ASC) RUN_NO  " +
                                 "FROM AG_IAS_LICENSE_H LH, AG_IAS_LICENSE_D LD,AG_IAS_LICENSE_TYPE_R LT " +
                                 "WHERE LT.LICENSE_TYPE_CODE = LH.LICENSE_TYPE_CODE AND LH.UPLOAD_GROUP_NO = LD.UPLOAD_GROUP_NO AND (LH.COMP_CODE  " + midCon + " OR LH.UPLOAD_BY_SESSION = '" + userProfile.CompCode.Trim() + "') "
                                 + " AND " + "To_char(LH.TRAN_DATE) BETWEEN TO_DATE('" + Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                                                      Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd') " + "  " + crit + critLicenseNo + " )A " + critRecNo;
                }
                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetListLicenseDetailByCriteria", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetResultLicenseVerifyHead(string petitionTypeCode,
                     DateTime? startDate,
                     DateTime? toDate, string Compcode, string CountPage, int pageNo, int recordPerPage)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();

                sb.Append(GetCriteria("LH.PETITION_TYPE_CODE = '{0}' AND ", petitionTypeCode));

                if (!string.IsNullOrEmpty(Compcode))
                {
                    sb.Append(" LH.COMP_CODE = " + Compcode + " AND ");
                }

                if (startDate != null && toDate != null)
                {
                    sb.Append("(to_char(LH.TRAN_DATE) BETWEEN TO_DATE('" +
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                }
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();

                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;
                string sql = string.Empty;
                if (CountPage == "Y")
                {
                    sql = "SELECT	(SELECT COUNT(*) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE " + crit + ") AS TOTAL , " +
                                 "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                                 "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                                 "         LH.UPLOAD_GROUP_NO , LH.FILENAME  , PT.PETITION_TYPE_NAME " +
                                 "FROM	    AG_IAS_PETITION_TYPE_R PT , " +
                                 "         AG_IAS_LICENSE_H	    LH, " +
                                 "         AG_LICENSE_TYPE_R	    LT " +
                                 "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	 " + crit;
                }
                else
                {
                    sql = "Select * from ( " +
                          "SELECT	(SELECT COUNT(*) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE " + crit + ") AS TOTAL , " +
                                "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                                "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                                "         LH.UPLOAD_GROUP_NO , LH.FILENAME  , PT.PETITION_TYPE_NAME,ROW_NUMBER() OVER (ORDER BY LH.COMP_CODE) RUN_NO " +
                                "FROM	    AG_IAS_PETITION_TYPE_R PT , " +
                                "         AG_IAS_LICENSE_H	    LH, " +
                                "         AG_LICENSE_TYPE_R	    LT " +
                                "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	 " + crit + ")A " + critRecNo;
                }
                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetResultLicenseVerifyHead", ex);
            }
            return res;
        }



        public DTO.ResponseService<DataSet> GetEditLicenseHead(string petitionTypeCode,
                    DateTime? startDate,
                    DateTime? toDate, string Compcode, string CountPage, int pageNo, int recordPerPage)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                #region SQL Statement

                StringBuilder sb = new StringBuilder();

                sb.Append(GetCriteria("LH.PETITION_TYPE_CODE = '{0}' AND ", petitionTypeCode));

                if (!string.IsNullOrEmpty(Compcode))
                {
                    sb.Append(" LH.COMP_CODE = " + Compcode + " AND ");
                }

                sb.Append(" LH.APPROVED_DOC = " + "'" + "W" + "'" + " AND ");

                if (startDate != null && toDate != null)
                {
                    sb.Append("(to_char(LH.TRAN_DATE) BETWEEN TO_DATE('" +
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                }
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();

                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;
                string sql = string.Empty;
                if (CountPage == "Y")
                {
                    sql = "SELECT	(SELECT COUNT(LH.LICENSE_TYPE_CODE) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE " + crit + ") AS TOTAL , " +
                                 "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                                 "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                                 "         LH.UPLOAD_GROUP_NO , LH.FILENAME  , PT.PETITION_TYPE_NAME " +
                                 "FROM	   AG_IAS_PETITION_TYPE_R PT , " +
                                 "         AG_IAS_LICENSE_H	    LH, " +
                                 "         AG_LICENSE_TYPE_R	    LT " +
                                 "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE AND LH.APPROVED_DOC = 'W'	 " + crit;
                }
                else
                {
                    sql = "Select * from ( " +
                          "SELECT	(SELECT COUNT(*) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE " + crit + ") AS TOTAL , " +
                                "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                                "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                                "         LH.UPLOAD_GROUP_NO , LH.FILENAME  , PT.PETITION_TYPE_NAME,ROW_NUMBER() OVER (ORDER BY LH.COMP_CODE) RUN_NO " +
                                "FROM	  AG_IAS_PETITION_TYPE_R PT , " +
                                "         AG_IAS_LICENSE_H	    LH, " +
                                "         AG_LICENSE_TYPE_R	    LT " +
                                "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE AND LH.APPROVED_DOC = 'W'	 " + crit + ")A " + critRecNo;
                }
                #endregion


                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetEditLicenseHead", ex);
            }
            return res;

        }


        public DTO.ResponseService<DataSet> GetListLicenseDetailByPersonal(string licenseNo, string licenseType,
                               DateTime? startDate, DateTime? toDate,
                               string paymentNo, string licenseTypeReceive,
                               DTO.UserProfile userProfile,
                               int pageNo, int recordPerPage, Boolean CountAgain)
        {
            var res = new DTO.ResponseService<DataSet>();

            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();

                string crit = licenseType.Length >= 2 ? " AND LH.LICENSE_TYPE_CODE = '" + licenseType + "'" : "";

                #region MILK
                string firstCon = string.Empty;
                string midCon = string.Empty;
                string lastCon = string.Empty;
                string sql = string.Empty;



                if (CountAgain)
                {
                    sql = " SELECT COUNT(*) rowcount FROM ( "
                    + "SELECT   LD.ID_CARD_NO , LD.TITLE_NAME , LD.NAMES , LD.LASTNAME , LD.APPROVED_DATE ,LD.LICENSE_NO,LD.LICENSE_DATE,LD.LICENSE_EXPIRE_DATE "
                    + " ,LD.APPROVED_BY, LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED ,LH.TRAN_DATE , LH.LICENSE_TYPE_CODE,LT.LICENSE_TYPE_NAME "
                    + "FROM AG_IAS_LICENSE_H LH, AG_IAS_LICENSE_D LD,AG_IAS_LICENSE_TYPE_R LT "
                    + "WHERE LT.LICENSE_TYPE_CODE = LH.LICENSE_TYPE_CODE AND LH.UPLOAD_GROUP_NO = LD.UPLOAD_GROUP_NO AND LD.ID_CARD_NO  =  '" + userProfile.IdCard + "') ";
                    //+ "ORDER BY A.RUN_NO ";
                }
                else
                {
                    firstCon = " SELECT * FROM (";
                    midCon = " , ROW_NUMBER() OVER (ORDER BY LD.UPLOAD_GROUP_NO) RUN_NO ";
                    lastCon = pageNo == 0
                                    ? "" :
                                    " Order By LD.UPLOAD_GROUP_NO ) A where A.RUN_NO BETWEEN " +
                                       pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +

                                       pageNo.ToRowNumber(recordPerPage).ToString() + " order by A.RUN_NO asc ";

                #endregion MILK
                    sql = firstCon + "SELECT   LD.ID_CARD_NO , LD.TITLE_NAME , LD.NAMES , LD.LASTNAME , LD.APPROVED_DATE ,LD.LICENSE_NO,LD.LICENSE_DATE,LD.LICENSE_EXPIRE_DATE " +
                        ",LD.APPROVED_BY, LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED ,LH.TRAN_DATE , LH.LICENSE_TYPE_CODE,LT.LICENSE_TYPE_NAME "
                        + ",(SELECT PETITION_TYPE_NAME FROM AG_IAS_PETITION_TYPE_R  WHERE PETITION_TYPE_CODE =  LH.PETITION_TYPE_CODE) PETITION_NAME "
                        + midCon +
                        "FROM AG_IAS_LICENSE_H LH, AG_IAS_LICENSE_D LD,AG_IAS_LICENSE_TYPE_R LT " +
                        "WHERE LT.LICENSE_TYPE_CODE = LH.LICENSE_TYPE_CODE AND LH.UPLOAD_GROUP_NO = LD.UPLOAD_GROUP_NO AND LD.ID_CARD_NO  =  '" + userProfile.IdCard + "' " + lastCon;

                #endregion
                }




                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetListLicenseDetailByPersonal", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetLicenseVerifyHeadByOIC(string petitionTypeCode,
                       DateTime? startDate,
                       DateTime? toDate, string requestCompCode, string CountPage, int pageNo, int recordPerPage, string StatusApprove)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();

                sb.Append(GetCriteria("LH.PETITION_TYPE_CODE = '{0}' AND ", petitionTypeCode));

                if (!string.IsNullOrEmpty(requestCompCode))
                {
                    //sb.Append(" LH.COMP_CODE = " + requestCompCode + " AND ");
                }
                if (!string.IsNullOrEmpty(StatusApprove))
                {
                    if (StatusApprove == "0")
                    {

                    }
                    else
                    {
                        sb.Append(" LH.APPROVED_DOC = '" + StatusApprove + "' AND ");
                    }
                    //sb.Append(" LH.COMP_CODE = " + requestCompCode + " AND ");
                }
                if (startDate != null && toDate != null)
                {
                    sb.Append("(to_char(LH.TRAN_DATE) BETWEEN TO_DATE('" +
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                }
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                string sql = string.Empty;

                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;
                if (CountPage == "Y")
                {
                    sql = "SELECT	(SELECT COUNT(*) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE " + crit + ") AS TOTAL , " +
                                  "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                                  "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                                  "         LH.UPLOAD_GROUP_NO , substr(substr(LH.FILENAME,Instr(LH.FILENAME,'\')+1),Instr(substr(LH.FILENAME,Instr(LH.FILENAME,'\')+1),'\')+1) FILENAME  , PT.PETITION_TYPE_NAME,case LENGTH(lh.upload_by_session) when 15  then T.NAMES ||'  '|| T.LASTNAME  when 4 then com.name else asso.association_name  end as names " +
                                  "FROM	    AG_IAS_PETITION_TYPE_R PT , " +
                                  "         AG_LICENSE_TYPE_R	    LT, " +
                                  "         AG_IAS_LICENSE_H	    LH " +
                                  "left join AG_IAS_PERSONAL_T T on lh.upload_by_session=T.ID " +
                                  "left join VW_IAS_COM_CODE COM on LH.upload_by_session =COM.ID " +
                                  "left join AG_IAS_ASSOCIATION ASSO on LH.upload_by_session=ASSO.association_code " +
                                  "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	" + " " + crit +
                                  " order by LH.TRAN_DATE";
                }
                else
                {
                    sql = "Select * from ( " +
                        "SELECT	(SELECT COUNT(*) FROM AG_IAS_LICENSE_H LH , AG_LICENSE_TYPE_R LT WHERE  LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE " + crit + ") AS TOTAL , " +
                      "         LH.COMP_CODE , LH.COMP_NAME , LH.APPROVED_DOC , " +
                      "		    LH.TRAN_DATE, LT.LICENSE_TYPE_NAME, LT.LICENSE_TYPE_CODE , LH.LOTS , " +
                      "         LH.UPLOAD_GROUP_NO , substr(substr(LH.FILENAME,Instr(LH.FILENAME,'\\')+1),Instr(substr(LH.FILENAME,Instr(LH.FILENAME,'\\')+1),'\\')+1) FILENAME  , pt.petition_type_code, PT.PETITION_TYPE_NAME,ROW_NUMBER() OVER (ORDER BY LH.TRAN_DATE) RUN_NO,case LENGTH(lh.upload_by_session) when 15  then T.NAMES ||'  '|| T.LASTNAME  when 4 then com.name else asso.association_name  end as names " +
                      "FROM	    AG_IAS_PETITION_TYPE_R PT , " +
                      "         AG_LICENSE_TYPE_R	    LT, " +
                      "         AG_IAS_LICENSE_H	    LH " +
                      "left join AG_IAS_PERSONAL_T T on lh.upload_by_session=T.ID " +
                      "left join VW_IAS_COM_CODE COM on LH.upload_by_session =COM.ID " +
                      "left join AG_IAS_ASSOCIATION ASSO on LH.upload_by_session=ASSO.association_code " +
                      "WHERE	PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE AND LH.LICENSE_TYPE_CODE	=	LT.LICENSE_TYPE_CODE	" +
                      "         " + crit + " order by LH.TRAN_DATE)A " + critRecNo;
                }
                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseVerifyHeadByOIC", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetListLicenseDetailAdminByCriteria(string licenseNo, string licenseType,
                              DateTime? startDate, DateTime? toDate,
                              string paymentNo, string licenseTypeReceive,
                              DTO.UserProfile userProfile,
                              int pageNo, int recordPerPage, string PageCount)
        {
            var res = new DTO.ResponseService<DataSet>();

            try
            {
                #region SQL Statement

                StringBuilder sb = new StringBuilder();
                // add by milk
                string crit = licenseType.Length >= 2 ? " AND LH.LICENSE_TYPE_CODE = '" + licenseType + "'" : "";
                string firstCon = "select * from ( ";

                string sql = string.Empty;
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                //-------------------------
                if (PageCount == "Y")
                {
                    sql = "select count(*) rowcount from ( " +
                                  "SELECT   LD.ID_CARD_NO , LD.TITLE_NAME , LD.NAMES , LD.LASTNAME , LD.APPROVED_DATE ,LD.LICENSE_NO,LD.LICENSE_DATE,LD.LICENSE_EXPIRE_DATE " +
                                  ",LD.APPROVED_BY, LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED ,LH.TRAN_DATE , LH.LICENSE_TYPE_CODE,LT.LICENSE_TYPE_NAME "
                                  + ",(Select Petition_Type_Name From AG_IAS_PETITION_TYPE_R  where PETITION_TYPE_CODE =  Lh.Petition_Type_Code) Petition_Name "
                                  + ",ROW_NUMBER() OVER (ORDER BY LD.ID_CARD_NO ASC) RUN_NO  " +
                                  "FROM AG_IAS_LICENSE_H LH, AG_IAS_LICENSE_D LD,AG_IAS_LICENSE_TYPE_R LT " +
                                  "WHERE LT.LICENSE_TYPE_CODE = LH.LICENSE_TYPE_CODE AND LH.UPLOAD_GROUP_NO = LD.UPLOAD_GROUP_NO AND  " +
                                  "To_char(LH.TRAN_DATE) BETWEEN TO_DATE('" + Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                                                       Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd') " + "  " + crit + " )A  ORDER BY  A.RUN_NO";
                }
                else
                {
                    sql = firstCon +
                                 "SELECT   LD.ID_CARD_NO , LD.TITLE_NAME , LD.NAMES , LD.LASTNAME , LD.APPROVED_DATE ,LD.LICENSE_NO,LD.LICENSE_DATE,LD.LICENSE_EXPIRE_DATE " +
                                 ",LD.APPROVED_BY, LD.UPLOAD_GROUP_NO, LD.SEQ_NO , LD.APPROVED ,LH.TRAN_DATE , LH.LICENSE_TYPE_CODE,LT.LICENSE_TYPE_NAME "
                                 + ",(Select Petition_Type_Name From AG_IAS_PETITION_TYPE_R  where PETITION_TYPE_CODE =  Lh.Petition_Type_Code) Petition_Name "
                                 + ",ROW_NUMBER() OVER (ORDER BY LD.ID_CARD_NO ASC) RUN_NO  " +
                                 "FROM AG_IAS_LICENSE_H LH, AG_IAS_LICENSE_D LD,AG_IAS_LICENSE_TYPE_R LT " +
                                 "WHERE LT.LICENSE_TYPE_CODE = LH.LICENSE_TYPE_CODE AND LH.UPLOAD_GROUP_NO = LD.UPLOAD_GROUP_NO AND  " +
                                 "To_char(LH.TRAN_DATE) BETWEEN TO_DATE('" + Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                                                      Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd') " + "  " + crit + " )A " + critRecNo;
                }
                #endregion

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetListLicenseDetailAdminByCriteria", ex);
            }
            return res;
        }

        public ResponseService<List<DTO.GBHoliday>> GETGBHoliday(string date)
        {
            var res = new ResponseService<List<DTO.GBHoliday>>();

            try
            {

                IAS.DAL.IASGBModelEntities ctxGB = new IASGBModelEntities();
                var result = (from gb in ctxGB.GB_HOLIDAY_R
                              where gb.HL_DESC != date
                              select new DTO.GBHoliday
                                 {
                                     HL_DATE = gb.HL_DATE,
                                     HL_DESC = gb.HL_DESC

                                 }).ToList();

                if (result.Count > 0)
                {
                    res.DataResponse = result;
                }

            }
            catch (Exception ex)
            {

                throw;

            }
            return res;

        }

        public ResponseService<List<DTO.ValidateLicense>> GetPropLiecense(string licenseType, string pettionType, Int32 renewTime, Int32 groupId)
        {
            var res = new ResponseService<List<DTO.ValidateLicense>>();
            try
            {

                var result = (from a in ctx.AG_IAS_VALIDATE_LICENSE
                              join b in ctx.AG_IAS_VALIDATE_LICENSE_CON on a.ID equals b.ID
                              where
                                  b.LICENSE_TYPE_CODE.Trim() == licenseType.Trim()
                                  && b.PETITION_TYPE_CODE.Trim() == pettionType.Trim()
                                  && b.ITEM_GROUP == groupId
                                  && a.STATUS == "A" && b.STATUS != "N"
                              select new DTO.ValidateLicense
                              {
                                  ID = a.ID,
                                  ITEM = a.ITEM
                              }).Distinct().OrderBy(s => s.ID).ToList();


                res.DataResponse = result;
            }
            catch (Exception ex)
            {

                LoggerFactory.CreateLog().Fatal("LicenseService_GetDetailbyGroupNo", ex);
            }


            return res;

        }

        public DTO.ResponseMessage<bool> ChkSpecialExam(List<string> fileType, string licenseType)
        {

            var res = new DTO.ResponseMessage<bool>();
            try
            {

                var tagUser = new string[] { "" };
                var agenType = ctx.AG_LICENSE_TYPE_R.SingleOrDefault(a => a.LICENSE_TYPE_CODE.Trim() == licenseType.Trim());

                //ตรวจสอบวุฒิการศึกษานำไปแทนผลการสอบ
                if (agenType.INSURANCE_TYPE == "1")
                {
                    tagUser = new string[] { "L", "B" };
                }
                else if (agenType.INSURANCE_TYPE == "2")
                {
                    tagUser = new string[] { "D", "B" };
                }
                else
                {
                    tagUser = new string[] { "B" };
                }


                var disExam = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                               from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                               where
                               dt.SPECIAL_TYPE_CODE_EXAM == ex.SPECIAL_TYPE_CODE
                               && dt.EXAM_DISCOUNT_STATUS == "Y"
                               && tagUser.Contains(ex.USED_TYPE.Trim())
                               && dt.STATUS == "A"
                               select dt.DOCUMENT_CODE).ToList();



                var eduFile = (from a in fileType
                               where disExam.Contains(a)
                               select a).FirstOrDefault();

                if (eduFile == null)
                {
                    res.ResultMessage = false;
                }
                else
                {
                    res.ResultMessage = true;
                }


            }
            catch (Exception ex)
            {

                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_chkSpecialExam", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<int> GetRenewTimebyLicenseNo(string licenseNo)
        {
            var res = new DTO.ResponseMessage<int>();
            int reNewTime = 0;
            var sql = string.Empty;
            OracleDB ora = new OracleDB();


            try
            {
                //var renewTime = (from r in ctx.AG_LICENSE_RENEW_T
                //                 where r.LICENSE_NO.Trim() == licenseNo.Trim()
                //                 select r.RENEW_TIME).Max();


                sql = "SELECT MAX(RENEW_TIME) "
                    + "FROM AG_LICENSE_RENEW_T  "
                    + "WHERE LICENSE_NO = '" + licenseNo.Trim() + "' ";


                DataTable dt = ora.GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    int reNew = dr[0].ToInt();

                    reNewTime = reNew;

                }
                else
                {
                    reNewTime = 0;
                }





                res.ResultMessage = reNewTime;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetRenewTimebyLicenseNo", ex);
                throw;
            }

            return res;
        }

        public ResponseMessage<bool> CheckSpecial(string idCard)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {

                var ls = base.ctx.AG_IAS_SPECIAL_T_TEMP
                                     .Where(w => w.ID_CARD_NO == idCard.Trim())
                                     .ToList();
                if (ls.Count > 0)
                {
                    res.ResultMessage = true;
                }
                else
                {
                    res.ResultMessage = false;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }

            return res;

        }

        public ResponseService<List<DTO.TrainSpecial>> GetSpecialTempTrainById(string idCard)
        {
            DTO.ResponseService<List<DTO.TrainSpecial>> res = new DTO.ResponseService<List<DTO.TrainSpecial>>();
            try
            {
                var ls = (from ST in base.ctx.AG_IAS_SPECIAL_T_TEMP
                          join TS in base.ctx.AG_TRAIN_SPECIAL_R on ST.SPECIAL_TYPE_CODE equals TS.SPECIAL_TYPE_CODE
                          where ST.TRAIN_DISCOUNT_STATUS == "Y"
                          && ST.ID_CARD_NO == idCard
                          select new DTO.TrainSpecial
                          {
                              SPECIAL_TYPE_CODE = TS.SPECIAL_TYPE_CODE,
                              SPECIAL_TYPE_DESC = TS.SPECIAL_TYPE_DESC
                          }).ToList();
                res.DataResponse = ls;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        public ResponseService<List<DTO.ExamSpecial>> GetSpecialTempExamById(string idCard)
        {
            DTO.ResponseService<List<DTO.ExamSpecial>> res = new DTO.ResponseService<List<DTO.ExamSpecial>>();
            try
            {
                var ls = (from ST in base.ctx.AG_IAS_SPECIAL_T_TEMP
                          join TS in base.ctx.AG_IAS_EXAM_SPECIAL_R on ST.SPECIAL_TYPE_CODE equals TS.SPECIAL_TYPE_CODE
                          where ST.EXAM_DISCOUNT_STATUS == "Y"
                          && ST.ID_CARD_NO == idCard
                          select new DTO.ExamSpecial
                          {
                              SPECIAL_TYPE_CODE = TS.SPECIAL_TYPE_CODE,
                              SPECIAL_TYPE_DESC = TS.SPECIAL_TYPE_DESC
                          }).ToList();
                res.DataResponse = ls;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        public DTO.ResponseMessage<bool> ValidateProp(string groupId)
        {
            var res = new DTO.ResponseMessage<bool>();

            long lgroupID = Convert.ToInt64(groupId);

            int renewTime = 0;

            StringBuilder messageError = new StringBuilder("");

            try
            {

                #region GetData

                //GetDataHead
                var head = base.ctx.AG_IAS_IMPORT_HEADER_TEMP
                       .Where(w => w.IMPORT_ID == lgroupID).FirstOrDefault();

                //GeDataDetail
                var detail = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                    .Where(w => w.IMPORT_ID == lgroupID).ToList();


                string petitionType = head.PETTITION_TYPE;
                string licenseType = head.LICENSE_TYPE_CODE;
                string comCode = head.COMP_CODE;

                #endregion


                foreach (var licenseDetail in detail)
                {
                    messageError = new StringBuilder("");

                    //ตรวจสอบขอใหม่
                    #region chkNewLicense

                    if (petitionType == "11")
                    {
                        messageError = ValidateLicenseNew(licenseDetail, licenseType, petitionType);
                    }

                    #endregion

                    //ตรวจสอบขออายุ 1 ปี
                    #region chkRenewLicense 1 year

                    else if (petitionType == "13")
                    {
                        messageError = ValidateRenewLicense(licenseDetail, licenseType, petitionType);
                    }

                    #endregion

                    //ตรวจสอบขอใบอนุญาตครั้งที่ 5 ปี
                    #region chkRenew 5 year


                    else if (petitionType == "14")
                    {
                        DTO.ResponseService<List<SpecialDocument>> docSpecial = GetSpecialDocType("A", "Y");

                        if (licenseDetail.SPECIAL_TYPE_CODE == null)
                        {
                            licenseDetail.SPECIAL_TYPE_CODE = "";
                        }

                        var resDocSpecial = docSpecial.DataResponse.Where(x => licenseDetail.SPECIAL_TYPE_CODE.Contains(x.SPECIAL_TYPE_CODE_TRAIN)).FirstOrDefault();

                        if (resDocSpecial == null)
                        {
                            renewTime = GetRenewTime(licenseDetail.LICENSE_NO);
                            renewTime = renewTime + 1;

                            if (renewTime >= 4)
                            {
                                renewTime = 4;
                            }

                            List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, renewTime);

                            if (tagGetProperty.Contains("4"))
                            {
                                DTO.ResponseService<DTO.TrainPersonHistory> result = new ResponseService<TrainPersonHistory>();

                                result = GetPersonalTrainByCriteria(licenseType, petitionType, renewTime.ToString(), licenseDetail.CITIZEN_ID, licenseDetail.LICENSE_NO, licenseDetail.SPECIAL_TYPE_CODE);
                                if (result.DataResponse.RESULT == "ไม่ผ่าน")
                                {
                                    messageError.Append("4" + "<br />");
                                }
                            }
                        }

                        //if (licenseDetail.SPECIAL_TYPE_CODE != "10006")
                        //{
                        //    renewTime = GetRenewTime(licenseDetail.LICENSE_NO);
                        //    renewTime = renewTime + 1;

                        //    if (renewTime >= 4)
                        //    {
                        //        renewTime = 4;
                        //    }

                        //    List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, renewTime);


                        //    if (tagGetProperty.Contains("4"))
                        //    {
                        //        DTO.ResponseService<DTO.TrainPersonHistory> result = new ResponseService<TrainPersonHistory>();

                        //        result = GetPersonalTrainByCriteria(licenseType, petitionType, renewTime.ToString(), licenseDetail.CITIZEN_ID, licenseDetail.LICENSE_NO);
                        //        if (result.DataResponse.RESULT == "ไม่ผ่าน")
                        //        {
                        //            messageError.Append("4" + "<br />");
                        //        }
                        //    }
                        //}
                    }

                    #endregion

                    //ตรวจสอบขาดต่อขอใหม่
                    #region chkOldNew

                    else if (petitionType == "15")
                    {
                        messageError = ValidateOldnewLicense(licenseDetail, licenseType, petitionType, comCode);
                    }

                    #endregion

                    //ตรวจสอบใบแทนใบอนุญาต
                    #region chkOtherLicense

                    else if (petitionType == "16")
                    {
                        messageError = ValidateOtherLicense(licenseDetail, licenseType, petitionType);
                    }

                    #endregion

                    //ตรวจสอบย้ายบริษัท
                    #region chkMove

                    else if (petitionType == "17")
                    {
                        messageError = ValidateMove(licenseDetail, licenseType, petitionType, comCode);


                    }

                    #endregion

                    //ตรวจสอบใบอนุญาตใบที่สอง
                    #region chkSecond

                    else if (petitionType == "18")
                    {
                        messageError = ValidateSecond(licenseDetail, licenseType, petitionType, comCode);

                    }

                    #endregion

                    //อัพเดต ErrMassage
                    #region UpdateErrMassage

                    var ent = new AG_IAS_IMPORT_DETAIL_TEMP();

                    ent = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                        .Where(w => w.IMPORT_ID == lgroupID && w.CITIZEN_ID.Trim() == licenseDetail.CITIZEN_ID.Trim() && w.SEQ == licenseDetail.SEQ).FirstOrDefault();

                    if (messageError.Length != 0 && messageError != null)
                    {
                        ent.ERR_MSG = messageError.ToString();
                    }

                    #endregion


                }
                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                //ผลการตรวจสอบคุณสมบัติ
                #region ResultMessage

                var details = base.ctx.AG_IAS_IMPORT_DETAIL_TEMP
                                  .Where(w => w.IMPORT_ID == lgroupID)
                                  .ToList();



                int hasError = details.Where(delegate(AG_IAS_IMPORT_DETAIL_TEMP temp)
                {
                    return !string.IsNullOrEmpty(temp.ERR_MSG);
                }).Count();


                if (hasError > 0)
                {
                    res.ResultMessage = false;
                }
                else
                {

                    res.ResultMessage = true;
                }

                #endregion



            }
            catch (ApplicationException appex)
            {
                res.ErrorMsg = Resources.errorLicenseService_024;
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateProp", appex);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorLicenseService_024;
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateProp", ex);
                throw;
            }



            return res;
        }

        private StringBuilder ValidateLicenseNew(AG_IAS_IMPORT_DETAIL_TEMP detail, string licenseType, string petitionType)
        {
            var res = new StringBuilder("");

            string importId = detail.IMPORT_ID.ToString();

            var dateNow = DateTime.Now;


            string idValue = string.Empty;

            var sql = string.Empty;
            OracleDB ora = new OracleDB();


            // GetPropLiecense
            try
            {

                #region GetData


                //เอกสารแนบ
                var AttachFileDetails = base.ctx.AG_IAS_ATTACH_FILE_LICENSE
                .Where(w => w.GROUP_LICENSE_ID == importId && w.ID_CARD_NO == detail.CITIZEN_ID).ToList();

                //ประเภทประกันชีวิต
                var tagLife = GetLiftInsurance();

                //ประเภทประกันวินาศภัย
                var tagCasualty = GetCasualtyInsurance();

                //ประเภทตัวแทน
                var agenType = ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(a => a.LICENSE_TYPE_CODE.Trim() == licenseType.Trim());


                //คุณสมบัติที่ตรวจสอบ
                List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, 0);

                #endregion

                //ตรวจสอบบรรลุนิติภาวะ
                #region chkMajority

                if (tagGetProperty.Contains("1"))
                {


                    string marType = DTO.DocumentLicenseType.Marriage_license.GetEnumValue().ToString();

                    var marFile = (from a in AttachFileDetails
                                   where marType.Contains(a.ATTACH_FILE_TYPE)
                                   select a.ATTACH_FILE_TYPE).FirstOrDefault();


                    if (string.IsNullOrEmpty(marFile))
                    {
                        var chkMajority = ChkMarjority(detail.CITIZEN_ID, licenseType, AttachFileDetails);

                        if (!chkMajority)
                        {
                            res.Append("1" + "<br />");
                        }
                    }
                }




                #endregion

                //ตรวจสอบรหัสพื้นที่
                #region AreaCode

                if (tagGetProperty.Contains("6"))
                {
                    var chkArea = new bool();

                    chkArea = ChkAreaCode(detail.CUR_AREA_CODE);

                    if (!chkArea)
                    {
                        res.Append("6" + "<br />");
                    }
                }

                #endregion

                //ตรวจสอบ license ประเภทประกันชีวิต
                #region chk Life Insurance


                // 01 ต้องไม่เป็น 03
                if (tagLife.Contains(licenseType))
                {
                    if (tagGetProperty.Contains("10") || tagGetProperty.Contains("11"))
                    {
                        sql = "SELECT lt.LICENSE_NO,lt.REVOKE_TYPE_CODE "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE  lt.RECORD_STATUS is  null "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.EXPIRE_DATE >= sysdate "
                              + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                              + "and lt.LICENSE_TYPE_CODE in ('01','03','07') "
                              + "group by lt.LICENSE_NO,lt.REVOKE_TYPE_CODE";


                        DataTable chkActive = ora.GetDataTable(sql);

                        if (chkActive != null && chkActive.Rows.Count > 0)
                        {

                            if (agenType.AGENT_TYPE == "A")
                            {
                                res.Append("10" + "<br />");
                            }
                            else
                            {
                                res.Append("11" + "<br />");
                            }

                        }
                        else
                        {
                            sql = "SELECT lt.LICENSE_NO "
                                  + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                  + "WHERE lt.RECORD_STATUS is null "
                                  + " and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                  + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                  + "and lt.EXPIRE_DATE < sysdate "
                                  + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                                  + "and lt.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                                  + "group by lt.LICENSE_NO";


                            DataTable NotActive = ora.GetDataTable(sql);
                            if (NotActive != null && NotActive.Rows.Count > 0)
                            {
                                if (agenType.AGENT_TYPE == "A")
                                {
                                    res.Append("10" + "<br />");
                                }
                                else
                                {
                                    res.Append("11" + "<br />");
                                }
                            }

                        }

                    }


                    if (tagGetProperty.Contains("13"))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี
                        string dateOldst;

                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }

                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE = 'B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('01','03','07') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("13" + "<br />");
                        }

                    }

                }

                #endregion

                //ตรวจสอบ license ประเภทประกันวินาศภัย
                #region chk casualty insurance

                else if (tagCasualty.Contains(licenseType))
                {
                    if (tagGetProperty.Contains("12") || tagGetProperty.Contains("15"))
                    {
                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE  lt.RECORD_STATUS is null "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.EXPIRE_DATE >= sysdate "
                              + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                              + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkActive = ora.GetDataTable(sql);

                        if (chkActive != null && chkActive.Rows.Count > 0)
                        {
                            if (agenType.AGENT_TYPE == "A")
                            {
                                res.Append("15" + "<br />");
                            }
                            else
                            {
                                res.Append("12" + "<br />");
                            }
                        }
                        else
                        {
                            sql = "SELECT lt.LICENSE_NO "
                                  + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                  + "WHERE lt.RECORD_STATUS is null "
                                  + " and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                  + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                  + "and lt.EXPIRE_DATE < sysdate "
                                  + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                                  + "and lt.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                                  + "group by lt.LICENSE_NO";

                            DataTable NotActive = ora.GetDataTable(sql);

                            if (NotActive != null && NotActive.Rows.Count > 0)
                            {
                                if (agenType.AGENT_TYPE == "A")
                                {
                                    res.Append("15" + "<br />");
                                }
                                else
                                {
                                    res.Append("12" + "<br />");
                                }
                            }


                        }
                    }

                    if (tagGetProperty.Contains("16"))
                    {

                        //chkBacklist
                        //กลับไป 5 ปี
                        string dateOldst;

                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }

                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE = 'B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("16" + "<br />");
                        }
                    }
                }





                #endregion


                //ตรวจสอบ license 11,12
                #region chkLicenseType11,12


                else if (licenseType == "11")
                {
                    if (tagGetProperty.Contains("26"))
                    {
                        sql = "SELECT lt.LICENSE_NO "
                            + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                            + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                            + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                            + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                            + "and lt.EXPIRE_DATE >= sysdate "
                            + "and lt.RECORD_STATUS is null "
                            + "and lt.LICENSE_TYPE_CODE = '04' "
                            + "group by lt.LICENSE_NO";

                        DataTable chkActive = ora.GetDataTable(sql);

                        if (chkActive.Rows.Count == 0)
                        {
                            res.Append("26" + "<br />");
                        }

                    }
                }
                else if (licenseType == "12")
                {
                    if (tagGetProperty.Contains("27"))
                    {
                        sql = "SELECT lt.LICENSE_NO "
                          + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                          + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                          + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                          + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                          + "and lt.EXPIRE_DATE >= sysdate "
                          + "and lt.RECORD_STATUS is null "
                          + "and lt.LICENSE_TYPE_CODE = '03' "
                          + "group by lt.LICENSE_NO";

                        DataTable chkActive = ora.GetDataTable(sql);

                        if (chkActive.Rows.Count == 0)
                        {
                            res.Append("27" + "<br />");
                        }
                    }
                }

                #endregion


                //ตรวจสอบผลสอบ
                #region chkExamResult
                if (tagGetProperty.Contains("2"))
                {
                    var chkExam = ChkExamResult(detail.CITIZEN_ID, licenseType, AttachFileDetails);
                    if (!chkExam)
                    {
                        res.Append("2" + "<br />");
                    }
                }
                #endregion


                //ตรวจสอบผลอบรม
                #region chkTrainResult
                if (tagGetProperty.Contains("4"))
                {


                    if (agenType.AGENT_TYPE != "B" && licenseType != "11" && licenseType != "12")
                    {
                        int renewTime = 0;

                        var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                        if (!chkTrain)
                        {
                            res.Append("4" + "<br />");
                        }

                    }
                    //เป็นตัวแทนไปอ่าน Config ว่าตรวจสอบผลอบรมหรือไม่
                    else if (agenType.AGENT_TYPE == "B" && licenseType != "11" && licenseType != "12")
                    {

                        if (licenseType == "03")
                        {
                            idValue = DTO.ConfigAgenType.AgentLife.GetEnumValue().ToString();
                        }
                        else if (licenseType == "04")
                        {
                            idValue = DTO.ConfigAgenType.AgentCasualty.GetEnumValue().ToString();
                        }

                        var result = GetAgentCheckTrain(idValue);

                        if (result.ResultMessage == true)
                        {
                            int renewTime = 0;

                            var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);


                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }
                    }

                }

                #endregion

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateLicenseNew", ex);
                throw ex;
            }
            return res;
        }

        private List<string> GetChkPropLiecense(string licenseType, string petitionType, int renewTime)
        {
            var res = new List<string>();
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {
                sql = "SELECT a.ID "
                      + "FROM AG_IAS_VALIDATE_LICENSE a,AG_IAS_VALIDATE_LICENSE_CON b "
                      + "WHERE a.id = b.id "
                      + "AND b.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                      + "AND b.PETITION_TYPE_CODE = '" + petitionType.Trim() + "' "
                      + "AND b.STATUS = 'A' "
                      + "AND a.STATUS = 'A' ";

                DataTable dt = ora.GetDataTable(sql);

                if (dt.Rows.Count == 0)
                {
                    res = null;
                }
                else
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        res.Add(dr["ID"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return res;
        }

        private StringBuilder ValidateRenewLicense(AG_IAS_IMPORT_DETAIL_TEMP detail, string licenseType, string petitionType)
        {
            var res = new StringBuilder("");

            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            string importId = detail.IMPORT_ID.ToString();
            int renewTime = 0;

            var tagCasualty = new string[] { "02", "05", "06", "08" };
            var tagAgen = new string[] { "03", "04" };
            var dateNow = DateTime.Now;
            string idValue = string.Empty;


            try
            {
                #region GetData

                //GetRenewTime
                renewTime = 0;
                renewTime = GetRenewTime(detail.LICENSE_NO);
                renewTime = renewTime + 1;

                //คุณสมบัติที่ตรวจสอบ
                List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, renewTime);



                #endregion

                //ตรวจสอบผลอบรม
                #region chkTrain

                if (licenseType == "01")
                {
                    if (tagGetProperty.Contains("4"))
                    {

                        sql = "SELECT lt.LICENSE_NO "
                                 + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                 + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                                 + "and lt.LICENSE_NO <> '" + detail.LICENSE_NO.Trim() + "' "
                                 + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                 + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                 + "and lt.RECORD_STATUS is null "
                                 + "and lt.LICENSE_TYPE_CODE ='01' "
                                 + "group by lt.LICENSE_NO";

                        DataTable chkOtherLicense = ora.GetDataTable(sql);

                        if (chkOtherLicense.Rows.Count == 0)
                        {
                            var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }
                        else
                        {
                            var chkTrain = ChkTrainRenew(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }

                        }

                    }

                }
                else if (licenseType == "07")
                {
                    if (tagGetProperty.Contains("4"))
                    {
                        sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO <> '" + detail.LICENSE_NO.Trim() + "' "
                                + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                + "and lt.EXPIRE_DATE >= sysdate "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE in ('01','07') "
                                + "group by lt.LICENSE_NO";

                        DataTable chkOtherLicense = ora.GetDataTable(sql);

                        if (chkOtherLicense.Rows.Count == 0)
                        {
                            var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }
                        else
                        {
                            var chkTrain = ChkTrainRenew(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }
                    }

                }
                else if (tagCasualty.Contains(licenseType))
                {
                    if (tagGetProperty.Contains("4"))
                    {
                        sql = "SELECT lt.LICENSE_NO "
                                 + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                 + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                                 + "and lt.LICENSE_NO <> '" + detail.LICENSE_NO.Trim() + "' "
                                 + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                 + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                 + "and lt.EXPIRE_DATE >= sysdate "
                                 + "and lt.RECORD_STATUS is null "
                                 + "and lt.LICENSE_TYPE_CODE in ('02','05','06','08') "
                                 + "group by lt.LICENSE_NO";

                        DataTable dtchkOtherLicense = ora.GetDataTable(sql);


                        if (dtchkOtherLicense.Rows.Count == 0)
                        {
                            var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }
                        else
                        {
                            var chkTrain = ChkTrainRenew(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }

                    }

                }
                else if (tagAgen.Contains(licenseType))
                {
                    if (tagGetProperty.Contains("4"))
                    {
                        var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                        if (!chkTrain)
                        {
                            res.Append("4" + "<br />");
                        }
                    }
                }
                else
                {
                    if (tagGetProperty.Contains("4"))
                    {
                        var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                        if (!chkTrain)
                        {
                            res.Append("4" + "<br />");
                        }
                    }
                }


                #endregion

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateRenewLicense", ex);
                throw ex;
            }
            return res;
        }

        private StringBuilder ValidateRenewLicese_4(string idCardNo, string licenseType, string petitionType)
        {
            var res = new StringBuilder("");
            var sql = string.Empty;
            OracleDB ora = new OracleDB();
            //string importId = detail.IMPORT_ID.ToString();

            try
            {
                string yearNow = DateTime.Now.Year.ToString();



                #region chkTrain

                sql = "SLECT PERIOD_HH "
                     + "FROM AG_TRAIN_PERIOD_R "
                     + "WHER YEAR_Y = '" + yearNow + "' "
                     + "AND LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                     + "AND TRAIN_TYPE = 'T' ";


                DataTable dtTrainPeriod = ora.GetDataTable(sql);

                if (dtTrainPeriod != null && dtTrainPeriod.Rows.Count > 0)
                {
                    DataRow drTrainPeriod = dtTrainPeriod.Rows[0];
                    int trainPeriod = drTrainPeriod.ToInt();


                    //sql =  "SELECT TRAIN_PERIOD "
                    //      +"FROM TRAIN_PERIOD "
                    //      +"WHERE 









                }



                //if (chkTrain.Rows.Count == 0)
                //{

                //}




                #endregion


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return res;

        }

        private StringBuilder ValidateOldnewLicense(AG_IAS_IMPORT_DETAIL_TEMP detail, string licenseType, string petitionType, string comCode)
        {
            var res = new StringBuilder("");
            int renewTime = 0;
            int trainHour = 0;


            var sql = string.Empty;
            OracleDB ora = new OracleDB();
            string idValue = string.Empty;


            var dateNow = DateTime.Now;

            try
            {

                #region GetData

                //var ChkActive = base.ctx.AG_LICENSE_T.FirstOrDefault(l => l.LICENSE_NO == detail.LICENSE_NO && l.LICENSE_TYPE_CODE == licenseType);

                var tagLife = GetLiftInsurance();

                var tagCasualty = GetCasualtyInsurance();

                //คุณสมบัติที่ตรวจสอบ
                List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, 0);


                #endregion

                //ตรวจสอบใบอนุญาตต้องไม่ Active
                #region chkActive and chkNotActive

                if (tagGetProperty.Contains("22"))
                {
                    // ประกันชีวิต chkActive license ที่เกี่ยวข้อง
                    if (tagLife.Contains(licenseType.Trim()))
                    {
                        sql = "SELECT lt.LICENSE_NO,lt.REVOKE_TYPE_CODE "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE  lt.RECORD_STATUS is  null "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.EXPIRE_DATE >= sysdate "
                              + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                              + "and lt.LICENSE_TYPE_CODE in ('01','03','07') "
                              + "group by lt.LICENSE_NO,lt.REVOKE_TYPE_CODE";


                        DataTable chkActive = ora.GetDataTable(sql);

                        if (chkActive != null && chkActive.Rows.Count > 0)
                        {
                            res.Append("22" + "<br />");
                        }
                        //chkNotActive
                        else
                        {
                            sql = "SELECT lt.LICENSE_NO LICENSE_NO,lt.REVOKE_TYPE_CODE  REVOKE_TYPE_CODE "
                                  + "FROM AG_LICENSE_T lt "
                                  + "WHERE lt.RECORD_STATUS is null "
                                  + "and   lt.EXPIRE_DATE < sysdate "
                                  + "and   lt.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                                  + "and   lt.LICENSE_NO = '" + detail.LICENSE_NO.Trim() + "' "
                                  + "group by lt.LICENSE_NO,lt.REVOKE_TYPE_CODE";

                            DataTable chkNotActive = ora.GetDataTable(sql);

                            if (chkNotActive.Rows.Count > 0)
                            {
                                DataRow dr = chkNotActive.Rows[0];
                                string revokeCode = dr["REVOKE_TYPE_CODE"].ToString();

                                if (revokeCode != "B" && !string.IsNullOrEmpty(revokeCode))
                                {
                                    res.Append("22" + "<br />");
                                }
                            }
                            else if (chkNotActive.Rows.Count == 0)
                            {
                                res.Append("22" + "<br />");
                            }
                        }

                    }
                }
                //ประกันวินาศภัย chkActive license ที่เกี่ยวข้อง
                else if (tagCasualty.Contains(licenseType.Trim()))
                {
                    sql = "SELECT lt.LICENSE_NO "
                         + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                         + "WHERE  lt.RECORD_STATUS is null "
                         + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                         + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                         + "and lt.EXPIRE_DATE >= sysdate "
                         + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                         + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                         + "group by lt.LICENSE_NO";


                    DataTable chkActive = ora.GetDataTable(sql);

                    if (chkActive != null && chkActive.Rows.Count > 0)
                    {
                        res.Append("22" + "<br />");
                    }
                    //chkNotAcitve
                    else
                    {
                        sql = "SELECT lt.LICENSE_NO LICENSE_NO,lt.REVOKE_TYPE_CODE  REVOKE_TYPE_CODE "
                        + "FROM AG_LICENSE_T lt "
                        + "WHERE lt.RECORD_STATUS is null "
                        + "and   lt.EXPIRE_DATE < sysdate "
                        + "and   lt.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                        + "and   lt.LICENSE_NO = '" + detail.LICENSE_NO.Trim() + "' "
                        + "group by lt.LICENSE_NO,lt.REVOKE_TYPE_CODE";

                        DataTable chkNotActive = ora.GetDataTable(sql);

                        if (chkNotActive.Rows.Count > 0)
                        {
                            DataRow dr = chkNotActive.Rows[0];
                            string revokeCode = dr["REVOKE_TYPE_CODE"].ToString();

                            if (revokeCode != "B" && !string.IsNullOrEmpty(revokeCode))
                            {
                                res.Append("22" + "<br />");
                            }
                        }
                        else if (chkNotActive.Rows.Count == 0)
                        {
                            res.Append("22" + "<br />");
                        }
                    }


                }
                #endregion

                //ตรวจสอบ BackList
                #region chkBackList

                if (tagGetProperty.Contains("13") || tagGetProperty.Contains("16"))
                {
                    if (tagLife.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี

                        string dateOldst;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }

                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('01','03','07') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("13" + "<br />");
                        }

                    }

                    else if (tagCasualty.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี

                        string dateOldst;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }

                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("16" + "<br />");
                        }
                    }

                }

                #endregion

                //ตรวจสอบผลอบรม
                #region chkTrainResult


                if (tagGetProperty.Contains("4"))
                {

                    tagCasualty = new List<string>(new string[] { "02", "04", "05", "06", "08" });

                    tagLife = new List<string>(new string[] { "01", "03", "07" });

                    var tageAgent = new string[] { "03", "04" };

                    if (tagLife.Contains(licenseType))
                    {
                        renewTime = 0;

                        var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                        if (!chkTrain)
                        {
                            res.Append("4" + "<br />");
                        }

                    }

                    else if (tagCasualty.Contains(licenseType))
                    {
                        sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO <> '" + detail.LICENSE_NO.Trim() + "' "
                                + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                + "and lt.EXPIRE_DATE >= sysdate "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE ='02' "
                                + "group by lt.LICENSE_NO";

                        DataTable chkOtherLicense = ora.GetDataTable(sql);

                        if (chkOtherLicense.Rows.Count == 0)
                        {
                            renewTime = 0;
                            var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }
                        else
                        {
                            renewTime = 0;
                            var chkTrain = ChkTrainRenew(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                            if (!chkTrain)
                            {
                                res.Append("4" + "<br />");
                            }
                        }
                    }
                    else if (tageAgent.Contains(licenseType))
                    {
                        //เป็นตัวแทนไปอ่าน Config ว่าตรวจสอบผลอบรมหรือไม่
                        var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, 0);

                        if (!chkTrain)
                        {
                            res.Append("4" + "<br />");
                        }

                    }
                }

                #endregion


            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_UploadDataLicense", ex);
                throw ex;
            }

            return res;
        }

        private StringBuilder ValidateOtherLicense(AG_IAS_IMPORT_DETAIL_TEMP detail, string licenseType, string petitionType)
        {
            var res = new StringBuilder("");
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {

                #region GetData

                var tagLife = new string[] { "01", "03", "07" };

                var tagCasualty = new string[] { "02", "04", "05", "06", "08" };

                //คุณสมบัติที่ตรวจสอบ
                List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, 0);


                #endregion

                //ตรวจสอบใบอนุญาตต้อง Active
                #region ChkActive


                if (tagGetProperty.Contains("19"))
                {
                    var ChkActive = base.ctx.AG_LICENSE_T.FirstOrDefault(l => l.LICENSE_NO == detail.LICENSE_NO && l.LICENSE_TYPE_CODE == licenseType);

                    if (ChkActive != null)
                    {
                        if ((ChkActive.EXPIRE_DATE < DateTime.Now) || (ChkActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(ChkActive.REVOKE_TYPE_CODE)) || (ChkActive.RECORD_STATUS == "X"))
                        {
                            res.Append("19" + "<br />");
                        }
                    }
                }

                #endregion

                //ตรวจสอบ BackList
                #region chkBackList

                if (tagGetProperty.Contains("13") || tagGetProperty.Contains("16"))
                {


                    if (tagLife.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี

                        string dateOldst;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }

                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('01','03','07') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("13" + "<br />");
                        }

                    }

                    else if (tagCasualty.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี

                        string dateOldst;

                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }

                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("16" + "<br />");
                        }
                    }

                }

                #endregion

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateOtherLicense", ex);
                throw ex;
            }
            return res;
        }

        private StringBuilder ValidateMove(AG_IAS_IMPORT_DETAIL_TEMP detail, string licenseType, string petitionType, string comCode)
        {
            var res = new StringBuilder("");
            var tagLife = new string[] { "01", "07" };
            var tagCasualty = new string[] { "02", "05", "06", "08" };
            var dateNow = DateTime.Now;
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {
                #region GetData


                var chkLicenseActive = base.ctx.AG_LICENSE_T.FirstOrDefault(lt => lt.LICENSE_NO.Trim() == detail.LICENSE_NO.Trim());
                List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, 0);


                #endregion

                //ตรวจสอบกรณีใบอนุญาตหมดอายุ
                #region chkNotActive

                //tagCasualty = new string[] { "02", "05", "06" };

                if (chkLicenseActive.EXPIRE_DATE < dateNow)
                {
                    if (tagGetProperty.Contains("23"))
                    {


                        if (licenseType == "01")
                        {

                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != "01"))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE = '01' "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }

                        }
                        else if (tagCasualty.Contains(licenseType))
                        {


                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != licenseType.Trim()))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }


                        }
                        else if (licenseType == "07")
                        {
                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != "07"))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE = '07' "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }


                        }
                        else if (licenseType == "08")
                        {
                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != "08"))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE = '08' "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }
                        }


                    }

                }
                #endregion


                //ตรวจสอบกรณีใบอนุญาตไม่หมดอายุ
                #region chkActive

                else
                {
                    if (tagGetProperty.Contains("23"))
                    {


                        if (licenseType == "01")
                        {
                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != "01"))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE = '01' "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }

                        }

                        else if (tagCasualty.Contains(licenseType))
                        {
                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != licenseType.Trim()))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }


                        }
                        else if (licenseType == "07")
                        {
                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != "07"))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE = '07' "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }


                        }
                        else if (licenseType == "08")
                        {

                            if ((chkLicenseActive.REVOKE_TYPE_CODE != "B" && !string.IsNullOrEmpty(chkLicenseActive.REVOKE_TYPE_CODE)) || (chkLicenseActive.RECORD_STATUS == "X") || (chkLicenseActive.LICENSE_TYPE_CODE != "08"))
                            {
                                res.Append("23" + "<br />");
                            }
                            else
                            {
                                //มีใบอนุญาตประเภทนี้แล้วในบริษัทนี้
                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE lt.REVOKE_LICENSE_DATE is null "
                                + "and lt.LICENSE_NO = ag.LICENSE_NO "
                                + "and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                                + "and ag.INSURANCE_COMP_CODE = '" + comCode.Trim() + "' "
                                + "and lt.RECORD_STATUS is null "
                                + "and lt.LICENSE_TYPE_CODE = '08' "
                                + "group by lt.LICENSE_NO";

                                DataTable chkLicenseCom = ora.GetDataTable(sql);


                                if (chkLicenseCom != null && chkLicenseCom.Rows.Count > 0)
                                {
                                    res.Append("23" + "<br />");
                                }
                            }
                        }
                    }

                }
                #endregion

                //ตรวจสอบ  BackList
                #region chkBackList
                if (tagGetProperty.Contains("13") || tagGetProperty.Contains("16"))
                {


                    if (tagLife.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี
                        string dateOldst;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }

                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('01','03','07') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("13" + "<br />");
                        }

                    }

                    else if (tagCasualty.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี

                        string dateOldst;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }


                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("16" + "<br />");
                        }
                    }
                }

                #endregion

                //ตรวจสอบผลอบรม
                #region chkTrainResult

                if (tagGetProperty.Contains("4"))
                {
                    int renewTime = 0;

                    var chkTrain = ChkTrainResult(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                    if (!chkTrain)
                    {
                        res.Append("4" + "<br />");
                    }
                }

                #endregion


            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateMove", ex);
                throw ex;
            }
            return res;
        }

        private StringBuilder ValidateSecond(AG_IAS_IMPORT_DETAIL_TEMP detail, string licenseType, string petitionType, string comCode)
        {
            var res = new StringBuilder("");
            var dateNow = DateTime.Now;
            var sql = string.Empty;
            OracleDB ora = new OracleDB();



            try
            {
                #region GetData

                var tagLife = new string[] { "01", "07" };
                var tagCasualty = new string[] { "02", "05", "06", "08" };

                //คุณสมบัติที่ตรวจสอบ
                List<string> tagGetProperty = GetChkPropLiecense(licenseType, petitionType, 0);



                #endregion


                //ตรวจสอบใบอนุญาตยังมีผลอยู่
                #region chkActive

                if (tagGetProperty.Contains("24"))
                {
                    //var chkLicenseActive = base.ctx.AG_LICENSE_T.FirstOrDefault(lt => lt.LICENSE_NO.Trim() == detail.LICENSE_NO.Trim());

                    if (tagLife.Contains(licenseType))
                    {
                        sql = "SELECT lt.LICENSE_NO,lt.REVOKE_TYPE_CODE "
                             + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                             + "WHERE  lt.RECORD_STATUS is  null "
                             + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                             + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                             + "and lt.EXPIRE_DATE >= sysdate "
                             + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                             + "and lt.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                             + "group by lt.LICENSE_NO,lt.REVOKE_TYPE_CODE";


                        DataTable chkActive = ora.GetDataTable(sql);

                        if (chkActive.Rows.Count == 0)
                        {
                            res.Append("24" + "<br />");
                        }

                    }
                    else if (tagCasualty.Contains(licenseType))
                    {
                        sql = "SELECT lt.LICENSE_NO "
                           + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                           + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                           + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                           + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                           + "and lt.EXPIRE_DATE >= sysdate "
                           + "and lt.RECORD_STATUS is null "
                           + "and lt.LICENSE_TYPE_CODE = '02' "
                           + "group by lt.LICENSE_NO";

                        DataTable chkActive02 = ora.GetDataTable(sql);

                        if (chkActive02.Rows.Count == 0)
                        {
                            sql = "SELECT ap.ID_CARD_NO "
                            + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                            + "WHERE ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "' "
                            + "and ap.TESTING_NO = ex.TESTING_NO "
                            + "and ex.LICENSE_TYPE_CODE = '02' "
                            + "and ap.RESULT = 'P' ";

                            DataTable chkExamResult = ora.GetDataTable(sql);

                            if (chkExamResult.Rows.Count == 0)
                            {


                                sql = "SELECT lt.LICENSE_NO "
                                + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                + "WHERE  lt.RECORD_STATUS is null "
                                + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                + "and lt.EXPIRE_DATE >= sysdate "
                                + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                                + "and lt.LICENSE_TYPE_CODE in ('02','05','06','08') "
                                + "group by lt.LICENSE_NO";

                                DataTable chkActive = ora.GetDataTable(sql);

                                if (chkActive.Rows.Count == 0)
                                {

                                    sql = "SELECT lt.LICENSE_NO "
                                    + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                    + "WHERE  lt.REVOKE_LICENSE_DATE is null "
                                    + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                    + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                    + "and lt.EXPIRE_DATE < sysdate "
                                    + "and lt.RECORD_STATUS is null "
                                    + "and lt.LICENSE_TYPE_CODE = '02' "
                                    + "group by lt.LICENSE_NO";

                                    DataTable chkNotActive02 = ora.GetDataTable(sql);

                                    if (chkNotActive02 != null && chkNotActive02.Rows.Count > 0)
                                    {
                                        sql = "SELECT lt.LICENSE_NO "
                                        + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                        + "WHERE  lt.RECORD_STATUS is null "
                                        + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                        + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                        + "and lt.EXPIRE_DATE >= sysdate "
                                        + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                                        + "and lt.LICENSE_TYPE_CODE in ('02','05','06','08')"
                                        + "group by lt.LICENSE_NO";

                                        DataTable chkOtherLicense = ora.GetDataTable(sql);

                                        if (chkOtherLicense.Rows.Count == 0)
                                        {
                                            res.Append("24" + "<br />");
                                        }
                                    }
                                    else
                                    {
                                        res.Append("24" + "<br />");

                                    }
                                }



                            }
                            else
                            {
                                sql = "SELECT lt.LICENSE_NO "
                                        + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                                        + "WHERE  lt.RECORD_STATUS is null "
                                        + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                                        + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                                        + "and lt.EXPIRE_DATE >= sysdate "
                                        + "and (lt.REVOKE_TYPE_CODE = 'B' or lt.REVOKE_TYPE_CODE is null) "
                                        + "and lt.LICENSE_TYPE_CODE in ('02','05','06','08')"
                                        + "group by lt.LICENSE_NO";

                                DataTable chkOtherLicense = ora.GetDataTable(sql);

                                if (chkOtherLicense.Rows.Count == 0)
                                {
                                    res.Append("24" + "<br />");
                                }
                            }
                        }


                    }

                }
                #endregion


                //ตรวจสอบ BackList
                #region chkBackList
                if (tagGetProperty.Contains("13") || tagGetProperty.Contains("16"))
                {

                    if (tagLife.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี

                        string dateOldst;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }


                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('01','03','07') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("13" + "<br />");
                        }

                    }

                    else if (tagCasualty.Contains(licenseType))
                    {
                        //chkBacklist
                        //กลับไป 5 ปี

                        string dateOldst;
                        if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5).AddYears(-543));
                        }
                        else
                        {
                            dateOldst = string.Format("{0:dd/MM/yyyy}", DateTime.Now.AddYears(-5));
                        }


                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.REVOKE_TYPE_CODE ='B' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + detail.CITIZEN_ID.Trim() + "')) "
                              + "and lt.REVOKE_LICENSE_DATE < TO_DATE('" + dateOldst + "','dd/MM/yyyy') "
                              + "and lt.LICENSE_TYPE_CODE in ('02','04','05','06','08') "
                              + "group by lt.LICENSE_NO";

                        DataTable chkBackList = ora.GetDataTable(sql);

                        if (chkBackList != null && chkBackList.Rows.Count > 0)
                        {
                            res.Append("16" + "<br />");
                        }
                    }

                }
                #endregion

                //ตรวจสอบผลอบรม
                #region chkTrainSecondResult

                if (tagGetProperty.Contains("4"))
                {
                    int renewTime = 0;


                    var chkTrain = ChkTrainRenew(licenseType, petitionType, detail.CITIZEN_ID, renewTime);
                    if (!chkTrain)
                    {
                        res.Append("4" + "<br />");
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ValidateSecond", ex);
                throw ex;
            }
            return res;
        }

        private int GetRenewTime(string licenseNo)
        {
            int reNewTime = 0;
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {

                sql = "SELECT MAX(RENEW_TIME) "
                      + "FROM AG_LICENSE_RENEW_T  "
                      + "WHERE LICENSE_NO = '" + licenseNo.Trim() + "' ";


                DataTable dt = ora.GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    int reNew = dr[0].ToInt();

                    reNewTime = reNew;

                }
                else
                {
                    reNewTime = 0;
                }






            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reNewTime;
        }

        private bool ChkMarjority(string idCard, string licenseType, List<AG_IAS_ATTACH_FILE_LICENSE> AttachFileDetails)
        {
            var res = new bool();
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {

                #region getAgen

                var tagUser = new string[] { "" };
                var agenType = ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(a => a.LICENSE_TYPE_CODE.Trim() == licenseType.Trim());

                //ตรวจสอบวุฒิการศึกษานำไปแทนผลการสอบ

                if (agenType.INSURANCE_TYPE == "1")
                {
                    tagUser = new string[] { "L", "B" };
                }
                else if (agenType.INSURANCE_TYPE == "2")
                {
                    tagUser = new string[] { "D", "B" };
                }
                else
                {
                    tagUser = new string[] { "B" };
                }
                #endregion

                var disExam = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                               from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                               where
                               dt.SPECIAL_TYPE_CODE_EXAM == ex.SPECIAL_TYPE_CODE
                               && dt.EXAM_DISCOUNT_STATUS == "Y"
                               && tagUser.Contains(ex.USED_TYPE.Trim())
                               && dt.STATUS == "A"
                               select dt.DOCUMENT_CODE).ToList();

                var examFile = (from a in AttachFileDetails
                                where disExam.Contains(a.ATTACH_FILE_TYPE)
                                select a.ATTACH_FILE_TYPE).FirstOrDefault();

                var examSpecial = (from a in ctx.AG_IAS_EXAM_SPECIAL_T
                                   from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                                   where a.ID_CARD_NO.Trim() == idCard.Trim()
                                   && tagUser.Contains(ex.USED_TYPE.Trim())
                                   select a.SPECIAL_TYPE_CODE).FirstOrDefault();


                if (examFile == null && examSpecial == null)
                {

                    sql = "SELECT ap.BIRTH_DATE,ap.TESTING_NO "
                           + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                           + "WHERE ap.ID_CARD_NO = '" + idCard.Trim() + "' "
                           + "and ap.TESTING_NO = ex.TESTING_NO "
                           + "and ex.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                           + "order by TESTING_NO desc ";
                    //+ "and ap.RESULT = 'P' ";


                    DataTable dtBirthDate = ora.GetDataTable(sql);


                    if (dtBirthDate != null && dtBirthDate.Rows.Count > 0)
                    {
                        DataRow dr = dtBirthDate.Rows[0];
                        string chkBirthDate = dr["BIRTH_DATE"].ToString();

                        DateTime birthDate = Convert.ToDateTime(chkBirthDate).AddYears(20);
                        DateTime currentDate = DateTime.Now;

                        string currDateFormat = String.Format("{0:dd/MM/yyy}", currentDate).ToString();
                        string birthDateFormat = String.Format("{0:dd/MM/yyyy}", birthDate).ToString();

                        DateTime currTime = DateTime.Parse(currDateFormat);
                        DateTime birthTime = DateTime.Parse(birthDateFormat);


                        int dateCompare = DateTime.Compare(birthTime, currTime);

                        if (dateCompare == 0 || dateCompare == -1)
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                        }

                    }
                    else
                    {
                        res = false;
                    }

                }
                else
                {
                    res = true;
                }


            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ChkMarjority", ex);
                throw ex;
            }

            return res;
        }

        private bool ChkExamResult(string idCard, string licenseType, List<AG_IAS_ATTACH_FILE_LICENSE> AttachFileDetails)
        {
            var res = new bool();
            var tagLife = new string[] { "07" };
            var tagCasualty = new string[] { "05", "06", "08" };
            var sql = string.Empty;
            OracleDB ora = new OracleDB();



            try
            {
                #region getAgen

                var tagUser = new string[] { "" };
                var agenType = ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(a => a.LICENSE_TYPE_CODE.Trim() == licenseType.Trim());

                //ตรวจสอบวุฒิการศึกษานำไปแทนผลการสอบ

                if (agenType.INSURANCE_TYPE == "1")
                {
                    tagUser = new string[] { "L", "B" };
                }
                else if (agenType.INSURANCE_TYPE == "2")
                {
                    tagUser = new string[] { "D", "B" };
                }
                else
                {
                    tagUser = new string[] { "B" };
                }
                #endregion


                var disExam = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                               from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                               where
                               dt.SPECIAL_TYPE_CODE_EXAM == ex.SPECIAL_TYPE_CODE
                               && dt.EXAM_DISCOUNT_STATUS == "Y"
                               && tagUser.Contains(ex.USED_TYPE.Trim())
                               && dt.STATUS == "A"
                               select dt.DOCUMENT_CODE).ToList();


                var examFile = (from a in AttachFileDetails
                                where disExam.Contains(a.ATTACH_FILE_TYPE)
                                select a.ATTACH_FILE_TYPE).FirstOrDefault();


                var examSpecial = (from a in ctx.AG_IAS_EXAM_SPECIAL_T
                                   from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                                   where a.ID_CARD_NO.Trim() == idCard.Trim()
                                   && tagUser.Contains(ex.USED_TYPE.Trim())
                                   select a.SPECIAL_TYPE_CODE).FirstOrDefault();



                if (examFile == null && examSpecial == null)
                {
                    //ตรวจสอบผลสอบที่ตรงกับการขอใบอนุญาต

                    sql = "SELECT ap.ID_CARD_NO "
                    + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                    + "WHERE ap.ID_CARD_NO = '" + idCard.Trim() + "' "
                    + "and ap.TESTING_NO = ex.TESTING_NO "
                    + "and ex.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                    + "and ap.RESULT = 'P' ";

                    DataTable chkExamResult = ora.GetDataTable(sql);



                    if (chkExamResult.Rows.Count == 0)
                    {
                        //ถ้าเป็นวินาศภัยใช้ 02 แทนได้
                        if (tagCasualty.Contains(licenseType))
                        {
                            sql = "SELECT ap.ID_CARD_NO "
                            + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                            + "WHERE ap.ID_CARD_NO = '" + idCard.Trim() + "' "
                            + "and ap.TESTING_NO = ex.TESTING_NO "
                            + "and ex.LICENSE_TYPE_CODE = '02' "
                            + "and ap.RESULT = 'P' ";

                            DataTable chkExamCasualty = ora.GetDataTable(sql);

                            if (chkExamCasualty.Rows.Count == 0)
                            {
                                res = false;
                            }
                            else
                            {
                                res = true;
                            }


                        }
                        //เป็นชีวิตใช้ 01 แทนได้
                        else if (tagLife.Contains(licenseType))
                        {
                            sql = "SELECT ap.ID_CARD_NO "
                               + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                               + "WHERE ap.ID_CARD_NO = '" + idCard.Trim() + "' "
                               + "and ap.TESTING_NO = ex.TESTING_NO "
                               + "and ex.LICENSE_TYPE_CODE = '01' "
                               + "and ap.RESULT = 'P' ";

                            DataTable chkExamLife = ora.GetDataTable(sql);

                            if (chkExamLife.Rows.Count == 0)
                            {
                                res = false;
                            }
                            else
                            {
                                res = true;
                            }

                        }
                        else
                        {
                            res = false;
                        }
                    }
                    else
                    {
                        res = true;
                    }

                }
                else
                {
                    res = true;
                }


            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ChkExamResult", ex);
                throw;
            }
            return res;
        }

        private bool ChkTrainResult(string licenseType, string petitionType, string idCard, int trainTime)
        {
            var res = new bool();
            var tagLife = new string[] { "07" };
            var tagCasualty = new string[] { "05", "06", "08" };
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {
                sql = "SELECT t.ID_CARD_NO "
                      + "FROM AG_TRAIN_PERSON_T t left join AG_TRAIN_PLAN_T p on t.TRAIN_CODE = p.TRAIN_CODE  "
                      + "WHERE t.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                      + "and t.TRAIN_TIMES = " + trainTime
                      + " and t.ID_CARD_NO = '" + idCard.Trim() + "' "
                      + "and t.TRAIN_EXP_DATE >= sysdate "
                      + "and t.TRAIN_TYPE = 'T' "
                      + "and t.RESULT = 'P' ";

                DataTable chkTrain = ora.GetDataTable(sql);


                if (chkTrain.Rows.Count == 0)
                {

                    //ถ้าเป็นวินาศภัยใช้ 02 แทนได้
                    if (tagCasualty.Contains(licenseType))
                    {
                        sql = "SELECT t.ID_CARD_NO "
                            + "FROM AG_TRAIN_PERSON_T t left join AG_TRAIN_PLAN_T p on t.TRAIN_CODE = p.TRAIN_CODE  "
                            + "WHERE t.LICENSE_TYPE_CODE = '02' "
                            + "and t.TRAIN_TIMES = " + trainTime
                            + " and t.ID_CARD_NO = '" + idCard.Trim() + "' "
                            + "and t.TRAIN_EXP_DATE >= sysdate "
                            + "and t.TRAIN_TYPE = 'T' "
                            + "and t.RESULT = 'P' ";

                        DataTable chkTrainCasualty = ora.GetDataTable(sql);

                        if (chkTrain.Rows.Count == 0)
                        {
                            res = false;
                        }
                        else
                        {
                            res = true;
                        }

                    }

                    //เป็นชีวิตใช้ 01 แทนได้
                    else if (tagLife.Contains(licenseType))
                    {
                        sql = "SELECT t.ID_CARD_NO "
                            + "FROM AG_TRAIN_PERSON_T t left join AG_TRAIN_PLAN_T p on t.TRAIN_CODE = p.TRAIN_CODE  "
                            + "WHERE t.LICENSE_TYPE_CODE = '01' "
                            + "and t.TRAIN_TIMES = " + trainTime
                            + " and t.ID_CARD_NO = '" + idCard.Trim() + "' "
                            + "and t.TRAIN_EXP_DATE >= sysdate "
                            + "and t.TRAIN_TYPE = 'T' "
                            + "and t.RESULT = 'P' ";

                        DataTable chkTrainLife = ora.GetDataTable(sql);

                        if (chkTrain.Rows.Count == 0)
                        {
                            res = false;
                        }
                        else
                        {
                            res = true;
                        }

                    }
                    else
                    {
                        res = false;
                    }
                }
                else
                {
                    res = true;
                }




            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ChkTrainResult", ex);
                throw ex;
            }

            return res;
        }

        private bool ChkTrainRenew(string licenseType, string petitionType, string idCard, int trainTime)
        {

            var res = new bool();
            var sql = string.Empty;
            OracleDB ora = new OracleDB();
            var tagLife = new string[] { "07" };
            var tagCasualty = new string[] { "05", "06", "08" };
            try
            {

                sql = "SELECT t.ID_CARD_NO "
                     + "FROM AG_TRAIN_PERSON_T t left join AG_TRAIN_PLAN_T p on t.TRAIN_CODE = p.TRAIN_CODE  "
                     + "WHERE t.LICENSE_TYPE_CODE = '" + licenseType.Trim() + "' "
                     + "and t.TRAIN_TIMES = " + trainTime
                     + " and t.ID_CARD_NO = '" + idCard.Trim() + "' "
                     + "and t.TRAIN_TYPE = 'T' "
                     + "and t.RESULT = 'P' ";

                DataTable chkTrain = ora.GetDataTable(sql);

                if (chkTrain.Rows.Count == 0)
                {

                    //ถ้าเป็นวินาศภัยใช้ 02 แทนได้
                    if (tagCasualty.Contains(licenseType))
                    {
                        sql = "SELECT t.ID_CARD_NO "
                              + "FROM AG_TRAIN_PERSON_T t left join AG_TRAIN_PLAN_T p on t.TRAIN_CODE = p.TRAIN_CODE  "
                              + "WHERE t.LICENSE_TYPE_CODE = '02' "
                              + "and t.TRAIN_TIMES = " + trainTime
                              + " and t.ID_CARD_NO = '" + idCard.Trim() + "' "
                              + "and t.TRAIN_TYPE = 'T' "
                              + "and t.RESULT = 'P' ";

                        DataTable chkTrainCasualty = ora.GetDataTable(sql);

                        if (chkTrainCasualty.Rows.Count == 0)
                        {
                            res = false;
                        }
                        else
                        {
                            res = true;
                        }

                    }

                    //ประกันชีวิตใช้ 01 แทนได้
                    else if (tagLife.Contains(licenseType))
                    {
                        sql = "SELECT t.ID_CARD_NO "
                              + "FROM AG_TRAIN_PERSON_T t left join AG_TRAIN_PLAN_T p on t.TRAIN_CODE = p.TRAIN_CODE  "
                              + "WHERE t.LICENSE_TYPE_CODE = '01' "
                              + "and t.TRAIN_TIMES = " + trainTime
                              + " and t.ID_CARD_NO = '" + idCard.Trim() + "' "
                              + "and t.TRAIN_TYPE = 'T' "
                              + "and t.RESULT = 'P' ";

                        DataTable chkTrainCasualty = ora.GetDataTable(sql);

                        if (chkTrainCasualty.Rows.Count == 0)
                        {
                            res = false;
                        }
                        else
                        {
                            res = true;
                        }
                    }
                    else
                    {
                        res = false;
                    }
                }
                else
                {
                    res = true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;

        }



        private bool ChkPaymentLicense(string idCard, string licenseType, string petitionType)
        {
            var res = new bool();
            try
            {
                var chkPay = (from dt in ctx.AG_IAS_SUBPAYMENT_D_T
                              from ld in ctx.AG_IAS_LICENSE_D
                              where
                              dt.UPLOAD_GROUP_NO.Trim() == ld.UPLOAD_GROUP_NO.Trim()
                              && ld.ID_CARD_NO.Trim() == dt.ID_CARD_NO.Trim()
                              && dt.PAYMENT_DATE != null
                              && ld.ID_CARD_NO.Trim() == idCard.Trim()
                              && dt.LICENSE_TYPE_CODE.Trim() == licenseType.Trim()
                              && dt.PETITION_TYPE_CODE.Trim() == petitionType.Trim()
                              select new DTO.DataItem
                              {
                                  Name = ld.ID_CARD_NO.Trim()

                              }).ToList();

                if (chkPay != null && chkPay.Count != 0 && !string.IsNullOrEmpty(chkPay[0].Name))
                {
                    res = true;

                }
                else
                {
                    res = false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }

        private bool ChkAreaCode(string areaCode)
        {
            var res = new bool();
            try
            {
                string province = areaCode.Substring(0, 2);

                var chkArea = base.ctx.VW_IAS_PROVINCE.Where(s => s.ID == province).FirstOrDefault();

                if (chkArea == null)
                {
                    res = false;

                }
                else
                {
                    res = true;
                }
            }

            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_ChkAreaCode", ex);
                throw ex;
            }
            return res;
        }

        private List<string> GetLiftInsurance()
        {
            var res = new List<string>();

            try
            {

                var licenseType = (from a in ctx.AG_IAS_LICENSE_TYPE_R
                                   where a.INSURANCE_TYPE == "1"
                                   select a.LICENSE_TYPE_CODE).ToList();

                if (licenseType == null)
                {
                    res.Add("");
                }
                else
                {
                    res = licenseType;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }

        private List<string> GetCasualtyInsurance()
        {
            var res = new List<string>();

            try
            {

                var licenseType = (from a in ctx.AG_IAS_LICENSE_TYPE_R
                                   where a.INSURANCE_TYPE == "2"
                                   select a.LICENSE_TYPE_CODE).ToList();

                if (licenseType == null)
                {
                    res.Add("");
                }
                else
                {
                    res = licenseType;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return res;
        }

        public DTO.ResponseMessage<bool> LicenseRevokedValidation(List<string> license, string licenseTypeCode)
        {
            var res = new DTO.ResponseMessage<bool>();
            List<DTO.PersonLicenseTransaction> revokelist = new List<PersonLicenseTransaction>();

            try
            {
                if (license.Count > 0)
                {

                    if (licenseTypeCode.Equals("03"))
                    {
                        //Get License Details
                        for (int i = 0; i < license.Count; i++)
                        {
                            string curLicense = license[i];
                            AG_LICENSE_T ent = base.ctx.AG_LICENSE_T.FirstOrDefault(li => li.LICENSE_NO.Equals(curLicense)
                                && li.REVOKE_TYPE_CODE.Equals("B") && (li.LICENSE_TYPE_CODE.Equals("01") || li.LICENSE_TYPE_CODE.Equals("03") || li.LICENSE_TYPE_CODE.Equals("07")));
                            if (ent != null)
                            {
                                DTO.PersonLicenseTransaction e = new PersonLicenseTransaction
                                {
                                    LICENSE_NO = ent.LICENSE_NO,
                                    LICENSE_TYPE_CODE = ent.LICENSE_TYPE_CODE,
                                    REVOKE_TYPE_CODE = ent.REVOKE_TYPE_CODE,
                                    REVOKE_LICENSE_DATE = ent.REVOKE_LICENSE_DATE
                                };

                                revokelist.Add(e);
                            }

                        }

                        /// <summary>
                        /// DateTime Compare
                        /// Result ได้ -1 If  Time_1 น้อยกว่า  Time_2
                        /// Result ได้ 0  If  Time_1 เท่ากับ Time_2
                        /// Result ได้ 1  If  Time_1 มากกว่า Time_2
                        /// How to use Compare?
                        /// 1.เวลาปัจจุบัน เทียบกับ เวลาที่เลือก
                        /// Coding BY Natta
                        /// </summary>
                        //Validate License Details
                        if (revokelist.Count > 0)
                        {
                            for (int i = 0; i < revokelist.Count; i++)
                            {
                                DateTime revokedate = Convert.ToDateTime(revokelist[i].REVOKE_LICENSE_DATE);
                                DateTime datenow = DateTime.Now.AddYears(-5);

                                int dateCompare = DateTime.Compare(revokedate, datenow);
                                switch (dateCompare)
                                {
                                    case -1:
                                        res.ResultMessage = true;
                                        //return res;
                                        break;
                                    case 0:
                                        res.ResultMessage = false;
                                        return res;
                                    //break;
                                    case 1:
                                        res.ResultMessage = false;
                                        return res;
                                    //break;
                                }

                            }

                        }
                        else
                        {
                            res.ResultMessage = true;
                        }
                    }
                    else if (licenseTypeCode.Equals("04"))
                    {

                        //Get License Details
                        for (int i = 0; i < license.Count; i++)
                        {
                            string curLicense = license[i];
                            AG_LICENSE_T ent = base.ctx.AG_LICENSE_T.FirstOrDefault(li => li.LICENSE_NO.Equals(curLicense)
                                && li.REVOKE_TYPE_CODE.Equals("B") && (li.LICENSE_TYPE_CODE.Equals("02") || li.LICENSE_TYPE_CODE.Equals("04") || li.LICENSE_TYPE_CODE.Equals("05")
                                || li.LICENSE_TYPE_CODE.Equals("06") || li.LICENSE_TYPE_CODE.Equals("08")));
                            if (ent != null)
                            {
                                DTO.PersonLicenseTransaction e = new PersonLicenseTransaction
                                {
                                    LICENSE_NO = ent.LICENSE_NO,
                                    LICENSE_TYPE_CODE = ent.LICENSE_TYPE_CODE,
                                    REVOKE_TYPE_CODE = ent.REVOKE_TYPE_CODE,
                                    REVOKE_LICENSE_DATE = ent.REVOKE_LICENSE_DATE
                                };

                                revokelist.Add(e);
                            }

                        }




                        /// <summary>
                        /// DateTime Compare
                        /// Result ได้ -1 If  Time_1 น้อยกว่า  Time_2
                        /// Result ได้ 0  If  Time_1 เท่ากับ Time_2
                        /// Result ได้ 1  If  Time_1 มากกว่า Time_2
                        /// How to use Compare?
                        /// 1.เวลาปัจจุบัน เทียบกับ เวลาที่เลือก
                        /// Coding BY Natta
                        /// </summary>
                        //Validate License Details
                        if (revokelist.Count > 0)
                        {
                            for (int i = 0; i < revokelist.Count; i++)
                            {
                                DateTime revokedate = Convert.ToDateTime(revokelist[i].REVOKE_LICENSE_DATE);
                                DateTime datenow = DateTime.Now.AddYears(-5);

                                int dateCompare = DateTime.Compare(revokedate, datenow);
                                switch (dateCompare)
                                {
                                    case -1:
                                        res.ResultMessage = true;
                                        //return res;
                                        break;
                                    case 0:
                                        res.ResultMessage = false;
                                        return res;
                                    //break;
                                    case 1:
                                        res.ResultMessage = false;
                                        return res;
                                    //break;
                                }

                            }

                        }
                        else
                        {
                            res.ResultMessage = true;
                        }
                    }
                    else
                    {

                        //Other License
                        res.ResultMessage = true;
                    }

                }
                else
                {
                    res.ResultMessage = true;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        public DTO.ResponseService<DataSet> GetRenewLicenseQuick(string PetitionType, DateTime? DateStart, DateTime? DateEnd, string CompCode, string Days)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = " select D.UPLOAD_GROUP_NO, H.TRAN_DATE, D.LICENSE_NO, D.ID_CARD_NO, D.NAMES NAME, D.LASTNAME, D.NAMES || ' ' || D.LASTNAME NAMES, "
                            + " T.PETITION_TYPE_NAME, D.LICENSE_EXPIRE_DATE "
                            + " from AG_IAS_LICENSE_H H, AG_IAS_LICENSE_D D, AG_IAS_PETITION_TYPE_R T "
                            + " where H.UPLOAD_GROUP_NO = D.UPLOAD_GROUP_NO and H.PETITION_TYPE_CODE = T.PETITION_TYPE_CODE "
                            + " and D.LICENSE_EXPIRE_DATE is not null and (D.APPROVED = 'W' or D.OIC_APPROVED_BY is null) ";

                if (!String.IsNullOrEmpty(PetitionType))
                {
                    sql += String.Format(" and H.PETITION_TYPE_CODE = '{0}' ", PetitionType.Trim());
                }
                else
                {
                    sql += " and (H.PETITION_TYPE_CODE IN ('13','14','16')) ";
                }

                if (DateStart != null && DateEnd != null)
                {
                    sql += String.Format(" and (H.TRAN_DATE >= to_date('{0}','yyyymmdd') and  D.LICENSE_EXPIRE_DATE <= to_date('{1}','yyyymmdd')) ",
                                    Convert.ToDateTime(DateStart).ToString_yyyyMMdd(),
                                    Convert.ToDateTime(DateEnd).ToString_yyyyMMdd());
                }
                else if (DateStart != null)
                {
                    sql += String.Format(" and H.TRAN_DATE = to_date('{0}','yyyymmdd') ", Convert.ToDateTime(DateStart).ToString_yyyyMMdd());
                }
                else if (DateEnd != null)
                {
                    sql += String.Format(" and D.LICENSE_EXPIRE_DATE = to_date('{0}','yyyymmdd') ", Convert.ToDateTime(DateEnd).ToString_yyyyMMdd());
                }

                if (!String.IsNullOrEmpty(CompCode))
                {
                    sql += String.Format(" and H.COMP_CODE = '{0}' ", CompCode.Trim());
                }

                if (!String.IsNullOrEmpty(Days))
                {
                    sql += String.Format(" and (SYSDATE >= TRUNC(LICENSE_EXPIRE_DATE-{0}) and SYSDATE < LICENSE_EXPIRE_DATE) ", Days);
                }

                sql += " order by D.LICENSE_EXPIRE_DATE ASC ";

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetRenewLicenseQuick", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.PersonLicenseApprover>> GetPersonalLicenseApprover(string licenseType)
        {

            var res = new DTO.ResponseService<List<DTO.PersonLicenseApprover>>();
            try
            {
                if (!licenseType.Equals(""))
                {
                    List<DTO.PersonLicenseApprover> result = new List<DTO.PersonLicenseApprover>();
                    result.Insert(0, new DTO.PersonLicenseApprover
                    {
                        ASSOCIATION_CODE = "0",
                        ASSOCIATION_NAME = "กรุณาเลือก"
                    });

                    if (licenseType != "" || licenseType != null)
                    {
                        var asso03 = (from A in base.ctx.AG_IAS_ASSOCIATION
                                      join B in base.ctx.AG_IAS_ASSOCIATION_APPROVE on A.ASSOCIATION_CODE equals B.ASSOCIATION_CODE
                                      where A.ACTIVE.Equals("Y") && B.APPROVE_DOC_TYPE.Equals(licenseType) && B.STATUS.Equals("Y")
                                      select new DTO.PersonLicenseApprover
                                      {
                                          ASSOCIATION_CODE = A.ASSOCIATION_CODE.Trim(),
                                          ASSOCIATION_NAME = A.ASSOCIATION_NAME.Trim(),
                                          USER_ID = A.USER_ID,
                                          USER_DATE = A.USER_DATE,
                                          UPDATED_BY = A.UPDATED_BY,
                                          UPDATED_DATE = A.UPDATED_DATE,
                                          ACTIVE = A.ACTIVE,
                                          COMP_TYPE = A.COMP_TYPE,
                                          AGENT_TYPE = A.AGENT_TYPE
                                      }).ToList();
                        res.DataResponse = result.Union(asso03).OrderBy(cd => cd.ASSOCIATION_CODE).ToList();
                    }



                }
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_GetPersonalLicenseApprover", ex.Message);
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<IEnumerable<DateTime>> GetLicenseRequestOicApprove(DTO.RangeDateRequest request)
        {
            DTO.ResponseService<IEnumerable<DateTime>> response = new ResponseService<IEnumerable<DateTime>>();


            request.EndDate = new DateTime(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day, 23, 59, 59);
            IEnumerable<AG_IAS_LICENSE_D> licenseDs = ctx.AG_IAS_LICENSE_D.
                                                        Where(a => a.OIC_APPROVED_DATE != null
                                                            && (a.OIC_APPROVED_DATE >= request.StartDate
                                                            && a.OIC_APPROVED_DATE <= request.EndDate));

            var licenseDates = licenseDs.GroupBy(p => p.OIC_APPROVED_DATE.Value.Date);

            IList<DateTime> approveDates = new List<DateTime>();

            foreach (var item in licenseDates)
            {
                DateTime dStart = new DateTime(item.Key.Year, item.Key.Month, item.Key.Day, 0, 0, 0);
                DateTime dEnd = new DateTime(item.Key.Year, item.Key.Month, item.Key.Day, 23, 59, 59); ;
                var curDateLicenses = licenseDs.Where(a => a.OIC_APPROVED_DATE != null
                                                            && (a.OIC_APPROVED_DATE >= dStart
                                                            && a.OIC_APPROVED_DATE <= dEnd)).Max(a => a.OIC_APPROVED_DATE);

                approveDates.Add((DateTime)curDateLicenses);

            }
            response.DataResponse = approveDates;
            return response;
        }

        public DTO.ResponseService<DataSet> GetMoveCompanyIn(string LicenseType, string CompCode, DateTime DateStart, DateTime DateEnd)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string whereLicenseType = string.Empty;
                string whereCompCode = string.Empty;
                if (!string.IsNullOrEmpty(LicenseType))
                {
                    if (LicenseType == "09")//รวมประเภทตัวแทนประกันชีวิต
                    {
                        whereLicenseType = " and B.LICENSE_TYPE_CODE in ('01','07') ";
                    }
                    else if (LicenseType == "10")//รวมประเภทตัวแทนประกันวินาศภัย
                    {
                        whereLicenseType = " and B.LICENSE_TYPE_CODE in ('02','05','06','08') ";
                    }
                    else
                    {
                        whereLicenseType = string.Format(" and B.LICENSE_TYPE_CODE = '{0}' ", LicenseType);
                    }
                }

                if (!string.IsNullOrEmpty(CompCode))
                {
                    whereCompCode = string.Format(" and A.COMP_MOVE_IN_ID = '{0}' ", CompCode);
                }
                string sql = string.Format(@"
                                select distinct A.COMP_MOVE_OUT_ID COMP_CODE_OLD
                                from AG_HIS_MOVE_COMP_AGENT_T A, AG_LICENSE_T B
                                where A.LICENSE_NO = B.LICENSE_NO {2} {3}
                                    and (A.MOVE_DATE >= to_date('{0}','yyyymmdd') and A.MOVE_DATE <= to_date('{1}','yyyymmdd')) "
                             , DateStart.ToString_yyyyMMdd(), DateEnd.ToString_yyyyMMdd(), whereLicenseType, whereCompCode);
                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string custom = string.Empty;
                    foreach (DataRow item in dt.Rows)
                    {
                        custom += string.Format(" count(decode(COMP_MOVE_OUT_ID, '{0}', COMP_MOVE_OUT_ID)) COMP_{0}, ", item[0].ToString());
                    }

                    string sqlcol = string.Format(@"
                                        select COMP_MOVE_IN_ID COMP_CODE_IN ,NAME_IN, {0}
                                        count(COMP_MOVE_OUT_ID) TOTALS from(
	                                        select COMP_MOVE_IN_ID, D.NAME NAME_IN, COMP_MOVE_OUT_ID from ( 
		                                        select A.COMP_MOVE_OUT_ID, A.COMP_MOVE_IN_ID
		                                        from AG_HIS_MOVE_COMP_AGENT_T A, AG_LICENSE_T B
		                                        where A.LICENSE_NO = B.LICENSE_NO {3} {4}
			                                        and (A.MOVE_DATE >= to_date('{1}','yyyymmdd') and A.MOVE_DATE <= to_date('{2}','yyyymmdd'))
		                                        group by A.COMP_MOVE_OUT_ID, A.COMP_MOVE_IN_ID
	                                        ) C, VW_IAS_COM_CODE D
	                                        where C.COMP_MOVE_IN_ID = D.ID and C.COMP_MOVE_OUT_ID != C.COMP_MOVE_IN_ID 
                                        ) group by COMP_MOVE_IN_ID, NAME_IN order by TOTALS desc "
                                        , custom, DateStart.ToString_yyyyMMdd(), DateEnd.ToString_yyyyMMdd(), whereLicenseType, whereCompCode);
                    res.DataResponse = ora.GetDataSet(sqlcol);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetMoveCompanyIn", ex);
            }
            return res;
        }

        public ResponseService<DataSet> GetMoveCompanyOut(string LicenseType, string CompCode, DateTime DateStart, DateTime DateEnd)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string whereLicenseType = string.Empty;
                string whereCompCode = string.Empty;
                if (!string.IsNullOrEmpty(LicenseType))
                {
                    if (LicenseType == "09")//รวมประเภทตัวแทนประกันชีวิต
                    {
                        whereLicenseType = " and B.LICENSE_TYPE_CODE in ('01','07') ";
                    }
                    else if (LicenseType == "10")//รวมประเภทตัวแทนประกันวินาศภัย
                    {
                        whereLicenseType = " and B.LICENSE_TYPE_CODE in ('02','05','06','08') ";
                    }
                    else
                    {
                        whereLicenseType = string.Format(" and B.LICENSE_TYPE_CODE = '{0}' ", LicenseType);
                    }
                }

                if (!string.IsNullOrEmpty(CompCode))
                {
                    whereCompCode = string.Format(" and A.COMP_MOVE_OUT_ID = '{0}' ", CompCode);
                }
                string sql = string.Format(@"
                                select distinct A.COMP_MOVE_IN_ID COMP_CODE_IN
                                from AG_HIS_MOVE_COMP_AGENT_T A, AG_LICENSE_T B
                                where A.LICENSE_NO = B.LICENSE_NO {2} {3}
                                    and (A.MOVE_DATE >= to_date('{0}','yyyymmdd') and A.MOVE_DATE <= to_date('{1}','yyyymmdd')) "
                             , DateStart.ToString_yyyyMMdd(), DateEnd.ToString_yyyyMMdd(), whereLicenseType, whereCompCode);
                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string custom = string.Empty;
                    foreach (DataRow item in dt.Rows)
                    {
                        custom += string.Format(" count(decode(COMP_MOVE_IN_ID, '{0}', COMP_MOVE_IN_ID)) COMP_{0}, ", item[0].ToString());
                    }

                    string sqlcol = string.Format(@"
                                        select COMP_MOVE_OUT_ID COMP_CODE_OLD, NAME_OLD, {0}
                                        count(COMP_MOVE_IN_ID) TOTALS from(
	                                        select COMP_MOVE_OUT_ID, D.NAME NAME_OLD, COMP_MOVE_IN_ID from ( 
		                                        select A.COMP_MOVE_OUT_ID, A.COMP_MOVE_IN_ID
		                                        from AG_HIS_MOVE_COMP_AGENT_T A, AG_LICENSE_T B
		                                        where A.LICENSE_NO = B.LICENSE_NO {3} {4}
			                                        and (A.MOVE_DATE >= to_date('{1}','yyyymmdd') and A.MOVE_DATE <= to_date('{2}','yyyymmdd'))
		                                        group by A.COMP_MOVE_OUT_ID, A.COMP_MOVE_IN_ID
	                                        ) C, VW_IAS_COM_CODE D
	                                        where C.COMP_MOVE_OUT_ID = D.ID and C.COMP_MOVE_OUT_ID != C.COMP_MOVE_IN_ID 
                                        ) group by COMP_MOVE_OUT_ID, NAME_OLD order by TOTALS desc "
                                        , custom, DateStart.ToString_yyyyMMdd(), DateEnd.ToString_yyyyMMdd(), whereLicenseType, whereCompCode);
                    res.DataResponse = ora.GetDataSet(sqlcol);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetMoveCompanyOut", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetLicenseStatisticsReport(string LicenseTypeCode, string StartDate1, string EndDate1, string StartDate2, string EndDate2)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                string ConLicense = string.Empty;
                string Concompcode = string.Empty;


                if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                {
                    StartDate1 = Convert.ToDateTime(StartDate1).AddYears(-543).ToShortDateString();
                    EndDate1 = Convert.ToDateTime(EndDate1).AddYears(-543).ToShortDateString();
                    StartDate2 = Convert.ToDateTime(StartDate2).AddYears(-543).ToShortDateString();
                    EndDate2 = Convert.ToDateTime(EndDate2).AddYears(-543).ToShortDateString();
                }
                else
                {
                    StartDate1 = Convert.ToDateTime(StartDate1).ToShortDateString();
                    EndDate1 = Convert.ToDateTime(EndDate1).ToShortDateString();
                    StartDate2 = Convert.ToDateTime(StartDate2).ToShortDateString();
                    EndDate2 = Convert.ToDateTime(EndDate2).ToShortDateString();
                }
                if (LicenseTypeCode != "")
                {
                    if (LicenseTypeCode == "01")
                    {
                        ConLicense = "  and (ALT.LICENSE_TYPE_CODE ='01' or ALT.LICENSE_TYPE_CODE ='07') ";
                    }
                    else if (LicenseTypeCode == "02")
                    {
                        ConLicense = "  and (ALT.LICENSE_TYPE_CODE ='02' or ALT.LICENSE_TYPE_CODE ='05' or ALT.LICENSE_TYPE_CODE ='06' or ALT.LICENSE_TYPE_CODE ='08' )";
                    }
                    else
                    {
                        ConLicense = " and ALT.LICENSE_TYPE_CODE ='" + LicenseTypeCode + "'";
                    }
                }
                else
                {

                }

                string sql = "select * from( "
                            + "select count(*) as COUNT_COME_CODE1 "
                            + "from AG_LICENSE_T ALT inner join AG_AGENT_LICENSE_T AAT ON ALT.LICENSE_NO = AAT.LICENSE_NO "
                            + "INNER JOIN VW_IAS_COM_CODE COM ON AAT.INSURANCE_COMP_CODE = COM.ID "
                            + "where ALT.DATE_LICENSE_ACT  between TO_DATE('" + StartDate1 + "','dd/MM/yyyy') and TO_DATE('" + EndDate1 + "','dd/MM/yyyy') "
                            + ConLicense
                            + "order by ALT.LICENSE_TYPE_CODE) A ";


                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetSumLicenseStatisticsReport(string StartDate1, string EndDate1)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                string ConLicense = string.Empty;
                string Concompcode = string.Empty;
                StartDate1 = Convert.ToDateTime(StartDate1).AddYears(-543).ToShortDateString();
                EndDate1 = Convert.ToDateTime(EndDate1).AddYears(-543).ToShortDateString();



                string sql = "select * from( "
                            + "select count(*) as COUNT_COME_CODE1 "
                            + "from AG_LICENSE_T ALT inner join AG_AGENT_LICENSE_T AAT ON ALT.LICENSE_NO = AAT.LICENSE_NO "
                            + "INNER JOIN VW_IAS_COM_CODE COM ON AAT.INSURANCE_COMP_CODE = COM.ID "
                            + "where ALT.DATE_LICENSE_ACT  between TO_DATE('" + StartDate1 + "','dd/MM/yyyy') and TO_DATE('" + EndDate1 + "','dd/MM/yyyy') "
                            + "and (ALT.LICENSE_TYPE_CODE ='01'or ALT.LICENSE_TYPE_CODE ='02' or ALT.LICENSE_TYPE_CODE ='03' "
                            + "or ALT.LICENSE_TYPE_CODE ='04'or ALT.LICENSE_TYPE_CODE ='05' or ALT.LICENSE_TYPE_CODE ='06' "
                            + "or ALT.LICENSE_TYPE_CODE ='07' or ALT.LICENSE_TYPE_CODE ='08') "
                            + "order by ALT.LICENSE_TYPE_CODE) A ";



                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetTopCompanyMoveOut(string LicenseType, DateTime? DateStart, DateTime? DateEnd)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string whereLicenseType = string.Empty;
                string whereDate = string.Empty;

                if (!string.IsNullOrEmpty(LicenseType))
                {
                    if (LicenseType == "09")//รวมประเภทตัวแทนประกันชีวิต
                    {
                        whereLicenseType = " and B.LICENSE_TYPE_CODE in ('01','07') ";
                    }
                    else if (LicenseType == "10")//รวมประเภทตัวแทนประกันวินาศภัย
                    {
                        whereLicenseType = " and B.LICENSE_TYPE_CODE in ('02','05','06','08') ";
                    }
                    else
                    {
                        whereLicenseType = string.Format(" and B.LICENSE_TYPE_CODE = '{0}' ", LicenseType);
                    }
                }

                if (DateStart != null && DateEnd != null)
                {
                    whereDate = string.Format(" and (A.MOVE_DATE >= to_date('{0}','yyyymmdd') and A.MOVE_DATE <= to_date('{1}','yyyymmdd')) ",
                                                ((DateTime)DateStart).ToString_yyyyMMdd(), ((DateTime)DateEnd).ToString_yyyyMMdd());
                }

                string sql = string.Format(@"
                                select COMP_MOVE_OUT_ID COMP_CODE_OLD, NAME_OLD, COUNT_OUT, COMP_MOVE_IN_ID COMP_CODE_IN, NAME_OUT, COUNT_IN 
                                from (
	                                select  rank() over (partition by A.COMP_MOVE_OUT_ID order by A.COUNT_OUT desc, rownum) RNK,
			                                A.COMP_MOVE_OUT_ID, A.NAME_OLD, A.COUNT_OUT, A.COMP_MOVE_IN_ID, B.NAME NAME_OUT, A.COUNT_IN 
	                                from(
		                                select A.COMP_MOVE_OUT_ID, B.NAME NAME_OLD, A.COUNT_OUT, A.COMP_MOVE_IN_ID, A.COUNT_IN 
		                                from(
			                                select A.COMP_MOVE_OUT_ID, A.COUNT_OUT, B.COMP_MOVE_IN_ID, count(B.COMP_MOVE_IN_ID) COUNT_IN 
			                                from(
				                                select * from (
					                                select A.COMP_MOVE_OUT_ID, count(*) COUNT_OUT
					                                from AG_HIS_MOVE_COMP_AGENT_T A, AG_LICENSE_T B
					                                where A.LICENSE_NO = B.LICENSE_NO and A.COMP_MOVE_OUT_ID != A.COMP_MOVE_IN_ID {0} {1}
					                                group by A.COMP_MOVE_OUT_ID
					                                order by count(*) desc
				                                )where rownum between 1 and 5 
			                                ) A,
			                                (
				                                select A.COMP_MOVE_OUT_ID, A.COMP_MOVE_IN_ID
				                                from AG_HIS_MOVE_COMP_AGENT_T A, AG_LICENSE_T B
				                                where A.LICENSE_NO = B.LICENSE_NO and A.COMP_MOVE_OUT_ID != A.COMP_MOVE_IN_ID {0} {1}
			                                )B
			                                where A.COMP_MOVE_OUT_ID = B.COMP_MOVE_OUT_ID
			                                group by A.COMP_MOVE_OUT_ID, A.COUNT_OUT, B.COMP_MOVE_IN_ID
		                                )A, VW_IAS_COM_CODE B
		                                where A.COMP_MOVE_OUT_ID = B.ID
	                                )A, VW_IAS_COM_CODE B
	                                where A.COMP_MOVE_IN_ID = B.ID
	                                order by A.COUNT_OUT desc, A.COUNT_IN desc
                                )where RNK <= 3 ", whereLicenseType, whereDate);
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetTopCompanyMoveOut", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.SubPaymentHead>> GetIndexSubPaymentH(string groupReqNo)
        {
            var res = new DTO.ResponseService<List<DTO.SubPaymentHead>>();

            try
            {
                var result = (from a in base.ctx.AG_IAS_SUBPAYMENT_H_T
                              where a.GROUP_REQUEST_NO.Equals(groupReqNo)
                              select new DTO.SubPaymentHead
                              {
                                  SEQ_OF_GROUP = a.SEQ_OF_GROUP,
                                  HEAD_REQUEST_NO = a.HEAD_REQUEST_NO,
                                  UPLOAD_GROUP_NO = a.UPLOAD_GROUP_NO,
                                  GROUP_REQUEST_NO = a.GROUP_REQUEST_NO

                              }).OrderBy(idx => idx.SEQ_OF_GROUP).ToList();

                if (result.Count > 0)
                {
                    res.DataResponse = result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return res;

        }

        public DTO.ResponseService<List<DTO.SubPaymentDetail>> GetIndexSubPaymentD(string headReqNo)
        {
            var res = new DTO.ResponseService<List<DTO.SubPaymentDetail>>();

            try
            {
                var result = (from a in base.ctx.AG_IAS_SUBPAYMENT_D_T
                              where a.HEAD_REQUEST_NO.Equals(headReqNo)
                              select new DTO.SubPaymentDetail
                              {
                                  SEQ_OF_SUBGROUP = a.SEQ_OF_SUBGROUP,
                                  SEQ_NO = a.SEQ_NO,
                                  HEAD_REQUEST_NO = a.HEAD_REQUEST_NO,
                                  PAYMENT_NO = a.PAYMENT_NO,
                                  UPLOAD_GROUP_NO = a.UPLOAD_GROUP_NO

                              }).OrderBy(idx => idx.SEQ_OF_SUBGROUP).ToList();

                if (result.Count > 0)
                {
                    res.DataResponse = result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return res;

        }

        public DTO.ResponseService<List<DTO.PersonLicenseDetail>> GetIndexLicenseD(string uploadGroupNo)
        {
            var res = new DTO.ResponseService<List<DTO.PersonLicenseDetail>>();

            try
            {
                var result = (from a in base.ctx.AG_IAS_LICENSE_D
                              where a.UPLOAD_GROUP_NO.Equals(uploadGroupNo)
                              select new DTO.PersonLicenseDetail
                              {
                                  SEQ_NO = a.SEQ_NO,
                                  HEAD_REQUEST_NO = a.HEAD_REQUEST_NO,
                                  UPLOAD_GROUP_NO = a.UPLOAD_GROUP_NO,
                                  LICENSE_NO = a.LICENSE_NO

                              }).OrderBy(idx => idx.SEQ_NO).ToList();

                if (result.Count > 0)
                {
                    res.DataResponse = result;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return res;

        }

        public DTO.ResponseService<DataSet> GetReplacementReport(string LicenseTypeCode, string Compcode, string Replacement, string StartDate, string EndDate)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                string ConLicense = string.Empty;
                string Concompcode = string.Empty;
                string ConReplacement1 = string.Empty;
                string ConReplacement2 = string.Empty;
                string Fees = string.Empty;



                if (System.Globalization.CultureInfo.CurrentCulture.Name.Equals("th-TH"))
                {
                    StartDate = Convert.ToDateTime(StartDate).AddYears(-543).ToShortDateString();
                    EndDate = Convert.ToDateTime(EndDate).AddYears(-543).ToShortDateString();
                }
                else
                {
                    StartDate = Convert.ToDateTime(StartDate).ToShortDateString();
                    EndDate = Convert.ToDateTime(EndDate).ToShortDateString();
                }



                if (LicenseTypeCode != "")
                {
                    if (LicenseTypeCode == "09")
                    {
                        ConLicense = "  and (LH.LICENSE_TYPE_CODE ='01' or LH.LICENSE_TYPE_CODE ='07') ";
                    }
                    else if (LicenseTypeCode == "10")
                    {
                        ConLicense = "  and (LH.LICENSE_TYPE_CODE ='02' or LH.LICENSE_TYPE_CODE ='05' or LH.LICENSE_TYPE_CODE ='06' or LH.LICENSE_TYPE_CODE ='08' )";
                    }
                    else
                    {
                        ConLicense = " and LH.LICENSE_TYPE_CODE ='" + LicenseTypeCode + "'";
                    }
                }
                if (Replacement != "")
                {
                    if (Replacement == "01")//เสียค่าธรรมเนียม
                    {
                        Fees = " and LD.FEES !='0' ";
                    }
                    else if (Replacement == "02")//เสียค่าธรรมเนียม
                    {
                        Fees = " and LD.FEES ='0' ";
                    }
                    else
                    {
                        Fees = " ";
                    }
                }
                if (Compcode != "")
                {
                    Concompcode = " and LH.comp_code ='" + Compcode + "'";
                }
                else
                {
                    Concompcode = " ";
                }

                ConReplacement1 = "select lh.upload_group_no,LH.Comp_code,comp.name,LD.FEES,LH.LICENSE_TYPE_CODE,ln.license_type_name,ld.oic_approved_date, case LD.Fees when 0 then 'ใบแทนที่ไม่เสียค่าธรรมเนียม' "
                                + "else 'ใบแทนที่เสียค่าธรรมเนียม' end  as ReplaceName from ag_ias_license_h LH "
                                + "left join ag_ias_license_d LD on lh.upload_group_no =ld.upload_group_no "
                                + "left join VW_IAS_COM_CODE Comp on lh.comp_code=COMP.ID "
                                + "left join AG_IAS_LICENSE_TYPE_R LN on LH.LICENSE_TYPE_CODE=LN.LICENSE_TYPE_CODE "
                                + "where lh.petition_type_code='16' "
                                + ConLicense
                                + Fees
                    //and (LH.LICENSE_TYPE_CODE='01' or LH.LICENSE_TYPE_CODE='07')
                    //and LD.FEES ='0'
                                + "and ld.oic_approved_date  between TO_DATE('" + StartDate + "','dd/MM/yyyy') and TO_DATE('" + EndDate + "','dd/MM/yyyy') "
                                + Concompcode;


                ConReplacement2 = "select count(*)as countType,LH.LICENSE_TYPE_CODE as LICENSE_TYPE_CODE2 from ag_ias_license_h LH "
                                + "left join ag_ias_license_d LD on lh.upload_group_no =ld.upload_group_no "
                                + "where lh.petition_type_code='16' "
                                + ConLicense
                                + Fees
                    //and (LH.LICENSE_TYPE_CODE='01' or LH.LICENSE_TYPE_CODE='07')
                    //and LD.FEES ='0'
                                + "and ld.oic_approved_date  between TO_DATE('" + StartDate + "','dd/MM/yyyy') and TO_DATE('" + EndDate + "','dd/MM/yyyy') "
                                + Concompcode
                                + "group by LH.LICENSE_TYPE_CODE";


                string sql = "select eee.*,ROUND(((eee.countcomp*100)/eee.countType),2) AS FORSHARE,eee.license_type_name from ( "
                            + "select * from( "
                            + "select count(*) as countcomp,aaa.Comp_code,aaa.name,aaa.License_type_code,aaa.License_type_name,aaa.oic_approved_date,aaa.ReplaceName from "
                            + "( "
                            + ConReplacement1
                            + ") "
                            + "aaa group by aaa.Comp_code, aaa.name, aaa.License_type_code, aaa.License_type_name, aaa.oic_approved_date, aaa.ReplaceName "
                            + "order by aaa.LICENSE_TYPE_CODE,aaa.comp_code "

                            + ")ddd "
                            + "INNER JOIN "
                            + "( "
                            + ConReplacement2
                            + ")ccc ON ddd.LICENSE_TYPE_CODE = ccc.LICENSE_TYPE_CODE2 )eee "
                            + "order by eee.License_type_code,eee.countcomp desc";

                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetLincse0304(string lincense)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            string sql = "select AC.ITEM,AC.ID,AC.ITEM_VALUE from AG_IAS_CONFIG AC where AC.ID IN('11','12')";
            OracleDB db = new OracleDB();
            res.DataResponse = db.GetDataSet(sql);
            return res;
        }

        public DTO.ResponseService<string> AddLincse0304(Dictionary<string, string> lincense)
        {
            DTO.ResponseService<string> res = new ResponseService<string>();
            try
            {
                Func<string, string> ConvertLicense = delegate(string key)
                {
                    //ID = 11 : License = 03
                    if ((key == "11") && (key != null))
                    {
                        key = "03";
                    }
                    //ID = 12 : License = 04
                    else
                    {
                        key = "04";
                    }
                    return key;
                };

                Func<string, string> ConvertStatus = delegate(string value)
                {
                    //ID = 11 : License = 03
                    if ((value == "0") && (value != null))
                    {
                        value = "N";
                    }
                    //ID = 12 : License = 04
                    else
                    {
                        value = "Y";
                    }
                    return value;
                };

                foreach (var item in lincense)
                {
                    var config = ctx.AG_IAS_CONFIG.FirstOrDefault(x => x.ID == item.Key);
                    if (config != null)
                    {
                        config.ITEM_VALUE = item.Value;


                        string LicenseType = ConvertLicense(config.ID);
                        string Status = ConvertStatus(config.ITEM_VALUE);

                        var result = (from A in base.ctx.AG_IAS_VALIDATE_LICENSE_CON
                                      where A.ID.Equals(4) && A.ITEM_GROUP.Equals(4) && A.LICENSE_TYPE_CODE == LicenseType && A.PETITION_TYPE_CODE == "11" && A.RENEW_TIME.Equals(0)
                                      select A).ToList();
                        if (result.Count > 0)
                        {
                            result.ForEach(s => s.STATUS = Status);
                        }


                    }
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.ConfigDocument>> GetLicenseConfigByPetition(string petitionType, string licenseType)
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                Int16 renewTime = 0;
                List<DTO.ConfigDocument> result = (from A in base.ctx.AG_IAS_VALIDATE_LICENSE_CON
                                                   where A.ID == 4 &&
                                                   A.ITEM_GROUP == 4 &&
                                                   A.PETITION_TYPE_CODE == petitionType &&
                                                   A.RENEW_TIME == renewTime &&
                                                   A.LICENSE_TYPE_CODE == licenseType
                                                   select new DTO.ConfigDocument
                                                   {
                                                       ID = A.ID,
                                                       LICENSE_TYPE_CODE = A.LICENSE_TYPE_CODE,
                                                       STATUS = A.STATUS,
                                                       PETITION_TYPE_CODE = A.PETITION_TYPE_CODE

                                                   }).ToList();

                res.DataResponse = result;

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("LicenseService_GetLicenseConfigByPetition", ex.Message);
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        public DTO.ResponseMessage<bool> GetAgentCheckTrain(string id)
        {
            var res = new DTO.ResponseMessage<bool>();
            var sql = string.Empty;
            OracleDB ora = new OracleDB();


            try
            {
                sql = "SELECT ITEM_VALUE "
                      + "FROM AG_IAS_CONFIG "
                      + "WHERE ID ='" + id.Trim() + "' ";

                DataTable dtchkConfig = ora.GetDataTable(sql);


                if (dtchkConfig.Rows.Count == 0)
                {
                    res.ResultMessage = false;
                }
                else
                {
                    DataRow dr = dtchkConfig.Rows[0];
                    string itemValue = dr[0].ToString();

                    if (itemValue == "1")
                    {
                        res.ResultMessage = true;
                    }
                    else
                    {
                        res.ResultMessage = false;
                    }
                }




            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetAgentCheckTrain", ex);
            }


            return res;
        }

        public DTO.ResponseService<DataSet> GetLicenseDetailByCriteria(DateTime dateStart, DateTime dateEnd, string IdCardNo, string Names, string Lastname, string LicenseType, string CompCode, int Page, int RowPerPage, bool isCount)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = string.Empty;

                sql += (isCount) ? " select count(*) from( " : " select * from ( ";
                sql += " select rownum as NUMROW, D.UPLOAD_GROUP_NO, D.ID_CARD_NO, D.NAMES, D.LASTNAME, H.COMP_CODE, "
                        + "     (case when H.COMP_CODE is null or H.COMP_NAME is null then 'บุคคลทั่วไป/ตัวแทน/นายหน้า' else H.COMP_NAME end) as COMP_NAME, "
                        + "     PR.PETITION_TYPE_NAME, LR.LICENSE_TYPE_NAME, D.OIC_APPROVED_DATE, D.IS_DOWNLOAD "
                        + " from AG_IAS_LICENSE_D D, AG_IAS_LICENSE_H H, AG_IAS_LICENSE_TYPE_R LR, AG_IAS_PETITION_TYPE_R PR "
                        + " where D.UPLOAD_GROUP_NO = H.UPLOAD_GROUP_NO and H.PETITION_TYPE_CODE =  PR.PETITION_TYPE_CODE "
                        + "  and H.LICENSE_TYPE_CODE = LR.LICENSE_TYPE_CODE and D.OIC_APPROVED_DATE is not null "
                        + "  and (D.OIC_APPROVED_DATE >= to_date('" + dateStart.ToString_yyyyMMdd() + "000000','yyyymmddhh24miss') "
                        + "  and D.OIC_APPROVED_DATE <= to_date('" + dateEnd.ToString_yyyyMMdd() + "235959','yyyymmddhh24miss')) ";

                if (!string.IsNullOrEmpty(IdCardNo))
                    sql += string.Format(" and D.ID_CARD_NO like '{0}%' ", IdCardNo);

                if (!string.IsNullOrEmpty(Names))
                    sql += string.Format(" and D.NAMES like '%{0}%' ", Names);

                if (!string.IsNullOrEmpty(Lastname))
                    sql += string.Format(" and D.LASTNAME like '%{0}%' ", Lastname);

                if (!string.IsNullOrEmpty(LicenseType))
                    sql += string.Format(" and H.LICENSE_TYPE_CODE = '{0}' ", LicenseType);

                if (!string.IsNullOrEmpty(CompCode))
                    sql += string.Format(" and H.COMP_CODE = '{0}' ", CompCode);

                sql += (isCount) ? " ) "
                        : string.Format(" ) where NUMROW between {0} and {1} "
                            , (((Page * RowPerPage) - RowPerPage) + 1), (Page * RowPerPage));

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_GetRenewLicenseDByCriteria", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> GenZipFileLicenseByIdCardNo(List<DTO.GenLicenseDetail> LicenseDetail, string username)
        {
            DTO.ResponseService<String> res = new ResponseService<string>();
            try
            {
                String fileZip = GenZipLicenseRequest.StartCompressByOicApprove(ctx, LicenseDetail, username, String.Empty);
                if (String.IsNullOrEmpty(fileZip))
                {
                    res.ErrorMsg = Resources.errorLicenseService_54;
                }

                foreach (var LD in LicenseDetail)
                {
                    AG_IAS_LICENSE_D update = base.ctx.AG_IAS_LICENSE_D.Where(x => x.UPLOAD_GROUP_NO == LD.UPLOAD_GROUP_NO && x.ID_CARD_NO == LD.ID_CARD_NO).FirstOrDefault();
                    if (update != null && update.IS_DOWNLOAD != "T")
                    {
                        update.IS_DOWNLOAD = "T";
                    }

                }
                ctx.SaveChanges();

                res.DataResponse = CryptoBase64.Encryption(fileZip);
            }
            catch (ApplicationException appex)
            {
                res.ErrorMsg = appex.Message;
                LoggerFactory.CreateLog().Fatal("LicenseService_GenZipFileLicenseByIdCardNo", appex);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorLicenseService_055;
                LoggerFactory.CreateLog().Fatal("LicenseService_GenZipFileLicenseByIdCardNo", ex);

            }

            return res;
        }

        public DTO.ResponseMessage<bool> ChkLicenseAboutActive(string idCard, string licenseType)
        {
            var res = new DTO.ResponseMessage<bool>();
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            try
            {
                if (licenseType == "04")
                {
                    sql = "SELECT lt.LICENSE_NO "
                         + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                         + "WHERE  lt.RECORD_STATUS is  null "
                         + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + idCard.Trim() + "') or "
                         + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + idCard.Trim() + "')) "
                         + "and lt.EXPIRE_DATE >= sysdate "
                         + "and lt.REVOKE_TYPE_CODE is null "
                         + "and lt.REVOKE_LICENSE_DATE is null "
                         + "and lt.LICENSE_TYPE_CODE = '02' "
                         + "group by lt.LICENSE_NO ";
                }
                else if (licenseType == "03")
                {
                    sql = "SELECT lt.LICENSE_NO "
                      + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                      + "WHERE  lt.RECORD_STATUS is  null "
                      + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + idCard.Trim() + "') or "
                      + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + idCard.Trim() + "')) "
                      + "and lt.EXPIRE_DATE >= sysdate "
                      + "and lt.REVOKE_TYPE_CODE is null "
                      + "and lt.REVOKE_LICENSE_DATE is null "
                      + "and lt.LICENSE_TYPE_CODE = '01' "
                      + "group by lt.LICENSE_NO ";
                }
                else if (licenseType == "01")
                {
                    sql = "SELECT lt.LICENSE_NO "
                     + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                     + "WHERE  lt.RECORD_STATUS is  null "
                     + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + idCard.Trim() + "') or "
                     + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + idCard.Trim() + "')) "
                     + "and lt.EXPIRE_DATE >= sysdate "
                     + "and lt.REVOKE_TYPE_CODE is null "
                     + "and lt.REVOKE_LICENSE_DATE is null "
                     + "and lt.LICENSE_TYPE_CODE = '03' "
                     + "group by lt.LICENSE_NO ";
                }
                else if (licenseType == "02")
                {
                    sql = "SELECT lt.LICENSE_NO "
                   + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag,AG_AGENT_LICENSE_PERSON_T ap "
                   + "WHERE  lt.RECORD_STATUS is  null "
                   + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + idCard.Trim() + "') or "
                   + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + idCard.Trim() + "')) "
                   + "and lt.EXPIRE_DATE >= sysdate "
                   + "and lt.REVOKE_TYPE_CODE is null "
                   + "and lt.REVOKE_LICENSE_DATE is null "
                   + "and lt.LICENSE_TYPE_CODE = '04' "
                   + "group by lt.LICENSE_NO ";
                }
                else
                {
                    sql = string.Empty;
                }


                if (!string.IsNullOrEmpty(sql))
                {
                    DataTable chkActive = ora.GetDataTable(sql);

                    if (chkActive != null && chkActive.Rows.Count > 0)
                    {
                        res.ResultMessage = false;
                    }
                    else
                    {
                        res.ResultMessage = true;
                    }
                }
                else
                {
                    res.ResultMessage = true;
                }


            }
            catch (Exception ex)
            {

                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("LicenseService_ChkLicenseAboutActive", ex);
            }

            return res;
        }

    }
}

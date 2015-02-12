using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using IAS.DAL;
using IAS.Utils;
using System.ServiceModel.Activation;
using IAS.Common.Email;
using System.Configuration;
using IAS.Common.Logging;
using IAS.DataServices.Helpers;

namespace IAS.DataServices.Account
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AccountService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class AccountService : AbstractService, IAccountService
    {

        #region Constructor

        public AccountService() { }
        public AccountService(IAS.DAL.Interfaces.IIASPersonEntities _ctx) : base(_ctx) { }

        #endregion

        public DTO.ResponseService<DataSet> GetAccountDetail(string member_type, string user_name, string id_card, string email, int num_page, int RowPerPage, Boolean Count)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "";
                if (Count)
                {
                    sql += "select count(*) from (";
                }
                else
                {
                    sql += " select * from ( ";
                }
                sql += " select rownum as num, P.ID USER_ID, P.ID_CARD_NO,TN.NAME TITLE_NAME, P.NAMES, P.LASTNAME, "
                        + " P.EMAIL, P.MEMBER_TYPE, T.MEMBER_NAME, U.IS_ACTIVE "
                        + " from AG_IAS_PERSONAL_T P, AG_IAS_USERS U, AG_IAS_MEMBER_TYPE T, VW_IAS_TITLE_NAME_PRIORITY TN  "
                        + " where P.ID = U.USER_ID and P.MEMBER_TYPE = T.MEMBER_CODE and U.IS_ACTIVE <> 'C' and P.PRE_NAME_CODE = TN.ID ";

                if (member_type != "" && member_type != "0") sql += " and P.MEMBER_TYPE = " + member_type;
                if (user_name != "") sql += " and (P.NAMES || P.LASTNAME) like '%" + user_name + "%' ";
                if (id_card != "") sql += " and P.ID_CARD_NO like '%" + id_card + "%' ";
                if (email != "") sql += " and P.EMAIL like '%" + email + "%' ";
                if (Count)
                {
                    sql += ") ";
                }
                else
                {
                    sql += " ) where num between " + (((num_page * RowPerPage) - RowPerPage) + 1) + " and " + (num_page * RowPerPage);
                }
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการค้นหาข้อมูล";
                LoggerFactory.CreateLog().Fatal("AccountService_GetAccountDetail", ex);
            }
            return res;
        }

        public DTO.ResponseService<DTO.AccountDetail> GetAccountDetailById(string id)
        {
            DTO.ResponseService<DTO.AccountDetail> res = new DTO.ResponseService<DTO.AccountDetail>();
            try
            {
                var qry = (from p in base.ctx.AG_IAS_PERSONAL_T
                           join u in base.ctx.AG_IAS_USERS on p.ID equals u.USER_ID
                           join t in base.ctx.AG_IAS_MEMBER_TYPE on p.MEMBER_TYPE equals t.MEMBER_CODE
                           where p.ID == id
                           select new DTO.AccountDetail { 
                                ID = p.ID,
                                ID_CARD_NO = p.ID_CARD_NO,
                                NAMES = p.NAMES,
                                LASTNAME = p.LASTNAME,
                                MEMBER_TYPE = p.MEMBER_TYPE,
                                EMAIL = p.EMAIL,
                                ACTIVE = u.IS_ACTIVE,
                                DELETE_USER = u.DELETE_USER,
                                OTH_DELETE_USER = u.OTH_DELETE_USER,
                                COMP_CODE = p.COMP_CODE,
                                MEMBER_TYPE_NAME = t.MEMBER_NAME,
                                TITLE_NAME = p.PRE_NAME_CODE,
                                OTH_USER_TYPE = u.OTH_USER_TYPE
                           }).FirstOrDefault();
                if (qry != null)
                {
                    short tid = qry.TITLE_NAME.ToShort();
                    qry.TITLE_NAME = base.ctx.VW_IAS_TITLE_NAME_PRIORITY.FirstOrDefault(s => s.ID == tid).NAME;
                    res.DataResponse = qry;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการเรียกดูข้อมูล";
                LoggerFactory.CreateLog().Fatal(string.Format("AccountService_GetAccountDetailById USER_ID :{0}", id), ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> EditMemberTypeAndActive(DTO.AccountDetail ent, DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<Boolean> res = new DTO.ResponseMessage<bool>();
            try
            {
                string loginfo = string.Empty;
                var Person = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(x => x.ID == ent.ID);
                var User = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_ID == ent.ID);
                if (Person == null || User == null)
                {
                    res.ErrorMsg = "ไม่พบข้อมูลผู้ใช้งาน";
                    LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูลผู้ใช้งาน USER_ID :{0} ในตาราง AG_IAS_PERSONAL_T หรือ AG_IAS_USERS", ent.ID));
                    return res;
                }

                #region Check from TYPE to TYPE
                string err = "ไม่สามารถย้ายประเภทผู้ใช้งาน";
                switch (Person.MEMBER_TYPE)
                {
                    case "1":
                        if (ent.MEMBER_TYPE != "1")
                        {
                            res.ErrorMsg = err;
                            return res;
                        }
                        break;
                    case "2":
                    case "3":
                    case "7":
                        if (ent.MEMBER_TYPE != "2" && ent.MEMBER_TYPE != "3" && ent.MEMBER_TYPE != "7")
                        {
                            res.ErrorMsg = err;
                            return res;
                        }
                        break;
                    case "4":
                    case "5":
                    case "6":
                        if (ent.MEMBER_TYPE != "5" && ent.MEMBER_TYPE != "6" && ent.MEMBER_TYPE != "4")
                        {
                            res.ErrorMsg = err;
                            return res;
                        }
                        break;
                }
                #endregion Check from TYPE to TYPE

                /**
                 * ย้าย MEMBER_TYPE จาก (02-บริษัท, 03-สมาคม) ไปเป็น (07-เจ้าหน้าที่สนามสอบ)
                 * ให้ตรวจสอบ (02-บริษัท, 03-สมาคม) ว่ามีการสมัครสอบหรือไม่?
                 * หากมีการสมัครสอบจะไม่สามารถย้ายไปเป็น (07-เจ้าหน้าที่สนามสอบ)
                 */
                if ((Person.MEMBER_TYPE == "2" || Person.MEMBER_TYPE == "3") && ent.MEMBER_TYPE == "7")
                {
                    OracleDB ora = new OracleDB();
                    string sql = "select count(*) haveCount from AG_APPLICANT_T where ID_CARD_NO = '" + Person.ID_CARD_NO + "' and "
                                + " EXAM_PLACE_CODE in (select EXAM_PLACE_CODE from AG_EXAM_PLACE_R) ";
                    DataTable dt = ora.GetDataTable(sql);
                    if (dt.Rows[0]["haveCount"].ToInt() > 0)
                    {
                        res.ErrorMsg = "ไม่สามารถย้ายประเภทผู้ใช้งาน";
                        return res;
                    }
                }

                /*
                 * 2-บริษัท, 3-สมาคม, 5-คปภ.การเงิน, 6-คปภ.ตัวแทน, 7-เจ้าหน้าที่สนามสอบ
                 */
                if (ent.MEMBER_TYPE == "2" || ent.MEMBER_TYPE == "3" || ent.MEMBER_TYPE == "7")
                {
                    if (ent.COMP_CODE == Person.COMP_CODE && Person.MEMBER_TYPE == ent.MEMBER_TYPE)
                    {
                        res.ErrorMsg = "ไม่มีการเปลี่ยนแปลงข้อมูล";
                        return res;
                    }

                    var Regis = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(s => s.ID == ent.ID);
                    if (Regis == null)
                    {
                        res.ErrorMsg = "ไม่พบข้อมูลผู้ใช้งาน";
                        return res;
                    }

                    // ย้ายได้เฉพาะ status = 2-อนุมัติ(สมัคร), 5-อนุมัติ(แก้ไข)
                    if (Person.STATUS != "2" && Person.STATUS != "5" && Person.STATUS != null)
                    {
                        var Status = base.ctx.AG_IAS_STATUS.FirstOrDefault(s => s.STATUS_CODE == Person.STATUS);
                        res.ErrorMsg = "ไม่สามารถย้ายประเภทผู้ใช้งาน เนื่องจากสถานะ " + Status.STATUS_NAME;
                        return res;
                    }
                    
                    string ID = OracleDB.GetGenAutoId();
                    #region AG_IAS_PERSONAL_T
                    AG_IAS_PERSONAL_T newPerson = new AG_IAS_PERSONAL_T
                    {
                        ID = ID,                            //new Data
                        MEMBER_TYPE = ent.MEMBER_TYPE,      //new Data
                        ID_CARD_NO = Person.ID_CARD_NO,
                        EMPLOYEE_NO = Person.EMPLOYEE_NO,
                        PRE_NAME_CODE = Person.PRE_NAME_CODE,
                        NAMES = Person.NAMES,
                        LASTNAME = Person.LASTNAME,
                        NATIONALITY = Person.NATIONALITY,
                        BIRTH_DATE = Person.BIRTH_DATE,
                        SEX = Person.SEX,
                        EDUCATION_CODE = Person.EDUCATION_CODE,
                        ADDRESS_1 = Person.ADDRESS_1,
                        ADDRESS_2 = Person.ADDRESS_2,
                        AREA_CODE = Person.AREA_CODE,
                        PROVINCE_CODE = Person.PROVINCE_CODE,
                        ZIP_CODE = Person.ZIP_CODE,
                        TELEPHONE = Person.TELEPHONE,
                        LOCAL_ADDRESS1 = Person.LOCAL_ADDRESS1,
                        LOCAL_ADDRESS2 = Person.LOCAL_ADDRESS2,
                        LOCAL_AREA_CODE = Person.LOCAL_AREA_CODE,
                        LOCAL_PROVINCE_CODE = Person.LOCAL_PROVINCE_CODE,
                        LOCAL_ZIPCODE = Person.LOCAL_ZIPCODE,
                        LOCAL_TELEPHONE = Person.LOCAL_TELEPHONE,
                        EMAIL = Person.EMAIL,
                        STATUS = Person.STATUS,
                        TUMBON_CODE = Person.TUMBON_CODE,
                        LOCAL_TUMBON_CODE = Person.LOCAL_TUMBON_CODE,
                        COMP_CODE = ent.COMP_CODE, //new Data
                        CREATED_BY = userProfile.Id,        //new Data
                        CREATED_DATE = DateTime.Now,        //new Data
                        UPDATED_BY = userProfile.Id,        //new Data
                        UPDATED_DATE = DateTime.Now,        //new Data
                        APPROVE_RESULT = Person.APPROVE_RESULT,
                        APPROVED_BY = Person.APPROVED_BY,
                        AGENT_TYPE = Person.AGENT_TYPE,
                        SIGNATUER_IMG = Person.SIGNATUER_IMG,
                        IMG_SIGN = Person.IMG_SIGN
                    };
                    #endregion

                    #region AG_IAS_USERS
                    AG_IAS_USERS newUser = new AG_IAS_USERS
                    {
                        USER_ID = ID,                                   //new Data
                        USER_NAME = User.USER_NAME,
                        USER_PASS = User.USER_PASS,
                        USER_TYPE = ent.MEMBER_TYPE,                    //new Data
                        IS_ACTIVE = User.IS_ACTIVE,
                        USER_RIGHT = ent.MEMBER_TYPE,                   //new Data
                        USER_TERM_ACCEPTED = User.USER_TERM_ACCEPTED,
                        CREATED_BY = userProfile.Id,                    //new Data
                        CREATED_DATE = DateTime.Now,                    //new Data
                        UPDATED_BY = userProfile.Id,                    //new Data
                        UPDATED_DATE = DateTime.Now,                    //new Data
                        RESET_TIMES = User.RESET_TIMES,
                        OIC_TYPE = User.OIC_TYPE,
                        MEMBER_TYPE = ent.MEMBER_TYPE,                  //new Data
                        OIC_EMP_NO = User.OIC_EMP_NO,
                        IS_APPROVE = User.IS_APPROVE,
                        APPROVED_BY = User.APPROVED_BY,
                        STATUS = User.STATUS,
                        APP_CLOSED = User.APP_CLOSED,
                        LASTPASSWORD_CHANGDATE = User.LASTPASSWORD_CHANGDATE,
                        OTH_USER_TYPE = ent.OTH_USER_TYPE               //new Data
                    };
                    #endregion

                    #region AG_IAS_REGISTRATION_T
                    AG_IAS_REGISTRATION_T newRegis = new AG_IAS_REGISTRATION_T { 
                        ID = ID,
                        MEMBER_TYPE = ent.MEMBER_TYPE,
                        ID_CARD_NO = Regis.ID_CARD_NO,
                        EMPLOYEE_NO = Regis.EMPLOYEE_NO,
                        PRE_NAME_CODE = Regis.PRE_NAME_CODE,
                        NAMES = Regis.NAMES,
                        LASTNAME = Regis.LASTNAME,
                        NATIONALITY = Regis.NATIONALITY,
                        BIRTH_DATE = Regis.BIRTH_DATE,
                        SEX = Regis.SEX,
                        EDUCATION_CODE = Regis.EDUCATION_CODE,
                        ADDRESS_1 = Regis.ADDRESS_1,
                        ADDRESS_2 = Regis.ADDRESS_2,
                        AREA_CODE = Regis.AREA_CODE,
                        PROVINCE_CODE = Regis.PROVINCE_CODE,
                        ZIP_CODE = Regis.ZIP_CODE,
                        TELEPHONE = Regis.TELEPHONE,
                        LOCAL_ADDRESS1 = Regis.LOCAL_ADDRESS1,
                        LOCAL_ADDRESS2 = Regis.LOCAL_ADDRESS2,
                        LOCAL_AREA_CODE = Regis.LOCAL_AREA_CODE,
                        LOCAL_PROVINCE_CODE = Regis.LOCAL_PROVINCE_CODE,
                        LOCAL_ZIPCODE = Regis.LOCAL_ZIPCODE,
                        LOCAL_TELEPHONE = Regis.LOCAL_TELEPHONE,
                        EMAIL = Regis.EMAIL,
                        STATUS = Regis.STATUS,
                        TUMBON_CODE = Regis.TUMBON_CODE,
                        LOCAL_TUMBON_CODE = Regis.LOCAL_TUMBON_CODE,
                        COMP_CODE = ent.COMP_CODE,
                        CREATED_BY = userProfile.Id,
                        CREATED_DATE = DateTime.Now,
                        UPDATED_BY = userProfile.Id, 
                        UPDATED_DATE = DateTime.Now,
                        NOT_APPROVE_DATE = Regis.NOT_APPROVE_DATE,
                        LINK_REDIRECT = Regis.LINK_REDIRECT,
                        REG_PASS  = Regis.REG_PASS,
                        APPROVE_RESULT = Regis.APPROVE_RESULT,
                        APPROVED_BY = Regis.APPROVED_BY,
                        AGENT_TYPE = Regis.AGENT_TYPE,
                        IMPORT_STATUS = Regis.IMPORT_STATUS
                    };
                    #endregion

                    base.ctx.AG_IAS_PERSONAL_T.AddObject(newPerson);
                    base.ctx.AG_IAS_USERS.AddObject(newUser);
                    base.ctx.AG_IAS_REGISTRATION_T.AddObject(newRegis);

                    Person.STATUS = "7";
                    Regis.STATUS = "7";
                    
                    User.IS_ACTIVE = "C";
                    User.UPDATED_BY = userProfile.Id;
                    User.UPDATED_DATE = DateTime.Now;
                    User.OTH_USER_TYPE = ent.OTH_USER_TYPE;
                    loginfo = string.Format("{0} {1} เปลี่ยนประเภทผู้ใช้งานจาก USER_ID:{2} เป็น USER_ID:{3}",Person.NAMES, Person.LASTNAME, ent.ID, ID);
                }
                else if (ent.MEMBER_TYPE == "5" || ent.MEMBER_TYPE == "6" || ent.MEMBER_TYPE == "4")
                {
                    if (ent.MEMBER_TYPE == Person.MEMBER_TYPE)
                    {
                        res.ErrorMsg = "ไม่มีการเปลี่ยนแปลงข้อมูล";
                        return res;
                    }
                    loginfo = string.Format("[OIC] USER_ID:{0} {1} {2} เปลี่ยนประเภทผู้ใช้งานจาก MEMBER_TYPE:{3} เป็น MEMBER_TYPE:{4}", ent.ID, Person.NAMES, Person.LASTNAME, Person.MEMBER_TYPE, ent.MEMBER_TYPE);
                    Person.MEMBER_TYPE = ent.MEMBER_TYPE;
                    Person.UPDATED_BY = userProfile.Id;
                    Person.UPDATED_DATE = DateTime.Now;

                    User.USER_TYPE = ent.MEMBER_TYPE;
                    User.MEMBER_TYPE = ent.MEMBER_TYPE;
                    User.UPDATED_BY = userProfile.Id;
                    User.UPDATED_DATE = DateTime.Now;
                    User.OTH_USER_TYPE = ent.OTH_USER_TYPE;
                }
                else
                {
                    res.ErrorMsg = "ไม่มีการเปลี่ยนแปลงข้อมูล";
                    return res;
                }

                base.ctx.SaveChanges();
                LoggerFactory.CreateLog().LogInfo(loginfo);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการเปลี่ยนประเภทผู้ใช้งาน";
                LoggerFactory.CreateLog().Fatal(string.Format("AccountService_EditMemberTypeAndActive USER_ID: {0}", ent.ID), ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> IsChangePassword(DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<Boolean> res = new DTO.ResponseMessage<bool>();
            try
            {
                var User = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_ID == userProfile.Id);
                if (User == null)
                {
                    res.ErrorMsg = "ไม่พบข้อมูลผู้ใช้งาน";
                    LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูลผู้ใช้งาน AG_IAS_USERS->USER_ID :{0}", userProfile.Id));
                    return res;
                }

                res.ResultMessage = false;

                // เปลี่ยนรหัสผ่านครั้งแรกที่ Login เฉพาะเจ้าหน้าที่สนามสอบ
                if (User.USER_TYPE == "7")
                {
                    var begin_config = base.ctx.AG_IAS_APPROVE_CONFIG.FirstOrDefault(s => s.KEYWORD == "Change_Password_Begin");
                    if (begin_config.ITEM_VALUE == "Y" && String.IsNullOrEmpty(User.RESET_TIMES.ToString()))
                    {
                        res.ResultMessage = true;
                        return res;
                    }
                }

                //เปลี่ยนทุก 3 เดือน
                var month_config = base.ctx.AG_IAS_APPROVE_CONFIG.FirstOrDefault(s => s.KEYWORD == "Change_Password_3months");
                if (month_config.ITEM_VALUE == "Y")
                {
                    int addMonths = 3;
                    DateTime ChangsPass = (DateTime)User.LASTPASSWORD_CHANGDATE;

                    string formatCurrDate = DateTime.Now.ToString("dd/MM/yyyy");
                    string formatPassDate = (ChangsPass.AddMonths(addMonths)).ToString("dd/MM/yyyy");

                    DateTime currTime = DateTime.Parse(formatCurrDate);
                    DateTime passTime = DateTime.Parse(formatPassDate);

                    int dateCompare = DateTime.Compare(passTime, currTime);
                    // วันที่เปลี่ยน password ล่าสุด + เงื่อนไข  แล้วน้อยกว่าวันที่ปัจจุบัน
                    if (dateCompare < 1)
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
                res.ErrorMsg = "พบข้อผิดพลาดในการตรวจสอบการเปลี่ยนรหัสผ่าน";
                LoggerFactory.CreateLog().Fatal(string.Format("AccountService_IsChangePassword USER_ID :{0}", userProfile.Id), ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> ChangePassword(DTO.User user, string newPassword)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                string _oldPassword = Utils.EncryptSHA256.Encrypt(user.USER_PASS);
                string _newPassword = Utils.EncryptSHA256.Encrypt(newPassword);

                AG_IAS_USERS ent = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_ID == user.USER_ID);
                if (ent == null)
                {
                    res.ErrorMsg = "ไม่พบข้อมูลผู้ใช้งาน";
                    LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูลผู้ใช้งาน AG_IAS_USERS->USER_ID :{0}", user.USER_ID));
                    return res;
                }

                if (ent.USER_PASS != _oldPassword)
                {
                    res.ErrorMsg = "รหัสผ่านเดิมไม่ถูกต้อง";
                    return res;
                }

                if (ent.USER_PASS == _newPassword)
                {
                    res.ErrorMsg = "รหัสผ่านใหม่ต้องไม่ซ้ำรหัสผ่านเดิม";
                    return res;
                }

                ent.USER_PASS = _newPassword;
                ent.LASTPASSWORD_CHANGDATE = DateTime.Now;
                ent.UPDATED_BY = user.USER_ID;
                ent.UPDATED_DATE = DateTime.Now;

                int Reset_pass_time = (ent.RESET_TIMES != null) ? Convert.ToInt16(ent.RESET_TIMES) : 0;
                if (Reset_pass_time == 0)
                {
                    ent.RESET_TIMES = 1;
                }
                else
                {
                    ent.RESET_TIMES = Reset_pass_time + 1;
                }

                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการเปลี่ยนรหัสผ่าน";
                LoggerFactory.CreateLog().Fatal(String.Format("AccountService_ChangePassword USER_ID :{0}", user.USER_ID), ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> DisableUser(DTO.AccountDetail user, DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var ent = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_ID == user.ID);
                if (ent == null)
                {
                    res.ErrorMsg = "ไม่พบข้อมูลผู้ใช้";
                    LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูลผู้ใช้งาน AG_IAS_USERS->USER_ID :{0}", user.ID));
                    return res;
                }
                if (ent.IS_ACTIVE == user.ACTIVE)
                {
                    res.ErrorMsg = "ไม่มีการเปลี่ยนแปลงข้อมูล";
                    return res;
                }
                ent.IS_ACTIVE = user.ACTIVE;
                ent.UPDATED_BY = userProfile.Id;
                ent.UPDATED_DATE = DateTime.Now;
                ent.DELETE_USER = user.DELETE_USER;
                ent.OTH_DELETE_USER = user.OTH_DELETE_USER;

                // ยกเลิกบัญชีผู้ใช้ [IS_ACTIVE=D] ให้ส่ง E-mail แจ้ง User
                if (ent.IS_ACTIVE == "D")
                {
                    var person = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(s => s.ID == user.ID);
                    if (person != null)
                    {
                        person.STATUS = "7";

                        #region sent mail
                        if (!String.IsNullOrEmpty(person.EMAIL))
                        {
                            string fromMail = ConfigurationManager.AppSettings["EmailOut"].ToString();
                            string toMail = person.EMAIL;
                            string Subject = "แจ้งยกเลิกการใช้งาน";
                            StringBuilder Body = new StringBuilder();
                            Body.Append("เนื่องด้วยบัญชีใช้งานของคุณ " + person.NAMES + " " + person.LASTNAME + " ได้ถูกยกเลิกการใช้งานใน ");
                            Body.Append(" ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ <br/>");
                            Body.Append(" เหตุผลการยกเลิกการใช้งาน‏ : เนื่องจาก " + user.DELETE_USER + (!string.IsNullOrEmpty(user.OTH_DELETE_USER) ? " - " + user.OTH_DELETE_USER : ""));
                            Body.Append(" <br/><br/>");
                            EmailSender.Sending(Body, toMail, Subject).Sent();
                        }
                        #endregion sent mail
                    }

                    var regis = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(s => s.ID == user.ID);
                    if (regis != null)
                    {
                        regis.STATUS = "7";
                    }
                }

                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการยกเลิกบัญชีผู้ใช้งาน";
                LoggerFactory.CreateLog().Fatal(string.Format("AccountService_DisableUser USER_ID:{0}", user.ID), ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> ChangePasswordByAdmin(DTO.User user, DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_IAS_USERS ent = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_ID == user.USER_ID);
                if (ent == null)
                {
                    res.ErrorMsg = "ไม่พบข้อมูลผู้ใช้งาน";
                    LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูลผู้ใช้งาน AG_IAS_USERS->USER_ID :{0}", user.USER_ID));
                    return res;
                }
                if (String.IsNullOrEmpty(user.USER_PASS))
                {
                    res.ErrorMsg = "กรุณากรอกรหัสผ่าน";
                    return res;
                }

                string _newPassword = Utils.EncryptSHA256.Encrypt(user.USER_PASS);

                ent.USER_PASS = _newPassword;
                ent.LASTPASSWORD_CHANGDATE = DateTime.Now;
                ent.UPDATED_BY = userProfile.Id;
                ent.UPDATED_DATE = DateTime.Now;

                int Reset_pass_time = (ent.RESET_TIMES != null) ? Convert.ToInt16(ent.RESET_TIMES) : 0;
                if (Reset_pass_time == 0)
                {
                    ent.RESET_TIMES = 1;
                }
                else
                {
                    ent.RESET_TIMES = Reset_pass_time + 1;
                }

                #region sent mail
                var person = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(s => s.ID == ent.USER_ID);
                if (!String.IsNullOrEmpty(person.EMAIL))
                {
                    string fromMail = ConfigurationManager.AppSettings["EmailOut"].ToString();
                    string toMail = person.EMAIL;
                    string Subject = "แจ้งเปลี่ยนรหัสผ่าน";
                    StringBuilder Body = new StringBuilder();
                    Body.Append("เนื่องด้วยบัญชีใช้งานของคุณ " + person.NAMES + " " + person.LASTNAME + " ได้ทำการเปลี่ยนรหัสผ่านโดย Admin ใน ");
                    Body.Append(" ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ ");
                    Body.Append("<br/>");
                    Body.Append(" User: " + ent.USER_NAME);
                    Body.Append("<br/>");
                    Body.Append(" Password: " + user.USER_PASS);
                    Body.Append(" <br/><br/>");
                    EmailSender.Sending(Body, toMail, Subject).Sent();
                }
                #endregion sent mail

                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการเปลี่ยนรหัสผ่าน";
                LoggerFactory.CreateLog().Fatal(string.Format("AccountService_ChangePasswordByAdmin USER_ID:{0}", user.USER_ID), ex);
            }
            return res;
        }

        #region applove asso
        public DTO.ResponseService<string> UpdateApploveDoctype(List<DTO.ApploveDocumnetType> listDoc,string by_user)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                foreach (var item in listDoc)
                {
                    var doc = ctx.AG_IAS_APPROVE_DOC_TYPE.FirstOrDefault(x => x.APPROVE_DOC_TYPE == item.APPROVE_DOC_TYPE);
                    doc.UPDATED_BY = by_user;
                    doc.UPDATED_DATE = DateTime.Now;
                    doc.ITEM_VALUE = item.ITEM_VALUE;
                }
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.ApploveDocumnetType>> SelectApploveDocumentType(string p)
        {
            DTO.ResponseService<List<DTO.ApploveDocumnetType>> res = new DTO.ResponseService<List<DTO.ApploveDocumnetType>>();
            res.DataResponse = new List<DTO.ApploveDocumnetType>();
            string sql = "SELECT * FROM AG_IAS_APPROVE_DOC_TYPE";
            var list_doc = ctx.ExecuteStoreQuery<DTO.ApploveDocumnetType>(sql).ToList();
            foreach (var item in list_doc)
            {
                var list_applove = SelectAssoApplove(item.APPROVE_DOC_TYPE).DataResponse;
                foreach (var item2 in list_applove)
                {
                    item.ASSO_APPLOVE += item2.ASSOCIATION_NAME+",";
                }
                if (list_applove.Count > 0)
                {
                    item.ASSO_APPLOVE = item.ASSO_APPLOVE.Remove(item.ASSO_APPLOVE.Length - 1, 1);
                }
                res.DataResponse.Add(item);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.ASSOCIATION>> SelectAsso(string p)
        {
            DTO.ResponseService<List<DTO.ASSOCIATION>> res = new DTO.ResponseService<List<DTO.ASSOCIATION>>();
            string sql = "SELECT ASSOCIATION_CODE,ASSOCIATION_NAME FROM AG_IAS_ASSOCIATION WHERE ACTIVE = 'Y'";
            var list = ctx.ExecuteStoreQuery<DTO.ASSOCIATION>(sql).ToList();
            res.DataResponse = list;
            return res;
        }

        public DTO.ResponseService<List<DTO.ASSOCIATION_APPROVE>> SelectAssoApplove(string p)
        {
            DTO.ResponseService<List<DTO.ASSOCIATION_APPROVE>> res = new DTO.ResponseService<List<DTO.ASSOCIATION_APPROVE>>();
            string sql = "SELECT " +
                "	APP.ASSOCIATION_APPROVE_ID, " +
                "	APP.ASSOCIATION_CODE, " +
                "	APP.APPROVE_DOC_TYPE, " +
                "	ASS.ASSOCIATION_NAME " +
                "FROM " +
                "	AG_IAS_ASSOCIATION_APPROVE APP " +
                "INNER JOIN AG_IAS_ASSOCIATION ASS ON APP.ASSOCIATION_CODE = ASS.ASSOCIATION_CODE WHERE APP.APPROVE_DOC_TYPE='" + p + "' and STATUS = 'Y'";
            res.DataResponse = ctx.ExecuteStoreQuery<DTO.ASSOCIATION_APPROVE>(sql).ToList();
            return res;
        }
        public DTO.ResponseService<string> AddAssocitionApplove(List<DTO.ASSOCIATION_APPROVE> listadd, List<DTO.ASSOCIATION_APPROVE> listdelete, string by_user)
        {
             DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {               
                foreach (var item in listadd)
                {
                    AG_IAS_ASSOCIATION_APPROVE asso = new AG_IAS_ASSOCIATION_APPROVE();
                    asso.APPROVE_DOC_TYPE = item.APPROVE_DOC_TYPE;
                    asso.ASSOCIATION_CODE = item.ASSOCIATION_CODE;
                    asso.CREATE_BY = by_user;
                    asso.CREATE_DATE = DateTime.Now;
                    asso.STATUS = "Y";
                    ctx.AG_IAS_ASSOCIATION_APPROVE.AddObject(asso);
                }

                foreach (var item in listdelete)
                {
                    var del = ctx.AG_IAS_ASSOCIATION_APPROVE.FirstOrDefault(x => x.APPROVE_DOC_TYPE == item.APPROVE_DOC_TYPE && x.ASSOCIATION_CODE == item.ASSOCIATION_CODE && x.STATUS == "Y");
                    if (del != null)
                    {
                        del.UPDATE_BY = by_user;
                        del.UPDATE_DATE = DateTime.Now;
                        del.STATUS = "N";
                    }
                }

                ctx.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        #endregion

        public DTO.ResponseService<DTO.DataItem> GetAssociationNameById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();
            try
            {
                var qry = base.ctx.AG_IAS_ASSOCIATION.Where(s => s.ASSOCIATION_CODE.Equals(Id)).FirstOrDefault();
                if (qry != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = qry.ASSOCIATION_CODE, Name = qry.ASSOCIATION_NAME };
                }                                    
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการเรียกดูข้อมูลสมาคม";
                LoggerFactory.CreateLog().Fatal(string.Format("AccountService_GetAssociationNameById AG_IAS_ASSOCIATION->ASSOCIATION_CODE:{0}", Id), ex);
            }
            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetCompanyNameById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();
            try
            {
                var qry = base.ctx.VW_IAS_COM_CODE.Where(s => s.ID.Equals(Id)).FirstOrDefault();
                if (qry != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = qry.ID, Name = qry.NAME };
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการเรียกดูข้อมูลบริษัท";
                LoggerFactory.CreateLog().Fatal(string.Format("AccountService_GetCompanyNameById VW_IAS_COM_CODE->ID:{0}", Id), ex);
            }
            return res;
        }
    }
}

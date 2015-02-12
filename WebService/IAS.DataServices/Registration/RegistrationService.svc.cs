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
using System.Configuration;

using IAS.DataServices.Registration.Helpers;
using IAS.DataServices.Properties;
using IAS.Common.Logging;
using System.ServiceModel.Activation;
using IAS.DTO.FileService;
using IAS.DataServices.FileManager;

namespace IAS.DataServices.Registration
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class RegistationService : AbstractService, IRegistrationService, IDisposable
    {

        private static readonly String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString();
        private static readonly List<string> AgentTypeDesc = new List<string>
        {
            "ตัวแทน",
            "นายหน้า",
            "ตัวแทนและนายหน้า"
        };

        #region Private Member...

        private string NotApprove = "N";
        private string ImportRegistration = "N";
        private string RegistrationMatch = "N";
        private Dictionary<string, string> dicConfig = null;
        private Dictionary<string, string> enableProfileConfig = null;
        private Dictionary<string, string> extraConfig = null;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public RegistationService()
        {
            dicConfig = new Dictionary<string, string>();
            InitDicConfig();

            enableProfileConfig = new Dictionary<string, string>();
            InitEnableProfileCfg();

            extraConfig = new Dictionary<string, string>();
            InitExtraConfig();
        }
        public RegistationService(IAS.DAL.Interfaces.IIASPersonEntities _ctx)
            :base(_ctx)
        {
            dicConfig = new Dictionary<string, string>();
            InitDicConfig();

            enableProfileConfig = new Dictionary<string, string>();
            InitEnableProfileCfg();

            extraConfig = new Dictionary<string, string>();
            InitExtraConfig();
        }

        #region Method...

        private int MonthEffective
        {
            get
            {
                return this.dicConfig["MonthEffective"].ToInt();
            }
        }

        private void InitDicConfig()
        {
            List<AG_IAS_APPROVE_CONFIG> list = base.ctx.AG_IAS_APPROVE_CONFIG.Where(w => w.ITEM_TYPE == "01").ToList();
            foreach (AG_IAS_APPROVE_CONFIG ap in list)
            {
                dicConfig.Add(ap.KEYWORD, ap.ITEM_VALUE);
            }
        }

        private void InitEnableProfileCfg()
        {
            //var list = (from ls1 in base.ctx.AG_IAS_APPROVE_CONFIG
            //              where ls1.KEYWORD.Contains("General_") && ls1.ITEM_TYPE == "01"
            //              select ls1).Union
            //              (from ls2 in base.ctx.AG_IAS_APPROVE_CONFIG
            //               where ls2.KEYWORD == "Registration_EditName" && ls2.ITEM_TYPE == "01"
            //               select ls2);
            var list = (from cfg in base.ctx.AG_IAS_APPROVE_CONFIG
                        where cfg.KEYWORD.Contains("General_") && cfg.ITEM_TYPE == "01"
                        select cfg);

            foreach (AG_IAS_APPROVE_CONFIG ap in list)
            {
                enableProfileConfig.Add(ap.KEYWORD, ap.ITEM_VALUE);
            }
        }

        private void InitExtraConfig()
        {
            //var list = (from cfg in base.ctx.AG_IAS_APPROVE_FIELD
            //            where cfg.STATUS == 1
            //            select cfg);
            var list = (from cfg in base.ctx.AG_IAS_APPROVE_FIELD
                        select cfg);

            foreach (AG_IAS_APPROVE_FIELD ap in list)
            {
                extraConfig.Add(ap.FIELD_NAME, ap.STATUS.ToString());
            }
        }

        /// <summary>
        /// เพิ่มข้อมูลการลงทะเบียน
        /// </summary>
        /// <param name="entity">ข้อมูล Registration</param>
        /// <param name="registerType">ประเภทการลงทะเบียน</param>
        /// <returns>ResponseService<Registration></returns>
        public DTO.ResponseService<DTO.Registration> Insert(DTO.Registration entity,
                                                            DTO.RegistrationType registerType)
        {

            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();

            if (registerType != DTO.RegistrationType.OIC)
            {
                res.ErrorMsg = Resources.errorRegistrationService_001;
                return res;
            }

            try
            {
                entity.ID_CARD_NO = entity.ID_CARD_NO.Trim();

                AG_IAS_REGISTRATION_T entExist = null;

                //กรณีบริษัทประกัน และสมาคม
                if (registerType == DTO.RegistrationType.Insurance ||
                    registerType == DTO.RegistrationType.Association)
                {
                    entExist = base.ctx.AG_IAS_REGISTRATION_T
                                       .SingleOrDefault(s => ((s.NAMES == entity.NAMES &&
                                                               s.LASTNAME == entity.LASTNAME) ||
                                                              (s.EMAIL == entity.EMAIL)) &&
                                                               s.COMP_CODE == entity.COMP_CODE);
                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (entExist != null)
                    {
                        res.ErrorMsg = string.Format(Resources.infoPersonService_001);
                        return res;
                    }
                }
                //กรณีบุคคลทั่วไปให้ตรวจสอบรหัสบัตรประชาชน
                else if (registerType == DTO.RegistrationType.General)
                {
                    entExist = base.ctx.AG_IAS_REGISTRATION_T
                                       .FirstOrDefault(s => s.ID_CARD_NO == entity.ID_CARD_NO &&
                                                            s.MEMBER_TYPE == registerType.GetEnumValue().ToString());

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (entExist != null)
                    {
                        res.ErrorMsg = string.Format(Resources.errorRegistrationService_002);
                        return res;
                    }

                    var user = base.ctx.AG_IAS_USERS
                                       .FirstOrDefault(s => s.USER_NAME == entity.ID_CARD_NO &&
                                                            s.USER_TYPE == registerType.GetEnumValue().ToString());

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (user != null)
                    {
                        res.ErrorMsg = string.Format(Resources.errorRegistrationService_002);
                        return res;
                    }
                }

                AG_IAS_REGISTRATION_T reg = new AG_IAS_REGISTRATION_T();
                entity.MappingToEntity(reg);

                //Gen New MemberNo
                //reg.MEMBER_NO = this.GetMemberNo(reg.COMP_CODE);

                string regType = registerType.ToString();

                //ตรวจสอบว่าต้องผ่านการอนุมัติจาก คปภ. หรือไม่
                AG_IAS_APPROVE_CONFIG cfg = base.ctx.AG_IAS_APPROVE_CONFIG.SingleOrDefault(w => w.KEYWORD == regType);

                //ถ้าไม่ต้องผ่านการอนุมัติจากเจ้าหน้าที่คปภ.
                if (cfg.ITEM_VALUE == this.NotApprove)
                {
                    //กรณีไม่ต้องอนุมัติจาก คปภ. กำหนดสถานะเป็น 2=อนุมัติ(สมัคร)
                    reg.STATUS = DTO.RegistrationStatus.Approve.GetEnumValue().ToString();

                    //Insert Data to Personal_T
                    InsertToPersonal(reg, registerType);
                }
                else
                {
                    //กำหนดสถานะรอการอนุมัติจากเจ้าหน้าที่ คปภ.
                    reg.STATUS = DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString();
                }

                base.ctx.AG_IAS_REGISTRATION_T.AddObject(reg);

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_Insert", ex);
            }

            return res;
        }


        //ลงทะเบียนหรือยัง true=ลงทะเบียนแล้ว
        public DTO.ResponseService<DTO.Registration> IsGeneralUserRegistered(string idCard)
        {
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();
            try
            {
                string statusNotApprove = DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString();

                AG_IAS_REGISTRATION_T entExist = base.ctx.AG_IAS_REGISTRATION_T
                                      .SingleOrDefault(s => s.ID_CARD_NO == idCard && s.MEMBER_TYPE == "1");
                if (entExist != null)
                {
                    //ตรวจสอบกรณีไม่อนุมัติ ข้อมูลเดิมจะมีผลหรือไม่
                    if (entExist.STATUS == statusNotApprove && entExist.NOT_APPROVE_DATE != null)
                    {
                        DateTime notApproveDate = entExist.NOT_APPROVE_DATE.Value;
                        int monthEffective = this.MonthEffective;
                        if (notApproveDate.AddMonths(monthEffective).Date <= DateTime.Now.Date)
                        {
                            DTO.Registration reg = new DTO.Registration();
                            entExist.MappingToEntity(reg);
                        }
                    }
                    else
                    {
                        DTO.Registration reg = new DTO.Registration();
                        entExist.MappingToEntity(reg);
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_IsGeneralUserRegistered", ex);
            }
            return res;
        }


        public DTO.ResponseService<DTO.Registration> IsCompAssoUserRegistered(string email, string name, string lastName, string compCode)
        {
            string statusNotApprove = DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString();
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();
            try
            {
                string status = DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString();
                AG_IAS_REGISTRATION_T entExist = base.ctx.AG_IAS_REGISTRATION_T
                              .SingleOrDefault(s => ((s.NAMES == name &&
                                                    s.LASTNAME == lastName) ||
                                                    (s.EMAIL == email)) &&
                                                    s.COMP_CODE == compCode);
                if (entExist != null)
                {
                    //ตรวจสอบกรณีไม่อนุมัติ ข้อมูลเดิมจะมีผลหรือไม่
                    if (entExist.STATUS == statusNotApprove && entExist.NOT_APPROVE_DATE != null)
                    {
                        DateTime notApproveDate = entExist.NOT_APPROVE_DATE.Value;
                        int monthEffective = this.MonthEffective;
                        if (notApproveDate.AddMonths(monthEffective).Date <= DateTime.Now.Date)
                        {
                            DTO.Registration reg = new DTO.Registration();
                            entExist.MappingToEntity(reg);
                        }
                    }
                    else
                    {
                        DTO.Registration reg = new DTO.Registration();
                        entExist.MappingToEntity(reg);
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_IsCompAssoUserRegistered", ex);
            }
            return res;
        }

        /// <summary>
        /// ตรวจสอบเลขบัตรประชาชน
        /// </summary>
        /// <param name="idCard">เลขบัตรประชาชน</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> VerifyIdCard(string idCard)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                res.ResultMessage = Utils.IdCard.Verify(idCard);

                //ตรวจสอบเลขบัตรประชาชน
                if (!res.ResultMessage)
                {
                    res.ErrorMsg = string.Format("บัตรประชาชนเลขที่ {0} ไม่ถูกต้อง", idCard);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_VerifyIdCard", ex);
            }
            return res;
        }

        /// <summary>
        /// ตรวจสอบข้อมูลก่อน Submit
        /// </summary>
        /// <param name="registerType">ประเภทการลงทะเบียน</param>
        /// <param name="entity">ข้อมูลการลงทะเบียน</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> ValidateBeforeSubmit(DTO.RegistrationType registerType,
                                                             DTO.Registration entity)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            try
            {
                #region Validate

                DTO.ResponseService<DTO.Registration> inRes = new DTO.ResponseService<DTO.Registration>();

                entity.ID_CARD_NO = entity.ID_CARD_NO.Trim();
                entity.EMAIL = entity.EMAIL.Trim();

                //ตรวจสอบเลขบัตรประชาชน
                if (!Utils.IdCard.Verify(entity.ID_CARD_NO))
                {
                    res.ErrorMsg = string.Format("บัตรประชาชนเลขที่ {0} ไม่ถูกต้อง", entity.ID_CARD_NO);
                    return res;
                }

                #region ของเก่า
                //ตรวจสอบ Email
                //if (entity.EMAIL != string.Empty)
                //{
                //    if (!Utils.Email.IsRightEmailFormat(entity.EMAIL))
                //    {
                //        res.ErrorMsg = string.Format("Email : {0} ไม่ถูกต้อง", entity.EMAIL);
                //        return res;
                //    }
                //    else
                //    {
                //        bool isDupplicate;
                //        using (var newCtx = new IASPersonEntities())
                //        {
                //            isDupplicate = newCtx.AG_IAS_USERS.SingleOrDefault(s => s.USER_NAME == entity.EMAIL) != null;
                //        }

                //        if (isDupplicate)
                //        {
                //            res.ErrorMsg = string.Format("Email : {0} ซ้ำไม่สามารถลงทะเบียนได้", entity.EMAIL);
                //            return res;
                //        }
                //    }
                //}
                #endregion

                //Check Email Dupplicate Status 1,2,4,5 
                if (entity.EMAIL != string.Empty)
                {
                    if (!Utils.Email.IsRightEmailFormat(entity.EMAIL))
                    {
                        res.ErrorMsg = string.Format("Email : {0} ไม่ถูกต้อง", entity.EMAIL);
                        return res;
                    }
                    else
                    {
                        //bool isDupplicate;
                        var lsRegis = base.ctx.AG_IAS_REGISTRATION_T.Where(w => w.EMAIL == entity.EMAIL && (w.STATUS == "1"
                                                                                                        || w.STATUS == "2"));
                        if (lsRegis != null)
                        {
                            if (lsRegis.ToList().Count > 0)
                            {
                                var lsPersonal = base.ctx.AG_IAS_PERSONAL_T.Where(w => w.EMAIL == entity.EMAIL && (w.MEMBER_TYPE == "1"
                                                                                                           || w.MEMBER_TYPE == "2"
                                                                                                           || w.MEMBER_TYPE == "4"
                                                                                                           || w.MEMBER_TYPE == "5"));
                                if (lsPersonal != null)
                                {
                                    if (lsPersonal.ToList().Count > 0)
                                    {
                                        res.ErrorMsg = string.Format("Email {0} ซ้ำไม่สามารถลงทะเบียนได้", entity.EMAIL);
                                        return res;
                                    }
                                }
                                res.ErrorMsg = string.Format("Email {0} ซ้ำไม่สามารถลงทะเบียนได้", entity.EMAIL);
                                return res;

                            }

                        }

                    }
                }

                //Validate กรณีบุคคลทั่วไป
                if (registerType == DTO.RegistrationType.General)
                {
                    inRes = this.IsGeneralUserRegistered(entity.ID_CARD_NO);

                    if (inRes.DataResponse != null)
                    {
                        res.ErrorMsg = string.Format("เลขบัตรประชาชน {0} ลงทะเบียนแล้ว", entity.ID_CARD_NO);
                        return res;
                    }
                }
                //กรณีบริษัทประกัน กับ สมาคม
                else if (registerType == DTO.RegistrationType.Insurance ||
                         registerType == DTO.RegistrationType.Association)
                {
                    inRes = this.IsCompAssoUserRegistered(entity.EMAIL, entity.NAMES, entity.LASTNAME, entity.COMP_CODE);

                    if (inRes.DataResponse != null)
                    {
                        res.ErrorMsg = string.Format("Email : {0} ลงทะเบียนแล้ว", entity.EMAIL);
                        return res;
                    }

                    string compCode = entity.COMP_CODE.Trim();
                    string compName = entity.Company_Name.Trim();

                    if (registerType == DTO.RegistrationType.Insurance)
                    {
                        VW_IAS_COM_CODE comp = base.ctx.VW_IAS_COM_CODE
                                                       .SingleOrDefault(s => s.ID == entity.COMP_CODE &&
                                                                             s.NAME == entity.Company_Name);
                        if (comp == null)
                        {
                            res.ErrorMsg = string.Format("ไม่พบ {0} {1} : {2} โปรดระบุให้ถูกต้อง", "บริษัทประกัน ", compCode, compName);
                        }
                    }
                    else
                    {
                        DTO.InsuranceAssociate asso = new DTO.InsuranceAssociate();
                        var assoList = GetInsuranceAssociateList();
                        var assoEnt = assoList.Single(s => s.Id == compCode && s.Name == compName);
                        if (assoEnt == null)
                        {
                            res.ErrorMsg = string.Format("ไม่พบ {0} {1} : {2} โปรดระบุให้ถูกต้อง", "สมาคม ", compCode, compName);
                        }
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_ValidateBeforeSubmit", ex);
            }
            return res;
        }

        ///Added by Nattapong 23/8/2556
        //Gen Insurance Associate List
        private List<DTO.InsuranceAssociate> GetInsuranceAssociateList()
        {
            var Comlist = (from vw in base.ctx.AG_EXAM_PLACE_GROUP_R
                           select new DTO.InsuranceAssociate
                           {
                               Id = vw.EXAM_PLACE_GROUP_CODE,
                               Name = vw.EXAM_PLACE_GROUP_NAME
                           }).ToList();

            return Comlist;
        }


        /// <summary>
        /// เพิ่มข้อมูลการลงทะเบียนและข้อมูลเอกสารแนบ
        /// </summary>
        /// <param name="registerType">ประเภทการลงทะเบียน</param>
        /// <param name="entity">ข้อมูลการลงทะเบียน</param>
        /// <param name="listAttatchFile">รายการเอกสารแนบ</param>
        /// <returns>ResponseService<Registration></returns>
        public DTO.ResponseService<DTO.Registration> InsertWithAttatchFile(DTO.RegistrationType registerType, DTO.Registration entity, List<DTO.RegistrationAttatchFile> listAttatchFile)
        {

            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();
            string memberType = string.Empty;

            try
            {
                entity.ID_CARD_NO = entity.ID_CARD_NO.Trim();
                entity.EMAIL = entity.EMAIL.Trim();
                memberType = Enum.GetName(typeof(DTO.RegistrationType), entity.MEMBER_TYPE.ToInt());

                AG_IAS_REGISTRATION_T entExist = null;

                #region Data Duplicate Validation
                //กรณีบริษัทประกัน และสมาคม
                if (registerType == DTO.RegistrationType.Insurance || registerType == DTO.RegistrationType.Association || registerType == DTO.RegistrationType.TestCenter)
                {
                    entExist = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(s => ((s.NAMES == entity.NAMES &&
                                                               s.LASTNAME == entity.LASTNAME) ||
                                                              (s.EMAIL == entity.EMAIL)) &&
                                                               s.COMP_CODE == entity.COMP_CODE && !s.STATUS.Equals("7"));
                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (entExist != null)
                    {
                        res.ErrorMsg = string.Format(Resources.infoPersonService_001);
                        return res;
                    }

                    string userType = registerType.GetEnumValue().ToString();

                    var personal = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(p => p.ID_CARD_NO == entity.ID_CARD_NO && p.MEMBER_TYPE == entity.MEMBER_TYPE && !p.STATUS.Equals("7"));
                    if (personal != null)
                    {
                        String member = base.ctx.AG_IAS_MEMBER_TYPE.FirstOrDefault(m => m.MEMBER_CODE == personal.MEMBER_TYPE).MEMBER_NAME;
                        res.ErrorMsg = String.Format("เลขบัตรประชาชนนี้ ได้ถูกใช้ในการลงทะเบียนแล้ว ในสถานะ {0}.", member);
                        return res;
                    }

                    var user = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_NAME == entity.EMAIL && !s.IS_ACTIVE.Equals("C") && !s.IS_ACTIVE.Equals("D"));

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (user != null)
                    {
                        res.ErrorMsg = string.Format(Resources.errorRegistrationService_003);
                        return res;
                    }

                    // การลงทะเบียน (เจ้าหน้าที่กลุ่มสนามสอบ) จะต้องเช็คข้อมูลว่ามีการสมัครสอบในหน่วยงานจัดสอบนี้หรือไม่
                    if (registerType == DTO.RegistrationType.TestCenter)
                    {
                        var appli = (   from A in base.ctx.AG_APPLICANT_T
                                        join B in base.ctx.AG_EXAM_LICENSE_R on A.TESTING_NO equals B.TESTING_NO
                                        join C in base.ctx.AG_EXAM_PLACE_R on A.EXAM_PLACE_CODE equals C.EXAM_PLACE_CODE
                                        join D in base.ctx.AG_EXAM_PLACE_GROUP_R on C.EXAM_PLACE_GROUP_CODE equals D.EXAM_PLACE_GROUP_CODE
                                        where A.ID_CARD_NO == entity.ID_CARD_NO 
                                            && D.EXAM_PLACE_GROUP_CODE == entity.COMP_CODE
                                            && B.TESTING_DATE >= DateTime.Now && (string.IsNullOrEmpty(A.RECORD_STATUS) || A.RECORD_STATUS != "X")
                                        select A
                                    ).FirstOrDefault();
                        if (appli != null)
                        {
                            res.ErrorMsg = "ไม่สามารถลงทะเบียนได้ เนื่องจากมีการสมัครสอบในหน่วยงานจัดสอบนี้";
                            return res;
                        }
                    }
                }
                //กรณีบุคคลทั่วไปให้ตรวจสอบรหัสบัตรประชาชน
                else if (registerType == DTO.RegistrationType.General)
                {
                    string memType = registerType.GetEnumValue().ToString();

                    entExist = base.ctx.AG_IAS_REGISTRATION_T.Where(s => s.ID_CARD_NO == entity.ID_CARD_NO &&
                                                            s.MEMBER_TYPE == memType && !s.STATUS.Equals("7")).FirstOrDefault();
                    //.FirstOrDefault(s => s.ID_CARD_NO == entity.ID_CARD_NO &&
                    //                     s.MEMBER_TYPE == memType);

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (entExist != null)
                    {
                        res.ErrorMsg = string.Format(Resources.errorRegistrationService_002);
                        return res;
                    }

                    var user = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_NAME == entity.ID_CARD_NO && !s.IS_ACTIVE.Equals("C") && !s.IS_ACTIVE.Equals("D"));

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (user != null)
                    {
                        res.ErrorMsg = string.Format(Resources.errorRegistrationService_002);
                        return res;
                    }

                    var personal = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(p => p.ID_CARD_NO == entity.ID_CARD_NO && p.MEMBER_TYPE == entity.MEMBER_TYPE && !p.STATUS.Equals("7"));
                    if (personal != null)
                    {
                        String member = base.ctx.AG_IAS_MEMBER_TYPE.FirstOrDefault(m => m.MEMBER_CODE == personal.MEMBER_TYPE).MEMBER_NAME;
                        res.ErrorMsg = String.Format("เลขบัตรประชาชนนี้ ได้ถูกใช้ในการลงทะเบียนแล้ว ในสถานะ {0}.", member);
                    }
                }
                #endregion

                AG_IAS_REGISTRATION_T reg = new AG_IAS_REGISTRATION_T();
                entity.MappingToEntity(reg);
                
                #region Assing AttachFiles for Move to Upload Directory
                IList<AG_IAS_ATTACH_FILE> attachFiles = new List<AG_IAS_ATTACH_FILE>(); // ประกาศตัวแปรเก็บ attach สำหรับ Move
                //ข้อมูลไฟล์ที่ Upload
                listAttatchFile.ForEach(file =>
                {
                    AG_IAS_ATTACH_FILE targetFile = new AG_IAS_ATTACH_FILE();
                    file.MappingToEntity(targetFile);
                    targetFile.CREATED_DATE = targetFile.UPDATED_DATE = DateTime.Now;
                    base.ctx.AG_IAS_ATTACH_FILE.AddObject(targetFile);

                    attachFiles.Add(targetFile);
                });
                #endregion
                
                //ถ้า memberType 
                //ถ้าเป็น User ที่ดูแลสนามสอบ
                //ตรวจสอบว่าต้องผ่านการอนุมัติจาก คปภ. หรือไม่ 
                //หรือ ถ้าเป็น User ประเภทดูแลสนามสอบ ไม่ต้องผ่านการอนุมัติ
                if (registerType == DTO.RegistrationType.TestCenter)
                {
                    //กรณีไม่ต้องอนุมัติจาก คปภ. กำหนดสถานะเป็น 2=อนุมัติ(สมัคร)
                    reg.STATUS = DTO.RegistrationStatus.Approve.GetEnumValue().ToString();

                    //Insert Data to Personal_T
                    InsertToPersonal(reg, registerType);

                }
                //กรณีประเภทสมาชิกเป็น บริษัท, สมาคม จะไม่มีกรณีการนำเข้าข้อมูล ระบบการอนุมัติตามปกติ
                else if (registerType == DTO.RegistrationType.Insurance || registerType == DTO.RegistrationType.Association)
                {
                    if (this.dicConfig[memberType] == this.NotApprove)
                    {
                        //กรณีไม่ต้องอนุมัติจาก คปภ. กำหนดสถานะเป็น 2=อนุมัติ(สมัคร)
                        reg.STATUS = DTO.RegistrationStatus.Approve.GetEnumValue().ToString();

                        //Insert Data to Personal_T
                        InsertToPersonal(reg, registerType);

                        //Move From Temp Folder to AttachFile Folder 
                        if (attachFiles.Count > 0)
                        {
                            MoveFileAfterApprove(reg, attachFiles);
                        }
                    }
                    else
                    {
                        //กำหนดสถานะรอการอนุมัติจากเจ้าหน้าที่ คปภ.
                        reg.STATUS = DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString();
                        AG_IAS_USERS user = new AG_IAS_USERS();
                        user.USER_ID = reg.ID;
                        user.USER_PASS = reg.REG_PASS;
                        user.USER_TYPE = reg.MEMBER_TYPE;
                        user.IS_ACTIVE = DTO.UserStatus.Active.ToString().Substring(0, 1);
                        user.USER_RIGHT = reg.MEMBER_TYPE;
                        user.IS_APPROVE = "N";
                        user.MEMBER_TYPE = reg.MEMBER_TYPE;
                        user.LASTPASSWORD_CHANGDATE = DateTime.Now;
                        if (registerType == DTO.RegistrationType.Insurance)
                        {
                            // insert user account แบบคนเดียว การลงทะเบียนครั้งแรก 
                            user.USER_NAME = reg.EMAIL;
                        }
                        else if (registerType == DTO.RegistrationType.Association)
                        {
                            // insert user account แบบคนเดียว การลงทะเบียนครั้งแรก 
                            user.USER_NAME = reg.EMAIL;
                        }
                        base.ctx.AG_IAS_USERS.AddObject(user);

                    }
                }
                //กรณีประเภทสมาชิกเป็น บุคคลทั่วไป จะมีกรณีการนำเข้าข้อมูล ระบบการอนุมัติตามการตั้งค่าระบบ ตาม TOR ใหม่
                else if (registerType == DTO.RegistrationType.General)
                {
                    //Step 1 : ตรวจสอบสถานะการนำเข้าของข้อมูลจากระบบเก่า IMPORT_STATUS = "Y"
                    //ตรวจสอบสถานะการนำเข้าของข้อมูลจากระบบเก่า
                    DTO.ResponseMessage<bool> resMatchCompare = this.RegistrationCompare(reg);
                    if (resMatchCompare.ResultMessage == true && this.RegistrationMatch.Equals("Y"))
                    {
                        reg.STATUS = DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString();
                        if (this.ImportRegistration.Equals("Y"))
                        {
                            reg.IMPORT_STATUS = this.ImportRegistration;
                        }

                        AG_IAS_USERS user = new AG_IAS_USERS();
                        user.USER_ID = reg.ID;
                        user.USER_PASS = reg.REG_PASS;
                        user.USER_TYPE = reg.MEMBER_TYPE;
                        user.IS_ACTIVE = DTO.UserStatus.Active.ToString().Substring(0, 1);
                        user.USER_RIGHT = reg.MEMBER_TYPE;
                        user.IS_APPROVE = "N";
                        user.MEMBER_TYPE = reg.MEMBER_TYPE;
                        user.LASTPASSWORD_CHANGDATE = DateTime.Now;
                        if (registerType == DTO.RegistrationType.General)
                        {
                            // insert user account แบบคนเดียว การลงทะเบียนครั้งแรก 
                            user.USER_NAME = reg.ID_CARD_NO;
                        }
                        base.ctx.AG_IAS_USERS.AddObject(user);
                    }
                    else
                    {
                        //ตรวจสอบ Config
                        //เป็นผู้สมัครใหม่ และไม่มีการอ้างอิงข้อมูลเดิม
                        //เป็นผู้สมัครใหม่ ไม่มีการอ้างอิงข้อมูลเดิม แต่มีความสอดคล้องของข้อมูล
                        //มีการอ้างอิงข้อมูลเดิม และไม่มีการแก้ไขข้อมูล
                        //มีการอ้างอิงข้อมูลเดิม และมีการแก้ไขข้อมูล
                        if (this.enableProfileConfig.Where(cfg => cfg.Value == "Y").Count() > 0)
                        {
                            DTO.ResponseMessage<bool> resCompare2 = this.RegistrationValidationByCfg(reg);
                            //ตรงตามเงื่อไข AG_IAS_APPROVE_CONFIG > ส่งอนุมัติ สถานะ : รออนุมัติ
                            if (resCompare2.ResultMessage == true)
                            {
                                //ส่งApprove
                                reg.STATUS = DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString();
                                if (this.ImportRegistration.Equals("Y"))
                                {
                                    reg.IMPORT_STATUS = this.ImportRegistration;
                                }

                                AG_IAS_USERS user = new AG_IAS_USERS();
                                user.USER_ID = reg.ID;
                                user.USER_PASS = reg.REG_PASS;
                                user.USER_TYPE = reg.MEMBER_TYPE;
                                user.IS_ACTIVE = DTO.UserStatus.Active.ToString().Substring(0, 1);
                                user.USER_RIGHT = reg.MEMBER_TYPE;
                                user.IS_APPROVE = "N";
                                user.MEMBER_TYPE = reg.MEMBER_TYPE;
                                user.LASTPASSWORD_CHANGDATE = DateTime.Now;
                                if (registerType == DTO.RegistrationType.General)
                                {
                                    // insert user account แบบคนเดียว การลงทะเบียนครั้งแรก 
                                    user.USER_NAME = reg.ID_CARD_NO;
                                }
                                base.ctx.AG_IAS_USERS.AddObject(user);
                            }
                            //ไม่ตรงตามเงื่อไข AG_IAS_APPROVE_CONFIG > อนุมัติ สถานะ : Auto
                            else
                            {
                                //กรณีไม่ต้องอนุมัติจาก คปภ. กำหนดสถานะเป็น 2=อนุมัติ(สมัคร)
                                reg.STATUS = DTO.RegistrationStatus.Approve.GetEnumValue().ToString();

                                //Insert Data to Personal_T
                                InsertToPersonal(reg, registerType);

                                //Move From Temp Folder to AttachFile Folder 
                                if (attachFiles.Count > 0)
                                {
                                    MoveFileAfterApprove(reg, attachFiles);
                                }
                            }

                        }
                        //ผ่านการอนุมัติ
                        else
                        {
                            //กรณีไม่ต้องอนุมัติจาก คปภ. กำหนดสถานะเป็น 2=อนุมัติ(สมัคร)
                            reg.STATUS = DTO.RegistrationStatus.Approve.GetEnumValue().ToString();

                            //Insert Data to Personal_T
                            InsertToPersonal(reg, registerType);

                            //Move From Temp Folder to AttachFile Folder 
                            if (attachFiles.Count > 0)
                            {
                                MoveFileAfterApprove(reg, attachFiles);
                            }
                        }

                    }
                    
                }
                //วันที่สร้างและ Update ล่าสุดของ REGISTRATION_T
                reg.CREATED_DATE = reg.UPDATED_DATE = DateTime.Now;

                base.ctx.AG_IAS_REGISTRATION_T.AddObject(reg);
                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                if (!MailConfirmHelper.SendMailConfirmRegistration(reg))
                {

                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_InsertWithAttatchFile" + ":" + ex.Message, ex.Message);
            }

            return res;
        }

        /// <summary>
        /// ปรับปรุงข้อมูล
        /// </summary>
        /// <param name="entity">ข้อมูลการลงทะเบียน</param>
        /// <returns>ResponseService<Registration></returns>
        public DTO.ResponseService<DTO.Registration> Update(DTO.Registration entity)
        {
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();
            try
            {
                AG_IAS_REGISTRATION_T reg = base.ctx.AG_IAS_REGISTRATION_T
                                               .SingleOrDefault(s => s.ID == entity.ID);

                entity.MappingToEntity(reg);

                reg.UPDATED_DATE = DateTime.Now;

                base.ctx.SaveChanges();

                reg.MappingToEntity(entity);
                res.DataResponse = entity;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_Update", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.Registration> UpdateWithAttachFiles(DTO.Registration entity, List<DTO.RegistrationAttatchFile> listAttatchFile)
        {
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();
            try
            {
                AG_IAS_REGISTRATION_T reg = base.ctx.AG_IAS_REGISTRATION_T
                                               .SingleOrDefault(s => s.ID == entity.ID);

                entity.MappingToEntity(reg);

                reg.UPDATED_DATE = DateTime.Now;

                foreach (DTO.RegistrationAttatchFile attachFile in listAttatchFile.Where(a => a.FILE_STATUS != "A"))
                {

                    foreach (DTO.RegistrationAttatchFile item in listAttatchFile)
                    {
                        if (item.FILE_STATUS != "A" && item.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE
                            && item.FILE_STATUS == attachFile.FILE_STATUS)
                        {
                            AG_IAS_ATTACH_FILE attach = base.ctx.AG_IAS_ATTACH_FILE.SingleOrDefault(a => a.REGISTRATION_ID == attachFile.REGISTRATION_ID && a.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE && a.FILE_STATUS == "A");
                            if (item.FILE_STATUS == "E")
                            {
                                item.MappingToEntity(attach);
                                attach.FILE_STATUS = "A";
                            }
                            else if (item.FILE_STATUS == "D")
                            {
                                base.ctx.DeleteObject(attach);
                            }
                            else if (item.FILE_STATUS == "W")
                            {
                                if (attach != null)
                                {
                                    //item.MappingToEntity(attach);
                                    AttachFileMapper(item, attach);
                                    attach.FILE_STATUS = "A";
                                }
                                else
                                {
                                    attach = new AG_IAS_ATTACH_FILE();
                                    item.MappingToEntity(attach);
                                    attach.FILE_STATUS = "A";
                                    base.ctx.AG_IAS_ATTACH_FILE.AddObject(attach);
                                }

                            }

                        }
                    }
                }

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }
            }


            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_UpdateWithAttachFiles" + ":" + ex.Message, ex.Message);
            }

            return res;
        }
        private void AttachFileMapper(DTO.RegistrationAttatchFile sourceValue, AG_IAS_ATTACH_FILE targetFile)
        {

            //targetFile.ID = sourceValue.ID;
            targetFile.REGISTRATION_ID = sourceValue.REGISTRATION_ID;
            targetFile.ATTACH_FILE_TYPE = sourceValue.ATTACH_FILE_TYPE;
            targetFile.ATTACH_FILE_PATH = sourceValue.ATTACH_FILE_PATH;
            targetFile.REMARK = sourceValue.REMARK;
            targetFile.CREATED_BY = sourceValue.CREATED_BY;
            targetFile.CREATED_DATE = sourceValue.CREATED_DATE;
            targetFile.UPDATED_BY = sourceValue.UPDATED_BY;
            targetFile.UPDATED_DATE = sourceValue.UPDATED_DATE;
            targetFile.FILE_STATUS = sourceValue.FILE_STATUS;


        }

        /// <summary>
        /// ลบรายการ
        /// </summary>
        /// <param name="Id">รหัสรายการ</param>
        /// <returns>ResponseService<Registration></returns>
        public DTO.ResponseService<DTO.Registration> Delete(string Id)
        {
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();
            try
            {
                var ent = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.ID == Id);
                base.ctx.AG_IAS_REGISTRATION_T.DeleteObject(ent);
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_Delete", ex);
            }

            return res;
        }

        /// <summary>
        /// ดึงข้อมูลด้วยรหัส
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>ResponseService<Registration></returns>
        public DTO.ResponseService<DTO.Registration> GetById(string Id)
        {
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();

            try
            {
                var reg = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.ID == Id);
                if (reg != null)
                {
                    res.DataResponse = new DTO.Registration();
                    reg.MappingToEntity(res.DataResponse);
                }


                //var per = base.ctx.AG_IAS_PERSONAL_T.SingleOrDefault(s => s.ID == Id);
                //res.DataResponse = new DTO.Registration();
                //if (per != null)
                //{
                //    per.MappingToEntity(res.DataResponse);
                //}
                //else
                //{
                //    var reg = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.ID == Id);
                //    reg.MappingToEntity(res.DataResponse);
                //}
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetById", ex);
            }

            return res;
        }

        /// <summary>
        /// ปรับปรุงข้อมูลเอกสารแนบ
        /// </summary>
        /// <param name="entity">รายการเอกสารแนบ</param>
        /// <returns>ResponseService<RegistrationAttatchFile></returns>
        public DTO.ResponseService<DTO.RegistrationAttatchFile> UpdateAttachFile(DTO.RegistrationAttatchFile entity)
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> res = new DTO.ResponseService<DTO.RegistrationAttatchFile>();
            try
            {
                AG_IAS_ATTACH_FILE reg = base.ctx.AG_IAS_ATTACH_FILE
                                               .SingleOrDefault(s => s.ID == entity.ID);
                entity.MappingToEntity(reg);
                reg.UPDATED_DATE = DateTime.Now;

                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_UpdateAttachFile", ex);
            }

            return res;
        }

        /// <summary>
        /// ลบเอกสารแนบ
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>ResponseService<RegistrationAttatchFile></returns>
        public DTO.ResponseService<DTO.RegistrationAttatchFile> DeleteAttatchFile(string Id)
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> res = new DTO.ResponseService<DTO.RegistrationAttatchFile>();
            try
            {
                var ent = base.ctx.AG_IAS_ATTACH_FILE.SingleOrDefault(s => s.ID == Id);
                base.ctx.AG_IAS_ATTACH_FILE.DeleteObject(ent);
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_DeleteAttatchFile", ex);
            }

            return res;
        }

        /// <summary>
        /// ดึงเอกสารแนบด้วย Id
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>ResponseService<RegistrationAttatchFile></returns>
        public DTO.ResponseService<DTO.RegistrationAttatchFile> GetAttatchFileById(string Id)
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> res = new DTO.ResponseService<DTO.RegistrationAttatchFile>();

            try
            {
                var reg = base.ctx.AG_IAS_ATTACH_FILE.SingleOrDefault(s => s.ID == Id);
                res.DataResponse = new DTO.RegistrationAttatchFile();
                reg.MappingToEntity(res.DataResponse);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetAttatchFileById", ex);
            }

            return res;
        }

        /// <summary>
        /// ดึงเอกสารแนบด้วย RegisterationId
        /// </summary>
        /// <param name="Id">Id</param>
        /// <returns>ResponseService<RegistrationAttatchFile></returns>
        public DTO.ResponseService<IList<DTO.RegistrationAttatchFile>> GetAttatchFilesByRegisterationNo(string registerationNo)
        {
            DTO.ResponseService<IList<DTO.RegistrationAttatchFile>> res = new DTO.ResponseService<IList<DTO.RegistrationAttatchFile>>();

            try
            {
                IList<AG_IAS_ATTACH_FILE> regs = base.ctx.AG_IAS_ATTACH_FILE.Where(a => a.REGISTRATION_ID == registerationNo).ToList();
                IList<AG_IAS_DOCUMENT_TYPE> docs = base.ctx.AG_IAS_DOCUMENT_TYPE.ToList();
                IEnumerable<DTO.RegistrationAttatchFile> result = from r in regs
                                                                  join d in docs on r.ATTACH_FILE_TYPE equals d.DOCUMENT_CODE into rd
                                                                  from d in rd.DefaultIfEmpty()
                                                                  select new DTO.RegistrationAttatchFile
                                                                  {
                                                                      ID = r.ID,
                                                                      REGISTRATION_ID = r.REGISTRATION_ID,
                                                                      ATTACH_FILE_TYPE = r.ATTACH_FILE_TYPE,
                                                                      ATTACH_FILE_PATH = r.ATTACH_FILE_PATH,
                                                                      REMARK = r.REMARK,
                                                                      CREATED_BY = r.CREATED_BY,
                                                                      CREATED_DATE = r.CREATED_DATE,
                                                                      UPDATED_BY = r.UPDATED_BY,
                                                                      UPDATED_DATE = r.UPDATED_DATE,
                                                                      FILE_STATUS = r.FILE_STATUS,
                                                                      DocumentTypeName = d.DOCUMENT_NAME,
                                                                      FileName = r.ATTACH_FILE_PATH.Split('\\')[r.ATTACH_FILE_PATH.Split('\\').Length - 1]

                                                                  };

                res.DataResponse = result.ToList();
                //foreach (AG_IAS_ATTACH_FILE reg in regs) { 
                //    DTO.RegistrationAttatchFile registeration = new DTO.RegistrationAttatchFile();
                //    reg.MappingToEntity(registeration);
                //    res.DataResponse.Add(registeration);
                //}

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetAttatchFilesByRegisterationNo", ex);
            }

            return res;
        }

        /// <summary>
        /// ค้นหาข้อมูลการลงทะเบียนโดยระบุเงื่อนไข
        /// </summary>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <param name="IdCard">เลขบัตรประชาชน</param>
        /// <param name="memberTypeCode"></param>
        /// <param name="email">E-mail</param>
        /// <param name="compCode">เลขที่บริษัท/สมาคม</param>
        /// <param name="oicCode">เลขที่พนักงาน คปภ.</param>
        /// <param name="status">สถานะ</param>
        /// <returns>ResponseService<DataSet></returns>
        //public DTO.ResponseService<DataSet> GetRegistrationsByCriteria(string firstName, string lastName,
        //                                                               DateTime? starDate, DateTime? toDate,
        //                                                               string IdCard, string memberTypeCode,
        //                                                               string email, string compCode,
        //                                                               string status, int pageNo, int recordPerPage, string para)
        //<LASTUPDATE>25/04/2557</LASTUPDATE>
        //<AUTHOR>Natta</AUTHOR>
        public DTO.ResponseService<DataSet> GetRegistrationsByCriteria(DTO.GetReistrationByCriteriaRequest request)
        {

            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {

                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };
                string critRecNo = string.Empty;
                critRecNo = request.PageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         request.PageNo.StartRowNumber(request.RecordPerPage).ToString() + " AND " +
                                         request.PageNo.ToRowNumber(request.RecordPerPage).ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("A.NAMES LIKE '%{0}%' AND ", request.FirstName));
                sb.Append(GetCriteria("A.LASTNAME LIKE '%{0}%' AND ", request.LastName));
                sb.Append(GetCriteria("A.ID_CARD_NO LIKE '{0}%' AND ", request.IdCard));
                sb.Append(GetCriteria("A.MEMBER_TYPE = '{0}' AND ", request.MemberTypeCode));
                sb.Append(GetCriteria("A.EMAIL = '{0}' AND ", request.Email));
                sb.Append(GetCriteria("A.COMP_CODE = '{0}' AND ", request.CompCode));
                //sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                if (request.Status != "0")
                    sb.Append(GetCriteria("STATUS = '{0}' AND ", request.Status));

                if (request.StartDate != null && request.ToDate != null)
                {
                    sb.Append("TO_CHAR(A.CREATED_DATE) BETWEEN TO_DATE('" + Convert.ToDateTime(request.StartDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                    Convert.ToDateTime(request.ToDate).ToString_yyyyMMdd() + "','yyyymmdd') AND");

                    //dd/MM/yyyy

                }


                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;
                if (request.Para == "1")
                {

                    string sql = "SELECT Count(*)rowcount from(SELECT A.ID, A.EMAIL,  C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, " +
                      "       B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, " +
                      "       S.STATUS_NAME, A.STATUS  " +
                      "FROM " + (request.Status == "1" || request.Status == "2" || request.Status == "3" ? " AG_IAS_REGISTRATION_T " : " AG_IAS_PERSONAL_T ") + " A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S " +
                      "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND " +
                      "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) AND A.STATUS NOT IN('7') " +
                      crit + ")A ";

                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);

                    res.DataResponse = ds;
                }
                else if (request.Para == "2")
                {
                    //string sql = "SELECT * FROM(SELECT A.ID, A.EMAIL,A.CREATED_DATE, C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, " +
                    //             "       B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, " +
                    //             "       S.STATUS_NAME, A.STATUS,(SELECT NAMES FROM AG_IAS_PERSONAL_T WHERE ID=A.APPROVED_BY) APPOVED_NAME," +
                    //             "  ROW_NUMBER() OVER (ORDER BY A.ID) RUN_NO " +
                    //             "FROM " + (request.Status == "1" ? " AG_IAS_REGISTRATION_T " : " AG_IAS_PERSONAL_T ") + " A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S " +
                    //             "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND " +
                    //             "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) AND A.STATUS NOT IN('7') " +
                    //             crit + ")A " + critRecNo;

                    string sql = "SELECT * FROM(SELECT A.ID, A.EMAIL,A.CREATED_DATE, C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, " +
                                 "       B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, " +
                                 "       S.STATUS_NAME, A.STATUS,(SELECT NAMES FROM AG_IAS_PERSONAL_T WHERE ID=A.APPROVED_BY) APPOVED_NAME," +
                                 "  ROW_NUMBER() OVER (ORDER BY A.ID) RUN_NO " +
                                 "FROM " + (request.Status == "1" || request.Status == "2" || request.Status == "3" ? " AG_IAS_REGISTRATION_T " : " AG_IAS_PERSONAL_T ") + " A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S " +
                                 "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND " +
                                 "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) AND A.STATUS NOT IN('7') " +
                                 crit + ")A " + critRecNo;

                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);

                    res.DataResponse = ds;
                }
                else if (request.Para == "3")
                {
                    string sql = "select count(*) rowcount from ( "
                               + "SELECT A.ID IDS, A.EMAIL,  CC.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, "
                               + "BB.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, "
                               + "SS.STATUS_NAME, A.STATUS "
                               + "FROM AG_IAS_PERSONAL_T A, AG_IAS_MEMBER_TYPE BB, GB_PREFIX_R CC, AG_IAS_STATUS SS "
                               + "WHERE A.MEMBER_TYPE = BB.MEMBER_CODE AND A.PRE_NAME_CODE = CC.PRE_CODE AND "
                               + "SS.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) AND A.STATUS NOT IN('7') " + crit
                               + "UNION ALL "
                               + "SELECT A.ID IDS, A.EMAIL,  C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, "
                               + "B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, "
                               + "S.STATUS_NAME, A.STATUS "
                               + "FROM AG_IAS_REGISTRATION_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S "
                               + "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND "
                               + "S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5')  ) AND A.STATUS NOT IN('7') " + crit
                               + "and id not in (Select Id  From Ag_Ias_Personal_T A,Ag_Ias_Member_Type B,Ag_Ias_Status C "
                               + "Where A.Member_Type = B.Member_Code And C.Status_Code = A.Status "
                               + "And ( Not A.Member_Type In('4','5') ) AND A.STATUS NOT IN('7') "
                               + crit + "))A ";


                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);

                    res.DataResponse = ds;
                }
                else if (request.Para == "4")
                {
                    string sql = "select * from ( "
                             + "select ID, EMAIL,CREATED_DATE,   PREFIX, NAMES, LASTNAME, "
                             + "MEMBER_TYPE,ID_CARD_NO, TELEPHONE, ZIP_CODE, "
                             + "STATUS_NAME, STATUS,APPOVED_NAME ,ROW_NUMBER() OVER (ORDER BY ID) RUN_NO from ( "

                             + "SELECT A.ID ID, A.EMAIL,A.CREATED_DATE,  CC.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, "
                             + "BB.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, "
                             + "SS.STATUS_NAME, A.STATUS,(SELECT NAMES FROM AG_IAS_PERSONAL_T WHERE ID=A.APPROVED_BY) APPOVED_NAME "
                             + "FROM AG_IAS_PERSONAL_T A, AG_IAS_MEMBER_TYPE BB, GB_PREFIX_R CC, AG_IAS_STATUS SS "
                             + "WHERE A.MEMBER_TYPE = BB.MEMBER_CODE AND A.PRE_NAME_CODE = CC.PRE_CODE AND "
                             + "SS.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) AND A.STATUS NOT IN('7') " + crit
                             + "UNION ALL "
                             + "SELECT A.ID ID, A.EMAIL,A.CREATED_DATE,  C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, "
                             + "B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, "
                             + "S.STATUS_NAME, A.STATUS ,(SELECT NAMES FROM AG_IAS_PERSONAL_T WHERE ID=A.APPROVED_BY) APPOVED_NAME "
                             + "FROM AG_IAS_REGISTRATION_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S "
                             + "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND "
                             + "S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5')  ) AND A.STATUS NOT IN('7') " + crit
                             + "and id not in (Select A.Id  From Ag_Ias_Personal_T A,Ag_Ias_Member_Type B,Ag_Ias_Status C "
                             + "Where A.Member_Type = B.Member_Code And C.Status_Code = A.Status "
                             + "And ( Not A.Member_Type In('4','5') ) AND A.STATUS NOT IN('7') "
                             + crit + ")))A " + critRecNo;


                    OracleDB db = new OracleDB();
                    DataSet ds = db.GetDataSet(sql);
                    res.DataResponse = ds;

                    
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetRegistrationsByCriteria", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.PagingResponse<DataSet>> GetRegistrationsByCriteriaAtPage(string firstName, string lastName,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, Int32 pageIndex, Int32 pageSize)
        {

            DTO.ResponseService<DTO.PagingResponse<DataSet>> res = new DTO.ResponseService<DTO.PagingResponse<DataSet>>();
            DTO.PagingResponse<DataSet> pagingResponse = new DTO.PagingResponse<DataSet>();
            try
            {

                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("NAMES LIKE '%{0}%' AND ", firstName));
                sb.Append(GetCriteria("LASTNAME LIKE '%{0}%' AND ", lastName));
                sb.Append(GetCriteria("ID_CARD_NO = '{0}%' AND ", IdCard));
                sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                sb.Append(GetCriteria("EMAIL = '{0}' AND ", email));
                sb.Append(GetCriteria("COMP_CODE = '{0}' AND ", compCode));
                //sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                sb.Append(GetCriteria("STATUS = '{0}' AND ", status));

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;
                string sql = "SELECT A.ID, A.EMAIL, '' AS OIC, C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, " +
                            "       B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, " +
                            "       S.STATUS_NAME, A.STATUS " +
                            "FROM AG_IAS_REGISTRATION_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S " +
                            "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND " +
                            "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) " +
                            crit;

                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql, pageIndex, pageSize);
                pagingResponse.DataResponse = ds;

                pagingResponse.PagingInfo = new DTO.PagingInfo() { CurrentPage = pageIndex, ItemsPerPage = pageSize, TotalItems = ds.Tables[0].Rows.Count };

                res.DataResponse = pagingResponse;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetRegistrationsByCriteriaAtPage", ex);
            }

            return res;


        }
        /// <summary>
        /// ดึงข้อมูลการลงทะเบียนด้วยรหัสบัตรประชาชน
        /// </summary>
        /// <param name="idCard">เลขบัตรประชาชน</param>
        /// <returns>ResponseService<Registration></returns>
        /// <Author>Natta</Author>
        /// <LASTUPDATE>28/04/2557</LASTUPDATE>
        public DTO.ResponseService<DTO.Registration> GetByIdCard(string idCard)
        {
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();

            try
            {
                //var reg = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.ID_CARD_NO == idCard);
                var reg = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(s => s.ID_CARD_NO == idCard && s.STATUS != "7");
                res.DataResponse = new DTO.Registration();
                reg.MappingToEntity(res.DataResponse);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetByIdCard", ex);
            }

            return res;
        }

        /// <summary>
        /// ดึงข้อมูลด้วยชื่อและนามสกุล
        /// </summary>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <returns>ResponseService<Registration></returns>
        public DTO.ResponseService<DTO.Registration> GetByFirstLastName(DTO.GetByFirstLastNameRequest request)
        {
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();

            try
            {
                var reg = base.ctx.AG_IAS_REGISTRATION_T
                             .FirstOrDefault(s => s.NAMES == request.FirstName &&
                                                  s.LASTNAME == request.LastName);
                res.DataResponse = new DTO.Registration();
                reg.MappingToEntity(res.DataResponse);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetByFirstLastName", ex);
            }

            return res;
        }


        private void InsertToPersonal(AG_IAS_REGISTRATION_T reg, DTO.RegistrationType registerType)
        {
            AG_IAS_PERSONAL_T per = new AG_IAS_PERSONAL_T();

            try
            {
                reg.MappingToEntity(per);

                //สถานะของ PERSONAL_T เป็นค่าว่าง
                per.STATUS = string.Empty;

                //วันที่สร้างและ Update ล่าสุดของ PERSONAL_T
                per.CREATED_DATE = per.UPDATED_DATE = DateTime.Now;

                base.ctx.AG_IAS_PERSONAL_T.AddObject(per);

                //string encryptPassword = Utils.EncryptSHA256.Encrypt(reg.REG_PASSWORD == null ? string.Empty : reg.REG_PASSWORD);
                //Update to AG_PERSONAL_T > if (!= null){ Update Entity }
                AG_PERSONAL_T oldPer = base.ctx.AG_PERSONAL_T.FirstOrDefault(idc => idc.ID_CARD_NO == per.ID_CARD_NO);
                if (oldPer != null)
                {
                    oldPer.ADDRESS1 = per.ADDRESS_1;
                    oldPer.ADDRESS2 = per.ADDRESS_2;
                    oldPer.ZIPCODE = per.ZIP_CODE;
                    oldPer.USER_ID = per.UPDATED_BY;
                    oldPer.USER_DATE = per.UPDATED_DATE;
                    oldPer.E_MAIL = per.EMAIL;

                    per.MappingToEntity(oldPer);

                }

                //Insert ข้อมูลเข้า AG_IAS_USERS
                var user = new AG_IAS_USERS
                {
                    IS_ACTIVE = DTO.UserStatus.Active.ToString().Substring(0, 1),
                    USER_ID = reg.ID,
                    USER_NAME = (registerType == DTO.RegistrationType.General ? reg.ID_CARD_NO : reg.EMAIL),
                    USER_TYPE = reg.MEMBER_TYPE,
                    USER_PASS = reg.REG_PASS,
                    USER_RIGHT = registerType.GetEnumValue().ToString(),
                    IS_APPROVE = "Y",
                    LASTPASSWORD_CHANGDATE = DateTime.Now,
                    MEMBER_TYPE = reg.MEMBER_TYPE
                };
                base.ctx.AG_IAS_USERS.AddObject(user);
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("Registration_Service" + ":" + ex.Message, ex.Message);
            }

        }

        /// <summary>
        /// อนุมัติการลงทะเบียน
        /// Last edit by Nattapong @23-12-56
        /// </summary>
        /// <param name="listId">Collection Id รายการที่อนุมัติ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> RegistrationApprove(DTO.RegistrationApproveRequest request)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            try
            {
                Func<string, string> NullToString = delegate(string input)
                {
                    if ((input == null) || (input == ""))
                    {
                        input = "";
                    }
                    return input;

                };

                List<AG_IAS_REGISTRATION_T> registers = new List<AG_IAS_REGISTRATION_T>();
                foreach (String Id in request.ListId)
                {

                    AG_IAS_REGISTRATION_T reg = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(s => s.ID == Id && s.MEMBER_TYPE == request.MemberType);

                    if (reg != null)
                    {
                        reg.STATUS = DTO.RegistrationStatus.Approve.GetEnumValue().ToString();
                        reg.APPROVED_BY = request.UserId;
                        reg.APPROVE_RESULT = request.AppreResult;
                        reg.UPDATED_DATE = DateTime.Now;

                        //<MapEntity>AG_IAS_PERSONAL_T</MapEntity>
                        AG_IAS_PERSONAL_T oldpert = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(pert => pert.ID_CARD_NO == reg.ID_CARD_NO && pert.MEMBER_TYPE == reg.MEMBER_TYPE);
                        if (oldpert != null)
                        {
                            //<UPDATE>AG_IAS_PERSONAL_T</UPDATE>
                            //oldpert.ID = Id;
                            oldpert.MEMBER_TYPE = reg.MEMBER_TYPE;
                            oldpert.ID_CARD_NO = reg.ID_CARD_NO;
                            oldpert.EMPLOYEE_NO = reg.EMPLOYEE_NO;
                            oldpert.PRE_NAME_CODE = reg.PRE_NAME_CODE;
                            oldpert.NAMES = reg.NAMES;
                            oldpert.LASTNAME = reg.LASTNAME;
                            oldpert.NATIONALITY = reg.NATIONALITY;
                            oldpert.BIRTH_DATE = reg.BIRTH_DATE;
                            oldpert.SEX = reg.SEX;
                            oldpert.EDUCATION_CODE = reg.EDUCATION_CODE;
                            oldpert.ADDRESS_1 = reg.ADDRESS_1;
                            oldpert.ADDRESS_2 = reg.ADDRESS_2;
                            oldpert.AREA_CODE = reg.AREA_CODE;
                            oldpert.PROVINCE_CODE = reg.PROVINCE_CODE;
                            oldpert.ZIP_CODE = reg.ZIP_CODE;
                            oldpert.TELEPHONE = reg.TELEPHONE;
                            oldpert.LOCAL_ADDRESS1 = reg.LOCAL_ADDRESS1;
                            oldpert.LOCAL_ADDRESS2 = reg.LOCAL_ADDRESS2;
                            oldpert.LOCAL_AREA_CODE = reg.LOCAL_AREA_CODE;
                            oldpert.LOCAL_PROVINCE_CODE = reg.LOCAL_PROVINCE_CODE;
                            oldpert.LOCAL_ZIPCODE = reg.LOCAL_ZIPCODE;
                            oldpert.LOCAL_TELEPHONE = reg.LOCAL_TELEPHONE;
                            oldpert.EMAIL = reg.EMAIL;
                            oldpert.STATUS = reg.STATUS;
                            oldpert.TUMBON_CODE = reg.TUMBON_CODE;
                            oldpert.LOCAL_TUMBON_CODE = reg.LOCAL_TUMBON_CODE;
                            oldpert.COMP_CODE = reg.COMP_CODE;
                            oldpert.CREATED_BY = reg.CREATED_BY;
                            oldpert.CREATED_DATE = reg.CREATED_DATE;
                            oldpert.UPDATED_BY = reg.UPDATED_BY;
                            oldpert.UPDATED_DATE = reg.UPDATED_DATE;
                            oldpert.APPROVE_RESULT = request.AppreResult;
                            oldpert.APPROVED_BY = request.UserId;
                            oldpert.AGENT_TYPE = reg.AGENT_TYPE;
                        }
                        else
                        {
                            AG_IAS_PERSONAL_T per = new AG_IAS_PERSONAL_T();
                            per.ID = Id;
                            per.MEMBER_TYPE = reg.MEMBER_TYPE;
                            per.ID_CARD_NO = reg.ID_CARD_NO;
                            per.EMPLOYEE_NO = reg.EMPLOYEE_NO;
                            per.PRE_NAME_CODE = reg.PRE_NAME_CODE;
                            per.NAMES = reg.NAMES;
                            per.LASTNAME = reg.LASTNAME;
                            per.NATIONALITY = reg.NATIONALITY;
                            per.BIRTH_DATE = reg.BIRTH_DATE;
                            per.SEX = reg.SEX;
                            per.EDUCATION_CODE = reg.EDUCATION_CODE;
                            per.ADDRESS_1 = reg.ADDRESS_1;
                            per.ADDRESS_2 = reg.ADDRESS_2;
                            per.AREA_CODE = reg.AREA_CODE;
                            per.PROVINCE_CODE = reg.PROVINCE_CODE;
                            per.ZIP_CODE = reg.ZIP_CODE;
                            per.TELEPHONE = reg.TELEPHONE;
                            per.LOCAL_ADDRESS1 = reg.LOCAL_ADDRESS1;
                            per.LOCAL_ADDRESS2 = reg.LOCAL_ADDRESS2;
                            per.LOCAL_AREA_CODE = reg.LOCAL_AREA_CODE;
                            per.LOCAL_PROVINCE_CODE = reg.LOCAL_PROVINCE_CODE;
                            per.LOCAL_ZIPCODE = reg.LOCAL_ZIPCODE;
                            per.LOCAL_TELEPHONE = reg.LOCAL_TELEPHONE;
                            per.EMAIL = reg.EMAIL;
                            per.STATUS = reg.STATUS;
                            per.TUMBON_CODE = reg.TUMBON_CODE;
                            per.LOCAL_TUMBON_CODE = reg.LOCAL_TUMBON_CODE;
                            per.COMP_CODE = reg.COMP_CODE;
                            per.CREATED_BY = reg.CREATED_BY;
                            per.CREATED_DATE = reg.CREATED_DATE;
                            per.UPDATED_BY = reg.UPDATED_BY;
                            per.UPDATED_DATE = reg.UPDATED_DATE;
                            per.APPROVE_RESULT = request.AppreResult;
                            per.APPROVED_BY = request.UserId;
                            per.AGENT_TYPE = reg.AGENT_TYPE;
                            base.ctx.AG_IAS_PERSONAL_T.AddObject(per);
                        }

                        //<MapEntity>AG_PERSONAL_T</MapEntity>
                        AG_PERSONAL_T oldper = base.ctx.AG_PERSONAL_T.FirstOrDefault(pers => pers.ID_CARD_NO == reg.ID_CARD_NO);
                        if (oldper != null)
                        {
                            //<UPDATE>Update AG_PERSONAL_T</UPDATE>
                            StringBuilder areacode = new StringBuilder();
                            areacode.Append(NullToString(reg.PROVINCE_CODE));
                            areacode.Append(NullToString(reg.AREA_CODE));
                            areacode.Append(NullToString(reg.TUMBON_CODE));

                            //oldper.ID_CARD_NO = reg.ID_CARD_NO;
                            oldper.PRE_NAME_CODE = reg.PRE_NAME_CODE;
                            oldper.NAMES = reg.NAMES;
                            oldper.LASTNAME = reg.LASTNAME;
                            oldper.NATIONALITY = reg.NATIONALITY;
                            oldper.BIRTH_DATE = reg.BIRTH_DATE;
                            oldper.SEX = reg.SEX;
                            oldper.EDUCATION_CODE = reg.EDUCATION_CODE;
                            oldper.ADDRESS1 = reg.ADDRESS_1;
                            oldper.ADDRESS2 = reg.ADDRESS_2;
                            oldper.AREA_CODE = reg.AREA_CODE;
                            oldper.PROVINCE_CODE = reg.PROVINCE_CODE;
                            oldper.ZIPCODE = reg.ZIP_CODE;
                            oldper.TELEPHONE = reg.TELEPHONE;
                            oldper.LOCAL_ADDRESS1 = reg.LOCAL_ADDRESS1;
                            oldper.LOCAL_ADDRESS2 = reg.LOCAL_ADDRESS2;
                            oldper.LOCAL_AREA_CODE = reg.LOCAL_AREA_CODE;
                            oldper.LOCAL_PROVINCE_CODE = reg.LOCAL_PROVINCE_CODE;
                            oldper.LOCAL_ZIPCODE = reg.LOCAL_ZIPCODE;
                            oldper.LOCAL_TELEPHONE = reg.LOCAL_TELEPHONE;
                            oldper.E_MAIL = reg.EMAIL;
                            //oldper.STATUS = reg.STATUS;
                            oldper.AREA_CODE = areacode.ToString();
                            //oldper.LOCAL_TUMBON_CODE = reg.LOCAL_TUMBON_CODE;
                            //oldper.COMP_CODE = reg.COMP_CODE;
                            //oldper.CREATED_BY = reg.CREATED_BY;
                            //oldper.CREATED_DATE = reg.CREATED_DATE;
                            //oldper.UPDATED_BY = reg.UPDATED_BY;
                            oldper.USER_DATE = reg.UPDATED_DATE;
                            //oldper.APPROVE_RESULT = request.AppreResult;
                            //oldper.APPROVED_BY = request.UserId;
                            //oldper.AGENT_TYPE = reg.AGENT_TYPE;
                        }
                    }

                    if (reg != null)
                    {
                        List<AG_IAS_ATTACH_FILE> attachFiles = base.ctx.AG_IAS_ATTACH_FILE.Where(a => a.REGISTRATION_ID == reg.ID).ToList();
                        if (attachFiles.Count > 0)
                        {
                            MoveFileAfterApprove(reg, attachFiles);
                        }
                        //AG_IAS_USERS user = base.ctx.AG_IAS_USERS.Where(u => u.USER_ID == reg.ID).Single();
                        AG_IAS_USERS user = base.ctx.AG_IAS_USERS.FirstOrDefault(u => u.USER_ID == reg.ID && u.USER_TYPE == request.MemberType);
                        if (user != null)
                        {
                            user.IS_APPROVE = "Y";
                            user.APPROVED_BY = request.UserId;
                            registers.Add(reg);
                        }
                    }

                }
                base.ctx.SaveChanges();

                foreach (var item in registers)
                {
                    AG_IAS_USERS user = base.ctx.AG_IAS_USERS.SingleOrDefault(a => a.USER_ID == item.ID);

                    if (user != null)
                    {
                        MailApproveRegisterHelper.SendMailApproveRegister(item, user.USER_NAME);
                    }

                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_RegistrationApprove", ex);
            }

            return res;
        }


        private void MoveFileAfterApprove(AG_IAS_REGISTRATION_T registration, IList<AG_IAS_ATTACH_FILE> attachFiles)
        {
            String pathFile = String.Format(@"{0}\{1}_{2}", AttachFileContainer, registration.MEMBER_TYPE, registration.ID_CARD_NO);

            foreach (AG_IAS_ATTACH_FILE attachFile in attachFiles)
            {
                String newFileName = String.Format("{0}_{1}.{2}",
                                                    registration.ID_CARD_NO,
                                                    Convert.ToInt32(attachFile.ATTACH_FILE_TYPE).ToString("00"),
                                                    GetExtensionFile(attachFile.ATTACH_FILE_PATH));

                MoveFileResponse response = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                {
                    CurrentContainer = "",
                    CurrentFileName = attachFile.ATTACH_FILE_PATH,
                    TargetContainer = pathFile,
                    TargetFileName = newFileName
                }).Action();
                if (response.Code != "0000")
                {
                    throw new ApplicationException(response.Message);
                }

                attachFile.ATTACH_FILE_PATH = response.TargetFullName;

            }
        }



        /// <summary>
        /// ไม่อนุมัติการลงทะเบียน
        /// </summary>
        /// <param name="listId">Collection Id รายการที่ไม่อนุมัติ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> RegistrationNotApprove(DTO.RegistrationNotApproveRequest request)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            AG_IAS_REGISTRATION_T reg = new AG_IAS_REGISTRATION_T();
            try
            {
                foreach (String Id in request.ListId)
                {

                    //string[] splID = Id.Split('|');
                    //string[] splID = listId[i].ToString().Split('|');
                    //string Id = splID[0];

                    reg = base.ctx.AG_IAS_REGISTRATION_T
                                 .SingleOrDefault(s => s.ID == Id);
                    reg.STATUS = DTO.RegistrationStatus.NotApprove.GetEnumValue().ToString();
                    reg.NOT_APPROVE_DATE = DateTime.Now;
                    reg.LINK_REDIRECT = Utils.EncryptSHA256.Encrypt(reg.ID);
                    reg.APPROVE_RESULT = request.AppreResult;
                    reg.APPROVED_BY = request.UserId;
                    reg.UPDATED_DATE = DateTime.Now;
                }
                base.ctx.SaveChanges();

                //foreach (var item in reg)
                //{
                AG_IAS_USERS user = base.ctx.AG_IAS_USERS.SingleOrDefault(a => a.USER_ID == request.UserId);

                if (user != null)
                {
                    MailApproveRegisterHelper.SendMailApproveRegister(reg, user.USER_NAME);
                }

                //}
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_RegistrationNotApprove", ex);
            }

            return res;
        }

        public void Dispose()
        {
            if (base.ctx != null) base.ctx.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion

        public DTO.ResponseService<DTO.RegistrationAttatchFile> InsertAttachFileToRegistration(string registrationId, DTO.RegistrationAttatchFile attachFile)
        {
            DTO.ResponseService<DTO.RegistrationAttatchFile> response = new DTO.ResponseService<DTO.RegistrationAttatchFile>();
            try
            {
                var reg = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.ID == registrationId);
                if (reg.ID == attachFile.REGISTRATION_ID)
                {

                    AG_IAS_ATTACH_FILE targetFile = new AG_IAS_ATTACH_FILE();
                    attachFile.MappingToEntity(targetFile);
                    targetFile.CREATED_DATE = targetFile.UPDATED_DATE = DateTime.Now;
                    base.ctx.AG_IAS_ATTACH_FILE.AddObject(targetFile);
                    base.ctx.SaveChanges();
                    response.DataResponse = attachFile;
                }
            }
            catch (Exception ex)
            {

                response.ErrorMsg = ex.Message;
                LoggerFactory.CreateLog().Fatal("RegistrationService_InsertAttachFileToRegistration", ex);
            }

            return response;

        }

        private String GetExtensionFile(String fileName)
        {
            String[] files = fileName.Split('.');
            return files[files.Length - 1];
        }


        #region New Regis Func
        //Last update 16-05-57
        //Sequence more than one element
        //Do not Edit *If u need to Edit please contact me @Nattapong
        public DTO.ResponseMessage<bool> EntityValidation(DTO.RegistrationType registerType, DTO.Registration entity)
        {

            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                #region Func
                Func<string, string> getMemtype = delegate(string input)
                {
                    if ((input != null) && (input != ""))
                    {
                        AG_IAS_MEMBER_TYPE ent = base.ctx.AG_IAS_MEMBER_TYPE.FirstOrDefault(type => type.MEMBER_CODE == input);
                        if (ent != null)
                        {
                            input = ent.MEMBER_NAME;
                        }
                        else if (ent == null)
                        {
                            input = Resources.errorRegistrationService_004;
                        }

                    }
                    return input;
                };
                #endregion

                entity.ID_CARD_NO = entity.ID_CARD_NO.Trim();
                entity.EMAIL = entity.EMAIL.Trim();

                AG_IAS_REGISTRATION_T entExist = null;

                //กรณีบริษัทประกัน และสมาคม
                if (registerType == DTO.RegistrationType.Insurance || registerType == DTO.RegistrationType.Association)
                {
                    string memType = registerType.GetEnumValue().ToString();
                    //entExist = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(s => (s.ID_CARD_NO == entity.ID_CARD_NO && s.EMAIL == entity.EMAIL && s.MEMBER_TYPE == "2")
                    //    || (s.ID_CARD_NO == entity.ID_CARD_NO && s.EMAIL == entity.EMAIL && s.MEMBER_TYPE == "3"));

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    IQueryable<AG_IAS_REGISTRATION_T> chk1 = (from a in base.ctx.AG_IAS_REGISTRATION_T
                              where a.ID_CARD_NO.Trim() == entity.ID_CARD_NO.Trim() &&
                              a.MEMBER_TYPE.Equals("2") && !a.STATUS.Equals("7")
                              select a);
                    IQueryable<AG_IAS_REGISTRATION_T> chk2 = (from a in base.ctx.AG_IAS_REGISTRATION_T
                                where a.ID_CARD_NO.Trim() == entity.ID_CARD_NO.Trim() &&
                                a.MEMBER_TYPE.Equals("3") && !a.STATUS.Equals("7")
                                select a);

                    if (chk1.Union(chk2).ToList().Count > 0)
                    {
                        var d = chk1.Union(chk2).ToList();
                        res.ErrorMsg = string.Format("เลขบัตรประชาชนนี้ ได้ถูกใช้ในการลงทะเบียนแล้ว ในประเภท {0}.", getMemtype(d[0].MEMBER_TYPE));
                        return res;
                    }

                    //entExist = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(s => (s.ID_CARD_NO == entity.ID_CARD_NO && s.MEMBER_TYPE == "2") || (s.ID_CARD_NO == entity.ID_CARD_NO && s.MEMBER_TYPE == "3"));
                    ////กรณีมีการลงทะเบียนในระบบแล้ว
                    //if (entExist != null)
                    //{
                    //    res.ErrorMsg = string.Format("เลขบัตรประชาชนนี้ ได้ถูกใช้ในการลงทะเบียนแล้ว ในประเภท {0}.", getMemtype(entExist.MEMBER_TYPE));
                    //    return res;
                    //}

                    var personal = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(p => ((p.EMAIL == entity.EMAIL && p.MEMBER_TYPE == "2") || (p.EMAIL == entity.EMAIL && p.MEMBER_TYPE == "3")) && !p.STATUS.Equals("7"));
                    if (personal != null)
                    {
                        //String member = base.ctx.AG_IAS_MEMBER_TYPE.FirstOrDefault(m => m.MEMBER_CODE == personal.MEMBER_TYPE).MEMBER_NAME;
                        res.ErrorMsg = String.Format("E-mail ได้ถูกใช้ในการลงทะเบียนแล้ว ในประเภท {0}.", getMemtype(personal.MEMBER_TYPE));
                        return res;
                    }

                    var user = base.ctx.AG_IAS_USERS.FirstOrDefault(s => ((s.USER_NAME == entity.EMAIL && s.USER_TYPE == "2") || (s.USER_NAME == entity.EMAIL && s.USER_TYPE == "3")) && !s.IS_ACTIVE.Equals("C") && !s.IS_ACTIVE.Equals("D"));

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (user != null)
                    {
                        res.ErrorMsg = string.Format("E-mail ได้ถูกใช้ในการลงทะเบียนแล้ว ในประเภท {0}.", getMemtype(user.MEMBER_TYPE));
                        return res;
                    }

                    else
                    {
                        res.ResultMessage = true;
                    }
                }
                //กรณีบุคคลทั่วไปให้ตรวจสอบรหัสบัตรประชาชน
                else if (registerType == DTO.RegistrationType.General)
                {
                    string memType = registerType.GetEnumValue().ToString();

                    entExist = base.ctx.AG_IAS_REGISTRATION_T.Where(s => s.ID_CARD_NO == entity.ID_CARD_NO && s.MEMBER_TYPE == memType && !s.STATUS.Equals("7")).FirstOrDefault();
                    //.FirstOrDefault(s => s.ID_CARD_NO == entity.ID_CARD_NO &&
                    //                     s.MEMBER_TYPE == memType);

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (entExist != null)
                    {
                        res.ErrorMsg = string.Format("เลขบัตรประชาชนนี้ได้ลงทะเบียนในระบบแล้ว ในประเภท {0}.", getMemtype(entExist.MEMBER_TYPE));
                        return res;
                    }

                    var user = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_NAME == entity.ID_CARD_NO && !s.IS_ACTIVE.Equals("C") && !s.IS_ACTIVE.Equals("D"));

                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (user != null)
                    {
                        res.ErrorMsg = string.Format("เลขบัตรประชาชนนี้ได้ลงทะเบียนในระบบแล้ว ในประเภท {0}.", getMemtype(user.MEMBER_TYPE));
                        return res;
                    }

                    var entExistmail = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.EMAIL == entity.EMAIL && s.MEMBER_TYPE == entity.MEMBER_TYPE && !s.STATUS.Equals("7"));
                    //กรณีมีการลงทะเบียนในระบบแล้ว
                    if (entExistmail != null)
                    {
                        res.ErrorMsg = string.Format("E-Mail นี้ได้ลงทะเบียนในระบบแล้ว ในประเภท{0}.", getMemtype(entExistmail.MEMBER_TYPE));
                        return res;
                    }

                    //var mail = base.ctx.AG_IAS_USERS.FirstOrDefault(s => s.USER_NAME == entity.EMAIL);
                    //var mail = base.ctx.AG_IAS_USERS.FirstOrDefault(s => (s.USER_NAME == entity.EMAIL && s.USER_TYPE == "2") || (s.USER_NAME == entity.EMAIL && s.USER_TYPE == "3"));
                    ////กรณีมีการลงทะเบียนในระบบแล้ว
                    //if (mail != null)
                    //{
                    //    res.ErrorMsg = string.Format("E-mail ได้ถูกใช้ในการลงทะเบียนแล้ว ในประเภท {0}.", getMemtype(mail.MEMBER_TYPE));
                    //    return res;
                    //}

                    var personal = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(p => p.ID_CARD_NO == entity.ID_CARD_NO && p.MEMBER_TYPE == entity.MEMBER_TYPE && !p.STATUS.Equals("7"));
                    if (personal != null)
                    {
                        String member = base.ctx.AG_IAS_MEMBER_TYPE.FirstOrDefault(m => m.MEMBER_CODE == personal.MEMBER_TYPE).MEMBER_NAME;
                        res.ErrorMsg = String.Format("เลขบัตรประชาชนนี้ ได้ถูกใช้ในการลงทะเบียนแล้ว ในสถานะ {0}.", member);
                    }

                    else
                    {
                        res.ResultMessage = true;
                    }
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_EntityValidation", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.AgentTypeEntity>> GetAgentType(string agentType)
        {

            var res = new DTO.ResponseService<List<DTO.AgentTypeEntity>>();
            try
            {
                //GET 2rows > Type A= ตัวแทน && B= นายหน้า && Z= ตัวแทนและนายหน้า
                List<DTO.AgentTypeEntity> result = new List<DTO.AgentTypeEntity>();
                for (int i = 0; i < AgentTypeDesc.Count; i++)
			    {
                    string agType = AgentTypeDesc[i];
                    var resx = (from agt in base.ctx.AG_AGENT_TYPE_R
                                where agt.AGENT_TYPE_DESC == agType
                                 group agt by new
                                 {
                                     AGENT_TYPE = agt.AGENT_TYPE,
                                     AGENT_TYPE_DESC = agt.AGENT_TYPE_DESC,
                                     AGENT_TYPE_REMARK = agt.AGENT_TYPE_REMARK

                                 } into newtb
                                  select new DTO.AgentTypeEntity
                                 {
                                     AGENT_TYPE = newtb.Key.AGENT_TYPE,
                                     AGENT_TYPE_DESC = newtb.Key.AGENT_TYPE_DESC,
                                     AGENT_TYPE_REMARK = newtb.Key.AGENT_TYPE_REMARK

                                 }).FirstOrDefault();

                    if (resx != null)
                    {
                        result.Add(resx);
                    }
			 
			    }

                if (result.Count > 0)
                {

                    res.DataResponse = result;
                }
                else
                {
                    res.ErrorMsg = Resources.errorRegistrationService_005;
                }
                             
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorRegistrationService_005;
                LoggerFactory.CreateLog().Fatal("RegistrationService_GetAgentType", ex);
            }
            return res;
        }

        public DTO.ResponseService<DTO.Person> GetPersonalDetailByIDCard(string idCard)
        {
            string nullData = Resources.errorRegistrationService_nullData;
            var res = new DTO.ResponseService<DTO.Person>();
            try
            {
                #region Func
                Func<string, string> getMemtype = delegate(string input)
                {
                    if ((input != null) && (input != ""))
                    {
                        AG_IAS_MEMBER_TYPE ent = base.ctx.AG_IAS_MEMBER_TYPE.FirstOrDefault(type => type.MEMBER_CODE == input);
                        if (ent != null)
                        {
                            input = ent.MEMBER_NAME;
                        }
                        else if (ent == null)
                        {
                            input = Resources.errorRegistrationService_004;
                        }

                    }
                    return input;
                };
                #endregion

                //1 Find from AG_IAS_PERSONAL_T
                AG_IAS_PERSONAL_T agias = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(idc => (idc.ID_CARD_NO == idCard && idc.STATUS != null) || (idc.ID_CARD_NO == idCard && idc.STATUS != "7"));

                if (agias != null)
                {
                    res.DataResponse = new DTO.Person
                    {
                        ID_CARD_NO = agias.ID_CARD_NO,
                        PRE_NAME_CODE = agias.PRE_NAME_CODE,
                        NAMES = agias.NAMES,
                        LASTNAME = agias.LASTNAME,
                        NATIONALITY = agias.NATIONALITY,
                        BIRTH_DATE = agias.BIRTH_DATE,
                        SEX = agias.SEX,
                        EDUCATION_CODE = agias.EDUCATION_CODE,
                        ADDRESS_1 = agias.ADDRESS_1,
                        ADDRESS_2 = agias.ADDRESS_2,
                        AREA_CODE = agias.AREA_CODE,
                        PROVINCE_CODE = agias.PROVINCE_CODE,
                        TUMBON_CODE = agias.TUMBON_CODE,
                        ZIP_CODE = agias.ZIP_CODE,
                        TELEPHONE = agias.TELEPHONE,
                        LOCAL_ADDRESS1 = agias.LOCAL_ADDRESS1,
                        LOCAL_ADDRESS2 = agias.LOCAL_ADDRESS2,
                        LOCAL_AREA_CODE = agias.LOCAL_AREA_CODE,
                        LOCAL_PROVINCE_CODE = agias.LOCAL_PROVINCE_CODE,
                        LOCAL_TUMBON_CODE = agias.LOCAL_TUMBON_CODE,
                        LOCAL_ZIPCODE = agias.LOCAL_ZIPCODE,
                        LOCAL_TELEPHONE = agias.LOCAL_TELEPHONE,
                        //USER_ID = per.USER_ID,
                        //USER_DATE = per.USER_DATE,
                        //REMARK = per.REMARK,
                        //UCOM_AGENT_ID = per.UCOM_AGENT_ID,
                        //UCOM_AGENT_TYPE = per.UCOM_AGENT_TYPE,
                        //NAME_CHG_DATE = per.NAME_CHG_DATE,
                        EMAIL = agias.EMAIL
                    };
                }
                //2 Find from AG_IAS_PERSONAL_T
                else if (agias == null)
                {
                    AG_PERSONAL_T per = base.ctx.AG_PERSONAL_T.FirstOrDefault(idc => idc.ID_CARD_NO.Trim() == idCard);
                    if (per != null)
                    {
                        //DTO.Person per = new DTO.Person();
                        //per.ID_CARD_NO = selectPer.ID_CARD_NO;

                        res.DataResponse = new DTO.Person
                        {
                            ID_CARD_NO = per.ID_CARD_NO,
                            PRE_NAME_CODE = per.PRE_NAME_CODE,
                            NAMES = per.NAMES,
                            LASTNAME = per.LASTNAME,
                            NATIONALITY = per.NATIONALITY,
                            BIRTH_DATE = per.BIRTH_DATE,
                            SEX = per.SEX,
                            EDUCATION_CODE = per.EDUCATION_CODE,
                            ADDRESS_1 = per.ADDRESS1,
                            ADDRESS_2 = per.ADDRESS2,
                            AREA_CODE = per.AREA_CODE,
                            PROVINCE_CODE = per.PROVINCE_CODE,
                            //TUMBON_CODE = per.TUMBON_CODE,
                            ZIP_CODE = per.ZIPCODE,
                            TELEPHONE = per.TELEPHONE,
                            LOCAL_ADDRESS1 = per.LOCAL_ADDRESS1,
                            LOCAL_ADDRESS2 = per.LOCAL_ADDRESS2,
                            LOCAL_AREA_CODE = per.LOCAL_AREA_CODE,
                            LOCAL_PROVINCE_CODE = per.LOCAL_PROVINCE_CODE,
                            //LOCAL_TUMBON_CODE = per.LOCAL_TUMBON_CODE,
                            LOCAL_ZIPCODE = per.LOCAL_ZIPCODE,
                            LOCAL_TELEPHONE = per.LOCAL_TELEPHONE,
                            //USER_ID = per.USER_ID,
                            //USER_DATE = per.USER_DATE,
                            //REMARK = per.REMARK,
                            //UCOM_AGENT_ID = per.UCOM_AGENT_ID,
                            //UCOM_AGENT_TYPE = per.UCOM_AGENT_TYPE,
                            //NAME_CHG_DATE = per.NAME_CHG_DATE,
                            EMAIL = per.E_MAIL
                        };
                    }
                    else
                    {
                        res.ErrorMsg = nullData;
                    }
                }
                else
                {
                    res.ErrorMsg = nullData;
                }
                

            }
            catch (Exception ex)
            {
                res.ErrorMsg = nullData;
                //LoggerFactory.CreateLog().Fatal("RegistrationService_GetPersonalDetailByIDCard", ex);
                
            }
            return res;

        }

        public static IEnumerable<DTO.Registration> NonIntersect(List<DTO.Registration> initial, List<DTO.Registration> final)
        {
            //subtracts the content of initial from final
            //assumes that final.length < initial.length
            return initial.Except(final);
        }

        public static IEnumerable<DTO.Registration> SymmetricDifference(List<DTO.Registration> initial, List<DTO.Registration> final)
        {
            IEnumerable<DTO.Registration> setA = NonIntersect(final, initial);
            IEnumerable<DTO.Registration> setB = NonIntersect(initial, final);
            // sum and return the two set.
            return setA.Concat(setB);
        }

        public static IEnumerable<string> SymmetricExcept(IEnumerable<string> First, IEnumerable<string> Second)
        {
            HashSet<string> hashSet = new HashSet<string>(First);
            hashSet.SymmetricExceptWith(Second);
            return hashSet.Select(x => x);
        }

        private DTO.ResponseMessage<bool> RegistrationCompare(DAL.AG_IAS_REGISTRATION_T regEntity)
        {
            var res = new DTO.ResponseMessage<bool>();
            List<string> reqlist = new List<string>();

            try
            {
                Func<string, string> NationConvert = delegate(string input)
                {

                    if((input != null) && (input != ""))
                    {
                        var list = ctx.AG_IAS_NATIONALITY.FirstOrDefault(n => n.NATIONALITY_NAME.Trim() == input);

                        if (list != null)
                        {
                            input = list.NATIONALITY_CODE;
                        }
                    }

                    return input;

                };

                //Get status = 1
                reqlist = this.extraConfig.Where(v => v.Value == "1").Select(ss => ss.Key).ToList();
                
                if (this.extraConfig.Where(v => v.Value == "1").Count() > 0)
                {
                    AG_IAS_PERSONAL_T iasper = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(iaspert => iaspert.ID_CARD_NO.Trim() == regEntity.ID_CARD_NO.Trim());

                    if (iasper != null)
                    {
                        foreach (string reqfield in reqlist)
                        {
                            switch (reqfield)
                            {
                                case "คำนำหน้าชื่อ":
                                    if (iasper.PRE_NAME_CODE != regEntity.PRE_NAME_CODE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ชื่อ":
                                    if (iasper.NAMES != regEntity.NAMES)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "นามสกุล":
                                    if (iasper.LASTNAME != regEntity.LASTNAME)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "สัญชาติ":
                                    if (iasper.NATIONALITY != regEntity.NATIONALITY)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "วันเกิด":
                                    if (iasper.BIRTH_DATE != regEntity.BIRTH_DATE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ระดับการศึกษา":
                                    if (iasper.EDUCATION_CODE != regEntity.EDUCATION_CODE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "อีเมล":
                                    if (iasper.EMAIL != regEntity.EMAIL)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "โทรศัพท์":
                                    if (iasper.LOCAL_TELEPHONE != regEntity.LOCAL_TELEPHONE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "โทรศัพท์มือถือ":
                                    if (iasper.TELEPHONE != regEntity.TELEPHONE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ที่อยู่ปัจจุบัน":
                                    if (iasper.ADDRESS_1 != regEntity.ADDRESS_1)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ที่อยู่ตามทะเบียนบ้าน":
                                    if (iasper.LOCAL_ADDRESS1 != regEntity.LOCAL_ADDRESS1)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    else
                    {

                        AG_PERSONAL_T per = base.ctx.AG_PERSONAL_T.FirstOrDefault(pert => pert.ID_CARD_NO.Trim() == regEntity.ID_CARD_NO.Trim());
                        if (per != null)
                        {
                            foreach (string reqfield in reqlist)
                            {
                                switch (reqfield)
                                {
                                    case "คำนำหน้าชื่อ":
                                        if (per.PRE_NAME_CODE != regEntity.PRE_NAME_CODE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ชื่อ":
                                        if (per.NAMES != regEntity.NAMES)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "นามสกุล":
                                        if (per.LASTNAME != regEntity.LASTNAME)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "สัญชาติ":
                                        if (NationConvert(per.NATIONALITY) != regEntity.NATIONALITY)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "วันเกิด":
                                        if (per.BIRTH_DATE != regEntity.BIRTH_DATE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ระดับการศึกษา":
                                        if (per.EDUCATION_CODE != regEntity.EDUCATION_CODE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "อีเมล":
                                        if (per.E_MAIL != regEntity.EMAIL)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "โทรศัพท์":
                                        if (per.LOCAL_TELEPHONE != regEntity.LOCAL_TELEPHONE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "โทรศัพท์มือถือ":
                                        if (per.TELEPHONE != regEntity.TELEPHONE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ที่อยู่ปัจจุบัน":
                                        if (per.ADDRESS1 != regEntity.ADDRESS_1)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ที่อยู่ตามทะเบียนบ้าน":
                                        if (per.LOCAL_ADDRESS1 != regEntity.LOCAL_ADDRESS1)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                    }
                }
                else
                {
                    res.ResultMessage = false;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_RegistrationCompare" + ":" + ex.Message, ex.Message);
            }

            if (res.ResultMessage == true)
            {
                this.ImportRegistration = "Y";
                this.RegistrationMatch = "Y";
            }

            return res; 

        }

        private DTO.ResponseMessage<bool> RegistrationCompareAllCfg(DAL.AG_IAS_REGISTRATION_T regEntity)
        {
            var res = new DTO.ResponseMessage<bool>();
            List<string> reqlist = new List<string>();

            try
            {
                Func<string, string> NationConvert = delegate(string input)
                {

                    if ((input != null) && (input != ""))
                    {
                        var list = ctx.AG_IAS_NATIONALITY.FirstOrDefault(n => n.NATIONALITY_NAME.Trim() == input);

                        if (list != null)
                        {
                            input = list.NATIONALITY_CODE;
                        }
                    }

                    return input;

                };

                //Get status = 1
                reqlist = this.extraConfig.Select(ss => ss.Key).ToList();

                if (this.extraConfig.Count() > 0)
                {
                    AG_IAS_PERSONAL_T iasper = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(iaspert => iaspert.ID_CARD_NO.Trim() == regEntity.ID_CARD_NO.Trim());

                    if (iasper != null)
                    {
                        foreach (string reqfield in reqlist)
                        {
                            switch (reqfield)
                            {
                                case "คำนำหน้าชื่อ":
                                    if (iasper.PRE_NAME_CODE != regEntity.PRE_NAME_CODE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ชื่อ":
                                    if (iasper.NAMES != regEntity.NAMES)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "นามสกุล":
                                    if (iasper.LASTNAME != regEntity.LASTNAME)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "สัญชาติ":
                                    if (iasper.NATIONALITY != regEntity.NATIONALITY)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "วันเกิด":
                                    if (iasper.BIRTH_DATE != regEntity.BIRTH_DATE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ระดับการศึกษา":
                                    if (iasper.EDUCATION_CODE != regEntity.EDUCATION_CODE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "อีเมล":
                                    if (iasper.EMAIL != regEntity.EMAIL)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "โทรศัพท์":
                                    if (iasper.LOCAL_TELEPHONE != regEntity.LOCAL_TELEPHONE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "โทรศัพท์มือถือ":
                                    if (iasper.TELEPHONE != regEntity.TELEPHONE)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ที่อยู่ปัจจุบัน":
                                    if (iasper.ADDRESS_1 != regEntity.ADDRESS_1)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                case "ที่อยู่ตามทะเบียนบ้าน":
                                    if (iasper.LOCAL_ADDRESS1 != regEntity.LOCAL_ADDRESS1)
                                    {
                                        res.ResultMessage = true;
                                    };
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                    else
                    {

                        AG_PERSONAL_T per = base.ctx.AG_PERSONAL_T.FirstOrDefault(pert => pert.ID_CARD_NO.Trim() == regEntity.ID_CARD_NO.Trim());
                        if (per != null)
                        {
                            foreach (string reqfield in reqlist)
                            {
                                switch (reqfield)
                                {
                                    case "คำนำหน้าชื่อ":
                                        if (per.PRE_NAME_CODE != regEntity.PRE_NAME_CODE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ชื่อ":
                                        if (per.NAMES != regEntity.NAMES)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "นามสกุล":
                                        if (per.LASTNAME != regEntity.LASTNAME)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "สัญชาติ":
                                        if (NationConvert(per.NATIONALITY) != regEntity.NATIONALITY)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "วันเกิด":
                                        if (per.BIRTH_DATE != regEntity.BIRTH_DATE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ระดับการศึกษา":
                                        if (per.EDUCATION_CODE != regEntity.EDUCATION_CODE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "อีเมล":
                                        if (per.E_MAIL != regEntity.EMAIL)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "โทรศัพท์":
                                        if (per.LOCAL_TELEPHONE != regEntity.LOCAL_TELEPHONE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "โทรศัพท์มือถือ":
                                        if (per.TELEPHONE != regEntity.TELEPHONE)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ที่อยู่ปัจจุบัน":
                                        if (per.ADDRESS1 != regEntity.ADDRESS_1)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    case "ที่อยู่ตามทะเบียนบ้าน":
                                        if (per.LOCAL_ADDRESS1 != regEntity.LOCAL_ADDRESS1)
                                        {
                                            res.ResultMessage = true;
                                        };
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                    }
                }
                else
                {
                    res.ResultMessage = false;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_RegistrationCompare" + ":" + ex.Message, ex.Message);
            }

            if (res.ResultMessage == true)
            {
                this.ImportRegistration = "Y";
                this.RegistrationMatch = "Y";
            }

            return res;

        }

        private DTO.ResponseMessage<bool> RegistrationValidationByCfg(DAL.AG_IAS_REGISTRATION_T regEntity)
        {
            var res = new DTO.ResponseMessage<bool>();
            List<string> reqlist = new List<string>();

            try
            {
                reqlist = this.enableProfileConfig.Where(v => v.Value == "Y").Select(ss => ss.Key).ToList();
                if (this.enableProfileConfig.Where(v => v.Value == "Y").Count() > 0)
                {
                    AG_IAS_PERSONAL_T iaspert = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(id => id.ID_CARD_NO.Trim() == regEntity.ID_CARD_NO.Trim());
                    AG_PERSONAL_T pert = base.ctx.AG_PERSONAL_T.FirstOrDefault(id => id.ID_CARD_NO.Trim() == regEntity.ID_CARD_NO.Trim());

                    foreach (string itemkey in reqlist)
                    {
                        switch (itemkey)
                        {
                            //เป็นผู้สมัครใหม่ และไม่มีการอ้างอิงข้อมูลเดิม
                            case "General_New":
                                if (regEntity.IMPORT_STATUS != null)
                                {
                                    if (regEntity.IMPORT_STATUS.Equals("N") && (iaspert == null) && (pert == null))
                                    {
                                        res.ResultMessage = true;
                                    }
                                }
                                break;

                            //เป็นผู้สมัครใหม่ ไม่มีการอ้างอิงข้อมูลเดิม แต่มีความสอดคล้องของข้อมูล
                            case "General_NotImport":
                                if (regEntity.IMPORT_STATUS != null)
                                {
                                    if (regEntity.IMPORT_STATUS.Equals("N"))
                                    {
                                        if ((iaspert != null) || (pert != null))
                                        {
                                            this.ImportRegistration = "Y";
                                            res.ResultMessage = true;
                                        }
                                        
                                    }
                                }
                                break;

                            //มีการอ้างอิงข้อมูลเดิม และไม่มีการแก้ไขข้อมูล
                            case "General_Old_Import_EditN":
                                if (regEntity.IMPORT_STATUS != null)
                                {
                                    if (regEntity.IMPORT_STATUS.Equals("Y"))
                                    {
                                        DTO.ResponseMessage<bool> resnewcfg = this.RegistrationCompareAllCfg(regEntity);
                                        if (resnewcfg.ResultMessage == true)
                                        {
                                            res.ResultMessage = true;
                                        }

                                    }
                                }
                                break;

                            //มีการอ้างอิงข้อมูลเดิม และมีการแก้ไขข้อมูล
                            case "General_Old_Import_EditY":
                                if (regEntity.IMPORT_STATUS != null)
                                {
                                    if (regEntity.IMPORT_STATUS.Equals("Y"))
                                    {
                                        DTO.ResponseMessage<bool> resnewcfg = this.RegistrationCompareAllCfg(regEntity);
                                        if (resnewcfg.ResultMessage == true)
                                        {
                                            res.ResultMessage = true;
                                        }

                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }

                }
                else
                {
                    res.ResultMessage = false;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("RegistrationService_RegistrationValidationByCfg" + ":" +ex.Message, ex.Message);
            }

            return res;

        }

        #endregion

        #region
        public DTO.ResponseService<DataSet> ReportRegisterLicense(string licensetype, string comcode, string startdate, string enddate)
        {

            string where = "";
            if (licensetype != "")
            {
                if (licensetype == "09")
                {
                    where += " AND ELR.LICENSE_TYPE_CODE = '01' OR  ELR.LICENSE_TYPE_CODE = '07' ";
                }
                else if (licensetype == "10")
                {
                    where += " AND ELR.LICENSE_TYPE_CODE = '02' OR  ELR.LICENSE_TYPE_CODE = '05' OR ELR.LICENSE_TYPE_CODE = '06' OR  ELR.LICENSE_TYPE_CODE = '08' ";
                }
                else
                {
                    where += " AND ELR.LICENSE_TYPE_CODE = '" + licensetype + "' ";
                }
            }

            if (comcode != "")
            {
                where += " AND ACT.INSUR_COMP_CODE = '"+comcode+"' ";
            }

            DateTime sdate = Convert.ToDateTime(startdate).AddYears(-543);
            DateTime edate = Convert.ToDateTime(enddate).AddYears(-543);

            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            string sql = "SELECT  " +
                        "	AAA.*, LTR.LICENSE_TYPE_NAME,  " +
                        "	(  " +
                        "		ROUND((AAA.COUNT_COMP_CODE * 100) / AAA.COUNT_TYPE_CODE,2)  " +
                        "	) AS FORSHARE  " +
                        "FROM  " +
                        "	(  " +
                        "		SELECT  " +
                        "			*  " +
                        "		FROM  " +
                        "			(  " +
                        "				SELECT  " +
                        "					COUNT (*) AS COUNT_COMP_CODE,  " +
                        "					ELR.LICENSE_TYPE_CODE,  " +
                        "					ACT.INSUR_COMP_CODE,  " +
                        "					VCOM. NAME  " +
                        "				FROM  " +
                        "					AG_APPLICANT_T ACT  " +
                        "				INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "				INNER JOIN VW_IAS_COM_CODE VCOM ON ACT.INSUR_COMP_CODE = VCOM. ID  " +
                        "               WHERE ACT.APPLY_DATE >= TO_DATE ('"+sdate.ToShortDateString()+"', 'DD/MM/yyyy') " +
                        "               AND ACT.APPLY_DATE <= TO_DATE ('"+edate.ToShortDateString()+"', 'DD/MM/yyyy')" +where+
                        "				GROUP BY  " +
                        "					ELR.LICENSE_TYPE_CODE,  " +
                        "					ACT.INSUR_COMP_CODE,  " +
                        "					VCOM. NAME  " +
                        "			) ACT  " +
                        "		INNER JOIN (  " +
                        "			SELECT  " +
                        "				COUNT (*) AS COUNT_TYPE_CODE,  " +
                        "				ELR.LICENSE_TYPE_CODE AS LICENSE_TYPE_CODE2  " +
                        "			FROM  " +
                        "				AG_APPLICANT_T ACT  " +
                        "			INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "           INNER JOIN VW_IAS_COM_CODE VCOM ON ACT.INSUR_COMP_CODE = VCOM. ID  "+
                        "           WHERE  ACT.APPLY_DATE >= TO_DATE ('"+sdate.ToShortDateString()+"', 'DD/MM/yyyy') " +
                        "               AND ACT.APPLY_DATE <= TO_DATE ('"+edate.ToShortDateString()+"', 'DD/MM/yyyy')" + where+
                        "			GROUP BY  " +
                        "				ELR.LICENSE_TYPE_CODE  " +
                        "		) COM ON ACT.LICENSE_TYPE_CODE = COM.LICENSE_TYPE_CODE2  " +
                        "	) AAA  " +
                        " INNER JOIN AG_IAS_LICENSE_TYPE_R LTR ON LTR.LICENSE_TYPE_CODE = AAA.LICENSE_TYPE_CODE "+
                        " ORDER BY AAA.LICENSE_TYPE_CODE,AAA.COUNT_COMP_CODE DESC ";

            OracleDB connect = new OracleDB();

            try
            {
                DataSet tb = connect.GetDataSet(sql);
                res.DataResponse = tb;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<DataSet> ReportRegisterLicenseStaticRatio(string lincensetype, string startdateone, string enddateone, string startdatetwo, string enddatetwo)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            string where = "";
            if (lincensetype != "")
            {
                where += " WHERE EE.TYPE_CODE = '"+lincensetype+"' ";
            }
            DateTime sdate1 = Convert.ToDateTime(startdateone).AddYears(-543);
            DateTime edate1 = Convert.ToDateTime(enddateone).AddYears(-543);
            DateTime sdate2 = Convert.ToDateTime(startdatetwo).AddYears(-543);
            DateTime edate2 = Convert.ToDateTime(enddatetwo).AddYears(-543);

            string sd1 = " AND ACT.APPLY_DATE >= TO_DATE('" + sdate1.ToShortDateString() + "', 'DD/MM/yyyy') AND ACT.APPLY_DATE <= TO_DATE('" + edate1.ToShortDateString() + "', 'DD/MM/yyyy')";
            string sd2 = " AND ACT.APPLY_DATE >= TO_DATE('" + sdate2.ToShortDateString() + "', 'DD/MM/yyyy') AND ACT.APPLY_DATE <= TO_DATE('" + edate2.ToShortDateString() + "', 'DD/MM/yyyy')";

            string type01_one = "						SELECT  " +
                        "							COUNT (*) AS COUNT_TYPE,  " +
                        "							'ตัวแทนประกันชีวิต' AS TYPE_NAME,  " +
                        "							'01' AS TYPE_CODE  " +
                        "						FROM  " +
                        "							AG_APPLICANT_T ACT  " +
                        "						INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "						WHERE  " +
                        "							ELR.LICENSE_TYPE_CODE IN ('01', '07')  " + sd1;
            string type02_one = "							SELECT  " +
                        "								COUNT (*) AS COUNT_TYPE,  " +
                        "								'ตัวแทนประกันวินาศภัย' AS TYPE_NAME,  " +
                        "								'02' AS TYPE_CODE  " +
                        "							FROM  " +
                        "								AG_APPLICANT_T ACT  " +
                        "							INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "							WHERE  " +
                        "								ELR.LICENSE_TYPE_CODE IN ('02', '05', '06', '08')  " + sd1;
            string type03_one = "								SELECT  " +
                        "									COUNT (*) AS COUNT_TYPE,  " +
                        "									'นายหน้าประกันชีวิต(บุคคลธรรมดา)' AS TYPE_NAME,  " +
                        "									'03' AS TYPE_CODE  " +
                        "								FROM  " +
                        "									AG_APPLICANT_T ACT  " +
                        "								INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "								WHERE  " +
                        "									ELR.LICENSE_TYPE_CODE IN ('03')  " + sd1;
            string type04_one = "									SELECT  " +
                        "										COUNT (*) AS COUNT_TYPE,  " +
                        "										'นายหน้าประกันวินาศภัย(บุคคลธรรมดา)' AS TYPE_NAME,  " +
                        "										'04' AS TYPE_CODE  " +
                        "									FROM  " +
                        "										AG_APPLICANT_T ACT  " +
                        "									INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "									WHERE  " +
                        "										ELR.LICENSE_TYPE_CODE IN ('04')  " + sd1;


            string type01_two = "							SELECT  " +
                        "								COUNT (*) AS COUNT_TYPE2,  " +
                        "								'ตัวแทนประกันชีวิต' AS TYPE_NAME2,  " +
                        "								'01' AS TYPE_CODE2  " +
                        "							FROM  " +
                        "								AG_APPLICANT_T ACT  " +
                        "							INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "							WHERE  " +
                        "								ELR.LICENSE_TYPE_CODE IN ('01', '07')  " + sd2;

            string type02_two = "								SELECT  " +
                        "									COUNT (*) AS COUNT_TYPE2,  " +
                        "									'ตัวแทนประกันวินาศภัย' AS TYPE_NAME2,  " +
                        "									'02' AS TYPE_CODE2  " +
                        "								FROM  " +
                        "									AG_APPLICANT_T ACT  " +
                        "								INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "								WHERE  " +
                        "									ELR.LICENSE_TYPE_CODE IN ('02', '05', '06', '08')  " + sd2;

            string type03_two = "									SELECT  " +
                        "										COUNT (*) AS COUNT_TYPE2,  " +
                        "										'นายหน้าประกันชีวิต(บุคคลธรรมดา)' AS TYPE_NAME2,  " +
                        "										'03' AS TYPE_CODE2  " +
                        "									FROM  " +
                        "										AG_APPLICANT_T ACT  " +
                        "									INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "									WHERE  " +
                        "										ELR.LICENSE_TYPE_CODE IN ('03')  " + sd2;

            string type04_two = "										SELECT  " +
                        "											COUNT (*) AS COUNT_TYPE2,  " +
                        "											'นายหน้าประกันวินาศภัย(บุคคลธรรมดา)' AS TYPE_NAME2,  " +
                        "											'04' AS TYPE_CODE2  " +
                        "										FROM  " +
                        "											AG_APPLICANT_T ACT  " +
                        "										INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "										WHERE  " +
                        "											ELR.LICENSE_TYPE_CODE IN ('04')  " + sd2;
            string select_one = "";
            string select_two = "";
            string select_in = "";
            if (lincensetype == "")
            {
                select_one = type01_one + "     UNION ALL  " + type02_one + "   UNION ALL  " + type03_one + " UNION ALL  " + type04_one;
                select_two = type01_two + "     UNION ALL  " + type02_two + "   UNION ALL  " + type03_two + " UNION ALL  " + type04_two;
                select_in = " '01','02','03','04','05','06','07','08' ";
            }
            else if (lincensetype == "01")
            {
                select_one = type01_one;
                select_two = type01_two;
                select_in = " '01','07' ";
            }
            else if (lincensetype == "02")
            {
                select_one = type02_one;
                select_two = type02_two;
                select_in = " '02','05','06','08'";
            }
            else if (lincensetype == "03")
            {
                select_one = type03_one;
                select_two = type03_two;
                select_in = " '03' ";
            }
            else if (lincensetype == "04")
            {
                select_one = type04_one;
                select_two = type04_two;
                select_in = " '04' ";
            }
            
            
            string sql = "SELECT  " +
                        "	EE.*, EE.FOR_SHARE2 - EE.FOR_SHARE AS RATIO  " +
                        "FROM  " +
                        "	(  " +
                        "		SELECT  " +
                        "			BB.*, ROUND (  " +
                        "				(  " +
                        "					(BB.COUNT_TYPE * 100) / BB.SUMONE  " +
                        "				),  " +
                        "				2  " +
                        "			) AS FOR_SHARE,  " +
                        "			CC.*  " +
                        "		FROM  " +
                        "			(  " +
                        "				SELECT  " +
                        "					*  " +
                        "				FROM  " +
                        "					(  " + select_one +                        
                        "					) AA,  " +
                        "					(  " +
                        "						SELECT  " +
                        "							COUNT (*) AS SUMONE  " +
                        "						FROM  " +
                        "							AG_APPLICANT_T ACT  " +
                        "						INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "						WHERE  " +
                        "							ELR.LICENSE_TYPE_CODE IN (  " +select_in+
                        "							)  " +sd1+
                        "					)  " +
                        "			) BB  " +
                        "		INNER JOIN (  " +
                        "			SELECT  " +
                        "				BB.*, ROUND (  " +
                        "					(  " +
                        "						(BB.COUNT_TYPE2 * 100) / BB.SUMONE2  " +
                        "					),  " +
                        "					2  " +
                        "				) AS FOR_SHARE2  " +
                        "			FROM  " +
                        "				(  " +
                        "					SELECT  " +
                        "						*  " +
                        "					FROM  " +
                        "						(  " +   select_two+                     
                        "						) AA,  " +
                        "						(  " +
                        "							SELECT  " +
                        "								COUNT (*) AS SUMONE2  " +
                        "							FROM  " +
                        "								AG_APPLICANT_T ACT  " +
                        "							INNER JOIN AG_EXAM_LICENSE_R ELR ON ACT.TESTING_NO = ELR.TESTING_NO  " +
                        "							WHERE  " +
                        "								ELR.LICENSE_TYPE_CODE IN (  " +select_in+
                        "								)  " + sd2 +
                        "						)  " +
                        "				) BB  " +
                        "		) CC ON BB.TYPE_CODE = CC.TYPE_CODE2  " +
                        "	) EE "+where;
            OracleDB db = new OracleDB();
            try
            {
                DataSet tb = db.GetDataSet(sql);
                res.DataResponse = tb;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        #endregion


        #region ReportLicense
        public DTO.ResponseService<DataSet> GetLicenseReport(string LicenseTypeCode, string CompCode, string StartDate, string EndDate)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                string ConLicense=string.Empty;
                string Concompcode=string.Empty;

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

                        if(LicenseTypeCode !="")
                        {
                            if (LicenseTypeCode == "09")
                            {
                                ConLicense = "  and (ALT.LICENSE_TYPE_CODE ='01' or ALT.LICENSE_TYPE_CODE ='07') ";
                            }
                            else if (LicenseTypeCode == "10")
                            {
                                ConLicense = "  and (ALT.LICENSE_TYPE_CODE ='02' or ALT.LICENSE_TYPE_CODE ='05' or ALT.LICENSE_TYPE_CODE ='06' or ALT.LICENSE_TYPE_CODE ='08' )";
                            }
                            else
                            {
                                ConLicense = " and ALT.LICENSE_TYPE_CODE ='" + LicenseTypeCode + "' ";
                            }
                        }
                        else
                        {ConLicense="";}
                        if(CompCode != "")
                        {
                            Concompcode = " and com.id like '" + CompCode + "' ";
                        }
                        else
                        {Concompcode="";}


                    string sql ="select CCC.*,ROUND(((CCC.COUNT_COME_CODE*100)/CCC.COUNT_TYPE_CODE),2) AS FORSHARE,alt.license_type_name FROM( "
                    +"select * FROM( "
                    +"select count(*) as COUNT_COME_CODE,AAT.INSURANCE_COMP_CODE,ALT.LICENSE_TYPE_CODE,com.id,COM.NAME from AG_LICENSE_T ALT "
                    +"inner join AG_AGENT_LICENSE_T AAT "
                    +"ON ALT.LICENSE_NO = AAT.LICENSE_NO "
                    +"INNER JOIN VW_IAS_COM_CODE COM "
                    +"ON AAT.INSURANCE_COMP_CODE = COM.ID "
                    + "where ALT.DATE_LICENSE_ACT  between TO_DATE('" + StartDate + "','dd/MM/yyyy') and TO_DATE('" + EndDate + "','dd/MM/yyyy')  "
                    + ConLicense 
                    + Concompcode 
                    +"group by AAT.INSURANCE_COMP_CODE, ALT.LICENSE_TYPE_CODE, com.id, COM.NAME "
                    +")AAA "
                    +"INNER JOIN "
                    +"( "
                    +"select count(*) as COUNT_TYPE_CODE,ALT.LICENSE_TYPE_CODE as LICENSE_TYPE_CODE2  from AG_LICENSE_T ALT "
                    +"inner join AG_AGENT_LICENSE_T AAT "
                    +"ON ALT.LICENSE_NO = AAT.LICENSE_NO "

                    + "where ALT.DATE_LICENSE_ACT  between TO_DATE('" + StartDate + "','dd/MM/yyyy') and TO_DATE('" + EndDate + "','dd/MM/yyyy') " 
                    + ConLicense
                    +"group by ALT.LICENSE_TYPE_CODE "

                    +")BBB ON AAA.LICENSE_TYPE_CODE = BBB.LICENSE_TYPE_CODE2 "
                    +") CCC "
                    +"inner join "
                    +"ag_ias_license_type_r alt "
                    +"on ccc.License_type_code=alt.license_type_code "
                    + "order by ccc.License_type_code,ccc.COUNT_COME_CODE desc";

                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;

        }

        
        #endregion
        
    }
}

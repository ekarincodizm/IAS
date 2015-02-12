using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IAS.DAL;
using IAS.Utils;
using System.Transactions;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

using IAS.DataServices.Person.Helpers;
using System.Web.Services;
using IAS.DAL.Interfaces;
using IAS.DataServices.Properties;
using IAS.Common.Logging;
using System.IO;
using System.ServiceModel.Activation;
using IAS.DTO.FileService;
using IAS.DataServices.FileManager;

namespace IAS.DataServices.Person
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PersonService : AbstractService, IPersonService
    {
        //Natta
        private static String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString();

        public PersonService()
        {
        }
        public PersonService(IIASPersonEntities _ctx)
            : base(_ctx)
        {
        }
        private static readonly string adPath = ConfigurationManager.AppSettings["ADPath"].ToString();
        private static readonly string userName = ConfigurationManager.AppSettings["ADUserName"].ToString();
        private static readonly string password = ConfigurationManager.AppSettings["ADPassword"].ToString();
        private static readonly string adDomain = ConfigurationManager.AppSettings["ADDomain"].ToString();

        public DTO.ResponseService<DTO.Person> GetUserProfile(string Id, string memType)
        {
            DTO.ResponseService<DTO.Person> res = new DTO.ResponseService<DTO.Person>();

            try
            {
                var per = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(s => s.ID == Id && s.MEMBER_TYPE == memType);
                if (per != null)
                {
                    res.DataResponse = new DTO.Person();
                    per.MappingToEntity(res.DataResponse);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetUserProfile", ex);
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetDataTo8Report(string id, string license_code)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = " select vitn.name TITLE,aipt.names NAMES,aipt.lastname LASTNAME,aipt.telephone MOBILE,aipt.email EMAIL, " +
                             " aipt.address_1 || ' ' || aipt.address_2 now_address,via.name now_A,VIT.NAME now_T,vip.name now_P,aipt.zip_code now_Z,  " +
                             " alrt.renew_time renew_time,alrt.expire_date Ex_date,vicc.name Com_name,aalt.license_no license_no, " +
                             " aipt.local_address1 || ' ' || aipt.local_address2 local_address, via_l.name local_A,VIT_l.NAME local_t,vip_l.name local_p,aipt.local_zipcode local_z, " +
                             " aipt.education_code edu_code,aipt.local_telephone tel " +
                             "           from ag_ias_personal_t AIPT " +
                            " left outer join ag_agent_license_t AALT  on aalt.id_card_no = AIPT.id_card_no  " +
                            " left outer join ag_license_t ALT on aalt.license_no=alt.license_no and alt.license_type_code ='" + license_code + "' " +
                            " left outer join ag_agent_license_person_t AGLPT on aglpt.license_no=alt.license_no " +
                            " left outer join vw_ias_com_code VICC on vicc.id = aalt.insurance_comp_code " +
                            " left outer join ag_license_renew_t ALRT on alrt.license_no = aalt.license_no " +
                            " left outer join vw_ias_title_name VITN on VITN.ID = AIPT.pre_name_code " +
                            " left outer join vw_ias_province VIP on VIP.ID = aipt.province_code " +
                            " left outer join vw_ias_tumbon VIT on VIT.id=aipt.tumbon_code and vit.ampurcode = aipt.area_code and vit.provincecode = aipt.province_code " +
                            " left outer join vw_ias_ampur VIA on via.id = aipt.area_code and via.provincecode = AIPT.province_code  " +
                            " left outer join vw_ias_province VIP_L on VIP_L.ID = AIPT.local_province_code " +
                            " left outer join vw_ias_tumbon VIT_L on VIT_L.id=aipt.local_tumbon_code and vit_L.ampurcode = aipt.local_area_code and vit_L.provincecode = aipt.local_province_code " +
                            " left outer join vw_ias_ampur VIA_L on via_L.id = aipt.local_area_code and via_L.provincecode = AIPT.local_province_code  " +
                            " where AIPT.id_card_no = '" + id + "'  " +
                            " order by alrt.expire_date desc";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetDataTo8Report", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetDataRenewReport(string id, string license_code, string license_no)
        {
            var res = new DTO.ResponseService<DataSet>();

            OracleDB ora = new OracleDB();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string sql = string.Empty;

            try
            {

                        //find AG_AGENT_LICENSE_T
                        sql = "select vitn.NAME title,aipt.NAMES names,aipt.LASTNAME lastname,aipt.TELEPHONE mobile,aipt.EMAIL email, " +
                            " aipt.ADDRESS_1 || ' ' || aipt.ADDRESS_2 now_address,via.NAME now_A,vit.NAME now_T,vip.NAME now_P,aipt.ZIP_CODE now_Z,  " +
                            " alrt.RENEW_TIME renew_time,alrt.EXPIRE_DATE ex_date,vicc.NAME Com_name,al.LICENSE_NO license_no, " +
                            " aipt.LOCAL_ADDRESS1 || ' ' || aipt.LOCAL_ADDRESS2 local_address, via_l.NAME local_A,vit_l.NAME local_t,vip_l.NAME local_p,aipt.LOCAL_ZIPCODE local_z, " +
                            " aipt.EDUCATION_CODE edu_code,aipt.LOCAL_TELEPHONE tel " +
                            "from AG_IAS_PERSONAL_T aipt " +
                            "left outer join VW_IAS_TITLE_NAME vitn on vitn.ID = aipt.PRE_NAME_CODE " +
                            "left outer join VW_IAS_PROVINCE vip on vip.ID = aipt.PROVINCE_CODE " +
                            "left outer join VW_IAS_TUMBON vit on vit.ID=aipt.TUMBON_CODE " +
                            "and vit.AMPURCODE = aipt.AREA_CODE " +
                            "and vit.PROVINCECODE = aipt.PROVINCE_CODE " +
                            "left outer join VW_IAS_AMPUR via on via.ID = aipt.AREA_CODE " +
                            "and via.PROVINCECODE = aipt.PROVINCE_CODE  " +
                            "left outer join VW_IAS_PROVINCE vip_l on vip_l.ID = aipt.LOCAL_PROVINCE_CODE  " +
                            "left outer join VW_IAS_TUMBON vit_l on vit_l.ID=aipt.LOCAL_TUMBON_CODE and vit_L.AMPURCODE = aipt.LOCAL_AREA_CODE " +
                            "and vit_l.PROVINCECODE = aipt.LOCAL_PROVINCE_CODE " +
                            "left outer join VW_IAS_AMPUR via_l on via_l.ID = aipt.LOCAL_AREA_CODE " +
                            "and via_l.PROVINCECODE = aipt.LOCAL_PROVINCE_CODE  " +
                            ",AG_AGENT_LICENSE_T aglt " +
                            "left outer join VW_IAS_COM_CODE vicc on vicc.id = aglt.INSURANCE_COMP_CODE " +
                            ",AG_LICENSE_T al " +
                            "left outer join AG_LICENSE_RENEW_T alrt on alrt.LICENSE_NO = al.LICENSE_NO " +
                            "where aglt.ID_CARD_NO = aipt.ID_CARD_NO " +
                            "and aglt.LICENSE_NO =al.LICENSE_NO " +
                            "and aipt.ID_CARD_NO = '" + id.Trim() + "' " +
                            "and al.LICENSE_NO = '" + license_no.Trim() + "' " +
                            "and al.LICENSE_TYPE_CODE = '" + license_code.Trim() + "' " +
                            "order by alrt.RENEW_TIME desc ";

                
                       ds = ora.GetDataSet(sql);

                       if (ds.Tables[0].Rows.Count != 0)
                       {
                           res.DataResponse = ds;

                       }
                       else
                       {
                           sql = string.Empty;
                           ds = new DataSet();
                         
                           //find AG_AGENT_LICENSE_T
                           sql = "select vitn.NAME title,aipt.NAMES names,aipt.LASTNAME lastname,aipt.TELEPHONE mobile,aipt.EMAIL email, " +
                            " aipt.ADDRESS_1 || ' ' || aipt.ADDRESS_2 now_address,via.NAME now_A,vit.NAME now_T,vip.NAME now_P,aipt.ZIP_CODE now_Z,  " +
                            " alrt.RENEW_TIME renew_time,alrt.EXPIRE_DATE ex_date,vicc.NAME Com_name,al.LICENSE_NO license_no, " +
                            " aipt.LOCAL_ADDRESS1 || ' ' || aipt.LOCAL_ADDRESS2 local_address, via_l.NAME local_A,vit_l.NAME local_t,vip_l.NAME local_p,aipt.LOCAL_ZIPCODE local_z, " +
                            " aipt.EDUCATION_CODE edu_code,aipt.LOCAL_TELEPHONE tel " +
                            "from AG_IAS_PERSONAL_T aipt " +
                            "left outer join VW_IAS_TITLE_NAME vitn on vitn.ID = aipt.PRE_NAME_CODE " +
                            "left outer join VW_IAS_PROVINCE vip on vip.ID = aipt.PROVINCE_CODE " +
                            "left outer join VW_IAS_TUMBON vit on vit.ID=aipt.TUMBON_CODE " +
                            "and vit.AMPURCODE = aipt.AREA_CODE " +
                            "and vit.PROVINCECODE = aipt.PROVINCE_CODE " +
                            "left outer join VW_IAS_AMPUR via on via.ID = aipt.AREA_CODE " +
                            "and via.PROVINCECODE = aipt.PROVINCE_CODE  " +
                            "left outer join VW_IAS_PROVINCE vip_l on vip_l.ID = aipt.LOCAL_PROVINCE_CODE  " +
                            "left outer join VW_IAS_TUMBON vit_l on vit_l.ID=aipt.LOCAL_TUMBON_CODE and vit_L.AMPURCODE = aipt.LOCAL_AREA_CODE " +
                            "and vit_l.PROVINCECODE = aipt.LOCAL_PROVINCE_CODE " +
                            "left outer join VW_IAS_AMPUR via_l on via_l.ID = aipt.LOCAL_AREA_CODE " +
                            "and via_l.PROVINCECODE = aipt.LOCAL_PROVINCE_CODE  " +
                            "left outer join VW_IAS_COM_CODE vicc on vicc.id = aipt.COMP_CODE " +
                            ",AG_AGENT_LICENSE_PERSON_T aglp " +
                            ",AG_LICENSE_T al " +
                            "left outer join AG_LICENSE_RENEW_T alrt on alrt.LICENSE_NO = al.LICENSE_NO " +
                            "where aglp.ID_CARD_NO = aipt.ID_CARD_NO " +
                            "and aglp.LICENSE_NO =al.LICENSE_NO " +
                            "and aipt.ID_CARD_NO = '" + id.Trim() + "' " +
                            "and al.LICENSE_NO = '" + license_no.Trim() + "' " +
                            "and al.LICENSE_TYPE_CODE = '" + license_code.Trim() + "' " +
                            "order by alrt.RENEW_TIME desc ";



                           ds = ora.GetDataSet(sql);

                           res.DataResponse = ds;
                       }

              
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetDataTo8Report", ex);
            }
            return res;
        
        
        
        }




        public DTO.ResponseService<DTO.Person> GetById(string Id)
        {
            DTO.ResponseService<DTO.Person> res = new DTO.ResponseService<DTO.Person>();

            try
            {
                var per = base.ctx.AG_IAS_PERSONAL_T
                             .SingleOrDefault(s => s.ID == Id);
                if (per != null)
                {
                    res.DataResponse = new DTO.Person();
                    per.MappingToEntity(res.DataResponse);
                    //res.DataResponse.IMG_SIGN = per.IMG_SIGN;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetById", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.Person> GetUserProfileById(string Id)
        {
            DTO.ResponseService<DTO.Person> res = new DTO.ResponseService<DTO.Person>();

            try
            {
                string waitForApproveCode = DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString();

                var per = base.ctx.AG_IAS_PERSONAL_T
                             .SingleOrDefault(s => s.ID == Id);
                if (per != null)
                {
                    DTO.Person ent = new DTO.Person();
                    if (per.STATUS == waitForApproveCode)
                    {
                        DTO.ResponseService<DTO.PersonTemp> resTemp = GetPersonTemp(Id);
                        resTemp.DataResponse.MappingToEntity(ent);
                    }
                    else
                    {
                        per.MappingToEntity(ent);
                    }
                    res.DataResponse = ent;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetUserProfileById", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.PersonTemp> GetPersonTemp(string Id)
        {
            DTO.ResponseService<DTO.PersonTemp> res = new DTO.ResponseService<DTO.PersonTemp>();

            try
            {
                var tmp = base.ctx.AG_IAS_TEMP_PERSONAL_T
                             .SingleOrDefault(s => s.ID == Id);

                res.DataResponse = new DTO.PersonTemp();

                if (tmp != null)
                {
                    tmp.MappingToEntity(res.DataResponse);
                }
                else
                {
                    var per = base.ctx.AG_IAS_PERSONAL_T
                                 .SingleOrDefault(s => s.ID == Id);
                    per.MappingToEntity(res.DataResponse);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetPersonTemp", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> SetPersonTemp(DTO.PersonTemp tmpPerson, List<DTO.PersonAttatchFile> tmpFiles)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool> { ResultMessage = false };

            try
            {


                AG_IAS_PERSONAL_T entExist = ctx.AG_IAS_PERSONAL_T
                  .FirstOrDefault(s => ((s.EMAIL == tmpPerson.EMAIL)) && (s.ID_CARD_NO != tmpPerson.ID_CARD_NO));

                if (entExist != null)
                {
                    res.ErrorMsg = string.Format(Resources.infoPersonService_001);
                    return res;
                }

                var oldTmp = base.ctx.AG_IAS_TEMP_PERSONAL_T
                             .SingleOrDefault(s => s.ID == tmpPerson.ID);

                if (oldTmp != null)
                {
                    oldTmp.UPDATED_DATE = DateTime.Now;
                    tmpPerson.MappingToEntity(oldTmp);
                    oldTmp.STATUS = DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString();
                }
                else
                {
                    var newTmp = new AG_IAS_TEMP_PERSONAL_T();
                    tmpPerson.MappingToEntity(newTmp);
                    tmpPerson.STATUS = DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString();
                    newTmp.UPDATED_DATE = DateTime.Now;
                    base.ctx.AG_IAS_TEMP_PERSONAL_T.AddObject(newTmp);
                }

                var person = base.ctx.AG_IAS_PERSONAL_T
                                 .SingleOrDefault(s => s.ID == tmpPerson.ID);

                var regis = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(r => r.ID == tmpPerson.ID);


                if (person != null)
                {
                    person.STATUS = DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString();
                    if ((tmpPerson.MEMBER_TYPE == DTO.RegistrationType.OIC.GetEnumValue().ToString()) || (tmpPerson.MEMBER_TYPE == DTO.RegistrationType.OICAgent.GetEnumValue().ToString())
                     || (tmpPerson.MEMBER_TYPE == DTO.RegistrationType.OICFinace.GetEnumValue().ToString()) || (tmpPerson.MEMBER_TYPE == DTO.RegistrationType.TestCenter.GetEnumValue().ToString()))
                    {

                        person.PRE_NAME_CODE = tmpPerson.PRE_NAME_CODE;
                        person.NAMES = tmpPerson.NAMES;
                        person.LASTNAME = tmpPerson.LASTNAME;
                        person.SEX = tmpPerson.SEX;
                        person.EMAIL = tmpPerson.EMAIL;
                        person.LOCAL_TELEPHONE = tmpPerson.LOCAL_TELEPHONE;
                        person.TELEPHONE = tmpPerson.TELEPHONE;
                        person.ADDRESS_1 = tmpPerson.ADDRESS_1;
                        person.ADDRESS_2 = tmpPerson.LOCAL_ADDRESS2;
                        person.PROVINCE_CODE = tmpPerson.PROVINCE_CODE;
                        person.AREA_CODE = tmpPerson.AREA_CODE;
                        person.TUMBON_CODE = tmpPerson.TUMBON_CODE;
                        person.ZIP_CODE = tmpPerson.ZIP_CODE;
                        person.UPDATED_BY = "agdoi";
                        person.UPDATED_DATE = DateTime.Now;
                    }
                }
                if (regis != null)
                {
                    regis.STATUS = DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString();
                    if ((tmpPerson.MEMBER_TYPE == DTO.RegistrationType.OIC.GetEnumValue().ToString()) || (tmpPerson.MEMBER_TYPE == DTO.RegistrationType.OICAgent.GetEnumValue().ToString())
                     || (tmpPerson.MEMBER_TYPE == DTO.RegistrationType.OICFinace.GetEnumValue().ToString()) || (tmpPerson.MEMBER_TYPE == DTO.RegistrationType.TestCenter.GetEnumValue().ToString()))
                    {

                        regis.PRE_NAME_CODE = tmpPerson.PRE_NAME_CODE;
                        regis.NAMES = tmpPerson.NAMES;
                        regis.LASTNAME = tmpPerson.LASTNAME;
                        regis.SEX = tmpPerson.SEX;
                        regis.EMAIL = tmpPerson.EMAIL;
                        regis.LOCAL_TELEPHONE = tmpPerson.LOCAL_TELEPHONE;
                        regis.TELEPHONE = tmpPerson.TELEPHONE;
                        regis.ADDRESS_1 = tmpPerson.ADDRESS_1;
                        regis.ADDRESS_2 = tmpPerson.LOCAL_ADDRESS2;
                        regis.PROVINCE_CODE = tmpPerson.PROVINCE_CODE;
                        regis.AREA_CODE = tmpPerson.AREA_CODE;
                        regis.TUMBON_CODE = tmpPerson.TUMBON_CODE;
                        regis.ZIP_CODE = tmpPerson.ZIP_CODE;
                        regis.UPDATED_BY = "agdoi";
                        regis.UPDATED_DATE = DateTime.Now;
                    }
                }
                //ลบข้อมูลเดิมก่อน
                List<AG_IAS_TEMP_ATTACH_FILE> oldList = base.ctx.AG_IAS_TEMP_ATTACH_FILE
                                                                .Where(s => s.REGISTRATION_ID == person.ID)
                                                                .ToList();
                foreach (AG_IAS_TEMP_ATTACH_FILE old in oldList)
                {
                    base.ctx.AG_IAS_TEMP_ATTACH_FILE.DeleteObject(old);
                }
                //เพิ่มข้อมูลใหม่
                foreach (DTO.PersonAttatchFile tmp in tmpFiles)
                {
                    AG_IAS_TEMP_ATTACH_FILE file = new AG_IAS_TEMP_ATTACH_FILE();

                    tmp.MappingToEntity(file);
                    base.ctx.AG_IAS_TEMP_ATTACH_FILE.AddObject(file);


                }

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                res.ResultMessage = true;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_SetPersonTemp", ex);
            }

            return res;
        }

        /// <summary>
        /// EditPerson : MappingToEntity(AG_PERSONAL_T)
        /// </summary>
        /// <param name="tmpPerson"></param>
        /// <returns></returns>
        /// <LastUpdate>30/06/2557</LastUpdate>
        public DTO.ResponseMessage<bool> EditPerson(DTO.PersonTemp tmpPerson)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool> { ResultMessage = false };

            try
            {

                var hisPerson = new AG_IAS_HIST_PERSONAL_T { TRANS_ID = OracleDB.GetGenAutoId() };

                var oldPerson = base.ctx.AG_IAS_PERSONAL_T
                                   .SingleOrDefault(s => s.ID == tmpPerson.ID);
                var user = base.ctx.AG_IAS_USERS.SingleOrDefault(a => a.USER_ID == tmpPerson.ID);

                var resgit = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.ID == tmpPerson.ID);

                var temp = base.ctx.AG_IAS_TEMP_PERSONAL_T.SingleOrDefault(s => s.ID == tmpPerson.ID);

                if (tmpPerson.STATUS == DTO.PersonDataStatus.Approve.GetEnumValue().ToString())
                {
                    tmpPerson.UPDATED_DATE = DateTime.Now;
                    //Update to Person
                    tmpPerson.MappingToEntity(oldPerson);

                    //History Keeping
                    tmpPerson.MappingToEntity(hisPerson);

                    //Update to Regitration  by ming
                    tmpPerson.MappingToEntity(resgit);

                    //Update to tmep
                    tmpPerson.MappingToEntity(temp);

                    //Update to AG_PERSONAL_T > if (!= null){ Update Entity }
                    AG_PERSONAL_T per = base.ctx.AG_PERSONAL_T.FirstOrDefault(idc => idc.ID_CARD_NO == tmpPerson.ID_CARD_NO);
                    if (per != null)
                    {
                        per.ADDRESS1 = tmpPerson.ADDRESS_1;
                        per.ADDRESS2 = tmpPerson.ADDRESS_2;
                        per.ZIPCODE = tmpPerson.ZIP_CODE;
                        per.USER_ID = tmpPerson.UPDATED_BY;
                        per.USER_DATE = tmpPerson.UPDATED_DATE;
                        per.E_MAIL = tmpPerson.EMAIL;

                        tmpPerson.MappingToEntity(per);

                    }

                    //Update to AG_IAS_REGISTRATION_T > if (!= null){ Update Entity }
                    AG_IAS_REGISTRATION_T regisT = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(idc => idc.ID_CARD_NO == tmpPerson.ID_CARD_NO);
                    if (regisT != null)
                    {
                        tmpPerson.MappingToEntity(regisT);

                    }

                    base.ctx.AG_IAS_HIST_PERSONAL_T.AddObject(hisPerson);


                    var attachFiles = ctx.AG_IAS_TEMP_ATTACH_FILE.Where(a => a.REGISTRATION_ID == tmpPerson.ID).ToList();

                    if (attachFiles.Count > 0)
                    {
                        UpdateFileAfterApprove(oldPerson, attachFiles);
                    }

                }
                else
                {

                    oldPerson.STATUS = DTO.PersonDataStatus.NotApprove.GetEnumValue().ToString();
                    oldPerson.APPROVE_RESULT = tmpPerson.APPROVE_RESULT.ToString();
                    oldPerson.UPDATED_DATE = DateTime.Now;
                    oldPerson.APPROVED_BY = tmpPerson.APPROVED_BY.ToString();//by milk


                    oldPerson.MappingToEntity(hisPerson);
                    oldPerson.MappingToEntity(resgit);
                    oldPerson.MappingToEntity(temp);

                    base.ctx.AG_IAS_HIST_PERSONAL_T.AddObject(hisPerson);

                    //var attachFiles = ctx.AG_IAS_TEMP_ATTACH_FILE.Where(a => a.REGISTRATION_ID == tmpPerson.ID).ToList();


                    //if (attachFiles.Count > 0)
                    //{
                    //    UpdateFileAfterApprove(attachFiles, oldPerson);
                    //}

                }

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                MailApprovePersonHelper.SendMailApprovePerson(tmpPerson, (user != null) ? user.USER_NAME : "");
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "โปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_EditPerson" + ":" + ex.Message, ex);
            }

            return res;
        }

        private void UpdateFileAfterApprove(AG_IAS_PERSONAL_T person, IList<AG_IAS_TEMP_ATTACH_FILE> tempAttachFiles)
        {

            String pathFile = String.Format(@"{0}\{1}", AttachFileContainer, person.ID_CARD_NO);

            var attachFiles = ctx.AG_IAS_ATTACH_FILE.Where(a => a.REGISTRATION_ID == person.ID).ToList();

            // Delete 
            IList<AG_IAS_TEMP_ATTACH_FILE> deleteAttachFiles = tempAttachFiles.Where(a => a.FILE_STATUS == DTO.AttachFileStatus.D.ToString()).ToList();
            foreach (AG_IAS_TEMP_ATTACH_FILE editAttachFile in deleteAttachFiles)
            {
                AG_IAS_ATTACH_FILE attachFile = attachFiles.Where(a => a.ATTACH_FILE_TYPE == editAttachFile.ATTACH_FILE_TYPE).SingleOrDefault();
                if (attachFile != null)
                {
                    String logFileName = String.Format("{0}_{1}_{2}.{3}", person.ID_CARD_NO,
                                                                            Convert.ToInt32(attachFile.ATTACH_FILE_TYPE).ToString("00"),
                                                                            OracleDB.GetGenAutoId(),
                                                                            GetExtensionFile(attachFile.ATTACH_FILE_PATH));

                    MoveFileResponse response = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                    {
                        CurrentContainer = "",
                        CurrentFileName = attachFile.ATTACH_FILE_PATH,
                        TargetContainer = pathFile,
                        TargetFileName = logFileName
                    }).Action();
                    if (response.Code != "0000") { throw new ApplicationException(Resources.errorPersonService_002); }


                    DeleteFileResponse resDelete = FileManagerService.RemoteFileCommand(
                                    new DeleteFileRequest() { TargetFileName = attachFile.ATTACH_FILE_PATH }).Action();
                    if (resDelete.Code != "0000")
                        throw new ApplicationException(Resources.errorPersonService_003);

                    //attachFile.ATTACH_FILE_PATH = response.TargetFullName;
                    //attachFile.FILE_STATUS=DTO.AttachFileStatus.D.ToString();

                    ctx.AG_IAS_ATTACH_FILE.DeleteObject(attachFile);
                    tempAttachFiles.Remove(editAttachFile);
                }
                // add after delete command
                //if (tempAttachFiles.Where(a => a.ATTACH_FILE_TYPE == editAttachFile.ATTACH_FILE_TYPE 
                //                            && a.FILE_STATUS == DTO.AttachFileStatus.W.ToString()).Count() > 0)
                //{
                //    AG_IAS_TEMP_ATTACH_FILE addAttachFile = tempAttachFiles.Single(a => a.ATTACH_FILE_TYPE == editAttachFile.ATTACH_FILE_TYPE 
                //                                                                    && a.FILE_STATUS == DTO.AttachFileStatus.W.ToString());

                //    String newFileName = String.Format("{0}_{1}.{2}", person.ID_CARD_NO,
                //                                                        Convert.ToInt32(addAttachFile.ATTACH_FILE_TYPE).ToString("00"),
                //                                                        GetExtensionFile(addAttachFile.ATTACH_FILE_PATH));

                //    MoveFileResponse responsetemp = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                //    {
                //        CurrentContainer = "",
                //        CurrentFileName = addAttachFile.ATTACH_FILE_PATH,
                //        TargetContainer = pathFile,
                //        TargetFileName = newFileName
                //    }).Action();

                //    if (responsetemp.Code != "0000")
                //        throw new ApplicationException("ไม่สามารถย้ายข้อมูลได้");



                //    addAttachFile.UPDATED_DATE = DateTime.Now;
                //    //addAttachFile.REMARK = editAttachFile.REMARK;
                //    addAttachFile.ATTACH_FILE_PATH = responsetemp.TargetFullName;
                //    addAttachFile.FILE_STATUS = DTO.AttachFileStatus.A.ToString();
                //    AG_IAS_ATTACH_FILE attach = new AG_IAS_ATTACH_FILE();
                //    addAttachFile.MappingToEntity(attach);
                //    attach.ID = OracleDB.GetGenAutoId();

                //    ctx.AG_IAS_ATTACH_FILE.AddObject(attach);
                //    tempAttachFiles.Remove(addAttachFile);
                //}
            }

            // Add new only
            IList<AG_IAS_TEMP_ATTACH_FILE> addAttachFiles = tempAttachFiles.Where(a => a.FILE_STATUS == DTO.AttachFileStatus.W.ToString()).ToList();
            foreach (AG_IAS_TEMP_ATTACH_FILE addAttach in addAttachFiles)
            {
                AG_IAS_ATTACH_FILE attachFile;
                if (attachFiles.Where(a => a.REGISTRATION_ID == addAttach.REGISTRATION_ID && a.ATTACH_FILE_TYPE == addAttach.ATTACH_FILE_TYPE).Count() > 0)
                {
                    attachFile = attachFiles.Single(a => a.REGISTRATION_ID == addAttach.REGISTRATION_ID && a.ATTACH_FILE_TYPE == addAttach.ATTACH_FILE_TYPE);
                    attachFile.REGISTRATION_ID = addAttach.REGISTRATION_ID;
                    attachFile.REMARK = addAttach.REMARK;
                    DateTime curDate = DateTime.Now;
                    attachFile.UPDATED_DATE = curDate;
                    attachFile.CREATED_DATE = curDate;
                    attachFile.FILE_STATUS = DTO.AttachFileStatus.A.ToString();


                    String logFileName = String.Format("{0}_{1}_{2}.{3}", person.ID_CARD_NO,
                                                                            Convert.ToInt32(attachFile.ATTACH_FILE_TYPE).ToString("00"),
                                                                            OracleDB.GetGenAutoId(),
                                                                            GetExtensionFile(attachFile.ATTACH_FILE_PATH));

                    MoveFileResponse response = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                    {
                        CurrentContainer = "",
                        CurrentFileName = attachFile.ATTACH_FILE_PATH,
                        TargetContainer = pathFile,
                        TargetFileName = logFileName
                    }).Action();
                    if (response.Code != "0000") { throw new ApplicationException(Resources.errorPersonService_002); }


                    DeleteFileResponse resDelete = FileManagerService.RemoteFileCommand(
                                    new DeleteFileRequest() { TargetFileName = attachFile.ATTACH_FILE_PATH }).Action();
                    if (resDelete.Code != "0000")
                        throw new ApplicationException(Resources.errorPersonService_003);

                }
                else
                {
                    attachFile = new AG_IAS_ATTACH_FILE();
                    addAttach.MappingToEntity(attachFile);

                    DateTime curDate = DateTime.Now;
                    attachFile.UPDATED_DATE = curDate;
                    attachFile.CREATED_DATE = curDate;
                    attachFile.FILE_STATUS = DTO.AttachFileStatus.A.ToString();

                }

                String newFileName = String.Format("{0}_{1}.{2}", person.ID_CARD_NO,
                                                                    Convert.ToInt32(addAttach.ATTACH_FILE_TYPE).ToString("00"),
                                                                    GetExtensionFile(addAttach.ATTACH_FILE_PATH));

                MoveFileResponse responsetemp = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                {
                    CurrentContainer = "",
                    CurrentFileName = addAttach.ATTACH_FILE_PATH,
                    TargetContainer = pathFile,
                    TargetFileName = newFileName
                }).Action();

                if (responsetemp.Code != "0000")
                    throw new ApplicationException(Resources.errorPersonService_004);

                attachFile.ATTACH_FILE_PATH = responsetemp.TargetFullName;
                tempAttachFiles.Remove(addAttach);
                if (attachFiles.Where(a => a.REGISTRATION_ID == addAttach.REGISTRATION_ID && a.ATTACH_FILE_TYPE == addAttach.ATTACH_FILE_TYPE).Count() <= 0)
                {
                    ctx.AG_IAS_ATTACH_FILE.AddObject(attachFile);

                }

            }

            // Edit
            IList<AG_IAS_TEMP_ATTACH_FILE> editAttachFiles = tempAttachFiles.Where(a => a.FILE_STATUS == DTO.AttachFileStatus.E.ToString()).ToList();
            foreach (AG_IAS_TEMP_ATTACH_FILE editAttachFile in editAttachFiles)
            {
                AG_IAS_ATTACH_FILE attachFile = attachFiles.Single(a => a.ATTACH_FILE_TYPE == editAttachFile.ATTACH_FILE_TYPE);
                attachFile.REMARK = editAttachFile.REMARK;
                attachFile.UPDATED_DATE = DateTime.Now;
                tempAttachFiles.Remove(editAttachFile);
            }

        }

        private String GetExtensionFile(String fileName)
        {
            String[] files = fileName.Split('.');
            return files[files.Length - 1];
        }
        public DTO.ResponseService<List<DTO.PersonAttatchFile>> GetUserProfileAttatchFileByPersonId(string personId)
        {
            DTO.ResponseService<List<DTO.PersonAttatchFile>> res = new DTO.ResponseService<List<DTO.PersonAttatchFile>>();
            var targetList = new List<DTO.PersonAttatchFile>();
            try
            {
                var person = base.ctx.AG_IAS_PERSONAL_T
                                 .SingleOrDefault(s => s.ID == personId);

                //กรณีข้อมูลมีการแก้ไข
                if (person.STATUS == ((int)DTO.PersonDataStatus.WaitForApprove).ToString())
                {
                    var list = base.ctx.AG_IAS_TEMP_ATTACH_FILE
                              .Where(s => s.REGISTRATION_ID == personId)
                              .ToList();

                    foreach (AG_IAS_TEMP_ATTACH_FILE l in list)
                    {
                        var pa = new DTO.PersonAttatchFile();
                        var ent = ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(w => w.DOCUMENT_CODE == l.ATTACH_FILE_TYPE);
                        l.MappingToEntity(pa);
                        pa.DocumentTypeName = ent.DOCUMENT_NAME;
                        targetList.Add(pa);
                    }

                    var attachs = base.ctx.AG_IAS_ATTACH_FILE.Where(s => s.REGISTRATION_ID == personId).ToList();

                    foreach (AG_IAS_ATTACH_FILE l in attachs)
                    {
                        var pa = new DTO.PersonAttatchFile();
                        var ent = ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(w => w.DOCUMENT_CODE == l.ATTACH_FILE_TYPE);
                        l.MappingToEntity(pa);
                        pa.DocumentTypeName = ent.DOCUMENT_NAME;
                        targetList.Add(pa);
                    }
                }
                else //ข้อมูล User Profile ไม่มีการแก้ไข
                {
                    var list = base.ctx.AG_IAS_ATTACH_FILE
                                  .Where(s => s.REGISTRATION_ID == personId)
                                  .ToList();

                    foreach (AG_IAS_ATTACH_FILE l in list)
                    {
                        var pa = new DTO.PersonAttatchFile();
                        var ent = ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(w => w.DOCUMENT_CODE == l.ATTACH_FILE_TYPE);
                        l.MappingToEntity(pa);
                        pa.DocumentTypeName = ent.DOCUMENT_NAME;
                        targetList.Add(pa);
                    }
                }

                res.DataResponse = targetList;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetUserProfileAttatchFileByPersonId", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.PersonAttatchFile>> GetAttatchFileByPersonId(string personId)
        {
            DTO.ResponseService<List<DTO.PersonAttatchFile>> res = new DTO.ResponseService<List<DTO.PersonAttatchFile>>();
            var targetList = new List<DTO.PersonAttatchFile>();
            try
            {
                var list = base.ctx.AG_IAS_ATTACH_FILE
                              .Where(s => s.REGISTRATION_ID == personId)
                              .ToList();

                foreach (AG_IAS_ATTACH_FILE l in list)
                {
                    var pa = new DTO.PersonAttatchFile();
                    var ent = ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(w => w.DOCUMENT_CODE == l.ATTACH_FILE_TYPE);
                    l.MappingToEntity(pa);
                    pa.DocumentTypeName = ent.DOCUMENT_NAME;
                    targetList.Add(pa);
                }
                res.DataResponse = targetList;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetAttatchFileByPersonId", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.PersonAttatchFile>> GetTempAttatchFileByPersonId(string personId)
        {
            DTO.ResponseService<List<DTO.PersonAttatchFile>> res = new DTO.ResponseService<List<DTO.PersonAttatchFile>>();
            var targetList = new List<DTO.PersonAttatchFile>();
            try
            {
                var list = base.ctx.AG_IAS_TEMP_ATTACH_FILE
                              .Where(s => s.REGISTRATION_ID == personId)
                              .ToList();

                foreach (AG_IAS_TEMP_ATTACH_FILE l in list)
                {
                    var pa = new DTO.PersonAttatchFile();
                    var ent = ctx.AG_IAS_DOCUMENT_TYPE.SingleOrDefault(w => w.DOCUMENT_CODE == l.ATTACH_FILE_TYPE);
                    l.MappingToEntity(pa);
                    pa.DocumentTypeName = ent.DOCUMENT_NAME;
                    targetList.Add(pa);
                }
                res.DataResponse = targetList;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetTempAttatchFileByPersonId", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> PersonApprove(List<string> listId)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            try
            {
                for (int i = 0; i < listId.Count; i++)
                {
                    string Id = listId[i];

                    var tmpPersonal = base.ctx.AG_IAS_TEMP_PERSONAL_T.SingleOrDefault(s => s.ID == Id);


                    var per = base.ctx.AG_IAS_PERSONAL_T
                                 .SingleOrDefault(s => s.ID == Id);
                    if (tmpPersonal != null)
                    {
                        tmpPersonal.MappingToEntity(per);
                    }

                    per.STATUS = DTO.RegistrationStatus.Approve.GetEnumValue().ToString();
                }
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_PersonApprove", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> PersonNotApprove(List<string> listId)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            try
            {
                for (int i = 0; i < listId.Count; i++)
                {
                    string Id = listId[i];
                    var per = base.ctx.AG_IAS_PERSONAL_T
                                 .SingleOrDefault(s => s.ID == Id);
                    per.STATUS = DTO.PersonDataStatus.NotApprove.GetEnumValue().ToString();
                }
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_PersonNotApprove", ex);
            }

            return res;
        }

        //ตรวจสอบแก้ไขรวม
        public DTO.ResponseMessage<bool> PersonEditApprove(List<string> listId, string appresult, string userid)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>() { ResultMessage = false }; ;

            try
            {
                for (int i = 0; i < listId.Count; i++)
                {
                    string Id = listId[i];
                    var hisPerson = new AG_IAS_HIST_PERSONAL_T { TRANS_ID = OracleDB.GetGenAutoId() };
                    var oldPerson = base.ctx.AG_IAS_PERSONAL_T.SingleOrDefault(s => s.ID == Id);
                    var resgit = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(s => s.ID == Id);
                    //var tempper =  base.ctx.AG_IAS_TEMP_PERSONAL_T.SingleOrDefault(s=>s.ID==Id);
                    var temp = base.ctx.AG_IAS_TEMP_PERSONAL_T.SingleOrDefault(s => s.ID == Id);
                    var tmpPerson = new DTO.PersonTemp();


                    if (temp != null)
                    {
                        temp.STATUS = DTO.PersonDataStatus.Approve.GetEnumValue().ToString();
                        temp.APPROVE_RESULT = appresult;
                        temp.UPDATED_DATE = DateTime.Now;
                        temp.APPROVED_BY = userid;

                        //Update to Person
                        temp.MappingToEntity(oldPerson);

                        //History Keeping
                        temp.MappingToEntity(hisPerson);


                        //Update to Regitration  
                        temp.MappingToEntity(resgit);

                        //Update temp
                        temp.MappingToEntity(tmpPerson);


                        base.ctx.AG_IAS_HIST_PERSONAL_T.AddObject(hisPerson);



                        var attachFiles = ctx.AG_IAS_TEMP_ATTACH_FILE.Where(a => a.REGISTRATION_ID == Id).ToList();
                        if (attachFiles.Count > 0)
                        {
                            UpdateFileAfterApprove(oldPerson, attachFiles);
                        }

                    }

                }

                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                    res.ResultMessage = true;
                }




            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_PersonEditApprove", ex);
            }

            return res;


        }

        /// <summary>
        /// ตรวจสอบผู้ใช้ Login
        /// </summary>
        /// <param name="userName">ชื่อผู้ใช้</param>
        /// <param name="password">รหัสผ่าน</param>
        /// <returns>ResponseService<UserProfile></returns>
        public DTO.ResponseService<DTO.UserProfile> Authentication(string userName, string password, bool IsOIC, string Ip)
        {
            DTO.ResponseService<DTO.UserProfile> res = new DTO.ResponseService<DTO.UserProfile>();

            try
            {
                bool isEmail = Regex.IsMatch(userName.ToLower(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
                bool isIdCard = Regex.IsMatch(userName, @"^[0-9]*$");

                //กรณีเป็นพนักงานคปภ.
                if (IsOIC && !isEmail && !isIdCard)
                {
                    #region Old Authentication with OICAD
                    //var resCheck = this.IsRightUserOIC(userName, password);

                    //if (!resCheck.ResultMessage)
                    //{
                    //    res.ErrorMsg = Resources.errorPersonService_005;
                    //    return res;
                    //}
                    #endregion

                    #region New Authentication with OICADService
                    DTO.ResponseService<DTO.OICADProperties> resADServiceAuth = this.OICAuthenWithADService(userName, password);
                    if (!resADServiceAuth.DataResponse.Result.Equals("SUCCESS"))
                    {
                        //AD Failed
                        res.ErrorMsg = Resources.errorPersonService_005;
                        return res;
                    }

                    //Create Log Detail for saving
                    string logDetails = this.CreateAuthLogDetail(resADServiceAuth);
                    LoggerFactory.CreateLog().LogInfo("PersonService_Authentication_OICAuthenWithADService" + logDetails, resADServiceAuth.DataResponse);

                    #endregion


                    AG_IAS_USERS ent = base.ctx.AG_IAS_USERS.SingleOrDefault(s => s.USER_NAME == userName);

                    if (ent != null)
                    {
                        //ตรวจสอบ Login Status
                        //STATUS == null : ยังไม่เคย Login เข้าระบบ
                        //if (ent.STATUS != null)
                        //{
                        //    string curStatus = ent.STATUS;
                        //    //STATUS = 1, APP_CLOSED = 1
                        //    if (curStatus == IAS.DTO.LoginStatus.OnLine.GetEnumValue().ToString())
                        //    {
                        //        res.ErrorMsg = Resources.errorPersonService_006 + ent.USER_NAME + Resources.errorPersonService_007;
                        //        return res;
                        //    }

                        //}

                        //ควรประกาศตัวแปรTypeเดียวกัน AG_IAS_PERSONAL_T
                        AG_IAS_PERSONAL_T member = base.ctx.AG_IAS_PERSONAL_T.SingleOrDefault(w => w.ID == ent.USER_ID && w.MEMBER_TYPE == ent.MEMBER_TYPE);

                        // กรณี เป็น account ที่ยังไม่ได้อนุมัติ จะไม่มี Personal
                        if (member == null && ent.IS_APPROVE == "N")
                        {
                            AG_IAS_REGISTRATION_T regis = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(w => w.ID == ent.USER_ID);

                            DTO.UserProfile profile_unApprove = new DTO.UserProfile
                            {
                                Id = ent.USER_NAME,
                                //Name = member.NAMES + " " + member.,
                                MemberType = ent.USER_TYPE.ToInt(),
                                //IdCard = member.ID_CARD_NO,
                                //CompCode = member.COMP_CODE,
                                LoginName = userName,
                                IS_APPROVE = ent.IS_APPROVE,
                                STATUS = (regis != null) ? regis.STATUS : ((int)DTO.RegistrationStatus.WaitForApprove).ToString(),
                                CanAccessSystem = new List<DTO.FunctionMenu>(),
                                AgentType = regis.AGENT_TYPE,
                                LoginStatus = ent.STATUS,

                                DepartmentCode = resADServiceAuth.DataResponse.DepartmentCode,
                                DepartmentName = resADServiceAuth.DataResponse.DepartmentName,
                                EmployeeCode = resADServiceAuth.DataResponse.EmployeeCode,
                                EmployeeName = resADServiceAuth.DataResponse.EmployeeName,
                                PositionCode = resADServiceAuth.DataResponse.PositionCode,
                                PositionName = resADServiceAuth.DataResponse.PositionName

                            };
                            res.DataResponse = profile_unApprove;
                            return res;
                        }

                        DTO.UserProfile profile = new DTO.UserProfile
                        {
                            Id = ent.USER_ID,
                            Name = member.NAMES,
                            LastName = member.LASTNAME,
                            MemberType = ent.USER_TYPE.ToInt(),
                            IdCard = member.ID_CARD_NO,
                            CompCode = member.COMP_CODE,
                            OIC_User_Id = ent.USER_NAME,
                            OIC_User_Type = ent.OIC_TYPE,
                            OIC_EMP_NO = ent.OIC_EMP_NO,
                            IS_APPROVE = ent.IS_APPROVE,
                            CanAccessSystem = new List<DTO.FunctionMenu>(),
                            LoginName = userName,
                            STATUS = member.STATUS,
                            AgentType = member.AGENT_TYPE,
                            LoginStatus = ent.STATUS,

                            DepartmentCode = resADServiceAuth.DataResponse.DepartmentCode,
                            DepartmentName = resADServiceAuth.DataResponse.DepartmentName,
                            EmployeeCode = resADServiceAuth.DataResponse.EmployeeCode,
                            EmployeeName = resADServiceAuth.DataResponse.EmployeeName,
                            PositionCode = resADServiceAuth.DataResponse.PositionCode,
                            PositionName = resADServiceAuth.DataResponse.PositionName
                        };

                        var userRight = (from r in base.ctx.AG_IAS_FUNCTION_R
                                         join u in base.ctx.AG_IAS_USERS_RIGHT
                                         on r.FUNCTION_ID equals u.FUNCTION_ID
                                         where (u.USER_RIGHT == ent.USER_RIGHT)
                                         select new { r.FUNCTION_ID, r.FUNCTION_URI });

                        foreach (var user in userRight)
                        {
                            profile.CanAccessSystem.Add(new DTO.FunctionMenu
                            {
                                FunctionId = user.FUNCTION_ID,
                                FunctionName = user.FUNCTION_URI
                            });
                        }
                        res.DataResponse = profile;

                        var biz = new DataCenter.DataCenterService();
                        biz.InsertLogOpenMenu(ent.USER_ID, "Login");

                        //Login Success and then set Logon, LogOff
                        //this.SetOnLineStatus(ent.USER_NAME, ent.USER_TYPE,Ip);
                    }
                    else
                    {
                        res.ErrorMsg = Resources.errorPersonService_008 + userName + Resources.errorPersonService_009;
                    }
                }
                else
                {
                    if (!IsOIC && (isEmail || isIdCard))
                    {
                        string encryptPassword = Utils.EncryptSHA256.Encrypt(password);

                        AG_IAS_USERS ent = base.ctx.AG_IAS_USERS
                                      .SingleOrDefault(s => s.USER_NAME == userName && s.USER_PASS == encryptPassword && s.IS_ACTIVE != "C"); // &&
                        //s.USER_PASS == encryptPassword);
                        if (ent != null)
                        {
                            if (ent.IS_ACTIVE == "I")
                            {
                                res.ErrorMsg = "บัญชีผู้ใช้ถูกระงับชั่วคราว";
                                return res;
                            }
                            else if (ent.IS_ACTIVE == "D")
                            {
                                res.ErrorMsg = "บัญชีผู้ใช้ถูกยกเลิกการใช้งาน";
                                return res;
                            }
                            //ตรวจสอบ Login Status
                            //STATUS == null : ยังไม่เคย Login เข้าระบบ
                            //if (ent.STATUS != null)
                            //{
                            //    string curStatus = ent.STATUS;
                            //    if (curStatus == IAS.DTO.LoginStatus.OnLine.GetEnumValue().ToString())
                            //    {
                            //        res.ErrorMsg = Resources.errorPersonService_010 + ent.USER_NAME + Resources.errorPersonService_007;
                            //        return res;
                            //    }
                            //}

                            AG_IAS_PERSONAL_T member = base.ctx.AG_IAS_PERSONAL_T.SingleOrDefault(w => w.ID == ent.USER_ID);

                            // กรณี เป็น account ที่ยังไม่ได้อนุมัติ จะไม่มี Personal
                            if (member == null && ent.IS_APPROVE == "N")
                            {
                                AG_IAS_REGISTRATION_T regis = base.ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(w => w.ID == ent.USER_ID);

                                DTO.UserProfile profile_unApprove = new DTO.UserProfile
                                {
                                    //Edit @23/8/56
                                    Id = ent.USER_ID,
                                    //Name = member.NAMES + " " + member.LASTNAME,
                                    MemberType = ent.USER_TYPE.ToInt(),
                                    //IdCard = member.ID_CARD_NO,
                                    //CompCode = member.COMP_CODE,
                                    IS_APPROVE = ent.IS_APPROVE,
                                    LoginName = userName,
                                    STATUS = (regis != null) ? regis.STATUS : ((int)DTO.RegistrationStatus.WaitForApprove).ToString(),
                                    CanAccessSystem = new List<DTO.FunctionMenu>(),
                                    AgentType = regis.AGENT_TYPE,
                                    LoginStatus = ent.STATUS
                                };
                                res.DataResponse = profile_unApprove;
                                return res;
                            }



                            DTO.UserProfile profile = new DTO.UserProfile
                            {
                                Id = ent.USER_ID,
                                Name = member.NAMES + " " + member.LASTNAME,
                                MemberType = ent.USER_TYPE.ToInt(),
                                IdCard = member.ID_CARD_NO,
                                CompCode = member.COMP_CODE,
                                IS_APPROVE = ent.IS_APPROVE,
                                LoginName = userName,
                                STATUS = member.STATUS,
                                CanAccessSystem = new List<DTO.FunctionMenu>(),
                                AgentType = member.AGENT_TYPE,
                                LoginStatus = ent.STATUS
                            };

                            if (Convert.ToInt32(ent.MEMBER_TYPE) == (int)DTO.RegistrationType.TestCenter && ent.RESET_TIMES == null)
                            {
                                AG_IAS_APPROVE_CONFIG changePasswordFirstTimeConfig = ctx.AG_IAS_APPROVE_CONFIG.SingleOrDefault(a => a.ID == "05");
                                if (changePasswordFirstTimeConfig != null)
                                {
                                    profile.STATUS = "99";
                                }
                            }

                            var userRight = (from r in base.ctx.AG_IAS_FUNCTION_R
                                             join u in base.ctx.AG_IAS_USERS_RIGHT
                                             on r.FUNCTION_ID equals u.FUNCTION_ID
                                             where u.USER_RIGHT == ent.USER_RIGHT
                                             select new { r.FUNCTION_ID, r.FUNCTION_URI });

                            foreach (var user in userRight)
                            {
                                profile.CanAccessSystem.Add(new DTO.FunctionMenu
                                {
                                    FunctionId = user.FUNCTION_ID,
                                    FunctionName = user.FUNCTION_URI
                                });
                            }

                            res.DataResponse = profile;
                            var biz = new DataCenter.DataCenterService();
                            biz.InsertLogOpenMenu(ent.USER_ID, "Login");

                            //Login Success and then set Logon, LogOff
                            //this.SetOnLineStatus(ent.USER_NAME, ent.USER_TYPE,Ip);

                        }
                        else
                        {
                            res.ErrorMsg = Resources.errorPersonService_011;
                        }

                    }//ไม่เป็นพนักงาน OIC
                    else
                    {
                        res.ErrorMsg = Resources.errorPersonService_011;
                    }

                } //เป็นพนักงาน คปภ. หรือไม่

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_Authentication", ex);
            }

            return res;
        }

        /// <summary>
        /// ดึงข้อมูลสถิติการ Reset Password ตามเงื่อนไข
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
                GetStatisticResetPassword(string idCard,
                                          string firstName, string lastName)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("P.ID_CARD_NO like '{0}%' AND ", idCard));
                sb.Append(GetCriteria("P.NAMES LIKE '%{0}%' AND ", firstName));
                sb.Append(GetCriteria("P.LASTNAME LIKE '%{0}%' AND ", lastName));

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                string sql = "SELECT	U.USER_NAME, MT.MEMBER_NAME, " +
                             "          TT.PRE_FULL || ' ' || P.NAMES || '  ' || P.LASTNAME FLNAME, " +
                             "          P.ID_CARD_NO,U.RESET_TIMES " +
                             "FROM	    AG_IAS_USERS U, " +
                             "          AG_IAS_PERSONAL_T P, " +
                             "          GB_PREFIX_R TT, " +
                             "          AG_IAS_MEMBER_TYPE MT " +
                             "WHERE     U.USER_ID = P.ID AND " +
                             "          P.PRE_NAME_CODE = TT.PRE_CODE AND " +
                             "          U.RESET_TIMES IS NOT NULL AND " +
                             "          P.MEMBER_TYPE = MT.MEMBER_CODE " + crit +
                             " ORDER BY U.RESET_TIMES  DESC ";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetStatisticResetPassword", ex);
            }
            return res;
        }

        /// <summary>
        /// เพิ่มข้อมูล User ที่เป็น คปภ.
        /// </summary>
        /// <param name="oicUserName">UserName เจ้าหน้าที่ คปภ.</param>
        /// <param name="preNameCode">รหัสคำนำ</param>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <param name="sex">เพศ M=ชาย, F=หญิง</param>
        /// <param name="oicTypeCode">0=Admin, 1=เจ้าหน้าที่ตัวแทน, 2=เจ้าหน้าที่การเงิน</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> InsertOIC(string oicEmpNo, string oicUserName, string preNameCode,
                                                   string firstName, string lastName,
                                                   string sex, string oicTypeCode, byte[] sign)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                string memberType = "";
                if (oicTypeCode == "1")
                {
                    memberType = DTO.RegistrationType.OICAgent.GetEnumValue().ToString();
                }
                if (oicTypeCode == "2")
                {
                    memberType = DTO.RegistrationType.OICFinace.GetEnumValue().ToString();
                }
                if (oicTypeCode == "0")
                {
                    memberType = DTO.RegistrationType.OIC.GetEnumValue().ToString();
                }

                //var entExist = base.ctx.AG_IAS_USERS
                //                       .Where(w => w.USER_NAME == oicUserName &&
                //                                   w.MEMBER_TYPE == memberType)
                //                       .FirstOrDefault();
                var entExist = base.ctx.AG_IAS_USERS
                                      .Where(w => w.USER_NAME == oicUserName)
                                      .FirstOrDefault();
                if (entExist != null)
                {
                    res.ErrorMsg = Resources.errorPersonService_012 + oicUserName + Resources.errorPersonService_013;
                    return res;
                }

                var per = new AG_IAS_PERSONAL_T();
                if (oicTypeCode == "2")
                {
                    per.IMG_SIGN = sign;
                }
                per.ID = OracleDB.GetGenAutoId();
                per.EMPLOYEE_NO = oicEmpNo;
                per.PRE_NAME_CODE = preNameCode;
                per.NAMES = firstName;
                per.LASTNAME = lastName;
                //per.MEMBER_TYPE = DTO.RegistrationType.OIC.GetEnumValue().ToString();
                //per.MEMBER_TYPE = oicTypeCode;
                per.MEMBER_TYPE = memberType;
                per.SEX = sex;
                base.ctx.AG_IAS_PERSONAL_T.AddObject(per);

                var user = new AG_IAS_USERS();
                user.USER_ID = per.ID;
                user.USER_NAME = oicUserName;
                user.MEMBER_TYPE = memberType;

                if (oicTypeCode == "1")
                {
                    user.USER_TYPE = user.USER_RIGHT = DTO.RegistrationType.OICAgent.GetEnumValue().ToString();
                }
                if (oicTypeCode == "2")
                {
                    user.USER_TYPE = user.USER_RIGHT = DTO.RegistrationType.OICFinace.GetEnumValue().ToString();
                }
                if (oicTypeCode == "0")
                {
                    user.USER_TYPE = user.USER_RIGHT = DTO.RegistrationType.OIC.GetEnumValue().ToString();
                }
                //user.USER_TYPE = user.USER_RIGHT = memberType;
                user.OIC_TYPE = oicTypeCode;
                user.OIC_EMP_NO = oicEmpNo;
                user.CREATED_BY = user.UPDATED_BY = "AGDOI";
                user.CREATED_DATE = user.UPDATED_DATE = DateTime.Now;
                user.IS_ACTIVE = "A";
                user.IS_APPROVE = "Y";
                base.ctx.AG_IAS_USERS.AddObject(user);
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_InsertOIC", ex);
            }
            return res;
        }

        /// <summary>
        /// ปรับปรุงข้อมูลข้อมูล User ที่เป็น คปภ.
        /// </summary>
        /// <param name="oicUserName">UserName เจ้าหน้าที่ คปภ.</param>
        /// <param name="preNameCode">รหัสคำนำ</param>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <param name="sex">เพศ M=ชาย, F=หญิง</param>
        /// <param name="oicTypeCode">0=Admin, 1=เจ้าหน้าที่ตัวแทน, 2=เจ้าหน้าที่การเงิน</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateOIC(string userId, string oicUserName, string preNameCode,
                                                   string firstName, string lastName,
                                                   string sex, string memberType, byte[] imgSign)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {

                var entExist = base.ctx.AG_IAS_USERS
                                      .Where(w => w.USER_ID == userId)
                                      .FirstOrDefault();
                if (entExist == null)
                {
                    res.ErrorMsg = Resources.errorPersonService_014 + oicUserName + Resources.errorPersonService_015;
                    return res;
                }


                var personExist = base.ctx.AG_IAS_PERSONAL_T.Where(w => w.ID == userId).FirstOrDefault();
                if (personExist == null)
                {
                    res.ErrorMsg = Resources.errorPersonService_014 + oicUserName + Resources.errorPersonService_015;
                    return res;
                }

                //บันทึกข้อมูลลง HIS
                AG_IAS_HIST_PERSONAL_T HisPerson = new AG_IAS_HIST_PERSONAL_T();
                HisPerson.TRANS_ID = OracleDB.GetGenAutoId();
                HisPerson.PRE_NAME_CODE = personExist.PRE_NAME_CODE;
                HisPerson.NAMES = personExist.NAMES;
                HisPerson.LASTNAME = personExist.LASTNAME;
                HisPerson.SEX = personExist.SEX;
                HisPerson.MEMBER_TYPE = personExist.MEMBER_TYPE;
                HisPerson.IMG_SIGN = personExist.IMG_SIGN;
                ctx.AG_IAS_HIST_PERSONAL_T.AddObject(HisPerson);


                personExist.PRE_NAME_CODE = preNameCode;
                personExist.NAMES = firstName;
                personExist.LASTNAME = lastName;
                personExist.SEX = sex;

                if (personExist.MEMBER_TYPE == "5" && imgSign != null && imgSign.Length != 0)
                {
                    personExist.IMG_SIGN = imgSign;
                }



                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_UpdateOIC", ex);
            }
            return res;
        }

        /// <summary>
        /// ตรวจสอบรหัสพนักงาน คปภ. ในระบบ AD
        /// </summary>
        /// <param name="oicUserName">รหัสพนักงานคปภ. ที่ต้องการตรวจสอบ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> IsRightUserOIC(string oicUserName)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                ADUtil ad = new ADUtil(adPath, userName, password);
                ad.SetFilter(oicUserName);

                res.ResultMessage = (ad.searchResult != null);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_IsRightUserOIC", ex);
            }
            return res;
        }

        /// <summary>
        /// ตรวจสอบรหัสพนักงาน คปภ. ในระบบ AD
        /// </summary>
        /// <param name="oicUserName">รหัสพนักงานคปภ. ที่ต้องการตรวจสอบ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> IsRightUserOIC(string oicUserName, string oicPassword)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {

                ADUtil ad = new ADUtil(adPath, userName, password);
                ad.SetFilter(oicUserName);

                //res.ResultMessage = (ad.searchResult != null);

                res.ResultMessage = IsAuthenUserOIC(ad.searchResult.Path, oicUserName, oicPassword).ResultMessage;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_IsRightUserOIC", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> IsAuthenUserOIC(String userPath, String user, String pass)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                LdapAuthentication ldapAuthen = new LdapAuthentication(userPath);
                res.ResultMessage = ldapAuthen.IsAuthenticated(adDomain, user, pass);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_IsAuthenUserOIC", ex);
            }

            return res;

        }

        /// <summary>
        /// เปลี่ยนรหัสผ่าน
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public DTO.ResponseMessage<bool> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                string _oldPassword = Utils.EncryptSHA256.Encrypt(oldPassword);
                AG_IAS_USERS ent = base.ctx.AG_IAS_USERS
                                  .SingleOrDefault(s => s.USER_ID == userId);

                if (ent == null)
                {
                    res.ErrorMsg = Resources.errorPersonService_016;
                    return res;
                }

                if (ent.USER_PASS != _oldPassword)
                {
                    res.ErrorMsg = Resources.errorPersonService_017;
                    return res;
                }

                string _newPassword = Utils.EncryptSHA256.Encrypt(newPassword);
                ent.USER_PASS = _newPassword;
                //milk

                int Reset_pass_time = (ent.RESET_TIMES != null) ? Convert.ToInt16(ent.RESET_TIMES) : 0;
                if (Reset_pass_time == 0)
                {
                    ent.RESET_TIMES = 1;
                }
                else
                {
                    ent.RESET_TIMES = Reset_pass_time + 1;
                }

                ent.UPDATED_BY = userId;
                ent.UPDATED_DATE = DateTime.Now;

                //milk
                base.ctx.SaveChanges();

                AG_IAS_PERSONAL_T person = ctx.AG_IAS_PERSONAL_T.SingleOrDefault(a => a.ID == ent.USER_ID);
                if (person != null)
                {
                    if (!String.IsNullOrEmpty(person.EMAIL))
                        MailChangePasswordHelper.SendMailChangePasswordRegistration(person, ent.USER_NAME, ent.USER_PASS, newPassword);
                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_ChangePassword", ex);
            }
            return res;
        }

        //milk
        //reset Password times
        public Boolean ChangePasswordTime(string userName)
        {
            Boolean SaveChange = true;

            try
            {

                string sql = "update ag_ias_users set reset_times = reset_times+1 where user_name ='" + userName + "'";
                OracleDB ora = new OracleDB();
                ora.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                SaveChange = false;
                LoggerFactory.CreateLog().Fatal("PersonService_ChangePasswordTime", ex);
            }
            return SaveChange;
        }
        //milk

        public DTO.ResponseMessage<Boolean> RenewPassword(String username, String email, String oldpassword, String newpassword)
        {
            DTO.ResponseMessage<Boolean> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_IAS_USERS entuser = ctx.AG_IAS_USERS.SingleOrDefault(a => a.USER_NAME == username && a.USER_PASS == oldpassword);
                if (entuser == null)
                {
                    res.ErrorMsg = Resources.errorPersonService_018;
                    return res;
                }

                AG_IAS_PERSONAL_T personT = ctx.AG_IAS_PERSONAL_T.SingleOrDefault(a => a.ID == entuser.USER_ID);

                if (personT != null)
                {
                    if (personT.EMAIL != email)
                    {
                        res.ErrorMsg = Resources.errorPersonService_018;
                        return res;
                    }

                    if (entuser.RESET_TIMES == null)
                    {
                        entuser.RESET_TIMES = 1m;
                    }
                    else
                        entuser.RESET_TIMES = (entuser.RESET_TIMES + 1m);


                    String password = Utils.EncryptSHA256.Encrypt(newpassword);
                    entuser.USER_PASS = password;
                    entuser.UPDATED_BY = entuser.USER_ID;
                    entuser.UPDATED_DATE = DateTime.Now;

                    ctx.SaveChanges();



                }
                else
                {
                    AG_IAS_REGISTRATION_T regis = ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(a => a.ID == entuser.USER_ID);
                    if (regis == null)
                    {
                        res.ErrorMsg = Resources.errorPersonService_018;
                        return res;
                    }
                    else
                    {
                        if (regis.EMAIL != email)
                        {
                            res.ErrorMsg = Resources.errorPersonService_018;
                            return res;
                        }

                        if (entuser.RESET_TIMES == null)
                        {
                            entuser.RESET_TIMES = 1m;
                        }
                        else
                            entuser.RESET_TIMES = (entuser.RESET_TIMES + 1m);


                        String password = Utils.EncryptSHA256.Encrypt(newpassword);
                        entuser.USER_PASS = password;
                        entuser.UPDATED_BY = entuser.USER_ID;
                        entuser.UPDATED_DATE = DateTime.Now;

                        ctx.SaveChanges();



                    }
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorPersonService_018;
                LoggerFactory.CreateLog().Fatal("PersonService_RenewPassword", ex);
                return res;

            }

            return res;
        }

        private bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[a-zA-Z0-9@#$%^&*(){}+?,_]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        public DTO.ResponseMessage<Boolean> ForgetPasswordRequest(String username, String email)
        {
            DTO.ResponseMessage<Boolean> res = new DTO.ResponseMessage<bool>();
            RandomPassword pubCreatePass = new RandomPassword();
            try
            {

                AG_IAS_USERS entuser = ctx.AG_IAS_USERS.SingleOrDefault(a => a.USER_NAME == username);
                if (entuser == null)
                {
                    res.ErrorMsg = Resources.errorPersonService_018;
                    return res;
                }

                AG_IAS_PERSONAL_T personT = ctx.AG_IAS_PERSONAL_T.SingleOrDefault(a => a.ID == entuser.USER_ID);

                if (personT != null)
                {
                    if (personT.EMAIL != email)
                    {
                        res.ErrorMsg = Resources.errorPersonService_018;
                        return res;
                    }

                    //Random random = new Random();
                    //String newPassword = random.Next(1, 999999).ToString("000000");
                    //แก้ไข RegularExpression = "a-zA-Z0-9@#$%^&*(){}+?,_" Length = 8-14

                    if (entuser.RESET_TIMES == null)
                    {
                        entuser.RESET_TIMES = 1m;
                    }
                    else
                    {
                        entuser.RESET_TIMES = (entuser.RESET_TIMES + 1m);
                    }

                    String newPassword = pubCreatePass.GeneratePassword(true, true, true, true, 8);

                    String password = Utils.EncryptSHA256.Encrypt(newPassword);
                    entuser.USER_PASS = password;
                    entuser.UPDATED_BY = entuser.USER_ID;
                    entuser.UPDATED_DATE = DateTime.Now;

                    ctx.SaveChanges();

                    MailChangePasswordHelper.SendMailChangePasswordRegistration(personT, entuser.USER_NAME, entuser.USER_PASS, newPassword);

                }
                else
                {
                    AG_IAS_REGISTRATION_T regis = ctx.AG_IAS_REGISTRATION_T.SingleOrDefault(a => a.ID == entuser.USER_ID);
                    if (regis == null)
                    {
                        res.ErrorMsg = Resources.errorPersonService_018;
                        return res;
                    }
                    else
                    {
                        if (regis.EMAIL != email)
                        {
                            res.ErrorMsg = Resources.errorPersonService_018;
                            return res;
                        }

                        //Random random = new Random();
                        //String newPassword = random.Next(1, 999999).ToString("000000");
                        //แก้ไข RegularExpression = "a-zA-Z0-9@#$%^&*(){}+?,_" Length = 8-14
                        if (entuser.RESET_TIMES == null)
                        {
                            entuser.RESET_TIMES = 1m;
                        }
                        else
                        {
                            entuser.RESET_TIMES = (entuser.RESET_TIMES + 1m);
                        }

                        String newPassword = pubCreatePass.GeneratePassword(true, true, true, true, 8);

                        String password = Utils.EncryptSHA256.Encrypt(newPassword);
                        entuser.USER_PASS = password;
                        entuser.UPDATED_BY = entuser.USER_ID;
                        entuser.UPDATED_DATE = DateTime.Now;

                        ctx.SaveChanges();

                        MailChangePasswordHelper.SendMailChangePasswordRegistration(regis, entuser.USER_NAME, entuser.USER_PASS, newPassword);

                    }
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorPersonService_018;
                LoggerFactory.CreateLog().Fatal("PersonService_ForgetPasswordRequest", ex);
                return res;

            }




            return res;
        }


        /// <summary>
        /// ดึงข้อมูลจาก username และ email
        /// </summary>
        /// <param name="userName">ชื่อผู้ใช้ระบบ</param>
        /// <param name="email">อีเมล</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetUserProfileByUsername(string userName, string email)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "SELECT AGP.ID , AGU.USER_ID , AGU.USER_NAME, AGP.EMAIL " +
                             "FROM AG_IAS_PERSONAL_T AGP , AG_IAS_USERS AGU " +
                             "WHERE AGP.ID = AGU.USER_ID AND AGU.USER_NAME  = '" + userName + "' AND " +
                             "AGP.EMAIL = '" + email + "'" +
                             " UNION " +
                             " SELECT AGP.ID , AGU.USER_ID , AGU.USER_NAME, AGP.EMAIL " +
                             " FROM AG_IAS_REGISTRATION_T AGP , AG_IAS_USERS AGU " +
                             "WHERE AGP.ID = AGU.USER_ID AND AGU.USER_NAME  = '" + userName + "' AND " +
                             "AGP.EMAIL = '" + email + "'";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetUserProfileByUsername", ex);
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
        public DTO.ResponseService<DataSet> GetPersonByCriteria(string firstName, string lastName,
                                                                DateTime? starDate, DateTime? toDate,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage, string para)
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
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("NAMES LIKE '%{0}%' AND ", firstName));
                sb.Append(GetCriteria("LASTNAME LIKE '%{0}%' AND ", lastName));
                //sb.Append(GetCriteria("ID_CARD_NO = '{0}%' AND ", IdCard));
                sb.Append(GetCriteria("ID_CARD_NO like '{0}%' AND ", IdCard));
                sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                sb.Append(GetCriteria("EMAIL = '{0}' AND ", email));
                sb.Append(GetCriteria("COMP_CODE = '{0}' AND ", compCode));
                //sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));




                if (status != "0")
                    sb.Append(GetCriteria("STATUS = '{0}' AND ", status));



                if (starDate != null && toDate != null)
                {
                    sb.Append("To_char(A.CREATED_DATE) BETWEEN TO_DATE('" + Convert.ToDateTime(starDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd') AND ");
                }

                string tmp = sb.ToString();



                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;
                if (para == "1")
                {
                    string sql = "SELECT Count(*)rowcount from(SELECT A.ID, A.EMAIL, C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, " +
                                 "       B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, " +
                                 "       S.STATUS_NAME, A.STATUS " +
                                 "FROM AG_IAS_PERSONAL_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S " +
                                 "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND " +
                                 "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) " +
                                 crit + ")A ";

                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);

                    res.DataResponse = ds;

                }
                else if (para == "2")
                {
                    string sql = "SELECT * FROM(SELECT A.ID, A.EMAIL,A.CREATED_DATE, C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, " +
                                 "       B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, " +
                                 "       S.STATUS_NAME, A.STATUS, (SELECT NAMES FROM AG_IAS_PERSONAL_T WHERE ID=A.APPROVED_BY) APPOVED_NAME," +
                                 "  ROW_NUMBER() OVER (ORDER BY A.ID) RUN_NO " +
                                 "FROM AG_IAS_PERSONAL_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S " +
                                 "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND " +
                                 "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) " +
                                  crit + ")A " + critRecNo;

                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);

                    res.DataResponse = ds;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetPersonByCriteria", ex);
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
        public DTO.ResponseService<DataSet> GetPersonByCriteriaAtPage(string firstName, string lastName,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage)
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
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("NAMES LIKE '%{0}%' AND ", firstName));
                sb.Append(GetCriteria("LASTNAME LIKE '%{0}%' AND ", lastName));
                sb.Append(GetCriteria("ID_CARD_NO = '{0}%' AND ", IdCard));
                sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                sb.Append(GetCriteria("EMAIL = '{0}' AND ", email));
                sb.Append(GetCriteria("COMP_CODE = '{0}' AND ", compCode));
                //sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                if (status != "0")
                    sb.Append(GetCriteria("STATUS = '{0}' AND ", status));

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;
                string sql = "SELECT * FROM(SELECT A.ID, A.EMAIL, '' AS OIC, C.PRE_FULL PREFIX, A.NAMES, A.LASTNAME, " +
                             "       B.MEMBER_NAME MEMBER_TYPE, A.ID_CARD_NO, A.TELEPHONE, A.ZIP_CODE, " +
                             "       S.STATUS_NAME, A.STATUS," +
                             "  ROW_NUMBER() OVER (ORDER BY A.ID) RUN_NO " +
                             "FROM AG_IAS_PERSONAL_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S " +
                             "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND A.PRE_NAME_CODE = C.PRE_CODE AND " +
                             "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) " +
                             crit + ") A " + critRecNo;

                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetPersonByCriteriaAtPage", ex);
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
        public DTO.ResponseService<DataSet> GetPersonTempEditByCriteria(string firstName, string lastName,
                                                                       DateTime? starDate, DateTime? toDate,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage, string para)
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
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("A.NAMES LIKE '%{0}%' AND ", firstName));
                sb.Append(GetCriteria("A.LASTNAME LIKE '%{0}%' AND ", lastName));
                sb.Append(GetCriteria("A.ID_CARD_NO = '{0}%' AND ", IdCard));
                sb.Append(GetCriteria("A.MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                sb.Append(GetCriteria("A.EMAIL = '{0}' AND ", email));
                sb.Append(GetCriteria("A.COMP_CODE = '{0}' AND ", compCode));
                //sb.Append(GetCriteria("MEMBER_TYPE = '{0}' AND ", memberTypeCode));
                sb.Append(GetCriteria("A.STATUS = '{0}' AND ", status));

                //search by startDate and toDate
                if (starDate != null && toDate != null)
                {
                    sb.Append("To_char(A.CREATED_DATE) BETWEEN TO_DATE('" + Convert.ToDateTime(starDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                    Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')  AND ");
                }


                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;
                if (para == "1")
                {
                    string sql = "SELECT Count(*)rowcount from(SELECT D.ID, D.EMAIL, '' AS OIC, C.PRE_FULL PREFIX, D.NAMES, D.LASTNAME, " +
                                 "       B.MEMBER_NAME MEMBER_TYPE, D.ID_CARD_NO, D.TELEPHONE, D.ZIP_CODE, " +
                                 "       S.STATUS_NAME  " +
                                 "FROM AG_IAS_TEMP_PERSONAL_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S,AG_IAS_PERSONAL_T D " +
                                 "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND D.PRE_NAME_CODE = C.PRE_CODE  AND A.ID=d.ID AND" +
                                 "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) " +
                                 crit + ") ";

                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);

                    res.DataResponse = ds;
                }
                else if (para == "2")
                {
                    string sql = "SELECT * FROM(SELECT D.ID, D.EMAIL,D.CREATED_DATE, '' AS OIC, C.PRE_FULL PREFIX, D.NAMES, D.LASTNAME, " +
                               "       B.MEMBER_NAME MEMBER_TYPE, D.ID_CARD_NO, D.TELEPHONE, D.ZIP_CODE, " +
                               "       S.STATUS_NAME,A.STATUS ,(SELECT NAMES FROM AG_IAS_PERSONAL_T WHERE ID=A.APPROVED_BY) APPOVED_NAME," +
                               "  ROW_NUMBER() OVER (ORDER BY A.ID) RUN_NO " +
                               "FROM AG_IAS_TEMP_PERSONAL_T A, AG_IAS_MEMBER_TYPE B, GB_PREFIX_R C, AG_IAS_STATUS S ,AG_IAS_PERSONAL_T D " +
                               "WHERE A.MEMBER_TYPE = B.MEMBER_CODE AND D.PRE_NAME_CODE = C.PRE_CODE AND A.ID=d.ID AND " +
                               "      S.STATUS_CODE = A.STATUS AND ( NOT A.MEMBER_TYPE IN('4','5') ) " +
                                  crit + ")A " + critRecNo;

                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);

                    res.DataResponse = ds;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetPersonTempEditByCriteria", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.Registration> getPDetailByIDCard(string idCard)
        {
            var res = new DTO.ResponseService<DTO.Registration>();
            try
            {
                AG_IAS_REGISTRATION_T ent = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(p => p.ID_CARD_NO == idCard);
                if (ent != null)
                {

                    res.DataResponse = new DTO.Registration();
                    ent.MappingToEntity(res.DataResponse);

                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_getPDetailByIDCard", ex);
            }
            return res;


        }

        /// <summary>
        /// AG_IAS_USERS
        /// ตรวจสอบ STATUS = 0
        /// ONLINE STATUS 0=OFFLINE, 1=ONLINE
        /// 
        /// ตรวจสอบ APP_CLOSED = 0
        /// APP_CLOSED 0=ถูกปิดโดย Logout Session, 1=ถูกปิดโดยวิธีการอื่น นอกเหนือจาก Logout Session
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userType"></param>
        private void SetOnLineStatus(string userName, string userType, string Ip)
        {
            AG_IAS_USERS ent = base.ctx.AG_IAS_USERS.FirstOrDefault(ag => ag.USER_NAME == userName &&
                ag.USER_TYPE == userType && ag.IS_ACTIVE == "A");

            if (ent != null)
            {
                //Set Status
                ent.STATUS = "1";
                ent.TIME_LOGIN = DateTime.Now;
                ent.IP_LOGIN = Ip;
                base.ctx.SaveChanges();
            }

        }

        [WebMethod]
        public DTO.ResponseMessage<bool> SetOffLineStatus(string userName)
        {
            var res = new DTO.ResponseMessage<bool>();

            try
            {
                AG_IAS_USERS ent = base.ctx.AG_IAS_USERS.FirstOrDefault(ag => ag.USER_NAME == userName && ag.IS_ACTIVE == "A");

                if (ent != null)
                {
                    //Set Status
                    //IAS.DTO.LoginStatus.OffLine.GetEnumValue().ToString();
                    ent.STATUS = "0";
                    ent.APP_CLOSED = "0";
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
                LoggerFactory.CreateLog().Fatal("PersonService_SetOffLineStatus", ex);
                throw;

            }

            return res;

        }

        public DTO.ResponseService<DTO.Person> GetPersonalDetail(string idCard)
        {
            var res = new DTO.ResponseService<DTO.Person>();
            try
            {
                //AG_PERSONAL_T ent = base.ctx.AG_PERSONAL_T.FirstOrDefault(idc => idc.ID_CARD_NO.Trim() == idCard);
                var result = (from per in base.ctx.AG_PERSONAL_T
                              where per.ID_CARD_NO.Trim() == idCard.Trim()
                              select new DTO.Person
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
                                  ZIP_CODE = per.ZIPCODE,
                                  TELEPHONE = per.TELEPHONE,
                                  LOCAL_ADDRESS1 = per.LOCAL_ADDRESS1,
                                  LOCAL_ADDRESS2 = per.LOCAL_ADDRESS2,
                                  LOCAL_AREA_CODE = per.LOCAL_AREA_CODE,
                                  LOCAL_PROVINCE_CODE = per.LOCAL_PROVINCE_CODE,
                                  LOCAL_ZIPCODE = per.LOCAL_ZIPCODE,
                                  LOCAL_TELEPHONE = per.LOCAL_TELEPHONE,
                                  //USER_ID = per.USER_ID,
                                  //USER_DATE = per.USER_DATE,
                                  //REMARK = per.REMARK,
                                  //UCOM_AGENT_ID = per.UCOM_AGENT_ID,
                                  //UCOM_AGENT_TYPE = per.UCOM_AGENT_TYPE,
                                  //NAME_CHG_DATE = per.NAME_CHG_DATE,
                                  EMAIL = per.E_MAIL,

                              }).FirstOrDefault();

                if (result != null)
                {
                    //DTO.Person newEnt = new DTO.Person();
                    //result.MappingToEntity(newEnt);
                    res.DataResponse = result;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorRegistrationService_nullData;
                LoggerFactory.CreateLog().Fatal("PersonService_GetPersonalDetail", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetOnLineUser(string userName)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            string sql = "SELECT " +
                         "	AIS.USER_NAME, " +
                         "	AIS.TIME_LOGIN, " +
                         "	AIS.USER_TYPE, " +
                         "  AIS.IP_LOGIN " +
                         "FROM " +
                         "	AG_IAS_USERS AIS " +
                         "WHERE " +
                         "	AIS.STATUS = '1' " +
                         "AND AIS.IS_ACTIVE = 'A' ";
            OracleDB db = new OracleDB();
            res.DataResponse = db.GetDataSet(sql);
            return res;
        }

        public DTO.ResponseMessage<bool> SetOffLineAllStatus(string userName)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var result = (from A in base.ctx.AG_IAS_USERS
                              where A.STATUS.Equals("1") ||
                              A.STATUS == null
                              select A).ToList();

                if (result.Count > 0)
                {
                    result.ForEach(s => s.STATUS = "0");

                    base.ctx.SaveChanges();
                    res.ResultMessage = true;
                }

            }
            catch (Exception ex)
            {

            }

            return res;
        }

        //[WebMethod]
        //public void SetOffline(string userName)
        //{
        //    try
        //    {
        //        AG_IAS_USERS ent = base.ctx.AG_IAS_USERS.FirstOrDefault(ag => ag.USER_NAME == userName && ag.IS_ACTIVE == "A");

        //        if (ent != null)
        //        {
        //            //Set Status
        //            //IAS.DTO.LoginStatus.OffLine.GetEnumValue().ToString();
        //            ent.STATUS = "0";
        //            ent.APP_CLOSED = "0";
        //            base.ctx.SaveChanges();
        //            //res.ResultMessage = true;
        //        }
        //        else
        //        {
        //            //res.ResultMessage = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}



        public DTO.ResponseService<DTO.SignatureImg> GetOicPersonSignImg(string id)
        {
            DTO.ResponseService<DTO.SignatureImg> res = new DTO.ResponseService<DTO.SignatureImg>();
            res.DataResponse = new DTO.SignatureImg();
            try
            {
                var per = base.ctx.AG_IAS_PERSONAL_T
                             .SingleOrDefault(s => s.ID == id);

                if (per != null && per.IMG_SIGN != null && per.IMG_SIGN.Length > 0)
                {
                    res.DataResponse.Signture = Convert.ToBase64String(per.IMG_SIGN, 0, per.IMG_SIGN.Length);// ByteArrayHelper.ConvertByteArrayToString(per.IMG_SIGN);
                }
                else
                    res.ErrorMsg = "ไม่พบข้อมูล.";
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PersonService_GetOicPersonSignImg", ex);
            }

            return res;
        }

        /// <summary>
        /// RandomPassword With in role
        /// </summary>
        /// <AUTHOR>Natta</AUTHOR>
        /// <CREATEDATE>22/05/2557</CREATEDATE>
        public class RandomPassword
        {
            public static Random rnd = new Random();
            private const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            private const string LOWER = "abcdefghijklmnopqrstuvwxyz";
            private const string NUMBERS = "0123456789";
            private const string SYMBOLS = "@#$%^&*(){}+?,_";

            public string GeneratePassword(bool useUpper, bool userLower, bool userNumbers, bool useSymbols, int passwordLength)
            {
                System.Text.StringBuilder charPool = new System.Text.StringBuilder();
                System.Text.StringBuilder curPassReq = new System.Text.StringBuilder();
                System.Text.StringBuilder newPass = new System.Text.StringBuilder();
                if (useUpper)
                {
                    //Assign All upper characters for random into newPass
                    charPool.Append(UPPER);
                    //Get one upper character to curPassReq
                    curPassReq.Append(UPPER[rnd.Next(UPPER.Length)]);
                }

                if (userLower)
                {
                    //Assign All lower characters for random into newPass
                    charPool.Append(LOWER);
                    //Get one lower character to curPassReq
                    curPassReq.Append(LOWER[rnd.Next(LOWER.Length)]);
                }

                if (userNumbers)
                {
                    //Assign All numbers for random into newPass
                    charPool.Append(NUMBERS);
                    //Get one number to curPassReq
                    curPassReq.Append(NUMBERS[rnd.Next(NUMBERS.Length)]);
                }

                if (useSymbols)
                {
                    //Assign All symbols character for random into newPass
                    charPool.Append(SYMBOLS);
                    //Get one symbol to curPassReq
                    curPassReq.Append(SYMBOLS[rnd.Next(SYMBOLS.Length)]);
                }

                int max = charPool.Length;

                //Assign curPassReq to newPass and then added equal passwordLength 
                newPass.Append(curPassReq.ToString());
                for (int x = newPass.Length; x < passwordLength; x++)
                {
                    newPass.Append(charPool[rnd.Next(max)]);

                }
                return newPass.ToString();
            }

        }

        public DTO.ResponseMessage<bool> CheckAuthorityEditExam(DTO.UserProfile userProfile, string testingNo, string testingDate)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                if (Convert.ToDateTime(testingDate) > DateTime.Now.Date)
                {
                    // หารอบสอบนั้นๆ
                    AG_EXAM_LICENSE_R exam = base.ctx.AG_EXAM_LICENSE_R.FirstOrDefault(w => w.TESTING_NO == testingNo);
                    if (exam != null)
                    {
                        // หาคนๆนี้ว่ามีอยู่จริงหรือไม่ 
                        AG_IAS_USERS user = base.ctx.AG_IAS_USERS.FirstOrDefault(w => w.USER_ID == userProfile.Id);


                        AG_IAS_ASSOCIATION examOwner = base.ctx.AG_IAS_ASSOCIATION.FirstOrDefault(w => w.ASSOCIATION_CODE == exam.EXAM_OWNER);
                        if (examOwner != null || user.MEMBER_TYPE == "4")
                        {
                            res.ResultMessage = true;
                        }
                        else
                        {
                            res.ErrorMsg = "ไม่พบสิทธิ์ในการเลื่อนรอบสอบ";
                            res.ResultMessage = false;
                        }
                    }
                }
                else
                {
                    res.ErrorMsg = "ไม่สามารถเลื่อนรอบสอบย้อนหลังได้";
                    res.ResultMessage = false;
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public List<string> GetEmailMoveExam(string testingNo)
        {
            List<String> emails = new List<String>();

            AG_EXAM_LICENSE_R exam = base.ctx.AG_EXAM_LICENSE_R.FirstOrDefault(w => w.TESTING_NO == testingNo);
            string examOwner = exam.EXAM_OWNER;

            //******** เก็บ email เจ้าของรอบสอบ ****************/
            List<AG_IAS_PERSONAL_T> lsOwner = base.ctx.AG_IAS_PERSONAL_T.Where(w => w.COMP_CODE == examOwner).ToList();
            foreach (var item in lsOwner)
            {
                if (emails.Where(a => a == item.EMAIL) != null)
                {
                    if (!String.IsNullOrEmpty(item.EMAIL))
                        emails.Add(item.EMAIL);
                }
            }

            //**************  ค้นหา email ผู้สมัครสอบ ******************//
            List<AG_APPLICANT_T> lsInsur = base.ctx.AG_APPLICANT_T.Where(w => w.TESTING_NO == testingNo).ToList();
            foreach (var item in lsInsur)
            {
                IEnumerable<AG_IAS_PERSONAL_T> lsPerso = base.ctx.AG_IAS_PERSONAL_T.Where(a => a.COMP_CODE == item.INSUR_COMP_CODE);

                if (lsPerso != null)
                {
                    foreach (var per in lsPerso)
                        if (emails.FirstOrDefault(a => a == per.EMAIL) == null)
                            emails.Add(per.EMAIL);

                    IEnumerable<AG_IAS_PERSONAL_T> inPerso = base.ctx.AG_IAS_PERSONAL_T.Where(a => a.COMP_CODE == item.INSUR_COMP_CODE);
                    foreach (var inItem in inPerso)
                    {
                        if (emails.FirstOrDefault(a => a == inItem.EMAIL) == null)
                            emails.Add(inItem.EMAIL);
                    }

                    IEnumerable<AG_IAS_PERSONAL_T> uploadPerso = base.ctx.AG_IAS_PERSONAL_T.Where(a => a.COMP_CODE == item.UPLOAD_BY_SESSION);
                    foreach (var uploadItem in uploadPerso)
                    {
                        if (emails.FirstOrDefault(a => a == uploadItem.EMAIL) == null)
                            emails.Add(uploadItem.EMAIL);
                    }

                }
            }
            /*******************************************************/
            return emails;
        }

        /// <summary>
        /// OIC AD Service : dont modify
        /// </summary>
        /// <param name="ADUserName"></param>
        /// <param name="ADPassword"></param>
        /// <returns>DTO.ResponseService<DTO.OICADProperties></returns>
        /// <AUTHOR>NATTA</AUTHOR>
        public DTO.ResponseService<DTO.OICADProperties> OICAuthenWithADService(string ADUserName, string ADPassword)
        {
            var res = new DTO.ResponseService<DTO.OICADProperties>();
            try
            {
                using (ADService.ADServiceAuthenClient ADsvc = new ADService.ADServiceAuthenClient())
                {
                    ADService.LoginResult LoginrResult = new ADService.LoginResult();
                    LoginrResult = ADsvc.Login(ADUserName, ADPassword);

                    res.DataResponse = new DTO.OICADProperties();
                    LoginrResult.MappingToEntity(res.DataResponse);

                }
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("PersonService_OICAuthenWithADService" + " : " + ex.Message.ToString(), ex.Message);
                res.ErrorMsg = "Authentication Error!!. Please Contact Administrator";
            }

            return res;

        }

        private string CreateAuthLogDetail(DTO.ResponseService<DTO.OICADProperties> obj)
        {
            //Create Log Detail for saving
            StringBuilder strLog = new StringBuilder();
            strLog.Append(" : LoginResult:" + obj.DataResponse.Result + "");
            strLog.Append(" | DepartmentCode:" + obj.DataResponse.DepartmentCode + "");
            strLog.Append(" | DepartmentName:" + obj.DataResponse.DepartmentName + "");
            strLog.Append(" | EmployeeCode:" + obj.DataResponse.EmployeeCode + "");
            strLog.Append(" | EmployeeName:" + obj.DataResponse.EmployeeName + "");
            strLog.Append(" | PositionCode:" + obj.DataResponse.PositionCode + "");
            strLog.Append(" | PositionName:" + obj.DataResponse.PositionName + "");
            strLog.Append(" | ExtensionData:" + obj.DataResponse.ExtensionData + "");
            return strLog.ToString();
        }

    }
}



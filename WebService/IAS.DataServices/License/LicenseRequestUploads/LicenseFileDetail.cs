using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.License.LicenseRequestUploads;
using IAS.DTO;
using IAS.Utils;
using System.Text;
using System.IO;
using IAS.DataServices.Properties;
using System.Data;
using System.Configuration;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class LicenseFileDetail : AG_IAS_IMPORT_DETAIL_TEMP
    {


        private IAS.DAL.Interfaces.IIASPersonEntities _ctx;

        private String _sequence;
        private LicenseFileHeader _licenseFileHeader;
        private Boolean IsNotDuplicate = true;
        private AG_IAS_PETITION_TYPE_R _petitionTypeR;
        private IList<AttachFileDetail> _attachFileDetails = new List<AttachFileDetail>();
        public String Sequence { get { return _sequence; } set { _sequence = value; } }
        public string replaceType { get; set; }

        public LicenseFileDetail()
        {

        }


        public LicenseFileDetail(IAS.DAL.Interfaces.IIASPersonEntities ctx)
        {
            this._ctx = ctx;
        }

        public LicenseFileDetail(IAS.DAL.Interfaces.IIASPersonEntities ctx, LicenseFileHeader licenseFileHeader)
        {
            this._ctx = ctx;
            _licenseFileHeader = licenseFileHeader;
        }

        public void AddAttachFileDetail(IEnumerable<AttachFileDetail> attachFiles)
        {


            if (attachFiles != null && attachFiles.Count() > 0)
            {
                foreach (AttachFileDetail item in attachFiles)
                {
                    AG_IAS_DOCUMENT_TYPE docType = LicenseFileHeader.DocumentTypes.SingleOrDefault(a => a.DOCUMENT_CODE == item.FileTypeCode);
                    if (docType == null)
                    {
                        item.Status = "F";
                    }
                    else
                    {
                        item.FileType = docType.DOCUMENT_NAME;
                    }
                    _attachFileDetails.Add(item);
                }
            }



        }



        public IEnumerable<AttachFileDetail> AttachFileDetails { get { return _attachFileDetails; } }

        public LicenseFileHeader LicenseFileHeader { get { return _licenseFileHeader; } }


        public void SetHeader(LicenseFileHeader header)
        {
            this.IMPORT_ID = header.IMPORT_ID;
            _licenseFileHeader = header;
        }

        public AG_IAS_PETITION_TYPE_R PetitionTypeR { get { return _petitionTypeR; } set { _petitionTypeR = value; } }



        private List<BusinessRule> _brokenRules = new List<BusinessRule>();

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }



        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public void DuplicateCitizen()
        {
            if (IsNotDuplicate)
                SetDuplicateCitizen();
        }

        protected void SetDuplicateCitizen()
        {
            _brokenRules.Add(LicenseFileDetailBusinessRules.CITIZEN_ID_DuplicateInFile);
            ERR_MSG += LicenseFileDetailBusinessRules.CITIZEN_ID_DuplicateInFile.Rule + "<br />";
            this.LOAD_STATUS = "F";
            IsNotDuplicate = false;
        }



        protected void Validate()
        {
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            if (IMPORT_ID == 0)
                AddBrokenRule(LicenseFileDetailBusinessRules.IMPORT_ID_Required);

            if (String.IsNullOrEmpty(PETITION_TYPE))
                AddBrokenRule(LicenseFileDetailBusinessRules.PETITION_TYPE_Required);

            if (PetitionTypeR == null)
                AddBrokenRule(LicenseFileDetailBusinessRules.PETITION_TYPE_Required);
            else
            {
                if (PetitionTypeR.PETITION_TYPE_CODE != "11" && PetitionTypeR.PETITION_TYPE_CODE != "18")
                {
                    //chkLicense
                    if (String.IsNullOrEmpty(LICENSE_NO))
                    {
                        AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_NO_Required);
                    }
                    else
                    {
                        //chkLicenseMathId
                        sql = "SELECT lt.LICENSE_NO "
                              + "FROM AG_LICENSE_T lt,AG_AGENT_LICENSE_T ag ,AG_AGENT_LICENSE_PERSON_T ap "
                              + "WHERE lt.LICENSE_NO = '" + LICENSE_NO.Trim() + "' "
                              + "and ((lt.LICENSE_NO = ag.LICENSE_NO and ag.ID_CARD_NO = '" + CITIZEN_ID.Trim() + "') or "
                              + "(lt.LICENSE_NO = ap.LICENSE_NO and ap.ID_CARD_NO = '" + CITIZEN_ID.Trim() + "')) "
                              + "group by lt.LICENSE_NO";

                        DataTable chkMathId = ora.GetDataTable(sql);


                        if (chkMathId.Rows.Count == 0)
                        {
                            AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_NO_NotMath_ID_CARD);
                        }
                        else
                        {
                            if (PetitionTypeR.PETITION_TYPE_CODE == "13")
                            {
                                int renewTime = 0;
                                renewTime = GetRenewTime(LICENSE_NO);
                                renewTime = renewTime + 1;

                                if (renewTime >= 3)
                                {
                                    AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_NO_NotMath_Renew_13);
                                }

                            }
                            else if (PetitionTypeR.PETITION_TYPE_CODE == "14")
                            {
                                int renewTime = 0;
                                renewTime = GetRenewTime(LICENSE_NO);
                                renewTime = renewTime + 1;

                                if (renewTime <= 2)
                                {
                                    AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_NO_NotMath_Renew_14);
                                }
                            }

                            if (PetitionTypeR.PETITION_TYPE_CODE != "17" && PetitionTypeR.PETITION_TYPE_CODE != "18")
                            //&& LicenseFileHeader.LICENSE_TYPE_CODE != "03" && LicenseFileHeader.LICENSE_TYPE_CODE != "04")
                            {

                                sql = "SELECT ID_CARD_NO "
                                              + "FROM AG_AGENT_LICENSE_T  "
                                              + "WHERE LICENSE_NO = '" + LICENSE_NO.Trim() + "' "
                                              + "AND INSURANCE_COMP_CODE = '" + COMP_CODE + "' ";

                                DataTable agent = ora.GetDataTable(sql);

                                if (agent.Rows.Count == 0)
                                {
                                    if (!string.IsNullOrEmpty(COMP_CODE))
                                    {
                                        AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_NO_NotMath_Required);
                                    }
                                }
                            }


                            if (LICENSE_ACTIVE_DATE == null || LICENSE_ACTIVE_DATE == DateTime.MinValue)
                                AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_ACTIVE_DATE_Required);
                            else
                            {
                                if (!String.IsNullOrEmpty(LICENSE_NO))
                                {
                                    DateTime atDate = Convert.ToDateTime(LICENSE_ACTIVE_DATE).AddYears(-543);

                                    sql = "SELECT lt.LICENSE_NO "
                                        + "FROM AG_LICENSE_T lt "
                                        + "WHERE lt.LICENSE_NO = '" + LICENSE_NO.Trim() + "' "
                                        + "AND lt.DATE_LICENSE_ACT = TO_DATE('" + string.Format("{0:dd/MM/yyyy}", atDate) + "','dd/MM/yyyy') ";

                                    DataTable chkActiveDate = ora.GetDataTable(sql);

                                    if (chkActiveDate.Rows.Count == 0)
                                    {
                                        AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_ACTIVE_DATE_Required);
                                    }
                                }
                            }

                            if (LICENSE_EXPIRE_DATE == null || LICENSE_EXPIRE_DATE == DateTime.MinValue)
                                AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_EXPIRE_DATE_Required);
                            else
                            {
                                if (!String.IsNullOrEmpty(LICENSE_NO))
                                {
                                    DateTime exDate = Convert.ToDateTime(LICENSE_EXPIRE_DATE).AddYears(-543);

                                    sql = "SELECT lt.LICENSE_NO "
                                        + "FROM AG_LICENSE_T lt "
                                        + "WHERE lt.LICENSE_NO = '" + LICENSE_NO.Trim() + "' "
                                        + "AND lt.EXPIRE_DATE = TO_DATE('" + string.Format("{0:dd/MM/yyyy}", exDate) + "','dd/MM/yyyy') ";

                                    DataTable chkExpireDate = ora.GetDataTable(sql);

                                    if (chkExpireDate.Rows.Count == 0)
                                    {
                                        AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_EXPIRE_DATE_Required);
                                    }
                                    else
                                    {
                                        if (PetitionTypeR.PETITION_TYPE_CODE == "13" || PetitionTypeR.PETITION_TYPE_CODE == "14")
                                        {
                                            string currDateFormat = String.Format("{0:dd/MM/yyy}", DateTime.Now).ToString();
                                            string ExDateFormat = String.Format("{0:dd/MM/yyyy}", LICENSE_EXPIRE_DATE).ToString();

                                            DateTime currTime = DateTime.Parse(currDateFormat);
                                            DateTime exTime = DateTime.Parse(ExDateFormat);

                                            int dateCompare = DateTime.Compare(exTime, currTime);

                                            if (dateCompare == -1)
                                            {
                                                AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_EXPIRE_DATE_Expire);
                                            }
                                            // check ต่ออายุภายใน 60 วัน
                                            else
                                            {
                                                string strDate = LICENSE_EXPIRE_DATE.ToString();
                                                string diffDate = DateCompare(strDate);

                                                if ((Convert.ToInt32(diffDate) < 0) || !(Convert.ToInt32(diffDate) <= 60))
                                                {
                                                    AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_EXPIRE_DATE_SixtyDay);

                                                }

                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }






                }
                else if (PetitionTypeR.PETITION_TYPE_CODE == "11" || PetitionTypeR.PETITION_TYPE_CODE == "18")
                {
                    if (!String.IsNullOrEmpty(LICENSE_NO))
                        AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_NO_Required);
                    if (LICENSE_ACTIVE_DATE != null || LICENSE_ACTIVE_DATE == DateTime.MinValue)
                        AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_ACTIVE_DATE_Required);

                    if (LICENSE_EXPIRE_DATE != null || LICENSE_EXPIRE_DATE == DateTime.MinValue)
                        AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_EXPIRE_DATE_Required);
                }
            }



            if (String.IsNullOrEmpty(COMP_CODE) && ("17_18").Contains(PetitionTypeR.PETITION_TYPE_CODE))
                AddBrokenRule(LicenseFileDetailBusinessRules.COMP_CODE_Required);


            //ขอรับใบอนุญาตเป็นบริษัทเดิมขอไม่ได้
            if (PetitionTypeR.PETITION_TYPE_CODE == "18")
            {
                sql = "SELECT aal.INSURANCE_COMP_CODE "
                       + "FROM AG_LICENSE_T al,AG_AGENT_LICENSE_T aal  "
                       + "WHERE aal.ID_CARD_NO = '" + CITIZEN_ID.Trim()  + "' "
                       + "AND al.LICENSE_NO = aal.LICENSE_NO "
                       + "AND al.LICENSE_TYPE_CODE = '" + LicenseFileHeader.LICENSE_TYPE_CODE.Trim() + "' ";

                  DataTable agent = ora.GetDataTable(sql);


                  if (agent != null && agent.Rows.Count > 0)
                  {
                      string comAgent = agent.Rows[0]["INSURANCE_COMP_CODE"].ToString();

                      if (comAgent.Trim() == this.COMP_CODE.Trim())
                      {
                          AddBrokenRule(LicenseFileDetailBusinessRules.COMP_CODE_Required_02);
                      }
                  }

            }



            if (String.IsNullOrEmpty(SEQ))
                AddBrokenRule(LicenseFileDetailBusinessRules.SEQ_Required);
            else if (SEQ != Sequence)
                AddBrokenRule(LicenseFileDetailBusinessRules.SEQ_NotOrder);

            //ใบแทนใบอนุญาตเปลี่ยนชื่อสกุลค่าธรรมเนียมเป็น 0
            if (PetitionTypeR.PETITION_TYPE_CODE == "16" && replaceType == DTO.ReplateType.ChangeNameandSur.GetEnumValue().ToString())
            {
                if (LICENSE_FEE == null || LICENSE_FEE != 0)
                    AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_FEE_Required);
            }
            else if (PetitionTypeR.PETITION_TYPE_CODE == "11" && (new[] { "11", "12" }).Contains(LicenseFileHeader.LICENSE_TYPE_CODE))
            {
                if (LICENSE_FEE == null || LICENSE_FEE != 0)
                    AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_FEE_Required);
            }
            else
            {
                if (LICENSE_FEE == null)
                    AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_FEE_Required);

                else
                {
                    sql = "SELECT FEE "
                          + "FROM AG_PETITION_TYPE_R  "
                          + "WHERE PETITION_TYPE_CODE = '" + PetitionTypeR.PETITION_TYPE_CODE + "' ";

                    DataTable dtFree = ora.GetDataTable(sql);


                    if (dtFree != null && dtFree.Rows.Count > 0)
                    {
                        DataRow drFree = dtFree.Rows[0];
                        decimal free = drFree[0].ToDecimal();

                        if (LICENSE_FEE != free)
                        {
                            AddBrokenRule(LicenseFileDetailBusinessRules.LICENSE_FEE_Required);
                        }
                    }
                }
            }



            if (String.IsNullOrEmpty(CITIZEN_ID) || CITIZEN_ID.Length != 13)
                AddBrokenRule(LicenseFileDetailBusinessRules.CITIZEN_ID_Required);
            else if (!Utils.IdCard.Verify(CITIZEN_ID))
            {
                AddBrokenRule(LicenseFileDetailBusinessRules.CITIZEN_ID_Required);
            }


            if (String.IsNullOrEmpty(TITLE_NAME))
                AddBrokenRule(LicenseFileDetailBusinessRules.TITLE_NAME_Required);
            else
            {
                sql = "SELECT PRE_NAME_CODE "
                         + "FROM AG_PERSONAL_T  "
                         + "WHERE ID_CARD_NO = '" + CITIZEN_ID.Trim() + "' ";

                DataTable dtPreNameCode = ora.GetDataTable(sql);
                if (dtPreNameCode != null && dtPreNameCode.Rows.Count > 0)
                {
                    DataRow dr = dtPreNameCode.Rows[0];
                    string perId = dr[0].ToString();

                    sql = "SELECT NAME "
                         + "FROM VW_IAS_TITLE_NAME_PRIORITY  "
                         + "WHERE ID = '" + perId.Trim() + "' ";


                    DataTable dtPerPreName = ora.GetDataTable(sql);

                    if (dtPerPreName != null && dtPerPreName.Rows.Count > 0)
                    {
                        var drPerPreName = dtPerPreName.Rows[0];
                        string perPreName = drPerPreName[0].ToString();

                        if (TITLE_NAME.Trim() != perPreName.Trim())
                        {
                            AddBrokenRule(LicenseFileDetailBusinessRules.TITLE_NAME_Required);
                        }
                    }

                }
                else if (dtPreNameCode.Rows.Count == 0)
                {
                    if (PetitionTypeR.PETITION_TYPE_CODE == "11")
                    {
                        sql = "SELECT ap.PRE_NAME_CODE,ap.TESTING_NO "
                              + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                              + "WHERE ap.ID_CARD_NO = '" + CITIZEN_ID.Trim() + "' "
                              + "and ap.TESTING_NO = ex.TESTING_NO "
                              + "and ex.LICENSE_TYPE_CODE = '" + LicenseFileHeader.LICENSE_TYPE_CODE + "' "
                              + "order by ap.TESTING_NO desc ";

                        DataTable dtAppPreCode = ora.GetDataTable(sql);

                        if (dtAppPreCode != null && dtAppPreCode.Rows.Count > 0)
                        {
                            DataRow drAppPreCode = dtAppPreCode.Rows[0];
                            string appPreId = drAppPreCode["PRE_NAME_CODE"].ToString();

                            sql = "SELECT NAME "
                                + "FROM VW_IAS_TITLE_NAME_PRIORITY  "
                                + "WHERE ID = '" + appPreId.Trim() + "' ";

                            DataTable dtAppPreName = ora.GetDataTable(sql);
                            if (dtAppPreName != null && dtAppPreName.Rows.Count > 0)
                            {
                                var drAppPreName = dtAppPreName.Rows[0];
                                string appPreName = drAppPreName[0].ToString();

                                if (TITLE_NAME.Trim() != appPreName.Trim())
                                {
                                    AddBrokenRule(LicenseFileDetailBusinessRules.TITLE_NAME_Required);
                                }
                            }


                        }
                    }

                }

            }


            if (String.IsNullOrEmpty(NAME))
                AddBrokenRule(LicenseFileDetailBusinessRules.NAME_Required);
            else
            {
                if (replaceType != DTO.ReplateType.ChangeNameandSur.GetEnumValue().ToString())
                {
                    sql = "SELECT NAMES "
                          + "FROM AG_PERSONAL_T  "
                          + "WHERE ID_CARD_NO = '" + CITIZEN_ID.Trim() + "' ";

                    DataTable dtPerName = ora.GetDataTable(sql);


                    if (dtPerName != null && dtPerName.Rows.Count > 0)
                    {
                        DataRow drPerName = dtPerName.Rows[0];
                        string perName = drPerName[0].ToString();

                        if (NAME.Trim() != perName.Trim())
                        {
                            AddBrokenRule(LicenseFileDetailBusinessRules.NAME_Required);
                        }
                    }
                    else if (dtPerName.Rows.Count == 0)
                    {
                        if (PetitionTypeR.PETITION_TYPE_CODE == "11")
                        {
                            sql = "SELECT ap.NAMES,ap.TESTING_NO "
                                  + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                                  + "WHERE ap.ID_CARD_NO = '" + CITIZEN_ID.Trim() + "' "
                                  + "and ap.TESTING_NO = ex.TESTING_NO "
                                  + "and ex.LICENSE_TYPE_CODE = '" + LicenseFileHeader.LICENSE_TYPE_CODE + "' "
                                  + "order by ap.TESTING_NO desc ";

                            DataTable dtAppName = ora.GetDataTable(sql);
                            if (dtAppName != null && dtAppName.Rows.Count > 0)
                            {
                                DataRow drAppName = dtAppName.Rows[0];
                                string appName = drAppName["NAMES"].ToString();

                                if (NAME.Trim() != appName.Trim())
                                {
                                    AddBrokenRule(LicenseFileDetailBusinessRules.NAME_Required);
                                }
                            }
                        }


                    }
                }


            }


            if (String.IsNullOrEmpty(SURNAME))
                AddBrokenRule(LicenseFileDetailBusinessRules.SURNAME_Required);
            else
            {
                if (replaceType != DTO.ReplateType.ChangeNameandSur.GetEnumValue().ToString())
                {
                    sql = "SELECT LASTNAME "
                          + "FROM AG_PERSONAL_T  "
                          + "WHERE ID_CARD_NO = '" + CITIZEN_ID.Trim() + "' ";

                    DataTable dtPerSurName = ora.GetDataTable(sql);
                    if (dtPerSurName != null && dtPerSurName.Rows.Count > 0)
                    {
                        DataRow drPerSurName = dtPerSurName.Rows[0];
                        string perSurName = drPerSurName[0].ToString();

                        if (SURNAME.Trim() != perSurName.Trim())
                        {
                            AddBrokenRule(LicenseFileDetailBusinessRules.SURNAME_Required);
                        }
                    }
                    else if (dtPerSurName.Rows.Count == 0)
                    {
                        if (PetitionTypeR.PETITION_TYPE_CODE == "11")
                        {
                            sql = "SELECT ap.LASTNAME,ap.TESTING_NO "
                                   + "FROM AG_APPLICANT_T ap,AG_EXAM_LICENSE_R ex "
                                   + "WHERE ap.ID_CARD_NO = '" + CITIZEN_ID.Trim() + "' "
                                   + "and ap.TESTING_NO = ex.TESTING_NO "
                                   + "and ex.LICENSE_TYPE_CODE = '" + LicenseFileHeader.LICENSE_TYPE_CODE.Trim() + "' "
                                   + "order by ap.TESTING_NO desc ";

                            DataTable dtAppLastName = ora.GetDataTable(sql);
                            if (dtAppLastName != null && dtAppLastName.Rows.Count > 0)
                            {
                                DataRow drAppLastName = dtAppLastName.Rows[0];
                                string appLastName = drAppLastName["LASTNAME"].ToString();

                                if (SURNAME.Trim() != appLastName.Trim())
                                {
                                    AddBrokenRule(LicenseFileDetailBusinessRules.SURNAME_Required);
                                }
                            }

                        }

                    }
                }
            }

            if (String.IsNullOrEmpty(ADDR1))
                AddBrokenRule(LicenseFileDetailBusinessRules.ADDR1_Required);
            else if (ADDR1.Length > 60)
                ADDR1 = ADDR1.Substring(0, 60);

            //if (String.IsNullOrEmpty(ADDR2)) 
            //    AddBrokenRule(LicenseFileDetailBusinessRules.ADDR2_Required);

            if (String.IsNullOrEmpty(AREA_CODE))
                AddBrokenRule(LicenseFileDetailBusinessRules.AREA_CODE_Required);

            //if (String.IsNullOrEmpty(EMAIL)) 
            //    AddBrokenRule(LicenseFileDetailBusinessRules.EMAIL_Required);

            if (String.IsNullOrEmpty(CUR_ADDR))
                AddBrokenRule(LicenseFileDetailBusinessRules.CUR_ADDR_Required);
            else if (CUR_ADDR.Length > 60)
                CUR_ADDR = CUR_ADDR.Substring(0, 60);

            //if (String.IsNullOrEmpty(TEL_NO)) 
            //    AddBrokenRule(LicenseFileDetailBusinessRules.TEL_NO_Required);

            if (String.IsNullOrEmpty(CUR_AREA_CODE))
                AddBrokenRule(LicenseFileDetailBusinessRules.CUR_AREA_CODE_Required);

            //if (String.IsNullOrEmpty(REMARK)) 
            //    AddBrokenRule(LicenseFileDetailBusinessRules.REMARK_Required);

            //if (String.IsNullOrEmpty(AR_ANSWER)) 
            //    AddBrokenRule(LicenseFileDetailBusinessRules.AR_ANSWER_Required);
            if (!String.IsNullOrEmpty(AR_ANSWER))
            {
                if (AR_ANSWER == "NOTARDATE")
                {
                    AddBrokenRule(LicenseFileDetailBusinessRules.AR_DATE_Required);
                }

            }



            if (PetitionTypeR.PETITION_TYPE_CODE == "17")
            {
                if (String.IsNullOrEmpty(OLD_COMP_CODE))
                    AddBrokenRule(LicenseFileDetailBusinessRules.OLD_COMP_CODE_Required);

                else
                {
                    sql = "SELECT INSURANCE_COMP_CODE "
                      + "FROM AG_AGENT_LICENSE_T  "
                      + "WHERE ID_CARD_NO = '" + CITIZEN_ID.Trim() + "' "
                      + "AND LICENSE_NO = '" + LICENSE_NO.Trim() + "' ";

                    DataTable dtInsurance = ora.GetDataTable(sql);

                    if (dtInsurance != null && dtInsurance.Rows.Count > 0)
                    {
                        DataRow dr = dtInsurance.Rows[0];
                        string oldComCode = dr[0].ToString();


                        if (OLD_COMP_CODE.Trim() != oldComCode.Trim())
                        {
                            AddBrokenRule(LicenseFileDetailBusinessRules.OLD_COMP_CODE_Required);
                        }
                    }

                }
            }
            else
            {
                if (!String.IsNullOrEmpty(OLD_COMP_CODE))
                {
                    if (OLD_COMP_CODE.Length > 4)
                    {
                        AddBrokenRule(LicenseFileDetailBusinessRules.OLD_COMP_CODE_Required);
                    }

                }
            }


            //if (String.IsNullOrEmpty(ERR_MSG)) 
            //    AddBrokenRule(LicenseFileDetailBusinessRules.ERR_MSG_Required);

            //if (String.IsNullOrEmpty(LOAD_STATUS)) 
            //    AddBrokenRule(LicenseFileDetailBusinessRules.LOAD_STATUS_Required);

            if (PetitionTypeR.PETITION_TYPE_CODE == "11")
            {


                var duplicense = (from hl in _ctx.AG_IAS_IMPORT_HEADER_TEMP
                                  join dl in _ctx.AG_IAS_IMPORT_DETAIL_TEMP on hl.IMPORT_ID equals dl.IMPORT_ID
                                  join lt in _ctx.AG_IAS_LICENSE_TYPE_R on hl.LICENSE_TYPE_CODE equals lt.LICENSE_TYPE_CODE
                                  join ld in _ctx.AG_IAS_LICENSE_D on dl.CITIZEN_ID equals ld.ID_CARD_NO
                                  join lh in _ctx.AG_IAS_LICENSE_H on ld.UPLOAD_GROUP_NO equals lh.UPLOAD_GROUP_NO


                                  where dl.CITIZEN_ID == this.CITIZEN_ID
                                  && dl.LOAD_STATUS == "C"
                                  && ld.APPROVED == "Y"
                                  && lh.LICENSE_TYPE_CODE == LicenseFileHeader.LICENSE_TYPE_CODE
                                  && lh.PETITION_TYPE_CODE == LicenseFileHeader.PETTITION_TYPE
                                  && hl.LICENSE_TYPE_CODE == LicenseFileHeader.LICENSE_TYPE_CODE
                                  && hl.PETTITION_TYPE == LicenseFileHeader.PETTITION_TYPE
                                  select lt.LICENSE_TYPE_NAME).FirstOrDefault();

                if (duplicense != null)
                {
                    AddBrokenRule(LicenseFileDetailBusinessRules.CITIZEN_ID_Duplicate);
                }
            }


            if (START_DATE != null)
            {
                if (START_DATE == DateTime.MinValue)
                {
                    AddBrokenRule(LicenseFileDetailBusinessRules.STARDATE_Required);
                }
            }


            if (!String.IsNullOrEmpty(SPECIAL_TYPE_CODE))
            {
                if (START_DATE == null)
                {
                    AddBrokenRule(LicenseFileDetailBusinessRules.STARDATE_Required);
                }
            }

            var waiting = _ctx.AG_IAS_LICENSE_D.Where(w => w.ID_CARD_NO == this.CITIZEN_ID && w.APPROVED == "W").FirstOrDefault();

            if (waiting != null)
            {
                AddBrokenRule(LicenseFileDetailBusinessRules.CITIZEN_ID_Waiting);
            }

            //Validate Attach File Request
            foreach (var item in LicenseFileHeader.DocumentTypeRequests)
            {
                var result = AttachFileDetails.FirstOrDefault(r => r.FileTypeCode == item.DOCUMENT_CODE);

                if (result == null)
                {
                    AG_IAS_DOCUMENT_TYPE attach = LicenseFileHeader.DocumentTypes.FirstOrDefault(a => a.DOCUMENT_CODE == item.DOCUMENT_CODE);
                    AddBrokenRule(new BusinessRule("AttachFileLicenses", String.Format("กรุณาแนบไฟล์ {0}.", (attach != null) ? attach.DOCUMENT_NAME : " ")));

                }
                else
                {
                    //#region checkSize
                    AG_IAS_DOCUMENT_TYPE attach = LicenseFileHeader.DocumentTypes.FirstOrDefault(a => a.DOCUMENT_CODE == item.DOCUMENT_CODE && a.IS_CARD_PIC == "Y");

                    if (attach != null)
                    {
                        string fileName = result.FileName + result.Extension;
                        var contentType = ContentTypeHelper.MimeType(fileName);

                        var tagPicture = new string[] { "image/bmp", "image/gif", "image/jpeg", "image/png", "image/x-png", "image/pjpeg", "image/tiff" };


                        if (!tagPicture.Contains(contentType))
                        {
                            AddBrokenRule(LicenseFileDetailBusinessRules.PICTURE_Formate);
                        }
                        // else
                        //{


                        // string path = result.FullFileName;
                        // System.Drawing.Image img = System.Drawing.Image.FromFile(path);

                        // var inchesWidth = img.Width/img.HorizontalResolution;
                        // var inchesHieght = img.Height/img.VerticalResolution;



                        //if (img.Width != 354 && img.Height != 472)
                        //if (inchesWidth )
                        //{

                        //}
                        //{

                        //    AddBrokenRule(new BusinessRule("AttachFileLicenses", Resources.errorLicenseFileDetail_001));
                        //    AddBrokenRule(new BusinessRule("AttachFileLicenses", Resources.errorLicenseFileDetail_002));
                        //}
                        //}
                    }

                    //#endregion

                }

            }


            //chkSpecialDocument
            foreach (var atFile in AttachFileDetails)
            {
                String imageTypeCode = ConfigurationManager.AppSettings["CODE_ATTACH_PHOTO"].ToString();

                sql = "SELECT DOCUMENT_CODE "
                      + "FROM AG_IAS_DOCUMENT_TYPE  "
                      + "WHERE DOCUMENT_CODE = '" + atFile.FileTypeCode.Trim() + "' "
                      + "AND DOCUMENT_CODE !='" + imageTypeCode.Trim() + "' ";

                DataTable attach = ora.GetDataTable(sql);


                if (attach != null && attach.Rows.Count != 0)
                {
                    string fileName = atFile.FileName + atFile.Extension;
                    var contentType = ContentTypeHelper.MimeType(fileName);

                    var tagDocument = new string[] { "image/bmp", "image/gif", "image/jpeg", "image/png", "image/x-png", "image/pjpeg", "image/tiff","application/msword"
                    ,"application/vnd.openxmlformats-officedocument.wordprocessingml.document","application/pdf"};

                    if (!tagDocument.Contains(contentType))
                    {
                        AddBrokenRule(LicenseFileDetailBusinessRules.DOC_Formate);
                    }
                    else
                    {
                        var Special = _ctx.AG_IAS_DOCUMENT_TYPE.FirstOrDefault(r => r.DOCUMENT_CODE == atFile.FileTypeCode && (r.SPECIAL_TYPE_CODE_EXAM != null || r.SPECIAL_TYPE_CODE_TRAIN != null));

                        if (Special != null)
                        {

                            if (Special.SPECIAL_TYPE_CODE_EXAM != null)
                            {
                                var chkSpecialTemp = _ctx.AG_IAS_SPECIAL_T_TEMP.FirstOrDefault(r => r.SPECIAL_TYPE_CODE == Special.SPECIAL_TYPE_CODE_EXAM.Trim() && r.ID_CARD_NO.Trim() == CITIZEN_ID.Trim() && r.STATUS != "N");

                                if (chkSpecialTemp != null)
                                    AddBrokenRule(new BusinessRule("AttachFileLicenses", String.Format("มีการส่งเอกสาร {0} มาแล้วไม่สามารส่งซ้ำได้.", (Special != null) ? Special.DOCUMENT_NAME : " ")));
                                else
                                {
                                    var chkSpecialExam = _ctx.AG_IAS_EXAM_SPECIAL_T.FirstOrDefault(r => r.SPECIAL_TYPE_CODE == Special.SPECIAL_TYPE_CODE_EXAM.Trim() && r.ID_CARD_NO.Trim() == CITIZEN_ID.Trim());

                                    if (chkSpecialExam != null)
                                        AddBrokenRule(new BusinessRule("AttachFileLicenses", String.Format("มีการส่งเอกสาร {0} มาแล้วไม่สามารส่งซ้ำได้.", (Special != null) ? Special.DOCUMENT_NAME : " ")));
                                }


                                if (Special.SPECIAL_TYPE_CODE_EXAM != SPECIAL_TYPE_CODE)
                                {
                                    AddBrokenRule(LicenseFileDetailBusinessRules.SPECIAL_TYPE_CODE_EXAM_NotFormate);
                                }

                            }
                            else if (Special.SPECIAL_TYPE_CODE_TRAIN != null)
                            {
                                var chkSpecialTemp = _ctx.AG_IAS_SPECIAL_T_TEMP.FirstOrDefault(r => r.SPECIAL_TYPE_CODE == Special.SPECIAL_TYPE_CODE_TRAIN.Trim() && r.ID_CARD_NO.Trim() == CITIZEN_ID.Trim() && r.STATUS != "N");

                                if (chkSpecialTemp != null)
                                {
                                    AddBrokenRule(new BusinessRule("AttachFileLicenses", String.Format("มีการส่งเอกสาร {0} มาแล้วไม่สามารส่งซ้ำได้.", (Special != null) ? Special.DOCUMENT_NAME : " ")));
                                }
                                else
                                {


                                    var docTrainSpecial = GetTrainSpecial();



                                    if (docTrainSpecial.Contains(Special.SPECIAL_TYPE_CODE_TRAIN))
                                    {
                                        var expireDate = _ctx.AG_TRAIN_SPECIAL_T.FirstOrDefault(r => r.SPECIAL_TYPE_CODE.Trim() == Special.SPECIAL_TYPE_CODE_TRAIN.Trim() && r.ID_CARD_NO.Trim() == CITIZEN_ID.Trim());

                                        if (expireDate != null)
                                        {
                                            //หมดอายุนับจาก startdate 5 ปี
                                            DateTime docDate = Convert.ToDateTime(expireDate.START_DATE).AddYears(5);
                                            DateTime currentDate = DateTime.Now;

                                            string docDateFormat = String.Format("{0:dd/MM/yyyy}", docDate).ToString();
                                            string currDateFormat = String.Format("{0:dd/MM/yyy}", currentDate).ToString();


                                            DateTime docTime = DateTime.Parse(docDateFormat);
                                            DateTime currTime = DateTime.Parse(currDateFormat);

                                            int dateCompare = DateTime.Compare(currTime, docTime);

                                            if (dateCompare == 1)
                                                AddBrokenRule(new BusinessRule("AttachFileLicenses", String.Format("เอกสาร {0} ยังไม่หมดอายุไม่สามารถส่งซ้ำได้.", (Special != null) ? Special.DOCUMENT_NAME : " ")));
                                        }

                                    }
                                    else
                                    {
                                        var chkSpecial = _ctx.AG_TRAIN_SPECIAL_T.FirstOrDefault(r => r.SPECIAL_TYPE_CODE.Trim() == Special.SPECIAL_TYPE_CODE_TRAIN.Trim() && r.ID_CARD_NO.Trim() == CITIZEN_ID.Trim());
                                        if (chkSpecial != null)
                                            AddBrokenRule(new BusinessRule("AttachFileLicenses", String.Format("มีการส่งเอกสาร {0} มาแล้วไม่สามารส่งซ้ำได้.", (Special != null) ? Special.DOCUMENT_NAME : " ")));
                                    }

                                    if (Special.SPECIAL_TYPE_CODE_TRAIN != SPECIAL_TYPE_CODE)
                                    {
                                        AddBrokenRule(LicenseFileDetailBusinessRules.SPECIAL_TYPE_CODE_TRAIN_NotFormate);
                                    }
                                }

                            }

                        }

                    }
                }

            }

            //ตรวจสอบเอกสารพิเศษ
            if (!String.IsNullOrEmpty(SPECIAL_TYPE_CODE) && !String.IsNullOrEmpty(START_DATE.ToString()))
            {

                var docType = (from a in _ctx.AG_IAS_DOCUMENT_TYPE
                               where a.SPECIAL_TYPE_CODE_EXAM == SPECIAL_TYPE_CODE
                               || a.SPECIAL_TYPE_CODE_TRAIN == SPECIAL_TYPE_CODE
                               select a.DOCUMENT_CODE).FirstOrDefault();

                if (docType != null)
                {
                    var Special = (from a in AttachFileDetails
                                   where a.FileTypeCode == docType
                                   select a.FileTypeCode).FirstOrDefault();

                    if (Special == null)
                    {
                        AddBrokenRule(LicenseFileDetailBusinessRules.SPECIAL_TYPE_CODE_NotFound);
                    }
                }
            }


        }

        private List<string> GetTrainSpecial()
        {
            var documentTrainSpecial = (from dt in _ctx.AG_IAS_DOCUMENT_TYPE
                                        from ex in _ctx.AG_TRAIN_SPECIAL_R
                                        where
                                        dt.SPECIAL_TYPE_CODE_TRAIN == ex.SPECIAL_TYPE_CODE
                                        && dt.TRAIN_DISCOUNT_STATUS == "Y"
                                        && dt.SPECIAL_TYPE_CODE_TRAIN.StartsWith("3")
                                        && dt.STATUS == "A"
                                        select dt.SPECIAL_TYPE_CODE_TRAIN).ToList();

            return documentTrainSpecial;
        }

        private string DateCompare(string stDate)
        {
            if (stDate != null)
            {
                DateTime expDate = Convert.ToDateTime(stDate);
                DateTime currentDate = DateTime.Now;

                string expDateFormat = String.Format("{0:dd/MM/yyy}", expDate).ToString();
                string currDateFormat = String.Format("{0:dd/MM/yyyy}", currentDate).ToString();

                DateTime expTime = DateTime.Parse(expDateFormat);
                DateTime currTime = DateTime.Parse(currDateFormat);



                TimeSpan diff = expTime - currTime;

                int diffday = diff.TotalDays.ToInt();

                if (diffday <= 60)
                {
                    stDate = Convert.ToString(diffday);
                }
                else
                {
                    int output = diff.Days;
                    stDate = Convert.ToString(output);
                }
            }



            return stDate;
        }


        private int GetRenewTime(string licenseNo)
        {
            var sql = string.Empty;
            OracleDB ora = new OracleDB();

            int reNewTime = 0;

            sql = "SELECT MAX(RENEW_TIME) "
                  + "FROM AG_LICENSE_RENEW_T  "
                  + "WHERE LICENSE_NO = '" + LICENSE_NO.Trim() + "' ";


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


            return reNewTime;
        }









    }
}
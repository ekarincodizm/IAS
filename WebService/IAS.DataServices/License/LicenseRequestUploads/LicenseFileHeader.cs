using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.Utils;
using IAS.DataServices.License.LicenseRequestUploads;
using IAS.DTO;
using System.Text;
using IAS.DataServices.License.Mapper;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class LicenseFileHeader : AG_IAS_IMPORT_HEADER_TEMP
    {
        // ต้องหาวิธี กันกรณีเปลี่ยน id
        private String functionId = DTO.DocFunction.LICENSE_FUNCTION.GetEnumValue().ToString();

        private IAS.DAL.Interfaces.IIASPersonEntities _ctx;
        private IList<LicenseFileDetail> _licenseFileDetails = new List<LicenseFileDetail>();
        private IList<AG_IAS_DOCUMENT_TYPE_T> _documentTypeRequests = new List<AG_IAS_DOCUMENT_TYPE_T>();
        private List<string> _documentTrainSpecial = new List<string>();

        private IList<AG_IAS_DOCUMENT_TYPE> _documentTypes = new List<AG_IAS_DOCUMENT_TYPE>();
        private CompressFileDetail _compressFileDetail = new CompressFileDetail();
        private String _pettitionTypeUserRequest = "";
        private String _licenseTypeUserRequest = "";
        public string replaceType { get; set; }
        private Boolean IsGetRequestDoc = false;
        private Boolean IsGetTypeDoc = false;

        private AG_IAS_LICENSE_TYPE_R _licenseTypeR;
        private AG_IAS_PETITION_TYPE_R _petitionTypeR;
        private String _fileName;
        private DTO.UserProfile _userProfile;

        public LicenseFileHeader() { }
        public LicenseFileHeader(IAS.DAL.Interfaces.IIASPersonEntities ctx, DTO.UserProfile userProfile, String fileName, String pettitionTypeCode, String licenseTypeCode)
        {
            _ctx = ctx;
            String id = OracleDB.GetGenAutoId();
            this.IMPORT_ID = Convert.ToInt64(id);
            //this.LICENSE_TYPE_CODE = licenseTypeCode;
            this.PETTITION_TYPE = pettitionTypeCode;
            this._pettitionTypeUserRequest = pettitionTypeCode;
            this._licenseTypeUserRequest = licenseTypeCode;
            this.FILE_NAME = fileName;
            _userProfile = userProfile;
        }

        public String PettitionTypeCodeRequest { get { return _pettitionTypeUserRequest; } }
        public String LicenseTypeCodeRequest { get { return _licenseTypeUserRequest; } }
        public AG_IAS_LICENSE_TYPE_R LicenseTypeR { get { return _licenseTypeR; } set { _licenseTypeR = value; } }
        public AG_IAS_PETITION_TYPE_R PetitionTypeR { get { return _petitionTypeR; } set { _petitionTypeR = value; } }

        public String FileName { get { return (this.FILE_NAME.LastIndexOf(@"\") > -1) ? this.FILE_NAME.Substring(this.FILE_NAME.LastIndexOf(@"\") + 1) : this.FILE_NAME; } }
        public IEnumerable<LicenseFileDetail> LicenseFileDetails { get { return _licenseFileDetails; } }
        public CompressFileDetail CompressFileDetail { get { return _compressFileDetail; } set { _compressFileDetail = value; } }

        public IEnumerable<AG_IAS_DOCUMENT_TYPE_T> DocumentTypeRequests
        {
            get
            {
                if (!IsGetRequestDoc)
                {
                    _documentTypeRequests = _ctx.AG_IAS_DOCUMENT_TYPE_T.Where(w => w.FUNCTION_ID == functionId && w.LICENSE_TYPE_CODE == LicenseTypeR.LICENSE_TYPE_CODE
                            && w.STATUS == "A" && w.DOCUMENT_REQUIRE == "Y"
                            && w.PETITION_TYPE_CODE == PetitionTypeR.PETITION_TYPE_CODE).ToList();

                    IsGetRequestDoc = true;
                }
                return _documentTypeRequests;
            }
        }

   

        public IEnumerable<AG_IAS_DOCUMENT_TYPE> DocumentTypes
        {
            get
            {
                if (!IsGetTypeDoc)
                {
                    _documentTypes = _ctx.AG_IAS_DOCUMENT_TYPE.Where(w => w.STATUS == "A").ToList();

                    IsGetTypeDoc = true;
                }
                return _documentTypes;
            }
        }
        public void AddDetail(LicenseFileDetail detail, string replateType)
        {
            detail.SetHeader(this);
            detail.IMPORT_ID = this.IMPORT_ID;
            detail.PETITION_TYPE = this.PETTITION_TYPE;
            detail.PetitionTypeR = this.PetitionTypeR;
            detail.COMP_CODE = this.COMP_CODE;
            detail.replaceType = replaceType;
            IEnumerable<AttachFileDetail> attachFiles = CompressFileDetail.AttatchFiles
                                                    .Where(a => a.FileName.Contains(detail.CITIZEN_ID));
            detail.AddAttachFileDetail(attachFiles);

            _licenseFileDetails.Add(detail);
            ValidateDetail(detail);
        }

        private void ValidateDetail(LicenseFileDetail detail)
        {


            //if (detail.GetBrokenRules().Count() > 0)
            //{
            StringBuilder messageError = new StringBuilder("");
            foreach (BusinessRule item in detail.GetBrokenRules())
            {
                messageError.Append(item.Rule + "<br />");
            }
            detail.ERR_MSG = messageError.ToString();
            if (string.IsNullOrEmpty(detail.ERR_MSG))
            {
                detail.LOAD_STATUS = "T";
            }
            else
            {
                detail.LOAD_STATUS = "F";
            }



            //}
            //else
            //{

            //}
        }

        public void ValidCiticenDuplicate()
        {
            foreach (LicenseFileDetail detail in LicenseFileDetails)
            {
                IEnumerable<LicenseFileDetail> licenseDetails = this.LicenseFileDetails.Where(a => a.CITIZEN_ID == detail.CITIZEN_ID);
                if (licenseDetails.Count() > 1)
                {
                    //this.DuplicateCitizen();
                    foreach (LicenseFileDetail licen in licenseDetails)
                    {
                        licen.DuplicateCitizen();
                    }
                }
            }

        }

        public void AddDetail(AG_IAS_IMPORT_DETAIL_TEMP detial)
        {
            LicenseFileDetail detail_A = detial as LicenseFileDetail;
            detail_A.SetHeader(this);
            detail_A.IMPORT_ID = this.IMPORT_ID;
            detail_A.PETITION_TYPE = this.PETTITION_TYPE;
            detail_A.PetitionTypeR = this.PetitionTypeR;
            detail_A.COMP_CODE = this.COMP_CODE;

            ValidateDetail(detail_A);
            _licenseFileDetails.Add(detail_A);
        }



        #region Validate
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

        public SummaryReceiveLicense ValidateDataOfData()
        {


            SummaryReceiveLicense summarize = this.ConvertToSummaryReceiveLicense();

            return summarize;
        }





        protected void Validate()
        {
            if (_userProfile.MemberType == 2)
            {
                VW_IAS_COM_CODE comEnt = _ctx.VW_IAS_COM_CODE.SingleOrDefault(a => a.ID == COMP_CODE);

                if (String.IsNullOrEmpty(COMP_CODE))
                    AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                else if (COMP_CODE != _userProfile.CompCode)
                    AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                else if (comEnt == null)
                    AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);


            }
            else if (_userProfile.MemberType == 3)
            {

                if ((new[] { "03", "04" }).Contains(this.LICENSE_TYPE_CODE))
                {

                    if (!String.IsNullOrEmpty(COMP_CODE))
                    {
                        var comEnt = _ctx.VW_IAS_COM_CODE.SingleOrDefault(a => a.ID == COMP_CODE).ID;

                        if (String.IsNullOrEmpty(comEnt))
                        {
                            AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                        }
                        else
                        {
                            if (!comEnt.StartsWith("3"))
                                AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                        }
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(COMP_CODE))
                    {
                        AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(COMP_CODE))
                        {
                            var comEnt = _ctx.VW_IAS_COM_CODE.SingleOrDefault(a => a.ID == COMP_CODE).ID;

                            if (String.IsNullOrEmpty(comEnt))
                                AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                            else
                            {
                                if ((new[] { "01", "07" }).Contains(this.LICENSE_TYPE_CODE))
                                {
                                    if (!comEnt.StartsWith("1"))
                                    {
                                        AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                                    }
                                }
                                else if ((new[] { "02", "05", "06", "08" }).Contains(this.LICENSE_TYPE_CODE))
                                {
                                    if (!comEnt.StartsWith("2"))
                                    {
                                        AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
                                    }
                                }

                            }
                        }
                    }

                }

            }


            else if (this.COMP_CODE.Length == 4)
            {

                VW_IAS_COM_CODE comEnt = _ctx.VW_IAS_COM_CODE.SingleOrDefault(a => a.ID == COMP_CODE);

            }
            else
            {
                AG_EXAM_PLACE_GROUP_R compEnt = _ctx.AG_EXAM_PLACE_GROUP_R.SingleOrDefault(s => s.EXAM_PLACE_GROUP_CODE == COMP_CODE);
                if (compEnt == null)
                    AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_CODE_Required);
            }

            if (IMPORT_ID == 0)
                AddBrokenRule(LicenseFileHeaderBusinessRules.IMPORT_ID_Required);



            if (String.IsNullOrEmpty(FILE_NAME))
                AddBrokenRule(LicenseFileHeaderBusinessRules.FILE_NAME_Required);

            //if (!String.IsNullOrEmpty(PETTITION_TYPE))
            //    AddBrokenRule(LicenseFileHeaderBusinessRules.PETTITION_TYPE_Required);
            if (PetitionTypeR == null)
                AddBrokenRule(LicenseFileHeaderBusinessRules.PETTITION_TYPE_Required);

            if (String.IsNullOrEmpty(LICENSE_TYPE_CODE))
                AddBrokenRule(LicenseFileHeaderBusinessRules.LICENSE_TYPE_CODE_Required);
            else if (LicenseTypeR == null)
                AddBrokenRule(LicenseFileHeaderBusinessRules.LICENSE_TYPE_Required);
            else if (LICENSE_TYPE_CODE != _licenseTypeUserRequest)
            {
                AddBrokenRule(LicenseFileHeaderBusinessRules.LICENSE_TYPE_CODE_Required);
            }

            if (_userProfile.MemberType == 2)
            {
                if (String.IsNullOrEmpty(COMP_NAME))
                    AddBrokenRule(LicenseFileHeaderBusinessRules.COMP_NAME_Required);
            }


            if (String.IsNullOrEmpty(LICENSE_TYPE))
                AddBrokenRule(LicenseFileHeaderBusinessRules.LICENSE_TYPE_Required);

            if (SEND_DATE == null || SEND_DATE == DateTime.MinValue)
                AddBrokenRule(LicenseFileHeaderBusinessRules.SEND_DATE_Required);
            else if (!(((DateTime)SEND_DATE).Date == DateTime.Now.Date))
                AddBrokenRule(LicenseFileHeaderBusinessRules.SEND_DATE_Required);



            if (TOTAL_AGENT == null || TOTAL_AGENT == 0)
                AddBrokenRule(LicenseFileHeaderBusinessRules.TOTAL_AGENT_Required);
            else if (TOTAL_AGENT != LicenseFileDetails.Count())
                AddBrokenRule(LicenseFileHeaderBusinessRules.TOTAL_AGENT_Required);


            //ใบแทนใบอนุญาตเปลี่ยนชื่อสกุลค่าธรรมเนียมเป็น 0
            if (PetitionTypeR.PETITION_TYPE_CODE == "16" && replaceType == DTO.ReplateType.ChangeNameandSur.GetEnumValue().ToString())
            {
                if (TOTAL_FEE == null || TOTAL_FEE != 0)
                    AddBrokenRule(LicenseFileHeaderBusinessRules.TOTAL_FEE_Required);
            }
            else if (PetitionTypeR.PETITION_TYPE_CODE == "11" && (new[] { "11", "12" }).Contains(this.LICENSE_TYPE_CODE))
            {
                if (TOTAL_FEE == null || TOTAL_FEE != 0)
                    AddBrokenRule(LicenseFileHeaderBusinessRules.TOTAL_FEE_Required);
            }
            else
            {
                if (TOTAL_FEE == null || TOTAL_FEE == 0)
                    AddBrokenRule(LicenseFileHeaderBusinessRules.TOTAL_FEE_Required);


                AG_PETITION_TYPE_R free = _ctx.AG_PETITION_TYPE_R.SingleOrDefault(s => s.PETITION_TYPE_CODE == PetitionTypeR.PETITION_TYPE_CODE);

                Decimal? count = LicenseFileDetails.Count();
                Decimal? total = 0;
                Decimal value = new Decimal();


                foreach (var item in LicenseFileDetails)
                {
                    if (string.IsNullOrEmpty(item.LICENSE_FEE.ToString()) || !Decimal.TryParse(item.LICENSE_FEE.ToString(), out value))
                    {
                        item.LICENSE_FEE = 0;
                    }
                    total += item.LICENSE_FEE;
                }


                if (TOTAL_FEE != total)
                    AddBrokenRule(LicenseFileHeaderBusinessRules.TOTAL_FEE_Required);

            }
            //if (String.IsNullOrEmpty(ERR_MSG)) 
            //    AddBrokenRule(LicenseFileHeaderBusinessRules.ERR_MSG_Required);

            //if (String.IsNullOrEmpty(APPROVE_COMPCODE)) 
            //    AddBrokenRule(LicenseFileHeaderBusinessRules.APPROVE_COMPCODE_Required);



        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.BLL;
using System.Collections.Specialized;
using IAS.Properties;
using System.Text;
using IAS.Utils;
using System.Data;


namespace IAS.License
{
    public partial class LicenseCheck : basepage
    {

        #region Private & Public Session
        public bool PaymentStatus
        {
            get
            {
                return (bool)Session["paymentstatus"] == false ? false : (bool)Session["paymentstatus"];
            }
            set
            {
                Session["paymentstatus"] = value;
            }
        }

        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }

        public List<string> ListUploadGroupNo
        {
            get
            {
                return (List<string>)Session["listuploadgroupno"] == null ? null : (List<string>)Session["listuploadgroupno"];
            }
            set
            {
                Session["listuploadgroupno"] = value;
            }

        }
        public List<string> ListPaymentLicense
        {
            get
            {
                if (Session["ListPaymentLicense"] == null)
                {
                    Session["ListPaymentLicense"] = new List<string>();
                }

                return (List<string>)Session["ListPaymentLicense"];
            }

            set
            {
                Session["ListPaymentLicense"] = value;
            }
        }

        public List<DTO.PersonLicenseTransaction> SessionListDT
        {
            get
            {
                return (List<DTO.PersonLicenseTransaction>)Session["SessionListDT"] == null ? null : (List<DTO.PersonLicenseTransaction>)Session["SessionListDT"];
            }
            set
            {
                Session["SessionListDT"] = value;
            }
        }

        public List<DTO.PersonLicenseTransaction> CurrentSessionListDT
        {
            get
            {
                return (List<DTO.PersonLicenseTransaction>)Session["CurrentSessionListDT"] == null ? null : (List<DTO.PersonLicenseTransaction>)Session["CurrentSessionListDT"];
            }
            set
            {
                Session["CurrentSessionListDT"] = value;
            }
        }

        public List<bool> CheckList
        {
            get
            {
                return (List<bool>)Session["CheckList"] == null ? null : (List<bool>)Session["CheckList"];
            }
            set
            {
                Session["CheckList"] = value;
            }
        }
        public List<string> ListPrintPayment
        {
            get
            {
                if (Session["ListPrintPayment"] == null)
                {
                    Session["ListPrintPayment"] = new List<string>();
                }

                return (List<string>)Session["ListPrintPayment"];
            }

            set
            {
                Session["ListPrintPayment"] = value;
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.HasPermit();
                MenuInit();

                if (Session["CheckClearSession"] == null)
                {
                    ClearSessionLicense();
                }
                else
                {
                    Session["CheckClearSession"] = null;
                }
            }

        }
        #endregion

        #region Public & Private Function
        private void MenuInit()
        {
            this.MasterLicense.ListUploadGroupNo = new List<string>();
            GetAllLicense();
            GetLicenseY();
            //GetLicenseW();

        }

        protected void ClearSessionLicense()
        {
            Session["lsLicensePayment"] = null;
            Session["personlicenseh"] = null;
            Session["personlicensed"] = null;
            Session["attachfiles"] = null;
        }

        /// <summary>
        /// GetAllLicense : IF Fee > 0 : PETITION_TYPE_CODE : ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)
        /// </summary>
        /// <Author>Natta</Author>
        /// <LastUpdate>30/06/2557</LastUpdate>
        private void GetAllLicense()
        {
            LicenseBiz biz = new LicenseBiz();

            DTO.ResponseService<DTO.PersonLicenseTransaction[]> resAll = biz.getViewPersonLicense(this.MasterLicense.UserProfile.IdCard, "A");
            if (resAll.DataResponse != null)
            {
                resAll.DataResponse.ToList().ForEach(lic =>
                {
                    if (lic.FEES == 0)
                    {
                        lic.PETITION_TYPE_CODE = "ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)";
                    }
                });

                this.ucLicensePaymentAll.lblGvHead.Text = Resources.propLicenseCheck_LicensePaymentAll;
                this.ucLicensePaymentAll.GridPayment.DataSource = resAll.DataResponse;
                this.ucLicensePaymentAll.GridPayment.DataBind();
            }
            else
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = resAll.ErrorMsg;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();

            }

            
        }

        /// <summary>
        /// GetLicenseY : Where Fee > 0 : Not show
        /// </summary>
        /// <Author>Natta</Author>
        /// <LastUpdate>30/06/2557</LastUpdate>
        private void GetLicenseY()
        {
            LicenseBiz biz = new LicenseBiz();
            DTO.ResponseService<DTO.PersonLicenseTransaction[]> resY = biz.getViewPersonLicense(this.MasterLicense.UserProfile.IdCard, "Y");
            if (resY.DataResponse != null)
            {
                List<DTO.PersonLicenseTransaction> lsPerLicenseY = new List<DTO.PersonLicenseTransaction>();
                resY.DataResponse.ToList().ForEach(lic =>
                    {
                        if (lic.FEES > 0)
                        {
                            DTO.PersonLicenseTransaction ent = new DTO.PersonLicenseTransaction();
                            lic.MappingToEntity<DTO.PersonLicenseTransaction>(ent);
                            lsPerLicenseY.Add(ent);
                        }
                    });

                if (resY.DataResponse.Count() <= 0)
                {
                    this.pnlSort.Enabled = false;
                    this.pnlSort.Visible = false;
                    this.pnlY.Enabled = false;
                    this.ucLicensePaymentY.lblGvHead.Text = Resources.propLicenseCheck_LicensePaymentY;
                    //this.ucLicensePaymentY.GridPayment.DataSource = resY.DataResponse;
                    this.ucLicensePaymentY.GridPayment.DataSource = lsPerLicenseY;
                    this.ucLicensePaymentY.GridPayment.DataBind();
                }
                else if (resY.DataResponse.Count() > 0)
                {
                    this.pnlSort.Enabled = true;
                    this.pnlSort.Visible = true;
                    this.pnlY.Enabled = true;
                    this.ucLicensePaymentY.lblGvHead.Text = Resources.propLicenseCheck_LicensePaymentY;
                    //this.ucLicensePaymentY.GridPayment.DataSource = resY.DataResponse;
                    this.ucLicensePaymentY.GridPayment.DataSource = lsPerLicenseY;
                    this.ucLicensePaymentY.GridPayment.DataBind();
                }
                
            }
            else if((resY.DataResponse == null) || (resY.IsError))
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = resY.ErrorMsg;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();

            }
        }

        private List<DTO.PersonLicenseTransaction> CreateIndex(List<DTO.PersonLicenseTransaction> ls)
        {
            List<DTO.PersonLicenseTransaction> dynamicList = new List<DTO.PersonLicenseTransaction>();
            if (ls.Count > 0)
            {
                var lsWithIndex = ls.Select((col, index) => new
                {
                    RUN_NO = index + 1,
                    SEQ_NO = col.SEQ_NO,
                    UPLOAD_GROUP_NO = col.UPLOAD_GROUP_NO,
                    HEAD_REQUEST_NO = col.HEAD_REQUEST_NO,
                    FEES = col.FEES,
                    APPROVED_DOC = col.APPROVED_DOC,
                    PETITION_TYPE_CODE = col.PETITION_TYPE_CODE,
                    TRAN_DATE = col.TRAN_DATE,


                }).ToList();

                for (int i = 0; i < lsWithIndex.Count; i++)
                {
                    dynamicList.Add(new DTO.PersonLicenseTransaction
                    {

                        RUN_NO = Convert.ToString(lsWithIndex[i].RUN_NO),
                        SEQ_NO = lsWithIndex[i].SEQ_NO,
                        UPLOAD_GROUP_NO = lsWithIndex[i].UPLOAD_GROUP_NO,
                        HEAD_REQUEST_NO = lsWithIndex[i].HEAD_REQUEST_NO,
                        FEES = lsWithIndex[i].FEES,
                        APPROVED_DOC = lsWithIndex[i].APPROVED_DOC,
                        PETITION_TYPE_CODE = lsWithIndex[i].PETITION_TYPE_CODE,
                        TRAN_DATE = lsWithIndex[i].TRAN_DATE
                    });

                }
            }

            this.LicenseNameConvertor(dynamicList);

            return dynamicList;
        }


        #endregion

        #region UI

        protected void gvLicenseD_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex;

            this.CurrentSessionListDT = new Class.InvoiceSortDescriptions().SortDescriptionsDT(this.SessionListDT, CurrentIndex, e.CommandName);

            //Rebind
            this.gvLicenseD.DataSource = this.CurrentSessionListDT;
            this.gvLicenseD.DataBind();
            this.mdlLicenseD.Show();

        }

        protected void gvLicenseD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblGroupNo = (Label)e.Row.FindControl("lblGroupNo");
                var LBUP = (LinkButton)e.Row.FindControl("LBUP");
                var LBDown = (LinkButton)e.Row.FindControl("LBDown");

                Label lblApproveDoc = (Label)e.Row.FindControl("lblApproveDoc");
                Image imgCorrect = (Image)e.Row.FindControl("imgCorrect");
                
                if ((imgCorrect != null) && (lblApproveDoc != null))
                {
                    if (lblApproveDoc.Text == Resources.propLicenseService_048.ToString() || lblApproveDoc.Text == "Y" )
                    {
                        lblApproveDoc.Visible = false;
                        imgCorrect.Visible = true;
                    }
                }

                var list = this.SessionListDT.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblGroupNo.Text);
                if (list != null)
                {
                    if (this.SessionListDT.Count == 1)
                    {
                        this.SessionListDT = this.CreateIndex(this.SessionListDT);
                        LBDown.Visible = false;
                        LBUP.Visible = false;
                    }
                    if (this.SessionListDT.IndexOf(list) == 0)
                    {
                        LBUP.Visible = false;
                    }
                    else if (this.SessionListDT.IndexOf(list) == this.SessionListDT.Count - 1)
                    {
                        LBDown.Visible = false;
                    }

                }

            }
        }

        /// <summary>
        /// Open Popup for sort by SEQ_OF_GROUP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <AUTHOR>Natta</AUTHOR>
        protected void btnSortPopup_Click(object sender, EventArgs e)
        {
            LicenseBiz bizLicense = new LicenseBiz();
            this.SessionListDT = new List<DTO.PersonLicenseTransaction>();
            this.CheckList = new List<bool>();

            if (this.ucLicensePaymentY.GridPayment.Rows.Count <= 0)
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.LicenseYNotFound;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
            }
            else
            {
                #region Get UploadGroupNo

                for (int i = 0; i < ucLicensePaymentY.GridPayment.Rows.Count; i++)
                {
                    CheckBox chkDelete = (CheckBox)ucLicensePaymentY.GridPayment.Rows[i].Cells[0].FindControl("chkBxSelect");
                    if (chkDelete != null)
                    {
                        this.CheckList.Add(chkDelete.Checked);
                        if (chkDelete.Checked)
                        {
                            HiddenField hf = (HiddenField)ucLicensePaymentY.GridPayment.Rows[i].Cells[0].FindControl("HiddenField1");

                            if (hf.Value != null)
                            {
                                //Get
                                DTO.ResponseService<DTO.PersonLicenseTransaction> resD = bizLicense.GetLicenseDetailByUploadGroupNo(hf.Value);
                                if ((resD.DataResponse != null) && (!resD.IsError))
                                {
                                    ///Get & Set SubGroupPayment
                                    this.SessionListDT.Add(new DTO.PersonLicenseTransaction
                                    {
                                        UPLOAD_GROUP_NO = resD.DataResponse.UPLOAD_GROUP_NO,
                                        HEAD_REQUEST_NO = resD.DataResponse.HEAD_REQUEST_NO,
                                        FEES = resD.DataResponse.FEES,
                                        APPROVED_DOC = resD.DataResponse.APPROVED_DOC,
                                        PETITION_TYPE_CODE = resD.DataResponse.PETITION_TYPE_CODE,
                                        TRAN_DATE = resD.DataResponse.TRAN_DATE,
                                        SEQ_NO = resD.DataResponse.SEQ_NO
                                    });

                                    //this.MasterLicense.ListUploadGroupNo.Add(hf.Value);
                                }


                            }
                        }
                    }
                }
                #endregion

                //ตรวจสอบรายการต่อใบสั่งจ่าย @1/7/2557
                int getSumConfig = GetSumConfigViewBillPayment();
                if (this.SessionListDT.Count > getSumConfig)
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = "กรุณาตรวจสอบจำนวนรายการใบอนุญาตที่ต้องการออกใบสั่งจ่าย";
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    return;
                }

                #region Open Popup and then add 'this.MasterLicense.lsLicensePayment' to gridview popup

                if (this.CheckList.Where(chk => chk == false).Count() == this.ucLicensePaymentY.GridPayment.Rows.Count)
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = "กรุณาเลือกรายการก่อนออกใบสั่งจ่าย";
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    return;
                }
                else
                {
                    //gvLicenseD.DataSource = this.SessionListDT;
                    //this.LicenseNameConvertor(this.SessionListDT);

                    gvLicenseD.DataSource = this.CreateIndex(this.SessionListDT);
                    gvLicenseD.DataBind();
                    this.mdlLicenseD.Show();
                }


                #endregion

            }
        }

        private void LicenseNameConvertor(List<DTO.PersonLicenseTransaction> ls)
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

            ls.ForEach(x =>
                {
                    x.PETITION_TYPE_NAME = ConvertPettion(x.PETITION_TYPE_CODE);
                    x.APPROVED_DOC = ConvertApproveDoc(x.APPROVED_DOC);
                });


        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentBiz biz = new PaymentBiz();
            LicenseBiz bizLicense = new LicenseBiz();
            string ref1 = string.Empty;
            string group = string.Empty;
            string headrequestNo = string.Empty;
            StringBuilder ChkUpload = new StringBuilder();
            this.MasterLicense.lsLicensePayment.Clear();

            if (this.gvLicenseD.Rows.Count <= 0)
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.LicenseYNotFound;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
            }
            else
            {
                #region Get UploadGroupNo

                for (int i = 0; i < gvLicenseD.Rows.Count; i++)
                {
                    HiddenField hf = (HiddenField)gvLicenseD.Rows[i].Cells[0].FindControl("hdfGroupUpLoadNo");

                    if (hf.Value != null)
                    {
                        //Get PersonLicenseTransaction from List
                        //var resD = this.CurrentSessionListDT.FirstOrDefault(idx => idx.UPLOAD_GROUP_NO.Equals(hf.Value));
                        var resD = this.SessionListDT.FirstOrDefault(idx => idx.UPLOAD_GROUP_NO.Equals(hf.Value));
                        if (resD != null)
                        {
                            ///Get & Set SubGroupPayment
                            this.MasterLicense.lsLicensePayment.Add(new DTO.SubGroupPayment
                            {
                                RUN_NO = resD.RUN_NO,
                                uploadG = resD.UPLOAD_GROUP_NO,
                                LicenseNo = resD.LICENSE_NO,
                                RenewTime = resD.RENEW_TIMES,
                                seqNo = resD.SEQ_NO,
                                PaymentType = resD.PETITION_TYPE_CODE

                            });

                            this.MasterLicense.ListUploadGroupNo.Add(hf.Value);
                        }


                    }
                }
                #endregion

                #region สร้างใบสั่งจ่ายย่อย > NewCreatePayment

                DTO.ResponseService<string> res = biz.SetSubGroupSingleLicense(this.MasterLicense.lsLicensePayment.ToArray(), this.UserProfile.Id, this.UserProfile.Id, out ref1);
                if ((res.DataResponse != null) && (!res.IsError))
                {
                    this.PaymentStatus = true;
                    this.MasterLicense.UCLicenseUCLicenseModelSuccess.ShowMessageSuccess = SysMessage.CreatePaymentSuccess;
                    this.MasterLicense.UCLicenseUCLicenseModelSuccess.ShowModalSuccess();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    group = ref1;

                    ChkUpload.Append(group);
                    ChkUpload.Append(" ");
                    ChkUpload.Append(this.UserProfile.Id);

                    GetAllLicense();
                    GetLicenseY();
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + group + "')", true);
                }
                else
                {
                    this.PaymentStatus = false;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    return;
                    //udpMain.Update();

                }

                if (this.PaymentStatus == true)
                {
                    ListPrintPayment.Add(group);
                    var resPrint = biz.UpdatePrintGroupRequestNo(ListPrintPayment.ToArray());
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + group + "')", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + ChkUpload.ToString() + "')", true);
                }

                #endregion

            }

        }

        private int GetSumConfigViewBillPayment()
        {
            int SumAllPayment = 0;
            PaymentBiz biz = new PaymentBiz();
            DTO.ResponseService<DataSet> ViewBillPayment = biz.GetConfigViewBillPayment();
            //SumAllPayment = ViewBillPayment.DataResponse.Tables[0].Rows.Count;

            if (ViewBillPayment.DataResponse.Tables[0].Rows.Count > 0)
            {
                DataRow drTran = ViewBillPayment.DataResponse.Tables[0].Rows[0];
                SumAllPayment = Convert.ToInt16(drTran["ITEM_VALUE"]);
            }
            return SumAllPayment;
        }

        //btnPayment เก่า
        //protected void btnPayment_Click(object sender, EventArgs e)
        //{
        //    PaymentBiz biz = new PaymentBiz();
        //    LicenseBiz bizLicense = new LicenseBiz();
        //    string ref1 = string.Empty;
        //    string group = string.Empty;
        //    string headrequestNo = string.Empty;
        //    StringBuilder ChkUpload = new StringBuilder();

        //    if (this.ucLicensePaymentY.GridPayment.Rows.Count <= 0)
        //    {
        //        this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.LicenseYNotFound;
        //        this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
        //        this.MasterLicense.UpdatePanelLicense.Update();
        //    }
        //    else
        //    {
        //        #region Get UploadGroupNo

        //        for (int i = 0; i < ucLicensePaymentY.GridPayment.Rows.Count; i++)
        //        {
        //            CheckBox chkDelete = (CheckBox)ucLicensePaymentY.GridPayment.Rows[i].Cells[0].FindControl("chkBxSelect");
        //            if (chkDelete != null)
        //            {
        //                if (chkDelete.Checked)
        //                {
        //                    HiddenField hf = (HiddenField)ucLicensePaymentY.GridPayment.Rows[i].Cells[0].FindControl("HiddenField1");

        //                    if (hf.Value != null)
        //                    {
        //                        //Get
        //                        DTO.ResponseService<DTO.PersonLicenseTransaction> resD = bizLicense.GetLicenseDetailByUploadGroupNo(hf.Value);
        //                        if ((resD.DataResponse != null) && (!resD.IsError))
        //                        {
        //                            ///Get & Set SubGroupPayment
        //                            this.MasterLicense.lsLicensePayment.Add(new DTO.SubGroupPayment
        //                            {
        //                                uploadG = resD.DataResponse.UPLOAD_GROUP_NO,
        //                                LicenseNo = resD.DataResponse.LICENSE_NO,
        //                                RenewTime = resD.DataResponse.RENEW_TIMES,
        //                                seqNo = resD.DataResponse.SEQ_NO,
        //                                PaymentType = resD.DataResponse.PETITION_TYPE_CODE
        //                            });

        //                            this.MasterLicense.ListUploadGroupNo.Add(hf.Value);
        //                        }


        //                    }
        //                }
        //            }
        //        }
        //        #endregion

        //        #region สร้างใบสั่งจ่ายย่อย > NewCreatePayment

        //        DTO.ResponseService<string> res = biz.SetSubGroupSingleLicense(this.MasterLicense.lsLicensePayment.ToArray(), this.UserProfile.Id, this.UserProfile.Id, out ref1);
        //        if ((res.DataResponse != null) && (!res.IsError))
        //        {
        //            this.PaymentStatus = true;
        //            this.MasterLicense.UCLicenseUCLicenseModelSuccess.ShowMessageSuccess = SysMessage.CreatePaymentSuccess;
        //            this.MasterLicense.UCLicenseUCLicenseModelSuccess.ShowModalSuccess();
        //            this.MasterLicense.UpdatePanelLicense.Update();
        //            group = ref1;

        //            ChkUpload.Append(group);
        //            ChkUpload.Append(" ");
        //            ChkUpload.Append(this.UserProfile.Id);

        //            GetAllLicense();
        //            GetLicenseY();
        //            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + group + "')", true);
        //        }
        //        else
        //        {
        //            this.PaymentStatus = false;
        //            this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
        //            this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
        //            this.MasterLicense.UpdatePanelLicense.Update();
        //            return;
        //            //udpMain.Update();
                    
        //        }

        //        if (this.PaymentStatus == true)
        //        {
        //            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + group + "')", true);
        //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + ChkUpload.ToString() + "')", true);
        //        }

        //        #endregion

        //    }

        //}

        #endregion
    }
}
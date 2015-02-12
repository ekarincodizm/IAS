using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.DTO;

namespace IAS.Setting
{
    public partial class CheckDocument : basepage
    {
        ApploveDocumnetTypeBiz biz = new ApploveDocumnetTypeBiz();

        public List<ASSOCIATION_APPROVE> list_association
        {
            get
            {
                if (Session["list_association"] == null)
                {
                    Session["list_association"] = new List<ASSOCIATION_APPROVE>();
                }
                return (List<ASSOCIATION_APPROVE>)Session["list_association"];
            }

            set
            {
                Session["list_association"] = value;
            }
        }

        public List<DTO.ASSOCIATION_APPROVE> list_add_association
        {
            get
            {
                if (Session["list_add_association"] == null)
                {
                    Session["list_add_association"] = new List<DTO.ASSOCIATION_APPROVE>();
                }
                return (List<DTO.ASSOCIATION_APPROVE>)Session["list_add_association"];
            }

            set
            {
                Session["list_add_association"] = value;
            }
        }

        public List<DTO.ASSOCIATION_APPROVE> list_delete_association
        {
            get
            {
                if (Session["list_delete_association"] == null)
                {
                    Session["list_delete_association"] = new List<DTO.ASSOCIATION_APPROVE>();
                }
                return (List<DTO.ASSOCIATION_APPROVE>)Session["list_delete_association"];
            }

            set
            {
                Session["list_delete_association"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }

        }

        private void BindGrid()
        {
            GvCheckDoc.DataSource = biz.SelectApploveDocumentType("").DataResponse;
            GvCheckDoc.DataBind();

            ddlAsso.DataSource = biz.SelectAsso("").DataResponse;
            ddlAsso.DataBind();

            ddlAsso.Items.Add(new ListItem("--เลือกสมาคม--", ""));
            ddlAsso.SelectedValue = "";
        }

        protected void btnApplove_Click(object sender, EventArgs e)
        {
            ddlAsso.SelectedValue = "";
            lblTypeName.Text = ((Label)((GridViewRow)((Button)sender).Parent.Parent).FindControl("lbldocname")).Text;
            hdTypeCode.Value = ((Label)((GridViewRow)((Button)sender).Parent.Parent).FindControl("lbldoccode")).Text;
            list_association = new List<ASSOCIATION_APPROVE>();
            list_association = biz.SelectAssoApplove(hdTypeCode.Value).DataResponse.ToList();
            list_add_association = new List<ASSOCIATION_APPROVE>();
            list_delete_association = new List<ASSOCIATION_APPROVE>();
            gvAsso.DataSource = list_association;
            gvAsso.DataBind();
            ModalPopup.Show();
        }

        protected void GvCheckDoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string applove = (string)DataBinder.Eval(e.Row.DataItem, "ITEM_VALUE");
                CheckBox check = (CheckBox)e.Row.FindControl("check");
                if (applove == "Y")
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (list_add_association.Count == 0 && list_delete_association.Count == 0)
            {
                UCModalError1.ShowMessageError = "ไม่ข้อมูลการทำรายการของคุณ";
                UCModalError1.ShowModalError();
            }
            else
            {
                var res = biz.AddAssocitionApplove(list_add_association, list_delete_association, base.UserId);// 
                ModalPopup.Hide();
                GvCheckDoc.DataSource = biz.SelectApploveDocumentType("").DataResponse;
                GvCheckDoc.DataBind();
                if (res.IsError)
                {
                    UCModalError1.ShowMessageError = "บันทึกข้อมูลไม่สำเร็จ";
                    UCModalError1.ShowModalError();
                }
                else
                {
                    UCModalSuccess1.ShowMessageSuccess = "บันทึกข้อมูลสำเร็จ";
                    UCModalSuccess1.ShowModalSuccess();
                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ModalPopup.Hide();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlAsso.SelectedValue == "")
            {
                UCModalError1.ShowMessageError = "กรุณาเลือกสมาคม";
                UCModalError1.ShowModalError();
                ModalPopup.Show();
            }
            else
            {
                if (list_association.FirstOrDefault(x => x.ASSOCIATION_CODE == ddlAsso.SelectedValue) == null)
                {
                    list_association.Add(new ASSOCIATION_APPROVE { APPROVE_DOC_TYPE = hdTypeCode.Value, ASSOCIATION_CODE = ddlAsso.SelectedValue, ASSOCIATION_NAME = ddlAsso.SelectedItem.Text });
                    list_add_association.Add(new ASSOCIATION_APPROVE { APPROVE_DOC_TYPE = hdTypeCode.Value, ASSOCIATION_CODE = ddlAsso.SelectedValue });

                    if (list_delete_association.FirstOrDefault(x => x.ASSOCIATION_CODE == ddlAsso.SelectedValue && x.APPROVE_DOC_TYPE == hdTypeCode.Value) != null)
                    {
                        list_delete_association.Remove(list_delete_association.FirstOrDefault(x => x.ASSOCIATION_CODE == ddlAsso.SelectedValue && x.APPROVE_DOC_TYPE == hdTypeCode.Value));
                    }
                    gvAsso.DataSource = list_association;
                    gvAsso.DataBind();
                    ModalPopup.Show();
                    UCModalSuccess1.ShowMessageSuccess = "เพิ่มเข้าในรายการแล้ว";
                    UCModalSuccess1.ShowModalSuccess();

                }
                else
                {
                    ModalPopup.Show();
                    UCModalError1.ShowMessageError = "สมาคมที่คุณเลือกมีแล้วในรายการ";
                    UCModalError1.ShowModalError();

                }
            }
        }

        protected void lblDelete_Click(object sender, EventArgs e)
        {
            string assocition = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblAssoCode")).Text;
            list_delete_association.Add(new ASSOCIATION_APPROVE { APPROVE_DOC_TYPE = hdTypeCode.Value, ASSOCIATION_CODE = assocition });
            list_association.Remove(list_association.FirstOrDefault(x => x.ASSOCIATION_CODE == assocition && x.APPROVE_DOC_TYPE == hdTypeCode.Value));
            gvAsso.DataSource = list_association;
            if (list_add_association.FirstOrDefault(x => x.ASSOCIATION_CODE == assocition && x.APPROVE_DOC_TYPE == hdTypeCode.Value) != null)
            {
                list_add_association.Remove(list_add_association.FirstOrDefault(x => x.ASSOCIATION_CODE == assocition && x.APPROVE_DOC_TYPE == hdTypeCode.Value));
            }
            gvAsso.DataBind();
            ModalPopup.Show();
            UCModalSuccess1.ShowMessageSuccess = "ลบข้อมูลออกจากรายการแล้ว";
            UCModalSuccess1.ShowModalSuccess();
        }

        protected void btnCancelDoctype_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnSaveDoctype_Click(object sender, EventArgs e)
        {
            var list = new List<DTO.ApploveDocumnetType>();
            foreach (GridViewRow row in GvCheckDoc.Rows)
            {
                Label lbldoccode = (Label)row.FindControl("lbldoccode");
                CheckBox check = (CheckBox)row.FindControl("check");
                ApploveDocumnetType doc = new ApploveDocumnetType();
                doc.APPROVE_DOC_TYPE = lbldoccode.Text;
                if (check.Checked)
                {
                    doc.ITEM_VALUE = "Y";
                }
                else
                {
                    doc.ITEM_VALUE = "N";
                }
                list.Add(doc);
            }
            var res = biz.UpdateApploveDoctype(list, base.UserId);//
            if (res.IsError)
            {
                UCModalError1.ShowMessageError = "บันทึกข้อมูลไม่สำเร็จ";
                UCModalError1.ShowModalError();
            }
            else
            {
                UCModalSuccess1.ShowMessageSuccess = "บันทึกข้อมูลสำเร็จ";
                UCModalSuccess1.ShowModalSuccess();
            }
        }
    }
}
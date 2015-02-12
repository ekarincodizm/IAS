<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterRegister.master"
    AutoEventWireup="true" CodeBehind="RegisGeneral.aspx.cs" Inherits="IAS.Register.RegisGeneral"
    meta:resourcekey="PageResource1" %>

<%@ Register Src="~/UserControl/ucAddress.ascx" TagName="ucAddress" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/ucCurrentLicense.ascx" TagName="UCLicense" TagPrefix="uclic" %>
<%@ Register Src="~/UserControl/UCLoadingProgress.ascx" TagName="UCLoading" TagPrefix="ucload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function CancelConfirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการนำเข้าข้อมูลหรือไม่?")) {
                confirm_value.value = "Y";
            } else {
                confirm_value.value = "N";
            }
            document.forms[0].appendChild(confirm_value);
        }

        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

        function confirmSave() {
            return confirm('คุณต้องการบันทึกข้อมูลหรือไม่');
        }

        function isValid() {
            var txtBirthDay = $('#<%=txtBirthDay.ClientID %>');
            var lblMsg = $('#<%=this.MasterPage.LabelMsg.ClientID %>');

            lblMsg.val("");

            if (txtBirthDay.val() != '') {
                var items = new Array();
                items = txtBirthDay.val().split('/');
                lblMsg.innerHTML = "";
                if (items != null && items.length == 3) {

                    return true;

                }
                else {
                    lblMsg.val("Date format is 'dd MMMM yyyy' only.");
                    return false;
                }
            }
            else {

                lblMsg.val("Please Enter Date.");
                return (txtBirthDay.val() != '');
            }
        }

        function Input_Eng(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 3585) {
                return false;
            } else {
                var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?^()";
                var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
                var ch = String.fromCharCode(charCode);
                if (iChars.indexOf(ch) == -1) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function fncInputNumeric() {
            debugger;
            if (!(event.keyCode == 48 && event.keyCode <= 57)) {
                event.returnValue = false;
                alert("Hello");
            }
        }

        function spacialCaractor(evt) {
            var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()1234567890๑๒๓๔฿๕๖๗๘๙๐";
            var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
            var ch = String.fromCharCode(charCode);
            if (iChars.indexOf(ch) == -1) {
                return true;
            }
            else {
                return false;
            }
        } 

    </script>
    <% if (Request.QueryString["Mode"] == null)
       {%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#Panel1").hide();
        });
    </script>
    <%} 
    %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="บุคคลทั่วไป" BorderStyle="None" ID="lblRegister" runat="server"
                    meta:resourcekey="lblRegisterResource1" />
            </div>
        </div>
        <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                <%--   <Triggers>
                <asp:PostBackTrigger ControlID="chkEnableEditName" />
            </Triggers>--%>
                <ContentTemplate>
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::เพิ่มสมาชิก</div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <div>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td colspan="6" align="center">
                                                <asp:Label ID="lblMessage" runat="server" CssClass="lbl" Font-Bold="true" ForeColor="Red"
                                                    Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                      <%--  <tr>
                                            <td colspan="7">
                                                &nbsp;
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td colspan="6">
                                                <div>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%;">
                                                                <%--<fieldset class="fsFixIe">--%>
                                                                <%-- <legend><asp:CheckBox ID="chkEnableEditName" runat="server" Checked="false" 
                                                                    AutoPostBack="true" Text="แก้ไข" 
                                                                    CausesValidation="false" Font-Bold="true" CssClass="lbl" 
                                                                    oncheckedchanged="chkEnableName_CheckedChanged" /></legend>--%>
                                                                <asp:Panel ID="Panel2" runat="server" Enabled="true">
                                                                    <table style="width: 100%; border-style: none;">
                                                                        <tr>
                                                                            <td style="text-align: right; width: 14%;">
                                                                                <span>
                                                                                    <asp:Label ID="lblTypeMember" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                                                                    :</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 22%;">
                                                                                <asp:TextBox ID="txtMemberType" runat="server" CssClass="txtreadonly" Enabled="false"
                                                                                    Width="170px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 14%;">
                                                                       
                                                                                    <asp:Label ID="lblAgentType" runat="server" CssClass="lbl" Text="ประเภทตัวแทนสมาชิก :"
                                                                                        Visible="false"></asp:Label>
                                                                                    <%--:--%>
                                                                            </td>
                                                                            <td style="text-align: left; width: 22%;">
                                                                                <asp:DropDownList ID="ddlAgentType" runat="server" CssClass="ddl" Visible="false"
                                                                                    Height="19px" Width="175px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="text-align: right; width: 12%;">
                                                                            </td>
                                                                            <td style="text-align: left; width: 20%;">
                                                                            </td>
                                                                            <td style="text-align: right; width: 11%;">
                                                                            </td>
                                                                            <td style="text-align: left; width: 21%;">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <%-- </fieldset>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <div>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%;">
                                                                <%--<fieldset class="fsFixIe">--%>
                                                                <%-- <legend><asp:CheckBox ID="chkEnableEditName" runat="server" Checked="false" 
                                                                    AutoPostBack="true" Text="แก้ไข" 
                                                                    CausesValidation="false" Font-Bold="true" CssClass="lbl" 
                                                                    oncheckedchanged="chkEnableName_CheckedChanged" /></legend>--%>
                                                                <asp:Panel ID="pnlIDCardValidate" runat="server" Enabled="true">
                                                                    <table style="width: 100%; border-style: none;">
                                                                        <tr>
                                                                            <td style="text-align: right; width: 14%;">
                                                                                <asp:Label ID="lblIDNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 22%;">
                                                                                <asp:TextBox ID="txtIDNumber" ClientIDMode="Static" runat="server" autocomplete="off"
                                                                                    CssClass="txt txtidcard" MaxLength="13" onkeypress="fncInputNumericValuesOnly()" Width="170px"></asp:TextBox>
                                                                                <asp:ImageButton ID="btnImgIDCardValidate" runat="server" Text="นำเข้าข้อมูล" CausesValidation="false"
                                                                                    ImageUrl="../Images/icons/search.png" ToolTip="ค้นหน้าข้อมูลโดยเลขบัตรประชาชน"
                                                                                    OnClick="btnImgIDCardValidate_Click" />
                                                                                <asp:Button ID="btnIDCards" runat="server" Style="display: none" />
                                                                                <ajaxToolkit:ModalPopupExtender ID="mdpIDCardValidation" runat="server" TargetControlID="btnIDCards"
                                                                                    PopupControlID="pnlIDCardValidation" BackgroundCssClass="modalbackground" CancelControlID="btnCancelImportByIDCard">
                                                                                </ajaxToolkit:ModalPopupExtender>
                                                                                <asp:Panel ID="pnlIDCardValidation" runat="server" CssClass="popup_container_import_data"
                                                                                    Style="display: none">
                                                                                    <div style="text-align: center;" class="popup_title_confirm">
                                                                                        <asp:Label ID="Label9" runat="server" Text="ตรวจสอบข้อมูล"></asp:Label>
                                                                                    </div>
                                                                                    <div style="padding: 15px;">
                                                                                        <table width="100%">
                                                                                            <tr>
                                                                                                <td style="text-align: right;">
                                                                                                    <asp:Label ID="lblImportBirthdayHead" runat="server" Text="วันเกิด"></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtImportBirthdayHead" runat="server"></asp:TextBox>
                                                                                                    <asp:ImageButton ID="imgPopup_txtImportBirthdayHead" ImageUrl="~/Images/calendar.gif"
                                                                                                        ImageAlign="Bottom" runat="server" />
                                                                                                    <ajaxToolkit:CalendarExtender ID="calExImportBirthDate" PopupButtonID="imgPopup_txtImportBirthdayHead"
                                                                                                        runat="server" Format="dd/MM/yyyy" TargetControlID="txtImportBirthdayHead">
                                                                                                    </ajaxToolkit:CalendarExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td style="text-align: right;">
                                                                                                    <asp:Label ID="lblImportLicenseNoHead" runat="server" Text="เลขที่ใบอนุญาต"></asp:Label>
                                                                                                </td>
                                                                                                <td style="text-align: left;">
                                                                                                    <asp:TextBox ID="txtImportLicenseNoHead" runat="server" MaxLength="10"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <div align="center">
                                                                                            <asp:Button ID="btnImportByIDCard" runat="server" CssClass="btn" Text="ยืนยัน" OnClick="btnImportByIDCard_Click" />
                                                                                            <asp:Button ID="btnCancelImportByIDCard" runat="server" CssClass="btn" Text="ยกเลิก" />
                                                                                        </div>
                                                                                </asp:Panel>
                                                                                <%--                                                                             <asp:ImageButton ID="btnImgImportDetail" runat="server" Text="นำเข้าข้อมูล" CausesValidation="false"
                                                                                    Visible="false" Enabled="false" ImageUrl="../Images/icons/yes.png" ToolTip="นำเข้าข้อมูล"/>--%>
                                                                                <asp:RequiredFieldValidator ID="IDNumberRequired" runat="server" ControlToValidate="txtIDNumber"
                                                                                    CssClass="failureNotification" ToolTip="กรุณากรอกเลขบัตรประชาชน" ErrorMessage="กรุณากรอกเลขบัตรประชาชน"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="reqRegIDNumber" runat="server" ErrorMessage="เลขบัตรประชาชนไม่ถูกต้อง"
                                                                                    ControlToValidate="txtIDNumber" ValidationExpression="^[0-9]{13}?$" ValidationGroup="regGuestValidationGroup"
                                                                                    CssClass="failureNotification">
                                                                                    <asp:Label ID="Label2" runat="server" Visible="false">*</asp:Label></asp:RegularExpressionValidator>
                                                                                <%--<ajaxToolkit:ConfirmButtonExtender ID="exConfirm" runat="server" DisplayModalPopupID="mdpConfirm" TargetControlID="btnImgImportDetail" >
                                                                                </ajaxToolkit:ConfirmButtonExtender>
                                                                                <ajaxToolkit:ModalPopupExtender ID="mdpConfirm" runat="server" 
                                                                                        PopupControlID="pnlConfirm" TargetControlID="btnImgImportDetail"
                                                                                        OkControlID="btnCYes" CancelControlID="btnCNo" PopupDragHandleControlID="pnlConfirm" BackgroundCssClass="modalbackground2">
                                                                                </ajaxToolkit:ModalPopupExtender>
                                                                                <asp:Panel ID="pnlConfirm" runat="server" style="display:none;" CssClass="popup_container">
                                                                                    <div class="popup_title_confirm">
                                                                                        <asp:Label ID="Label8" runat="server" Text="นำเข้าข้อมูล"></asp:Label>
                                                                                    </div>
                                                                                    <div style="padding:15px;">
                                                                                    <table width="90%" cellpadding="3" cellspacing="3">
                                                                                        <tr >
                                                                                            <td align="center" width="20px" style="background:none;border:none;">
                                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/info.png" />
                                                                                            </td>
                                                                                            <td  align="center" style="background:none;border:none;">
                                                                                               คุณต้องการนำเข้าข้อมูลหรือไม่
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>		    
                                                                                     <div align="center">
                                                                                        <asp:Button ID="btnCYes" runat="server" CssClass="btn" Text="ใช่" />
                                                                                        <asp:Button ID="btnCNo" runat="server" CssClass="btn" Text="ไม่" />
                                                                                   </div>
                                                                                </asp:Panel>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 14%;">
                                                                                <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 22%;">
                                                                                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ddl" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlTitle"
                                                                                    ErrorMessage="กรุณาเลือกคำนำหน้าชื่อ" CssClass="failureNotification" ToolTip="กรุณาเลือกคำนำหน้าชื่อ"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td style="text-align: right; width: 12%;">
                                                                                <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 20%;">
                                                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" autocomplete="off" MaxLength="50"
                                                                                    onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName"
                                                                                    ErrorMessage="กรุณากรอกชื่อ" CssClass="failureNotification" ToolTip="กรุณากรอกชื่อ"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="REV1" Display="Dynamic" runat="server" ControlToValidate="txtFirstName"
                                                                                    ErrorMessage="รูปแบบชื่อไม่ถูกต้อง" CssClass="failureNotification" ValidationGroup="regGuestValidationGroup"
                                                                                    ValidationExpression="^[ก-ฮ|a-zA-Z|\p{L}|\p{M}]{1,50}?$">
                                                                                    <asp:Label ID="Label6" runat="server" Visible="false">*</asp:Label>
                                                                                </asp:RegularExpressionValidator>
                                                                            </td>
                                                                            <td style="text-align: right; width: 11%;">
                                                                                <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 21%;">
                                                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" autocomplete="off" MaxLength="70"
                                                                                    onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                                                                                    ErrorMessage="กรุณากรอกนามสกุล" CssClass="failureNotification" ToolTip="กรุณากรอกนามสกุล"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic"
                                                                                    runat="server" ControlToValidate="txtLastName" ErrorMessage="รูปแบบนามสกุลไม่ถูกต้อง"
                                                                                    CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" ValidationExpression="^[ก-ฮ|a-zA-Z|\s|\p{L}|\p{M}]{1,70}?$">
                                                                                    <asp:Label ID="Label7" runat="server" Visible="false">*</asp:Label>
                                                                                </asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="6">
                                                                                <div>
                                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                        <tr style="width: 100%;">
                                                                                            <td style="width: 100%;">
                                                                                                <%--<fieldset class="fsFixIe">--%>
                                                                                                <%-- <legend><asp:CheckBox ID="chkEnableEditName" runat="server" Checked="false" 
                                                                    AutoPostBack="true" Text="แก้ไข" 
                                                                    CausesValidation="false" Font-Bold="true" CssClass="lbl" 
                                                                    oncheckedchanged="chkEnableName_CheckedChanged" /></legend>--%>
                                                                                                <asp:Panel ID="Panel1" runat="server" Enabled="true">
                                                                                                    <table style="width: 100%; border-style: none;">
                                                                                                        <tr>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </asp:Panel>
                                                                                                <%-- </fieldset>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 12%;">
                                                                                <asp:Label ID="lblBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 20%;">
                                                                                <asp:TextBox ID="txtBirthDay" runat="server"></asp:TextBox>
                                                                                <asp:ImageButton ID="imgPopup_txtBirthDay" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                                                    runat="server" />
                                                                                <ajaxToolkit:CalendarExtender ID="calexBirthDay" PopupButtonID="imgPopup_txtBirthDay"
                                                                                    runat="server" TargetControlID="txtBirthDay" Format="dd/MM/yyyy">
                                                                                </ajaxToolkit:CalendarExtender>
                                                                                <asp:RequiredFieldValidator ID="BirthDayRequired" runat="server" ControlToValidate="txtBirthDay"
                                                                                    ErrorMessage="กรุณาเลือกวัน เดือน ปี เกิด" CssClass="failureNotification" ToolTip="กรุณาเลือกวัน เดือน ปี เกิด"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td style="text-align: right; width: 11%;">
                                                                                <asp:Label ID="lblSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left;">
                                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" Enabled="false"
                                                                                                CssClass="rb">
                                                                                                <asp:ListItem Text="ชาย" Value="M"></asp:ListItem>
                                                                                                <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                                                                            </asp:RadioButtonList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblSex"
                                                                                                Display="Dynamic" CssClass="failureNotification" ToolTip="กรุณาระบุเพศ" ErrorMessage="กรุณาระบุเพศ"
                                                                                                ValidationGroup="regGuestValidationGroup">*
                                                                                            </asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <%-- </fieldset>--%>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <div>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                        <tr style="width: 100%;">
                                                            <td style="width: 100%;">
                                                                <%--<fieldset class="fsFixIe">--%>
                                                                <%-- <legend><asp:CheckBox ID="chkEnableEditName" runat="server" Checked="false" 
                                                                    AutoPostBack="true" Text="แก้ไข" 
                                                                    CausesValidation="false" Font-Bold="true" CssClass="lbl" 
                                                                    oncheckedchanged="chkEnableName_CheckedChanged" /></legend>--%>
                                                                <asp:Panel ID="Panel3" runat="server" Enabled="true">
                                                                    <table style="width: 100%; border-style: none;">
                                                                        <tr>
                                                                            <td style="text-align: right; width: 14%;">
                                                                                <asp:Label ID="lblNationality" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 22%;">
                                                                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="ddl">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="NationalityRequired" runat="server" ControlToValidate="ddlNationality"
                                                                                    ErrorMessage="กรุณาเลือกสัญชาติ" CssClass="failureNotification" ToolTip="กรุณาเลือกสัญชาติ"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td style="text-align: right;width: 12%;">
                                                                                <asp:Label ID="lblEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left;width: 20%;">
                                                                                <asp:DropDownList ID="ddlEducation" runat="server" CssClass="ddl">
                                                                                </asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="EducationRequired" runat="server" ControlToValidate="ddlEducation"
                                                                                    ErrorMessage="กรุณาเลือกระดับการศึกษา" CssClass="failureNotification" ToolTip="กรุณาเลือกระดับการศึกษา"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                            </td>
                                                                            <td style="text-align: right;width: 11%;">
                                                                                <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left;width: 21%;">
                                                                                <asp:TextBox ID="txtEmail" runat="server" onkeypress="return Input_Eng(event);" CssClass="txt"
                                                                                    autocomplete="off" MaxLength="255"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                                                                                    ErrorMessage="กรุณากรอกอีเมล" CssClass="failureNotification" ToolTip="กรุณากรอกอีเมล"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                                <!--Email ValidationExpression Nattapong@22/8/2556-->
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic"
                                                                                    runat="server" ControlToValidate="txtEmail" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง"
                                                                                    CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$">
                                                                                    <asp:Label ID="Label3" runat="server" Visible="false">*</asp:Label>
                                                                                </asp:RegularExpressionValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 14%;">
                                                                                <asp:Label ID="lblTel" runat="server" Text="โทรศัพท์" CssClass="lbl"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 22%;">
                                                                                <asp:TextBox ID="txtTel" runat="server" CssClass="txt-tel" MaxLength="9"  onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                                                                <asp:Label ID="lblTelExt" runat="server" CssClass="lbl" Text="ต่อ"></asp:Label>
                                                                                <asp:TextBox ID="txtTelExt" runat="server" CssClass="txt-tel-ext" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="GeneralTelRequired" runat="server" ControlToValidate="txtTel"
                                                                                    ErrorMessage="กรุณากรอกเบอร์โทรศัพท์" CssClass="failureNotification" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="รูปแบบโทรศัพท์ไม่ถูกต้อง"
                                                                                    CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" ControlToValidate="txtTel"
                                                                                    ValidationExpression="^0[0-9]{8}">
                                                                                    <asp:Label ID="Label4" runat="server" Visible="false">*</asp:Label>
                                                                                </asp:RegularExpressionValidator>
                                                                            </td>
                                                                            <td style="text-align: right;width: 12%;">
                                                                                <asp:Label ID="lblMobilePhone" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left;width: 20%;">
                                                                                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="txt" MaxLength="10" onkeypress="fncInputNumeric();"
                                                                                    onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="MobilePhoneRequired" runat="server" ControlToValidate="txtMobilePhone"
                                                                                    ErrorMessage="กรุณากรอกเบอร์โทรศัพท์มือถือ" CssClass="failureNotification" ToolTip="กรุณากรอกเบอร์โทรศัพท์มือถือ"
                                                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="failureNotification"
                                                                                    ValidationGroup="regGuestValidationGroup" ControlToValidate="txtMobilePhone"
                                                                                    ErrorMessage="รูปแบบโทรศัพท์มือถือไม่ถูกต้อง" ValidationExpression="^0[0-9]{9}">
                                                                                    <asp:Label ID="Label5" runat="server" Visible="false">*</asp:Label>
                                                                                </asp:RegularExpressionValidator>
                                                                            </td>
                                                                            <td align="right">
                                                                                <%--คุณวุฒิเพิ่มเติม :--%>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:DropDownList ID="ddlQualification" runat="server" CssClass="ddl" Visible="false">
                                                                                </asp:DropDownList>
                                                                                <asp:LinkButton ID="lbtAddQualification" runat="server" Visible="false" OnClick="lbtAddQualification_Click"><img src="../Images/add.png" /></asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <%-- </fieldset>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                </td> </tr>
                                <tr>
                                    <td style="text-align: left;" colspan="3">
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="หมายเหตุ : Email ที่ใช้จะต้องเป็น Email ที่ใช้งานได้จริงเท่านั้น เพราะจะถูกใช้ในการติดต่อ"></asp:Label>
                                    </td>
                                    <td align="right" colspan="3">
                                        <asp:GridView ID="GvQualification" runat="server" AutoGenerateColumns="false" Width="500px"
                                            CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1">
                                            <AlternatingRowStyle CssClass="altrow" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ลำดับ">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SPECIAL_TYPE_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อคุณวุฒิ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("SPECIAL_TYPE_DESC") %>'></asp:Label>
                                                        <asp:DropDownList ID="ddlQualification" runat="server" CssClass="ddl" Visible="false">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="แก้ไข">
                                                    <ItemTemplate>
                                                        <div align="center">
                                                            <asp:LinkButton ID="lbtSave" ToolTip="บันทึก" runat="server" OnClick="lbtSave_Click"
                                                                Visible="false"><img src="../Images/save.png" /> </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtCancel" ToolTip="ยกเลิก" runat="server" OnClick="lbtCancel_Click"
                                                                Visible="false"><img src="../Images/cancel.png" /> </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtUpdate" ToolTip="แก้ไข" OnClick="lbtUpdate_Click" runat="server"><img src="../Images/edit-icon.gif" /></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ลบ">
                                                    <ItemTemplate>
                                                        <div align="center">
                                                            <asp:LinkButton ID="lblDelete" ToolTip="ลบ" OnClick="lblDelete_Click" runat="server"><img src="../Images/delete_icon.png" /></asp:LinkButton>
                                                            <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe"
                                                                TargetControlID="lblDelete">
                                                            </ajaxToolkit:ConfirmButtonExtender>
                                                            <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                                                TargetControlID="lblDelete" Drag="true" OkControlID="btnYes" CancelControlID="btnNo"
                                                                PopupDragHandleControlID="pnlPopup" BackgroundCssClass="modalbackground2">
                                                            </ajaxToolkit:ModalPopupExtender>
                                                            <asp:Panel ID="pnlPopup" runat="server" CssClass="popup_container" Wrap="true">
                                                                <div class="popup_title_confirm">
                                                                    <asp:Label ID="lblTitle" runat="server" Text="แจ้งเตือน"></asp:Label>
                                                                </div>
                                                                <div style="padding: 15px;">
                                                                    <table width="90%" cellpadding="3" cellspacing="3">
                                                                        <tr>
                                                                            <td align="center" width="20px" style="background: none; border: none;">
                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/info.png" />
                                                                            </td>
                                                                            <td align="center" style="background: none; border: none;">
                                                                                คุณต้องการลบข้อมูลใช่หรือไม่
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <div align="center">
                                                                        <asp:Button ID="btnYes" runat="server" CssClass="btn" Text="ใช่" />
                                                                        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="ไม่" />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="text-align: center;">
                                    </td>
                                </tr>
                                </table>
                                <table style="width: 100%;" align="center">
                                    <tr>
                                        <td>
                                            <ajaxToolkit:TabContainer ID="TabAddress" runat="server" Width="100%" ActiveTabIndex="0"
                                                AutoPostBack="true" CssClass="ajax_tabs" OnActiveTabChanged="TabAddress_ActiveTabChanged">
                                                <ajaxToolkit:TabPanel ID="TabCurrentAddress" runat="server" Height="140px">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCurrentAddressHeader" runat="server" ForeColor="Black" Text="ที่อยู่ปัจจุบัน"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <uc1:ucAddress ID="ucAddressCurrent" runat="server" />
                                                        <br />
                                                        <asp:CheckBox ID="chkCopyAdd" runat="server" AutoPostBack="True" OnCheckedChanged="chkCopyAdd_CheckedChanged"
                                                            Text="ที่อยู่ตามทะเบียนบ้านเหมือนที่อยู่ปัจจุบัน" />
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel ID="TabRegisterAddress" runat="server" Height="140px">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblRegisterAddressHeader" runat="server" ForeColor="Black" Text="ที่อยู่ตามทะเบียนบ้าน"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <uc1:ucAddress ID="ucAddressRegister" runat="server" />
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                                <table style="width: 100%;" align="center">
                                    <tr>
                                        <td>
                                            <ajaxToolkit:TabContainer ID="TabLicnese" runat="server" Width="100%" ActiveTabIndex="0"
                                                AutoPostBack="false" CssClass="ajax_tabs">
                                                <ajaxToolkit:TabPanel ID="TabCurrentLicnese" runat="server">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCurrentLicneseHeader" runat="server" ForeColor="Black" Text="ข้อมูลใบอนุญาต"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <uclic:UCLicense ID="ucCurrentLicense" runat="server"></uclic:UCLicense>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    </div>
                    <uc2:UCModalError ID="UCModalError1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <% if (false)
           {  %>
    </div>
    <%} %>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Reg_NotApprove.aspx.cs" Inherits="IAS.Register.Reg_NotApprove" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/ucAttachFileControl.ascx" TagName="ucAttachFileControl" tagprefix="uc4" %>
<%@ Register Src="~/UserControl/ucAddress.ascx" TagName="ucAddress" TagPrefix="uc5"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

        function confirmSave() {
            return confirm('คุณต้องการบันทึกข้อมูลหรือไม่');
        }

        function isValid() {
            var txtBirthDay = document.getElementById('<%=txtBirthDay.ClientID %>');
            var lblMsg = document.getElementById('<%=lblMsg.ClientID %>');

            lblMsg.innerHTML = "";

            if (txtBirthDay.value != '') {
                var items = new Array();
                items = txtBirthDay.value.split('/');
                lblMsg.innerHTML = "";
                if (items != null && items.length == 3) {

                    return true;

                }
                else {
                    lblMsg.innerHTML = "Date format is 'dd MMMM yyyy' only.";
                    return false;
                }
            }
            else {

                lblMsg.innerHTML = "Please Enter Date.";
                return txtBirthDay.value != '';
            }
        }

        function checkUser() {
            $.ajax({
                url: "../Service/RegistrationWebService.asmx/VerifyIdCard",
                data: "{ strIdCard : '" + $('#<%= txtIDNumber.ClientID %>').val() + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data == null) {
                        alert("nothing.");
                    }
                    else {
                        if (data.d == true) {
                        }
                        else {
                            alert("กรุณากรอกเลขบัตรประชาชนให้ถูกต้อง");
                        }
                    }
                }
            });
        }

        function checkemail() {
            var emailFilter = /^.+@.+\..{2,3}$/;
            var Email = document.getElementById('<%=txtEmail.ClientID %>');
            var str = Email.value;
            if (!(emailFilter.test(str))) {
                alert("ท่านใส่อีเมลไม่ถูกต้อง");
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
<div class="box_content">
	<div class="box_content_header">
		<div class="box_content_header_text"></div>
	</div>

 
        <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="box_body_content">

                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label ID="lblMessage" runat="server" CssClass="lbl" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblTypeMember" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtTypeMember" runat="server" CssClass="txtreadonly"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlTitle"
                                    CssClass="failureNotification" ToolTip="กรุณาเลือกคำนำหน้าชื่อ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" autocomplete="off" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName"
                                    CssClass="failureNotification" ToolTip="กรุณากรอกชื่อ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" autocomplete="off" MaxLength="70"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                                    CssClass="failureNotification" ToolTip="กรุณากรอกนามสกุล" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblIDNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtIDNumber" runat="server" autocomplete="off" CssClass="txt" MaxLength="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="IDNumberRequired" runat="server" ControlToValidate="txtIDNumber"
                                    CssClass="failureNotification" ToolTip="กรุณากรอกเลขบัตรประชาชน" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtBirthDay" runat="server" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDay" runat="server" TargetControlID="txtBirthDay" Format = "dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="BirthDayRequired" runat="server" ControlToValidate="txtBirthDay"
                                    CssClass="failureNotification" ToolTip="กรุณาเลือกวัน เดือน ปี เกิด" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="rb">
                                    <asp:ListItem Text="ชาย" Value="M" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblNationality" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="NationalityRequired" runat="server" ControlToValidate="ddlNationality"
                                    CssClass="failureNotification" ToolTip="กรุณาเลือกสัญชาติ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlEducation" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="EducationRequired" runat="server" ControlToValidate="ddlEducation"
                                    CssClass="failureNotification" ToolTip="กรุณาเลือกระดับการศึกษา" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" autocomplete="off" MaxLength="255"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                                    CssClass="failureNotification" ToolTip="กรุณากรอกอีเมล" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblTel" runat="server" Text="โทรศัพท์" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtTel" runat="server" CssClass="txt" MaxLength="15"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblMobilePhone" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="txt" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="MobilePhoneRequired" runat="server" ControlToValidate="txtMobilePhone"
                                    CssClass="failureNotification" ToolTip="กรุณากรอกเบอร์โทรศัพท์มือถือ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="4">
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="หมายเหตุ : Email ที่ใช้จะต้องเป็น Email ที่ใช้งานได้จริงเท่านั้น เพราะจะถูกใช้ในการติดต่อ"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%;" align="center">
                        <tr>
                            <td>
                                <ajaxToolkit:TabContainer ID="TabAddress" runat="server" Width="100%" ActiveTabIndex="0">
                                    <ajaxToolkit:TabPanel ID="TabCurrentAddress" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCurrentAddressHeader" runat="server" Text="ที่อยู่ปัจจุบัน" ForeColor="Black"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <uc5:ucAddress ID="UcCurrentAdd" runat="server"  />
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabRegisterAddress" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblRegisterAddressHeader" runat="server" Text="ที่อยู่ตามทะเบียนบ้าน"
                                                ForeColor="Black"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <uc5:ucAddress ID="UcRegisAdd" runat="server"  />
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <table style="width: 90%;" align="center">
                        <tr>
                            <td>
                                <uc4:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
                            </td>
                        </tr>
                    </table>
                        <table style="width: 90%;" align="center">

                            <tr>
                                <td style="text-align: center;" colspan="4">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: left;" colspan="4">
                                    <asp:ValidationSummary ID="regGuestValidationSummary" runat="server" CssClass="failureNotification"
                                        ValidationGroup="regGuestValidationGroup" HeaderText="กรุณากรอกข้อมูลให้ครบ"
                                        ShowSummary="False" ShowMessageBox="true" />
                                </td>
                            </tr>
                        </table>

                    <table align="center" style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 25%;">
                                <asp:Label ID="lblResultReg" runat="server" Text="ผลการสมัคร" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 75%;">
                                <asp:TextBox ID="txtResultReg" runat="server" CssClass="txt" Height="100px" Width="500px"
                                    TextMode="MultiLine" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <uc2:UCModalError ID="UCModalError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />

	            </div>
                </ContentTemplate>
  
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="PnlAddButton" runat="server" Visible="true">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="text-align: center;" colspan="2">
                        <%--<asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOk_Click"
                                        OnClientClick="javascript:isValid();" ValidationGroup="regGuestValidationGroup" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClientClick="javascript:return confirm('ต้องการยกเลิกใช่หรือไม่?');"
                                        OnClick="btnCancel_Click" />
                                    &nbsp;&nbsp;--%>
                        <asp:Button ID="btnEdit" runat="server" Text="แก้ไขข้อมูล" OnClick="btnEdit_OnClick" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlEditButton" runat="server" Visible="false">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="text-align: center;" colspan="2">
                        <asp:Button ID="btnOkEdit" runat="server" Text="ตกลง" CssClass="btn" OnClientClick="javascript:isValid();return confirmSave();"
                            ValidationGroup="regGuestValidationGroup" OnClick="btnOkEdit_Click" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnCancelEdit" runat="server" Text="ยกเลิก" CssClass="btn" OnClientClick="javascript:return confirm('ต้องการยกเลิกใช่หรือไม่?');"
                            OnClick="btnCancelEdit_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

    <asp:HiddenField ID="HiddenField_ID" runat="server" />

</div>
<div style="clear:left;"></div>
</asp:Content>

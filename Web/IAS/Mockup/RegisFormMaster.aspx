<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterRegister.master" AutoEventWireup="true" CodeBehind="RegisFormMaster.aspx.cs" Inherits="IAS.Mockup.RegisFormMaster" %>
<%@ Register src="~/UserControl/ucAddress.ascx" tagname="ucAddress" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


                     $(document).ready(function () {
                         $('#txtIDNumber').bind('keydown', function () {
                             checkKeyNumberFixPoint(event, this, 13);
                         });
                         $('#txtIDNumber').bind('blur', function () {
                             checkUser(event, this, 13);
                         });
                         $('#txtIDNumber').bind('keypress', function () {
                             checkKeyNumberKeyPressFixPoint(event, this, 13);
                         });
                     });

                     function confirmDelete() {
                         return confirm('คุณต้องการลบข้อมูลหรือไม่?');
                     }

                     function confirmSave() {
                         return confirm('คุณต้องการบันทึกข้อมูลหรือไม่');
                     }

                     function isValid() {
                         var txtBirthDay = $('#txtBirthDay');
                         var lblMsg = $('#lblMsg');

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

                     function checkUser() {
                         $.ajax({
                             url: "../Service/RegistrationWebService.asmx/VerifyIdCard",
                             data: "{ strIdCard : '" + $('#txtIDNumber').val() + "' }",
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
                         var Email = $('#txtEmail');
                         var str = Email.val();
                         if (!(emailFilter.test(str))) {
                             alert("ท่านใส่อีเมลไม่ถูกต้อง");
                             return false;
                         }
                         return true;
                     }



    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
        <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
    <table style="width: 100%;">
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
                                <asp:Label ID="lblIDNumber"  runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtIDNumber" ClientIDMode="Static" runat="server" autocomplete="off" CssClass="txt" MaxLength="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="IDNumberRequired" runat="server" ControlToValidate="txtIDNumber"
                                    CssClass="failureNotification" ToolTip="กรุณากรอกเลขบัตรประชาชน" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtBirthDay" runat="server"  ClientIDMode="Static"></asp:TextBox>
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
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left">
                                            <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="rb">
                                                <asp:ListItem Text="ชาย" Value="M" ></asp:ListItem>
                                                <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblSex" Display="Dynamic"
                                                CssClass="failureNotification" ToolTip="กรุณาระบุเพศ" ValidationGroup="regGuestValidationGroup" Text="*">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
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
                                <asp:DropDownList ID="ddlEducation" runat="server" CssClass="ddl" >
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
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" 
                                    Text="*" ControlToValidate="txtTel" ValidationExpression="^[0-9|-]{1,15}?$" >
                                </asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblMobilePhone" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="txt" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="MobilePhoneRequired" runat="server" ControlToValidate="txtMobilePhone"
                                    CssClass="failureNotification" ToolTip="กรุณากรอกเบอร์โทรศัพท์มือถือ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" 
                                    Text="*" ControlToValidate="txtMobilePhone" ValidationExpression="^[0-9|-]{1,15}?$" >
                                </asp:RegularExpressionValidator>
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
                                <ajaxToolkit:TabContainer ID="TabAddress" runat="server" Width="100%" 
                                    ActiveTabIndex="0" AutoPostBack="false" 
                                    onactivetabchanged="TabAddress_ActiveTabChanged" >
                                    <ajaxToolkit:TabPanel ID="TabCurrentAddress" runat="server"><HeaderTemplate><asp:Label ID="lblCurrentAddressHeader" runat="server" Text="ที่อยู่ปัจจุบัน" ForeColor="Black"></asp:Label></HeaderTemplate><ContentTemplate>
                                    
                                    
                                        <uc1:ucAddress ID="ucAddressCurrent" runat="server" />
                                    
                                    
                                    </ContentTemplate></ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabRegisterAddress" runat="server">
                                    <HeaderTemplate><asp:Label ID="lblRegisterAddressHeader" runat="server" Text="ที่อยู่ตามทะเบียนบ้าน"
                                                ForeColor="Black"></asp:Label></HeaderTemplate><ContentTemplate>
                                                    <uc1:ucAddress ID="ucAddressRegister" runat="server" />
                                                    <br />
                                                    <asp:CheckBox ID="chkCopyAdd" runat="server" Text="เหมือนที่อยู่ปัจจุบัน" 
                                                            oncheckedchanged="chkCopyAdd_CheckedChanged" AutoPostBack="True"/>
                                                </ContentTemplate></ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                    </table>
                        </ContentTemplate>
 
            </asp:UpdatePanel>
        </asp:Panel>
</asp:Content>
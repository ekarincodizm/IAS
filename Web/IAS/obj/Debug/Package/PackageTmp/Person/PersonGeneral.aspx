<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPerson.master"
    AutoEventWireup="true" CodeBehind="PersonGeneral.aspx.cs" Inherits="IAS.Person.PersonGeneral" %>

<%@ Register Src="~/UserControl/ucAddress.ascx" TagName="ucAddress" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //    $(function setdate() {
        //        $('#txtBirthDay').attr('readOnly', 'true');
        //        //$('#txtBirthDay').keypress(function (event) { event.preventDefault(); });
        //    });

        //        $("#datepicker").datepicker({ dateFormat: "dd/MM/yyyy" });
        //        $("#datepicker").datepicker("option", "minDate", -1);
        //        $("#datepicker").datepicker("option", "maxDate", -2);
        //        $("#datepicker").datepicker({ minDate: -1, maxDate: -2 }).attr('readonly', 'readonly');     
        //        $('#datepickerBirth').datepicker({
        //            inline: true,
        //            changeYear: true,
        //            changeMonth: true,
        //            dateFormat: 'MM dd, yy',
        //            showOtherMonths: true
        //        });
        //        $("#datepickerBirth").datepicker("setDate", "10/12/1986");

        //        $(document).ready(function () {
        //            $('#<%=txtIDNumber.ClientID %>').bind('keydown', function () {
        //                checkKeyNumberFixPoint(event, this, 13);
        //            });
        //            $('#<%=txtIDNumber.ClientID %>').bind('blur', function () {
        //                checkUser(event, this, 13);
        //            });
        //            $('#<%=txtIDNumber.ClientID %>').bind('keypress', function () {
        //                checkKeyNumberKeyPressFixPoint(event, this, 13);
        //            });

        //            $('#<%=txtEmail.ClientID %> input').bind('keypress', function (e) {
        //                $('#<%=txtTel.ClientID %>').focus();
        //            });

        //         
        //        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Detail" runat="server">
    <script type="text/javascript">
        //    $(document).ready(function () {
        //        $('#txtIDNumber').bind('keydown', function () {
        //            checkKeyNumberFixPoint(event, this, 13);
        //        });
        //        $('#txtIDNumber').bind('blur', function () {
        //            checkUser(event, this, 13);
        //        });
        //        $('#txtIDNumber').bind('keypress', function () {
        //            checkKeyNumberKeyPressFixPoint(event, this, 13);
        //        });
        //    });
        function fncInputNumericValuesOnly() {
            if (!(event.keyCode == 45 ||
                event.keyCode == 46 ||
                event.keyCode == 48 ||
                event.keyCode == 49 ||
                event.keyCode == 50 ||
                event.keyCode == 51 ||
                event.keyCode == 52 ||
                event.keyCode == 53 ||
                event.keyCode == 54 ||
                event.keyCode == 55 ||
                event.keyCode == 56 ||
                event.keyCode == 57 ||
                event.keyCode == 8 ||
                event.keyCode == 190 ||
                event.keyCode == 37 ||
                event.keyCode == 39 ||
                event.keyCode == 16)) {
                event.returnValue = false;
            }

        }
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

    </script>
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label ID="lblhead" runat="server"></asp:Label>
            </div>
        </div>
        <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box_body_content">
                        <div class="box_t_h">
                        </div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="6" style="text-align: center; width: 100%;">
                                            <asp:Label ID="lblMessage" runat="server" CssClass="lbl" Font-Bold="true" ForeColor="Red"
                                                Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTypeMember" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <%--<asp:TextBox ID="txtTypeMember" runat="server" CssClass="txtreadonly" Enabled="false"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtMemberType" runat="server" CssClass="txtreadonly" Enabled="false"
                                                Width="170"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <%--<tr>
                            <td style="text-align: right;">
                                <asp:Label ID="lblAgentType" runat="server" CssClass="lbl" Text="ประเภทตัวแทนสมาชิก"></asp:Label>:
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlAgentType" runat="server" CssClass="ddl" ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqAgentType" runat="server" ControlToValidate="ddlAgentType"
                                                    ErrorMessage="กรุณาเลือกประเภทตัวแทน" CssClass="failureNotification" ToolTip="กรุณาเลือกประเภทตัวแทน"
                                                    ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>--%>
                                    <tr>
                                        <td style="text-align: right;">
                                         <asp:Label ID="lblIDNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                        <asp:TextBox ID="txtIDNumber" ClientIDMode="Static" runat="server" autocomplete="off"
                                                CssClass="txt txtidcard" MaxLength="13" Width="170px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="IDNumberRequired" runat="server" ControlToValidate="txtIDNumber"
                                                ErrorMessage="กรุณากรอกเลขบัตรประชาชน" CssClass="failureNotification" ToolTip="กรุณากรอกเลขบัตรประชาชน"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="reqRegIDNumber" runat="server" ErrorMessage="เลขบัตรประชาชนไม่ถูกต้อง"
                                                ControlToValidate="txtIDNumber" ValidationExpression="^[0-9]{13}?$" ValidationGroup="regGuestValidationGroup"
                                                CssClass="failureNotification">
                                                <asp:Label ID="Label2" runat="server" Visible="false">*</asp:Label></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right;">
                                        </td>
                                        <td style="text-align: left;">
                                        </td>
                                        <td style="text-align: right;">
                                        </td>
                                        <td style="text-align: left;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ddl" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlTitle"
                                                ErrorMessage="กรุณาเลือกคำนำหน้าชื่อ" CssClass="failureNotification" ToolTip="กรุณาเลือกคำนำหน้าชื่อ"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" autocomplete="off" MaxLength="50"
                                                onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName"
                                                CssClass="failureNotification" ToolTip="กรุณากรอกชื่อ" ErrorMessage="กรุณากรอกชื่อ"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REV1" Display="Dynamic" runat="server" ControlToValidate="txtFirstName"
                                                ErrorMessage="รูปแบบชื่อไม่ถูกต้อง" CssClass="failureNotification" ValidationGroup="regGuestValidationGroup"
                                                ValidationExpression="^[ก-ฮ|a-zA-Z|\p{L}|\p{M}]{1,50}?$">
                                                <asp:Label ID="Label6" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" autocomplete="off" MaxLength="70"
                                                onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                                                CssClass="failureNotification" ToolTip="กรุณากรอกนามสกุล" ErrorMessage="กรุณากรอกนามสกุล"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic"
                                                runat="server" ControlToValidate="txtLastName" ErrorMessage="รูปแบบนามสกุลไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" ValidationExpression="^[ก-ฮ|a-zA-Z|\s|\p{L}|\p{M}]{1,70}?$">
                                                <asp:Label ID="Label7" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                               <asp:Label ID="lblBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                             <asp:TextBox ID="txtBirthDay" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgPopup_txtBirthDay" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                runat="server" />
                                            <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDay" PopupButtonID="imgPopup_txtBirthDay"
                                                runat="server" TargetControlID="txtBirthDay" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="BirthDayRequired" runat="server" ControlToValidate="txtBirthDay"
                                                ErrorMessage="กรุณาเลือกวัน เดือน ปี เกิด" CssClass="failureNotification" ToolTip="กรุณาเลือกวัน เดือน ปี เกิด"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right;">
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
                                        <td style="text-align: right;">
                                            
                                        </td>
                                        <td style="text-align: left;">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblNationality" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlNationality" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="NationalityRequired" runat="server" ControlToValidate="ddlNationality"
                                                ErrorMessage="กรุณาเลือกสัญชาติ" CssClass="failureNotification" ToolTip="กรุณาเลือกสัญชาติ"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlEducation" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="EducationRequired" runat="server" ControlToValidate="ddlEducation"
                                                ErrorMessage="กรุณาเลือกระดับการศึกษา" CssClass="failureNotification" ToolTip="กรุณาเลือกระดับการศึกษา"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" autocomplete="off" MaxLength="255"
                                                onkeypress="return Input_Eng(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="กรุณากรอกอีเมล" CssClass="failureNotification" ToolTip="กรุณากรอกอีเมล"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                            <!--Email ValidationExpression Nattapong@22/8/2556-->
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic"
                                                runat="server" ControlToValidate="txtEmail" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                <asp:Label ID="Label3" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTel" runat="server" Text="โทรศัพท์" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtTel" runat="server" CssClass="txt-tel" MaxLength="9" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                                            <asp:Label ID="lblTelExt" runat="server" CssClass="lbl" Text="ต่อ"></asp:Label>
                                            <asp:TextBox ID="txtTelExt" runat="server" CssClass="txt-tel-ext" MaxLength="4" onkeypress="fncInputNumericValuesOnly()"
                                                onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="GeneralTelRequired" runat="server" ControlToValidate="txtTel"
                                                ErrorMessage="กรุณากรอกเบอร์โทรศัพท์" CssClass="failureNotification" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="รูปแบบโทรศัพท์ไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="regGuestValidationGroup" ControlToValidate="txtTel"
                                                ValidationExpression="^0[0-9]{8}">
                                                <asp:Label ID="Label4" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblMobilePhone" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="txt" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="MobilePhoneRequired" runat="server" ControlToValidate="txtMobilePhone"
                                                ErrorMessage="กรุณากรอกเบอร์โทรศัพท์มือถือ" CssClass="failureNotification" ToolTip="กรุณากรอกเบอร์โทรศัพท์มือถือ"
                                                ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" CssClass="failureNotification"
                                                ValidationGroup="regGuestValidationGroup" ControlToValidate="txtMobilePhone"
                                                ErrorMessage="รูปแบบโทรศัพท์มือถือไม่ถูกต้อง" ValidationExpression="^0[0-9]{9}">
                                                <asp:Label ID="Label5" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" colspan="6">
                                            <br />
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
                                            <ajaxToolkit:TabContainer ID="TabAddress" runat="server" Width="100%" CssClass="ajax_tabs"
                                                ActiveTabIndex="0" AutoPostBack="True" OnActiveTabChanged="TabAddress_ActiveTabChanged">
                                                <ajaxToolkit:TabPanel ID="TabCurrentAddress" runat="server">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCurrentAddressHeader" runat="server" Text="ที่อยู่ปัจจุบัน" ForeColor="Black">
                                                        </asp:Label>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <uc1:ucAddress ID="ucAddressCurrent" runat="server" />
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel ID="TabRegisterAddress" runat="server">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblRegisterAddressHeader" runat="server" Text="ที่อยู่ตามทะเบียนบ้าน"
                                                            ForeColor="Black"></asp:Label></HeaderTemplate>
                                                    <ContentTemplate>
                                                        <uc1:ucAddress ID="ucAddressRegister" runat="server" />
                                                        <br />
                                                        <asp:CheckBox ID="chkCopyAdd" runat="server" Text="เหมือนที่อยู่ปัจจุบัน" OnCheckedChanged="chkCopyAdd_CheckedChanged"
                                                            AutoPostBack="True" />
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <% if (false)
           { %>
    </div>
    <%} %>
</asp:Content>

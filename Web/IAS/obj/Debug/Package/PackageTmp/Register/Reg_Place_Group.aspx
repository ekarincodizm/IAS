<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Reg_Place_Group.aspx.cs" Inherits="IAS.Register.Reg_Place_Group" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

        function confirmSave() {
            return confirm('คุณต้องการบันทึกข้อมูลหรือไม่');
        }

        //        function checkUser() {
        //            $.ajax({
        //                url: "../Service/RegistrationWebService.asmx/VerifyIdCard",
        //                data: "{ strIdCard : '" + $('#<%= txtIDNumber.ClientID %>').val() + "' }",
        //                dataType: "json",
        //                type: "POST",
        //                contentType: "application/json; charset=utf-8",
        //                success: function (data) {
        //                    if (data == null) {
        //                        alert("nothing.");
        //                    }
        //                    else {
        //                        if (data.d == true) {
        //                        }
        //                        else {
        //                            alert("กรุณากรอกเลขบัตรประชาชนให้ถูกต้อง");
        //                        }
        //                    }
        //                }
        //            });
        //        }

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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            } else {
                return true;
            }
        }
        function Input_Eng(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 3585) {
                return false;
            } else {
                return true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ลงทะเบียน (เจ้าหน้าที่กลุ่มสนามสอบ)" BorderStyle="None" ID="lblRegister"
                    runat="server" />
            </div>
        </div>
        <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
            <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
            <asp:UpdatePanel ID="UpdatePanelUpload" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::กลุ่มสนามสอบ</div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: right; width: 15%;">
                                            <asp:Label ID="lblCompany" runat="server" Text="หน่วยงานจัดสอบ" CssClass="lbl"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;" colspan="5">
                                            <asp:DropDownList ID="ddlPlaceGroup" runat="server" AutoPostBack="True" CssClass="ddl"
                                                Width="150px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="CompanyRequired" runat="server" ControlToValidate="ddlPlaceGroup"
                                                ErrorMessage="กรุณาเลือกหน่วยงานจัดสอบ" CssClass="failureNotification" ToolTip="กรุณาเลือกหน่วยงานจัดสอบ"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblCompanyAddress1" runat="server" Text="ที่อยู่ 1" CssClass="lbl"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left; width: 20%;" rowspan="3">
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="txt" Height="70px" TextMode="MultiLine"
                                                MaxLength="60"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="CompanyAddress1Required" runat="server" ControlToValidate="txtAddress"
                                                ErrorMessage="กรุณากรอกที่อยู่" CssClass="failureNotification" ToolTip="กรุณากรอกที่อยู่"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="valInput" ControlToValidate="txtAddress"
                                                ValidationExpression="^[ก-๙|0-9a-zA-Z|\s|.|(|)|-|\S|/]{1,100}?$" ErrorMessage="รูปแบบที่อยู่ไม่ถูกต้อง"
                                                CssClass="failureNotification" Display="Dynamic" ValidationGroup="PlaceGroupValidationGroup">
                                                <asp:Label ID="Label1" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right; width: 10%;">
                                            <asp:Label ID="lblDistrict" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" CssClass="ddl"
                                                Width="150px" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="DistrictRequired" runat="server" ControlToValidate="ddlProvince"
                                                ErrorMessage="กรุณาเลือกจังหวัด" CssClass="failureNotification" ToolTip="กรุณาเลือกจังหวัด"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            &nbsp;<span><asp:Label ID="lblParish" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" CssClass="ddl"
                                                Width="150px" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ParishRequired" runat="server" ControlToValidate="ddlDistrict"
                                                ErrorMessage="กรุณาเลือกเขต/อำเภอ" CssClass="failureNotification" ToolTip="กรุณาเลือกเขต/อำเภอ"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; height: 17px;">
                                        </td>
                                        <td style="text-align: right; height: 17px;">
                                            &nbsp;<asp:Label ID="lblCompanyPostcode" runat="server" CssClass="lbl" Text="แขวง/ตำบล"></asp:Label>:
                                        </td>
                                        <td style="text-align: left; height: 17px;">
                                            <asp:DropDownList ID="ddlParish" runat="server" AutoPostBack="True" CssClass="ddl"
                                                Width="150px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="CompanyPostcodeRequired" runat="server" ControlToValidate="ddlParish"
                                                ErrorMessage="กรุณาเลือกแขวง/ตำบล" CssClass="failureNotification" ToolTip="กรุณาเลือกแขวง/ตำบล"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblCompanyPostcode0" runat="server" CssClass="lbl" Text="รหัสไปรษณีย์"></asp:Label>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPostcode" runat="server" CssClass="txt" MaxLength="5" onkeypress="return isNumberKey(event);"
                                                onkeyup="this.value=this.value.replace(/^0.*$|[^0-9]/g,'');"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="CompanyPostcodeRequired0" runat="server" ControlToValidate="txtPostcode"
                                                ErrorMessage="กรุณากรอกรหัสไปรษณีย์" CssClass="failureNotification" ToolTip="กรุณากรอกรหัสไปรษณีย์"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="รูปแบบรหัสไปรษณีย์ไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup" ControlToValidate="txtPostcode"
                                                ValidationExpression="[1-9]{1}[0-9]{4}">
                                                <asp:Label ID="Label11" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblCompanyTel" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์"></asp:Label>
                                                : </span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtTel" runat="server" CssClass="txt" MaxLength="9" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                            <asp:Label ID="lblTelExt" runat="server" CssClass="lbl" Text="ต่อ"></asp:Label>
                                            <asp:TextBox ID="txtTelExt" runat="server" CssClass="txt-tel-ext" MaxLength="4" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="CompanyTelRequired" runat="server" ControlToValidate="txtTel"
                                                ErrorMessage="กรุณาระบุโทรศัพท์" CssClass="failureNotification" ToolTip="กรุณาระบุโทรศัพท์"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="รูปแบบโทรศัพท์ไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup" ControlToValidate="txtTel"
                                                ValidationExpression="^0[0-9]{8}">
                                                <asp:Label ID="Label4" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::ข้อมูลเจ้าหน้าที่กลุ่มสนามสอบ</div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: right; width: 15%;">
                                            <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtMemberType" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 10%;">
                                        </td>
                                        <td style="text-align: left;">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblIDCard" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtIDNumber" ClientIDMode="Static" runat="server" autocomplete="off"
                                                CssClass="txt txtidcard" MaxLength="13" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="IDCardRequired" runat="server" ControlToValidate="txtIDNumber"
                                                ErrorMessage="กรุณาระบุเลขบัตรประชาชน" CssClass="failureNotification" ToolTip="กรุณาระบุเลขบัตรประชาชน"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="reqRegIDNumber" runat="server" ControlToValidate="txtIDNumber"
                                                ValidationExpression="^[0-9]{13}?$" ValidationGroup="PlaceGroupValidationGroup"
                                                ErrorMessage="รูปแบบเลขบัตรประชาชนไม่ถูกต้อง" CssClass="failureNotification">
                                                <asp:Label ID="Label5" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right;">
                                        </td>
                                        <td style="text-align: left;">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                :</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ddl" Width="150px" AutoPostBack="true"
                                                onselectedindexchanged="ddlTitle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlTitle"
                                                ErrorMessage="กรุณาเลือกคำนำหน้าชื่อ" CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" MaxLength="50" onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName"
                                                ErrorMessage="กรุณาระบุชื่อ" CssClass="failureNotification" ToolTip="กรุณาระบุชื่อ"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="REV1" Display="Dynamic" runat="server" ControlToValidate="txtFirstName"
                                                ErrorMessage="รูปแบบชื่อไม่ถูกต้อง" CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup"
                                                ValidationExpression="^[ก-๙|a-zA-Z|]{1,50}?$">
                                                <asp:Label ID="Label6" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" MaxLength="70" onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                                                ErrorMessage="กรุณาระบุนามสกุล" CssClass="failureNotification" ToolTip="กรุณาระบุนามสกุล"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic"
                                                runat="server" ControlToValidate="txtLastName" ErrorMessage="รูปแบบนามสกุลไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup" ValidationExpression="^[ก-๙|a-zA-Z' ']{1,70}?$">
                                                <asp:Label ID="Label7" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; vertical-align: top;">
                                            <asp:Label ID="lblStartDate0" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left; vertical-align: top;">
                                            <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" Enabled="false">
                                                <asp:ListItem Text="ชาย" Value="M" ></asp:ListItem>
                                                <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="text-align: right; vertical-align: top;">
                                            <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                            :
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtEmail" runat="server" onkeypress="return Input_Eng(event);" CssClass="txt"
                                                MaxLength="255"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="กรุณาระบุอีเมล" CssClass="failureNotification" ToolTip="กรุณาระบุอีเมล"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic"
                                                runat="server" ControlToValidate="txtEmail" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                                <asp:Label ID="Label3" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblTel" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                                            :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPlaceGroupTel" runat="server" CssClass="txt" MaxLength="10" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="TelRequired" runat="server" ControlToValidate="txtPlaceGroupTel"
                                                ErrorMessage="กรุณาระบุเบอร์โทรศัพท์มือถือ" CssClass="failureNotification" ToolTip="กรุณาระบุเบอร์โทรศัพท์มือถือ"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="รูปแบบเบอร์โทรศัพท์มือถือไม่ถูกต้อง"
                                                CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup" ControlToValidate="txtPlaceGroupTel"
                                                ValidationExpression="^0[0-9]{9}">
                                                <asp:Label ID="Label2" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr id="divPassword" runat="server">
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblPassword" runat="server" CssClass="lbl" Text="รหัสผ่าน"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txt" MaxLength="13" TextMode="Password"
                                                autocomplete="off"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="กรุณาระบุรหัสผ่าน" CssClass="failureNotification" ToolTip="กรุณาระบุรหัสผ่าน"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="reqRegPW1" Display="Dynamic" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="รหัสผ่านต้องมีความยาวอย่างน้อย 8 ตัวอักษร แต่ไม่เกิน 14 ตัวอักษร<br/>ประกอบด้วยตัวอักษรพิมพ์ใหญ่, ตัวอักษรพิมพ์เล็ก, ตัวเลขและ<br/>อักขระที่อนุญาตให้ใช้งานได้คือ @ # $ % ^ & * ( ) { } + ? , _ เท่านั้น"
                                                CssClass="failureNotification" ValidationGroup="PlaceGroupValidationGroup" ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$">
                                                <asp:Label ID="Label8" runat="server" Visible="false">*</asp:Label>
                                            </asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblIDCard2" runat="server" CssClass="lbl" Text="ยืนยันรหัสผ่าน"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txt" MaxLength="13"
                                                autocomplete="off" TextMode="Password"></asp:TextBox>
                                            <%-- <asp:RegularExpressionValidator ID="reqRegPW2" Display="Dynamic"
                              runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="รหัสผ่านต้องมีความยาวอย่างน้อย 6 ตัวอักษร แต่ไม่เกิน 14 ตัวอักษร" CssClass="failureNotification"
                              ValidationGroup="PlaceGroupValidationGroup" ValidationExpression="^([a-z]|[A-Z]|[!@&#]|[0-9]){6,14}$"><asp:Label ID="Label9" runat="server" visible="false">*</asp:Label>
                             </asp:RegularExpressionValidator>--%>
                                            <asp:RequiredFieldValidator ID="IDCardRequired2" runat="server" ControlToValidate="txtConfirmPassword"
                                                ErrorMessage="กรุณาระบุยืนยันรหัสผ่าน" CssClass="failureNotification" ToolTip="กรุณาระบุยืนยันรหัสผ่าน"
                                                ValidationGroup="PlaceGroupValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="reqComparePW" runat="server" Display="Dynamic" ControlToValidate="txtPassword"
                                                ControlToCompare="txtConfirmPassword" ValidationGroup="PlaceGroupValidationGroup"
                                                ErrorMessage="รหัสผ่านไม่ตรงกัน" CssClass="failureNotification">
                                                <asp:Label ID="Label10" runat="server" Visible="false">*</asp:Label>
                                            </asp:CompareValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                    <asp:Panel ID="PnlAddButton" runat="server" Visible="true">
                        <table align="center" style="width: 100%;">
                            <tr>
                                <td style="text-align: center;" colspan="2">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="ตกลง" OnClick="btnSubmit_Click"
                                        CausesValidation="false" ValidationGroup="PlaceGroupValidationGroup" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClientClick="javascript:return confirm('ต้องการยกเลิกใช่หรือไม่?');"
                                        OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PnlEditButton" runat="server" Visible="false">
                        <table align="center" style="width: 100%;">
                            <tr>
                                <td style="text-align: center;" colspan="2">
                                    &nbsp;<asp:Button ID="btnEditSubmit" runat="server" CssClass="btn" Text="ตกลง" ValidationGroup="PlaceGroupValidationGroup"
                                        CausesValidation="false" OnClick="btnEditSubmit_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnEditCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClientClick="javascript:return confirm('ต้องการยกเลิกใช่หรือไม่?');"
                                        OnClick="btnEditCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <uc2:UCModalError ID="UCModalError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                </ContentTemplate>
                <%-- <Triggers>
                <asp:PostBackTrigger ControlID="btnUploadFile" />
            </Triggers>--%>
            </asp:UpdatePanel>
        </asp:Panel>
        <br />
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

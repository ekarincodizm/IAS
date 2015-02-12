<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Reg_OIC.aspx.cs" Inherits="IAS.Register.regOfficerOIC1" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            } else {
                return true;
            }
        } 
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ลงทะเบียน (เจ้าหน้าที่ คปภ.)" BorderStyle="None" ID="lblRegisterOfficer"
                    runat="server" />
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc2:UCModalError ID="UCModalError1" runat="server" />
                <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="box_body_content">
            <div class="box_t_h">
                ::เพิ่มสมาชิก</div>
            <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center;">
                                <span>
                                    <asp:Label ID="lblAssociation" runat="server" Text="ประเภทสมาชิก" CssClass="lbl"></asp:Label>
                                    :</span>
                                <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn" OnClick="btnAdd_Click" Text="เพิ่ม" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div runat="server" id="dvOIC" visible="false">
            <div class="box_body_content">
                <div class="box_t_h">
                </div>
                <div class="box_textbox_bgadd">
                    <div class="box_texbox_add_box">
                        <table style="width: 90%; margin: 0px auto 10px auto;">
                            <tr>
                                <td style="text-align: right; width: 13%;">
                                    <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtMemberType" runat="server" CssClass="txtreadonly">เจ้าหน้าที่ คปภ ตัวแทน</asp:TextBox>
                                </td>
                                <td style="text-align: right; width: 13%;">
                                    <asp:Label ID="lblStartDate3" runat="server" CssClass="lbl" Text="รหัสเจ้าหน้าที่ คปภ."></asp:Label>:
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtIDNumber" onkeypress="return isNumberKey(event);" MaxLength="6"
                                        runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <td style="text-align: right; width: 13%;">
                                    <asp:Label ID="lblAntecedent0" runat="server" CssClass="lbl" Text="ชื่อผู้ใช้ในระบบ AD"></asp:Label>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOICUserName" runat="server" CssClass="txt" MaxLength="15" onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblAntecedent" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlAntecedent" runat="server" CssClass="ddl" AutoPostBack="true"
                                        Width="150px" OnSelectedIndexChanged="ddlAntecedent_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="AntecedentRequired" runat="server" ControlToValidate="ddlAntecedent"
                                        CssClass="failureNotification" ValidationGroup="OICValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                    &nbsp;:
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" MaxLength="50" onkeypress="return charEngThai(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="g0" runat="server" ControlToValidate="txtFirstName"
                                        CssClass="failureNotification" ValidationGroup="OICValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right; height: 17px;">
                                    <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left; height: 17px;">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" MaxLength="70" onkeypress="return charEngThai(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="g" runat="server" ControlToValidate="txtLastName"
                                        CssClass="failureNotification" ValidationGroup="OICValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;" valign="top">
                                    <asp:Label ID="lblStartDate0" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>:
                                </td>
                                <td style="text-align: left;" valign="top">
                                    <asp:RadioButtonList ID="rblSex" runat="server" CssClass="rb" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="ชาย" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblSex"
                                        CssClass="failureNotification" ValidationGroup="OICValidationGroup"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" valign="top">
                                    <asp:Label ID="lbl_licent" runat="server" CssClass="lbl" Text="ลายเซ็น :"></asp:Label>
                                </td>
                                <td valign="top">
                                    <asp:FileUpload runat="server" ID="fulSignature" />
                                    <br />
                                    <asp:Label ID="lblDescription" runat="server" Text="ลายเซ็นต้องเป็นไฟล์ png เท่านั้น <br/> ขนาดไม่เกิน 500 k"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="text-align: center">
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                        Text="ตกลง" ValidationGroup="OICValidationGroup" />
                                    &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClientClick="javascript:return confirm('ต้องการยกเลิกใช่หรือไม่?');"
                                        OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <asp:ValidationSummary ID="regOICValidationSummary" runat="server" CssClass="failureNotification"
                                    ValidationGroup="OICValidationGroup" HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowSummary="False"
                                    ShowMessageBox="true" />
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="PersonOIC.aspx.cs" Inherits="IAS.Person.PersonOIC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="" BorderStyle="None" ID="lblRegisterOfficer" runat="server" />
            </div>
        </div>
        <div runat="server" id="dvOIC">
            <div class="box_body_content">
                <div class="box_t_h">
                </div>
                <div class="box_textbox_bgadd">
                    <div class="box_texbox_add_box">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="width: 90%; margin: 0px auto 10px auto;">
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <span>
                                                <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                                :</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <%--เจ้าหน้าที่ คปภ ตัวแทน--%>
                                            <asp:TextBox ID="txtMemberType" runat="server" Enabled="false" CssClass="txtreadonly"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <span>
                                                <asp:Label ID="lblAntecedent0" runat="server" CssClass="lbl" Text="ชื่อผู้ใช้ในระบบ AD"></asp:Label>
                                                :</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtOICUserName" runat="server" Enabled="false" CssClass="txtreadonly"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblStartDate3" runat="server" CssClass="lbl" Text="รหัสเจ้าหน้าที่ คปภ."></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtIDNumber" runat="server" Enabled="false" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            &nbsp;<span><asp:Label ID="lblAntecedent" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                : </span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:DropDownList ID="ddlAntecedent" runat="server" CssClass="ddl" Width="150px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="AntecedentRequired" runat="server" ControlToValidate="ddlAntecedent"
                                                CssClass="failureNotification" ValidationGroup="OICValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblStartDate0" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:RadioButtonList ID="rblSex" runat="server" CssClass="rb" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="ชาย" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%; height: 17px;">
                                            &nbsp;<span><asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                :</span>
                                        </td>
                                        <td style="text-align: left; width: 30%; height: 17px;">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="g" runat="server" ControlToValidate="txtFirstName"
                                                CssClass="failureNotification" ValidationGroup="OICValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right; width: 20%; height: 17px;">
                                            &nbsp;<asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left; width: 30%; height: 17px;">
                                            &nbsp;<asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                                            &nbsp;<asp:RequiredFieldValidator ID="g0" runat="server" ControlToValidate="txtLastName"
                                                CssClass="failureNotification" ValidationGroup="OICValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <asp:Label ID="lblSign" runat="server" Text="ลายเซ็น :"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Image ID="ImgSign" runat="server" Width="150px" Height="100px" />
                                            <asp:FileUpload ID="FileSign" runat="server" />
                                            <br />
                                            <asp:Label ID="lblDescription" runat="server" Text="ลายเซ็นต้องเป็นไฟล์ png เท่านั้น <br/> ขนาดไม่เกิน 500 k"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                        <td align="right">
                                        </td>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%; height: 17px;">
                                        </td>
                                        <td style="text-align: right; width: 30%; height: 17px;">
                                            &nbsp;
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" OnClick="btnSubmit_Click"
                                                Text="ตกลง" ValidationGroup="OICValidationGroup" />
                                        </td>
                                        <td style="text-align: left; width: 20%; height: 17px;">
                                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClientClick="javascript:return confirm('ต้องการยกเลิกใช่หรือไม่?');"
                                                OnClick="btnCancel_Click" />
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; width: 30%; height: 17px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <asp:ValidationSummary ID="regOICValidationSummary" runat="server" CssClass="failureNotification"
                                            ValidationGroup="OICValidationGroup" HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowSummary="False"
                                            ShowMessageBox="true" />
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="LicenseReplaceRename.aspx.cs" Inherits="IAS.License.LicenseReplaceRename" %>
<%@ Register Src="~/UserControl/ucReplaceLicense.ascx" TagName="ucReplace" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
<Triggers>
    <asp:PostBackTrigger ControlID="btnNext" />
</Triggers>
<ContentTemplate>
    <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center">
        <table width="100%">
            <tr>
                <td>
                </td
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table width="80%">
                        <tr>
                            <td align="left">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <fieldset>
                                            <legend><asp:Label ID="Label1" runat="server" Text="ชื่อ-สกุลใหม่" CssClass="lbl"></asp:Label></legend>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:100px;">
                                                            <asp:Label ID="lblFirstName" runat="server" Text="ชื่อ : " CssClass="lbl"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName"
                                                                                ErrorMessage="กรุณากรอกชื่อ" CssClass="failureNotification" ToolTip="กรุณากรอกชื่อ"
                                                                                ValidationGroup="ReplaceRenameValidationGroup">*</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="REV1" Display="Dynamic" runat="server" ControlToValidate="txtFirstName"
                                                                                ErrorMessage="รูปแบบชื่อไม่ถูกต้อง" CssClass="failureNotification" ValidationGroup="ReplaceRenameValidationGroup"
                                                                                ValidationExpression="^[ก-ฮ|a-zA-Z|\p{L}|\p{M}]{1,50}?$">
                                                                                <asp:Label ID="Label6" runat="server" Visible="false">*</asp:Label>
                                                             </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:100px;">
                                                            <asp:Label ID="lblLastName" runat="server" Text="นามสกุล : " CssClass="lbl"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                                                                                ErrorMessage="กรุณากรอกนามสกุล" CssClass="failureNotification" ToolTip="กรุณากรอกนามสกุล"
                                                                                ValidationGroup="ReplaceRenameValidationGroup">*</asp:RequiredFieldValidator>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic"
                                                                                runat="server" ControlToValidate="txtLastName" ErrorMessage="รูปแบบนามสกุลไม่ถูกต้อง"
                                                                                CssClass="failureNotification" ValidationGroup="ReplaceRenameValidationGroup" ValidationExpression="^[ก-ฮ|a-zA-Z|\s|\p{L}|\p{M}]{1,70}?$">
                                                                                <asp:Label ID="Label7" runat="server" Visible="false">*</asp:Label>
                                                             </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <uc1:ucReplace ID="ucReplaceLi" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width:40%; margin: 0px auto 10px auto;">
                                            <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Images/button_next.png" onclick="btnNext_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

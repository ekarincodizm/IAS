<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="IAS.UI.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">
        function isValid() {
            var txtUserName = document.getElementById('<%=txtUserName.ClientID %>');
            var txtPassword = document.getElementById('<%=txtPassword.ClientID%>');
            var lblMsg = document.getElementById('<%=lblMsg.ClientID %>');
            lblMsg.innerHTML = "";

            if (txtUserName != "") {
                if (txtPassword != "") {
                    return true;
                }
                else {
                    lblMsg.innerHTML = "Please enter Password";
                    return false;
                }
            }
            else {
                lblMsg.innerHTML = "Please enter UserName";
                return false;
            }

        }

    </script>
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <table style="width: 100%;">
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: right; width: 10%;">
                        <asp:Label ID="lblUserName" runat="server" Text="ชื่อผู้ใช้งาน" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtUserName" runat="server" autocomplete="off" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: right; width: 10%;">
                        <asp:Label ID="lblPassword" runat="server" Text="รหัสผ่าน" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" autocomplete="off" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: left;">
                        <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" OnClientClick="javascript:isValid(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: left; width: 14%">
                        <asp:HyperLink ID="HplRegister" runat="server" Text="สมัครสมาชิก" CssClass="lbl" NavigateUrl="http://www.ar.co.th/"></asp:HyperLink>
                    </td>
                    <td style="text-align: left;">
                        <asp:HyperLink ID="HplForgetPassword" runat="server" Text="ลืมรหัสผ่าน" CssClass="lbl" NavigateUrl="http://www.ar.co.th/"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_ID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

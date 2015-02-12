<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="IAS.Mockup.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <table style="width: 100%;">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: right; width: 10%;">
                            <asp:Label ID="lblUserName" runat="server" Text="ชื่อผู้ใช้งาน"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtUserName" runat="server" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: right; width: 10%;">
                            <asp:Label ID="lblPassword" runat="server" Text="รหัสผ่าน"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: left;">
                            <asp:Button ID="btnOk" runat="server" Text="ตกลง" Width="100px" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: left;">
                            <asp:HyperLink ID="HplRegister" runat="server" Text="สมัครสมาชิก" NavigateUrl="http://www.ar.co.th/"></asp:HyperLink>
                        </td>
                        <td style="text-align: left;">
                            <asp:HyperLink ID="HplForgetPassword" runat="server" Text="ลืมรหัสผ่าน" NavigateUrl="http://www.ar.co.th/"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoLogout.aspx.cs" Inherits="IAS.AutoLogout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ออกจากระบบ</title>
    <link href="style/main.css" rel="stylesheet" type="text/css" />
    <link href="style/menu.css" rel="stylesheet" type="text/css" />
    <link href="style/content.css" rel="stylesheet" type="text/css" />
    <link href="style/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <link href="style/UCStyle.css" rel="stylesheet" type="text/css" />
    <link href="style/PopupPageStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblResultLogout" runat="server" Text="คุณได้ออกจากระบบเรียบร้อยแล้ว" ForeColor="Green"  CssClass="lbl" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnClose" runat="server" Text="ปิดหน้านี้" 
                        onclick="btnClose_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

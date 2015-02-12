<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupApp.aspx.cs" Inherits="IAS.Mockup.GroupApp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" 
            onclick="btnSearch_Click" /><br />
        <asp:TextBox ID="txtIdCard" runat="server"></asp:TextBox>
        <asp:Button ID="btnIdCard" runat="server" Text="Check IdCard" 
            onclick="btnIdCard_Click" />
        <asp:GridView ID="gv" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>

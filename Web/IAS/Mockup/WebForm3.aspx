<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="IAS.Mockup.WebForm3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btn" runat="server" Text="Testing AD" onclick="btn_Click" />
        <br />
        <asp:Label ID="lblName" runat="server" /><br />
        <asp:Label ID="lblLastName" runat="server" /><br />
        <asp:Label ID="lblMail" runat="server" /><br />
        <asp:GridView ID="gv" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>

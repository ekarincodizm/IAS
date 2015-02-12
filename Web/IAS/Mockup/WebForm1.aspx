<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="IAS.Mockup.WebForm1" %>

<%@ Register src="WebUserControl1.ascx" tagname="WebUserControl1" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <uc1:WebUserControl1 ID="uc1" runat="server" />
        <br />
        <br />
        <asp:Label ID="lbl" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btn1" runat="server" Text="Show Value1" onclick="btn1_Click" />
        <asp:Button ID="btn2" runat="server" Text="Show Value2" onclick="btn2_Click" />
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebTestOracleStoreProc.aspx.cs" Inherits="IAS.Mockup.WebTestOracleStoreProc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnTest" runat="server" Text="Test Store Proc" 
            onclick="btnTest_Click" />
        <asp:Button ID="btnTest2" runat="server" Text="Get Group Place" 
            onclick="btnTest2_Click" />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <br />
    </div>
    </form>
</body>
</html>

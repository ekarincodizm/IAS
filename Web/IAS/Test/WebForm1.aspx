<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="IAS.Test.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnShow" runat="server" Text="show" onclick="btnShow_Click"/><br />
            <asp:Button ID="btnPopup" runat="server" Text="Pop" onclick="btnPopup_Click" />
            <br />
            <br />
            <br />
            <asp:Label ID="lblDate" runat="server" Text="26082556"></asp:Label>&nbsp;
            <asp:Button ID="btnConvert" runat="server" Text="Convert" 
                onclick="btnConvert_Click" />
            <br />

            <asp:TextBox ID="txtDate1" runat="server"></asp:TextBox>&nbsp;date1<br />
            <asp:TextBox ID="txtDate2" runat="server"></asp:TextBox>&nbsp;date1<br />
        </div>
    </form>
</body>
</html>

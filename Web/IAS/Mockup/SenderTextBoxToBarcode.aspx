<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SenderTextBoxToBarcode.aspx.cs" Inherits="TestBarCode.SenderTextBoxToBarcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txt1" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btn1" runat="server" Text="Click" OnClick="btn1_Click" />
        </div>
    </form>
</body>
</html>

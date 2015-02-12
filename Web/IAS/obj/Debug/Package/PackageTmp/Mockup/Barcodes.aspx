<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Barcodes.aspx.cs" Inherits="TestBarCode.Barcodes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="myBarCode" runat="server"></asp:Image>
            <%--<br />
            <br />
            <asp:Label ID="lblBarCode" runat="server"></asp:Label>--%>
        </div>
    </form>
</body>
</html>

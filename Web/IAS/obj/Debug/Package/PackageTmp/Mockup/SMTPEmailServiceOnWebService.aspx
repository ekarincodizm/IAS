<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SMTPEmailServiceOnWebService.aspx.cs" Inherits="IAS.Mockup.SMTPEmailServiceOnWebService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
             <asp:Button runat="server" ID="btnSendMail" Text="Send" 
                 onclick="btnSendMail_Click" />
    </div>
    </form>
</body>
</html>

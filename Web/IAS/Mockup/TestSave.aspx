﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSave.aspx.cs" Inherits="IAS.Mockup.TestSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestDownload.aspx.cs" Inherits="IAS.Mockup.TestDownload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


input[type=submit], input[type=button]
{
    display: inline-block; /* width: 80px; */ /* padding: 5px; */
    border: 1px #D7D7D7 solid;
    text-align: center;
    text-decoration: none;
    color: #333333;
    background: url(../images/button_bg.png) repeat-x;
}
.btn
{
    font-family: Microsoft Sans Serif, AngsanaUPC;
    font-size: 14px;
    width: 80px;
    cursor: hand;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
                                    <asp:Button ID="btnDownload" runat="server" 
            Text="ดาวน์โหลด"  CssClass="btn"
                                        OnClick="btnDownload_Click" />
    
    </div>
    </form>
</body>
</html>

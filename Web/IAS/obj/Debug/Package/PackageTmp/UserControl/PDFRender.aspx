<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDFRender.aspx.cs" Inherits="IAS.UserControl.PDFRender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/Scripts01042013/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script type="text/javascript" >
        $(document).ready(function () {
            $("#image").attr('src', $('#<%= SourceImage.ID.ToString() %>').val());
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField runat="server" ID="SourceImage" Value="" />
        <img alt="" src="" id="image" name="image" />
    </div>
    </form>
   
</body>
</html>

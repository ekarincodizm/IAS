<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webTestAutoComplete.aspx.cs"
    Inherits="IAS.Mockup.webTestAutoComplete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="../style/jquery-ui-1.8.10.custom.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.9.2.js" type="text/javascript"></script>--%>

    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css"
        type="text/css" media="all" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.3/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js"
        type="text/javascript"></script>
    <script type="text/javascript">

        $(function () {
            $('#<%= txt.ClientID %>').autocomplete({
                source: "AutoComplete.ashx"
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txt" runat="server"></asp:TextBox>
        <asp:DropDownList ID="ddl" runat="server"></asp:DropDownList>
    </div>
    </form>
</body>
</html>

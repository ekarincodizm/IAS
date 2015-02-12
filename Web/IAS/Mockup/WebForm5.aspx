<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="IAS.Mockup.WebForm5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AutoComplete Box with jQuery</title>
    <link href="../style/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui-1.4.2.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var json = $('#hdf').val();
            $(".tb").autocomplete({
                source: eval("(" + json + ")"),
                minLength: 1
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="ui-widget">
            <asp:HiddenField ID="hdf" runat="server" />
            <label for="tbAuto">
                Enter Company :
            </label>
            <asp:TextBox ID="tbAuto" class="tb" runat="server" Width="300px">
            </asp:TextBox>
        </div>
    </div>
    </form>
</body>
</html>

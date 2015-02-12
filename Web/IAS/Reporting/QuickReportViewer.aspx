<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuickReportViewer.aspx.cs" Inherits="IAS.Reporting.QuickReportViewer" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body bgcolor="#BAC9DD">
    <form id="form1" runat="server">
    <center>
        <CR:CrystalReportViewer ID="cryViewer" runat="server" EnableDatabaseLogonPrompt="false"  HasToggleParameterPanelButton="false"
            AutoDataBind="true" HasCrystalLogo="False"  HasToggleGroupTreeButton="False" ToolPanelView="None" HasSearchButton="False"/>
    </center>
    </form>
</body>
</html>

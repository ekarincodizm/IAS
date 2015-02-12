<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LicenseStatisticsReportViewer.aspx.cs" Inherits="IAS.Reporting.LicenseStatisticsReportViewer" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=windows-874" />
    <title></title>
</head>
<body bgcolor="#BAC9DD">
    <form id="form1" runat="server">
    <center>
        <CR:CrystalReportViewer ID="RptReciveReportViewer" runat="server" AutoDataBind="true"
            HasCrystalLogo="False" HasToggleGroupTreeButton="False"  HasToggleParameterPanelButton="false"
            ToolPanelView="None" HasSearchButton="False" />
    </center>
    </form>
</body>
</html>

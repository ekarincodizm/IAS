<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopRegisterLicense.aspx.cs" Inherits="IAS.Reporting.PopRegisterLicense" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body bgcolor="#BAC9DD">
    <form id="form1" runat="server">
    <div>
        <center>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None"
                 HasToggleParameterPanelButton="false" HasToggleGroupTreeButton="false" HasSearchButton="False"/>

            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </center>
    </div>
    </form>
</body>
</html>

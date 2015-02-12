<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RcvPayment.aspx.cs" Inherits="IAS.Reporting.RcvPayment" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <CR:CrystalReportViewer ID="RptPaymentReportViewer" runat="server" AutoDataBind="true"
                    HasCrystalLogo="False" HasExportButton="False" HasToggleGroupTreeButton="False"
                    ToolPanelView="None" HasSearchButton="False" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

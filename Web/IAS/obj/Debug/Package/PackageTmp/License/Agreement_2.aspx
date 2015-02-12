<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agreement_2.aspx.cs" Inherits="IAS.License.Agreement_2" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
<%--<script src="/crystalreportviewers13/js/crviewer/crv.js"type="test/javascript"></script>--%>
<script type="text/javascript" src="/crystalreportviewers13/js/crviewer/crv.js"></script>
    <form id="form1" runat="server" >
    <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td>
                            <CR:CrystalReportViewer ID="LicenseCrystalReportViewer" runat="server" DisplayPage="true" DisplayStatusbar="false" DisplayToolbar="true" 
                            AutoDataBind="true" HasCrystalLogo="False" HasExportButton="False" HasToggleGroupTreeButton="False"
                            ToolPanelView="None" HasSearchButton="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

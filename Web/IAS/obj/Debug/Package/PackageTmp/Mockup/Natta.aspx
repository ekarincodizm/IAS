<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Natta.aspx.cs" Inherits="IAS.Mockup.Natta" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:ScriptManager ID="Scriptmanager1" runat="server" EnablePageMethods="true">
                    </asp:ScriptManager>
                    <CR:CrystalReportViewer ID="CrystalReportViewer" runat="server"
                        AutoDataBind="true" HasCrystalLogo="False" HasExportButton="False" HasToggleGroupTreeButton="False"
                        ToolPanelView="None" HasSearchButton="False" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUCPersonalSkill.aspx.cs" Inherits="IAS.Mockup.TestUCPersonalSkill" %>
<%@ Register Src="../UserControl/ucPersonalSkills.ascx" TagName="UserControlPersonalSkills" TagPrefix="ucPerSkills" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="toolkit1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="false">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Label ID="lblInstalledUICulture" runat="server"></asp:Label><br />
    <asp:Label ID="lblCurrentCulture" runat="server"></asp:Label><br />
    <asp:Label ID="lblCurrentUICulture" runat="server"></asp:Label><br />
    <asp:Label ID="lblInvariantCulture" runat="server"></asp:Label><br />
    <br />
    <div>
    <ucPerSkills:UserControlPersonalSkills ID="ucPerSkills" runat="server"></ucPerSkills:UserControlPersonalSkills>
    </div>
    </form>
</body>
</html>

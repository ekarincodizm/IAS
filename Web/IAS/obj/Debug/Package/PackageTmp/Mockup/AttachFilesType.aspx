<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttachFilesType.aspx.cs" Inherits="IAS.Mockup.AttachFilesType" %>
<%@ Register Src="~/UserControl/ucAttachFileControl.ascx" TagName="ucAttachFileControl" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true"
        LoadScriptsBeforeUI="true" EnablePageMethods="true" ScriptMode="Release" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <uc4:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetADProp.aspx.cs" Inherits="IAS.Mockup.GetADProp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true"
        EnableScriptGlobalization="true" LoadScriptsBeforeUI="true" EnablePageMethods="true"
        ScriptMode="Release" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Button ID="btnAuth" runat="server" Text="Auth" onclick="btnAuth_Click" /><br />
    <asp:TextBox ID="txtUserName" runat="server" Text="it-prapatu"></asp:TextBox>&nbsp;
    <asp:TextBox ID="txtPassword" runat="server" Text="abcd12345"></asp:TextBox>&nbsp;
    <asp:Button ID="btnAuthByNewService" runat="server" Text="AuthWithService" 
        onclick="btnAuthByNewService_Click" /><br />
    <asp:Label ID="lblResult" runat="server" ></asp:Label><br />
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:GridView ID="gvADProperties" runat="server" AutoGenerateColumns="true" Width="100%" >
        <HeaderStyle Font-Bold="true" Font-Size="Large" Font-Names="tahoma" BackColor="Aquamarine" />
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

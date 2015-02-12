<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUCAddress.aspx.cs" Inherits="IAS.Mockup.TestUCAddress" %>
<%@ Register Src="~/UserControl/ucAddress.ascx" TagName="ucAddress" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/ucAttachFileLicense.ascx" TagName="ucLicense" TagPrefix="uc2" %>

<!DOCTYPE html />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--For JQuery--%>
    <script type="text/javascript"  src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <%--For JQuery UI--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js"></script>
    <link href="../style/jquery-ui-1.8.10.custom.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true"
        LoadScriptsBeforeUI="true" EnablePageMethods="true" ScriptMode="Release" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server">
            <ajaxToolkit:TabContainer ID="TabAddress" runat="server" Width="100%" ActiveTabIndex="0" OnActiveTabChanged="TabAddress_ActiveTabChanged">
                                                    <ajaxToolkit:TabPanel ID="TabCurrentAddress" runat="server" Height="140px" TabIndex="0">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblCurrentAddressHeader" runat="server"  Text="ที่อยู่ปัจจุบัน"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <uc1:ucAddress ID="ucAddressCurrent" runat="server" /><br />
                                                            <asp:CheckBox ID="chkCopyAdd" runat="server" AutoPostBack="True" 
                                                                OnCheckedChanged="chkCopyAdd_CheckedChanged" 
                                                                Text="ที่อยู่ตามทะเบียนบ้านเหมือนที่อยู่ปัจจุบัน" />
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <ajaxToolkit:TabPanel ID="TabRegisterAddress" runat="server" Height="140px" TabIndex="1">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblRegisterAddressHeader" runat="server" Text="ที่อยู่ตามทะเบียนบ้าน"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                            <uc1:ucAddress ID="ucAddressRegister" runat="server" />
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                </ajaxToolkit:TabContainer>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestReport.aspx.cs" Inherits="IAS.Mockup.TestReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:Button ID="btnTestEnum" runat="server" Text="Test Enum" 
            onclick="btnTestEnum_Click" /><br />
        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox><br />
        <asp:Label ID="lblEncrypt" runat="server"></asp:Label><br />
        <asp:Label ID="lblNumber" runat="server"></asp:Label><br />
        <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt" 
            onclick="btnEncrypt_Click" /><br />
        <asp:TextBox ID="txtIdCard" runat="server"></asp:TextBox>
        <asp:Label ID="lblResult" runat="server" Text="Wait"></asp:Label>
        <asp:Button ID="vIdCard" runat="server" Text="Verify IdCard" 
            onclick="vIdCard_Click" />
        <br />
        <asp:TextBox ID="txt" runat="server" Text="1234567890123"></asp:TextBox>
        <asp:Button ID="btn" runat="server" Text="Show Report" OnClick="btn_Click" />
        <br />
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/updateprogress.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

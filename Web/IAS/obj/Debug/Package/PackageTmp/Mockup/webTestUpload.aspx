<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webTestUpload.aspx.cs"
    Inherits="IAS.Mockup.webTestUpload" %>

<%@ Register src="../UserControl/UCLoadingProgress.ascx" tagname="UCLoadingProgress" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true"
        LoadScriptsBeforeUI="true" EnablePageMethods="true" ScriptMode="Release" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        <%--<asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                <asp:Button ID="btnGet" runat="server" Text="Get Com Code" OnClick="btnGet_Click" />
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                <br />
                <asp:FileUpload ID="fUpload" runat="server" />
                <asp:Button ID="btnUpload" runat="server" Text="Upload to Temp" OnClick="btnUpload_Click" />
                <asp:Button ID="btn1" runat="server" Text="Upload to Attach" OnClick="btn1_Click" />
                <asp:Button ID="btn2" runat="server" Text="Upload to OIC" OnClick="btn2_Click" />
                <br />
                <asp:Button ID="btnGetData" runat="server" Text="Get Data" 
            onclick="btnGetData_Click" />
                <asp:Button ID="btn3" runat="server" Text="Upload Data" 
            onclick="btn3_Click" />
                <asp:Button ID="btn4" runat="server" Text="Upload Data" 
            onclick="btn4_Click" />
            <asp:Button ID="btn5" runat="server" Text="Download" onclick="btn5_Click" />
                <br />
                <asp:Label ID="lblHash" runat="server"></asp:Label><br />
                <asp:Label ID="lblHash2" runat="server"></asp:Label>
                <asp:GridView ID="gv" runat="server">
                </asp:GridView>
                <asp:GridView ID="gv2" runat="server">
                </asp:GridView>
                <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
            <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>

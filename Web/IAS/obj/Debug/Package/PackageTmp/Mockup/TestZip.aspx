<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestZip.aspx.cs" Inherits="IAS.Mockup.TestZip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnListOfZip" runat="server" Text="List of zip" 
            onclick="btnListOfZip_Click" />
        <asp:Button ID="btnListOfRar" runat="server" Text="List of Rar" 
            onclick="btnListOfRar_Click" />
        <asp:Button ID="btnExtractZip" runat="server" Text="Extract Zip" 
            onclick="btnExtractZip_Click" />
        <asp:Button ID="btnExtractRar" runat="server" Text="Extract Rar" 
            onclick="btnExtractRar_Click" />
        <asp:FileUpload ID="fUpload" runat="server" />
        <asp:Button ID="btnUload" runat="server" Text="Upload" 
            onclick="btnUload_Click" />
        <asp:Button ID="Redirect" runat="server" onclick="Redirect_Click" />
        <asp:Button ID="btnTestPDF" runat="server" Text="Add Password" 
            onclick="btnTestPDF_Click" />
        <asp:Button ID="btnTestStore" runat="server" Text="Run Store Proc" 
            onclick="btnTestStore_Click" />
        <asp:Button ID="Button1" runat="server" Text="Run Store Proc Update" 
            onclick="Button1_Click" />
        <asp:Button ID="btnTestDownload" runat="server" Text="Download and Zip" 
            onclick="btnTestDownload_Click" />
        <asp:GridView ID="gv" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>

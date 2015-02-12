<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Regis_GetDetailByID.aspx.cs" Inherits="IAS.Mockup.Regis_GetDetailByID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
            <asp:Label ID="lblID" runat="server" Text="IDCard : "></asp:Label>
            <asp:TextBox ID="txtIDCard" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnValid" runat="server" Text="Validation" 
            onclick="btnValid_Click" /><br />
        <br />
        <asp:GridView ID="gvIDCard" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageIndex="20" Font-Size="Small" Font-Names="TAHOMA">
            <Columns>
                <asp:TemplateField HeaderText="ID_CARD_NO">
                    <ItemTemplate>
                        <asp:Label ID="lblIDCard" runat="server" Text='<%# Bind("ID_CARD_NO") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvAll" runat="server" AllowPaging="true" PageIndex="10" AutoGenerateColumns="true">
        </asp:GridView>
    </div>
    </form>
</body>
</html>

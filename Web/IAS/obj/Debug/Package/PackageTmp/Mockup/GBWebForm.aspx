<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBWebForm.aspx.cs" Inherits="IAS.Mockup.GBWebForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvHoliday" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField HeaderText="วันที่">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("HL_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="รายละเอียด">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("HL_DESC") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

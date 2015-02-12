<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestImportFile.aspx.cs"
    Inherits="IAS.Mockup.TestImportFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#999999"
            HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3"
            GridLines="Vertical" Width="100%" AllowPaging="True" BorderStyle="None" EnableTheming="True"
            PageSize="12" ssClass="gridtxlist" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">"
            PagerSettings-PreviousPageText="<" PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First"
            AllowSorting="True" PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center"
            AutoGenerateColumns="False" PagerStyle-CssClass="viewpager">
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                NextPageText="&gt;" PreviousPageText="&lt;" />
            <RowStyle CssClass="view" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
            <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
            <HeaderStyle Font-Bold="True" CssClass="viewheader" />
            <AlternatingRowStyle CssClass="view2" />
            <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="IDNumber" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle Width="20%" />
                    <ItemTemplate>
                        <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Bind("IDNumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FirstName" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle Width="20%" />
                    <ItemTemplate>
                        <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LastName" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle Width="20%" />
                    <ItemTemplate>
                        <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BirthDay" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle Width="20%" />
                    <ItemTemplate>
                        <asp:Label ID="lblBirthDayGv" runat="server" Text='<%# Bind("BirthDay") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExamID" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle Width="20%" />
                    <ItemTemplate>
                        <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Bind("ExamID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

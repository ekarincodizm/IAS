<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testImg.aspx.cs" Inherits="IAS.Mockup.testImg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="171px" AutoGenerateRows="False"
            AutoGenerateInsertButton="False" DefaultMode="Insert" DataSourceID="SqlDataSource1"
            OnItemInserting="AttachmentsDetailsView_ItemInserting">
            <Fields>
                <asp:BoundField DataField="ATTACHMENT_ID" HeaderText="ATTACHMENT_ID" SortExpression="ATTACHMENT_ID"
                    ReadOnly="True" Visible="false" />
                <asp:TemplateField HeaderText="REQUEST_ID" SortExpression="REQUEST_ID">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Session["ccREQUESTID"] %>'></asp:Label>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <asp:TextBox ID="TextBox1" ReadOnly="True" runat="server" Text='<%# Session["ccREQUESTID"] %>'></asp:TextBox>
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" SortExpression="DESCRIPTION" />
                <asp:BoundField DataField="DATE_ATTACHED" HeaderText="DATE_ATTACHED" SortExpression="DATE_ATTACHED"
                    Visible="false" />
                <asp:BoundField DataField="FILE_NAME" HeaderText="FILE_NAME" SortExpression="FILE_NAME"
                    Visible="false" />
                <asp:TemplateField HeaderText="DATA" SortExpression="DATA">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("DATA") %>'></asp:Label>
                    </ItemTemplate>
                    <InsertItemTemplate>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowCancelButton="False" ShowInsertButton="True" />
            </Fields>
        </asp:DetailsView>
        <asp:Label ID="Label3" runat="server" Text="Select a file and click insert."></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM &quot;TESTIMAGE&quot;"
            InsertCommand="INSERT INTO AGDOI.TESTIMAGE() VALUES ()"></asp:SqlDataSource>
        <asp:EntityDataSource ID="EntityDataSource1" runat="server">
        </asp:EntityDataSource>
        <br />
    </div>
    </form>
</body>
</html>

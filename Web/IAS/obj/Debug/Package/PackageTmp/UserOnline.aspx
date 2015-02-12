<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserOnline.aspx.cs" Inherits="IAS.UserOnline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family:Verdana;
            font-size:13px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None" 
            onrowdatabound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="ลำดับ">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Username">
                    <ItemTemplate>
                        <asp:Label ID="lbl_username" runat="server" Text='<%# Bind("USER_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ประเภทสมาชิก">
                    <ItemTemplate>
                        <asp:Label ID="lbl_type" runat="server" Text='<%# Bind("USER_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ไอพีล็อกอิน">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ip" runat="server" Text='<%# Bind("IP_LOGIN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="เวลาที่ล็อกอิน">
                    <ItemTemplate>
                        <asp:Label ID="lbl_timelogin" runat="server" Text='<%# Bind("TIME_LOGIN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <EmptyDataTemplate>
                ไม่มี useronline
            </EmptyDataTemplate>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>    
        <br />
        <pre>&nbsp;</pre>
    </div>
    </form>
</body>
</html>

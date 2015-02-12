<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SetLincense.aspx.cs" Inherits="IAS.Setting.SetLincense" %>

<%@ Register src="../UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc1" %>
<%@ Register src="../UserControl/UCModalSuccess.ascx" tagname="UCModalSuccess" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content" align="center">
        <div class="box_content_header">
            <div class="box_content_header_text">
               ตั้งค่าการตรวจสอบคุณสมบัติ การขอรับใบอนุญาต
            </div>
        </div>
        <div style="clear: left;">
        </div>
        <br />
        <br />
        
        <asp:GridView ID="GvLicense" runat="server" AutoGenerateColumns="false" Width="500px" BorderWidth="1"
    CssClass="datatable" CellSpacing="-1" GridLines="None" onrowdatabound="GvLicense_RowDataBound">
            <AlternatingRowStyle CssClass="altrow" />
            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Id" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                    <ItemTemplate>
                        <asp:Label ID="lbl_name" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ITEM_VALUE" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbl_value" runat="server" Text='<%# Bind("ITEM_VALUE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddl_select" runat="server" CssClass="ddl">
                            <asp:ListItem Text="ตรวจสอบผลอบรม" Value="1"></asp:ListItem>
                            <asp:ListItem Text="ไม่ตรวจสอบผลอบรม" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />

        <asp:Button ID="btnSave" runat="server" Text="บันทึก" onclick="btnSave_Click" CssClass="btn" />
        <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" 
            onclick="btnCancel_Click" CssClass="btn" />

        <br />
        <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />
        <br />
        <uc1:UCModalError ID="UCModalError1" runat="server" />
        <br />

    </div>
</asp:Content>

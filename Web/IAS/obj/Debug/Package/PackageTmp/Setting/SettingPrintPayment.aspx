<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SettingPrintPayment.aspx.cs" Inherits="IAS.Setting.SettingPrintPayment" %>
    <%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .bordernone
        {
            border:none;
        }
        .bordernone tr
        {
            border:none;
        }
        .bordernone td
        {
            border:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="การตั้งค่าสิทธิ์ในการพิมพ์ใบสั่งจ่าย" BorderStyle="None" ID="lblCreatePayment"
                    runat="server" />
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box_body_content">
                        <table width="98%">
                        <tr style = "padding-top:20px">
                        <td>
                        &nbsp;
                        </td>
                        </tr>
                            <tr >
                                <td align = "center">
                                    <asp:GridView ID="gvConfigPrint" runat="server" Width="70%" CssClass="datatable"
                                        ShowHeaderWhenEmpty="True" AllowSorting="True" AutoGenerateColumns="False" >
                                        <AlternatingRowStyle CssClass="altrow" />
                                        <HeaderStyle BorderStyle="None" />
                                        <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                ไม่พบข้อมูล...</div>
                                        </EmptyDataTemplate>
                                        <Columns>

                                            <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย">
                                                <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
                                             </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" Width="40%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สิทธิ์ในการพิมพ์ใบสั่งจ่าย">
                                                <HeaderStyle HorizontalAlign="Center" Width="60%" />
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="RBLConfigPrint" runat="server" SelectedValue='<%#Eval("ITEM_VALUE")%>' 
                                                    RepeatDirection = "Horizontal" CssClass="bordernone"  OnSelectedIndexChanged = "RBLConfigPrint_Change" AutoPostBack = "true">
                                                    <asp:ListItem Text ="สมาคม"  Value = "0"></asp:ListItem>
                                                    <asp:ListItem Text ="บริษัทประกันภัย"  Value = "1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="60%" />
                                            </asp:TemplateField>
                                                  <asp:TemplateField Visible = "false" >
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField>
                                                  <asp:TemplateField Visible = "false">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupCode" runat="server" Text='<%# Bind("GROUP_CODE") %>'></asp:Label>
                                             </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                          </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                            <td align = "center" style = "padding-top:20px">
                                <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass = "btn" 
                                    onclick="btnSave_Click" />&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass = "btn" 
                                    onclick="btnCancel_Click" />
                            </td>
                            </tr>
                        </table>
                                     <uc2:UCModalError ID="UCModalError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>

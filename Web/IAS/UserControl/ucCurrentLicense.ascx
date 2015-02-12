<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCurrentLicense.ascx.cs" Inherits="IAS.UserControl.ucCurrentLicense" %>
<%--<%@ Register src="UCLoadingProgress.ascx" tagname="UCLoadingProgress" tagprefix="uc1" %>--%>
<link href="../style/main.css" rel="stylesheet" type="text/css" />
    <link href="../style/menu.css" rel="stylesheet" type="text/css" />
    <link href="../style/content.css" rel="stylesheet" type="text/css" />
    <link href="../style/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <link href="../style/UCStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style/PopupPageStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.theme.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.datepicker.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.tabs.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="udpPayment" runat="server" UpdateMode="Always" >
<ContentTemplate>
    <asp:Panel ID="pnlPayment" runat="server" Visible="true" Width="100%" >
<%--                <fieldset >
                <legend><asp:Label ID="lblCurrentLicense" runat="server" Text="ใบอนุญาตที่ยังมีผลอยู่" CssClass="lbl"></asp:Label></legend>--%>
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvCurrentLicense" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                            width="100%" BorderStyle="None" HorizontalAlign="Center" >
                                <AlternatingRowStyle CssClass="table_alter_row" />
                                <HeaderStyle BorderStyle="None" />
                                <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                  <EmptyDataTemplate>
                                    <div style="text-align: center; margin-left: 0; margin-right: 0">
                                        ไม่พบข้อมูล</div>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="table_item_row" />
                                <Columns>
                                    <asp:TemplateField HeaderText="เลขใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="8%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLicenseNo" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLicenseType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่หมดอายุ" Visible="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpireDate" runat="server" Text='<%# Bind("LICENSE_EXPIRE_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                <%--</fieldset>--%>
        </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
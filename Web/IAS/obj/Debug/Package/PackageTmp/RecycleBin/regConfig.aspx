<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Global.Master" AutoEventWireup="true" CodeBehind="regConfig.aspx.cs" Inherits="IAS.RecycleBin.regConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <table style="width: 90%;" align="center">
        <tr>
            <td>
                <asp:Button Text="ตั้งค่าทั่วไป" BorderStyle="None" ID="TabControl" CssClass="Initial" runat="server"
                    OnClick="TabControl_Click" />
                <asp:Button Text="ตั้งค่า Config" BorderStyle="None" ID="TabConfig" CssClass="Initial" runat="server"
                    OnClick="TabConfig_Click" />
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td style="text-align: center;">
                                    <asp:GridView ID="gvControl" runat="server" BackColor="White" BorderColor="#999999"
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
                                            <asp:TemplateField HeaderText="ชื่อฟังก์ชั่น" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFunationControlGv" runat="server" Text='<%# Eval("Funation") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ค่าที่กำหนด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPreferencesControlGv" runat="server" Text='<%# Eval("Preferences") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="คำอธิบาย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="30%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDetailControlGv" runat="server" Text='<%# Eval("SurName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                            <tr>
                                <td style="text-align: center;">
                                    <asp:GridView ID="gvConfig" runat="server" BackColor="White" BorderColor="#999999"
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
                                            <asp:TemplateField HeaderText="ชื่อฟังก์ชั่น" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFunationConfigGv" runat="server" Text='<%# Eval("Funation") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ค่าที่กำหนด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPreferencesConfigGv" runat="server" Text='<%# Eval("Preferences") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="คำอธิบาย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="30%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDetailConfigGv" runat="server" Text='<%# Eval("SurName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
</asp:Content>

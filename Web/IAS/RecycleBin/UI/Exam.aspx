<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="Exam.aspx.cs" Inherits="IAS.UI.Exam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <fieldset>
                <legend class="lbl">ค้นหา:</legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 30%;">
                            <asp:Label ID="lblIdNumber" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:TextBox ID="txtIdNumber" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblCertifyNumber" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:TextBox ID="txtCertifyNumber" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: center; width: 10%;" rowspan="2">
                            <asp:Button runat="server" ID="btnSearch" Text="ค้นหา" CssClass="btn" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:TextBox ID="txtName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblSurName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:TextBox ID="txtSurName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="5">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;">
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
                                <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdNumberGv" runat="server" Text='<%# Eval("IdNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameGv" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSurNameGv" runat="server" Text='<%# Eval("SurName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCertifyNumberGv" runat="server" Text='<%# Eval("CertifyNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_ID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

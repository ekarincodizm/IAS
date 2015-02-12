<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="License1.aspx.cs" Inherits="IAS.UI.License1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblFileName" runat="server" Text="ไฟล์ข้อมูล" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 60%;">
                        <asp:FileUpload ID="FuFile" runat="server" Width="500px" />
                    </td>
                    <td style="text-align: left; width: 20%;">
                        <asp:Button ID="btnLoadFile" runat="server" Text="โหลดข้อมูล" CssClass="btn" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;">
                        <asp:GridView ID="gvFile" runat="server" BackColor="White" BorderColor="#999999"
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
                                <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="40%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemAllGv" runat="server" Text='<%# Eval("ItemAll") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายการที่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCorrectGv" runat="server" Text='<%# Eval("ItemCorrect") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemInCorrectGv" runat="server" Text='<%# Eval("ItemInCorrect") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
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
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameGv" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSurNameGv" runat="server" Text='<%# Eval("SurName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่สมัคร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTestGv" runat="server" Text='<%# Eval("DateTest") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เวลาที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTimeTestGv" runat="server" Text='<%# Eval("TimeTest") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlaceTestGv" runat="server" Text='<%# Eval("PlaceTest") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProblemGv" runat="server" Text='<%# Eval("Problem") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProblemGv" runat="server" Text='<%# Eval("Cause") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;" colspan="4">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <asp:Button ID="btnSystem" runat="server" Text="นำเข้าระบบ" CssClass="btn" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_ID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

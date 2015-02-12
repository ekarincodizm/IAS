<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="License3.aspx.cs" Inherits="IAS.Mockup.License3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <fieldset>
                <legend></legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblTestPlace" runat="server" Text="สถานที่สอบ"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtTestPlace" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblTestDate" runat="server" Text="วันที่สอบ"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtTestDate" runat="server" Width="180px"></asp:TextBox>
                            <asp:Image ID="ImgTestDate" runat="server" ImageUrl="~/Images/cal.JPG" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblPeople" runat="server" Text="จำนวนคน"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtPeople" runat="server" Width="180px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblTestTime" runat="server" Text="เวลาที่สอบ"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtTestTime" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button runat="server" ID="btnSearch" Text="ค้นหา" Width="100px" />
                        </td>
                    </tr>
                </table>
            </fieldset>
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
        </asp:Panel>
    </div>
    </form>
</body>
</html>

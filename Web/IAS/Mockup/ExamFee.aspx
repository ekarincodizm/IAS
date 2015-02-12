<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamFee.aspx.cs" Inherits="IAS.Mockup.ExamFee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <fieldset>
                    <legend>ค้นหา:</legend>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblOrderNumber" runat="server" Text="เลขใบสั่งจ่าย"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtOrderNumber" runat="server" Width="180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblStartDate" runat="server" Text="ตั้งแต่วันที่"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtStartDate" runat="server" Width="180px"></asp:TextBox>
                                <asp:Image ID="ImgStartDate" runat="server" ImageUrl="~/Images/cal.JPG" />
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblEndDate" runat="server" Text="ถึงวันที่"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtEndDate" runat="server" Width="180px"></asp:TextBox>
                                <asp:Image ID="ImgEndDate" runat="server" ImageUrl="~/Images/cal.JPG" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="4">
                                <asp:Button runat="server" ID="btnSearch" Text="ค้นหา" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#999999"
                                HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3" GridLines="Vertical" Width="100%"
                                AllowPaging="True" BorderStyle="None" EnableTheming="True" PageSize="12" ssClass="gridtxlist"
                                PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">" PagerSettings-PreviousPageText="<"
                                PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First" AllowSorting="True"
                                PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                PagerStyle-CssClass="viewpager">
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                <RowStyle CssClass="view" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                <AlternatingRowStyle CssClass="view2" />
                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="เลขใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderNumberGv" runat="server" Text='<%# Eval("OrderNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="16%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumberPeopleGv" runat="server" Text='<%# Eval("NumberPeople") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="16%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountGv" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่จ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="16%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDatePayGv" runat="server" Text='<%# Eval("DatePay") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สถานะ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="16%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ดำเนินรายการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="16%" />
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
    </form>
</body>
</html>

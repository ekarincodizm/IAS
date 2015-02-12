<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayExamFee3.aspx.cs" Inherits="IAS.Mockup.PayExamFee3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <div align="center">
                    <table style="border: 3px solid black; width: 72%; text-align: center; vertical-align: middle;">
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblReceiptNumber" runat="server" Text="เลขใบสั่งจ่าย"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txReceiptNumber" runat="server" Width="180px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblPayDate" runat="server" Text="วันที่จ่าย"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtPayDate" runat="server" Width="180px"></asp:TextBox>
                                <asp:Image ID="ImgPayDate" runat="server" ImageUrl="~/Images/cal.JPG" />
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
                                <asp:Label ID="lblReceiptDate" runat="server" Text="วันที่ออกใบเสร็จ"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtReceiptDate" runat="server" Width="180px"></asp:TextBox>
                                <asp:Image ID="ImgReceiptDate" runat="server" ImageUrl="~/Images/cal.JPG" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
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
                                        <asp:TemplateField HeaderText="วันที่สมัคร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateTestGv" runat="server" Text='<%# Eval("DateTest") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPayment" runat="server"  Checked='<%# Eval("Payment") %>'/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamResult1.aspx.cs" Inherits="IAS.Mockup.ExamResult1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblFileName" runat="server" Text="ไฟล์ข้อมูล"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 60%;">
                            <asp:FileUpload ID="FuFile" runat="server" Width="700px" />
                        </td>
                        <td style="text-align: left; width: 20%;">
                            <asp:Button ID="btnLoadFile" runat="server" Text="โหลดข้อมูล" />
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:GridView ID="gvFile" runat="server" BackColor="White" BorderColor="#999999"
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
                            <asp:GridView ID="gvCheckList" runat="server" BackColor="White" BorderColor="#999999"
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
                                    <asp:TemplateField HeaderText="เลขที่" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoGv" runat="server" Text='<%# Eval("No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่จ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPayDateGv" runat="server" Text='<%# Eval("PayDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo1Gv" runat="server" Text='<%# Eval("RefNo1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่อ้างอิง2" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo2Gv" runat="server" Text='<%# Eval("RegisterDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountGv" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblProblemGv" runat="server" Text='<%# Eval("Problem") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCauseGv" runat="server" Text='<%# Eval("Cause") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button ID="btnSystem" runat="server" Text="นำเข้าระบบ" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

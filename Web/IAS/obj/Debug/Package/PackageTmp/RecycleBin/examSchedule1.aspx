<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/GlobalMenu.Master" AutoEventWireup="true"
    CodeBehind="examSchedule1.aspx.cs" Inherits="IAS.RecycleBin.examSchedule1" %>

<%@ Register Src="../UserControl/examSchedule.ascx" TagName="examSchedule" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/examDetail.ascx" TagName="examDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.min.js" type="text/javascript"></script>
    <link href="../style/jquery-ui-1.8.10.custom.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui-1.8.10.offset.datepicker.min.js" type="text/javascript"></script>
    <script src="../Scripts/LoadScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Label Text="กำหนดการสมัครสอบ" BorderStyle="None" ID="lblRegisterOfficer" CssClass="Initial"
        runat="server" /><br />
    <br />
    <fieldset style="width: 90%; margin: 0px auto 0px auto;">
        <legend class="lbl">เพิ่มข้อมูลการสมัครสอบ
        </legend>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 20%;">
                    &nbsp;
                </td>
                <td style="text-align: left; width: 30%;">
                    &nbsp;
                </td>
                <td style="text-align: right; width: 20%;">
                    &nbsp;
                </td>
                <td style="text-align: right; width: 30%;">
                    <asp:Button ID="btnInsert" runat="server" CssClass="btn" Text="เพิ่ม" />
                </td>
            </tr>
        </table>
    </fieldset>
    <asp:Panel ID="pnlCalendar" runat="server" Visible="true">
                <div style="width: 90%; margin: 0px auto 10px auto;">
                    <asp:Calendar ID="cld" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4"
                        DayNameFormat="Full" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="382px"
                        Width="100%" ShowGridLines="True">
                        <DayHeaderStyle BackColor="#CCCCCC" BorderColor="Black" Font-Bold="True" Height="50px"
                            HorizontalAlign="Left" />
                        <DayStyle BorderColor="Black" Height="50px" HorizontalAlign="Left" />
                        <TitleStyle BorderColor="Black" Height="15px" HorizontalAlign="Center" />
                        <TodayDayStyle BackColor="#FF9900" />
                    </asp:Calendar>
                    <br />
                    <asp:GridView ID="gvSingleApplication" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CaptionAlign="Top" CellPadding="3" DataKeyNames="ID" EnableTheming="True"
                        GridLines="Vertical" HeaderStyle-BackColor="#999999" PagerSettings-FirstPageText="First"
                        PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;"
                        PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager"
                        PagerStyle-HorizontalAlign="Center" PageSize="12" ssClass="gridtxlist" Width="100%">
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            NextPageText="&gt;" PreviousPageText="&lt;" />
                        <RowStyle CssClass="view" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                        <PagerStyle CssClass="viewpager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="viewheader" Font-Bold="True" />
                        <AlternatingRowStyle CssClass="view2" />
                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Link" CancelText="ยกเลิก" EditText="แก้ไข" ItemStyle-Width="10%"
                                ShowCancelButton="true" ShowEditButton="true" UpdateText="ตกลง" />
                            <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamNumber" runat="server" Text='<%# Bind("LicenseNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("ApproveDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="กลุ่มสนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamYard" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProvince" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
    <div>
    <fieldset style="width: 90%; margin: 0px auto 10px auto;">
        <legend class="Initial">
            <asp:Label ID="Label4" runat="server" BorderStyle="None" Text="ข้อมูลการสอบ" />
        </legend>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 30%;">
                    <span>
                        <asp:Label ID="lblDetailExamCode" runat="server" CssClass="lbl" 
                        Text="รหัสสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtDetailExamCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 25%;">
                    <span>
                        <asp:Label ID="lblDetailDateExam" runat="server" CssClass="lbl" 
                        Text="วันที่สอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtDetailDateExam" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="lblDetailTimeExamCode" runat="server" CssClass="lbl" 
                        Text="รหัสเวลาที่สอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailTimeExamCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailTimeExam" runat="server" CssClass="lbl" 
                        Text="เวลาที่สอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailTimeExam" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="lblDetailYardCode" runat="server" CssClass="lbl" 
                        Text="รหัสสนามสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailYardCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailNameYard" runat="server" CssClass="lbl" 
                        Text="ชื่อสนามสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailNameYard" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="lblDetailOfficerCode" runat="server" CssClass="lbl" 
                        Text="รหัสประเภทใบอนุญาตที่สอบ"></asp:Label>
                    &nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailOfficerCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailTypeLicenseExam" runat="server" CssClass="lbl" 
                        Text="ชื่อประเภทใบอนุญาตที่สอบ"></asp:Label>
                    </span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailTypeLicenseExam" runat="server" 
                        CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="Label15" runat="server" CssClass="lbl" Text="บังคับสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailQuantityExam" runat="server" CssClass="lbl" 
                        Text="จำนวนสมัคร (คน)"></asp:Label>
                    </span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailQuantityExam" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="lblDetailNumberOfSeat" runat="server" CssClass="lbl" 
                        Text="จำนวนที่นั่ง (คน)"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailNumberOfSeat" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailFee" runat="server" CssClass="lbl" 
                        Text="ค่าธรรมเนียม (บาท)"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailFee" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>
    </div>
    <br />
    <div style="width: 90%; margin: 0px auto 10px auto; text-align:center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="บันทึก" />
        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิก" />
        <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="ลบ" />
    </div>
    &nbsp;
</asp:Content>


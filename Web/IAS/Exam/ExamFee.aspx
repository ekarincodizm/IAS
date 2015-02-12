<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ExamFee.aspx.cs" Inherits="IAS.Exam.ExamFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function setdate() {
            $('#txtEndDate').attr('readOnly', 'true');
            $('#txtStartDate').attr('readOnly', 'true');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:Label Text="ขอรับใบอนุญาต" BorderStyle="None" ID="lblOrderh" CssClass="Initial"
        runat="server" /><br />
    <br />
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ใบสั่งจ่าย" BorderStyle="None" ID="lblOrder" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::ค้นหา</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblTypeOrder" runat="server" Text="ประเภทใบสังจ่าย" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlTypeOrder" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblOrdersGroup" runat="server" Text="ใบสั่งจ่ายรายกลุ่ม" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtOrdersGroup" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblEaxmID" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtEaxmID" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                        <br />
                                        <br />
                                        <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblStartDate" runat="server" Text="ตั้งแต่วันที่" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                            runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CLDtxtStartDate" PopupButtonID="imgPopup_txtStartDate"
                                            runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblEndDate" runat="server" Text="ถึงวันที่" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                            runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CLDtxtEndDate" runat="server" PopupButtonID="imgPopup_txtEndDate" TargetControlID="txtEndDate"
                                            Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <br />
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="box_body_content">
                            <div class="box_t_h">
                            </div>
                            <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                                    <ProgressTemplate>
                                                        Please wait....
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:GridView ID="gvDetail" runat="server" Width="100%" AllowPaging="false" ShowHeaderWhenEmpty="True"
                                                    CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True"
                                                    AutoGenerateColumns="False">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeOrderGv" runat="server" Text='<%# Bind("TypeOrder") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ใบสั่งจ่ายรายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrdersGroupGv" runat="server" Text='<%# Eval("OrdersGroup") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmountPeopleGv" runat="server" Text='<%# Eval("AmountPeople") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEaxmIDGv" runat="server" Text='<%# Eval("EaxmID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmountGv" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่สั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDatePayGv" runat="server" Text='<%# Eval("DatePay") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ผู้จ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDispenserGv" runat="server" Text='<%# Eval("Dispenser") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สถานะ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusGv" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>&nbsp;&nbsp;
                                                                <asp:LinkButton ID="hplPrint" runat="server" OnClick="hplPrint_Click"><img src="../Images/print-icon.gif" title="พิมพ์" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

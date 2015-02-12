<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="GenPayment.aspx.cs" Inherits="IAS.Payment.GenPayment" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <script type="text/javascript">
        function confirmDeleteAttachFile() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

        function OpenPopupee() {
            var qstr = "../PDF/PDF_OIC/RptPayment.pdf";
            window.open(qstr, 'popupwindow', 'fullscreen=no,toolbar=0, menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        }
        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else if (charCode == 13) {
                return false;
            }
            return true;
        }
    </script>
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="สร้างใบเสร็จ" BorderStyle="None" ID="lblPaymentGroup" runat="server" />
            </div>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStartPaidSubDate" runat="server" Text="วันที่รับเงิน(เริ่ม)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartPaidSubDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEStartPaidSubDate" PopupButtonID="imgPopup_txtStartPaidSubDate"
                                                        runat="server" TargetControlID="txtStartPaidSubDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndPaidSubDate" runat="server" Text="วันที่รับเงิน(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndPaidSubDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup_txtEndPaidSubDate"
                                                        runat="server" TargetControlID="txtEndPaidSubDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPaidGroup" runat="server" Text="ใบสั่งจ่ายกลุ่ม" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtPaidGroup" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click"
                                                        Width="75px" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="PnlDetailSearchGridView" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <div class="box_textbox_bg_table" runat="server" id="bludDiv">
                                    <div class="box_texbox_add_box">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvSearch" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                        GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="False" OnRowDataBound="gvSearch_RowDataBound">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="Checkall" runat="server" AutoPostBack="true" OnCheckedChanged="Checkall_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectPayment_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbllno" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGroupRequsetNo" runat="server" Text='<%# Bind("GROUP_REQUEST_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่รับชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptDatePayGv" runat="server" Text='<%# Bind("PAYMENT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmountMoneyGv" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPetitionTypeGv" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ-นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstLastNameGv" runat="server" Text='<%# Bind("FULL_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center" HeaderText="เลขที่ใบเสร็จ">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptNo" runat="server" Text='<%# Bind("RECEIPT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHEADREQUESTNO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPAYMENTNO" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplPrint" runat="server" OnClick="StatusA_Click" CommandArgument='<%# Bind("ID_CARD_NO") %>'><img src="../Images/approved-icon.gif" title="อนุมัติ"   /></asp:LinkButton>&nbsp;&nbsp;
                                                                    <%-- <asp:LinkButton ID="Status_C" runat="server" Visible = "false"><img src="../Images/success.png" title="ดำเนินการสำเร็จ" /></asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divGv1" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 80%">
                                                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1"
                                                                        Enabled="false"></asp:TextBox>
                                                                    <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                                    <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblHeadInputMaxrow" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                    <asp:TextBox ID="txtInputMaxrow" runat="server" AutoComplete="off" Visible="false"
                                                                        Width="5%" onkeypress="return runScript(event)" MaxLength="4" Style="text-align: center"
                                                                        onpaste="return false;" ondrop="return false;"></asp:TextBox>
                                                                    &nbsp;
                                                                    <asp:Button ID="btngo" runat="server" Text="Go" OnClick="btnSearch_Click" />
                                                                </td>
                                                                <td style="width: 20%; text-align: right">
                                                                    <asp:Label ID="lblHeadTotal" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblTotalrecord" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblEndTotal" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnGenReceiptAll" runat="server" Text="สร้างใบเสร็จ" OnClick="btnGenReceiptAll_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <asp:HiddenField ID="hdfSession" runat="server" />
        <asp:HiddenField ID="hdfMemberType" runat="server" />
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Invoice5.aspx.cs" Inherits="IAS.Payment.Invoice5" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenPopup(para) {

            var qstr = "../Reporting/RptBillPayment.aspx?Invoice=" + para + "&Click=Print";

            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');

            //window.open(qstr, 'popupwindow', 'fullscreen=no,toolbar=0, menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        }
        function OpenPopupSingle(para) {

            var qstr = "../Reporting/RptBillPayment.aspx?Invoice=" + para + "&Click=PrintS";

            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');

            //window.open(qstr, 'popupwindow', 'fullscreen=no,toolbar=0, menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
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
        $(function setdate() {
            $('#txtStartPaidSubDate').attr('readOnly', 'true');
            $('#txtEndPaidSubDate').attr('readOnly', 'true');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: 16px;
            color: #000066;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ใบสั่งจ่ายกลุ่ม" BorderStyle="None" ID="lblPaymentGroup" runat="server" />
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
                                                    <asp:Label ID="lblStartPaidSubDate" runat="server" Text="วันที่สร้างใบสั่งจ่าย(เริ่ม)"
                                                        CssClass="lbl"></asp:Label>
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
                                                    <asp:Label ID="lblEndPaidSubDate" runat="server" Text="วันที่สร้างใบสั่งจ่าย(สิ้นสุด)"
                                                        CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndPaidSubDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEEndPaidSubDate" PopupButtonID="imgPopup_txtEndPaidSubDate"
                                                        runat="server" TargetControlID="txtEndPaidSubDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStartExamDate" runat="server" Text="วันที่สอบ(เริ่ม)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartExamDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartExamDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEStartExamDate" PopupButtonID="imgPopup_txtStartExamDate"
                                                        runat="server" TargetControlID="txtStartExamDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndExamDate" runat="server" Text="วันที่สอบ(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndExamDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndExamDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEEndExamDate" PopupButtonID="imgPopup_txtEndExamDate"
                                                        runat="server" TargetControlID="txtEndExamDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPaidGroup" runat="server" Text="เลขที่ใบสั่งจ่ายกลุ่ม" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtPaidGroup" runat="server" CssClass="txt" onkeypress="return runScript(event)"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblLicenseType" runat="server" Text="ประเภทใบอนุญาต" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlLicenseType" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTestingNo" runat="server" Text="รหัสรอบสอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtTestingNo" runat="server" CssClass="txt" onkeypress="return runScript(event)"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click"
                                                        Width="75px" />
                                                    &nbsp; &nbsp; &nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnMainCancle_Click"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <%--      <table style="width: 100%;">
                            <tr>
                                <td>
                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>--%>
                        <asp:Panel ID="PnlDetailSearchGridView" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <%--<div style="float: right; padding-right: 50px;">
                                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/image/exel.png" OnClick="btnExportExcel_Click"
                                        Visible="false" ToolTip="ดาวน์โหลดข้อมูล" />
                                </div>--%>
                                <div class="box_textbox_bg_table" runat="server" id="boxResult" visible="false">
                                    <div class="box_texbox_add_box">
                                        <div id="divGv1" runat="server">
                                            <fieldset style="width: 98%; height: auto">
                                                <legend runat="server" visible="false" id="legendGV1">
                                                    <asp:Label ID="lblLegendGv1" runat="server" Style="font-weight: 700; color: #000066;
                                                        font-size: 16px;"></asp:Label></legend>
                                                <div style="float: right; padding-right: 2px;">
                                                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/image/exel.png" OnClick="btnExportExcel_Click"
                                                        Visible="false" ToolTip="ดาวน์โหลดข้อมูล" />
                                                </div>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:GridView ID="gvSearch" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                                GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="false" OnRowDataBound="gvSearch_RowDataBound">
                                                                <AlternatingRowStyle CssClass="altrow" />
                                                                <HeaderStyle BorderStyle="None" />
                                                                <PagerStyle HorizontalAlign="Left" />
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
                                                                            <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="20%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGroupRequsetNo" runat="server" Text='<%# Bind("GROUP_REQUEST_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันที่สร้างใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDatePayGv" runat="server" Text='<%# Bind("GROUP_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
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
                                                                            <asp:Label ID="lblAmountMoneyGv" runat="server" Text='<%# Bind("GROUP_AMOUNT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="จำนวนใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAmountPeopleGv" runat="server" Text='<%# Bind("PERSON_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="หมายเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRemarkGv" runat="server" Text='<%# Bind("REMARK") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ผู้สร้างใบสั่งจ่าย" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUploadBySession" runat="server" Text='<%# Bind("UPLOAD_BY_SESSION") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="สถานะการจ่ายเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatusText" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันหมดอายุใบสั่งจ่าย" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEXPIRATION_DATE" runat="server" Text='<%# Bind("EXPIRATION_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLastPrint" runat="server" Text='<%# Bind("LAST_PRINT","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="hplPrint" runat="server" OnClick="hplPrint_Click"><img src="../Images/print-icon.gif" title="พิมพ์" /></asp:LinkButton>&nbsp;&nbsp;
                                                                            <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>&nbsp;&nbsp;
                                                                            <asp:LinkButton ID="hplDelete" runat="server" OnClick="hplDelete_Click"><img src="../Images/delete_icon.png" title="ลบใบสั่งจ่าย" /></asp:LinkButton>&nbsp;&nbsp;
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:HiddenField ID="H_GroupRequestNoValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
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
                                                                <tr>
                                                                    <td style="text-align: center" colspan="2">
                                                                        <asp:Button ID="btnsumPrint" runat="server" CssClass="btn" Text="พิมพ์ใบสั่งจ่ายรวม"
                                                                            OnClick="btnsumPrint_Click" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="divGv2" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="padding-bottom: 20px; padding-top: 20px" align="left">
                                                                <asp:Label ID="group_request" runat="server" Text="" Style="font-weight: bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div style="float: right; padding-right: 2px;">
                                                                    <asp:ImageButton ID="btnExportExcel2" runat="server" ImageUrl="~/image/exel.png"
                                                                        OnClick="btnExportExcel2_Click" ToolTip="ดาวน์โหลดข้อมูล" />
                                                                </div>
                                                                <asp:GridView ID="gvSubDetail" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                                    GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                    ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="false">
                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <PagerStyle HorizontalAlign="Left" />
                                                                    <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                            ไม่พบข้อมูล...</div>
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblHeadRequestNoSubGv" runat="server" Text='<%# Bind("head_request_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="25%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTypePaidGv" runat="server" Text='<%# Bind("petition_type_name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmountPeopleSubGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ชื่อ-นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmountMoneySubGv" runat="server" Text='<%# Bind("FIRSTLASTNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amount","{0:n2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="วันที่ออกใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDateMadeGv" runat="server" Text='<%# Bind("SUBPAYMENT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="วันที่สร้างใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcreateD" runat="server" Text='<%# Bind("created_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 80%">
                                                                            <asp:Button ID="btnPreviousGvSearch2" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch2_Click" />
                                                                            <asp:TextBox ID="txtNumberGvSearch2" runat="server" Width="15px" Visible="false"
                                                                                Text="1" Enabled="false"></asp:TextBox>
                                                                            <asp:Label ID="lblParaPage2" runat="server" Text="/" Visible="false"></asp:Label>
                                                                            <asp:Label ID="txtTotalPage2" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Button ID="btnNextGvSearch2" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch2_Click" />
                                                                            &nbsp;&nbsp;
                                                                            <asp:Label ID="lblHeadInputMaxrow2" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                            <asp:TextBox ID="txtInputMaxrow2" runat="server" Visible="false" Width="5%" onkeypress="return runScript(event)"
                                                                                MaxLength="4" Style="text-align: center" AutoComplete="off" onpaste="return false;"></asp:TextBox>
                                                                            &nbsp;
                                                                            <asp:Button ID="btngo2" runat="server" Text="Go" OnClick="hplGo2_Click" Visible="false" />
                                                                        </td>
                                                                        <td style="width: 20%; text-align: right">
                                                                            <asp:Label ID="lblHeadTotal2" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblTotalrecord2" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblEndTotal2" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <br />
                                        <br />
                                        <div id="divNon" runat="server">
                                            <fieldset style="width: 98%; height: auto">
                                                <legend class="style1"><strong>ใบสั่งจ่ายที่เกี่ยวข้อง</strong></legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td align="right">
                                                            <asp:ImageButton ID="btnExport3" runat="server" ImageUrl="~/image/exel.png" OnClick="btnExport3_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr style="width: 100%">
                                                        <td align="right">
                                                            <asp:GridView ID="GVSearchN" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                BorderWidth="1px" CellSpacing="-1" CssClass="datatable" EnableTheming="True"
                                                                GridLines="None" ShowHeaderWhenEmpty="True" Visible="False" Width="100%" OnRowDataBound="gvSearchNon_RowDataBound">
                                                                <AlternatingRowStyle CssClass="altrow" />
                                                                <HeaderStyle BorderStyle="None" />
                                                                <PagerStyle HorizontalAlign="Left" />
                                                                <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                <EmptyDataTemplate>
                                                                    <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                        ไม่พบข้อมูล...</div>
                                                                </EmptyDataTemplate>
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                        <ItemStyle CssClass="td-center" Width="5%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายกลุ่ม" ItemStyle-HorizontalAlign="Center"
                                                                        Visible="true">
                                                                        <ItemStyle Width="20%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGroupRequsetNo" runat="server" Text='<%# Bind("GROUP_REQUEST_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันที่สร้างใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDatePayGv" runat="server" Text='<%# Bind("GROUP_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันที่รับชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblReceiptDatePayGv" runat="server" Text='<%# Bind("PAYMENT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="สถานะการจ่ายเงิน" ItemStyle-HorizontalAlign="Center"
                                                                        Visible="true">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันหมดอายุใบสั่งจ่าย" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblEXPIRATION_DATE" runat="server" Text='<%# Bind("EXPIRATION_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="hplViewNon" runat="server" OnClick="hplViewNon_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:HiddenField ID="Non_GroupRequestNoValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 80%">
                                                            <asp:Button ID="btnPreviousGvSearchNon" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearchNon_Click" />
                                                            <asp:TextBox ID="txtNumberGvSearchNon" runat="server" Width="15px" Visible="false"
                                                                Text="1" Enabled="false"></asp:TextBox>
                                                            <asp:Label ID="lblParaPageNon" runat="server" Text="/" Visible="false"></asp:Label>
                                                            <asp:Label ID="txtTotalPageNon" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnNextGvSearchNon" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearchNon_Click" />
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="lblHeadInputMaxrowNon" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                            <asp:TextBox ID="txtInputMaxrowNon" runat="server" AutoComplete="off" Visible="false"
                                                                Width="5%" onkeypress="return runScript(event)" MaxLength="4" Style="text-align: center"
                                                                onpaste="return false;" ondrop="return false;"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Button ID="btngoNon" runat="server" Text="Go" OnClick="btnSearch_Click" />
                                                        </td>
                                                        <td style="width: 20%; text-align: right">
                                                            <asp:Label ID="lblHeadTotalNon" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                            <asp:Label ID="lblTotalrecordNon" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                            <asp:Label ID="lblEndTotalNon" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div id="divGvNonSubPayment" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="padding-bottom: 20px; padding-top: 20px" align="left">
                                                                <asp:Label ID="lblNonGroupPayment" runat="server" Text="" Visible="false" Style="font-weight: bold"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:ImageButton ID="btnExport4" runat="server" ImageUrl="~/image/exel.png" OnClick="btnExport4_Click"
                                                                    Visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="GVSubNonPayment" runat="server" Width="100%" CssClass="datatable"
                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                    ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="false">
                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <PagerStyle HorizontalAlign="Left" />
                                                                    <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                            ไม่พบข้อมูล...</div>
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="20%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblHeadRequestNoSubGv" runat="server" Text='<%# Bind("head_request_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="25%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTypePaidGv" runat="server" Text='<%# Bind("petition_type_name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmountPeopleSubGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ชื่อ-นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmountMoneySubGv" runat="server" Text='<%# Bind("FIRSTLASTNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("amount","{0:n2}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="วันที่ออกใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDateMadeGv" runat="server" Text='<%# Bind("SUBPAYMENT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="วันที่สร้างใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblcreateD" runat="server" Text='<%# Bind("created_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 80%">
                                                                            <asp:Button ID="btnPreviousGvSearch3" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch3_Click" />
                                                                            <asp:TextBox ID="txtNumberGvSearch3" runat="server" Width="15px" Visible="false"
                                                                                Text="1" Enabled="false"></asp:TextBox>
                                                                            <asp:Label ID="lblParaPage3" runat="server" Text="/" Visible="false"></asp:Label>
                                                                            <asp:Label ID="txtTotalPage3" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Button ID="btnNextGvSearch3" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch3_Click" />
                                                                            &nbsp;&nbsp;
                                                                            <asp:Label ID="lblHeadInputMaxrow3" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                            <asp:TextBox ID="txtInputMaxrow3" runat="server" AutoComplete="off" Visible="false"
                                                                                Width="5%" onkeypress="return runScript(event)" MaxLength="4" Style="text-align: center"
                                                                                onpaste="return false;" ondrop="return false;"></asp:TextBox>
                                                                            &nbsp;
                                                                            <asp:Button ID="btngo3" runat="server" Text="Go" OnClick="btnGo3_Click" Visible="false" />
                                                                        </td>
                                                                        <td style="width: 20%; text-align: right">
                                                                            <asp:Label ID="lblHeadTotal3" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblTotalrecord3" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblEndTotal3" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                        <asp:PostBackTrigger ControlID="btnExportExcel2" />
                        <asp:PostBackTrigger ControlID="btnExport3" />
                        <asp:PostBackTrigger ControlID="btnExport4" />
                        <%--   <asp:PostBackTrigger ControlID="btnExportExcel3" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <asp:HiddenField ID="hdfSession" runat="server" />
        <asp:HiddenField ID="hdfMemberType" runat="server" />
        <div style="clear: left;">
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Content>

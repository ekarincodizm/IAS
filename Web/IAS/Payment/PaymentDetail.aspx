<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="PaymentDetail.aspx.cs" Inherits="IAS.Payment.PaymentDetail" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .style1
        {
            width: 40%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <meta http-equiv="Content-Language" content="th">
    <meta http-equiv="content-Type" content="text/html; charset=window-874">
    <meta http-equiv="content-Type" content="text/html; charset=tis-620">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลการชำระ" BorderStyle="None" ID="lblDetailPay" runat="server" />
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
                                                    <asp:Label ID="lblTypePay" runat="server" Text="ประเภทการชำระ" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlTypePay" runat="server" CssClass="ddl">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStartDate" runat="server" Text="วันที่สั่งจ่าย(เริ่ม)" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartDate" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtStartDate_CalendarExtender" PopupButtonID="imgPopup_txtStartDate"
                                                        runat="server" Enabled="True" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndDate" runat="server" Text="วันที่สั่งจ่าย(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndDate" runat="server" AutoPostBack="true"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtEndDate_CalendarExtender" PopupButtonID="imgPopup_txtEndDate"
                                                        runat="server" Enabled="True" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: left;" rowspan="2">
                                                    <asp:Button ID="btnSearch" runat="server" Width="75px" Text="ค้นหา" CssClass="btn"
                                                        OnClick="btnSearch_Click" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblOrder" runat="server" Text="ใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtOrder" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblIdCard" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtIdCard" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblReceiptNumber" runat="server" Text="เลขที่ใบเสร็จ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtReceiptNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <%--   <table style="width: 100%;">
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
                                <div style="float: right; padding-right: 20px;">
                                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/image/exel.png" OnClick="btnExportExcel_Click"
                                        Visible="false" ToolTip="ดาวน์โหลดข้อมูล" />
                                </div>
                                <div class="box_textbox_bg_table" runat="server" id="boxResult" visible="false">
                                    <div class="box_texbox_add_box">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EnableTheming="True" CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1"
                                                        Width="100%" OnRowDataBound="gvSearch_RowDataBound">
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
                                                                <ItemStyle Width="5%" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectPayment" runat="server" AutoPostBack="true" Checked="false"
                                                                        OnCheckedChanged="chkSelectPayment_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทการชำระ" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTypePayGv" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="บัตรประชาชน" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ-นามสกุล" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FIRST_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="นามสกุล" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderGv" runat="server" Text='<%# Bind("group_request_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่ออกใบสั่งจ่าย" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDatePayGv" runat="server" Text='<%# Bind("CREATED_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่ชำระเงิน" ItemStyle-HorizontalAlign="Center"
                                                                Visible="false">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptDateGv" runat="server" Text='<%# Bind("payment_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบเสร็จ" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptNumberGv" runat="server" Text='<%# Bind("RECEIPT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TestingNo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestingNoGv" runat="server" Text='<%# Bind("head_request_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAMOUNTGv" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HeadNo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHeadNoGv" runat="server" Text='<%# Bind("head_request_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PaymentNo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentNoGv" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หมายเหตุ">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRecordStatus" runat="server" Text='<%# Bind("ACCOUNTING") %>'></asp:Label>
                                                                    <asp:LinkButton ID="ltnRecordStatus" runat="server" Visible="false" OnClick="ltnRecordStatus_Click">LinkButton</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="ดำเนินการ">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplPrint" runat="server" OnClick="btnPrint_Click"><img src="../Images/print-icon.gif" title="พิมพ์" /></asp:LinkButton>&nbsp;&nbsp;
                                                                    <asp:LinkButton ID="linkDownload" runat="server" OnClick="btnDownload_Click"><img src="../Images/download-icon.gif"  title="Download"/> </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPathFile" runat="server" Text='<%# Bind("RECEIVE_PATH") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserUpload" runat="server" Text='<%# Bind("UPLOAD_BY_SESSION") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiveDate" runat="server" Text='<%# Bind("RECEIPT_DATE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAMOUNT" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
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
                                                                <td class="style1">
                                                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Enabled="false"
                                                                        Text="1"></asp:TextBox>
                                                                    <asp:Label ID="lblParaPage1" runat="server" Text="/" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblTotalPage1" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblHeadInputMaxrow1" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                    <asp:TextBox ID="txtInputMaxrow1" runat="server" Visible="false" Width="10%" onkeypress="return runScript(event)"
                                                                        MaxLength="4" AutoComplete="off" Style="text-align: center" ondrop="return false;"
                                                                        onpaste="return false;"></asp:TextBox>
                                                                    &nbsp;
                                                                    <asp:Button ID="btngo1" runat="server" Text="Go" OnClick="btnSearch_Click" Visible="false" />
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btn_Print_All" runat="server" Text="พิมพ์ใบเสร็จรวม" OnClick="Print_All"
                                                                        Visible="false" CssClass="btn" />
                                                                    &nbsp;<asp:Button ID="btn_Load_All" runat="server" Text="ดาวน์โหลดใบเสร็จรวม" OnClick="Load_All"
                                                                        Visible="false" CssClass="btn" />
                                                                </td>
                                                                <td style="width: 20%; text-align: right">
                                                                    <asp:Label ID="lblHeadTotal1" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblTotalrecord1" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblEndTotal1" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="PnlDetail" runat="server" Visible="false">
                                <div class="box_body_content">
                                    <div class="box_t_h">
                                        ::
                                        <asp:Label Text="การชำระ" BorderStyle="None" ID="lblPayDetail" runat="server" />
                                    </div>
                                    <div class="box_textbox_bgadd">
                                        <div class="box_texbox_add_box">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailOrderPay" runat="server" Text="ใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:TextBox ID="txtDetailOrderPay" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailExamID" runat="server" Text="เลขที่การสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtDetailExamID" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailCheckOutOrderDate" runat="server" Text="วันที่ออกใบสั่งจ่าย"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:TextBox ID="txtDetailCheckOutOrderDate" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailTypeOrder" runat="server" Text="ประเภทใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtDetailTypeOrder" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailIDCard" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:TextBox ID="txtDetailIDCard" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailCompanyID" runat="server" Text="รหัสบริษัท" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtDetailCompanyID" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailLicenseNumber" runat="server" Text="เลขที่ใบอนุญาต" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:TextBox ID="txtDetailLicenseNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailLicenseNumberComplaint" runat="server" Text="เลขที่ใบอนุญาตที่ยื่นคำร้อง"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtDetailLicenseNumberComplaint" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailReceiptNumber" runat="server" Text="เลขที่ใบเสร็จรับเงิน"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:TextBox ID="txtDetailReceiptNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailReceiptNumberDate" runat="server" Text="วันที่บันทึกเลขที่ใบเสร็จรับเงิน"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtDetailReceiptNumberDate" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblAmount" runat="server" Text="จำนวนเงิน" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailTypeLicenseNumber" runat="server" Text="รหัสประเภทใบอนุญาต"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtDetailTypeLicenseNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="PnlPopup" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnS1" runat="server" Style="display: none" />
                                            <ajaxToolkit:ModalPopupExtender ID="MPDetail" runat="server" TargetControlID="btnS1"
                                                PopupControlID="pnlValidateprop" BackgroundCssClass="modalbackground" CancelControlID="btnCancle">
                                            </ajaxToolkit:ModalPopupExtender>
                                            <asp:Panel ID="pnlValidateprop" Style="display: inherit" runat="server" CssClass="modalpopup"
                                                Width="800px">
                                                <div style="width: 100%;">
                                                    <div class="box_body_content">
                                                        <div class="box_t_h" style="width: 50%;">
                                                            ::ดาวโหลดใบเสร็จรับเงิน
                                                        </div>
                                                        <div class="box_textbox_bg_table">
                                                            <div class="box_texbox_add_box">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblheadpettition" runat="server" Text="ประเภทการชำระ :" CssClass="lbl"></asp:Label>
                                                                            <asp:Label ID="lbldetailPettion" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblheadGroupRequestNo" runat="server" Text="เลขที่ใบสั่งจ่าย :" CssClass="lbl"></asp:Label>
                                                                            <asp:Label ID="lbldetailGroupRequestNo" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblheadIdCard" runat="server" Text="รหัสประจำตัวประชาชน :" CssClass="lbl"></asp:Label>
                                                                            <asp:Label ID="lbldetailPopupIdCard" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblheadCreateDate" runat="server" Text="วันที่ออกใบสั่งจ่าย :" CssClass="lbl"></asp:Label>
                                                                            <asp:Label ID="lbldetailCreateDate" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblHeadName" runat="server" Text="ชื่อ-นามสกุล :" CssClass="lbl"></asp:Label>
                                                                            <asp:Label ID="lblDetailName" runat="server" CssClass="lbl"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="padding-bottom: 20px">
                                                                        <td style="padding-bottom: 20px">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <asp:GridView ID="GVPopupReceipt" runat="server" Width="100%" CssClass="datatable"
                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                    ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="false" OnRowDataBound="GVPopupReceipt_RowDataBound">
                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center">
                                                                            ไม่พบข้อมูล...</div>
                                                                    </EmptyDataTemplate>
                                                                    <PagerStyle HorizontalAlign="Left" />
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="CheckallPopup" runat="server" AutoPostBack="true" OnCheckedChanged="CheckallPopup_CheckedChanged" />
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="5%" />
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelectPaymentPopup" runat="server" AutoPostBack="true" Checked="false"
                                                                                    OnCheckedChanged="chkSelectPaymentPopup_CheckedChanged" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="td-center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRunno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่ใบเสร็จ" ItemStyle-HorizontalAlign="Center"
                                                                            Visible="true">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReceiptNumberGv" runat="server" Text='<%# Bind("RECEIPT_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="HeadNo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                            <ItemStyle Width="15%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblHeadNoGv" runat="server" Text='<%# Bind("head_request_no") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PaymentNo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                            <ItemStyle Width="15%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPaymentNoGv" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPathFile" runat="server" Text='<%# Bind("RECEIVE_PATH") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="true" HeaderText="วันที่ออกใบเสร็จ">
                                                                            <ItemStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblReceiveDate" runat="server" Text='<%# Bind("RECEIPT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="true" HeaderText="จำนวนเงิน">
                                                                            <ItemStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAMOUNT" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderText="ดำเนินการ">
                                                                            <ItemStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="hplPrint" runat="server" OnClick="btnPrint_Click"><img src="../Images/print-icon.gif" title="พิมพ์" /></asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="linkDownload" runat="server" OnClick="btnDownload_Click"><img src="../Images/download-icon.gif"  title="Download"/> </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField Visible="false">
                                                                            <ItemStyle Width="10%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUserUpload" runat="server" Text='<%# Bind("UPLOAD_BY_SESSION") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="text-align: center;" align="center">
                                                    <div>
                                                        <table width="100%">
                                                            <tr align="center">
                                                                <td>
                                                                    <br />
                                                                    <asp:Button ID="btnPrintPopup" runat="server" Text="พิมพ์ใบเสร็จรวม" OnClick="PrintPopup_All"
                                                                        Visible="false" CssClass="btn" />&nbsp;&nbsp;
                                                                    <asp:Button ID="btnPopupDownload" runat="server" Text="ดาวโหลดใบเสร็จรวม" CssClass="btn"
                                                                        OnClick="LoadPopup_All" Visible="false" />
                                                                    <asp:Button ID="btnCancle" runat="server" Text="ปิด" CssClass="btn" Visible="true" />
                                                                </td>
                                                            </tr>
                                                            <caption>
                                                                &nbsp;
                                                                <br />
                                                            </caption>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <asp:HiddenField ID="hdfSession" runat="server" />
        <asp:HiddenField ID="hdfMemberType" runat="server" />
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

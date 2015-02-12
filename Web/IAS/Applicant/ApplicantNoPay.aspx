<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ApplicantNoPay.aspx.cs" Inherits="IAS.Applicant.ApplicantNoPay" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
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
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .datepicker
        {
            margin-left: 17px;
        }
        .style1
        {
            width: 11%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ผู้สมัครสอบที่ไม่ได้ชำระเงิน" BorderStyle="None" ID="lblCandidateNoPay"
                    runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelGv" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ค้นหา
                                    <asp:Label ID="lblDayValue" runat="server" Visible="false"></asp:Label></div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table id="tblAdd" style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblStartPaidSubDate" runat="server" Text="วันที่หมดอายุใบสั่งจ่าย(เริ่ม)"
                                                        CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartPaidSubDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtStartPaidSubDate_CalendarExtender" PopupButtonID="imgPopup_txtStartPaidSubDate"
                                                        runat="server" Enabled="True" TargetControlID="txtStartPaidSubDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndPaidSubDate" runat="server" Text="วันที่หมดอายุใบสั่งจ่าย(สิ้นสุด)"
                                                        CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndPaidSubDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtEndPaidSubDate_CalendarExtender" PopupButtonID="imgPopup_txtEndPaidSubDate"
                                                        runat="server" Enabled="True" TargetControlID="txtEndPaidSubDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;" class="style1">
                                                    <asp:Label ID="Label1" runat="server" Text="วันที่สอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtTestingDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtTestingDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtTestingDate_CalendarExtender" PopupButtonID="imgPopup_txtTestingDate" runat="server"
                                                        Enabled="True" TargetControlID="txtTestingDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="Label2" runat="server" Text="รหัสรอบสอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtTestingNo" runat="server" onpaste="return false;" ondrop="return false;"
                                                        AutoComplete="Off" onkeypress="return runScript(event)"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label3" runat="server" Text="รหัสสนามสอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtExamPlaceCode" runat="server" onpaste="return false;" ondrop="return false;"
                                                        AutoComplete="Off" onkeypress="return runScript(event)"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;" class="style1">
                                                    <asp:Label ID="Label4" runat="server" Text="เลขที่ใบสั่งจ่าย :" CssClass="lbl" Visible="False"></asp:Label>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtPaymentNo" runat="server" onpaste="return false;" ondrop="return false;"
                                                            AutoComplete="Off" onkeypress="return runScript(event)" Visible="False"></asp:TextBox>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="Label5" runat="server" Text="รหัสสานักงานประกันภัย :" CssClass="lbl"
                                                        Visible="False"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlAcceptOffCode" runat="server" Height="21px" Width="272px"
                                                        Visible="False">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label6" runat="server" Text="รหัสสนามสอบ :" CssClass="lbl" Visible="False"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="TextBox2" runat="server" onpaste="return false;" ondrop="return false;"
                                                        AutoComplete="Off" onkeypress="return runScript(event)" Visible="False"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;" class="style1">
                                                    <asp:Label ID="Label7" runat="server" Text="เลขที่ใบสั่งจ่าย :" CssClass="lbl" Visible="False"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="TextBox3" runat="server" onpaste="return false;" ondrop="return false;"
                                                        AutoComplete="Off" onkeypress="return runScript(event)" Visible="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                    &nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn"
                                                        OnClick="btnCancle_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="clear">
                        </div>
                        <table width="100%">
                            <tr>
                                <td>
                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelGv">
                                        <ProgressTemplate>
                                            Please wait....</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PnlDetail" runat="server" Visible="false">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div class="box_body_content">
                                            <div class="box_t_h">
                                            </div>
                                            <div style="float: right; padding-right: 20px;">
                                                <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/image/exel.png" OnClick="btnExportExcel_Click"
                                                    Visible="false" ToolTip="ดาวโหลดข้อมูล" />
                                            </div>
                                            <div class="box_textbox_bg_table">
                                                <div class="box_texbox_add_box">
                                                    <asp:GridView ID="gvSearch" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                        GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowDataBound="gvSearch_RowDataBound" ShowHeaderWhenEmpty="True" AllowSorting="True"
                                                        Visible="false">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" HorizontalAlign="Center" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText=" " Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="CheckAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckAll_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="Chk_nopay" runat="server" AutoPostBack="true" OnCheckedChanged="Chk_nopay_CheckedChanged">
                                                                    </asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="4%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNUM" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGroupRequsetNo" runat="server" Text='<%# Bind("GROUP_REQUEST_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่หมดอายุใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptDatePayGv" runat="server" Text='<%# Bind("EXPIRATION_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนเงินในใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmountMoneyGv" runat="server" Text='<%# Bind("GROUP_AMOUNT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินกาาร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <%--                              <asp:LinkButton ID="hpldel" runat="server"  OnClientClick="return confirmDelete();" OnClick="btnDelete_Click"><img src="../Images/x-icon.png" title="ยกเลิก" /></asp:LinkButton>&nbsp;&nbsp;
                                                                    --%>
                                                                    <asp:LinkButton ID="hplView" runat="server" OnClick="hplViewG_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="H_NOW_REQUEST" runat="server" />
                                                </div>
                                            </div>
                                            <table width="98%">
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                        <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false"
                                                            Text="1"></asp:TextBox>
                                                        <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                        <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                        <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;"
                                                            Visible="true" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                        <asp:TextBox ID="rowPerpage" runat="server" Visible="false" MaxLength="4" onpaste="return false;"
                                                            ondrop="return false;" AutoComplete="Off" onkeypress="return runScript(event)"
                                                            Width="5%" Style="text-align: center"></asp:TextBox>&nbsp;
                                                        <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearch_Click" />
                                                    </td>
                                                    <td align="right">
                                                        <asp:Panel ID="panel_count" runat="server" Visible="false">
                                                            จำนวน <b>
                                                                <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label></b> รายการ
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table align="center" style="width: 100%;">
                                            <tr>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn" OnClick="btnDelete_Click"
                                                        OnClientClick="return confirmDelete();" Text="ยกเลิกสมัครสอบ" Visible="false"
                                                        Width="145px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Panel ID="gv2" runat="server">
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    <asp:Label ID="group_request" runat="server" Text=""></asp:Label></div>
                                                <div style="float: right; padding-right: 20px;">
                                                    <asp:ImageButton ID="exportExcel2" runat="server" ImageUrl="~/image/exel.png" Visible="false"
                                                        ToolTip="ดาวโหลดข้อมูล" OnClick="exportExcel2_Click" />
                                                </div>
                                                <div class="box_textbox_bg_table">
                                                    <div class="box_texbox_add_box">
                                                        <asp:GridView ID="gvApplicantNoPay" runat="server" Width="100%" EnableTheming="True"
                                                            AutoGenerateColumns="False" CssClass="datatable" CellSpacing="-1" GridLines="None"
                                                            BorderWidth="1" ShowHeaderWhenEmpty="True" AllowSorting="True">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center">
                                                                    ไม่พบข้อมูล...</div>
                                                            </EmptyDataTemplate>
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Left" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNUM" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="25%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdCardGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่สมัคร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCreateAccountGv" runat="server" Text='<%# Bind("CREATED_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FIRSTNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExamDateGv" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TESTINGNO" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTestingNoGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="APPLICANTCODE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApplicantCodeGv" runat="server" Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EXAMPLACECODE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExamPlaceCodeGv" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด"  /></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <table width="98%">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Button ID="btn_P" runat="server" Text="<" Visible="true" OnClick="btnPreviousDGvSearch_Click" />
                                                                <asp:TextBox ID="txt_Page_now" runat="server" Width="15px" Visible="true" Enabled="false"
                                                                    Text="1"></asp:TextBox>
                                                                <asp:Label ID="lblPer" runat="server" Text="/" Visible="false"></asp:Label>
                                                                <asp:Label ID="lbltotalP" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btn_N" runat="server" OnClick="btnNextDGvSearch_Click" Text="&gt;"
                                                                    Visible="true" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblThai" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                                <asp:TextBox ID="txt_input" runat="server" Visible="false" MaxLength="4" Width="5%"
                                                                    Style="text-align: center" onpaste="return false;" AutoComplete="Off" ondrop="return false;"
                                                                    onkeypress="return runScript(event)"></asp:TextBox>&nbsp;
                                                                <asp:Button ID="btn_GO" runat="server" Visible="false" Text="GO" OnClick="btn_GO_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <asp:Panel ID="panel_count2" runat="server">
                                                                    จำนวน <b>
                                                                        <asp:Label ID="lblRec" runat="server" Visible="false"></asp:Label></b> รายการ
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="detaiLL" runat="server" Visible="false" Enabled="false">
                                <div class="box_body_content">
                                    <div class="box_t_h">
                                        ::<asp:Label Text="ข้อมูลผู้สมัครสอบที่ไม่ได้ชำระเงิน" BorderStyle="None" ID="lblDetailCandidateNoPay"
                                            runat="server" /></div>
                                    <div class="box_textbox_bgadd">
                                        <div class="box_texbox_add_box">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailTitleName" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailTitle" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailName" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailSurName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailSurName" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailIdNumber" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailIdNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:RadioButtonList ID="rblDetailSex" runat="server" RepeatDirection="Horizontal"
                                                            CssClass="rb">
                                                            <asp:ListItem Text="ชาย" Value="M" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailBirthDay" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailEducation" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailExamDate" runat="server" Text="วันที่สอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailExamDate" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailRegExamID" runat="server" Text="เลขที่ผู้สมัครสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailRegExamID" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailExamNumber" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailExamNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailPlaceExam" runat="server" Text="สนามสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailPlaceExam" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblDetailAssociationExamID" runat="server" Text="สำนักงานประกันภัยที่สมัครสอบ"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtDetailAssociationExamID" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblInsuranceCompany" Visible="false" runat="server" Text="รหัสบริษัทประกันภัย"
                                                            CssClass="lbl"></asp:Label>
                                                        <%--<span>:</span>--%>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtInsuranceCompany" Visible="false" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDelete" />
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                        <asp:PostBackTrigger ControlID="exportExcel2" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

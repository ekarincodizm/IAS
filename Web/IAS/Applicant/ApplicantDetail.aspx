<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ApplicantDetail.aspx.cs" Inherits="IAS.Applicant.ApplicantDetail" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function confirmDelete() {
            return confirm('คุณต้องการลบหรือไม่?');
        }

        function showload() {
            document.getElementById('Detail_UCLoadingProgress1_Image1').src = '../Images/newloading23.gif';
            $find('Detail_UCLoadingProgress1_mpeProgress').show();

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

        function spacialCaractor(evt) {
            var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()1234567890๑๒๓๔฿๕๖๗๘๙๐";
            var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
            var ch = String.fromCharCode(charCode);
            if (iChars.indexOf(ch) == -1) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลผู้สมัครสอบ" BorderStyle="None" ID="lblDetailPersonExam" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    :: ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStartExamDate0" runat="server" CssClass="lbl" Text="วันที่สมัครสอบ(เริ่ม)"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartCandidates" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartCandidates" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtStartCandidates_CalendarExtender" PopupButtonID="imgPopup_txtStartCandidates"
                                                        runat="server" Enabled="True" TargetControlID="txtStartCandidates">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndExamDate0" runat="server" Text="วันที่สมัครสอบ(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndCandidates" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndCandidates" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtEndCandidates_CalendarExtender" PopupButtonID="imgPopup_txtEndCandidates"
                                                        runat="server" Enabled="True" TargetControlID="txtEndCandidates">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td align="left">
                                                    &nbsp;
                                                </td>
                                                <td rowspan="7">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancle_Click"
                                                        Text="ยกเลิก" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStartExamDate" runat="server" CssClass="lbl" Text="วันที่สอบ(เริ่ม)"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartExamDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartExamDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtStartExamDate_CalendarExtender" PopupButtonID="imgPopup_txtStartExamDate"
                                                        runat="server" Enabled="True" TargetControlID="txtStartExamDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndExamDate" runat="server" CssClass="lbl" Text="วันที่สอบ(สิ้นสุด)"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndExamDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndExamDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtEndExamDate_CalendarExtender" PopupButtonID="imgPopup_txtEndExamDate"
                                                        runat="server" Enabled="True" TargetControlID="txtEndExamDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblSurName0" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl" Width="50%" DataTextField="Name"
                                                        DataValueField="Id">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblIdNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtIdCard" runat="server" autocomplete="off" CssClass="txt" MaxLength="13"
                                                        onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblSurName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtSurName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEaxmID" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEaxmID" runat="server" CssClass="txt" MaxLength="6" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndExamDate1" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="ddl" Width="50%" DataTextField="Name"
                                                        DataValueField="Id">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblBillNo0" runat="server" CssClass="lbl" Text="ใบสั่งจ่ายเลขที่"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="txt" onkeypress="return runScript(event);"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblBillNo" runat="server" CssClass="lbl" Text="ใบเสร็จเลขที่"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="txt" onkeypress="return runScript(event);">
                                                    </asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblBillNo1" runat="server" CssClass="lbl" Text="ผลสอบ"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlExamResult" runat="server" AutoPostBack="false" CssClass="ddl"
                                                        OnSelectedIndexChanged="ddlPlaceGroup_SelectedIndexChanged" Width="50%">
                                                        <asp:ListItem Value="all">ทั้งหมด</asp:ListItem>
                                                        <asp:ListItem Value="null">รอผลการสอบ</asp:ListItem>
                                                        <asp:ListItem Value="P">ผ่าน</asp:ListItem>
                                                        <asp:ListItem Value="F">ตก</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEaxmID0" runat="server" CssClass="lbl" Text="หน่วยงานที่จัดสอบ"></asp:Label>
                                                    :&nbsp;
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlPlaceGroup" runat="server" AutoPostBack="True" CssClass="ddl"
                                                        OnSelectedIndexChanged="ddlPlaceGroup_SelectedIndexChanged" Width="50%">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEaxmID1" runat="server" CssClass="lbl" Text="สนามสอบ"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlPlace" runat="server" CssClass="ddl" Width="50%">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <%-- <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....</ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PnlDetailSearchGridView" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <div style="float: right; padding-right: 20px;">
                                    <asp:ImageButton ImageUrl="~/image/csv.png" ID="btnExportCSV" runat="server" Visible="false"
                                        ToolTip="ดาวน์โหลดไฟล์นำเข้าผู้สมัครสอบ" OnClick="btnExportCSV_Click" />
                                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/image/exel.png" OnClick="btnShowModalPopUpEmail"
                                        ToolTip="ดาวน์โหลดข้อมูลผู้สมัครสอบ" Visible="true" />
                                </div>
                                <div class="box_textbox_bg_table" runat="server" id="bludDiv1">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvDetail" runat="server" GridLines="None" Width="100.5%" CssClass="datatable"
                                                        EnableTheming="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" BorderWidth="1px"
                                                        CellSpacing="-1" AllowSorting="True" PageSize="20" OnRowDataBound="gvDetail_RowDataBound">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApplicantCode" runat="server" Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRownum" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHeadRequestNO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="9%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="6%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FIRSTNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="7%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestingNoGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseTypeNameGv" runat="server" Text='<%# Bind("LICENSE_TYPE_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="7%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestingDateGv" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เวลาสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="7%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltestingtime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ผลการสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="7%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblResultGv" runat="server" Text='<%# Bind("RESULT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="9%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentNoGv" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่ชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="7%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptDateGv" runat="server" Text='<%# Bind("PAYMENT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExpireDate" runat="server" Text='<%# Bind("EXPIRATION_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สถานะการชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="7%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ใบเสร็จรับเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle Width="8%" />
                                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBillNoGv" runat="server" Text='<%# Bind("BILLNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หมายเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle Width="9%" />
                                                                <ItemStyle Width="9%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCancelReason" runat="server" Text='<%# Bind("CANCEL_REASON") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="APPLICANTCODE" ItemStyle-HorizontalAlign="Center"
                                                                Visible="False">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAPPLICANTCODEGv" runat="server" Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EXAMPLACECODE" ItemStyle-HorizontalAlign="Center"
                                                                Visible="False">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEXAMPLACECODEGv" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EXAMPLACECODE" ItemStyle-HorizontalAlign="Center"
                                                                Visible="False">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestTimeCode" runat="server" Text='<%# Bind("TEST_TIME_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EXAMPLACECODE" ItemStyle-HorizontalAlign="Center"
                                                                Visible="False">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="7%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="upnlnk" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                            <asp:LinkButton ID="hplEdit" runat="server" OnClick="hplEdit_Click"><img src="../Images/edit-icon.gif" title="แก้ไขรายละเอียด" /></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="hplView" />
                                                                            <asp:PostBackTrigger ControlID="hplEdit" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ยกเลิกการสมัคร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="upnCanCel" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:LinkButton ID="hplCancel" runat="server" OnClick="hplCancel_Click" OnClientClick="return confirmDelete();"><img src="../Images/x-icon.png" title="ลบรายละเอียด"   /></asp:LinkButton>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="hplCancel" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
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
                                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" Style="text-align: center"
                                                                    MaxLength="4" ondrop="return false;" AutoComplete="Off" onpaste="return false;"
                                                                    Width="5%" onkeypress="return runScript(event) "></asp:TextBox>&nbsp;
                                                                <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearch_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <div runat="server" id="div_totalPage">
                                                                    จำนวน <b>
                                                                        <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label></b> รายการ</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div id="showModalPopupEmail" runat="server">
                        </div>
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupEmail" runat="server" PopupControlID="PanelEmail"
                            TargetControlID="showModalPopupEmail" BackgroundCssClass="modalbackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="PanelEmail" runat="server" Style="display: none">
                            <div style="background-color: Green; padding: 2px; width: 350px;">
                                <table width="100%" cellpadding="0" cellspacing="0" bgcolor="white">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <div style="background-color: Green; height: 25px; color: White;">
                                                <b>ส่งไฟล์ให้สมาคม</b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <br />
                                            อีเมล์ :
                                            <br />
                                        </td>
                                        <td align="left">
                                            <br />
                                            <asp:TextBox ID="txtEmail" runat="server" Width="250px" CssClass="txt"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <br />
                                            <asp:Button ID="btnSendMail" runat="server" CssClass="btn" Text="ส่ง" OnClick="btnExportExcel_Click" />
                                            <asp:Button ID="btnCancelMail" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancelMail_Click" />
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PnlDetail" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    <asp:Label Text=":: ผู้สมัครสอบ" BorderStyle="None" ID="lblExaminationCandidate"
                                        runat="server" />
                                </div>
                                <div class="box_textbox_bgadd" runat="server" id="bludDiv2">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDetailIdNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน :"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailIdNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDetailIdNumber0" runat="server" CssClass="lbl" Text="รอบสอบพิเศษ :"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:CheckBox ID="chkSpecial" runat="server" />
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblDetailSurName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailSurName" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
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
                                                    &nbsp;<asp:Label ID="lblDetailExamNumber" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblInsuranceCompany1" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamLicenseType" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    &nbsp;<span><asp:Label ID="lblInsuranceCompany0" runat="server" CssClass="lbl" Text="บริษัทสังกัด"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtInsuranceCompany" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblDetailExamTime" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamTime" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblInsuranceCompany2" runat="server" CssClass="lbl" Text="ผลการสอบ :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailTotalExam" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    &nbsp;<span><asp:Label ID="lblDetailExamDate" runat="server" CssClass="lbl" Text="วันที่สอบ :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamDate" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblCandidateDate0" runat="server" CssClass="lbl" Text="สมาคมเจ้าของรอบสอบ :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamOwner" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblDetailRegExamID" runat="server" CssClass="lbl" Text="ลำดับที่นั่งสอบ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailRegExamID" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblCandidateDate" runat="server" CssClass="lbl" Text="วันที่สมัครสอบ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtCandidateDate" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    &nbsp;<span><asp:Label ID="lblCandidateDate1" runat="server" CssClass="lbl" Text="จังหวัด :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamProvince" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblDetailExamDate0" runat="server" CssClass="lbl" Text="หน่วยงานจัดสอบ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailPlaceGroup" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblDetailGroundExam" runat="server" CssClass="lbl" Text="สนามสอบ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailGroundExam" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblCandidateDate2" runat="server" CssClass="lbl" Text="เลขที่ใบเสร็จ :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamBillNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblNumberOrder" runat="server" CssClass="lbl" Text="เลขที่ใบสั่งจ่าย :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtNumberOrder" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblCandidateDate3" runat="server" CssClass="lbl" Text="วันที่ชำระเงิน :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtDetailExamPaymentDate" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="6">
                                                    <br />
                                                    <br />
                                                    <div class="box_t_h" align="left">
                                                        <asp:Label Text=":: ผลคะแนนสอบ" BorderStyle="None" ID="txt_score" runat="server" />
                                                    </div>
                                                    <br />
                                                    <br />
                                                    <asp:GridView ID="gvSubject" runat="server" Width="100%" AllowPaging="false" CssClass="datatable"
                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" EmptyDataText="ไม่มีข้อมูล..."
                                                        AutoGenerateColumns="False">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="รหัสวิชา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTypePermitGv" runat="server" Text='<%# Eval("SubjectCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วิชา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="80%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSubjectGv" runat="server" Text='<%# Eval("LicenseType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="คะแนน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPointGv" runat="server" Text='<%# Eval("Score") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:UpdatePanel ID="UplEditDetail" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <ajaxToolkit:ModalPopupExtender ID="ModEditDetail" runat="server" CancelControlID="btnCancel"
                                    TargetControlID="btnModal" PopupControlID="PnlEditDetail" BackgroundCssClass="modalbackground">
                                </ajaxToolkit:ModalPopupExtender>
                                <asp:Button ID="btnModal" runat="server" Text="Open" Style="display: none" />
                                <asp:Panel ID="PnlEditDetail" runat="server" CssClass="modalpopup" Style="display: none">
                                    <div class="box_body_content">
                                        <div class="box_t_h">
                                            ::แก้ไขข้อมูลการสอบ</div>
                                        <div class="box_textbox_bgadd">
                                            <div class="box_texbox_add_box">
                                                <table style="width: 100%; text-align: center;">
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label12" runat="server" CssClass="lbl" Text="รหัสสอบ :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <asp:TextBox ID="txtDetailTestingNO" runat="server" CssClass="txtreadonly" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="รหัสบัตรประชาชน :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <asp:TextBox ID="txtEditDetailIDCard" runat="server" CssClass="txt" MaxLength="13"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="EditDetailIDCardRequired" runat="server" ControlToValidate="txtEditDetailIDCard"
                                                                CssClass="failureNotification" ValidationGroup="editDetailValidationGroup">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <asp:DropDownList ID="ddlEditDetailTitle" runat="server" AutoPostBack="True" CssClass="ddl"
                                                                Width="100px" OnSelectedIndexChanged="ddlEditDetailTitle_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlEditDetailTitle"
                                                                CssClass="failureNotification" ValidationGroup="editDetailValidationGroup">*</asp:RequiredFieldValidator>
                                                            <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="ชื่อ :"></asp:Label>
                                                            <asp:TextBox ID="txtFirstNane" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstNane"
                                                                CssClass="failureNotification" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                            <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="นามสกุล :"></asp:Label>
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                                                                CssClass="failureNotification" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="วันเกิด :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <asp:TextBox ID="txtBirthDay" runat="server"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup_txtBirthDay" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                                runat="server" />
                                                            <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDay" runat="server" PopupButtonID="imgPopup_txtBirthDay" TargetControlID="txtBirthDay"
                                                                Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="BirthDayRequired" runat="server" ControlToValidate="txtBirthDay"
                                                                CssClass="failureNotification" ValidationGroup="editDetailValidationGroup">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="เพศ :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <asp:RadioButtonList ID="rblSex" runat="server" CssClass="rb" Enabled="false" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="ชาย" Value="M"></asp:ListItem>
                                                                <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="ระดับการศึกษา :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <span>
                                                                <asp:DropDownList ID="ddlEducation" runat="server" AutoPostBack="True" CssClass="ddl"
                                                                    Width="150px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="EducationRequired" runat="server" ControlToValidate="ddlEducation"
                                                                    CssClass="failureNotification" ValidationGroup="editDetailValidationGroup">*</asp:RequiredFieldValidator>
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label11" runat="server" CssClass="lbl" Text="ที่อยู่ :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <asp:TextBox ID="txtAddress" runat="server" Columns="10" CssClass="txt" Height="50px"
                                                                MaxLength="100" onkeyDown="return checkTextAreaMaxLength(this,event,'100');"
                                                                onkeypress="return checkTextAreaMaxLength(this,event,'100');" onkeyup="this.value=checkMaxLength(this);"
                                                                Rows="10" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 30%;">
                                                            <asp:Label ID="Label9" runat="server" CssClass="lbl" Text="รหัสพื้นที่ :"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left; width: 70%;">
                                                            <asp:TextBox ID="txtAreaCode" runat="server" CssClass="txt" Width="100px" MaxLength="8"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center; width: 100%;" colspan="2">
                                                            <asp:Button ID="btnSubmitEditDatail" runat="server" CssClass="btn" OnClick="btnSubmitEditDatail_Click"
                                                                Text="แก้ไข" ValidationGroup="editDetailValidationGroup" />
                                                            <asp:Button ID="btnCancelEditDatail" runat="server" CssClass="btn" OnClick="btnCancelEditDatail_Click"
                                                                Text="ยกเลิก" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center; width: 100%;">
                                                            <asp:ValidationSummary ID="editDetailValidationSummary" runat="server" CssClass="failureNotification"
                                                                HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="true" ShowSummary="False"
                                                                ValidationGroup="editDetailValidationGroup" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <uc1:UCLoadingProgress ID="UCLoadingProgress2" runat="server" />
                                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UplEditDetail">
                                                                <ProgressTemplate>
                                                                    Please wait image uploaded....</ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <uc2:UCModalError ID="UCModalError" runat="server" style="display: none" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" style="display: none" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                        <asp:PostBackTrigger ControlID="btnExportCSV" />
                        <asp:PostBackTrigger ControlID="ddlPlaceGroup" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ExamSchedule.aspx.cs" Inherits="IAS.Register.examSchedule" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmCancel() {
            return confirm('คุณต้องการปิดหรือไม่?');
        }

        function confirmDelete() {
            return confirm('คุณต้องการลบหรือไม่?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลสมัครสอบ" BorderStyle="None" ID="lblExam" runat="server" />
            </div>
        </div>
        <br />
        <br />
        <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
        <asp:UpdatePanel ID="upn" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="pnlSearch" runat="server">
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหารหัสสอบ" /></div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTypeLicense" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>:
                                        </td>
                                        <td class="style3">
                                            <asp:DropDownList ID="ddlTypeLicense" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td style="width: 17%;">
                                        </td>
                                        <td rowspan="3">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                                Text="ค้นหา" /><br />
                                            <br />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancle_Click"
                                                Text="ยกเลิก" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblYardGroup" runat="server" CssClass="lbl" Text="หน่วยงานจัดสอบ"></asp:Label>
                                                :</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlPlaceGroup" runat="server" CssClass="ddl" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlPlaceGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblYard" runat="server" CssClass="lbl" Text="สถานที่สอบ"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlPlace" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblMonth" runat="server" CssClass="lbl" Text="เดือน"></asp:Label>
                                                : </span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblYear" runat="server" CssClass="lbl" Text="ปี"></asp:Label>
                                                :</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtYear" runat="server" Style="text-align: center;" Width="120" />
                                            <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" RefValues=""
                                                ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID="" TargetButtonUpID=""
                                                TargetControlID="txtYear" Width="120" />
                                        </td>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblTimeExam" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                                :</span>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">
                                            <span>
                                                <asp:Label ID="lblDisplay" runat="server" CssClass="lbl" Text="แสดงผล"></asp:Label>
                                                : </span>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblDisplay" runat="server" AutoPostBack="True" CssClass="rb"
                                                OnSelectedIndexChanged="rblDisplay_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Text="ปฏิทิน" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="ตาราง" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlCalendar" runat="server" Visible="true">
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::ข้อมูลการสอบ</div>
                        <span style="float: right; padding-right: 75px;">
                            <asp:Button ID="btnInsert" runat="server" CssClass="btn" Text="เพิ่ม" OnClick="btnInsert_Click" /></span>
                        <div class="box_textbox_bg_table">
                            <div class="box_texbox_add_box">
                                <asp:Calendar ID="cldExam" runat="server" BackColor="White" BorderColor="#999999"
                                    CellPadding="4" DayNameFormat="Full" Font-Names="Verdana" Font-Size="10pt" ForeColor="Black"
                                    Height="382px" Width="100%" ShowGridLines="True" OnDayRender="cldExam_DayRender"
                                    OnSelectionChanged="cldExam_SelectionChanged" OnVisibleMonthChanged="cldExam_VisibleMonthChanged">
                                    <DayHeaderStyle BackColor="#3399FF" BorderColor="Black" Font-Bold="True" Height="50px"
                                        HorizontalAlign="center" />
                                    <DayStyle BorderColor="Black" Height="50px" HorizontalAlign="Center" Font-Bold="True" />
                                    <TitleStyle BorderColor="Black" Height="15px" HorizontalAlign="Center" BackColor="#3399FF" />
                                    <TodayDayStyle BackColor="#FFFF99" />
                                </asp:Calendar>
                            </div>
                        </div>
                    </div>
                    <div class="box_body_content">
                        <div class="box_t_h">
                        </div>
                        <div class="box_texbox_bg_table" id="Div1" runat="server">
                            <div class="box_texbox_add_box">
                                <br />
                                <asp:GridView ID="gvExamSchedule" runat="server" AllowPaging="false" AllowSorting="True"
                                    AutoGenerateColumns="False" CaptionAlign="Top" EnableTheming="True" CssClass="datatable"
                                    CellSpacing="-1" GridLines="None" BorderWidth="1" PagerSettings-FirstPageText="First"
                                    ShowHeaderWhenEmpty="True" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                    PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;"
                                    PagerStyle-HorizontalAlign="Center" Width="98%" OnRowDataBound="gvExamSchedule_RowDataBound"
                                    EmptyDataText="ไม่มีข้อมูล..." OnPageIndexChanging="gvExamSchedule_PageIndexChanging"
                                    PageSize="20">
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" />
                                    <AlternatingRowStyle CssClass="altrow" />
                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="7%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                                    OnClick="lnkExamNumber_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="7%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center"
                                            Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlaceName" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblProvince" runat="server" Text='<%# Bind("PROVINCE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                            Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamAdmission" runat="server" Text='<%# Bind("EXAM_ADMISSION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                            Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseTypeCodeNumber" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="7%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamFee" runat="server" Text='<%# Bind("EXAM_FEE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ที่นั่ง" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalApply" runat="server" Text='<%# Bind("TOTAL_APPLY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlTable" runat="server" Visible="false">
                    <div class="box_body_content">
                        <div class="box_t_h">
                        </div>
                        <div class="box_textbox_bg_table" id="boxgvTable" runat="server">
                            <div class="box_texbox_add_box">
                                <asp:GridView ID="gvTable" runat="server" AllowPaging="false" AllowSorting="True"
                                    AutoGenerateColumns="False" CellSpacing="-1" GridLines="None" BorderWidth="1px"
                                    CaptionAlign="Top" CellPadding="0" EnableTheming="True" PagerSettings-FirstPageText="First"
                                    EmptyDataText="ไม่มีข้อมูล..." PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                    PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;"
                                    PagerStyle-HorizontalAlign="Center" PageSize="20" CssClass="datatable" Width="100%"
                                    ShowHeaderWhenEmpty="True" OnPageIndexChanging="gvTable_PageIndexChanging" OnRowDataBound="gvTable_RowDataBound">
                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                    <PagerStyle HorizontalAlign="Center" />
                                    <HeaderStyle Font-Bold="True" />
                                    <AlternatingRowStyle CssClass="altrow" />
                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTableExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                                    OnClick="lnkTableExamNumber_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TESTING_DATE","{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center"
                                            Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblProvince" runat="server" Text='<%# Bind("PROVINCE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                            Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamAdmission" runat="server" Text='<%# Bind("EXAM_ADMISSION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รหัสสถานที่สอบ" ItemStyle-HorizontalAlign="Center"
                                            Visible="false">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                            Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseTypeCodeNumber" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamFee" runat="server" Text='<%# Bind("EXAM_FEE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ที่นั่ง" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalApply" runat="server" Text='<%# Bind("TOTAL_APPLY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div style="display: none">
                    <asp:Button ID="btnModal" runat="server" Text="Open" />
                </div>
                <ajaxToolkit:ModalPopupExtender ID="ModSingleApplicant" runat="server" CancelControlID="btnCancelPop"
                    OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                    Drag="true" BackgroundCssClass="modalbackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlModal" Style="display: block" runat="server" CssClass="modalpopup">
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::ข้อมูลการสอบ</div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: right; width: 25%;">
                                            <span>
                                                <asp:Label ID="lblDetailExamCode" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                            </span>&nbsp;
                                        </td>
                                        <td style="text-align: left; width: 27%;">
                                            <asp:TextBox ID="txtDetailExamCode" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 15%;">
                                            <span>
                                                <asp:Label ID="lblDetailDateExam" runat="server" CssClass="lbl" Text="วันที่สมัครสอบ"></asp:Label>
                                            </span>&nbsp;
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailDateExam" runat="server" Width="130px"></asp:TextBox>
                                            <asp:ImageButton ID="imgPopup_txtDetailDateExam" ImageUrl="~/Images/calendar.gif"
                                                ImageAlign="Bottom" runat="server" />
                                            <ajaxToolkit:CalendarExtender ID="CLDtxtDetailDateExam" PopupButtonID="imgPopup_txtDetailDateExam" runat="server" TargetControlID="txtDetailDateExam"
                                                Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="detailDateExamValidator" runat="server" ControlToValidate="txtDetailDateExam"
                                                CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblDetailTimeExamCode" runat="server" CssClass="lbl" Text="เวลาที่สอบ"></asp:Label>
                                            </span>&nbsp;
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlDetailTimeExamCode" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="detailTimeExamCodeValidator" runat="server" ControlToValidate="ddlDetailTimeExamCode"
                                                CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <span>
                                                <asp:Label ID="lblDetailFee" runat="server" CssClass="lbl" Text="ค่าธรรมเนียม (บาท)"></asp:Label>
                                            </span>&nbsp;
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailFee" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblDetailYardGroupCode" runat="server" CssClass="lbl" Text="หน่วยงานจัดสอบ"></asp:Label>
                                            </span>
                                        </td>
                                        <td style="text-align: left;" colspan="3">
                                            <asp:DropDownList ID="ddlDetailYardGroupCode" runat="server" AutoPostBack="True"
                                                CssClass="ddl" Width="50%" OnSelectedIndexChanged="ddlDetailYardGroupCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblDetailYardCode" runat="server" CssClass="lbl" Text="สนามสอบ"></asp:Label>
                                            </span>&nbsp;
                                        </td>
                                        <td style="text-align: left;" colspan="3">
                                            <asp:DropDownList ID="ddlDetailExamYardCode" runat="server" CssClass="ddl" AutoPostBack="True"
                                                Width="50%" OnSelectedIndexChanged="ddlDetailExamYardCode_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="detailNumberOfSeatValidator" runat="server" ControlToValidate="txtDetailNumberOfSeat"
                                                CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="DetailExamYardCodeValidator" runat="server" ControlToValidate="ddlDetailExamYardCode"
                                                CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                                            &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblDetailOfficerCode" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาตที่สอบ"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left;" colspan="3">
                                            <asp:DropDownList ID="ddlDetailOfficerCode" runat="server" CssClass="ddl" Width="50%">
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txtDetailOfficerCode" runat="server" CssClass="txtreadonly"></asp:TextBox>--%>
                                            <asp:RequiredFieldValidator ID="detailOfficerCodeValidator" runat="server" ControlToValidate="ddlDetailOfficerCode"
                                                CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                                            &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="Label15" runat="server" CssClass="lbl" Text="บังคับสอบ"></asp:Label>
                                            </span>&nbsp;
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:CheckBox ID="cbStatus" runat="server" Checked="false" Enabled="false" />
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            &nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td style="text-align: right;">
                                            <span>
                                                <asp:Label ID="lblDetailNumberOfSeat" runat="server" CssClass="lbl" Text="จำนวนรับสมัคร (คน)"></asp:Label>
                                            </span>&nbsp;
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtDetailNumberOfSeat" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            &nbsp;
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            &nbsp;
                                            <asp:Button ID="btnDetailSubmit" runat="server" ValidationGroup="exSchValidationGroup"
                                                CssClass="btn" Text="ตกลง" OnClick="btnDetailSubmit_Click" />
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn" Text="ลบ" OnClick="btnDelete_Click"
                                                OnClientClick="return confirmDelete();" />
                                            <asp:Button ID="btnCancelPop" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancel_Click" />
                                            <asp:ValidationSummary ID="exSchValidationSummary" runat="server" CssClass="failureNotification"
                                                HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="true" ShowSummary="False"
                                                ValidationGroup="exSchValidationGroup" />
                                        </td>
                                    </tr>
                                </table>
                                <uc1:UCLoadingProgress ID="UCLoadingProgress2" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <uc2:UCModalError ID="UCModalError" runat="server" />
                <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

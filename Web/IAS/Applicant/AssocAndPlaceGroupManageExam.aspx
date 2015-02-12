<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="AssocAndPlaceGroupManageExam.aspx.cs" Inherits="IAS.Applicant.AssocManageExam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content" align="center">
        <div class="box_content_header">
            <div class="box_content_header_text">
                สร้างรอบสอบ
            </div>
        </div>
        <%--<asp:UpdatePanel ID="UplSearch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
        <asp:Panel ID="pnlSearch" runat="server">
            <div class="box_body_content">
                <div class="box_t_h" align="left">
                    ::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหารหัสสอบ" />
                </div>
                <div class="box_textbox_bgadd">
                    <div class="box_texbox_add_box">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblTypeLicense" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlTypeLicense" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <span>&nbsp;</span>
                                </td>
                                <td style="text-align: left;">
                                </td>
                                <td style="text-align: right;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left;">
                                </td>
                                <td rowspan="3">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" OnClick="btnSearch_Click" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnCancle" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancle_Click"
                                        Width="75px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblYardGroup" runat="server" CssClass="lbl" Text="หน่วยงานจัดสอบ"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlPlaceGroup" runat="server" CssClass="ddl" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlPlaceGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <span>
                                        <asp:Label ID="lblYard" runat="server" CssClass="lbl" Text="สถานที่สอบ"></asp:Label>
                                        :</span>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlPlace" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblMonth" runat="server" CssClass="lbl" Text="เดือน"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblYear" runat="server" CssClass="lbl" Text="ปี"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtYear" runat="server" Style="text-align: center" MaxLength="4"
                                        onkeypress="return runScript(event)" onpaste="return false;" AutoComplete="Off"
                                        ondrop="return false;" />
                                    <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" RefValues=""
                                        ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID="" TargetButtonUpID=""
                                        TargetControlID="txtYear" Width="120" />
                                </td>
                                <td style="text-align: right; height: 17px;">
                                    <asp:Label ID="lblTimeExam" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left; height: 17px;">
                                    <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblDisplay" runat="server" CssClass="lbl" Text="แสดงผล"></asp:Label>:
                                </td>
                                <td style="text-align: left;">
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
                <div class="box_t_h" align="left">
                    ::ข้อมูลการสอบ</div>
                <span style="float: right; padding-right: 30px;" align="absmiddle">
                    <asp:Button ID="btnInsertExamSchedule" runat="server" CssClass="btn" Text="เพิ่มรอบสอบ"
                        OnClick="btnInsertExamSchedule_Click" />
                </span>
                <div class="box_textbox_bg_table">
                    <div class="box_texbox_add_box">
                        <asp:Calendar ID="cldExam" runat="server" BackColor="White" BorderColor="#999999"
                            CssClass="table_Calendar" CellPadding="4" DayNameFormat="Full" Font-Names="Verdana"
                            Font-Size="10pt" ForeColor="Black" Height="382px" Width="100%" ShowGridLines="True"
                            OnDayRender="cldExam_DayRender" OnSelectionChanged="cldExam_SelectionChanged"
                            OnVisibleMonthChanged="cldExam_VisibleMonthChanged">
                            <DayHeaderStyle BackColor="#e4e4e4" ForeColor="#000000" BorderColor="#ffffff" Font-Bold="True"
                                Height="30px" HorizontalAlign="center" BorderWidth="1px" />
                            <DayStyle BorderColor="#ffffff" BorderWidth="1px" BackColor="#f6f6f6" ForeColor="#3575e7"
                                Height="50px" HorizontalAlign="Center" Font-Bold="True" />
                            <TitleStyle BorderColor="#ffffff" Height="30px" BorderWidth="1px" HorizontalAlign="Center"
                                BackColor="#3575e7" ForeColor="#e4e4e4" Font-Size="Larger" Font-Bold="true" />
                            <TodayDayStyle BackColor="#fbfba6" />
                            <NextPrevStyle ForeColor="#e4e4e4" Font-Size="Medium" />
                        </asp:Calendar>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlTable" runat="server" Visible="true">
            <div class="box_body_content">
                <div class="box_textbox_bg_table" id="Div1" runat="server">
                    <div class="box_t_h" align="left">
                        ::ข้อมูลการสอบ</div>
                    <span style="float: right; padding-right: 30px;">
                        <table>
                            <tr>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Button ID="btnInsertExamSchedule2" runat="server" CssClass="btn" Text="เพิ่มรอบสอบ"
                                        OnClick="btnInsertExamSchedule_Click" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </span>
                    <div class="box_texbox_add_box">
                        <asp:GridView ID="gvExamSchedule" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True"
                            AutoGenerateColumns="False" CssClass="datatable" CellSpacing="-1" GridLines="None"
                            BorderWidth="1px" CaptionAlign="Top" CellPadding="3" EnableTheming="True" PagerSettings-FirstPageText="First"
                            PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;"
                            PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager"
                            PagerStyle-HorizontalAlign="Center" Width="100%" OnRowDataBound="gvExamSchedule_RowDataBound"
                            PageSize="20">
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                NextPageText="&gt;" PreviousPageText="&lt;" />
                            <SelectedRowStyle Font-Bold="True" BackColor="#008A8C" />
                            <PagerStyle HorizontalAlign="Center" />
                            <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#FFFFFF" ForeColor="#000000" />
                            <EmptyDataTemplate>
                                <div style="text-align: center; background-color: White">
                                    ไม่พบข้อมูล...</div>
                            </EmptyDataTemplate>
                            <HeaderStyle Font-Bold="True" />
                            <AlternatingRowStyle CssClass="altrow" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับที่">
                                    <ItemTemplate>
                                        <asp:Label ID="ExamRUNNO" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="td-center" Width="5%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="8%" CssClass="td-center" />
                                    <ItemTemplate>
                                        <asp:Label ID="ExamNumberNo" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                            Visible="false" OnClick="lnkExamNumber_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="8%" CssClass="td-center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="8%" CssClass="td-center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center"
                                    Visible="true">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlaceName" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัส" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="10%" CssClass="td-center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProvice" runat="server" Text='<%# Bind("PROVINCE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                    Visible="true">
                                    <ItemStyle Width="8%" CssClass="td-center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExamAdmission" runat="server" Text='<%# Bind("SEAT_AMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="10%" CssClass="td-center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseTypeName" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เลขที่ประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle Width="10%" CssClass="td-center" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseTypeCodeNumber" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="5%" CssClass="td-center" />
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
                                <asp:TemplateField HeaderText="ประเภทสมาชิก" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblAgentType" runat="server" Text='<%# Bind("AGENT_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle CssClass="td-left" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkExamRegister" runat="server" Text='สมัครสอบ' OnClick="lnkExamNumber_Click"
                                            Visible="false"></asp:LinkButton>
                                        <asp:Label ID="lblExamRegister" runat="server" Text='สมัครสอบ' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lnkExamManageTest" runat="server" Text='แก้ไข' OnClick="lnkExamManageTest_Click"
                                            Visible="false"></asp:LinkButton>
                                        <asp:Label ID="lblExamManageTest" runat="server" Text='แก้ไข' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสเวลา" Visible="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTestTimeCode" runat="server" Text='<%# Bind("TEST_TIME_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสจังหวัด" Visible="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProvinceCode" runat="server" Text='<%# Bind("PROVINCE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExamPlaceGroupCode" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สมาคมเจ้าของรอบสอบ" ItemStyle-HorizontalAlign="Center"
                                    Visible="true">
                                    <ItemStyle Width="8%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExamOwnerName" runat="server" Text='<%# Bind("EXAM_OWNER_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสมาคมเจ้าของรอบสอบ" Visible="false">
                                    <ItemStyle Width="0%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblExamOwner" runat="server" Text='<%# Bind("EXAM_OWNER") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <table width="100%" runat="server" id="table_Paging" visible="false">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                        <asp:TextBox ID="txtCurrentPage" runat="server" Width="15px" Visible="true" Enabled="false"
                            Text="1"></asp:TextBox>
                        <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                        <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                        <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;"
                            Visible="true" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                        <asp:TextBox ID="txtPageSize" runat="server" Visible="false" Style="text-align: center"
                            MaxLength="4" onkeypress="return runScript(event)" onpaste="return false;" AutoComplete="Off"
                            ondrop="return false;" Width="5%"></asp:TextBox>&nbsp;
                        <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearch_Click" />
                    </td>
                    <td align="right">
                        <asp:Label ID="lblTotalItems" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--      </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <div style="clear: left;">
    </div>
    <div style="display: none">
        <asp:Button ID="btnExamSchedule" runat="server" Text="Open" />
    </div>
    <ajaxToolkit:ModalPopupExtender ID="ModalExamSchedule" runat="server" CancelControlID="btnExamScheduleCancel"
        OkControlID="btnScheduleOkay" TargetControlID="btnExamSchedule" PopupControlID="pnlModalExamSchedule"
        PopupDragHandleControlID="PopupHeader" BackgroundCssClass="modalbackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlModalExamSchedule" Style="display: none;" Width="80%" runat="server"
        CssClass="modalpopup" TabIndex="100">
        <div class="box_body_content">
            <div class="box_t_h">
                ::จัดการกำหนดการสอบ</div>
            <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="รหัสรอบสอบ"></asp:Label>
                                </span>&nbsp;
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtScheduleExamCode" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnChangeRoom" runat="server" CssClass="btn" OnClick="btnChangeRoom_Click"
                                    OnClientClick="return confirmChange();" Text="เลื่อนรอบสอบนี้" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn" OnClick="btnDelete_Click"
                                    OnClientClick="return confirmDelete();" Text="ลบรอบสอบนี้" />
                            </td>
                            <td style="text-align: right;">
                                <asp:Label ID="lblS" runat="server" CssClass="lbl" Text="รอบสอบพิเศษ"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList runat="server" ID="ddlSpecial" AutoPostBack="true" CssClass="ddl"
                                    Width="111px" OnSelectedIndexChanged="ddlSpecial_SelectedIndexChanged">
                                    <asp:ListItem Value="" Text="ไม่" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="Y" Text="ใช่"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="วันที่สอบ"></asp:Label>
                                </span>&nbsp;
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtScheduleDetailDateExam" runat="server" CssClass="txt" AutoPostBack="true"
                                    Width="130px" OnTextChanged="txtScheduleDetailDateExam_TextChanged"></asp:TextBox>
                                <asp:ImageButton ID="imgPopup_txtScheduleDetailDateExam" ImageUrl="~/Images/calendar.gif"
                                    ImageAlign="Bottom" runat="server" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup_txtScheduleDetailDateExam"  runat="server" TargetControlID="txtScheduleDetailDateExam"
                                    Format="dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                                <asp:RequiredFieldValidator ID="detailDateExamValidator" runat="server" ControlToValidate="txtScheduleDetailDateExam"
                                    CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="lblDetailTimeExamCode" runat="server" CssClass="lbl" Text="เวลาที่สอบ"></asp:Label>
                                </span>
                            </td>
                            <td style="text-align: left;">
                                <asp:DropDownList ID="ddlDetailTimeExamCode" runat="server" CssClass="ddl" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDetailTimeExamCode_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="detailTimeExamCodeValidator" runat="server" ControlToValidate="ddlDetailTimeExamCode"
                                    CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="สังกัด"></asp:Label>
                                </span>
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <asp:DropDownList ID="ddlGroupType" runat="server" AutoPostBack="True" CssClass="ddl"
                                    Width="50%" OnSelectedIndexChanged="ddlGroupType_SelectedIndexChanged">
                                    <asp:ListItem Text="เลือก" Selected="True" Value=""></asp:ListItem>
                                    <asp:ListItem Text="หน่วยงานจัดสอบ" Value="G"></asp:ListItem>
                                    <asp:ListItem Text="สมาคม" Value="A"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGroupType"
                                    CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="lblDetailYardGroupCode" runat="server" CssClass="lbl" Text="หน่วยงานจัดสอบ/สมาคม"></asp:Label>
                                </span>
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <asp:DropDownList ID="ddlExamGroupCode" runat="server" AutoPostBack="True" CssClass="ddl"
                                    Width="50%" OnSelectedIndexChanged="ddlExamGroupCode_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqddlDetailYardGroupCode" runat="server" ControlToValidate="ddlExamGroupCode"
                                    CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="lblProvince" runat="server" CssClass="lbl" Text="จังหวัดที่จัดสอบ"></asp:Label>
                                </span>
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <asp:DropDownList ID="ddlProvince" runat="server" AutoPostBack="True" CssClass="ddl"
                                    Width="35%" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProvince"
                                    CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="lblDetailYardCode" runat="server" CssClass="lbl" Text="สนามสอบ"></asp:Label>
                                </span>&nbsp;
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <asp:DropDownList ID="ddlPlace_forRoom" runat="server" CssClass="ddl" AutoPostBack="True"
                                    Width="50%" OnSelectedIndexChanged="ddlPlace_forRoom_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="DetailExamYardCodeValidator" runat="server" ControlToValidate="ddlPlace_forRoom"
                                    CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                                &nbsp; &nbsp;<asp:Label ID="txtFree" runat="server" Visible="false" Text="เครื่องหมาย * หมายถึงสนามสอบรวมในจังหวัดที่เลือก"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="สมาคมเจ้าของรอบสอบ"></asp:Label>
                                &nbsp;
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <asp:DropDownList ID="ddlExamOnwer" runat="server" CssClass="ddl" Width="50%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlExamOnwer_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlExamOnwer"
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
                                <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl" Width="50%" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlLicenseType_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlLicenseType"
                                    CssClass="failureNotification" ValidationGroup="exSchValidationGroup">*</asp:RequiredFieldValidator>
                                &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="lblDetailNumberOfSeat" runat="server" CssClass="lbl" Text="จำนวนรับสมัคร (คน)"></asp:Label>
                                </span>&nbsp;
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtExamUseSeatRoom" runat="server" Enabled="false" onpaste="return false;"
                                    ondrop="return false;" AutoComplete="Off" Width="122px"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                <asp:TextBox ID="txtCourseNum" runat="server" Visible="false"></asp:TextBox>
                            </td>
                            <td style="text-align: left;">
                                &nbsp;
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <span>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="lbl" Text="เหตุผล"></asp:Label>
                                </span>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtRemark" runat="server" Columns="10" CssClass="txt" Height="50px"
                                    MaxLength="4000" TextMode="MultiLine" Width="200px" Enabled="False"></asp:TextBox>
                            </td>
                            <td style="text-align: right;">
                                &nbsp;
                            </td>
                            <td style="text-align: left;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <!-- START:2.1.5 -->
                        <tr>
                            <td colspan="4" align="center">
                                <table width="80%">
                                    <tr>
                                        <td align="left">
                                            <style type="text/css">
                                                fieldset.gvExamroom div
                                                {
                                                    height: auto;
                                                    overflow: auto;
                                                }
                                            </style>
                                            <fieldset class="gvExamroom" id="FS_Room" runat="server">
                                                <legend>กำหนดห้องสอบ</legend>
                                                <center>
                                                    <asp:DropDownList ID="ddlExamRoom" runat="server" CssClass="ddl" Width="381px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlLicenseType"
                                                        CssClass="failureNotification" ValidationGroup="AddGROUP">*</asp:RequiredFieldValidator>
                                                    &nbsp; &nbsp;&nbsp;
                                                    <asp:Button ID="btnAddExamRoom" runat="server" Text="เพิ่มห้องสอบ" ValidationGroup="AddGROUP"
                                                        OnClick="btnAddExamRoom_Click" Enabled="false" /></center>
                                                <br />
                                                <br />
                                                <asp:GridView ID="gvExamRoom" runat="server" AutoGenerateColumns="false" CssClass="datatable"
                                                    GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                ลำดับ</HeaderTemplate>
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                ชื่อห้องสอบ</HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExamRoomCode" runat="server" Text='<%# Bind("EXAM_ROOM_CODE") %>'
                                                                    Visible="false"></asp:Label>
                                                                <asp:Label ID="lblExamRoomName" runat="server" Text='<%# Bind("ROOM_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                จำนวนรับสมัคร / จำนวนที่นั่งทั้งหมด</HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExamNumberSeatRoom" runat="server" Text='<%# Bind("NUMBER_SEAT_ROOM") %>'
                                                                    MaxLength="6" ondrop="return false;" AutoComplete="Off" onpaste="return false;"
                                                                    onkeypress="return runScript(event)"></asp:TextBox>
                                                                /
                                                                <asp:Label ID="lblSeatAmount" runat="server" Text='<%# Bind("SEAT_AMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                ยกเลิกห้องนี้</HeaderTemplate>
                                                            <ItemTemplate>
                                                                <center>
                                                                    <asp:LinkButton ID="lbtnRemoveExamRoom" runat="server" OnClick="lbtnRemoveExamRoom_Click"><img src="../Images/x-icon.png" title="ลบ" /></asp:LinkButton>
                                                                </center>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <br />
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <!-- END:2.1.5 -->
                        <tr>
                            <td style="text-align: center;" colspan="4">
                                &nbsp;
                                <asp:Button ID="btnExamScheduleSubmit" runat="server" ValidationGroup="exSchValidationGroup"
                                    CssClass="btn" Text="ตกลง" OnClick="btnExamScheduleSubmit_Click" />
                                <asp:Button ID="btnExamScheduleCancel" runat="server" CssClass="btn" Text="ยกเลิก"
                                    OnClick="btnExamScheduleCancel_Click" />
                                <asp:ValidationSummary ID="exSchValidationSummary" runat="server" CssClass="failureNotification"
                                    HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="true" ShowSummary="False"
                                    ValidationGroup="exSchValidationGroup" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>

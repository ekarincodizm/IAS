<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ApplicantCompany.aspx.cs" Inherits="IAS.Applicant.ApplicantCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content" align="center">
        <div class="box_content_header">
            <div class="box_content_header_text">
                นำเข้าข้อมูลสมัครสอบ
            </div>
        </div>

            <asp:UpdatePanel ID="UplSearch" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

        <asp:Panel ID="pnlExamSearch" runat="server">
            <div class="box_body_content">
                <div class="box_t_h">
                    ::<asp:Label Text="รหัสสอบ" BorderStyle="None" ID="Label4" CssClass="lbl" runat="server" /></div>
                <div class="box_textbox_bgadd">
                    <div class="box_texbox_add_box">
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right; width: 35%;">
                                    <span>
                                        <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                        :</span>
                                </td>
                                <td style="text-align: left; width: 20%;">
                                    <asp:TextBox ID="txtExamNumber" runat="server" CssClass="txt"></asp:TextBox>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Button ID="btnSearchExamCode" runat="server" CssClass="btn" Text="ค้นหารหัสสอบ"
                                        Width="150px" OnClick="btnSearchExamCode_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancleSearch" runat="server" CssClass="btn" Text="ยกเลิก" Width="150px"
                                        OnClick="btnCancleSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:Panel>
    
                <asp:Panel ID="panContent" runat="server">
                    <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                        <div class="box_body_content">
                            <div class="box_t_h">
                                ::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหารหัสสอบ" /></div>
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
                                                <%--<asp:DropDownList ID="ddlAgentType" runat="server" CssClass="ddl" 
                                                        onselectedindexchanged="ddlAgentType_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>--%>
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
                            <div class="box_t_h">
                                ::ข้อมูลการสอบ</div>
                            <span style="float: right; padding-right: 30px;" align="absmiddle">
                                <asp:Button ID="btnInsertExamSchedule" runat="server" CssClass="btn" Text="เพิ่มรอบสอบ"
                                    OnClick="btnInsertExamSchedule_Click" Visible="false" />
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
                                <div class="box_t_h">
                                    ::ข้อมูลการสอบ</div>
                                <span style="float: right; padding-right: 30px;">
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <asp:ImageButton ID="btnExportExcel" Visible="true" runat="server" ImageUrl="~/image/exel.png"
                                                    OnClick="btnExportExcel_Click" />
                                            </td>
                                            <td align="center">
                                                <%-- asp:Button ID="btn_addtable" runat="server" vertical-align="middle" CssClass="btn"
                                                Text="เพิ่ม" OnClick="btnInsertExamSchedule_Click" Visible="false" /--%>
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
                                                    <asp:Label ID="ExamRUNNO" runat="server" Text='<%# Bind("RunNo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="td-center" Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="ExamNumberNo" runat="server" Text='<%# Bind("TestingNo") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TestingDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TestingTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <ItemStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("ExamPlaceGroup") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProvice" runat="server" Text='<%# Bind("Province") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlaceName" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamAdmission" runat="server" Text='<%# Bind("SeatAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLicenseTypeName" runat="server" Text='<%# Bind("LicenseType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamFee" runat="server" Text='<%# Bind("ExamFee") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ประเภทสมาชิก" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAgentType" runat="server" Text='<%# Bind("AgentType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle CssClass="td-left" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkExamRegister" runat="server" Text='สมัครสอบ' OnClick="lnkExamNumber_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สมาคมเจ้าของรอบสอบ" ItemStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <ItemStyle Width="8%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamOwnerName" runat="server" Text='<%# Bind("ExamOwner") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัส" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสเวลา" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTestTimeCode" runat="server" Text='<%# Bind("TEST_TIME_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                                Visible="false">
                                                <ItemStyle />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสจังหวัด" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProvinceCode" runat="server" Text='<%# Bind("PROVINCE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสสนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemStyle Width="8%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamPlaceGroupCode" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_CODE") %>'></asp:Label>
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
                        <table width="100%" runat="server" id="table_Paging">
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
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

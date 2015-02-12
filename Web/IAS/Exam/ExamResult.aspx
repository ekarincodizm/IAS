<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ExamResult.aspx.cs" Inherits="IAS.Exam.ExamResult" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $('#txtExpireExamDate').ready(function () {
            $('#txtExpireExamDate').attr('readOnly', 'true');
        });
        function confirmImport() {
            return confirm('คุณต้องการนำเข้าระบบหรือไม่?');
        }
        function confirmCancle() {
            return confirm('คุณต้องการยกเลิกการนำเข้าข้อมูลชุดนี้หรือไม่?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .style3
        {
            width: 26%;
        }
        .datepicker
        {
        }
        .style4
        {
            width: 27%;
        }
        .style5
        {
            width: 29%;
        }
        .style6
        {
            width: 24%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLoadFile" />
        </Triggers>
        <ContentTemplate>
            <div class="box_content">
                <div class="box_content_header">
                    <div class="box_content_header_text">
                        <asp:Label Text="ผลการสอบ" BorderStyle="None" ID="lblResult" runat="server" />
                    </div>
                </div>
                <asp:Panel ID="pnlSearch" runat="server">
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
                                                            </td>
                                                            <td style="text-align: left;">
                                                            </td>
                                                            <td rowspan="3">
                                                                <asp:Button ID="btnSearchExam" runat="server" CssClass="btn" Text="ค้นหา" OnClick="btnSearchExam_Click" />
                                                                <br />
                                                                <br />
                                                                <asp:Button ID="btnCancleExam" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancleExam_Click"
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
                </asp:Panel>
                <asp:Panel ID="pnlTable" runat="server" Visible="false">
                    <div class="box_body_content">
                        <div class="box_textbox_bg_table" id="Div1" runat="server">
                            <div class="box_t_h">
                                ::ข้อมูลการสอบ</div>
                            <div style="height:30px">  &nbsp;             
                            </div>  
                            <div class="box_texbox_add_box">
                                <asp:GridView ID="gvExamSchedule" runat="server" AllowPaging="false" AllowSorting="True"
                                    ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="datatable" CellSpacing="-1"
                                    GridLines="None" BorderWidth="1" CaptionAlign="Top" CellPadding="3" EnableTheming="True"
                                    PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                    PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;"
                                    PagerStyle-CssClass="viewpager" PagerStyle-HorizontalAlign="Center" Width="100%"
                                    OnRowDataBound="gvExamSchedule_RowDataBound" OnPageIndexChanging="gvExamSchedule_PageIndexChanging"
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
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="ExamNumberNo" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                <asp:LinkButton ID="lnkExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                                    Visible="false" OnClick="lnkExamNumber_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" CssClass="td-center" />
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
                                            <ItemStyle Width="10%" CssClass="td-center" />
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
                                        <asp:TemplateField HeaderText="ประเภทสมาชิก" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAgentType" runat="server" Text='<%# Bind("AGENT_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkExamRegister" runat="server" Text='นำเข้าผลการสอบ' OnClick="lnkExamNumber_Click"
                                                    Visible="false"></asp:LinkButton>
                                                <asp:Label ID="lblExamRegister" runat="server" Text='นำเข้าผลการสอบ' Visible="false"></asp:Label>
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
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamPlaceGroupCode" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รหัสสมาคมเจ้าของรอบสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamOwnerName" runat="server" Text='<%# Bind("EXAM_OWNER_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <br />
                            </div>
                        </div>
                    </div>
       
                    <table width="100%" runat="server" id="table_Paging" visible="false">
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
                                    MaxLength="4" onkeypress="return runScript(event)" onpaste="return false;" AutoComplete="Off"
                                    ondrop="return false;" Width="5%"></asp:TextBox>&nbsp;
                                <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearchExam_Click" />
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                        
                </asp:Panel> 
                <asp:Panel ID="FileUploadPanel" runat="server">
                    <div class="box_body_content">
                        <div class="box_textbox_bg_table" id="Div3" runat="server">
                            <div class="box_t_h">
                                ::นำเข้าผลการสอบของรหัสรอบสอบ <asp:Label ID="testingNoInput" runat="server" ></asp:Label> </div>  
                                  <div style="height:30px">  &nbsp;             
                                </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                <div class="box_texbox_add_box">
                                <fieldset>
                                    <div class="box_textbox_bgadd">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;" class="style3">
                                                    <asp:Label ID="lblFileName" runat="server" CssClass="lbl" Text="ไฟล์ข้อมูล :"></asp:Label>
                                                </td>
                                                <td style="text-align: left;" class="style5">
                                                    <asp:FileUpload ID="fUpload" runat="server" Width="374px" />
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    &nbsp;
                                                    <asp:Button ID="btnLoadFile" runat="server" CssClass="btn" OnClick="btnLoadFile_Click"
                                                        Text="โหลดข้อมูล" />
                                                </td>
                                                <td style="text-align: right;" class="style4">
                                                    <span>
                                                        <asp:Label ID="lblExpireExamDate" runat="server" CssClass="lbl" Visible="false" Text="วันหมดอายุผลสอบ :"></asp:Label>
                                                    </span>&nbsp;
                                                </td>
                                                <td style="text-align: left;" class="style6">
                                                    <asp:TextBox ID="txtExpireExamDate" runat="server" CssClass="datepicker" Visible="false"
                                                        Width="171px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                           
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelUpload">
                                                        <ProgressTemplate>
                                                            Please wait....</ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="PnlImport" runat="server" Visible="false">
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::ผลการนำเข้า</div>
                                                <div class="box_textbox_bg_table" runat="server" id="bludDiv1">
                                                    <div class="box_texbox_add_box">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="gvSubject" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                                                        CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True"
                                                                        AutoGenerateColumns="False" OnRowDataBound="gvSubject_RowDataBound">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <PagerStyle HorizontalAlign="Right" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="50%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("UploadFileName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("Totals") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="รายการที่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("RightTrans") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemsIncorrectGv" runat="server" Text='<%# Eval("MissingTrans") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="รหัสห้องสอบ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTestingNo" runat="server" Visible="false" Text='<%# Eval("TestingNo") %>'></asp:Label>
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
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::ตรวจสอบรายการ</div>
                                                <div class="box_textbox_bg_table" runat="server" id="bludDiv2">
                                                    <div class="box_texbox_add_box">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="text-align: center;" colspan="4">
                                                                    <asp:GridView ID="gvCheckList" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                                                        CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True"
                                                                        AutoGenerateColumns="False" OnRowDataBound="gvCheckList_RowDataBound">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <PagerStyle HorizontalAlign="Right" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNOOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Eval("ID_CARD_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("NAMES") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("LAST_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ผลสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblResultExamGv" runat="server" Text='<%# Eval("EXAM_RESULT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ขาดสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAbsentGv" runat="server" Text='<%# Eval("ABSENT_EXAM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="สังกัด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="20%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBelongToGv" runat="server" Text='<%# Eval("InsurCompName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="รายละเอียด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="5%" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="hplview" runat="server" OnClick="hplView_Click" Visible="false">แสดง</asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="30%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("ERROR_MSG") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SEQ_NO" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSeqNoGv" runat="server" Text='<%# Eval("SEQ_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="text-align: center;" colspan="4">
                                                                    <asp:Button ID="btnConfirm" runat="server" Text="นำเข้าระบบ" CssClass="btn" OnClientClick="return confirmImport();"
                                                                        OnClick="btnConfirm_Click" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="btnCancle" runat="server" Text="ยกเลิกข้อมูลชุดนี้" CssClass="btn"
                                                                        OnClientClick="return confirmCancle();"   OnClick="btnCancle_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>   
                                    </div>

                                </fieldset>
                                 <div style="height:20px">     &nbsp;          
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            
         <asp:UpdatePanel ID="udpUpload" runat="server" UpdateMode="Always">
                <ContentTemplate>  
                    <asp:HiddenField ID="hdfGroupID" runat="server" />
                        <div style="display: none">
                        <asp:Button ID="btnHide3" runat="server" Style="display: none" Text="Button" />
                        </div>
                    <center>
                        <ajaxToolkit:ModalPopupExtender ID="mpDetail" TargetControlID="btnHide3" CancelControlID="btnCancleDetail"
                            BackgroundCssClass="modalbackground" PopupControlID="PnlPopup" runat="server">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="PnlPopup" runat="server"  CssClass="modalpopup" TabIndex="100" Width="85%" Visible="true">
                            <div class="box_body_content" id="blueDiv" runat="server" visible="true">
                                <div class="box_textbox_bgadd">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblIdCardNumber" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 13%;">
                                                <asp:TextBox ID="txtIdCardNumber" runat="server" CssClass="txt" Width="130px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblTitleName" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 10%;">
                                                <asp:TextBox ID="txtTitleName" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <asp:Label ID="lblName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="txt" Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 15%;">
                                                <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 13%;">
                                                <asp:TextBox ID="txtSex" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblAreaCode" runat="server" Text="รหัสพื้นที่" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 10%;">
                                                <asp:TextBox ID="txtAreaCode" runat="server" CssClass="txt" Width="70px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%">
                                                <asp:Label ID="lblAddress1" runat="server" Text="ที่อยู่ 1" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 15%;">
                                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="txt" Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblAddress2" runat="server" Text="ที่อยู่ 2" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 15%;">
                                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="txt" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right; width: 25%;">
                                                <asp:Label ID="lblEducationCode" runat="server" Text="รหัสการศึกษา" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 10%;">
                                                <asp:TextBox ID="txtEducationCode" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <asp:Label ID="lblBirthDay" runat="server" Text="วันเกิด" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtBirthDay" runat="server" CssClass="txt" Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblApproveDate" runat="server" Text="วันที่อนุมัติ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 10%;">
                                                <asp:TextBox ID="txtApproveDate" runat="server" Width="100px" CssClass="txt"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <asp:Label ID="lblResultExam" runat="server" Text="ผลการสอบ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtResultExam" runat="server" Width="50px" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 25%;">
                                                <asp:Label ID="lblAssociationID" runat="server" Text="รหัสสมาคม" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtAssociationID" runat="server" Width="50px" CssClass="txt"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%">
                                                <asp:Label ID="lblAssociationName" runat="server" Text="ชื่อสมาคม" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtAssociationName" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblAffiliatedInsuranceID" runat="server" Text="รหัสสังกัดบริษัท" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtAffiliatedInsuranceID" Width="50px" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <asp:Label ID="lblProvinceID" runat="server" Text="รหัสจังหวัด" Width="100px" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtProvinceID" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 25%;">
                                                <asp:Label ID="lblTypePermitID" runat="server" Text="รหัสประเภทใบอนุญาต" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtTypePermitID" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <asp:Label ID="lblExamDate" runat="server" Text="วันสอบ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtExamDate" runat="server" CssClass="txt" Width="150px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 15%;">
                                                <asp:Label ID="lblTimeExamID" runat="server" Text="รหัสเวลาสอบ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtTimeExamID" runat="server" CssClass="txt" Width="50px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 10%;">
                                                <asp:Label ID="lblExamSitNumber" runat="server" Text="เลขที่นั่งสอบ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtExamSitNumber" Width="50px" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="8" align="center">
                                                <br />
                                                <asp:GridView ID="GVScorevieW" runat="server" AutoGenerateColumns="False" BorderWidth="1"
                                                    CellSpacing="-1" CssClass="datatable" EnableTheming="True" GridLines="None" ShowHeaderWhenEmpty="True"
                                                    Visible="true" Width="85%">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ลำดับที่" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle CssClass="td-center" Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOO" runat="server" Text='<%# Bind("ListNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รหัสวิชา" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle CssClass="td-center" Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubCode" runat="server" Text='<%# Bind("SubCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ชื่อวิชา" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="30%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubName" runat="server" Text='<%# Bind("SubName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="คะแนนเต็ม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle CssClass="td-center" Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFullScore" runat="server" Text='<%# Bind("FullScore") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="คะแนนที่ได้รับ" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle CssClass="td-center" Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                            </td>
                                            <tr>
                                                <td colspan="4" style="text-align: center;">
                                                    <asp:Button ID="btnCancleDetail" runat="server" CssClass="btn" Text="ยกเลิก"    />
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                           
                                </div>
                            </div>
                        </asp:Panel>
                    </center>
            </ContentTemplate>
        </asp:UpdatePanel>
    </ContentTemplate>
  </asp:UpdatePanel>         
</asp:Content>

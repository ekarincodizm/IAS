<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ApplicantGeneral.aspx.cs" Inherits="IAS.Applicant.ApplicantGeneral" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<script type="text/javascript">
    function OpenPopupSingle(para) {

        var qstr = "../Reporting/RptBillPayment.aspx?Invoice=" + para + "&Click=PrintS";  
        window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
}
</script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
     <div class="box_content" align="center">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
                    สมัครสอบ
                </div>
            </div>
                       
    
            <asp:UpdatePanel ID="update" runat="server">
                <ContentTemplate>
                

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
            <div align="right">
            <div style="margin-right:15px;">
            <asp:Button ID="btnList" runat="server" Text="รายการสมัครสอบที่เลือก" CssClass="btn" onclick="btnList_Click" />
            </div>
            </div>
            <asp:Panel ID="pnlTable" runat="server" Visible="true">
                <div class="box_body_content">
                    <div class="box_textbox_bg_table" id="Div1" runat="server">
                        <div class="box_t_h" align="left">
                            ::ข้อมูลการสอบ
                         </div>
                        
                        <div class="box_texbox_add_box">
                            
                            <asp:GridView ID="gvExamSchedule" runat="server" AllowSorting="True"
                                ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" 
                                CssClass="datatable" CellSpacing="-1"
                                GridLines="None" BorderWidth="1px" CaptionAlign="Top" CellPadding="3" EnableTheming="True"
                                PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;"
                                PagerStyle-CssClass="viewpager" PagerStyle-HorizontalAlign="Center" Width="100%"
                                OnRowDataBound="gvExamSchedule_RowDataBound"
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
                                            <asp:LinkButton ID="lnkExamNumber" runat="server" Text='<%# Bind("TestingNo") %>'
                                                Visible="false" OnClick="lnkExamNumber_Click"></asp:LinkButton>
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
                                    <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" 
                                        Visible="true">
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlaceName" runat="server" 
                                                Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
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

                                            <asp:TemplateField HeaderText="รหัสสนามสอบ" ItemStyle-HorizontalAlign="Center" Visible ="false">
                                            <ItemStyle Width="8%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamPlaceGroupCode" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 

                                </Columns>
                            </asp:GridView>
                            <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />
                            <uc1:UCModalError ID="UCModalError1" runat="server" />
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
            <ajaxToolkit:ModalPopupExtender ID="ModSingleApplicant" runat="server" CancelControlID="btnCancel"
                TargetControlID="btnModal" PopupControlID="pnlModal" BackgroundCssClass="modalbackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Button ID="btnModal" runat="server" Text="Open" Style="display: none" />
            <asp:Panel ID="pnlModal" Style="display: none" runat="server" CssClass="modalpopup"
                TabIndex="100">
                <div class="box_body_content">
                    <div class="box_t_h" align="left">
                        ::ข้อมูลการสอบ                          
                      </div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <asp:UpdatePanel ID="UplPopUp" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 25%;">
                                                <span>
                                                    <asp:Label ID="lblDetailExamCode" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                                </span>
                                            </td>
                                            <td style="text-align: left; width: 27%;">
                                                <asp:TextBox ID="txtDetailExamCode" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 22%;">
                                                <span>
                                                    <asp:Label ID="lblDetailDateExam" runat="server" CssClass="lbl" Text="วันที่สอบ"></asp:Label>
                                                </span>&nbsp;
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtDetailDateExam" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <span>
                                                    <asp:Label ID="lblDetailCompanyCode" runat="server" CssClass="lbl" Text="รหัสบริษัทประกันภัย"></asp:Label>
                                                </span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlCompanyCode" runat="server" CssClass="ddl" Width="250px">
                                                </asp:DropDownList>                                                
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <span>
                                                    <asp:Label ID="lblDetailPlaceCode" runat="server" CssClass="lbl" Text="สนามสอบ"></asp:Label>
                                                </span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtDetailPlaceCode" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblTestTime" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtTestTime" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblExamPlaceName" runat="server" CssClass="lbl" Text="หน่วยงานจัดสอบ"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtExamPlaceName" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblProvincePopup" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtProvincePopup" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblLicenseTypeName" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtLicenseTypeName" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblProvincePopup0" runat="server" CssClass="lbl" Text="รอบสอบพิเศษ"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:CheckBox ID="chkSpecial" runat="server" Enabled="false" />
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblProvincePopup1" runat="server" CssClass="lbl" Text="สมาคมเจ้าของรอบสอบ"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtExamOwner" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;" colspan="4">
                                                &nbsp;
                                                <asp:Button ID="btnDetailSubmit" runat="server" CssClass="btn" Text="ตกลง" OnClick="btnDetailSubmit_Click"
                                                    ValidationGroup="singleApplicantValidationGroup" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center;">
                                                <asp:ValidationSummary ID="singleApplicantValidationSummary" runat="server" CssClass="failureNotification"
                                                    HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="true" ShowSummary="False"
                                                    ValidationGroup="singleApplicantValidationGroup" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Button ID="btnListExam" runat="server" Text="Open" Style="display: none;" />
            <ajaxToolkit:ModalPopupExtender ID="ModalListExam" runat="server" CancelControlID="btnCancel"
                TargetControlID="btnListExam" PopupControlID="pnlListExam" BackgroundCssClass="modalbackground"
                OkControlID="btnOkay">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlListExam" Style="display: none" runat="server" CssClass="modalpopup">
                <div class="box_body_content">

                    <div class="box_t_h" align="left">
                         ::<asp:Label ID="Label2" runat="server" BorderStyle="None" Text="ข้อมูลการสอบ" />
                    </div>

                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <asp:UpdatePanel ID="UplListExam" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:GridView ID="gvListExam" runat="server" AllowPaging="false" AllowSorting="True"
                                                    AutoGenerateColumns="False" BorderWidth="1" CaptionAlign="Top" CellPadding="3"
                                                    CellSpacing="-1" CssClass="datatable" EnableTheming="True" GridLines="None" OnRowDataBound="gvListExam_RowDataBound"
                                                    PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;"
                                                    PagerStyle-CssClass="viewpager" PagerStyle-HorizontalAlign="Center" PageSize="20"
                                                    ShowHeaderWhenEmpty="True" Width="100%" OnRowCommand="gvListExam__RowCommand">
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                                    <PagerStyle HorizontalAlign="Center" />
                                                    <EmptyDataRowStyle BackColor="#FFFFFF" ForeColor="#000000" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; background-color: White">
                                                            ไม่พบข้อมูล...</div>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ลำดับที่">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRunnoTemp" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                                <asp:Label ID="lblRunnoBase" runat="server" Text='<%#Eval("ExamNumber") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="td-center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamNumberNo" runat="server" Text='<%#Eval("ExamNumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamDate" runat="server" Text='<%# Eval("ExamDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamTime" runat="server" Text='<%# Eval("ExamTime") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamYard" runat="server" Text='<%# Eval("ExamPlaceGroup") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamPlaceName" runat="server" Text='<%# Eval("ExamPlace") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamProvice" runat="server" Text='<%# Eval("Province") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamAdmission" runat="server" Text='<%# Eval("Seat") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamLicenseTypeCode" runat="server" Text='<%# Eval("LicenseTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamFee" runat="server" Text='<%# Eval("ExamFee") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทสมาชิก" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListExamAgentType" runat="server" Text='<%# Eval("AgentType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkListExamRegister" runat="server" Text='ยกเลิก' OnClick="lnkListExamRegister_Click"></asp:LinkButton>
                                                                <asp:Label ID="lblListExamRegister" runat="server" Text='สมัครสอบ' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBUP" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="Up"><img src="../Images/arrowup.png" /></asp:LinkButton>
                                                                <asp:LinkButton ID="LBDown" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="Down"><img src="../Images/arrowdown.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Button ID="btnListExamConfirm" runat="server" CssClass="btn" OnClick="btnListExamConfirm_Click"
                                                    Text="ยืนยันการสมัครสอบ" />
                                                <asp:Button ID="btnListExamContinue" runat="server" CssClass="btn" OnClick="btnListExamContinue_Click"
                                                    Text="กลับไปหน้าสมัครสอบ" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>   

            <div align="center" style="width: 500px">
                    <asp:Button ID="btnListExamNDel" runat="server" Visible="false" />
                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmModalPopupExtenderListExam" runat="server"
                        DisplayModalPopupID="ModalPopupExtenderListExam" TargetControlID="btnListExamNDel">
                    </ajaxToolkit:ConfirmButtonExtender>
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderListExam" runat="server" PopupControlID="pnlConfirmListExamPopup"
                        TargetControlID="btnListExamNDel" Drag="true" CancelControlID="btnConfirmExamListCancel"
                        PopupDragHandleControlID="pnlConfirmListExamPopup" BackgroundCssClass="modalbackgroundComfirm">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirmListExamPopup" runat="server" CssClass="popup_container_import_data_Confirm"
                        Wrap="true" Style="display: none;">
                        <div class="popup_title_confirm">
                            <asp:Label ID="Label8" runat="server" Text="แจ้งเตือน"></asp:Label>
                        </div>
                        <div style="padding: 15px;">
                            <table width="90%" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td align="center" width="20px" style="background: none; border: none;">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/info.png" />
                                      
                                    </td>
                                    <td align="center" style="background: none; border: none;">
                                        <%--สอบซ้ำ กดยืนยันเพื่อสอบ--%>
                                          <asp:Label ID="lblConfirmExam" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <div align="center">
                                <asp:Button ID="btnConfirmExamList" runat="server" CssClass="btn" Text="ยืนยัน" OnClick="btnConfirmExamList_Click" />
                                <asp:Button ID="btnCancelConfirmExamList" runat="server" CssClass="btn" 
                                    Text="ยกเลิก" onclick="btnCancelConfirmExamList_Click" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>

 
              </ContentTemplate>
            </asp:UpdatePanel>

          
        </div>
    <div style="clear:left;"></div>
</asp:Content>

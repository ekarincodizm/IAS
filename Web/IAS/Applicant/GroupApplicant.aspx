<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="GroupApplicant.aspx.cs" Inherits="IAS.Applicant.GroupApplicant" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="สมัครสอบแบบกลุ่ม" BorderStyle="None" ID="lblDetailExamGroup" runat="server" />
            </div>
        </div>
        <div>
            <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
            <asp:UpdatePanel ID="upn" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnl1" runat="server" DefaultButton="btnSearchExamCode">
                        <div class="box_body_content">
                            <div class="box_t_h">
                                ::
                                <asp:Label Text="รหัสสอบ" BorderStyle="None" ID="Label3" CssClass="lbl" runat="server" /></div>
                            <div class="box_textbox_bgadd">
                                <div class="box_texbox_add_box">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 35%;">
                                                <span>
                                                    <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                                    :</span>
                                            </td>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:TextBox ID="txtExamNumber" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Button ID="btnSearchExamCode" runat="server" CssClass="btn" Text="ค้นหารหัสสอบ"
                                                    Width="150px" OnClick="btnSearchExamCode_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlSearch" runat="server" Visible="false">
                        <div class="box_body_content">
                            <div class="box_t_h">
                                ::
                                <asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหารหัสสอบ" /></div>
                            <div class="box_textbox_bgadd">
                                <div class="box_texbox_add_box">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTypeLicense" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                                                :
                                            </td>
                                            <td colspan="3">
                                                <asp:DropDownList ID="ddlTypeLicense" runat="server" CssClass="ddl" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td rowspan="5">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                                    Text="ค้นหา" ValidationGroup="AssociationValidationGroup" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <span>
                                                    <asp:Label ID="lblYardGroup" runat="server" CssClass="lbl" Text="หน่วยงานจัดสอบ"></asp:Label>
                                                    :</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:DropDownList ID="ddlPlaceGroup" runat="server" CssClass="ddl" Width="150px"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPlaceGroup_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <span>
                                                    <asp:Label ID="lblYard" runat="server" CssClass="lbl" Text="สถานที่สอบ"></asp:Label>
                                                    :</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:DropDownList ID="ddlPlace" runat="server" CssClass="ddl" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                &nbsp;<asp:Label ID="lblMonth" runat="server" CssClass="lbl" Text="เดือน"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <span>
                                                    <asp:Label ID="lblYear" runat="server" CssClass="lbl" Text="ปี"></asp:Label>
                                                    : </span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtYear" runat="server" Style="text-align: center" Width="120" />
                                                <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" RefValues=""
                                                    ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID="" TargetButtonUpID=""
                                                    TargetControlID="txtYear" Width="120" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTimeExam" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblDisplay" runat="server" CssClass="lbl" Text="แสดงผล"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:RadioButtonList ID="rblDisplay" runat="server" AutoPostBack="True" CssClass="rb"
                                                    OnSelectedIndexChanged="rblDisplay_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="ปฏิทิน" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="ตาราง" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%; height: 17px;">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left; width: 30%; height: 17px;">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: right; width: 20%; height: 17px;">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left; width: 30%; height: 17px;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlCalendar" runat="server" Visible="false">
                        <div class="box_body_content">
                            <div class="box_t_h">
                            </div>
                            <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">
                                    <div style="width: 100%; margin: 0px auto 10px auto;">
                                        <asp:Calendar ID="cldGroupApplicant" runat="server" BackColor="White" BorderColor="#999999"
                                            CellPadding="4" DayNameFormat="Full" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"
                                            Height="382px" Width="100%" ShowGridLines="True" Font-Bold="False" OnDayRender="cldGroupApplicant_DayRender"
                                            OnSelectionChanged="cldGroupApplicant_SelectionChanged">
                                            <DayHeaderStyle BackColor="#3399FF" BorderColor="Black" Font-Bold="True" Height="50px"
                                                HorizontalAlign="Left" />
                                            <DayStyle BorderColor="Black" Height="50px" HorizontalAlign="Center" Font-Bold="True" />
                                            <TitleStyle BorderColor="Black" Height="15px" HorizontalAlign="Center" BackColor="#3399FF" />
                                            <TodayDayStyle BackColor="#FFFF99" />
                                        </asp:Calendar>
                                        <br />
                                        <br />
                                        <asp:GridView ID="gvGroupApplicant" runat="server" AllowPaging="false" AllowSorting="True"
                                            AutoGenerateColumns="False" CssClass="datatable" CellSpacing="-1" GridLines="None"
                                            BorderWidth="1" CaptionAlign="Top" EnableTheming="True" PagerSettings-FirstPageText="First"
                                            PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;"
                                            PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager"
                                            PagerStyle-HorizontalAlign="Center" Width="100%" OnRowDataBound="gvGroupApplicant_RowDataBound" ShowHeaderWhenEmpty="True" 
                                            EmptyDataText="ไม่มีข้อมูล..." > 
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="&gt;" PreviousPageText="&lt;" />
                                            <RowStyle CssClass="view" />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                            <PagerStyle HorizontalAlign="Center" />
                                            <HeaderStyle Font-Bold="True" />
                                            <AlternatingRowStyle CssClass="altrow" />
                                            <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                                            OnClick="lnkExamNumber_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TESTING_DATE","{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อห้องสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProvice" runat="server" Text='<%# Bind("PROVINCE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                    <ItemStyle Width="11%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExamAdmission" runat="server" Text='<%# Bind("EXAM_ADMISSION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="11%" />
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
                                    </div>
                                </div>
                            </div>                             
                        </div>
                         <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false" Text="1"></asp:TextBox>
                                                <asp:Label ID="lblParaPage" runat="server" Text="/" Visible = "false"></asp:Label>
                                                <asp:Label ID="txtTotalPage" runat="server" Visible = "false"></asp:Label>
                                                <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;" Visible="true" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" style="text-align:center" MaxLength="2" Width="5%" onkeypress="return runScript(event)"></asp:TextBox>&nbsp;
                                                <asp:Button ID="pageGo" runat="server" Visible="false" text="GO" 
                                                    onclick="pageGo_Click"/>
                                            </td>
                                            <td align="right">
                                                 <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                   </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlTable" runat="server" Visible="false">
                        <div class="box_body_content">
                            <div class="box_t_h">
                            </div>
                            <div class="box_textbox_bg_table" id="boxgvTable" runat="server">
                                <div class="box_texbox_add_box">
                                    <asp:GridView ID="gvTable" runat="server" AllowPaging="false" AllowSorting="True"
                                        AutoGenerateColumns="False" CssClass="datatable" CellSpacing="-1" GridLines="None"
                                        BorderWidth="1" CaptionAlign="Top" EnableTheming="True" PagerSettings-FirstPageText="First"
                                        PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;"
                                        PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager"
                                        PagerStyle-HorizontalAlign="Center" Width="100%" EmptyDataText="ไม่มีข้อมูล..." ShowHeaderWhenEmpty="True" >
                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                        <SelectedRowStyle Font-Bold="True" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <HeaderStyle Font-Bold="True" />
                                        <AlternatingRowStyle CssClass="altrow" />
                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="12%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkTableExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                                        OnClick="lnkTableExamNumber_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="12%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TESTING_DATE","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="12%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="12%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อห้องสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="12%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="12%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProvince" runat="server" Text='<%# Bind("PROVINCE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                <ItemStyle Width="12%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLicenseType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสสถานที่สอบ" ItemStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <ItemStyle Width="12%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                          <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnP_table" runat="server" Text="<" Visible="true" OnClick="btn_tablePreviousGvSearch_Click" />
                                                <asp:TextBox ID="txt_page_now" runat="server" Width="15px" Visible="true" Enabled="false" Text="1"></asp:TextBox>
                                                <asp:Label ID="lblPer" runat="server" Text="/" Visible = "false"></asp:Label>
                                                <asp:Label ID="lbl_pageMax" runat="server" Visible = "false"></asp:Label>
                                                <asp:Button ID="btnN_table" runat="server" OnClick="btn_tableNextGvSearch_Click" Text="&gt;" Visible="true" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbl" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                <asp:TextBox ID="txt_input" runat="server" Visible="false" style="text-align:center" MaxLength="2" Width="5%" onkeypress="return runScript(event)"></asp:TextBox>&nbsp;
                                                <asp:Button ID="btn_DETAIL_GOOOOOO" runat="server" Visible="false" text="GO" onclick="btnSearch_Click"/>
                                            </td>
                                            <td align="right">
                                                 <asp:Label ID="lblDetail" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                   </table>
                    </asp:Panel>
                    <uc2:UCModalError ID="UCModalError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div style="clear: left">
    </div>
</asp:Content>

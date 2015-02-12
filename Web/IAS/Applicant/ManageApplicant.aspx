<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ManageApplicant.aspx.cs" Inherits="IAS.Applicant.ManageApplicant" %>

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
        $(function setdate() {
            $('#txtStartPaidSubDate').attr('readOnly', 'true');
            $('#txtEndPaidSubDate').attr('readOnly', 'true');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="การจัดห้องสอบ" BorderStyle="None" ID="lblManageApplicant" runat="server" />
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
                                                    <asp:Label ID="Label1" runat="server" Text="รหัสรอบสอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtTestNo" runat="server" onkeypress="return runScript(event)" MaxLength="6"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="Label4" runat="server" Text="ประเภทใบอนุญาต" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlLicenseType" runat="server" Width="250px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlLicenseType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStartExamDate" runat="server" Text="วันที่สอบ(เริ่ม)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartExamDate" runat="server" onkeydown="return false;"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartExamDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEStartExamDate" PopupButtonID="imgPopup_txtStartExamDate" runat="server" TargetControlID="txtStartExamDate"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndExamDate" runat="server" Text="วันที่สอบ(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndExamDate" runat="server" onkeydown="return false;"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndExamDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEEndExamDate" PopupButtonID="imgPopup_txtEndExamDate" runat="server" TargetControlID="txtEndExamDate"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPlaceGroup" runat="server" Text="สมาคมเจ้าของรอบสอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlAsso" runat="server" Width="250px" OnSelectedIndexChanged="ddlAsso_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblPlaceName" runat="server" Text="สนามสอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlPlaceName" runat="server" Width="250px">
                                                    </asp:DropDownList>
                                                </td>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblTestingNo" runat="server" CssClass="lbl" Text="เวลาที่สอบ"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl" Width="250px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="ดำเนินการ"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlEvent" runat="server" CssClass="ddl" Width="250px">
                                                            <asp:ListItem Selected="True" Text="จัดคนเข้าห้องสอบ" Value="in"></asp:ListItem>
                                                            <asp:ListItem Text="นำคนออกจากห้องสอบ" Value="out"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="4">
                                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                                            Text="ค้นหา" Width="75px" />
                                                        &nbsp; &nbsp; &nbsp;
                                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnMainCancle_Click"
                                                            Text="ยกเลิก" Width="75px" />
                                                    </td>
                                                </tr>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="PnlDetailSearchGridView" runat="server">
                            <div class="box_body_content">
                                <div class="box_textbox_bg_table" runat="server" id="boxResult" visible="false">
                                    <div class="box_texbox_add_box">
                                        <div id="divGv1" runat="server" visible="false">
                                            <fieldset style="width: 98%; height: auto">
                                                <legend>ข้อมูลรอบสอบ</legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <asp:GridView ID="gvSearch" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
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
                                                                    <asp:TemplateField HeaderText="รหัสรอบสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="8%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTestingNo" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="15%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDatePayGv" runat="server" Text='<%# Bind("LICENSE_TYPE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="8%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("EXAM_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="สมาคมจัดสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="20%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlaceG" runat="server" Text='<%# Bind("PLACE_GROUP") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="สนามสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="15%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlace" runat="server" Text='<%# Bind("PLACE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="เวลาที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="8%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTime" runat="server" Text='<%# Bind("EXAM_TIME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="8%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>&nbsp;&nbsp;
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
                                            </fieldset>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <div id="divGv2" runat="server" visible="false">
                                            <fieldset style="width: 98%; height: auto">
                                                <legend>ข้อมูลผู้สมัครสอบ [รอบสอบ :
                                                    <asp:Label ID="selectRoom" runat="server"></asp:Label>]</legend>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <div style="float: right; padding-right: 2px;">
                                                            </div>
                                                            <asp:GridView ID="gvSearch2" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                                GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                OnRowDataBound="gvSearch2_RowDataBound" ShowHeaderWhenEmpty="True" AllowSorting="True"
                                                                Visible="false">
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
                                                                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="td-center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="4%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:TemplateField HeaderText="ลำดับที่นั่งสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAPPCODE" runat="server" Text='<%# Bind("APP_CODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="หมายเลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("IDNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ชื่อ - นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="14%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblfullname" runat="server" Text='<%# Bind("NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="8%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltestingno" runat="server" Text='<%# Bind("TESTNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="11%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllicensetype" runat="server" Text='<%# Bind("LicenseType") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="8%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltestingdate" runat="server" Text='<% #Bind("DDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlaceGroup" runat="server" Text='<%# Bind("OWNER") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="สนามสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="10%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlace" runat="server" Text='<%# Bind("PLACE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="เวลาที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="8%" CssClass="td-center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbltestingtime" runat="server" Text='<%# Bind("TIMER") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ห้องสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="30%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblplaceroom" runat="server" Text='<%# Bind("RoomName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="applicantCode" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemStyle Width="30%" CssClass="td-left" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblappcode" runat="server" Text='<%# Bind("APP_CODE") %>'></asp:Label>
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
                                                                        <asp:Button ID="btnPreviousGvSearch2" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch2_Click" />
                                                                        <asp:TextBox ID="txtNumberGvSearch2" runat="server" Width="15px" Visible="true" Enabled="false"
                                                                            Text="1"></asp:TextBox>
                                                                        <asp:Label ID="lblParaPage2" runat="server" Text="/" Visible="false"></asp:Label>
                                                                        <asp:Label ID="txtTotalPage2" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Button ID="btnNextGvSearch2" runat="server" OnClick="btnNextGvSearch2_Click"
                                                                            Text="&gt;" Visible="true" />
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:Label ID="TXTrowperpage2" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                                        <asp:TextBox ID="rowPerpage2" runat="server" Visible="false" Style="text-align: center"
                                                                            MaxLength="4" ondrop="return false;" AutoComplete="Off" onpaste="return false;"
                                                                            Width="5%" onkeypress="return runScript(event) "></asp:TextBox>&nbsp;
                                                                        <asp:Button ID="pageGo2" runat="server" Visible="false" Text="GO" OnClick="hplGo2_Click" />
                                                                    </td>
                                                                    <td align="right">
                                                                        <div runat="server" id="div_totalPage2">
                                                                            จำนวน <b>
                                                                                <asp:Label ID="lblTotal2" runat="server" Visible="false"></asp:Label></b> รายการ</div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <div runat="server" visible="false" id="manageApp" width="100%">
                                                    <center>
                                                        <div runat="server" visible="false" id="divAddIn">
                                                            <table width="80%">
                                                                <tr>
                                                                    <td style="width: 30%" align="right">
                                                                        <asp:Label runat="server" ID="lblshould" Text="เลือกห้องสอบ"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 10%" align="center">
                                                                        <asp:DropDownList runat="server" ID="ddlroom" Width="350px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 60%" align="left">
                                                                        <asp:Button runat="server" ID="btnAddIn" CssClass="btn" Text="จัดคนเข้าห้องสอบ" OnClick="btnAddIn_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div runat="server" visible="false" id="divGetOut">
                                                            <asp:Button runat="server" ID="btnGetOut" CssClass="btn" Text="นำคนออกจากห้องสอบ"
                                                                OnClick="btnGetOut_Click" />
                                                        </div>
                                                    </center>
                                                </div>
                                            </fieldset>
                                            <br />
                                            <br />
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="regSearchOfficerOIC.aspx.cs" Inherits="IAS.Register.regSearchOfficerOIC" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        //Renew Check All check box Natta@6-9-2556
        //Only Check Header Node
        //        function CheckAllRecords(headerChk, childId) {

        //            var IsChecked = headerChk.checked;
        //            var gv = document.getElementById('<%= gvSearchOfficerOIC.ClientID %>');
        //            var cellindex = headerChk.parentNode.cellIndex;
        //            for (i = 0; i < gv.rows.length; i++) {
        //                var curTd = gv.rows[i].cells[cellindex];
        //                var item = curTd.getElementsByTagName('input');
        //                for (j = 0; j < item.length; j++) {
        //                    if (item[j].id != headerChk && item[j].type == "checkbox") {
        //                        if (item[j].checked != IsChecked) {
        //                            item[j].click();
        //                        }
        //                    }
        //                }
        //            }
        //        }

        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function Input_Eng(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode >= 3585) {
                return false;
            } else {
                var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?^()";
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
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:UpdatePanel ID="upnSeachOIC" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box_content">
                <div class="box_content_header">
                    <div class="box_content_header_text">
                        <asp:Label Text="อนุมัติสมาชิก" BorderStyle="None" ID="lblRegisterOfficer" runat="server" />
                    </div>
                </div>
                <asp:ValidationSummary ID="SearchOfficerOICValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="SearchOfficerOICValidationGroup" />
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::ค้นหา</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right; width: 15%;">
                                        <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left; width: 25%;">
                                        <asp:DropDownList ID="ddlSearchMemberType" runat="server" CssClass="ddl" Width="150px">
                                        </asp:DropDownList>
                                        <span class="failureNotification">*</span>
                                        <asp:RequiredFieldValidator ID="SearchMemberTypeRequired" runat="server" ControlToValidate="ddlSearchMemberType"
                                            Visible="false" CssClass="failureNotification" ErrorMessage="SearchMemberType is required."
                                            ToolTip="SearchMemberType is required." ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="สถานะ"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="ddl" Width="150px">
                                        </asp:DropDownList>
                                        <span class="failureNotification">*</span>
                                        <asp:RequiredFieldValidator ID="StatusRequired" runat="server" ControlToValidate="ddlSearchStatus"
                                            Visible="false" CssClass="failureNotification" ErrorMessage="Status is required."
                                            ToolTip="Status is required." Display="Dynamic" ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt" MaxLength="13" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                    </td>
                                    <td rowspan="2">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" Width="75px"
                                            OnClick="btnSearch_Click" /><br />
                                        <br />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิก" Width="75px"
                                            OnClick="btnCancle_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" onkeypress="return Input_Eng(event);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblstartDate" runat="server" CssClass="lbl" Text="ตั้งแต่วันที่"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtstartDate" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtstartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                            runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="txtstartDate_CalendarExtender" PopupButtonID="imgPopup_txtstartDate"
                                            runat="server" Enabled="True" TargetControlID="txtstartDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lbltoDate" runat="server" CssClass="lbl" Text="ถึงวันที่"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txttoDate" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txttoDate" ImageUrl="~/Images/calendar.gif"
                                            ImageAlign="Bottom" runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="txttoDate_CalendarExtender" PopupButtonID="imgPopup_txttoDate" runat="server" Enabled="True"
                                            TargetControlID="txttoDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="text-align: right;">
                                    </td>
                                    <td style="text-align: left;">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlDetail" runat="server" Visible="true">
                    <div class="box_body_content">
                        <div class="box_textbox_bg_table">
                            <div class="box_texbox_add_box">
                                <div style="float: right; padding-right: 20px;">
                                    <asp:ImageButton ID="btnExportExcel" ToolTip="ดาวน์โหลดข้อมูล" Visible="false" runat="server"
                                        ImageUrl="~/image/exel.png" OnClick="btnExportExcel_Click" />
                                </div>
                                <br />
                                <asp:GridView ID="gvSearchOfficerOIC" runat="server" CellSpacing="-1" GridLines="None"
                                    Width="100%" EnableTheming="True" AutoGenerateColumns="False" AllowSorting="True"
                                    ShowHeaderWhenEmpty="true" BorderWidth="1px" CssClass="datatable" OnRowDataBound="gvSearchOfficerOIC_RowDataBound">
                                    <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText=">" PreviousPageText="<"
                                        Mode="NumericFirstLast" />
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
                                                <%--   <input id="chkAll" onclick="CheckAllRecords(this,'chkSelect')" runat="server"
                                        type="checkbox" visible="true"/>--%>
                                                <asp:CheckBox ID="checkAll" runat="server" OnCheckedChanged="checkAll_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged"
                                                    Visible="true" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="td-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ลำดับ">
                                            <ItemStyle Width="5%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่สมาชิก" Visible="False">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IDCard">
                                            <HeaderTemplate>
                                                เลขบัตรประชาชน
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDCard" runat="server" Text='<%#Bind("ID_CARD_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <HeaderTemplate>
                                                อีเมล
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Bind("EMAIL") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--                            <asp:TemplateField HeaderText="OICCODE">
                                <HeaderTemplate>
                                    รหัสเจ้าหน้าที่
                                </HeaderTemplate>
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOicCode" runat="server" Text='<%#Bind("OIC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                วันที่สมัคร
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegisterDate" runat="server" Text='<%#Bind("CREATED_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FirstName">
                                            <HeaderTemplate>
                                                ชื่อ
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="FirstName" runat="server" Text='<%#Bind("NAMES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LastName">
                                            <HeaderTemplate>
                                                นามสกุล
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastName" runat="server" Text='<%#Bind("LASTNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MemberType">
                                            <HeaderTemplate>
                                                ประเภทสมาชิก
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMemberType" runat="server" Text='<%#Bind("MEMBER_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TELEPHONE">
                                            <HeaderTemplate>
                                                เบอร์โทรศัพท์
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTel" runat="server" Text='<%#Bind("TELEPHONE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZipCode">
                                            <HeaderTemplate>
                                                รหัสไปรษณีย์
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblZipCode" runat="server" Text='<%#Bind("ZIP_CODE") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <HeaderTemplate>
                                                สถานะ
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("STATUS_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Appoved_by">
                                            <HeaderTemplate>
                                                ผู้อนุมัติ
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAppoved" runat="server" Text='<%#Bind("APPOVED_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="URL" ItemStyle-CssClass="td-center">
                                            <HeaderTemplate>
                                                ดำเนินการ
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                <asp:LinkButton ID="hplApprove" runat="server" OnClick="hplApprove_Click" Visible="false"><img src="../Images/approved-icon.gif" title="อนุมัติ" /></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemStyle CssClass="td-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="StatusCode" Visible="false">
                                            <HeaderTemplate>
                                                สถานะ
                                            </HeaderTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatusCode" runat="server" Text='<%#Bind("STATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Panel ID="pnPage" runat="server" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1"
                                                    Enabled="false"></asp:TextBox>
                                                <asp:Label ID="lblspace" runat="server" Text="/" Visible="false"></asp:Label>
                                                <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                <span>จำนวนรายการต่อหน้า</span>
                                                <asp:TextBox ID="txtPage" Text="20" MaxLength="4" runat="server" Style="text-align: center"
                                                    ondrop="return false;" AutoComplete="Off" onpaste="return false;" Width="5%"
                                                    onkeypress="return runScript(event)"></asp:TextBox>
                                                <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" />
                                            </td>
                                            <td align="right">
                                                <span>จำนวนรายการ </span><span>
                                                    <asp:Label ID="lblrows" runat="server"></asp:Label></span> <span>รายการ</span>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%-- <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />--%>
                                <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnSeachOIC" >
                        <ProgressTemplate>
                            Please wait....
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                                <asp:Button runat="server" ID="btnGroupApprove" Text="อนุมัติกลุ่ม" OnClick="btnGroupApprove_Click"
                                    Visible="false" />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <uc2:UCModalError ID="UCModalError" runat="server" />
                <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
            </div>
            <div style="clear: left;">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

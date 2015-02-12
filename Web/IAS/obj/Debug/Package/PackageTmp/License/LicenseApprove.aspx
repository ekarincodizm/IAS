<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="LicenseApprove.aspx.cs" Inherits="IAS.License.LicenseApprove" %>

<%@ Register Src="../UserControl/ucPersonalSkills.ascx" TagName="UserControlPersonalSkills"
    TagPrefix="ucPerSkills" %>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ให้ความเห็นชอบในการออกใบอนุญาต" BorderStyle="None" ID="lblLicenseApproveMain"
                    runat="server" />
            </div>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <%--                <Triggers>
                    <asp:PostBackTrigger ControlID="LinkButton1" />
                </Triggers>--%>
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblTypePay" runat="server" Text="ประเภทการชำระ" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="ddlTypePay" runat="server" CssClass="ddl">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblStartDate" runat="server" Text="วันที่สั่งจ่าย(เริ่ม)" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="startDate" PopupButtonID="imgPopup_txtStartDate"
                                                        runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblEndDate" runat="server" Text="วันที่สั่งจ่าย(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="EndDate" PopupButtonID="imgPopup_txtEndDate" runat="server"
                                                        TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: left" rowspan="2">
                                                    <asp:Button ID="btnSearch" runat="server" Width="75px" Text="ค้นหา" CssClass="btn"
                                                        OnClick="btnSearch_Click" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" Width="75px"
                                                        OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblOrder" runat="server" Text="ใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtOrder" runat="server" CssClass="txt" onkeypress="return runScript(event)"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblIdCard" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtIdCard" runat="server" CssClass="txt" onkeypress="return runScript(event)"
                                                        MaxLength="13"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                </td>
                                                <td style="text-align: left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblFirstName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <%-- <asp:TextBox ID="txtCal" runat="server" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calEx1" runat="server" TargetControlID="txtStartDate"></ajaxToolkit:CalendarExtender>--%>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="PnlDetailSearchGridView" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <div class="box_textbox_bg_table" runat="server" id="bludDiv">
                                    <div class="box_texbox_add_box">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvSearch" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                        GridLines="None" BorderWidth="1px" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        OnRowDataBound="gvSearch_RowDataBound">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="checkall" runat="server" AutoPostBack="true" OnCheckedChanged="checkall_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectGroupR" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectGroupR_CheckedChanged" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                                <ItemStyle CssClass="td-center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblno" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdCard" runat="server" Text='<%# Bind("id_card_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("Names") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทคำขอ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPettionName" runat="server" Text='<%# Bind("petition_type_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseName" runat="server" Text='<%# Bind("license_type_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGroupRequestNo" runat="server" Text='<%# Bind("group_request_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่ชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptDatePayGv" runat="server" Text='<%# Bind("payment_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>&nbsp;&nbsp;
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentNo" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHeadRequestNo" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPetitionCode" runat="server" Text='<%# Bind("petition_type_code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRenewTime" runat="server" Text='<%# Bind("renew_time") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseno" runat="server" Text='<%# Bind("license_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUPLOAD_GROUP_NO" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSEQ_NO" runat="server" Text='<%# Bind("SEQ_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="divGv1" runat="server">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 80%">
                                                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1"
                                                                        Enabled="false"></asp:TextBox>
                                                                    <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                                    <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblHeadInputMaxrow" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                    <asp:TextBox ID="txtInputMaxrow" runat="server" AutoComplete="off" Visible="false"
                                                                        Width="5%" onkeypress="return runScript(event)" MaxLength="4" Style="text-align: center"
                                                                        onpaste="return false;" ondrop="return false;"></asp:TextBox>
                                                                    &nbsp;
                                                                    <asp:Button ID="btngo" runat="server" Text="Go" Visible="false" OnClick="btnSearch_Click" />
                                                                </td>
                                                                <td style="width: 20%; text-align: right">
                                                                    <asp:Label ID="lblHeadTotal" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblTotalrecord" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblEndTotal" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnGenLicense" runat="server" Text="ออกใบอนุญาต" CssClass="btn" OnClick="btnGenLicense_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <table width="700px">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnS" runat="server" Style="display: none" />
                                                <ajaxToolkit:ModalPopupExtender ID="MPDetailLicense" runat="server" TargetControlID="btnS"
                                                    PopupControlID="pnlValidateprop" BackgroundCssClass="modalbackground" CancelControlID="btnCloseProp">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <asp:Panel ID="pnlValidateprop" Style="display: inherit" runat="server" CssClass="modalpopup"
                                                    Width="750px">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <%--<ucPerSkills:UserControlPersonalSkills ID="ucPerSkills" runat="server"></ucPerSkills:UserControlPersonalSkills>--%>
                                                                <div style="width: 100%;">
                                                                    <div class="box_body_content">
                                                                        <div class="box_t_h" style="width: 50%;">
                                                                            ::ตรวจสอบคุณสมบัติ
                                                                        </div>
                                                                        <br />
                                                                        <div class="box_t_h" style="text-align: right; float: right; padding-right: 30px;">
                                                                            *หมายเหตุ
                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                Height="16px" />&nbsp ผ่าน&nbsp
                                                                            <asp:Image ID="Image2" runat="server" ImageUrl="../Images/discorrect.png" Width="15px"
                                                                                Height="16px" />&nbsp ไม่ผ่าน
                                                                        </div>
                                                                        <div class="box_textbox_bg_table">
                                                                            <div class="box_texbox_add_box">
                                                                                <asp:GridView ID="gvGeneral" runat="server" EnableTheming="True" AutoGenerateColumns="False"
                                                                                    CssClass="datatableRow" CellSpacing="-1" GridLines="None" BorderWidth="1px" CaptionAlign="Top"
                                                                                    ShowHeaderWhenEmpty="True" PagerStyle-HorizontalAlign="Center">
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                    <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="คุณสมบัติทั่วไป" HeaderStyle-Wrap="true">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex + 1 %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" Wrap="True" />
                                                                                            <ItemStyle CssClass="td-center" Width="15%" VerticalAlign="Top" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="45%" CssClass="td-left" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                    Height="15px" Visible="true" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                            <ItemStyle CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <HeaderStyle Wrap="true" />
                                                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                                </asp:GridView>
                                                                                <asp:GridView ID="gvExamResult" runat="server" Width="100%" CssClass="datatableRow"
                                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1px" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                                    AutoGenerateColumns="False">
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                    <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="ผลสอบ">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex + 1 %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                            <ItemStyle CssClass="td-center" Width="15%" VerticalAlign="Top" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="45%" CssClass="td-left" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                    Height="15px" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                            <ItemStyle CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                                </asp:GridView>
                                                                                <asp:GridView ID="gvEducation" runat="server" Width="100%" CssClass="datatableRow"
                                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1px" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                                    AutoGenerateColumns="False">
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                    <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="คุณวุฒิการศึกษา">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex + 1 %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                            <ItemStyle CssClass="td-center" Width="15%" VerticalAlign="Top" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="45%" CssClass="td-left" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                    Height="15px" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                            <ItemStyle CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                                </asp:GridView>
                                                                                <asp:GridView ID="gvTrainResult" runat="server" Width="100%" CssClass="datatableRow"
                                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1px" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                                    AutoGenerateColumns="False">
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                    <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="ผลอบรม">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex + 1 %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                            <ItemStyle CssClass="td-center" Width="15%" VerticalAlign="Top" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="45%" CssClass="td-left" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                    Height="15px" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                            <ItemStyle CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                                </asp:GridView>
                                                                                <asp:GridView ID="gvOther" runat="server" Width="100%" CssClass="datatableRow" CellSpacing="-1"
                                                                                    GridLines="None" BorderWidth="1px" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                                    AutoGenerateColumns="False">
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                    <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="อื่น ๆ">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex + 1 %>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                                            <ItemStyle CssClass="td-center" Width="15%" VerticalAlign="Top" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="45%" CssClass="td-left" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                    Height="15px" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                            <ItemStyle CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                                </asp:GridView>
                                                                                <asp:GridView ID="gvExamSpecial" runat="server" Width="100%" CssClass="datatableRow"
                                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                                    OnRowCreated="Head_RowCreated">
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                    <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="คุณสมบัติพิเศษลดชั่วโมงการสอบ" HeaderStyle-Wrap="true">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex + 1 %>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="50%" CssClass="td-left" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                            <ItemStyle CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                                </asp:GridView>
                                                                                <asp:GridView ID="gvTrainSpecial" runat="server" Width="100%" CssClass="datatableRow"
                                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                                    OnRowCreated="Head_RowCreated">
                                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                                    <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="คุณสมบัติพิเศษลดชั่วโมงการอบรม" HeaderStyle-Wrap="true">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex + 1 %>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="50%" CssClass="td-left" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("DOCUMENT_NAME") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                            <ItemStyle CssClass="td-center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_ATTACH_FILE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td>
                                                                <br />
                                                                <asp:Button runat="server" ID="btnCloseProp" Text="ปิด" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

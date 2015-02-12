<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="RequestVerifydoc.aspx.cs" Inherits="IAS.Reporting.RequestVerifydoc" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

        function confirmSave() {
            return confirm('คุณต้องการบันทึกข้อมูลหรือไม่');
        }


        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
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
                <asp:Label Text="ตรวจสอบเอกสาร" BorderStyle="None" ID="lblCheckDocument" runat="server" />
            </div>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="box_body_content">
                            <div class="box_t_h">
                                ::ค้นหา</div>
                            <div class="box_textbox_bgadd">
                                <div class="box_texbox_add_box">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblTypeDocument" runat="server" Text="ประเภท" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblStartDate" runat="server" Text="ตั้งแต่วันที่" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CLDtxtStartDate" PopupButtonID="imgPopup_txtStartDate"
                                                    runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:ImageButton ID="ibtClearStartPaidDate" runat="server" Height="15px" ImageUrl="~/Images/clear_button.png"
                                                    OnClick="ibtClearStartPaidDate_Click" />
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblEndDate" runat="server" CssClass="lbl" Text="ถึงวันที่"></asp:Label>
                                                :&nbsp;&nbsp;
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CLDtxtEndDate" PopupButtonID="imgPopup_txtEndDate"
                                                    runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                                <asp:ImageButton ID="ibtClearEndPaidDate" runat="server" Height="15px" ImageUrl="~/Images/clear_button.png"
                                                    OnClick="ibtClearEndPaidDate_Click" />
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                                    <ProgressTemplate>
                                                        <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                                        Please wait image uploaded....</ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlSearch" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvDetail" runat="server" GridLines="None" Width="100%" AllowPaging="True"
                                                        BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False" EmptyDataText="ไม่มีข้อมูล..."
                                                        OnRowDataBound="gvDetail_RowDataBound" CellSpacing="-1" CssClass="datatable"
                                                        BorderWidth="1">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ-สกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FLNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseNumberGv" runat="server" Text='<%# Bind("TRAN_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseTypeGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หมายเลขกลุ่ม" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGroupIDNumberGv" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หมายเลข" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Bind("SEQ_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseTypeName" runat="server" Text='<%#Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="บริษัท" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompanyRequest" runat="server" Text='<%#Bind("COMP_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่พิจารณา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproveDate" runat="server" Text='<%#Bind("APPROVED_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ผู้พิจารณา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproveBy" runat="server" Text='<%#Bind("NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สถานะ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("APPROVED") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินรายการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplViewDoc" runat="server" OnClick="hplViewDoc_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:HiddenField ID="hdfNumber" runat="server" />
                                                    <asp:HiddenField ID="hdfGroupNumber" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <%--    <div style="display: none">
                        <asp:Button ID="btnModal" runat="server" Text="Open" />
                    </div>--%>
                        <%--  <ajaxToolkit:ModalPopupExtender ID="ModVerify" runat="server" CancelControlID="btnCancel"
                        OkControlID="btnSubmit" TargetControlID="btnModal" PopupControlID="pnlDetail"
                        PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalbackground">
                    </ajaxToolkit:ModalPopupExtender>--%>
                        <asp:Panel ID="pnlDetail" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ตรวจสอบเอกสาร</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" ReadOnly="true" MaxLength="70"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblIDNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtIDNumber" runat="server" CssClass="txt" MaxLength="13" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    &nbsp;<asp:Label ID="lblDateLicense" runat="server" CssClass="lbl" Text="วันที่อนุญาต"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtDateLicense" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblExpireDate" runat="server" CssClass="lbl" Text="วันหมดอายุ"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtExpireDate" runat="server" CssClass="txt" MaxLength="13" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblNumberLicense" runat="server" CssClass="lbl" Text="เลขที่อนุญาต"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtNumberLicense" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" MaxLength="13" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblTimeMove" runat="server" CssClass="lbl" Text="ครั้งที่ย้าย"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtTimeMove" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblCompCode" runat="server" CssClass="lbl" Text="รหัสบริษัทเก่า"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtCompCode" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblDateAccept" runat="server" CssClass="lbl" Text="ข้อมูลวันที่ตอบรับ/ลาออก"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtDateAccept" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblFee" runat="server" Text="ค่าธรรมเนียม" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtFee" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 90%;" align="center">
                                            <tr>
                                                <td>
                                                    <ajaxToolkit:TabContainer ID="TabAddress" CssClass="ajax_tabs" runat="server" Width="90%"
                                                        ActiveTabIndex="0">
                                                        <ajaxToolkit:TabPanel ID="TabCurrentAddress" runat="server">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblCurrentAddressHeader" runat="server" ForeColor="Black" Text="ที่อยู่ปัจจุบัน"></asp:Label></HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table style="width: 100%; border-width: 1px; border-color: #dddddd; border-style: solid">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 5%;">
                                                                            <asp:Label ID="lblCurrentAddress" runat="server" CssClass="lbl" Text="ที่อยู่"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td rowspan="2" style="text-align: left; width: 25%;">
                                                                            <asp:TextBox ID="txtCurrentAddress" runat="server" CssClass="txt" Height="50px" MaxLength="200"
                                                                                TextMode="MultiLine" Width="200px" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblProvinceCurrentAddress" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:DropDownList ID="ddlProvinceCurrentAddress" runat="server" AutoPostBack="True"
                                                                                CssClass="ddl" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 10%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <span>
                                                                                <asp:Label ID="lblParishCurrentAddress" runat="server" CssClass="lbl" Text="แขวง/ตำบล"></asp:Label>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:DropDownList ID="ddlParishCurrentAddress" runat="server" AutoPostBack="True"
                                                                                CssClass="ddl" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 5%;">
                                                                            <span>
                                                                                <asp:Label ID="lblDistrictCurrentAddress" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 25%;">
                                                                            <asp:DropDownList ID="ddlDistrictCurrentAddress" runat="server" AutoPostBack="True"
                                                                                CssClass="ddl" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                        <ajaxToolkit:TabPanel ID="TabVerifyAddress" runat="server">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblRegisterAddressHeader" runat="server" ForeColor="Black" Text="ที่อยู่ตามทะเบียนบ้าน"></asp:Label></HeaderTemplate>
                                                            <ContentTemplate>
                                                                <table style="width: 100%; border-width: 1px; border-color: #dddddd; border-style: solid">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 5%;">
                                                                            <asp:Label ID="lblRegisterAddress" runat="server" CssClass="lbl" Text="ที่อยู่"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td rowspan="2" style="text-align: left; width: 25%;">
                                                                            <asp:TextBox ID="txtRegisterAddress" runat="server" CssClass="txt" Height="50px"
                                                                                MaxLength="100" TextMode="MultiLine" Width="200px" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblProvinceRegisterAddress" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:DropDownList runat="server" ID="ddlProvinceRegisterAddress" AutoPostBack="true"
                                                                                CssClass="ddl" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 10%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <span>
                                                                                <asp:Label ID="lblParishRegisterAddress" runat="server" CssClass="lbl" Text="แขวง/ตำบล"></asp:Label>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:DropDownList runat="server" ID="ddlParishRegisterAddress" AutoPostBack="true"
                                                                                CssClass="ddl" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 5%;">
                                                                            <span>
                                                                                <asp:Label ID="lblDistrictRegisterAddress" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 25%;">
                                                                            <asp:DropDownList runat="server" ID="ddlDistrictRegisterAddress" AutoPostBack="true"
                                                                                CssClass="ddl" Width="150px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                    </ajaxToolkit:TabContainer>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="ปิด" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
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
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

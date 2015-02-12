<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="ResultVerify.aspx.cs" Inherits="IAS.Reporting.ResultVerify" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function setdate() {
            $('#txtStartDate').attr('readOnly', 'true');
            $('#txtEndDate').attr('readOnly', 'true');
        });
        function runScript(e) {
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
    <div>
        <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
    </div>
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ตรวจสอบเอกสาร" BorderStyle="None" ID="lblCheckDocument" runat="server" />
            </div>
        </div>
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
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTypeDocument" runat="server" Text="ประเภท" CssClass="lbl"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: right;">
                                            </td>
                                            <td style="text-align: left;">
                                            </td>
                                            <td style="width: 20%;" rowspan="2">
                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click"
                                                    Width="75px" />
                                                <br />
                                                <br />
                                                <%--<a href="../Register/regViewRegis4.aspx">regViewRegis4.aspx</a><br />--%>
                                                <asp:Button ID="btnCancelMain" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnMainCancle_Click"
                                                    Width="75px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblStartDate" runat="server" Text="ตั้งแต่วันที่" CssClass="lbl"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtStartDate" runat="server" onfocus="blur()" onKeyPress="return false;"
                                                    onKeyDown="return false"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CLDtxtStartDate" PopupButtonID="imgPopup_txtStartDate"
                                                    runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblEndDate" runat="server" CssClass="lbl" Text="ถึงวันที่"></asp:Label>:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtEndDate" runat="server" onfocus="blur()" onKeyPress="return false;"
                                                    onKeyDown="return false"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CLDtxtEndDate" PopupButtonID="imgPopup_txtEndDate"
                                                    runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlSearch" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::รายการใบขออนุญาต</div>
                                <div style="float: right; padding-right: 30px;">
                                    <asp:ImageButton ID="btnExportExcel" Visible="false" runat="server" ImageUrl="~/image/exel.png"
                                        OnClick="btnExportExcel_Click" />
                                </div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvHead" runat="server" CssClass="datatable" GridLines="None" CellSpacing="-1"
                                                        BorderWidth="1" Width="100%" BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowDataBound="gvHead_RowDataBound" ShowHeaderWhenEmpty="true">
                                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                                        <AlternatingRowStyle CssClass="altrow" />
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
                                                            <asp:TemplateField HeaderText="เลขที่ใบคำขออนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUploadGroupNoGv" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FILENAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseHeadNumberGv" runat="server" Text='<%# Bind("TRAN_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รูปแบบการขอใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPetitionNameGv" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทการขอใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseNameGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseTypeGv" runat="server" Text='<%# Bind("LOTS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สถานะ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("APPROVED_DOC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สถานะ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatusName" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplHeadViewDoc" runat="server" OnClick="hplHeadViewDoc_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
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
                                                    <asp:TextBox ID="txtInputMaxrow" runat="server" Visible="false" Width="5%" onkeypress="return runScript(event)"
                                                        MaxLength="4" Style="text-align: center" onpaste="return false;" ondrop="return false;"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:Button ID="btngo" runat="server" Text="Go" OnClick="btnSearch_Click" Visible="false" />
                                                </td>
                                                <td style="width: 20%; text-align: right">
                                                    <asp:Label ID="lblHeadTotal" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblTotalrecord" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblEndTotal" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <div class="box_textbox_bg_table" id="divDetail" runat="server">
                                    <div style="float: right; padding-right: 2px;">
                                        <asp:ImageButton ID="btnExportExcel2" Visible="true" runat="server" ImageUrl="~/image/exel.png"
                                            OnClick="btnExportExcel2_Click" />
                                    </div>
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvDetail" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                        GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowDataBound="gvDetail_RowDataBound" ShowHeaderWhenEmpty="true">
                                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
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
                                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="14%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หมายเลขกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="14%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGroupIDNumberGv" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หมายเลข" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Bind("SEQ_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่พิจารณา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="14%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproveDate" runat="server" Text='<%#Bind("APPROVED_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สถานะ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("APPROVED") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สถานะ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="14%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatusName" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ผู้พิจารณา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="14%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApproveBy" runat="server" Text='<%#Bind("NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ข้อมูลส่วนตัว" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="14%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplViewDoc" runat="server" OnClick="hplViewDoc_Click"><img src="../Images/view-icon.png" title="รายละเอียด" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รายละเอียดรูปภาพ" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="14%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplViewImg" runat="server" OnClick="hplViewImg_Click">รายละเอียดเอกสาร</asp:LinkButton>
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
                                                            <td style="width: 80%">
                                                                <asp:Button ID="btnPreviousGvSearch2" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch2_Click" />
                                                                <asp:TextBox ID="txtNumberGvSearch2" runat="server" Width="15px" Visible="false"
                                                                    Text="1" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="lblParaPage2" runat="server" Text="/" Visible="false"></asp:Label>
                                                                <asp:Label ID="txtTotalPage2" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btnNextGvSearch2" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch2_Click" />
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblHeadInputMaxrow2" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                <asp:TextBox ID="txtInputMaxrow2" runat="server" Visible="false" Width="5%" onkeypress="return runScript(event)"
                                                                    MaxLength="4" Style="text-align: center" onpaste="return false;" ondrop="return false;"></asp:TextBox>
                                                                &nbsp;
                                                                <asp:Button ID="btngo2" runat="server" Text="Go" OnClick="hplGo2_Click" Visible="false" />
                                                                <asp:Label ID="lblGroupIdGv2" runat="server" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblIdCardGV2" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td style="width: 20%; text-align: right">
                                                                <asp:Label ID="lblHeadTotal2" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblTotalrecord2" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblEndTotal2" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                        <asp:Panel ID="pnlAttach" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::เอกสารแนบ</div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;" colspan="4">
                                                    <asp:GridView ID="gvUpload" runat="server" AutoGenerateColumns="False" CssClass="datatable"
                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" OnRowDataBound="gvUpload_RowDataBound"
                                                        ShowHeaderWhenEmpty="true" Width="100%">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <EditItemTemplate>
                                                                    <asp:LinkButton ID="lbnOkGv" runat="server" CausesValidation="True" CommandName="Update"
                                                                        Text="ตกลง"></asp:LinkButton>
                                                                    &nbsp;<asp:LinkButton ID="lbnCancelGv" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                        Text="ยกเลิก"></asp:LinkButton>
                                                                </EditItemTemplate>
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNOOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทไฟล์แนบ" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="20%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTypeGv" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ไฟล์แนบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileGv" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>' Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplViewDetailImg" runat="server"><img src="../Images/view-icon.png" title="ดูเอกสาร" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TempFilePath" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="30%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTempFilePath" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ATTACH_FILE_PATH" ItemStyle-HorizontalAlign="Center"
                                                                Visible="false">
                                                                <ItemStyle Width="20%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAttachFilePathGv" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 80%">
                                                    <asp:Button ID="btnPreviousGvSearch3" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch3_Click" />
                                                    <asp:TextBox ID="txtNumberGvSearch3" runat="server" Width="15px" Visible="false"
                                                        Text="1" Enabled="false"></asp:TextBox>
                                                    <asp:Label ID="lblParaPage3" runat="server" Text="/" Visible="false"></asp:Label>
                                                    <asp:Label ID="txtTotalPage3" runat="server" Visible="false"></asp:Label>
                                                    <asp:Button ID="btnNextGvSearch3" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch3_Click" />
                                                    &nbsp;&nbsp;
                                                    <asp:Label ID="lblHeadInputMaxrow3" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                    <asp:TextBox ID="txtInputMaxrow3" runat="server" Visible="false" Width="5%" onkeypress="return runScript(event)"
                                                        MaxLength="4" Style="text-align: center" onpaste="return false;" ondrop="return false;"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:Button ID="btngo3" runat="server" Text="Go" OnClick="btnGo3_Click" Visible="false" />
                                                </td>
                                                <td style="width: 20%; text-align: right">
                                                    <asp:Label ID="lblHeadTotal3" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblTotalrecord3" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblEndTotal3" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
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
                                                    <ajaxToolkit:TabContainer ID="TabAddress" runat="server" Width="90%" ActiveTabIndex="0">
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
                                                                                CssClass="ddl" Width="150px" Enabled="False">
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
                                                                                CssClass="ddl" Width="150px" Enabled="False">
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
                                                                                CssClass="ddl" Width="150px" Enabled="False">
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
                                                                            <asp:DropDownList runat="server" ID="ddlProvinceRegisterAddress" AutoPostBack="True"
                                                                                CssClass="ddl" Width="150px" Enabled="False">
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
                                                                            <asp:DropDownList runat="server" ID="ddlParishRegisterAddress" AutoPostBack="True"
                                                                                CssClass="ddl" Width="150px" Enabled="False">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 5%;">
                                                                            <span>
                                                                                <asp:Label ID="lblDistrictRegisterAddress" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 25%;">
                                                                            <asp:DropDownList runat="server" ID="ddlDistrictRegisterAddress" AutoPostBack="True"
                                                                                CssClass="ddl" Width="150px" Enabled="False">
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
                                                    <asp:RadioButtonList ID="rblApprove" runat="server" CssClass="rb" RepeatDirection="Horizontal"
                                                        Enabled="false">
                                                        <asp:ListItem Text="อนุมัติ" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="ไม่อนุมัติ" Value="N"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblMsg" runat="server" CssClass="lbl" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:ValidationSummary ID="verifyValidationSummary" runat="server" CssClass="failureNotification"
                                                        HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="True" ShowSummary="False"
                                                        ValidationGroup="verifyValidationGroup" />
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
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                        <asp:PostBackTrigger ControlID="btnExportExcel2" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

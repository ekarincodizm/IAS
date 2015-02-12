<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Verifydoc.aspx.cs" Inherits="IAS.Reporting.Verifydoc" %>

<%@ Register Src="../UserControl/ucPersonalSkills.ascx" TagName="UserControlPersonalSkills"
    TagPrefix="ucPerSkills" %>
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


        var TotalChkBx; //=10;

        var CountLicense = 0;
        var CounterPayment = 0;

        function Get_TotalChkBx(GVCount) {
            TotalChkBx = GVCount;
        }

        function HeaderClick(CheckBox, GVid, MomId, ChildId) {
            var TargetBaseControl = null;
            if (GVid.toString() == 'gvDetail') {
                var TargetBaseControl = document.getElementById('<%= this.gvDetail.ClientID %>');
            }
            //            else {// gvLicenseNumber
            //                var TargetBaseControl = document.getElementById('<%= this.gvDetail.ClientID %>');
            //            }
            var TargetChildControl = ChildId;
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;
            if (GVid.toString() == 'gvDetail') {
                CounterPayment = CheckBox.checked ? TotalChkBx : 0;
            }
            else {   // gvLicenseNumber
                CountLicense = CheckBox.checked ? TotalChkBx : 0;
            }

        }

        function ChildClick(CheckBox, HCheckBox, GVid, ChildId) {
            //debugger;
            var HeaderCheckBox = document.getElementById(HCheckBox);

            var TargetBaseControl = document.getElementById('<%= this.gvDetail.ClientID %>');

            var total = 0;
            var TargetChildControl = ChildId;
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (var n = 0; n < Inputs.length; ++n) {
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    if (Inputs[n].checked == true) {
                        total += 1;
                    }
                }
            }
            if (total == Inputs.length - 1) {
                HeaderCheckBox.checked = true;
            }
            else {

                if (HeaderCheckBox.checked == true && CheckBox.checked == false) {
                    HeaderCheckBox.checked = false;
                }

            }



            // var TargetBaseControl = document.getElementById('<%= this.gvDetail.ClientID %>');
            //var count = TargetBaseControl.length;


            //            if (GVid.toString() == 'gvDetail') {
            //                if (CheckBox.checked && CounterPayment < TotalChkBx)
            //                    CounterPayment++;
            //                else if (CounterPayment > 0)
            //                    CounterPayment--;
            //                if (CounterPayment < TotalChkBx)
            //                    HeaderCheckBox.checked = false;
            //                else if (CounterPayment == TotalChkBx)
            //                    HeaderCheckBox.checked = true;
            //            }
            //            else { // gvLicenseNumber     
            //                if (CheckBox.checked && CountLicense < TotalChkBx)
            //                    CountLicense++;
            //                else if (CountLicense > 0)
            //                    CountLicense--;
            //                if (CountLicense < TotalChkBx)
            //                    HeaderCheckBox.checked = false;
            //                else if (CountLicense == TotalChkBx)
            //                    HeaderCheckBox.checked = true;
            //            }



        }


        //        $(function setdate() {
        //            $('#txtStartDate').attr('readOnly', 'true');
        //            $('#txtEndDate').attr('readOnly', 'true');
        //        });



        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .modalpopup
        {
            background-color: transparent !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div>
        <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
        <div>
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
                                                    <td style="text-align: right; width: 12%;">
                                                        <asp:Label ID="lblTypeDocument" runat="server" Text="ประเภท" CssClass="lbl"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: right; width: 10%;">
                                                        <asp:Label ID="lblTypeDocument0" runat="server" CssClass="lbl" Text="ผู้ยื่นคำร้อง"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlCompanyRequest" runat="server" CssClass="ddl">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblStatusApprove" runat="server" CssClass="lbl" Text="สถานะ"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlStatusApprove" runat="server" CssClass="ddl">
                                                            <asp:ListItem Text="ทั้งหมด" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="รอพิจารณา" Value="W"></asp:ListItem>
                                                            <asp:ListItem Text="อนุมัติ" Value="Y"></asp:ListItem>
                                                            <asp:ListItem Text="ไม่อนุมัติ" Value="N"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 12%;" rowspan="2">
                                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click"
                                                            Width="75px" />
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="btnCancelMain" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnMainCancle_Click"
                                                            Width="75px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lblStartDate" runat="server" Text="วันที่" CssClass="lbl"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
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
                                        <%--<div class="box_t_h">
                                            ::รายการใบขออนุญาต</div>--%>
                                        <div class="box_textbox_bg_table" id="gv1" runat="server">
                                            <fieldset>
                                                <legend>::รายการใบขออนุญาต</legend>
                                                <div style="float: right; padding-right: 20px;">
                                                    <asp:ImageButton ID="btnExportExcel" Visible="true" runat="server" ImageUrl="~/image/exel.png"
                                                        OnClick="btnExportExcel_Click" ToolTip="ดาวน์โหลดข้อมูล" />
                                                </div>
                                                <div class="box_texbox_add_box">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: center;">
                                                                <asp:GridView ID="gvHead" runat="server" CssClass="datatable" GridLines="None" CellSpacing="-1"
                                                                    BorderWidth="1" Width="100%" BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False"
                                                                    OnRowDataBound="gvHead_RowDataBound" ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="gvHead_SelectedIndexChanged"
                                                                    OnRowCommand="gvHead_RowCommand">
                                                                    <AlternatingRowStyle CssClass="table_alter_row" />
                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                    <SelectedRowStyle BackColor="#99FFCC" />
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
                                                                        <asp:TemplateField HeaderText="วันที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLicenseHeadNumberGv" runat="server" Text='<%# Bind("TRAN_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
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
                                                                        <asp:TemplateField HeaderText="ผู้ขอรับใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblNames" runat="server" Text='<%# Bind("NAMES") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLicenseTypeGv" runat="server" Text='<%# Bind("LOTS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="PititionTypeCode" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPetitionTypeCode" runat="server" Text='<%# Bind("petition_type_code") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="LicenseTypeCode" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
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
                                                                                <asp:LinkButton ID="hplHeadViewDoc" CommandName="Select" runat="server" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                                    OnClick="hplHeadViewDoc_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:HiddenField ID="H_UploadGroupNo" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                                                    <asp:TextBox ID="txtInputMaxrow" runat="server" Visible="false" Width="5%" onkeypress="return runScript(event)"
                                                                        MaxLength="4" Style="text-align: center" ondrop="return false;" onpaste="return false;"></asp:TextBox>
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
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="box_body_content">
                                        <div class="box_t_h">
                                        </div>
                                        <div class="box_textbox_bg_table" id="divDetail" runat="server">
                                            <fieldset>
                                                <legend>::รายละเอียด</legend>
                                                <div style="float: right; padding-right: 20px;">
                                                    <asp:ImageButton ID="btnexport2" Visible="true" runat="server" ImageUrl="~/image/exel.png"
                                                        ToolTip="ดาวน์โหลดข้อมูล" OnClick="btnexport2_Click" />
                                                </div>
                                                <div class="box_texbox_add_box">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView ID="gvDetail" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                                    GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                    ShowHeaderWhenEmpty="true" OnRowDataBound="gvDetail_RowDataBound" OnRowCreated="gvDetail_RowCreated"
                                                                    OnSelectedIndexChanged="gvDetail_SelectedIndexChanged" OnRowCommand="gvDetail_RowCommand">
                                                                    <AlternatingRowStyle CssClass="table_alter_row" />
                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                    <SelectedRowStyle BackColor="#99FFCC" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                            ไม่พบข้อมูล...</div>
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:CheckBox ID="chkAll" onclick="javascript:HeaderClick(this,'gvDetail','chkAll','chkSelectDetail');"
                                                                                    runat="server" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSelectDetail" runat="server" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle CssClass="td-center" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="14%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblGroupIDNumberGv" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="14%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ผู้พิจารณา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="14%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblApproveBy" runat="server" Text='<%#Bind("NAME") %>'></asp:Label>
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
                                                                        <asp:TemplateField HeaderText="วันเวลาที่พิจารณา" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="14%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblApproveDateTime" runat="server" Text='<%#Bind("APPROVED_DATE","{0:dd/MM/yyyy HH:mm}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สถานะ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("APPROVED") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="RenewTime" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRenewTime" runat="server" Text='<%#Bind("RENEW_TIMES") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="LicenseNo" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblLicenseNo" runat="server" Text='<%#Bind("LICENSE_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="สถานะ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="14%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStatusName" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="คุณสมบัติพิเศษ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Image ID="imgSpecial" runat="server" Visible="false" ImageUrl="~/Images/icon_Success.png"
                                                                                    Width="25px" Height="25px" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="12%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="hplViewDoc" runat="server" CommandName="Select" OnClick="hplViewDoc_Click"
                                                                                    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"><img src="../Images/view-icon.png" title="รายละเอียด" /></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="รายละเอียดรูปภาพ" ItemStyle-HorizontalAlign="Center"
                                                                            Visible="false">
                                                                            <ItemStyle Width="14%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="hplViewImg" runat="server" OnClick="hplViewImg_Click">รายละเอียดเอกสาร</asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <div id="divGv2" runat="server">
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
                                                                                    MaxLength="4" Style="text-align: center" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                                                                &nbsp;
                                                                                <asp:Button ID="btngo2" runat="server" Text="Go" OnClick="hplGo2_Click" Visible="false" />
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
                                                        </div>
                                                        <tr>
                                                            <td style="text-align: right;">
                                                                <asp:HiddenField ID="hdfNumber" runat="server" />
                                                                <asp:HiddenField ID="hdfGroupNumber" runat="server" />
                                                                <asp:HiddenField ID="hdfgroupId" runat="server" />
                                                                <asp:HiddenField ID="hdfIdCard" runat="server" />
                                                                <asp:Button ID="btnOverAllApprove" runat="server" Text="อนุมัติรวม" CssClass="btn"
                                                                    OnClick="btnOverAllApprove_Click" Enabled="true" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </fieldset>
                                            <br />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlDetail" runat="server" Visible="false">
                                    <div class="box_body_content">
                                        <div class="box_textbox_bgadd">
                                            <fieldset>
                                                <legend>::ตรวจสอบเอกสาร</legend>
                                                <div class="box_texbox_add_box">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblIDNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtIDNumber" runat="server" CssClass="txt" MaxLength="13" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblNumberLicense" runat="server" CssClass="lbl" Text="เลขที่ใบอนุญาต"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtNumberLicense" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtTitle" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; width: 20%;">
                                                                &nbsp;<asp:Label ID="lblDateLicense" runat="server" CssClass="lbl" Text="วันที่ออกอนุญาต"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtDateLicense" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblExpireDate" runat="server" CssClass="lbl" Text="วันที่หมดอายุใบอนุญาต"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtExpireDate" runat="server" CssClass="txt" MaxLength="13" ReadOnly="true"></asp:TextBox>
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
                                                                <asp:Label ID="lblTimeMove" runat="server" CssClass="lbl" Text="ครั้งที่ย้าย"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtTimeMove" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" MaxLength="13" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblCompCode" runat="server" CssClass="lbl" Text="บริษัทเดิม"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                                <asp:TextBox ID="txtCompCode" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                            </td>
                                                            <td style="text-align: right; width: 20%;">
                                                            </td>
                                                            <td style="text-align: left; width: 30%;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Panel ID="pnlAttach" runat="server" Visible="false">
                                                        <div class="box_body_content">
                                                            <%--<div class="box_t_h">
                                            ::เอกสารแนบ</div>--%>
                                                            <div class="box_textbox_bg_table">
                                                                <div class="box_texbox_add_box">
                                                                    <fieldset>
                                                                        <legend>::เอกสารแนบ</legend>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="text-align: center;" colspan="4">
                                                                                    <asp:GridView ID="gvUpload" runat="server" AutoGenerateColumns="False" CssClass="datatable"
                                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" OnPreRender="gvUpload_PreRender"
                                                                                        OnRowCancelingEdit="gvUpload_RowCancelingEdit" OnRowDataBound="gvUpload_RowDataBound"
                                                                                        ShowHeaderWhenEmpty="true" OnRowEditing="gvUpload_RowEditing" OnRowUpdating="gvUpload_RowUpdating"
                                                                                        Width="100%">
                                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                                        <HeaderStyle BorderStyle="None" />
                                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField ShowHeader="False" Visible="false">
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
                                                                                                <ItemStyle Width="30%" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblFileGv" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>' Visible="true"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="hplViewDetailImg" runat="server"><img src="../Images/view-icon.png" title="ดูเอกสาร" /></asp:LinkButton>
                                                                                                    <%--   <asp:LinkButton ID="hplDelete" runat="server" OnClick="hplDelete_Click" OnClientClick="return confirmDelete();">ลบ</asp:LinkButton>--%>
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
                                                                                                <ItemStyle Width="10%" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblAttachFilePathGv" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                    </fieldset>
                                                                </div>
                                                                <div class="clear">
                                                                </div>
                                                                <div id="divGv3" runat="server">
                                                                    <tr>
                                                                        <td>
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
                                                                                            MaxLength="4" Style="text-align: center" ondrop="return false;" onpaste="return false;"></asp:TextBox>
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
                                                                        </td>
                                                                    </tr>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <fieldset>
                                                        <legend>::คุณสมบัติพิเศษ</legend>
                                                        <table style="width: 100%;">
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="text-align: right;">
                                                                    <asp:GridView ID="gvExamSpecial" runat="server" AutoGenerateColumns="False" BorderWidth="1"
                                                                        CellSpacing="-1" CssClass="datatable" EnableTheming="True" GridLines="None" ShowHeaderWhenEmpty="true"
                                                                        Width="90%">
                                                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                        <EmptyDataTemplate>
                                                                            <div style="text-align: left; margin-left: 0; margin-right: 0">
                                                                                ไม่พบข้อมูล...</div>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="คุณสมบัติ(ลดชั่วโมงสอบ)" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="true">
                                                                                <ItemStyle CssClass="td-center" Width="5%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRunNo0" runat="server" Text='<%# Bind("SPECIAL_TYPE_DESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td colspan="2" style="text-align: right;">
                                                                    <asp:GridView ID="gvTrainSpecial" runat="server" AutoGenerateColumns="False" BorderWidth="1"
                                                                        CellSpacing="-1" CssClass="datatable" EnableTheming="True" GridLines="None" ShowHeaderWhenEmpty="true"
                                                                        Width="90%">
                                                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                        <EmptyDataTemplate>
                                                                            <div style="text-align: left; margin-left: 0; margin-right: 0">
                                                                                ไม่พบข้อมูล...</div>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="คุณสมบัติ(ลดชั่วโมงผลอบรม)" ItemStyle-HorizontalAlign="Left"
                                                                                Visible="true">
                                                                                <ItemStyle CssClass="td-center" Width="5%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRunNo0" runat="server" Text='<%# Bind("SPECIAL_TYPE_DESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <tr>
                                                                    <td colspan="2" style="text-align: right;">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td colspan="2" style="text-align: right;">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                    <br />
                                                    <fieldset>
                                                        <legend>::ที่อยู่</legend>
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
                                                        </table>
                                                    </fieldset>
                                                    <br />
                                                    <fieldset>
                                                        <legend>::ตรวจสอบคุณสมบัติ</legend>
                                                        <br />
                                                        <div>
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <table width="100%;">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Panel ID="pnlValidateprop" runat="server">
                                                                                    <div style="width: 80%; text-align: center;">
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
                                                                                                        CssClass="datatableRow" CellSpacing="-1" GridLines="None" BorderWidth="1" CaptionAlign="Top"
                                                                                                        ShowHeaderWhenEmpty="True" PagerStyle-HorizontalAlign="Center" OnRowCreated="Head_RowCreated">
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
                                                                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                                        Height="15px" Visible="true" />
                                                                                                                    <%--<asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px" Visible="true" />--%>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="15%" CssClass="td-center" />
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
                                                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated">
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
                                                                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                                        Height="15px" />
                                                                                                                    <%--<asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px"  />--%>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="15%" CssClass="td-center" />
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
                                                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated">
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
                                                                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                                        Height="15px" />
                                                                                                                    <%--<asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px"  />--%>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="15%" CssClass="td-center" />
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
                                                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated">
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
                                                                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                                        Height="15px" Visible="true" />
                                                                                                                    <%--<asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px"  Visible="true"/>--%>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="15%" CssClass="td-center" />
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
                                                                                                 <%--   <asp:GridView ID="gvOther" runat="server" Width="100%" CssClass="datatableRow" CellSpacing="-1"
                                                                                                        GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True" Visible="false"
                                                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated">
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
                                                                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                                                        Height="15px" />
                                                                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px"  />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                                                <ItemStyle Width="15%" CssClass="td-center" />
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
                                                                                                    </asp:GridView>--%>
                                                                                                    <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="datatableRow"
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
                                                                                                    <asp:GridView ID="GridView2" runat="server" Width="100%" CssClass="datatableRow"
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
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <br />
                                                    </fieldset>
                                                    <br />
                                                    <fieldset>
                                                        <legend>::ให้ความเห็นชอบ</legend>
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right; width: 15%;">
                                                                    <asp:Label ID="lblStatus" runat="server" Text="สถานะ :" CssClass="lbl"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 40%;">
                                                                    <%--<asp:RadioButtonList ID="rblApprove" runat="server" CssClass="rb" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="อนุมัติ" Value="Y"></asp:ListItem>
                                                                <asp:ListItem Text="ไม่อนุมัติ" Value="N"></asp:ListItem>
                                                            </asp:RadioButtonList>--%>
                                                                    <asp:DropDownList ID="rblApprove" runat="server" CssClass="ddl">
                                                                        <asp:ListItem Text="เลือก" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="อนุมัติ" Value="Y"></asp:ListItem>
                                                                        <asp:ListItem Text="ไม่อนุมัติ" Value="N"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right; width: 10%;">
                                                                    <asp:Label ID="lbldatetimeApprove" runat="server" CssClass="lbl" Text="วันที่พิจารณา : "></asp:Label>
                                                                </td>
                                                                <td style="text-align: left; width: 20%;">
                                                                    <asp:TextBox ID="txtdatetimeApprove" runat="server" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center; width: 80%;" colspan="4">
                                                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="ตกลง" ValidationGroup="verifyValidationGroup"
                                                                        OnClick="btnSubmit_Click" OnClientClick="return confirmSave();" />
                                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </fieldset>
                                                    <br />
                                                    <table>
                                                        <tr>
                                                            <td style="text-align: left; width: 80%;" colspan="2">
                                                                <asp:Label ID="lblMsg" runat="server" CssClass="lbl" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left; width: 80%;" colspan="2">
                                                                <asp:ValidationSummary ID="verifyValidationSummary" runat="server" CssClass="failureNotification"
                                                                    HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="True" ShowSummary="False"
                                                                    ValidationGroup="verifyValidationGroup" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </div>
                                                <br />
                                            </fieldset>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <uc2:UCModalError ID="UCModalError" runat="server" />
                                <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportExcel" />
                                <asp:PostBackTrigger ControlID="btnexport2" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

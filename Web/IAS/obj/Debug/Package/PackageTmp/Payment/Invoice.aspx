<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="Invoice.aspx.cs" Inherits="IAS.Payment.Invoice" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmProcess() {
            return confirm('ยืนยันการประมวล');
        }

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
            // $('#txtStartPaidSubDate').attr('readOnly', 'true');
            //  $('#txtEndPaidSubDate').attr('readOnly', 'true');
        });

        function OnContactSelected(source, eventArgs) {

            var hdfValueID = "<%= hdf.ClientID %>";

            document.getElementById(hdfValueID).value = eventArgs.get_value();
            __doPostBack(hdfValueID, "");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="สร้างใบสั่งจ่ายย่อย" BorderStyle="None" ID="lblCreatePayment" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
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
                                                <span>
                                                    <asp:Label ID="lblStartPaidSubDate" runat="server" CssClass="lbl" Text="วันที่(สอบ/ต่ออายุ)เริ่ม"></asp:Label>
                                                    : </span>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtStartPaidSubDate" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtStartPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                    ImageAlign="Bottom" runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CEStartPaidSubDate" PopupButtonID="imgPopup_txtStartPaidSubDate"
                                                    runat="server" TargetControlID="txtStartPaidSubDate" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblEndPaidSubDate" runat="server" CssClass="lbl" Text="วันที่(สอบ/ต่ออายุ)สิ้นสุด"></asp:Label>
                                                <span>:</span>&nbsp;
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtEndPaidSubDate" runat="server"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtEndPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                    ImageAlign="Bottom" runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CEEndPaidSubDate" PopupButtonID="imgPopup_txtEndPaidSubDate"
                                                    runat="server" TargetControlID="txtEndPaidSubDate" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblTypePayment" runat="server" Text="ประเภทใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlTypePayment" runat="server" CssClass="ddl">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="Req_ddlTypePayment" runat="server" Display="Dynamic"
                                                    ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlTypePayment" ValidationGroup="req1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblComCode" runat="server" Text="บริษัท"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox runat="server" ID="txtCompany" CssClass="txt" Width="60%"></asp:TextBox>
                                                <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txtCompany"
                                                    ServiceMethod="GetCompanyCodeAsCompanyTname" ServicePath="~/Service/AutoComplete.asmx"
                                                    MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
                                                    ShowOnlyCurrentWordInCompletionListItem="true" FirstRowSelected="true" OnClientItemSelected="OnContactSelected">
                                                </ajaxToolkit:AutoCompleteExtender>
                                                <asp:HiddenField ID="hdf" runat="server" OnValueChanged="hdf_ValueChanged" />
                                                <asp:TextBox ID="txtID" runat="server" Width="400px" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="75px" CssClass="btn"
                                                    ValidationGroup="req1" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click"
                                                    Width="75px" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%--</fieldset>--%>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div class="box_body_content">
                            <div class="box_t_h">
                            </div>
                            <div style="float: right; padding-right: 20px;">
                                <asp:ImageButton ID="btnExportExcel" Visible="false" runat="server" ImageUrl="~/image/exel.png"
                                    OnClick="btnExportExcel_Click" />
                                <br />
                            </div>
                            <div class="box_textbox_bg_table" runat="server" id="blueDiv">
                                <div class="box_texbox_add_box">
                                    <table width="100%">
                                        <div id="divGv1" runat="server">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvPayment" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                        GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="false" OnRowDataBound="gvPayment_RowDataBound">
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
                                                                    <asp:CheckBox ID="Checkall" runat="server" AutoPostBack="true" OnCheckedChanged="Checkall_CheckedChanged" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectPayment_CheckedChanged" />
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
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUploadGroupNo" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หน่วยงานที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUPLOAD_BY_SESSION" runat="server" Text='<%# Bind("UPLOAD_BY_SESSION") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentTypeNameGv" runat="server" Text='<%# Bind("PAYMENT_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPerson" runat="server" Text='<%# Bind("CountPerson") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestingDateGv" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestingNoGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="EXAM_PLACE_CODE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamPlaceCodeGv" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplView" runat="server" OnClick="hplViewExam_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
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
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Button ID="btnPreviousGvPayment" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvPayment_Click" />
                                                                        <asp:TextBox ID="txtNumberGvPayment" runat="server" Width="15px" Visible="false"
                                                                            Enabled="false" Text="1"></asp:TextBox>
                                                                        <asp:Label ID="lblParaPage1" runat="server" Text="/" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblTotalPage1" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Button ID="btnNextGvPayment" runat="server" Text=">" Visible="false" OnClick="btnNextGvPayment_Click"
                                                                            Width="20px" />
                                                                        &nbsp;&nbsp;
                                                                        <asp:Label ID="lblHeadInputMaxrow1" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                        <asp:TextBox ID="txtInputMaxrow1" runat="server" Visible="false" Width="5%" onpaste="return false;"
                                                                            ondrop="return false;" onkeypress="return runScript(event)" MaxLength="2" AutoComplete="off"
                                                                            Style="text-align: center"></asp:TextBox>
                                                                        &nbsp;
                                                                        <asp:Button ID="btngo1" runat="server" Text="Go" OnClick="btnSearch_Click" Visible="false" />
                                                                    </td>
                                                                    <td style="width: 20%; text-align: right">
                                                                        <div runat="server" id="div_total1">
                                                                            <asp:Label ID="lblHeadTotal1" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblTotalrecord1" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblEndTotal1" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblSubHeadExam" runat="server" Text="เลขที่นำส่ง" Visible="false"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblDisplayNumberSubHeadExam" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <div id="DivExam" runat="server">
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:GridView ID="GvSubExam" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                            GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                            ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="false">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <HeaderStyle BorderStyle="None" />
                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center">
                                                                    ไม่พบข้อมูล...</div>
                                                            </EmptyDataTemplate>
                                                            <PagerStyle HorizontalAlign="Left" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPaymentTypeNameGv" runat="server" Text='<%# Bind("PAYMENT_TYPE_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="25%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIDCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FIRST_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTestingDateGv" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTestingNoGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="APPLICANT_CODE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApplicantCodeGv" runat="server" Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="EXAM_PLACE_CODE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExamPlaceCodeGv" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="licenseC" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="1%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllicenseC" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="compCode" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="1%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblcompCode" runat="server" Text='<%# Bind("INSUR_COMP_CODE") %>'></asp:Label>
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
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:Button ID="btnPreviousGvSubExam" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSubExam_Click" />
                                                                            <asp:TextBox ID="txtNumberGvSubExam" runat="server" Width="15px" Visible="false"
                                                                                Enabled="false" Text="1"></asp:TextBox>
                                                                            <asp:Label ID="lblParaPage3" runat="server" Text="/" Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblTotalPage3" runat="server" Visible="false"></asp:Label>
                                                                            <asp:Button ID="btnNextGvSubExam3" runat="server" Text=">" Visible="false" OnClick="btnNextGvSubExam3_Click"
                                                                                Width="20px" />
                                                                            &nbsp;&nbsp;
                                                                            <asp:Label ID="lblHeadInputMaxrow3" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                            <asp:TextBox ID="txtInputMaxrow3" runat="server" Visible="false" Width="5%" onpaste="return false;"
                                                                                ondrop="return false;" onkeypress="return runScript(event)" MaxLength="2" AutoComplete="off"
                                                                                Style="text-align: center"></asp:TextBox>
                                                                            &nbsp;
                                                                            <asp:Button ID="btngo3" runat="server" Text="Go" OnClick="btngo3_Click" Visible="false" />
                                                                        </td>
                                                                        <td style="width: 20%; text-align: right">
                                                                            <div runat="server" id="div1">
                                                                                <asp:Label ID="lblHeadTotal3" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                                <asp:Label ID="lblTotalrecord3" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                                <asp:Label ID="lblEndTotal3" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </div>
                                        </div>
                                        <div id="divGv2" runat="server">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvLicenseNumber" runat="server" CssClass="datatable" CellSpacing="-1"
                                                        ShowHeaderWhenEmpty="True" GridLines="None" BorderWidth="1" Width="100%" EnableTheming="True"
                                                        AutoGenerateColumns="False" Visible="false" OnRowDataBound="gvLicenseNumber_RowDataBound">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chkAll_CheckedChanged"
                                                                        AutoPostBack="true" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged"
                                                                        AutoPostBack="true" />
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
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUploadGroupNo" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หน่วยงานที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUPLOAD_BY_SESSION" runat="server" Text='<%# Bind("UPLOAD_BY_SESSION") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="22%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPetitionType" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="22%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLots" runat="server" Text='<%# Bind("LOTS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplView" runat="server" OnClick="hplViewLicense_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
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
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Button ID="btnPreviousGvLicenseNumber" runat="server" Text="<" Visible="false"
                                                                            OnClick="btnPreviousGvLicenseNumber_Click" />
                                                                        <asp:TextBox ID="txtNumberGvLicenseNumber" runat="server" Width="15px" Visible="false"
                                                                            Text="1" Enabled="false"></asp:TextBox>
                                                                        <asp:Label ID="lblParaPage2" runat="server" Text="/" Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblTotalPage2" runat="server" Visible="false"></asp:Label>
                                                                        <asp:Button ID="btnNextGvLicenseNumber" runat="server" Text=">" Visible="false" OnClick="btnNextGvLicenseNumber_Click" />
                                                                        &nbsp;&nbsp;
                                                                        <asp:Label ID="lblHeadInputMaxrow2" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                                        <asp:TextBox ID="txtInputMaxrow2" runat="server" Visible="false" onpaste="return false;"
                                                                            ondrop="return false;" Width="5%" onkeypress="return runScript(event)" MaxLength="2"
                                                                            AutoComplete="off" Style="text-align: center"></asp:TextBox>
                                                                        &nbsp;
                                                                        <asp:Button ID="btngo2" runat="server" Text="Go" OnClick="btnSearch_Click" Visible="false" />
                                                                    </td>
                                                                    <td style="width: 20%; text-align: right">
                                                                        <div runat="server" id="div_total2">
                                                                            <asp:Label ID="lblHeadTotal2" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblTotalrecord2" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                            <asp:Label ID="lblEndTotal2" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblSubheadLicense" runat="server" Text="เลขที่นำส่ง" Visible="false"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblDisplayUpload" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <div id="SubLicense" runat="server">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvSubLicenseNumber" runat="server" CssClass="datatable" CellSpacing="-1"
                                                            ShowHeaderWhenEmpty="True" GridLines="None" BorderWidth="1" Width="100%" EnableTheming="True"
                                                            AutoGenerateColumns="False" Visible="false">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Left" />
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center">
                                                                    ไม่พบข้อมูล...</div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เลขใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="22%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLicenseNoGv" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ครั้งที่ต่ออายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRenewTimeGv" runat="server" Text='<%# Bind("renew_times") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่ต่ออายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRenewDateGv" runat="server" Text='<%# Bind("RENEW_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่หมดอายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("EXPIRE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFirstNameRenewGv" runat="server" Text='<%# Bind("T_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLastNameRenewGv" runat="server" Text='<%# Bind("T_LAST") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="uploadG" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="1%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbluploadG" runat="server" Text='<%# Bind("upload_group_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="seqNo" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="1%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblseqNo" runat="server" Text='<%# Bind("seq_no") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 80%">
                                                            <tr>
                                                                <td align="left">
                                                                    <asp:Button ID="btnPreviousGvSubLicense" runat="server" OnClick="btnPreviousGvSubLicense_Click"
                                                                        Text="&lt;" Visible="false" />
                                                                    <asp:TextBox ID="txtNumberGvSublicense" runat="server" Enabled="false" Text="1" Visible="false"
                                                                        Width="15px"></asp:TextBox>
                                                                    <asp:Label ID="lblParaPage4" runat="server" Text="/" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lblTotalPage4" runat="server" Visible="false"></asp:Label>
                                                                    <asp:Button ID="btnNextGvSubLicense" runat="server" OnClick="btnNextGvSubLicense_Click"
                                                                        Text="&gt;" Visible="false" Width="20px" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="lblHeadInputMaxrow4" runat="server" Text="จำนวนรายการต่อหน้า" Visible="false"></asp:Label>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="txtInputMaxrow4" runat="server" AutoComplete="off" MaxLength="2"
                                                                        ondrop="return false;" onkeypress="return runScript(event)" onpaste="return false;"
                                                                        Style="text-align: center" Visible="false" Width="5%"></asp:TextBox>
                                                                    &nbsp;
                                                                    <asp:Button ID="btngo4" runat="server" OnClick="btngo4_Click" Text="Go" Visible="false" />
                                                                </td>
                                                                <td style="width: 20%; text-align: right">
                                                                    <asp:Label ID="lblHeadTotal4" runat="server" Text="จำนวน" Visible="false"></asp:Label>
                                                                    &nbsp;
                                                                    <asp:Label ID="lblTotalrecord4" runat="server" Style="font-weight: bold" Visible="false"></asp:Label>
                                                                    &nbsp;
                                                                    <asp:Label ID="lblEndTotal4" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            </tr>
                                        </div>
                                    </table>
                                    <table align="center" style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;" colspan="2">
                                                <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" Visible="false"
                                                    OnClick="btnOk_Click" />
                                                <br />
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="UploadgroupNoPage" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnS1" runat="server" Style="display: none" />
                                                <ajaxToolkit:ModalPopupExtender ID="MPDetail" runat="server" TargetControlID="btnS1"
                                                    PopupControlID="pnlValidateprop" BackgroundCssClass="modalbackground" CancelControlID="btnCloseProp">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <asp:Panel ID="pnlValidateprop" Style="display: inherit" runat="server" CssClass="modalpopup"
                                                    Width="800px">
                                                    <div id="remark" runat="server" visible="false">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 10%">
                                                                    <asp:Label ID="lblremark1A" runat="server" Text="หมายเหตุ :"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblremark1B" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;* การเรียงลำดับใบสั่งจ่ายนั้นเป็นส่วนหนึ่งของการเรียงลำดับเพื่อตัดยอดในการชำระเงิน กรณีที่ชำระเงินไม่เต็มจำนวนตามใบสั่งจ่าย"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblremark2B" runat="server" Text="&nbsp; ** กรุณาเรียงลำดับของรายการของการตรวจสอบการชำระเงินเพื่อประโยชน์ในการชำระค่าธรรมเนียมของตัวท่านเอง"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblremark3B" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   โดยเรียงลำดับที่1 เป็นลำดับที่ได้ทำการตรวจสอบการชำระเงินจากธนาคารก่อน ตามลำดับของรายการที่ได้เลือกไว้"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblremark4B" runat="server" Text="***   การเรียงลำดับของใบสั่งจ่ายนั้น ไม่มีผลต่อวันหมดอายุของใบสั่งจ่าย และยอดเงินรวมในใบสั่งจ่าย"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="width: 100%;">
                                                        <div class="box_body_content">
                                                            <div class="box_t_h" style="width: 50%;">
                                                                ::จัดเรียงลำดับการออกใบสั่งจ่ายย่อย
                                                            </div>
                                                            <div class="box_textbox_bg_table">
                                                                <div class="box_texbox_add_box">
                                                                    <asp:GridView ID="GVExamPopup" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                                        GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                        ShowHeaderWhenEmpty="True" AllowSorting="True" Visible="false" OnRowDataBound="GVPopupLicense_RowDataBound"
                                                                        OnRowCommand="GVExamPopup__RowCommand">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                        <EmptyDataTemplate>
                                                                            <div style="text-align: center">
                                                                                ไม่พบข้อมูล...</div>
                                                                        </EmptyDataTemplate>
                                                                        <PagerStyle HorizontalAlign="Left" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="เลขที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUploadGroupNo" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="หน่วยงานที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUPLOAD_BY_SESSION" runat="server" Text='<%# Bind("UPLOAD_BY_SESSION") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPETITION_TYPE_NAME" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPerson" runat="server" Text='<%# Bind("CountPerson") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTestingDateGv" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTestingNoGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="EXAM_PLACE_CODE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="10%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblExamPlaceCodeGv" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
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
                                                                    <asp:GridView ID="GVPopupLicense" runat="server" CssClass="datatable" CellSpacing="-1"
                                                                        ShowHeaderWhenEmpty="True" GridLines="None" BorderWidth="1" Width="100%" EnableTheming="True"
                                                                        AutoGenerateColumns="False" Visible="false" OnRowDataBound="GVPopupLicense_RowDataBound"
                                                                        OnRowCommand="GVPopupLicense__RowCommand">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <PagerStyle HorizontalAlign="Left" />
                                                                        <EmptyDataTemplate>
                                                                            <div style="text-align: center">
                                                                                ไม่พบข้อมูล...</div>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="เลขที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUploadGroupNo" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="หน่วยงานที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUPLOAD_BY_SESSION" runat="server" Text='<%# Bind("UPLOAD_BY_SESSION") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="22%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPetitionType" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="22%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLicenseType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLots" runat="server" Text='<%# Bind("LOTS") %>'></asp:Label>
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
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="text-align: center;" align="center">
                                                        <div>
                                                            <table width="100%">
                                                                <tr align="center">
                                                                    <td>
                                                                        <br />
                                                                        <asp:Button ID="btnSavePopup" runat="server" Text="ออกใบสั่งจ่ายย่อย" CssClass="btn"
                                                                            OnClick="btnSavePopup_Click" />
                                                                        &nbsp;
                                                                        <asp:Button runat="server" ID="btnCloseProp" Text="ปิด" CssClass="btn" />
                                                                    </td>
                                                                </tr>
                                                                <caption>
                                                                    &nbsp;
                                                                    <br />
                                                                </caption>
                                                            </table>
                                                        </div>
                                                        <div>
                                                            <br />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <%--                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess2" runat="server" />--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="btnOk" />
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

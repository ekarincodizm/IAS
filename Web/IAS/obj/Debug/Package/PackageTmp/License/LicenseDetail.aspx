<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="LicenseDetail.aspx.cs" Inherits="IAS.License.LicenseDetail" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/ucLicenseDetail.ascx" TagName="UCLicenseDetail" TagPrefix="uc4" %>
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

        function showload() {
            document.getElementById('ucLoad_Image1').src = '../Images/newloading23.gif';
            if (document.getElementById('ucLoad_Image1').src != null) {
                $find('ucLoad_mpeProgress').show();
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .datepicker
        {
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลการขอรับใบอนุญาต" BorderStyle="None" ID="lblTheLicensee" runat="server" />
            </div>
        </div>
        <br />
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server" Visible="false" Width="100%">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblLicenseNumber" runat="server" Text="เลขที่อนุญาต" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="txt" MaxLength="10" onkeypress="return runScript(event)"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblLicenseType" runat="server" Text="ประเภทใบอนุญาต" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 20%;" rowspan="2">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblApproveDate" runat="server" Text="ตั้งแต่วันที่" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtstartDate" runat="server" onKeyPress="return false;" onKeyDown="return false"
                                                        Width="128px"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtstartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CLDtxtstartDate" PopupButtonID="imgPopup_txtstartDate"
                                                        runat="server" TargetControlID="txtstartDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <%--    <asp:ImageButton ID="ibtClearStartPaidDate" runat="server" Height="15px" ImageUrl="~/Images/clear_button.png"
                                                        OnClick="ibtClearStartPaidDate_Click" />--%>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblExpireDate" runat="server" Text="ถึงวันที่" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txttoDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txttoDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CLDtxttoDate" PopupButtonID="imgPopup_txttoDate"
                                                        runat="server" TargetControlID="txttoDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <%--    <asp:ImageButton ID="ibtClearEndPaidDate" runat="server" Height="15px" ImageUrl="~/Images/clear_button.png"
                                                        OnClick="ibtClearEndPaidDate_Click" />--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <%--<uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....</ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                </td>
                            </tr>
                        </table>
                        <div class="box_body_content" id="divDetail" runat="server">
                            <div class="box_t_h">
                            </div>
                            <div style="float: right; padding-right: 50px;">
                                <asp:ImageButton ID="btnExportExcel" Visible="false" runat="server" ImageUrl="~/image/exel.png"
                                    OnClick="btnExportExcel_Click" />
                            </div>
                            <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:GridView ID="gvSearch" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                    GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                    AutoGenerateColumns="False" OnRowDataBound="gvSearch_RowDataBound">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                            ไม่พบข้อมูล</div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <%-- <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOOO" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="ลำดับที่">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRUN_NO" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="td-center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("NAMES") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทคำขอ" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypePettiotnGv" runat="server" Text='<%# Bind("PETITION_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeLicenseGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="เลขที่ใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLicenseNumberGv" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่อนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblApproveDateGv" runat="server" Text='<%# Bind("LICENSE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันหมดอายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("LICENSE_EXPIRE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--          <asp:TemplateField HeaderText="PETITION_TYPE_CODE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPetitionTypeCodeGv" runat="server" Text='<%# Bind("PETITION_TYPE_CODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="สถานะอนุมัติเอกสาร" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("APPROVED") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ผลการตรวจสอบเอกสาร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusName" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รายละเอียด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
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
                                                            <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                            <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1"
                                                                Enabled="false"></asp:TextBox>
                                                            <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                            <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="lblHeadInputMaxrow" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                            <asp:TextBox ID="txtInputMaxrow" runat="server" AutoComplete="Off" Visible="false"
                                                                Width="5%" onkeypress="return runScript(event)" MaxLength="4" Style="text-align: center"
                                                                ondrop="return false;" onpaste="return false;"></asp:TextBox>
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
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <uc4:UCLicenseDetail ID="ucLicenseDetail" runat="server" />
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

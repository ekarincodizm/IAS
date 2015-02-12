<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="LicenseDetail7.aspx.cs" Inherits="IAS.License.LicenseDetail7" %>

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลการขอรับใบอนุญาต" BorderStyle="None" ID="lblDetailObtainLicense"
                    runat="server" />
            </div>
        </div>
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
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblAllowNumber" runat="server" CssClass="lbl" Text="เลขที่อนุญาต"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtAllowNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblTypeLicense" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:DropDownList ID="ddlTypeLicense" runat="server" CssClass="ddl">
                                                    </asp:DropDownList>
                                                </td>
                                                <td rowspan="2">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblStartPaidSubDate" runat="server" Text="วันที่อนุญาต(เริ่ม)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
                                                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CLDtxtStartDate" PopupButtonID="imgPopup_txtStartDate"
                                                        runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblEndPaidSubDate" runat="server" Text="วันที่อนุญาต(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 30%;">
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
                            <asp:Panel ID="PnlGridView" runat="server" Visible="false">
                                <div class="box_body_content">
                                    <div class="box_t_h">
                                    </div>
                                    <div class="box_textbox_bg_table">
                                        <div class="box_texbox_add_box">
                                            <table width="100%">
                                                <tr>
                                                    <td style="text-align: center;">
                                                        <asp:GridView ID="gvLicense" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                            GridLines="None" BorderWidth="1" ShowHeaderWhenEmpty="True" EnableTheming="True"
                                                            AutoGenerateColumns="False" AllowSorting="True">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Left" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNOOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เลขที่อนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLicenseDetailGv" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่อนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLicenseDateGv" runat="server" Text='<%# Bind("DATE_LICENSE_ACT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่หมดอายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="15%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("EXPIRE_DATE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="20%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeLicenseGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ชื่อ-นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFirstLastNameGv" runat="server" Text='<%# Bind("FLNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="COMP_ABBR_NAMET" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCompNameGv" runat="server" Text='<%# Bind("COMP_ABBR_NAMET") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="4">
                                                        <asp:Button ID="btnApplicantFile" runat="server" Text="ไฟล์ผู้ขอรับใบอนุญาต" CssClass="btn"
                                                            Width="161px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

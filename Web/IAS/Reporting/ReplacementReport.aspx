<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ReplacementReport.aspx.cs" Inherits="IAS.Reporting.ReplacementReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function OpenPopupViewer(LicenseType, CompCode, Replacement, DateStart, DateEnd) {
            var qstr = "../Reporting/ReplacementReportViewer.aspx?LicenseType=" + LicenseType + "&CompCode=" + CompCode + "&Replacement=" + Replacement + "&DateStart=" + DateStart + "&DateEnd=" + DateEnd;
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                รายงานการออกใบแทน</div>
        </div>
        <div>
            <asp:Panel ID="pnlMain" runat="server">
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="กรุณาเลือก" /></div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <asp:UpdatePanel ID="update" runat="server">
                                <ContentTemplate>
                                    <table width="99%">
                                        <tr>
                                            <td align="right">
                                                ประเภทใบอนุญาต :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                บริษัท :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="ddl" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td rowspan="3">
                                                <asp:Button ID="btnView" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnView_Click" /><br />
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                สถานะใบแทน :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlReplacement" runat="server" CssClass="ddl">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                วันที่ออกใบอนุญาต :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateStart" runat="server" CssClass="txt" onkeypress="return false;"
                                                    onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtDateStart" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CEDateStart" PopupButtonID="imgPopup_txtDateStart"
                                                    runat="server" TargetControlID="txtDateStart">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                ถึง :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateEnd" runat="server" CssClass="txt" onkeypress="return false;"
                                                    onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtDateEnd" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CEDateEnd" runat="server" PopupButtonID="imgPopup_txtDateEnd"
                                                    TargetControlID="txtDateEnd">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Content>

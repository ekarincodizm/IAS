<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="LicenseStatisticsReport.aspx.cs" Inherits="IAS.Reporting.LicenseStatisticsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenPopupViewer(LicenseType, DateStart1, DateEnd1, DateStart2, DateEnd2) {
            var qstr = "../Reporting/LicenseStatisticsReportViewer.aspx?LicenseType=" + LicenseType + "&DateStart1=" + DateStart1 + "&DateEnd1=" + DateEnd1 + "&DateStart2=" + DateStart2 + "&DateEnd2=" + DateEnd2;
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
        }
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        
    }

    function OpenPopupSingle(para) {

            var qstr = "../Reporting/LicenseStatisticsReportViewer.aspx?";

            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                สถิติการออกใบอนุญาตคนกลางประกันภัย</div>
        </div>
        <div>
            <asp:Panel ID="pnlMain" runat="server">
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="กรุณาเลือก" /></div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table width="99%">
                                <tr>
                                    <td align="right">
                                        ประเภทใบอนุญาต :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <%--<asp:DropDownList ID="ddlCompany" runat="server" CssClass="ddl" Width="200px"></asp:DropDownList>--%>
                                    </td>
                                    <td rowspan="3">
                                        <asp:Button ID="btnView" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnView_Click" /><br />
                                        <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        วันที่ออกใบอนุญาต ช่วงที่ 1 :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateStart1" runat="server" CssClass="txt" onkeypress="return false;"
                                            onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtDateStart1" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                            runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CEDateStart" PopupButtonID="imgPopup_txtDateStart1"
                                            runat="server" TargetControlID="txtDateStart1">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td align="right">
                                        ถึง :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateEnd1" runat="server" CssClass="txt" onkeypress="return false;"
                                            onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtDateEnd1" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                            runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CEDateEnd" PopupButtonID="imgPopup_txtDateEnd1"
                                            runat="server" TargetControlID="txtDateEnd1">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        วันที่ออกใบอนุญาต ช่วงที่ 2 :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateStart2" runat="server" CssClass="txt" onkeypress="return false;"
                                            onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtDateStart2" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                            runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup_txtDateStart2"
                                            runat="server" TargetControlID="txtDateStart2">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td align="right">
                                        ถึง :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateEnd2" runat="server" CssClass="txt" onkeypress="return false;"
                                            onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtDateEnd2" ImageUrl="~/Images/calendar.gif"
                                            ImageAlign="Bottom" runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup_txtDateEnd2" runat="server" TargetControlID="txtDateEnd2">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="CheckFileSizeReport.aspx.cs" Inherits="IAS.Reporting.CheckFileSizeReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function OpenPopupViewer(TypePay, DateStart, DateEnd) {
            var qstr = "../Reporting/CheckFileSizeReportViewer.aspx?TypePay=" + TypePay + "&DateStart=" + DateStart + "&DateEnd=" + DateEnd;
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                รายงานแสดงขนาดไฟล์ใบเสร็จ</div>
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
                                        <%--<tr>
                                            <td align="right">ประเภทใบอนุญาต : </td>
                                            <td><asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl"></asp:DropDownList></td>
                                            <td align="right">บริษัท : </td>
                                            <td><asp:DropDownList ID="ddlCompany" runat="server" CssClass="ddl" Width="200px"></asp:DropDownList></td>
                                            <td rowspan="2">
                                                <asp:Button ID="btnView" runat="server" Text="ค้นหา" CssClass="btn" 
                                                    onclick="btnView_Click" /><br />
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                                    onclick="btnCancel_Click" />
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblTypePay" runat="server" Text="ประเภทการชำระ" CssClass="lbl"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:DropDownList ID="ddlTypePay" runat="server" CssClass="ddl">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                วันที่สร้างใบเสร็จ(เริ่ม) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateStart" runat="server" CssClass="txt" onkeypress="return false;"
                                                    onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtDateStart" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CEDateStart" PopupButtonID="imgPopup_txtDateStart"
                                                    runat="server" TargetControlID="txtDateStart" Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td align="right">
                                                วันที่สร้างใบเสร็จ(สิ้นสุด) :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateEnd" runat="server" CssClass="txt" onkeypress="return false;"
                                                    onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtDateEnd" ImageUrl="~/Images/calendar.gif"
                                                    ImageAlign="Bottom" runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CEDateEnd" PopupButtonID="imgPopup_txtDateEnd" runat="server" TargetControlID="txtDateEnd"
                                                    Format="dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td colspan="2" align="right">
                                                <asp:Button ID="btnView" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnView_Click" />&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
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

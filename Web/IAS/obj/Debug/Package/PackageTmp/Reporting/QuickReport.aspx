<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="QuickReport.aspx.cs" Inherits="IAS.Reporting.QuickReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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

        function OpenPopupViewer(PetitionType, DateStart, DateEnd, CompCode, Days) {
            var qstr = "../Reporting/QuickReportViewer.aspx?PetitionType=" + PetitionType + "&DateStart=" + DateStart + "&DateEnd=" + DateEnd + "&CompCode=" + CompCode + "&Days=" + Days;
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                รายงานกิจกรรมที่ต้องเร่งดำเนินการให้กับทาง นายหน้า/ตัวแทน</div>
        </div>
        <div>
            <asp:Panel ID="pnlMain" runat="server">
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหา" /></div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <asp:UpdatePanel ID="update" runat="server">
                                <ContentTemplate>
                                    <table width="99%">
                                        <tr>
                                            <td align="right" width="20%">
                                                ประเภทการยื่นขอรับความเห็นชอบ
                                            </td>
                                            <td colspan="5">
                                                <asp:DropDownList ID="ddlPetitionType" runat="server" CssClass="ddl" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlPetitionType_SelectedIndexChanged">
                                                    <asp:ListItem>---- เลือก ----</asp:ListItem>
                                                    <asp:ListItem>ต่ออายุใบอนุญาต 1 ปี</asp:ListItem>
                                                    <asp:ListItem>ต่ออายุใบอนุญาต 5 ปี</asp:ListItem>
                                                    <asp:ListItem>ใบแทนใบอนุญาต</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td rowspan="3" width="10%">
                                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                วันที่ยื่นรายการ(เริ่ม)
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
                                            <td align="right" width="30%">
                                                วันที่ยื่นรายการ(สิ้นสุด)
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateEnd" runat="server" CssClass="txt" Width="130px" onkeypress="return false;"
                                                    onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtDateEnd" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="CEDateEnd" PopupButtonID="imgPopup_txtDateEnd"
                                                    runat="server" TargetControlID="txtDateEnd">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                รหัสบริษัทผู้ยื่นคำร้อง
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCompCode" runat="server" onkeypress="return runScript(event)"
                                                    onpaste="return false;" AutoComplete="Off" ondrop="return false;"></asp:TextBox>
                                            </td>
                                            <td align="right" runat="server" id="tdDay1" visible="false">
                                                จำนวนระยะเวลาก่อนหมดอายุใบอนุญาต
                                            </td>
                                            <td runat="server" id="tdDay2" visible="false">
                                                <asp:TextBox ID="txtDays" Text="5" runat="server" CssClass="txt" Width="100px" MaxLength="3"
                                                    onkeypress="return runScript(event)" onpaste="return false;" AutoComplete="Off"
                                                    ondrop="return false;"></asp:TextBox>
                                                วัน
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

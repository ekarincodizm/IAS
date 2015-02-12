<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="RegisterLicenseStaticRatio.aspx.cs" Inherits="IAS.Reporting.RegisterLicenseStaticRatio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenPopup() {


            var ddlLicenseType = document.getElementById('<%=ddlLicenseType.ClientID %>');
            var startdate = document.getElementById('<%=txtStartDate.ClientID %>');
            var enddate = document.getElementById('<%=txtEndDate.ClientID %>');
            var startdate2 = document.getElementById('<%=txtStartDate2.ClientID %>');
            var enddate2 = document.getElementById('<%=txtEndDate2.ClientID %>');
            var LicenseType_Select = ddlLicenseType.options[ddlLicenseType.selectedIndex].value;
            var qstr = "../Reporting/PopRegisterLicenseStaticRatio.aspx?startdate=" + startdate.value + "&enddate=" + enddate.value + "&startdate2=" + startdate2.value + "&enddate2=" + enddate2.value + "&licensetype=" + LicenseType_Select;
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                รายงานสถิติการสมัครสอบ</div>
        </div>
        <div style="clear: left;">
        </div>
        <div>
            <div class="box_body_content">
                <div class="box_t_h">
                    :: ค้นหา</div>
                <div class="box_textbox_bgadd">
                    <div class="box_texbox_add_box">
                        <table width="100%" cellpadding="5" cellspacing="5">
                            <tr>
                                <td align="right">
                                    <span class="lbl">ประเภทใบอนุญาต :</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl" DataTextField="Name"
                                        DataValueField="Id">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                </td>
                                <td align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span class="lbl">วันที่สมัครสอบ ช่วงที่ 1 :</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                        runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="txtStartDate_CalendarExtender" PopupButtonID="imgPopup_txtStartDate"
                                        runat="server" Enabled="True" TargetControlID="txtStartDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="right">
                                    <span class="lbl">ถึง :</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                        runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="txtEndDate_CalendarExtender" PopupButtonID="imgPopup_txtEndDate"
                                        runat="server" Enabled="True" TargetControlID="txtEndDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span class="lbl">วันที่สมัครสอบ ช่วงที่ 2 :</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtStartDate2" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="imgPopup_txtStartDate2" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                        runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup_txtStartDate2"
                                        runat="server" Enabled="True" TargetControlID="txtStartDate2">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                                <td align="right">
                                    <span class="lbl">ถึง :</span>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEndDate2" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                    <asp:ImageButton ID="imgPopup_txtEndDate2" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                        runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup_txtEndDate2"
                                        runat="server" Enabled="True" TargetControlID="txtEndDate2">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">
                                    <input type="button" value="ค้นหา" class="btn" onclick="OpenPopup()" />
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Content>

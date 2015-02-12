<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="RegisterLicense.aspx.cs" Inherits="IAS.Reporting.RegisterLicense" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenPopup() {


            var ddlLicenseType = document.getElementById('<%=ddlLicenseType.ClientID %>');
            var ddlCompany = document.getElementById('<%=ddlCompany.ClientID %>');
            var startdate = document.getElementById('<%=txtStartDate.ClientID %>');
            var enddate = document.getElementById('<%=txtEndDate.ClientID %>');
            var LicenseType_Select = ddlLicenseType.options[ddlLicenseType.selectedIndex].value;
            var compay_select = ddlCompany.options[ddlCompany.selectedIndex].value;
            var qstr = "../Reporting/PopRegisterLicense.aspx?startdate=" + startdate.value + "&enddate=" + enddate.value + "&licensetype=" + LicenseType_Select + "&comcode=" + compay_select;
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
                รายงานการสมัครสอบเพื่อขอรับใบอนุญาต</div>
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
                                    <span class="lbl">บริษัท :</span>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="ddl" DataTextField="Name"
                                        DataValueField="Id">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <span class="lbl">วันที่สมัครสอบ :</span>
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
                                <td align="center" colspan="4">
                                    <input type="button" value="ค้นหา" class="btn" onclick="OpenPopup()" />
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" OnClick="btnCancel_Click"
                                        CssClass="btn" />
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

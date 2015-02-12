<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="TopCompanyMoveOut.aspx.cs" Inherits="IAS.Reporting.TopCompanyMoveOut" %>

<%@ Register Src="~/UserControl/UCModalError.ascx" TagPrefix="uc" TagName="UCModalError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">

        function OpenPopupViewer(LicenseType, DateStart, DateEnd, LicenseName) {
            var qstr = "../Reporting/TopCompanyMoveOutViewer.aspx?LicenseType=" + LicenseType + "&DateStart=" + DateStart + "&DateEnd=" + DateEnd + "&LicenseName=" + LicenseName;
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                5 อันดับตัวแทนย้ายออก</div>
        </div>
        <div>
            <asp:Panel ID="pnlMain" runat="server">
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหา" /></div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <asp:UpdatePanel ID="upnPanel" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table width="99%">
                                        <tr>
                                            <td>
                                                ประเภทใบอนุญาต
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLicenseType" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                วันที่
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateStart" runat="server" onkeypress="return false;" onpaste="return false;"
                                                    AutoComplete="Off" ondrop="return false;" CssClass="txt"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtDateStart" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="ceDateStart" PopupButtonID="imgPopup_txtDateStart"
                                                    runat="server" TargetControlID="txtDateStart">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                                วันที่สิ้นสุด
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDateEnd" runat="server" onkeypress="return false;" onpaste="return false;"
                                                    AutoComplete="Off" ondrop="return false;" CssClass="txt"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup_txtDateEnd" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                    runat="server" />
                                                <ajaxToolkit:CalendarExtender ID="ceDateEnd" PopupButtonID="imgPopup_txtDateEnd"
                                                    runat="server" TargetControlID="txtDateEnd">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnViewer" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnViewer_Click" /><br />
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <uc:UCModalError runat="server" ID="UCError" />
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

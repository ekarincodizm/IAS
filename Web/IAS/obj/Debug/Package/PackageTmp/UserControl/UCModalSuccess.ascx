<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCModalSuccess.ascx.cs"
    Inherits="IAS.UserControl.UCModalSuccess" %>

<asp:UpdatePanel ID="updateuc" runat="server">
<ContentTemplate>
<asp:Panel ID="pnlMessageModalSuccess" runat="server" CssClass="modalBackgroundSE">
    <table width="100%" style="border: Solid 2px #008000; width: 100%; height: 100%"
        cellpadding="0" cellspacing="0">
        <tr style="background-color: Green">
            <td style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger;
                font-family: Calibri" align="left">
                <asp:Label ID="lblTitleMessage" runat="server" Font-Names="Arial" Text="เรียบร้อย" />
            </td>
            <td style="color: White; font-weight: bold; padding: 3px; font-size: larger" align="right">
                <asp:LinkButton ID="lbtnClose" runat="server" EnableTheming="True" ForeColor="White">X</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="padding: 5px; font-family: Calibri">
                <img src="../Images/icon_Success.png" width="40px" height="40px" />
                <%--<div style="vertical-align: top;">
                    <%= ShowMessageSuccess %>
                </div>--%>
                <asp:UpdatePanel ID="udpMsgSuccess" runat="server"  UpdateMode="Always">
                    <ContentTemplate>
                    <div style="Color:Black;">
                        <%= ShowMessageSuccess %>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Button ID="btnClose" runat="server" CssClass="btn" Text="ปิด" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeMessageModalSuccess" runat="server" TargetControlID="pnlMessageModalSuccess"
    PopupControlID="pnlMessageModalSuccess" BackgroundCssClass="modalpopupMessageSE"
    DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>
</ContentTemplate>
</asp:UpdatePanel>

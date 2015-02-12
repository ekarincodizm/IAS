<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCModalError.ascx.cs"
    Inherits="IAS.UserControl.UCModalError" ViewStateMode="Enabled" %>

<asp:UpdatePanel ID="updateUc" runat="server">
<ContentTemplate>

<asp:Panel ID="pnlMessageModalError" runat="server" style="width:550px;" CssClass="modalBackgroundSE">
    <table width="100%" style="border: Solid 2px #ff0000; width: 100%; height: 100%"
        cellpadding="0" cellspacing="0">
        <tr style="background-color: Red">
            <td style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger;
                font-family: Calibri" align="left">
                <asp:Label ID="lblTitleMessage" runat="server" Font-Names="Arial" Text="ผิดพลาด" />
            </td>
            <td style="color: White; font-weight: bold; padding: 3px; font-size: larger" align="right">
                <asp:LinkButton ID="lbtnClose" runat="server" EnableTheming="True" ForeColor="White">X</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="padding: 5px; font-family: Calibri">
                <img src="../Images/ico_error.png" width="40px" height="40px" />
                <%--<div style="vertical-align: top;">
                    <%= ShowMessageError %>
                </div>--%>
                <asp:UpdatePanel ID="udpMsgError" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                    <div style="Color:Black; width:auto;">                  
                        <%= ShowMessageError %>
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
                <asp:Button ID="btnClose" runat="server" Text="ปิด" CssClass="btn" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeMessageModalError" runat="server" TargetControlID="pnlMessageModalError"
    PopupControlID="pnlMessageModalError" BackgroundCssClass="modalpopupMessageSE" DropShadow="false" CancelControlID="btnClose" />
<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>
</ContentTemplate>
</asp:UpdatePanel>
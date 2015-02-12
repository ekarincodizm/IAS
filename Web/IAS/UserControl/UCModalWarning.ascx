<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCModalWarning.ascx.cs"
    Inherits="IAS.UserControl.UCModalWarning" ViewStateMode="Enabled" %>
<asp:UpdatePanel ID="updateuc" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlMessageModalWarning" runat="server" CssClass="modalpopupMessage" ChildrenAsTriggers=false>
            <table width="100%" style="border: Solid 2px #0099FF; width: 100%; height: 100%"
                cellpadding="0" cellspacing="0">
                <tr style="background-color: #0099FF;">
                    <td colspan="4" style="height: 10%; color: White; font-weight: bold; padding: 3px;
                        font-size: larger; font-family: Calibri" align="center">
                        <asp:Label ID="lblTitle" runat="server" Text="แจ้งเตือน"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td width="20%">
                        &nbsp;
                    </td>
                    <td align="right" width="30px">
                     <asp:Image ID="Image2" Width="30px" runat="server" ImageUrl="~/Images/info.png"  />
                    </td>
                    <td align="left" width ="80%" style="background: none; border: none; font-family: Calibri; margin-left:0px; margin-right:0px;">
                        <asp:UpdatePanel ID="udpMsgWarning" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div style="color: Black;width:auto;">
                                   
                                    <%= ShowMessageWarning%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td width="20%">
                        &nbsp;
                    </td>
                </tr>
                <%--     </td>
                    <td width="20px" style="background: none; border: none;">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/info.png" />
                    </td>
                    <td style="background: none; border: none; font-family: Calibri;">
                        <asp:UpdatePanel ID="udpMsgWarning" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div style="color: Black; text-align:left;">
                                    <%= ShowMessageWarning%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>--%>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;">
                        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="ปิด" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mpeMessageModalWarning" runat="server" TargetControlID="pnlMessageModalWarning"
            PopupControlID="pnlMessageModalWarning" BackgroundCssClass="modalbackground"
            DropShadow="false" CancelControlID="btnNo" />
        <script type="text/javascript">
            function fnClickOK(sender, e) {
                __doPostBack(sender, e);
            }
        </script>
    </ContentTemplate>
</asp:UpdatePanel>

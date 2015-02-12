<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLicenseArgreement.ascx.cs" Inherits="IAS.UserControl.ucLicenseArgreement" %>
<%@ Register src="UCLoadingProgress.ascx" tagname="UCLoadingProgress" tagprefix="uc1" %>
 <style type="text/css">
    .popup{
        margin: 0 auto;
        display: block;
        position: static;
        z-index: -99 !important;
    }
</style>
<asp:UpdatePanel ID="udpArgreement" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <asp:Panel ID="pnlArgreement" runat="server" Visible="true" Width="100%" HorizontalAlign="Center" class="popup">
            <table width="100%">
                <tr>
                    <td>
                        <iframe id="iframAgree" runat="server" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:70%; height:500px;" allowTransparency="true"></iframe>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
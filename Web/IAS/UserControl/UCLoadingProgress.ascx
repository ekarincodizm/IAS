<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLoadingProgress.ascx.cs"
    Inherits="IAS.UserControl.UCLoadingProgress" %>
<style type="text/css">
    .modalBackground
    {
        background-color: #e6e6e6;
        filter: alpha(opacity=60);
        opacity: 0.60;
    }
    .PrProgress
    {
        display: block;
        position: absolute;
        width: 242px;
        height: 200px;
        left: 50%;
        top: 50%;
        margin-left: -121px;
        margin-top: -100px;
        padding: 2px 3px;
    }
    .modalPopup
{
   background-color: transparent;
   padding-top: 5px;
   padding-left: 5px;
   padding-right: 5px;
   z-index: 999999 !important;
}
.modalBackground
{
    background-color: #FCF8F8;
    filter: alpha(opacity=30);
    opacity: 0.7;
    z-index: 999998 !important;
    
}
</style>
<asp:Panel ID="pnlProgress" runat="server" CssClass="modalPopup">
 <%--   <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">--%>
  <%--  <ProgressTemplate>--%>
    <%--<div style="padding: 8px">
        <asp:Image ID="Image1" ImageUrl="~/Images/NewLoading2.gif"  runat="server" 
            Width="70px"  />
    </div>--%>
    <div align="center">
        <%--<asp:Image ID="Image2" ImageUrl="~/Images/ajax-loader.gif" runat="server"  />--%>
        <asp:Image ID="Image1" ImageUrl="~/Images/newloading23.gif" runat="server"  />
    </div>
    <%--</ProgressTemplate>--%>
    <%--</asp:UpdateProgress>--%>
</asp:Panel>
<ajaxToolkit:ModalPopupExtender ID="mpeProgress" runat="server" TargetControlID="pnlProgress"
    PopupControlID="pnlProgress" BackgroundCssClass="modalBackground" DropShadow="false" />
<script type="text/javascript">
    $(document).ready(function () {
        Sys.Net.WebRequestManager.add_invokingRequest(onInvoke);
        Sys.Net.WebRequestManager.add_completedRequest(onComplete);

        function onInvoke(sender, args) {
            if ($find('<%= mpeProgress.ClientID %>') != null) {
                $find('<%= mpeProgress.ClientID %>').show();
            }
           
        }

        function onComplete(sender, args) {
            if ($find('<%= mpeProgress.ClientID %>') != null) {
                $find('<%= mpeProgress.ClientID %>').hide();
            }
        }


        function pageUnload(sender, args) {

            Sys.Net.WebRequestManager.remove_invokingRequest(onInvoke);
            Sys.Net.WebRequestManager.remove_completedRequest(onComplete);
        }
    });
 

</script>

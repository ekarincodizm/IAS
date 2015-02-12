<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelPopupTabpanelTest.aspx.cs" Inherits="IAS.Mockup.ModelPopupTabpanelTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <form id="form1" runat="server">
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ajaxToolkit:ModalPopupExtender ID="PopupDetail" runat="server" CancelControlID="Close_PopUp"
                   TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                    Drag="true" BackgroundCssClass="modalbackground">
                </ajaxToolkit:ModalPopupExtender>

               
          <ContentTemplate>
           <asp:Panel ID="pnlModal" Style="display: block;" runat="server" CssClass="modalpopup" >


                </asp:Panel>
          </ContentTemplate>

          
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

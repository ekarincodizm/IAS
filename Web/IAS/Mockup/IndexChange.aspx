<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexChange.aspx.cs" Inherits="IAS.Mockup.IndexChange" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="ucModelError" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress" TagPrefix="ucLoading" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="ucSuccess" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../style/main.css" rel="stylesheet" type="text/css" />
    <link href="../style/menu.css" rel="stylesheet" type="text/css" />
    <link href="../style/content.css" rel="stylesheet" type="text/css" />
    <link href="../style/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <link href="../style/UCStyle.css" rel="stylesheet" type="text/css" />
    <link href="../style/PopupPageStyle.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.core.css"
        rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.theme.css"
        rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.datepicker.css"
        rel="stylesheet" type="text/css" />
    <link href="../Scripts/Scripts01042013/jquery/development-bundle/themes/base/ui.tabs.css"
        rel="stylesheet" type="text/css" />
    <%--    <script src="../Scripts/Scripts01042013/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="../Scripts/formatTextBox.js" type="text/javascript"></script>--%>
    <script src="../Scripts/formatTextBox.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/validate-custom-init.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <ajax:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true"
            LoadScriptsBeforeUI="true" EnablePageMethods="true" ScriptMode="Release" runat="server">
        </ajax:ToolkitScriptManager>
        <br />
        <asp:Label ID="lblGroupReqNo" runat="server" Text="GROUP_REQUEST_NO :"></asp:Label>
        <asp:TextBox ID="txtGroupReqNo" runat="server" Width="200px" Text="999999561000000359"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" 
            onclick="btnSearch_Click" />
        <br />
        <asp:GridView ID="gvHT" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" >
            <Columns>
                <asp:TemplateField HeaderText = "SEQ_OF_GROUP">
                        <ItemTemplate>
                            <asp:Label ID="lblSEQ_OF_GROUP" runat="server" Text='<%# Bind("SEQ_OF_GROUP") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "HEAD_REQUEST_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblHEAD_REQUEST_NO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText = "UPLOAD_GROUP_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblUPLOAD_GROUP_NO" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText = "GROUP_REQUEST_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblGROUP_REQUEST_NO" runat="server" Text='<%# Bind("GROUP_REQUEST_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText = "ID_CARD_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblID_CARD_NO" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="btnShow" runat="server" Text="Open Popup" Visible="false"
            onclick="btnShow_Click" />
        <ajax:ModalPopupExtender ID="modalMain" runat="server" TargetControlID="btnS" PopupControlID="pnlAMin" 
        BackgroundCssClass="modalbackground" CancelControlID="btnCancel"  >
        </ajax:ModalPopupExtender>
        <asp:Button ID="btnS" runat="server" style="display:none;" />
        <asp:Panel ID="pnlAMin" runat="server" CssClass="popup_container"  style="display:inherit;">
            <asp:GridView ID="gvHTpop" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="gvHTpop_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText = "SEQ_OF_GROUP">
                        <ItemTemplate>
                            <asp:Label ID="lblSEQ_OF_GROUP" runat="server" Text='<%# Bind("SEQ_OF_GROUP") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "HEAD_REQUEST_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblHEAD_REQUEST_NO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText = "UPLOAD_GROUP_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblUPLOAD_GROUP_NO" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText = "GROUP_REQUEST_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblGROUP_REQUEST_NO" runat="server" Text='<%# Bind("GROUP_REQUEST_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="จัดลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="20%" />
                        <ItemTemplate>
                            <div align="center">
                                <asp:LinkButton ID="lbtMoveUp" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Up" ToolTip="MoveUp" runat="server"><img src="../Images/moveup.png" width="16" height="16" /> </asp:LinkButton>
                                <asp:LinkButton ID="lbtMoveDown" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Down" ToolTip="MoveDown" runat="server"><img src="../Images/movedown.png" width="16" height="16"/> </asp:LinkButton>
                                <asp:LinkButton ID="lbtView" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="View" ToolTip="View Details" runat="server"><img src="../Images/view-icon.png" width="16" height="16" /> </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div align="center">
                <asp:Button ID="btnCancel" runat="server" Text="Close" />
            </div>
        </asp:Panel>
        <!--AG_IAS_SUBPAYMENT_D_T-->
        <ajax:ModalPopupExtender ID="modalMain2" runat="server" TargetControlID="btnS2" PopupControlID="pnlAMin2" 
        BackgroundCssClass="modalbackground" CancelControlID="btnCancel2"  >
        </ajax:ModalPopupExtender>
        <asp:Button ID="btnS2" runat="server" style="display:none;" />
        <asp:Panel ID="pnlAMin2" runat="server" CssClass="popup_container"  style="display:inherit;">
            <asp:GridView ID="gvDTpop" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="gvDTpop_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText = "SEQ_OF_SUBGROUP">
                        <ItemTemplate>
                            <asp:Label ID="lblSEQ_OF_SUBGROUP" runat="server" Text='<%# Bind("SEQ_OF_SUBGROUP") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "SEQ_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblSEQ_NO" runat="server" Text='<%# Bind("SEQ_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "HEAD_REQUEST_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblHEAD_REQUEST_NO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "PAYMENT_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblPAYMENT_NO" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "UPLOAD_GROUP_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblUPLOAD_GROUP_NO" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="จัดลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="20%" />
                        <ItemTemplate>
                            <div align="center">
                                <asp:LinkButton ID="lbtMoveUp" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Up" ToolTip="MoveUp" runat="server"><img src="../Images/moveup.png" width="16" height="16" /> </asp:LinkButton>
                                <asp:LinkButton ID="lbtMoveDown" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Down" ToolTip="MoveDown" runat="server"><img src="../Images/movedown.png" width="16" height="16"/> </asp:LinkButton>
                                <asp:LinkButton ID="lbtView" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="View" ToolTip="View Details" runat="server"><img src="../Images/view-icon.png" width="16" height="16" /> </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div align="center">
                <asp:Button ID="btnCancel2" runat="server" Text="Close" />
            </div>
        </asp:Panel>
        <!--AG_IAS_LICENSE_D-->
        <ajax:ModalPopupExtender ID="modalMain3" runat="server" TargetControlID="btnS3" PopupControlID="pnlAMin3" 
        BackgroundCssClass="modalbackground" CancelControlID="btnCancel3"  >
        </ajax:ModalPopupExtender>
        <asp:Button ID="btnS3" runat="server" style="display:none;" />
        <asp:Panel ID="pnlAMin3" runat="server" CssClass="popup_container"  style="display:inherit;">
            <asp:GridView ID="gvLicenseD" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowCommand="gvLicenseD_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText = "SEQ_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblSEQ_NO" runat="server" Text='<%# Bind("SEQ_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "HEAD_REQUEST_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblHEAD_REQUEST_NO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "LICENSE_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblLICENSE_NO" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText = "UPLOAD_GROUP_NO">
                        <ItemTemplate>
                            <asp:Label ID="lblUPLOAD_GROUP_NO" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText="จัดลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="20%" />
                        <ItemTemplate>
                            <div align="center">
                                <asp:LinkButton ID="lbtMoveUp" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Up" ToolTip="MoveUp" runat="server"><img src="../Images/moveup.png" width="16" height="16" /> </asp:LinkButton>
                                <asp:LinkButton ID="lbtMoveDown" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Down" ToolTip="MoveDown" runat="server"><img src="../Images/movedown.png" width="16" height="16"/> </asp:LinkButton>
                                <asp:LinkButton ID="lbtView" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="View" ToolTip="View Details" runat="server"><img src="../Images/view-icon.png" width="16" height="16" /> </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
            <div align="center">
                <asp:Button ID="btnCancel3" runat="server" Text="Close" />
            </div>
        </asp:Panel>

        <ucLoading:UCLoadingProgress ID="ucLoading" runat="server" />
        <ucModelError:UCModalError ID="ucError" runat="server" />
        <ucSuccess:UCModalSuccess ID="ucSuccess" runat="server" />
   </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

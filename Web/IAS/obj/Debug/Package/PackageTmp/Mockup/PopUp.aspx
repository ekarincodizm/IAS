<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUp.aspx.cs" Inherits="IAS.Mockup.PopUp" %>

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
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true"
        LoadScriptsBeforeUI="true" EnablePageMethods="true" ScriptMode="Release" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:Button ID="btnSortPopup" runat="server" Text="ShowPop" CssClass="btn" 
        Width="120px" onclick="btnSortPopup_Click" />
    <ajaxToolkit:ModalPopupExtender ID="mdlLicenseD" runat="server" TargetControlID="btnS" PopupControlID="pnlLicenseD" 
                        BackgroundCssClass="modalbackground" CancelControlID="btnCancel"  >
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Button ID="btnS" runat="server" style="display:none;" />
                        <asp:Panel ID="pnlLicenseD" runat="server" CssClass="modalpopup"  style="display:inherit;">
                        <asp:GridView ID="gvLicenseD" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"  >
                            <Columns>
                                <asp:TemplateField HeaderText = "ลำดับ" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "เลขที่นำส่ง">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGroupNo" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                        <asp:HiddenField ID="hdfGroupUpLoadNo" runat="server" Value = '<%#Eval("UPLOAD_GROUP_NO")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "เลขที่ใบสั่งจ่ายย่อย" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHeadRequestNo" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "ประเภทคำขอ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPetitionNo" runat="server" Text='<%# Bind("PETITION_TYPE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "จำนวนเงิน">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMoney" runat="server" Text='<%# Bind("FEES","{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานะเอกสาร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproveDoc" runat="server" Text='<%# Bind("APPROVED_DOC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่สร้างเอกสาร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTranDate" runat="server" Text='<%# Bind("TRAN_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="จัดลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <div align="center">
                                            <%--<asp:LinkButton ID="lbtMoveUp" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Up" ToolTip="MoveUp" runat="server"><img src="../Images/moveup.png" width="16" height="16" /> </asp:LinkButton>
                                            <asp:LinkButton ID="lbtMoveDown" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Down" ToolTip="MoveDown" runat="server"><img src="../Images/movedown.png" width="16" height="16"/> </asp:LinkButton>--%>
                                            <asp:LinkButton ID="LBUP" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Up" ><img src="../Images/arrowup.png" /></asp:LinkButton>
                                            <asp:LinkButton ID="LBDown" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Down"  ><img src="../Images/arrowdown.png" /></asp:LinkButton>
                                            <%--<asp:LinkButton ID="lbtView" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="View" ToolTip="View Details" runat="server"><img src="../Images/view-icon.png" width="16" height="16" /> </asp:LinkButton>--%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div align="center">
                            <asp:Button ID="btnPayment" runat="server" Text="Show newpop" CssClass="btn" 
                                Width="120px" onclick="btnPayment_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn" Width="120px" />
                        </div>
                        </asp:Panel>
                            <ajaxToolkit:ModalPopupExtender ID="mdlPop2" runat="server" TargetControlID="btnS2" PopupControlID="Panel2" 
                        BackgroundCssClass="modalbackground" CancelControlID="Button1"  >
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Button ID="btnS2" runat="server" style="display:none;" />
                        <asp:Panel ID="Panel2" runat="server" CssClass="modalpopup"  style="display:inherit; width:600px;" >
                            <asp:Label ID="lbl" runat="server" Text="dsdsdsdsd"></asp:Label><br />
                            <asp:Label ID="Label1" runat="server" Text="dsdsdsdsd"></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text="dsdsdsdsd"></asp:Label><br />
                            <asp:Label ID="Label3" runat="server" Text="dsdsdsdsd"></asp:Label><br />
                            <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn" Width="120px" />
                        </asp:Panel>
    </form>
</body>
</html>

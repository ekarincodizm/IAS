<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="CheckDocument.aspx.cs" Inherits="IAS.Setting.CheckDocument" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #Detail_UCModalSuccess1_pnlMessageModalSuccess, #Detail_UCModalError1_pnlMessageModalError
        {
            z-index: 11005 !important;
        }
        .modalpopupMessageSE
        {
            z-index: 11003 !important;
        }
        .popup_container
        {
            z-index: 11004 !important;
        }
        #Detail_panPop
        {
            z-index: 11001 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />
    <uc1:UCModalError ID="UCModalError1" runat="server" />
    <div class="box_content" align="center">
        <div class="box_content_header">
            <div class="box_content_header_text">
                ผู้ตรวจสอบเอกสาร
            </div>
        </div>
        <div style="clear: left;">
        </div>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="GvCheckDoc" runat="server" AutoGenerateColumns="false" Width="95%"
                    BorderWidth="1" CssClass="datatable" CellSpacing="-1" GridLines="None" OnRowDataBound="GvCheckDoc_RowDataBound">
                    <AlternatingRowStyle CssClass="altrow" />
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                                <div align="center">
                                    <%# Container.DataItemIndex+1 %>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสประเภทใบอนุญาต" Visible="false">
                            <ItemTemplate>
                                <div align="left">
                                    <asp:Label ID="lbldoccode" runat="server" Text='<%# Bind("APPROVE_DOC_TYPE") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                            <ItemTemplate>
                                <div align="left">
                                    <asp:Label ID="lbldocname" runat="server" Text='<%# Bind("APPROVE_DOC_NAME") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="250px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="กำหนดผู้ตรวจสอบเอกสาร">
                            <ItemTemplate>
                                <div align="center">
                                    <asp:Button ID="btnApplove" runat="server" Text="กำหนดผู้ตรวจสอบ" CssClass="btn"
                                        OnClick="btnApplove_Click" />
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ใช้งาน">
                            <ItemTemplate>
                                <div align="center">
                                    <asp:CheckBox ID="check" runat="server" />
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ผู้ตรวจสอบ">
                            <ItemTemplate>
                                <div align="left">
                                    <asp:Label ID="lblApplove" runat="server" Text='<%# Bind("ASSO_APPLOVE") %>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="400px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <br />
                <div align="center">
                    <asp:Button ID="btnSaveDoctype" runat="server" Text="บันทึก" CssClass="btn" OnClick="btnSaveDoctype_Click" />
                    <asp:Button ID="btnCancelDoctype" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancelDoctype_Click" />
                </div>
                <div id="divshow" runat="server">
                </div>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopup" runat="server" PopupDragHandleControlID="panPop"
                    Drag="true" TargetControlID="divshow" PopupControlID="panPop" BackgroundCssClass="modalbackground2">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="panPop" runat="server" CssClass="popup_container" Width="750px" Wrap="true" style="display:none;">
                    <div class="popup_title_confirm">
                        <asp:Label ID="lblTitle" runat="server" Text="กำหนดผู้ตรวจสอบเอกสาร"></asp:Label>
                    </div>
                    <br />
                    <table width="100%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td align="right">
                                ประเภทใบอนุญาต :
                            </td>
                            <td align="left">
                                <asp:Label ID="lblTypeName" runat="server"></asp:Label>
                                <asp:HiddenField ID="hdTypeCode" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                กำหนดสมาคมผู้ตรวจสอบเอกสาร :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlAsso" runat="server" DataValueField="ASSOCIATION_CODE" DataTextField="ASSOCIATION_NAME">
                                </asp:DropDownList>
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="เพิ่ม" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                    <div align="center">
                        <asp:GridView ID="gvAsso" runat="server" AutoGenerateColumns="false" BorderWidth="1"
                            CssClass="datatable" CellSpacing="-1" GridLines="None">
                            <AlternatingRowStyle CssClass="altrow" />
                            <Columns>
                                <asp:TemplateField HeaderText="ลำดับ">
                                    <ItemTemplate>
                                        <div align="center">
                                            <%# Container.DataItemIndex+1 %>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสสมาคม">
                                    <ItemTemplate>
                                        <div align="left">
                                            <div align="center">
                                                <asp:Label ID="lblAssoCode" runat="server" Text='<%# Bind("ASSOCIATION_CODE") %>'></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อสมาคม">
                                    <ItemTemplate>
                                        <div align="left">
                                            <asp:Label ID="lblAssoName" runat="server" Text='<%# Bind("ASSOCIATION_NAME") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ลบ">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:LinkButton ID="lblDelete" ToolTip="ลบ" OnClick="lblDelete_Click" runat="server"><img src="../Images/delete_icon.png" /></asp:LinkButton>
                                            <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe"
                                                TargetControlID="lblDelete">
                                            </ajaxToolkit:ConfirmButtonExtender>
                                            <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                                TargetControlID="lblDelete" Drag="true" OkControlID="btnYes" CancelControlID="btnNo"
                                                PopupDragHandleControlID="pnlPopup" BackgroundCssClass="modalbackground2">
                                            </ajaxToolkit:ModalPopupExtender>
                                            <asp:Panel ID="pnlPopup" runat="server" CssClass="popup_container" Wrap="true">
                                                <div class="popup_title_confirm">
                                                    <asp:Label ID="lblTitle" runat="server" Text="แจ้งเตือน"></asp:Label>
                                                </div>
                                                <div style="padding: 15px;">
                                                    <table width="90%" cellpadding="3" cellspacing="3">
                                                        <tr>
                                                            <td align="center" width="20px" style="background: none; border: none;">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/info.png" />
                                                            </td>
                                                            <td align="center" style="background: none; border: none;">
                                                                คุณต้องการลบข้อมูลใช่หรือไม่
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div align="center">
                                                        <asp:Button ID="btnYes" runat="server" CssClass="btn" Text="ใช่" />
                                                        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="ไม่" />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:Button ID="btnsave" runat="server" Text="บันทึก" CssClass="btn" OnClick="btnsave_Click" />
                        <asp:Button ID="btncancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btncancel_Click" />
                    </div>
                    <br />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

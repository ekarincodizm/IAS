<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SetHoliday.aspx.cs" Inherits="IAS.Setting.SetHoliday" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content" align="center">
        <div class="box_content_header">
            <div class="box_content_header_text">
                เพิ่มวันหยุด
            </div>
        </div>
        <div style="clear: left;">
        </div>
        <br />
        <br />
        <asp:UpdatePanel ID="update" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="3" cellspacing="3">
                    <tr>
                        <td align="right" width="40%">
                            วันที่หยุด :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDateHoliday" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="imgPopup_txtDateHoliday" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                runat="server" />
                            <ajaxToolkit:CalendarExtender ID="txtDateHoliday_CalendarExtender" PopupButtonID="imgPopup_txtDateHoliday"
                                runat="server" Enabled="True" TargetControlID="txtDateHoliday">
                            </ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" ControlToValidate="txtDateHoliday"
                                runat="server" ErrorMessage="กรุณาเลือกวันหยุด" Text="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            ชื่อวันหยุด :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtNameHoliday" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a" ControlToValidate="txtNameHoliday"
                                runat="server" ErrorMessage="กรุณาป้อนชื่อวันหยุด" Text="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancelSave" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancelSave_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <hr style="border-bottom: none; border-left: none; border-right: none; color: #ccc;
                    width: 800px;" />
                <br />
                <div align="center">
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                </div>
                <br />
                <asp:GridView ID="GVholiday" Width="700px" runat="server" AutoGenerateColumns="False"
                    BorderWidth="1" EnableTheming="True" CssClass="datatable" CellSpacing="-1" GridLines="None">
                    <AlternatingRowStyle CssClass="altrow" />
                    <HeaderStyle BorderStyle="None" />
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Bind("NUM") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="วันที่หยุด">
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server" Text='<%# Bind("HL_DATE", "{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อวันหยุด">
                            <ItemTemplate>
                                <asp:Label ID="lblNamedate" runat="server" Text='<%# Bind("HL_DESC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="แก้ไข">
                            <ItemTemplate>
                                <div align="center">
                                    <asp:LinkButton ID="lbtUpdate" runat="server" OnClick="lbtUpdate_Click"><img src="../Images/edit-icon.gif" /></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ลบ">
                            <ItemTemplate>
                                <div align="center">
                                    <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click"><img src="../Images/delete_icon.png" /></asp:LinkButton>
                                </div>
                                <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe"
                                    TargetControlID="lbtDelete">
                                </ajaxToolkit:ConfirmButtonExtender>
                                <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                                    TargetControlID="lbtDelete" Drag="true" OkControlID="btnYes" CancelControlID="btnNo"
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
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        ไม่พบข้อมูล
                    </EmptyDataTemplate>
                </asp:GridView>
                <uc2:UCModalError ID="UCModalError1" runat="server" />
                <uc3:UCModalSuccess ID="UCModalSuccess1" runat="server" />
                <div id="divshow" runat="server">
                </div>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupUpdate" runat="server" PopupDragHandleControlID="pnUpdate"
                    Drag="true" TargetControlID="divshow" PopupControlID="pnUpdate" BackgroundCssClass="modalbackground2">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnUpdate" runat="server" CssClass="popup_container" Width="500px"
                    Wrap="true">
                    <div class="popup_title_confirm">
                        <asp:Label ID="lblTitle" runat="server" Text="แก้ไขข้อมูล"></asp:Label>
                    </div>
                    <br />
                    <table width="100%" cellpadding="3" cellspacing="3">
                        <tr>
                            <td align="right">
                                วันที่หยุด :
                            </td>
                            <td align="left">
                                <asp:Label ID="lbldatate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ชื่อวันหยุด :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtUpdatename" runat="server" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b" ControlToValidate="txtNameHoliday"
                                    runat="server" ErrorMessage="กรุณาป้อนชื่อวันหยุด" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnUpdate" runat="server" Text="แก้ไข" CssClass="btn" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
                <asp:Panel ID="pnPage" runat="server" align="center">
                    <table width="100%" cellpadding="3" cellspacing="3">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnFirst" runat="server" Text="First" OnClick="btnFirst_Click" />
                                จำนวน<asp:TextBox ID="txtCount" runat="server" Text="20" Width="30px" onkeypress="return runScript(event)"
                                    AutoCompleteType="None"></asp:TextBox>
                                หน้า<asp:TextBox ID="txtPage" runat="server" Text="1" Width="30px" onkeypress="return runScript(event)"></asp:TextBox>
                                <asp:Button ID="btnLast" runat="server" Text="Last" OnClick="btnLast_Click" />
                                <asp:Button ID="btnShow" runat="server" Text="แสดง" OnClick="btnShow_Click" />
                            </td>
                            <td align="center">
                                จำนวนหน้า
                                <asp:Label ID="lblPage" runat="server"></asp:Label>
                                หน้า จำนวนทั้งหมด
                                <asp:Label ID="lblCount" runat="server"></asp:Label>
                                แถว
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

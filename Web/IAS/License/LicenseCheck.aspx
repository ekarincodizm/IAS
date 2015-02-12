<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="LicenseCheck.aspx.cs" Inherits="IAS.License.LicenseCheck" %>
<%@ Register Src="~/UserControl/ucLicensePayment.ascx" TagName="UCLicensePayment" TagPrefix="uc1" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <script type="text/javascript">
        function confirmCancel() {
            return confirm('คุณต้องการปิดหรือไม่?');
        }

        function confirmDelete() {
            return confirm('คุณต้องการลบหรือไม่?');
        }
    </script>
<asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server">
        <div align="center">
            <table width="90%">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlAll" runat="server" Enabled="false">
                        <uc1:UCLicensePayment ID="ucLicensePaymentAll" runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlY" runat="server" Enabled="true">
                        <uc1:UCLicensePayment ID="ucLicensePaymentY" runat="server" />
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnlSort" runat="server" Enabled="true">
                            <asp:Button ID="btnSortPopup" runat="server" Text="ออกใบสั่งจ่าย" CssClass="btn" Width="120px" OnClick="btnSortPopup_Click" />
                        </asp:Panel>&nbsp;&nbsp;&nbsp;

                    </td>
                </tr>
                <tr>
                    <td>
                        <ajaxToolkit:ModalPopupExtender ID="mdlLicenseD" runat="server" TargetControlID="btnS" PopupControlID="pnlLicenseD" 
                        BackgroundCssClass="modalbackground" CancelControlID="btnCancel"  >
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Button ID="btnS" runat="server" style="display:none;" />
                        <asp:Panel ID="pnlLicenseD" runat="server" CssClass="popup_container"  style="display:inherit; width:800px;">
                        <asp:GridView ID="gvLicenseD" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDataBound="gvLicenseD_RowDataBound" OnRowCommand="gvLicenseD_RowCommand" >
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
                                        <asp:Label ID="lblPetitionName" runat="server"  Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                        <asp:Label ID="lblPetitionNo" runat="server"  Visible="false" Text='<%# Bind("PETITION_TYPE_CODE") %>'></asp:Label>
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
                                            <asp:Image ID="imgCorrect" runat="server" ImageUrl="~/Images/correct.png" Width="16px" Height="16px" Visible="false" />
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
                            <br />
                            <asp:Button ID="btnPayment" runat="server" Text="ออกใบสั่งจ่าย" CssClass="btn" Width="120px" onclick="btnPayment_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn" Width="120px" />
                            <br /><br />
<%--                            <asp:Panel ID="pnlButton" runat="server" Enabled="true">
                            <asp:Button ID="btnPayment" runat="server" Text="ออกใบสั่งจ่ายรวม" 
                                CssClass="btn" Width="120px" onclick="btnPayment_Click" />&nbsp;&nbsp;&nbsp;
                            </asp:Panel>--%>
                        </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<uc1:UCLicensePayment ID="ucLicensePaymentW" runat="server" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

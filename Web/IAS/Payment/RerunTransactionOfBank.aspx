<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="RerunTransactionOfBank.aspx.cs" Inherits="IAS.Payment.RerunTransactionOfBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function confirmImport() {
            return confirm('คุณต้องการนำเข้าระบบหรือไม่?');
        }
        function confirmCancle() {
            return confirm('คุณต้องการยกเลิกการนำเข้าข้อมูลชุดนี้หรือไม่?');
        }

        function confirmChange() {
            return confirm("คุณต้องการแก้ไขข้อมูลใช่หรือไม่?");
        }   
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ตรวจสอบการนำเข้าข้อมูลการชำระเงิน" BorderStyle="None" ID="lblTheLicensee"
                    runat="server" />
            </div>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server" Visible="true" Width="100%">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%; margin: 0px auto 10px auto;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <center>
                                                        <table style="width: 50%; margin: 0px auto 10px auto;">
                                                            <tr>
                                                                <th>
                                                                    เลขที่อ้างอิง 1 :
                                                                </th>
                                                                <td colspan="3">
                                                                    <asp:TextBox runat="server" ID="txtFindRefNo" Width="100%"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <th>
                                                                    วันที่ชำระเงิน :
                                                                </th>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtPaymentDateStart"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgPopup_txtPaymentDateStart" ImageUrl="~/Images/calendar.gif"
                                                                        ImageAlign="Bottom" runat="server" />
                                                                    <ajaxToolkit:CalendarExtender ID="calexPaymentNumberStart" PopupButtonID="imgPopup_txtPaymentDateStart"
                                                                        runat="server" TargetControlID="txtPaymentDateStart" Format="dd/MM/yyyy">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    ถึง
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtPaymentDateEnd"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgPopup_txtPaymentDateEnd" ImageUrl="~/Images/calendar.gif"
                                                                        ImageAlign="Bottom" runat="server" />
                                                                    <ajaxToolkit:CalendarExtender ID="calexPaymentNumberEnd" PopupButtonID="imgPopup_txtPaymentDateEnd"
                                                                        runat="server" TargetControlID="txtPaymentDateEnd" Format="dd/MM/yyyy">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnSearchPayment" Text="ค้นหา" OnClick="btnSearchPayment_OnClick"
                                                                        CssClass="btn" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </center>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlData">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::รายการชำระเงินที่ไม่พบข้อมูลในระบบ</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <asp:GridView runat="server" ID="gvPaymentMissing" CssClass="datatable" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingNOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่ชำระเงิน">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingPaymentDate" runat="server" Text='<%# Bind("PaymentDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingRef1" runat="server" Text='<%# Bind("Ref1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงิน">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย ที่เลือก">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingChangeRef1" runat="server" Text='<%# Bind("ChangeRef1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="จำนวนเงิน">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingChangeAmount" runat="server" Text='<%# Bind("ChangeAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ดำเนินการ" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="hplFindPayment" runat="server" CausesValidation="false" CommandName=""
                                                            Text="ตรวจสอบ" OnClick="hplFindPaymentControl_OnClick"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPaymentMissingId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div style="display: none">
                                            <asp:Button ID="btnPnlFindPayment" runat="server" Text="Open" />
                                            <asp:TextBox ID="txtFindPaymentSelected" runat="server"></asp:TextBox>
                                        </div>
                                        <ajaxToolkit:ModalPopupExtender ID="ModFindPayment" runat="server" CancelControlID="btnFindPaymentCancel"
                                            OkControlID="btnFindPaymentOkey" TargetControlID="btnPnlFindPayment" PopupControlID="PnlFindPayment"
                                            PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalbackground">
                                        </ajaxToolkit:ModalPopupExtender>
                                        <asp:Panel ID="PnlFindPayment" Style="display: block" runat="server" CssClass="modalpopup">
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::รายการใบสั่งจ่ายที่ยังไม่ชำระเงิน</div>
                                                <div class="box_textbox_bgadd">
                                                    <div class="box_texbox_add_box">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <th>
                                                                                เลขที่ใบสั่งจ่าย :
                                                                            </th>
                                                                            <td>
                                                                                <asp:TextBox runat="server" ID="txtPaymentNumberStart"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                ถึง
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox runat="server" ID="txtPaymentNumberEnd"></asp:TextBox>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button runat="server" ID="btnFindPayment" OnClick="btnFindPayment_OnClick" Text="ค้นหา" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <div style="height: 200px; overflow: scroll">
                                                                        <asp:GridView runat="server" ID="gvFindPaymentResult" AutoGenerateColumns="False"
                                                                            Width="100%" Height="200px">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPaymentRefNo" runat="server" Text='<%# Bind("PaymentRefNo") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="วันที่สร้างใบสั่งจ่าย">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("CreateDate") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="จำนวนเงิน">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ShowHeader="False">
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="btnSelectedMappingPaymentMissing" runat="server" CausesValidation="false"
                                                                                            CommandName="" Text="เลือก" OnClick="btnSelectedMappingPaymentMissing_OnClick" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblgvFindPaymentResultId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="btnFindPaymentCancel" runat="server" Text="ปิด" CssClass="btn" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div style="text-align: center;">
                                        <br />
                                        <asp:Button runat="server" CssClass="btn" ID="btnImport" Text="นำเข้าระบบ" OnClick="btnImport_Click"
                                            OnClientClick="return confirmImport();" Enabled="false" />
                                        &nbsp;&nbsp;
                                        <asp:Button runat="server" CssClass="btn" ID="btnCancle" Text="ยกเลิก" OnClick="btnCancle_Click"
                                            OnClientClick="return confirmCancle();" />
                                        <br />
                                        <asp:Label runat="server" ID="lblMessageError" Text="" Visible="false" Enabled="false"
                                            ForeColor="Red"></asp:Label>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
</asp:Content>

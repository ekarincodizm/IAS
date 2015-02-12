<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ImportApplicantAssoc.aspx.cs" Inherits="IAS.Applicant.ImportApplicantOIC" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPersonalApplicantDetail.ascx" TagName="ucPersonalApplicantDetail"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmImport() {
            return confirm('คุณต้องการนำเข้าระบบหรือไม่?');
        }
        function confirmCancle() {
            return confirm('คุณต้องการยกเลิกการนำเข้าข้อมูลชุดนี้หรือไม่?');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:UpdatePanel ID="upn" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="box_content">
                <div class="box_content_header" style="height: 50px;">
                    <div class="box_content_header_text">
                        <asp:Label Text="ตรวจสอบเอกสาร" BorderStyle="None" ID="lblCheckDocument" runat="server" />
                    </div>
                    <div>    <div style="text-align: right;">
                        <asp:LinkButton ID="linkApplicantFile" runat="server">
                        
                                <asp:Image runat="server" ID="linkImageCSV" ImageUrl="~/image/csv-1.png" Width="30px"
                                    Height="35px" />
                        </asp:LinkButton></div>
                    <div style="text-align: right;">
                        <asp:Label Text="ดาวน์โหลดไฟล์ตัวอย่าง" BorderStyle="None" ID="lblDownload" runat="server"
                            Font-Bold="true" ForeColor="White" /></div>
                </div>
            </div>
            <div id="divSubMain" style="visibility: visible">
                <div class="box_body_content">
                    <div class="box_t_h">
                    </div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:Label ID="lblFileName" runat="server" CssClass="lbl" Text="ไฟล์ข้อมูล"></asp:Label>
                                        &nbsp;<asp:FileUpload ID="FuFile" runat="server" CssClass="txt" Height="20px" Width="450px" />
                                        <asp:Button ID="btnLoadFile" runat="server" CssClass="btn" OnClick="btnLoadFile_Click"
                                            Text="โหลดข้อมูล" />
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upn">
                                            <ProgressTemplate>
                                                Please wait image uploaded....</ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlImportFile" runat="server" Visible="false">
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::ผลการนำเข้า</div>
                        <div class="box_textbox_bg_table">
                            <div class="box_texbox_add_box">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <asp:GridView ID="gvImportFile" runat="server" CaptionAlign="Top" CssClass="datatable"
                                                CellSpacing="-1" GridLines="None" BorderWidth="1" Width="100%" AllowPaging="false"
                                                ShowHeaderWhenEmpty="True" EnableTheming="True" PageSize="12" ssClass="gridtxlist"
                                                PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">" PagerSettings-PreviousPageText="<"
                                                PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First" AllowSorting="True"
                                                PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                                OnRowDataBound="gvImportFile_RowDataBound">
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                                <SelectedRowStyle Font-Bold="True" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="True" />
                                                <AlternatingRowStyle CssClass="altrow" />
                                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับที่">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="td-center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="30%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("UploadFileName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("Totals") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายการที่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("RightTrans") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="20%" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemsIncorrectGv" runat="server" Text='<%# Eval("MissingTrans") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                        <ItemStyle Width="20%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemsIncorrectRemark" runat="server" Text=''></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::เพิ่มสมาชิก</div>
                        <div class="box_textbox_bg_table">
                            <div class="box_texbox_add_box">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <asp:GridView ID="gvCheckList" runat="server" CssClass="datatable" CellSpacing="-1"
                                                ShowHeaderWhenEmpty="True" GridLines="None" BorderWidth="1" CaptionAlign="Top"
                                                Width="100%" EnableTheming="True" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                                OnRowDataBound="gvCheckList_RowDataBound">
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                                <PagerStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="True" />
                                                <AlternatingRowStyle CssClass="altrow" />
                                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับที่">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="td-center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="20%" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Eval("ID_CARD_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("NAMES") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("LASTNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Eval("TESTING_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="hplview" runat="server" OnClick="hplView_Click" Visible="false">แสดง</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("ERROR_MSG") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SEQ_NO" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSeqNoGv" runat="server" Text='<%# Eval("SEQ_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <div>
                                                <asp:Button ID="btnImport" runat="server" Text="นำเข้าระบบ" CssClass="btn" OnClick="btnImport_Click"
                                                    OnClientClick="return confirmImport();" />&nbsp;&nbsp;
                                                <asp:Button ID="btnImportCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnImportCancel_Click"
                                                    OnClientClick="return confirmCancle();" />
                                            </div>
                                            <div>
                                                <asp:Label runat="server" ID="lblErrMessage" Text="" ForeColor="Red"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hdfGroupID" runat="server" />
                <asp:HiddenField ID="hdfSegNo" runat="server" />
            </div>
            <div style="display: none">
                <asp:Button ID="btnModal" runat="server" Text="Open" />
            </div>
            <ajaxToolkit:ModalPopupExtender ID="ModGroupApplicant" runat="server" CancelControlID="btnCancel"
                OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                Drag="true" BackgroundCssClass="modalbackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlModal" runat="server" CssClass="modalpopup" Style="display: block">
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::ข้อมูลการสมัครสอบ</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <uc4:ucPersonalApplicantDetail ID="ucPersonalApplicantDetail1" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <uc2:UCModalError ID="UCModalError" runat="server" />
            <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
            </div>
            <div style="clear: left;">
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnLoadFile" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

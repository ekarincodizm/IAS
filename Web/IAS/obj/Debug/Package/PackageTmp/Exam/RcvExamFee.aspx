<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="RcvExamFee.aspx.cs" Inherits="IAS.Exam.WebForm4" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(function setdate() {
            $('#txtStartDate').attr('readOnly', 'true');
            $('#txtEndDate').attr('readOnly', 'true');
        });
        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลการชำระ" BorderStyle="None" ID="lblDetailPay" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblTypePayment" runat="server" CssClass="lbl" Text="ประเภทการชำระ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlTypePayment" runat="server" CssClass="ddl" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblExamCode" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtExamCode" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTimeExam" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                    :
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtExamCode1" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td rowspan="2">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                    <br />
                                                    <br />
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblDateStartExam" runat="server" CssClass="lbl" Text="วันที่สั่งจ่าย (เริ่ม)"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CLDtxtStartDate" PopupButtonID="imgPopup_txtStartDate"
                                                        runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndDate" runat="server" Text="วันที่สั่งจ่าย(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CLDtxtEndDate" PopupButtonID="imgPopup_txtEndDate" runat="server" TargetControlID="txtEndDate"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <span>
                                                        <asp:Label ID="lblReceiptNumber" runat="server" CssClass="lbl" Text="เลขที่ใบเสร็จ"></asp:Label>
                                                        :</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtExamCode0" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PnlDetailSearchGridView" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvSearch" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                                        CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True"
                                                        EmptyDataText="ไม่มีข้อมูล..." AutoGenerateColumns="False">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Right" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-CssClass="td-center">
                                                                <HeaderTemplate>
                                                                    <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                                                        type="checkbox" />
                                                                </HeaderTemplate>
                                                                <ItemStyle Width="5%" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelectPayment" runat="server" Checked="false" AutoPostBack="true" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทการชำระ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTypePayGv" runat="server" Text='<%# Bind("strPaymentType") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="บัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("strIDNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("strFirstName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("strLastName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่สั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDatePayGv" runat="server" Text='<%# Bind("dChequeDate") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderGv" runat="server" Text='<%# Bind("strCheque") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบเสร็จ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblReceiptNumberGv" runat="server" Text='<%# Bind("strBillNumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1"></asp:TextBox>
                                                    <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;" colspan="2">
                                                    <asp:Button ID="btnPrint" runat="server" Text="พิมพ์" CssClass="btn" Visible="false"
                                                        OnClick="btnPrint_Click" />
                                                </td>
                                                <td style="text-align: left;" colspan="2">
                                                    <asp:Button ID="btnDownload" runat="server" Text="ดาวน์โหลด" CssClass="btn" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="PaymentDetail3.aspx.cs" Inherits="IAS.Payment.PaymentDetail3" %>

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


        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else if (charCode == 13) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลการชำระเงินของสนามสอบ" BorderStyle="None" ID="lblDetailPay"
                    runat="server" />
            </div>
        </div>
        <br />
        <br />
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
                                                <td style="text-align: right; width: 15%;">
                                                    <asp:Label ID="Label1" runat="server" Text="สังกัด" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="ddl"
                                                        Width="200px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                        <asp:ListItem Text="เลือก" Value="" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="หน่วยงานจัดสอบ" Value="G"></asp:ListItem>
                                                        <asp:ListItem Text="สมาคม" Value="A"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right; width: 15%;">
                                                    <asp:Label ID="lblTypePay" runat="server" Text="หน่วยงานจัดสอบ/สมาคม" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 15%">
                                                    <asp:DropDownList ID="ddlGroupExam" runat="server" AutoPostBack="True" CssClass="ddl"
                                                        Width="200px" OnSelectedIndexChanged="ddlGroupExam_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblOrder" runat="server" Text="สนามสอบ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:DropDownList ID="ddlExamPlaceCode" runat="server" AutoPostBack="True" CssClass="ddl"
                                                        Width="300px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblStartDate" runat="server" Text="วันที่สั่งจ่าย(เริ่ม)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtStartDate_CalendarExtender" PopupButtonID="imgPopup_txtStartDate"
                                                        runat="server" Enabled="True" TargetControlID="txtStartDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndDate" runat="server" Text="วันที่สั่งจ่าย(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="txtEndDate_CalendarExtender" PopupButtonID="imgPopup_txtEndDate"
                                                        runat="server" Enabled="True" TargetControlID="txtEndDate">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td colspan="2" style="text-align: center;">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Width="75px" CssClass="btn"
                                                        OnClick="btnSearch_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" OnClick="btnCancle_Click"
                                                        Text="ยกเลิก" Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
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
                                <div style="float: right; padding-right: 20px;">
                                    <asp:ImageButton ID="btnExportExcel" Visible="false" runat="server" ImageUrl="~/image/exel.png"
                                        OnClick="btnExportExcel_Click" ToolTip="ดาวน์โหลดข้อมูล" />
                                </div>
                                <div class="box_textbox_bg_table" runat="server" id="bludDiv">
                                    <div class="box_texbox_add_box">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvSearch" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1"
                                                        GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        AllowSorting="True" ShowHeaderWhenEmpty="True" OnRowDataBound="gvSearch_RowDataBound"
                                                        Visible="false">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNOO" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทการชำระ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTypePayGv" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="บัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FIRST_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderPayGv" runat="server" Text='<%# Bind("group_request_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รหัสรอบสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderTestGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="วันที่ออกใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="8%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOrderPayDateGv" runat="server" Text='<%# Bind("CREATED_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด"  /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--  <asp:TemplateField HeaderText="วันที่ชำระเงิน" ItemStyle-HorizontalAlign="Center"
                                                Visible="true">
                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReceiptDateGv" runat="server" Text='<%# Bind("payment_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                            <%--<asp:TemplateField HeaderText="เลขที่ใบเสร็จ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReceiptNoGv" runat="server" Text='<%# Bind("RECEIPT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="RUN_NO" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRunNoGv" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAMOUNTGv" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HeadNo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHeadNoGv" runat="server" Text='<%# Bind("head_request_no") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PaymentNo" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentNoGv" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="10%" HeaderText="ดำเนินการ" Visible="false">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplPrint" runat="server" OnClick="btnPrint_Click"><img src="../Images/print-icon.gif" title="พิมพ์" /></asp:LinkButton>&nbsp;&nbsp;
                                                                    <asp:LinkButton ID="linkDownload" runat="server" OnClick="btnDownload_Click"><img src="../Images/download-icon.gif"  title="Download"/> </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemStyle Width="0%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPathFile" runat="server" Text='<%# Bind("RECEIVE_PATH") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false"
                                                                    Text="1"></asp:TextBox>
                                                                <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                                <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;"
                                                                    Visible="true" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" MaxLength="4" Style="text-align: center"
                                                                    Width="5%" onkeypress="return runScript(event)" ondrop="return false;" AutoComplete="off"
                                                                    onpaste="return false;"></asp:TextBox>&nbsp;
                                                                <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearch_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Panel ID="PnlPopup" runat="server">
                                            <table width="80%">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnS1" runat="server" Style="display: none" />
                                                        <ajaxToolkit:ModalPopupExtender ID="MPDetail" runat="server" TargetControlID="btnS1"
                                                            PopupControlID="pnlValidateprop" BackgroundCssClass="modalbackground" CancelControlID="btnClosePop">
                                                        </ajaxToolkit:ModalPopupExtender>
                                                        <asp:Panel ID="pnlValidateprop" Style="display: inherit" runat="server" CssClass="modalpopup"
                                                            Width="800px">
                                                            <div style="width: 100%;">
                                                                <div class="box_t_h" style="width: 50%;">
                                                                    <asp:Label ID="lblpaymentNo" runat="server"></asp:Label>&nbsp;</div>
                                                                <div class="box_body_content" style="text-align: center">
                                                                    <div class="box_textbox_bg_table">
                                                                        <div class="box_texbox_add_box">
                                                                            <asp:GridView ID="GVPopupReceipt" runat="server" Width="100%" CssClass="datatable"
                                                                                CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                                ShowHeaderWhenEmpty="True" AllowSorting="True">
                                                                                <AlternatingRowStyle CssClass="altrow" />
                                                                                <HeaderStyle BorderStyle="None" />
                                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                                <EmptyDataTemplate>
                                                                                    <div style="text-align: center">
                                                                                        ไม่พบข้อมูล...</div>
                                                                                </EmptyDataTemplate>
                                                                                <PagerStyle HorizontalAlign="Left" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="ลำดับ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                        <ItemStyle Width="5%" CssClass="td-center" />
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblRunno" runat="server" Text='<%#Bind("rownum") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="เลขที่ใบเสร็จ" ItemStyle-HorizontalAlign="Center"
                                                                                        Visible="true">
                                                                                        <ItemStyle Width="10%" CssClass="td-center" />
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblReceiptNumberGv" runat="server" Text='<%# Bind("RECEIPT_NO") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="true" HeaderText="วันที่ออกใบเสร็จ">
                                                                                        <ItemStyle Width="10%" />
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblReceiveDate" runat="server" Text='<%# Bind("receipt_date","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="true" HeaderText="จำนวนเงิน">
                                                                                        <ItemStyle Width="10%" />
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblAMOUNT" runat="server" Text='<%# Bind("amount","{0:###,###,###}") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                    <asp:Button ID="btnClosePop" CssClass="btn" Text="ปิด" runat="server" />
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="InvoiceSubGroup.aspx.cs" Inherits="IAS.Payment.InvoiceSubGroup" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>--%>
    <script type="text/javascript">

        function confirmProcess() {
            return confirm('ยืนยันการประมวล');
        }

        var TotalChkBx;
        var CounterPayment = 0;


        function Set_TotalChkBxSub(GVCount) {
            alert(GVCount);
            TotalChkBx = GVCount;
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
        function OnContactSelected(source, eventArgs) {

            var hdfValueID = "<%= hdf.ClientID %>";

            document.getElementById(hdfValueID).value = eventArgs.get_value();
            __doPostBack(hdfValueID, "");
        }
        //      function HeaderClick(CheckBox, GVid, MomId, ChildId) {
        //       

        //          var TargetBaseControl = document.getElementById('<%= this.gvPaymentAssimilate.ClientID %>');
        //         
        //          var TargetChildControl = ChildId;
        //          var Inputs = TargetBaseControl.getElementsByTagName("input");

        //          for (var n = 0; n < Inputs.length; ++n)
        //              if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
        //                  Inputs[n].checked = CheckBox.checked;
        //        
        //              CounterPayment = CheckBox.checked ? TotalChkBx : 0;
        //        

        //      }

        //      function ChildClick(CheckBox, HCheckBox, GVid) {
        //        
        //          var HeaderCheckBox = document.getElementById(HCheckBox);
        //              if (CheckBox.checked && CounterPayment < TotalChkBx)
        //                  CounterPayment++;
        //              else if (CounterPayment > 0)
        //                  CounterPayment--;
        //              if (CounterPayment < TotalChkBx)
        //                  HeaderCheckBox.checked = false;
        //              else if (CounterPayment == TotalChkBx)
        //                  HeaderCheckBox.checked = true;
        //        
        //      }
        $(function setdate() {
            $('#txtStartPaidSubDate').attr('readOnly', 'true');
            $('#txtEndPaidSubDate').attr('readOnly', 'true');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="สร้างใบสั่งจ่ายกลุ่ม" BorderStyle="None" ID="lblCreatePaymentAssimilate"
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
                                                <td style="text-align: right; width: 20%;">
                                                    <span>
                                                        <asp:Label ID="lblStartPaidSubDate" runat="server" CssClass="lbl" Text="วันที่เริ่ม"></asp:Label>
                                                        : </span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtStartPaidSubDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtStartPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEStartPaidSubDate" PopupButtonID="imgPopup_txtStartPaidSubDate"
                                                        runat="server" TargetControlID="txtStartPaidSubDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblEndPaidSubDate" runat="server" CssClass="lbl" Text="วันที่สิ้นสุด"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtEndPaidSubDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtEndPaidSubDate" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CEEndPaidSubDate" PopupButtonID="imgPopup_txtEndPaidSubDate"
                                                        runat="server" TargetControlID="txtEndPaidSubDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblComCode" runat="server" Text="บริษัท"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox runat="server" ID="txtCompany" CssClass="txt" Width="60%"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txtCompany"
                                                        ServiceMethod="GetCompanyCodeAsCompanyTname" ServicePath="~/Service/AutoComplete.asmx"
                                                        MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
                                                        ShowOnlyCurrentWordInCompletionListItem="true" FirstRowSelected="true" OnClientItemSelected="OnContactSelected">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <asp:HiddenField ID="hdf" runat="server" OnValueChanged="hdf_ValueChanged" />
                                                    <asp:TextBox ID="txtID" runat="server" Width="400px" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                                        Text="ค้นหา" Width="75px" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnMainCancle_Click"
                                                        Width="75px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        &nbsp;
                        <div class="box_body_content" runat="server" id="Detaill">
                            <div class="box_t_h">
                            </div>
                            <div style="float: right; padding-right: 20px;">
                                <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/image/exel.png" OnClick="btnExportExcel_Click"
                                    Visible="false" />
                            </div>
                            <div class="box_textbox_bg_table" runat="server" visible="false" id="boxresult">
                                <div class="box_texbox_add_box">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:GridView ID="gvPaymentAssimilate" runat="server" CssClass="datatable" CellSpacing="-1"
                                                    ShowHeaderWhenEmpty="True" GridLines="None" BorderWidth="1" Width="100%" EnableTheming="True"
                                                    AutoGenerateColumns="False" Visible="false" OnRowDataBound="gvPaymentAssimilate_RowDataBound1">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle HorizontalAlign="Left" />
                                                    <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                            ไม่พบข้อมูล...</div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <%--  <asp:CheckBox Id="chkAll" onclick="javascript:HeaderClick(this,'gvPaymentAssimilate','chkAll','chkSelectPayment');"  runat="server"  />  --%>
                                                                <asp:CheckBox ID="checkall" runat="server" AutoPostBack="true" OnCheckedChanged="checkall_CheckedChanged" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectPayment" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectPayment_CheckedChanged1" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="td-center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaymentAssimilateGv" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="30%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypePaymentGv" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmountPeopleGv" runat="server" Text='<%# Bind("PERSON_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmountMoneyGv" runat="server" Text='<%# Bind("SUBPAYMENT_AMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่จ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="25%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaymentAssimilateDateGv" runat="server" Text='<%# Bind("SUBPAYMENT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
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
                                                        <td style="width: 80%">
                                                            <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                            <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1"
                                                                Enabled="false"></asp:TextBox>
                                                            <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                            <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="lblHeadInputMaxrow" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า"></asp:Label>&nbsp;
                                                            <asp:TextBox ID="txtInputMaxrow" runat="server" Visible="false" Width="5%" onkeypress="return runScript(event)"
                                                                MaxLength="4" Style="text-align: center" onpaste="return false;" ondrop="return false;"
                                                                AutoComplete="off"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:Button ID="btngo" runat="server" Text="Go" OnClick="btnSearch_Click" Visible="false" />
                                                        </td>
                                                        <td style="width: 20%; text-align: right">
                                                            <div runat="server" id="div_total">
                                                                <asp:Label ID="lblHeadTotal" runat="server" Visible="false" Text="จำนวน"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblTotalrecord" runat="server" Visible="false" Style="font-weight: bold"></asp:Label>&nbsp;
                                                                <asp:Label ID="lblEndTotal" runat="server" Text="รายการ" Visible="false"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table align="center" style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;" colspan="2">
                                                <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOk_Click" />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" />
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnS1" runat="server" Style="display: none" />
                                            <ajaxToolkit:ModalPopupExtender ID="MPDetail" runat="server" TargetControlID="btnS1"
                                                PopupControlID="pnlValidateprop" BackgroundCssClass="modalBackground2" DropShadow="true">
                                            </ajaxToolkit:ModalPopupExtender>
                                            <asp:Panel ID="pnlValidateprop" Style="display: inherit" runat="server" CssClass="modalpopup2"
                                                Width="800px">
                                                <table width="100%" id="remark" runat="server" visible="false">
                                                    <tr>
                                                        <td style="width: 10%">
                                                            <asp:Label ID="lblremark1A" runat="server" Text="หมายเหตุ :"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblremark1B" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;* การเรียงลำดับใบสั่งจ่ายนั้นเป็นส่วนหนึ่งของการเรียงลำดับเพื่อตัดยอดในการชำระเงินกรณีที่ชำระเงินไม่เต็มจำนวนตามใบสั่งจ่าย"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblremark2B" runat="server" Text="&nbsp; ** กรุณาเรียงลำดับของรายการของการตรวจสอบการชำระเงินเพื่อประโยชน์ในการชำระค่าธรรมเนียมของตัวท่านเอง"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblremark3B" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   โดยเรียงลำดับที่1 เป็นลำดับที่ได้ทำการตรวจสอบการชำระเงินจากธนาคารก่อน ตามลำดับของรายการที่ได้เลือกไว้"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblremark4B" runat="server" Text="***   การเรียงลำดับของใบสั่งจ่ายนั้น ไม่มีผลต่อวันหมดอายุของใบสั่งจ่าย และยอดเงินรวมในใบสั่งจ่าย"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div style="width: 100%;">
                                                    <div class="box_body_content">
                                                        <div class="box_t_h" style="width: 50%;">
                                                            ::จัดเรียงลำดับการออกใบสั่งจ่ายกลุุ่ม
                                                        </div>
                                                        <div class="box_textbox_bg_table">
                                                            <div class="box_texbox_add_box">
                                                                <asp:GridView ID="GvInvoicePopup" runat="server" CssClass="datatable" CellSpacing="-1"
                                                                    ShowHeaderWhenEmpty="True" GridLines="None" BorderWidth="1" Width="100%" EnableTheming="True"
                                                                    AutoGenerateColumns="False" Visible="false" OnRowDataBound="GvInvoicePopup_RowDataBound"
                                                                    OnRowCommand="GvInvoicePopup__RowCommand">
                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <PagerStyle HorizontalAlign="Left" />
                                                                    <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                    <EmptyDataTemplate>
                                                                        <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                            ไม่พบข้อมูล...</div>
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblRunNo" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPaymentAssimilateGv" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="30%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTypePaymentGv" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmountPeopleGv" runat="server" Text='<%# Bind("CountPerson") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblAmountMoneyGv" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="วันที่จ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="25%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPaymentAssimilateDateGv" runat="server" Text='<%# Bind("SubPaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="LBUP" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                    CommandName="Up"><img src="../Images/arrowup.png" /></asp:LinkButton>
                                                                                <asp:LinkButton ID="LBDown" runat="server" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                                    CommandName="Down"><img src="../Images/arrowdown.png" /></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="text-align: center;" align="center">
                                                    <div>
                                                        <table width="100%">
                                                            <tr align="center">
                                                                <td>
                                                                    <br />
                                                                    <asp:Button ID="btnSavePopup" runat="server" Text="ออกใบสั่งจ่ายกลุ่ม" CssClass="btn"
                                                                        OnClick="btnSavePopup_Click" />
                                                                    &nbsp;
                                                                    <asp:Button runat="server" ID="btnCloseProp" Text="ปิด" CssClass="btn" OnClick="btnCloseProp_Click" />
                                                                </td>
                                                            </tr>
                                                            <caption>
                                                                &nbsp;
                                                                <br />
                                                            </caption>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <br />
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnOk" />
                        <asp:PostBackTrigger ControlID="btnExportExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpEdit" TargetControlID="btnHide3" CancelControlID="btnCancelPop"
                BackgroundCssClass="modalbackground" PopupControlID="PnlPopup" runat="server">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PnlPopup" runat="server" CssClass="modalpopup" Style="width: 30%;">
                <%--                <asp:UpdatePanel ID="UplPopUp" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>--%>
                <asp:Button ID="btnHide3" runat="server" Style="display: none" Text="Button" />
                <div id="Div2" class="popupContainer">
                    <div>
                    </div>
                    <br />
                    <table style="width: auto;">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblPaymentAssimilateNumberDetail" runat="server" CssClass="lbl" Text="ต้องการสร้างใบสั่งจ่ายกลุ่มหรือไม่"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 500px">
                        <tr>
                            <td style="text-align: right; width: 20%">
                                <asp:Label ID="lblRemark" runat="server" Text="หมายเหตุ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 80%">
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="txt" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnConfirm" runat="server" CssClass="btn" OnClick="btnConfirm_Click"
                                    Text="ตกลง" />
                                &nbsp;
                                <asp:Button ID="btnCancelPop" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--                    </ContentTemplate>
                </asp:UpdatePanel>--%>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

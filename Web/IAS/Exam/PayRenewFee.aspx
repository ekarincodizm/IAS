<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="PayRenewFee.aspx.cs" Inherits="IAS.Exam.PayRenewFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        function visibleDiv(id) {
            if (document.getElementById(id).style.visibility == "hidden") {
                document.getElementById(id).style.visibility = "visible";
            }
            else {
                document.getElementById(id).style.visibility = "hidden";
            }
        }

        function visible() { 

            divSubMain.style.visibility = "hidden";
            divDetailExam.style.visibility = "hidden";
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">


<div class="box_content">
<div class="box_content_header">
<div class="box_content_header_text">
 <asp:Label Text="ชำระเงิน" BorderStyle="None" ID="lblPayment"
        runat="server" />
</div>
</div>



 
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">


            <div class="box_body_content">
            <div class="box_t_h"></div>
            <div class="box_textbox_bgadd">
            <div class="box_texbox_add_box">

            <table style="width: 100%;">
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblFileName" runat="server" Text="ไฟล์ข้อมูล" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 50%;" colspan="2">
                        <asp:FileUpload ID="FuFile" runat="server" Width="450px" CssClass="fileUpload" />
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:Button ID="btnLoadFile" runat="server" Text="โหลดข้อมูล" CssClass="btn" OnClientClick="visibleDiv('divSubMain'); return false;"
                            OnClick="btnLoadFile_Click" />
                    </td>
                </tr>
            </table>
        
            </div>
            </div>
            </div>
            

            <asp:UpdatePanel ID="UpdatePanelLoadFile" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divSubMain" style="visibility: hidden">
                        <asp:Panel ID="PnlSubMain" runat="server" Width="100%">
                      
                       

                                <div class="box_body_content">
                                <div class="box_t_h">::ผลการนำเข้า</div>
                                <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">

                               
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <asp:GridView ID="gvInportFile" runat="server" BackColor="White" BorderColor="#999999"
                                                HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3"
                                                GridLines="Vertical" Width="100%" AllowPaging="True" BorderStyle="None" EnableTheming="True"
                                                PageSize="12" ssClass="gridtxlist" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">"
                                                PagerSettings-PreviousPageText="<" PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First"
                                                AllowSorting="True" PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center"
                                                AutoGenerateColumns="False" PagerStyle-CssClass="viewpager">
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                                <RowStyle CssClass="view" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                                <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                                <AlternatingRowStyle CssClass="view2" />
                                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="50%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("ViewAll") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายการที่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("ItemsCorrect") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="20%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemsIncorrectGv" runat="server" Text='<%# Eval("ItemsIncorrect") %>'></asp:Label>
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
                                <div class="box_t_h">::ตรวจสอบรายการ</div>
                                <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">

                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <asp:GridView ID="gvCheckList" runat="server" BackColor="White" BorderColor="#999999"
                                                HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3"
                                                GridLines="Vertical" Width="100%" AllowPaging="True" BorderStyle="None" EnableTheming="True"
                                                PageSize="12" ssClass="gridtxlist" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">"
                                                PagerSettings-PreviousPageText="<" PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First"
                                                AllowSorting="True" PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center"
                                                AutoGenerateColumns="False" PagerStyle-CssClass="viewpager">
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                                <RowStyle CssClass="view" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                                <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                                <AlternatingRowStyle CssClass="view2" />
                                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="เลขที่บัญชี" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAccountNumberGv" runat="server" Text='<%# Eval("AccountNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่รับชำระ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPaymentDateGv" runat="server" Text='<%# Eval("PaymentDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อลูกค้า" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerNameGv" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReferenceNumber1Gv" runat="server" Text='<%# Eval("ReferenceNumber1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่เช็ค" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCheckNumberGv" runat="server" Text='<%# Eval("CheckNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmountGv" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="hplview" runat="server">แสดง</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <asp:Button ID="btnSearch" runat="server" Text="นำเข้าระบบ" CssClass="btn" OnClick="btnSearch_Click"
                                                OnClientClick="visibleDiv('divDetailExam'); return false;" />
                                        </td>
                                    </tr>
                                </table>

                                </div>
                                </div>
                                </div>
                                
                        </asp:Panel>
                    </div>
                    <br />
                    <br />
                    <div id="divDetailExam" style="visibility: hidden">
                        <asp:Panel ID="PnlDetailExam" runat="server" Width="100%">


               
                                <div class="box_body_content">
                                <div class="box_t_h">::การชำระเงิน</div>
                                <div class="box_textbox_bgadd">
                                <div class="box_texbox_add_box">

                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblPayeeName" runat="server" Text="ชื่อผู้รับเงิน" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtPayeeName" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblEffectiveDate" runat="server" Text="วันที่มีผล" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblServiceCode" runat="server" Text="รหัสบริการ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtServiceCode" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblAmount" runat="server" Text="จำนวนเงินรวม" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblBankCode" runat="server" Text="รหัสธนาคาร" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtBankCode" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblAccountNumber" runat="server" Text="เลขที่บัญชี" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDatePay" runat="server" Text="วันที่รับชำระ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtDatePay" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblTimePay" runat="server" Text="เวลาที่รับชำระ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtTimePay" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblCustomerName" runat="server" Text="ชื่อลูกค้า" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblReferenceNumber1" runat="server" Text="เลขที่ใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtReferenceNumber1" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblReferenceNumber2" runat="server" Text="เลขที่อ้างอิง2" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtReferenceNumber2" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblReferenceNumber3" runat="server" Text="เลขที่อ้างอิง3" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtReferenceNumber3" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblBranchID" runat="server" Text="รหัสสาขา" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtBranchID" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblTellerID" runat="server" Text="เลขที่ Teller" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtTellerID" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblTypeList" runat="server" Text="ประเภทรายการ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtTypeList" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblListID" runat="server" Text="รหัสรายการ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtListID" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblCheckID" runat="server" Text="เลขที่เช็ค" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtCheckID" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblAmountCheck" runat="server" Text="จำนวนเงิน" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtAmountCheck" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblCheckBankID" runat="server" Text="รหัสเช็คธนาคาร" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtCheckBankID" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;" colspan="4">
                                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;" colspan="2">
                                            <asp:Button runat="server" ID="btnSubmit" Text="ตกลง" CssClass="btn" />
                                        </td>
                                        <td style="text-align: left;" colspan="2">
                                            <asp:Button runat="server" ID="btnCacel" Text="ยกเลิก" CssClass="btn" />
                                        </td>
                                    </tr>
                                </table>

                                </div>
                                </div>
                                </div>
                                

                            </fieldset>
                        </asp:Panel>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnLoadFile" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>


</div>
<div style="clear:left;"></div>

</asp:Content>

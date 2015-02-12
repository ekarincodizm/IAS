<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="PayExamFee.aspx.cs" Inherits="IAS.Exam.PayExamFee" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
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
   <asp:Label Text="ชำระเงิน" BorderStyle="None" ID="lblPayment"  runat="server" />
</div>
</div>





    <div>
        <asp:Panel ID="PnlMain" runat="server">
            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                <ContentTemplate>


                <div class="box_body_content">
                <div class="box_t_h"></div>
                <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">

                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 40%;">
                                <asp:Label ID="lblTypePayment" runat="server" Text="ประเภทการชำระเงิน" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 60%;" colspan="2">
                                <asp:DropDownList ID="ddlTypePayment" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 40%;">
                                <asp:Label ID="lblFileName" runat="server" Text="ไฟล์ข้อมูล" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:FileUpload ID="FuFile" runat="server" Width="400px" CssClass="fileUpload" />
                            </td>
                            <td style="text-align: left; width: 60%;">
                                <asp:Button ID="btnLoadFile" runat="server" Text="โหลดข้อมูล" CssClass="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelUpload">
                                    <ProgressTemplate>
                                        <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                        Please wait image uploaded....</ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>

                </div>
                </div>
                </div>
                
                  
                    <div class="box_body_content">
                    <div class="box_t_h">::ผลการนำเข้า</div>
                    <div class="box_textbox_bg_table">
                    <div class="box_texbox_add_box">
                 


                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSubject" runat="server"  Width="100%" AllowPaging="false" 
                                    CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" ShowHeaderWhenEmpty="True" 
                                        EnableTheming="True" AutoGenerateColumns="False">
                                        <AlternatingRowStyle CssClass="altrow" />
                                        <HeaderStyle BorderStyle="None" />
                                        <PagerStyle  HorizontalAlign="Right" />
                                      
                                        <Columns>
                                         <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("ViewAll") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายการที่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("ItemsCorrect") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20%" CssClass="td-center" />
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

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnLoadFile" />
                </Triggers>
            </asp:UpdatePanel>
      


            <div class="box_body_content">
            <div class="box_t_h">::ตรวจสอบรายการ</div>
            <div class="box_textbox_bg_table">
            <div class="box_texbox_add_box">


          


                <asp:UpdatePanel ID="UpdatePanelCheckList" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center;" colspan="4">

                                    <asp:GridView ID="gvCheckList" runat="server"  Width="100%" AllowPaging="false"
                                    CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" ShowHeaderWhenEmpty="True"
                                        EnableTheming="True" AutoGenerateColumns="False">
                                        <AlternatingRowStyle CssClass="altrow" />
                                        <HeaderStyle BorderStyle="None" />
                                        <PagerStyle  HorizontalAlign="Right" />                                   
                                        <Columns>
                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Eval("IDNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่สมัครสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateRegExamGv" runat="server" Text='<%# Eval("DateRegExam") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Eval("ExamID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="hplview" runat="server"><img src="../Images/view-icon.png" title="แสดงรายละเอียด" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>

                    </ContentTemplate>
                </asp:UpdatePanel>
         
         </div>
         </div>
         </div>
         
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanelDetailPayment" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="นำเข้าระบบ" CssClass="btn" 
                                OnClientClick="visibleDiv('divDetailPayment'); return false;" 
                                onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div id="divDetailPayment" style="visibility: hidden">
                    <asp:Panel ID="PnlDetailPayment" runat="server" Width="100%">

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
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtPayeeName" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblEffectiveDate" runat="server" Text="วันที่มีผล" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblServiceID" runat="server" Text="รหัสบริการ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtServiceID" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblTotal" runat="server" Text="จำนวนเงินรวม" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblBankID" runat="server" Text="รหัสธนาคาร" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtBankID" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
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
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtDatePay" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
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
                   

                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>




</div>
<div style="clear:left;"></div>
</asp:Content>

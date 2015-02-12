<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="PayRenewFee.aspx.cs" Inherits="IAS.Payment.PayRenewFee" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    

      <div class="box_content">
      <div class="box_content_header">
      <div class="box_content_header_text">
       <asp:Label Text="นำเข้าข้อมูลการชำระเงิน" BorderStyle="None" ID="lblTheLicensee" runat="server" />
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
            	        <div class="box_t_h">::ค้นหา</div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">

                        <table style="width: 90%; margin: 0px auto 10px auto;">
                            <tr>
                                <td style="text-align: center;">
                                    <asp:Label ID="lblBank" runat="server" Text="ธนาคาร " CssClass="lbl" ></asp:Label>
                                    <asp:DropDownList runat="server" ID="ddlBankTranfer" >
                                        <asp:ListItem Value="" >กรุณาเลือก</asp:ListItem>
                                        <asp:ListItem Value="006" Text="ธนาคารกรุงไทย"></asp:ListItem>
                                        <asp:ListItem Value="Cit" Text="ธนาคารซิตี้แบงก์"></asp:ListItem>
                                    </asp:DropDownList>
             
                                    &nbsp;&nbsp;
                                    <span>
                                        <asp:Label ID="lblOrder" runat="server" CssClass="lbl" Text="ไฟล์ข้อมูล"></asp:Label>
                                    </span>
                                    <asp:FileUpload ID="FuFile" runat="server" CssClass="txt" Height="25px" Width="40%" />
                                    <asp:Button ID="btnLoadFile" runat="server" CssClass="btn" OnClick="btnLoadFile_Click"
                                        Text="โหลดข้อมูล"  />

                                </td>
                            </tr>
                        </table>

                        </div>
                        </div>
                        </div>


                    </asp:Panel>
                    <div class="clear"></div>
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

                    <asp:Panel ID="pnlConfirmImportList" runat="server" Visible="false">
                        <div class="box_body_content">
            	        <div class="box_t_h">::รายการชำระเงินล่าช้า</div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">
                                    <asp:GridView runat="server" ID="gvPaymentLate" CssClass="datatable" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ยืนยันการนำเข้า">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkgvPaymentLate" runat="server" OnCheckedChanged="chkgvPaymentLate_CheckedChanged" AutoPostBack="true" 
                                                    Checked='<%# Bind("Selected") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ลำดับ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLateSequenceNo" runat="server" Text='<%# Bind("SequenceNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่ชำระเงิน">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLatePaymentDate" runat="server" Text='<%# Bind("PaymentDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่กำหนดชำระเงิน">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLateExpireDate" runat="server" Text='<%# Bind("ExpireDate") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLateCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLateRef1" runat="server" Text='<%# Bind("Ref1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="จำนวนเงิน">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLateAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สถานะการชำระเงิน">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLateStatus" runat="server" Text="ไม่นำเข้า" ForeColor="Red"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentLateId" runat="server" Text='<%# Bind("Id") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                        </div></div></div>


                        <div class="box_body_content">
            	        <div class="box_t_h">::รายการชำระเงินที่ไม่พบข้อมูลในระบบ</div>
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
                                                    <asp:LinkButton ID="hplFindPayment" runat="server" CausesValidation="false" 
                                                        CommandName="" Text="ตรวจสอบ" OnClick="hplFindPaymentControl_OnClick"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaymentMissingId" runat="server" Text='<%# Bind("Id") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    
                                    </asp:GridView>
                        <div style="display: none">
                            <asp:Button ID="btnPnlFindPayment" runat="server" Text="Open" />
                            <asp:TextBox ID="txtFindPaymentSelected" runat="server" ></asp:TextBox>
                        </div>
                        <ajaxToolkit:ModalPopupExtender ID="ModFindPayment" runat="server" CancelControlID="btnFindPaymentCancel"
                                OkControlID="btnFindPaymentOkey" TargetControlID="btnPnlFindPayment" PopupControlID="PnlFindPayment" PopupDragHandleControlID="PopupHeader"
                                Drag="true" BackgroundCssClass="modalbackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Panel ID="PnlFindPayment" Style="display: block" runat="server" CssClass="modalpopup">
                            <div class="box_body_content">
            	            <div class="box_t_h">::รายการใบสั่งจ่ายที่ยังไม่ชำระเงิน</div>
            	            <div class="box_textbox_bgadd">
                	        <div class="box_texbox_add_box">
                             <table style="width: 100%;" >
                                  <tr>
                                    <td>
                                        <table style="width:100%;">
                                            <tr>
                                                 <th>เลขที่ใบสั่งจ่าย : </th><td><asp:TextBox runat="server" ID="txtPaymentNumberStart" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox></td>
                                                   <th>ถึง</th><td><asp:TextBox runat="server" ID="txtPaymentNumberEnd" onkeydown = "return (event.keyCode!=13);" ></asp:TextBox></td>
                                                   <td><asp:Button runat="server" ID="btnFindPayment" OnClick="btnFindPayment_OnClick" 
                                                           Text="ค้นหา" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>
                                        <div style="height:200px; overflow:scroll">
                                        <asp:GridView runat="server" ID="gvFindPaymentResult" 
                                            AutoGenerateColumns="False" Width="100%" Height="200px">
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
                                                        <asp:Button ID="btnSelectedMappingPaymentMissing" runat="server" CausesValidation="false" CommandName="" 
                                                            Text="เลือก" OnClick="btnSelectedMappingPaymentMissing_OnClick" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvFindPaymentResultId" runat="server" Text='<%# Bind("Id") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td align="center"><asp:Button ID="btnFindPaymentCancel" runat="server" Text="ปิด" CssClass="btn" /></td>
                                  </tr>
                             </table>
                             
                            </div>
                            </div>
                            </div>

                        </asp:Panel>
                        </div></div></div>
                        <div class="box_body_content">
            	        <div class="box_t_h">::รายการข้อมูลไม่ถูกต้อง</div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">


                            <asp:GridView runat="server" ID="gvPaymentInvalid" CssClass="datatable" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="5%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaymentInvalidNOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่ชำระ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="15%" CssClass="td-center" />
                                        <ItemTemplate>                             
                                            <asp:Label ID="lblgvPaymentInvalidPaymentDate" runat="server" Text='<%# Eval("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อผู้ชำระเงิน" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaymentInvalidCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย" ItemStyle-HorizontalAlign="Center"
                                        Visible="true">
                                        <ItemStyle Width="15%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaymentInvalidRef1" runat="server" Text='<%# Eval("Ref1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่เช็ค" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="10%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaymentInvalidChequeNo" runat="server" Text='<%# Eval("ChequeNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="10%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaymentInvalidAmount" runat="server" Text='<%# Eval("Amount","{0:n2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ปัญหา" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hplview" runat="server" OnClick="hplView_Click" Visible="false">แสดง</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvPaymentInvalidErrorMessage" runat="server" Text='<%# Eval("ErrorMessage") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemStyle Width="15%"  />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIDGv" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                   
                        </div>
                        </div>
                        </div>
                                             
                        <div style="text-align: center;">
                            <br />
                            <asp:Button runat="server" CssClass="btn" ID="bntConfirmChange" Text="ยืนยัน" OnClick="bntConfirmChange_Click"  OnClientClick="return confirmChange();"  />
                            &nbsp;&nbsp;
                            <asp:Button runat="server" CssClass="btn" ID="btnCancleChange" Text="ยกเลิก" OnClick="btnCancle_Click"  OnClientClick="return confirmCancle();" />
        
                            <br />
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlImportFile" runat="server" Visible="false">
                  

                        <div class="box_body_content">
            	        <div class="box_t_h">::ผลการนำเข้า</div>
            	        <div class="box_textbox_bg_table">
                	    <div class="box_texbox_add_box">
                            <table width="100%">
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:GridView ID="gvImportFile" runat="server" AllowPaging="True" AllowSorting="True"
                                            AutoGenerateColumns="False" CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" 
                                             CaptionAlign="Top" CellPadding="3" EnableTheming="True"  ShowHeaderWhenEmpty="True"
                                            PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                            PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10"
                                            PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager" PagerStyle-HorizontalAlign="Center"
                                            PageSize="12" ssClass="gridtxlist" Width="100%">
                                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                NextPageText="&gt;" PreviousPageText="&lt;" />
                                            <RowStyle  />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                            <PagerStyle  HorizontalAlign="Center" />
                                            <HeaderStyle  Font-Bold="True" />

                                            <AlternatingRowStyle CssClass="altrow" />

                                            <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อไฟล์" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="20%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="รายการทั้งหมด" ItemStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                    <ItemStyle Width="20%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("NumberOfItems") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="รายการที่ถูกต้อง" ItemStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                    <ItemStyle Width="20%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("NumberOfValid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" ItemStyle-HorizontalAlign="Center"
                                                    Visible="true">
                                                    <ItemStyle Width="20%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemsIncorrectGv" runat="server" Text='<%# Eval("NumberOfInValid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่นำเข้า" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                    <ItemStyle Width="20%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemsUploadDateGv" runat="server" Text='<%# Eval("UploadDate","{0:dd/MM/yyyy}") %>'></asp:Label>
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


                            <asp:GridView ID="gvCheckList" runat="server" Width="100%" CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1"
                                EnableTheming="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                CaptionAlign="Top"  PagerStyle-HorizontalAlign="Center" 
                                OnRowDataBound="gvCheckList_RowDataBound">
                                <AlternatingRowStyle CssClass="altrow" />
                                <HeaderStyle CssClass="viewheader" Font-Bold="True" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                <PagerStyle  HorizontalAlign="Center" />
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                <RowStyle CssClass="view" />
                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                     </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่ชำระ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="15%" CssClass="td-center" />
                                        <ItemTemplate>                             
                                            <asp:Label ID="lblPaymentDateGv" runat="server" Text='<%# Eval("PaymentDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อผู้ชำระเงิน" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่าย" ItemStyle-HorizontalAlign="Center"
                                        Visible="true">
                                        <ItemStyle Width="15%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("Ref1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่เช็ค" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="10%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblChequeNoGv" runat="server" Text='<%# Eval("ChequeNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงิน" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="10%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountGv" runat="server" Text='<%# Eval("Amount","{0:n2}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ปัญหา" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hplview" runat="server" OnClick="hplView_Click" Visible="false">แสดง</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                        <ItemStyle Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("ErrorMessage") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่" ItemStyle-HorizontalAlign="Center" Visible="false">
                                        <ItemStyle Width="15%"  />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIDGv" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                       
                      
                            <div style="text-align: center;">
                                <br />
                                <asp:Button runat="server" CssClass="btn" ID="btnImport" Text="นำเข้าระบบ" OnClick="btnImport_Click"  OnClientClick="return confirmImport();" Enabled="false" />
                                &nbsp;&nbsp;
                                <asp:Button runat="server" CssClass="btn" ID="Button1" Text="ยกเลิก" OnClick="btnCancle_Click" OnClientClick="return confirmCancle();" />
                                <br />
                                <asp:Label runat="server" ID="lblMessageError" Text="" Visible="false" Enabled="false" ForeColor="Red" ></asp:Label>
                                <br />
                                <br />
                            </div>

                                             
                        </div>
                        </div>
                        </div>
                    </asp:Panel>
                    <asp:HiddenField ID="hdfGroupID" runat="server" />
                    <asp:HiddenField ID="hdfID" runat="server" />
                    <div style="display: none">
                        <asp:Button ID="btnModal" runat="server" Text="Open" />
                    </div>
                    <ajaxToolkit:ModalPopupExtender ID="ModRenewFee" runat="server" CancelControlID="btnCancel"
                        OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                        Drag="true" BackgroundCssClass="modalbackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlModal" Style="display: block" runat="server" CssClass="modalpopup">


                        <div class="box_body_content">
            	        <div class="box_t_h">::การชำระเงิน</div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">

                     


                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <span>
                                            <asp:Label ID="lblDetailTitileName" runat="server" CssClass="lbl" Text="ผู้ชำระเงิน"></asp:Label>
                                        </span>&nbsp;
                                    </td>
                                    <td style="text-align: left; width: 25%;">
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 22%;">
                                        <span>
                                            <asp:Label ID="lblDetailName" runat="server" CssClass="lbl" Text="วันที่มีผล"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left; width: 25%;">
                                        <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailLastName" runat="server" CssClass="lbl" Text="รหัสบริการ"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtServiceCode" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailIdCardNumber" runat="server" CssClass="lbl" Text="จำนวนเงินรวม"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtTotal" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailSex" runat="server" CssClass="lbl" Text="รหัสธนาคาร"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtBankCode" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailBirthDay" runat="server" CssClass="lbl" Text="เลขที่บัญชี"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCompanyAccount" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailEducation" runat="server" CssClass="lbl" Text="วันที่ชำระเงิน"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailExamDate" runat="server" CssClass="lbl" Text="เวลาที่รับชำระ"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtPaymentTime" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblDetailPersonExam" runat="server" CssClass="lbl" Text="ชื่อผู้ชำระเงิน"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailExamCode" runat="server" CssClass="lbl" Text="เลขที่ใบสั่งจ่าย"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCustomerNoRef1" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailYardCode" runat="server" CssClass="lbl" Text="เลขที่อ้างอิง2"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtRef2" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailAssocExam" runat="server" CssClass="lbl" Text="เลขที่อ้างอิง3"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtRef3" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailCompany" runat="server" CssClass="lbl" Text="รหัสสาขา"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtBranchNo" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailCompany0" runat="server" CssClass="lbl" Text="เลขที่ Teller"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtTellerNo" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailCompany1" runat="server" CssClass="lbl" Text="ประเภทรายการ"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtKindOfTransaction" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailAssocExam0" runat="server" CssClass="lbl" Text="รหัสรายการ"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtTranSactionCode" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailCompany2" runat="server" CssClass="lbl" Text="เลขที่เช็ค"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtChequeNo" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailAssocExam1" runat="server" CssClass="lbl" Text="จำนวนเงิน"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;">
                                        <span>
                                            <asp:Label ID="lblDetailCompany3" runat="server" CssClass="lbl" Text="รหัสเช็คธนาคาร"></asp:Label>
                                        </span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtChequeBankCode" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">
                                    </td>
                                    <td style="text-align: left;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <asp:Button ID="btnCloseModal" runat="server" Text="ปิด" CssClass="btn" />
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
                    <asp:PostBackTrigger ControlID="btnLoadFile" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>


  </div>
  <div style="clear:left;"></div>

       
</asp:Content>

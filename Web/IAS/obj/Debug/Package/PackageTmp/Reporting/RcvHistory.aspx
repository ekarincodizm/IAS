<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="RcvHistory.aspx.cs" Inherits="IAS.Reporting.RcvHistory" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register src="../UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
     
        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            } else {
                return true;
            }
        }

        function spacialCaractor(evt) {
            var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()1234567890๑๒๓๔฿๕๖๗๘๙๐";
            var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
            var ch = String.fromCharCode(charCode);
            if (iChars.indexOf(ch) == -1) {
                return true;
            }
            else {
                return false;
            }
        }

        function OpenPopup() {
            var ddlLicenseType = document.getElementById('<%=ddlLicenseType.ClientID %>');
            var txtIDCard = document.getElementById('<%=txtIDCard.ClientID %>');
            var txtFirstName = document.getElementById('<%=txtFirstName.ClientID %>');
            var txtLastName = document.getElementById('<%=txtLastName.ClientID %>');
           var LicenseType_Select = ddlLicenseType.options[ddlLicenseType.selectedIndex].value;

                        var qstr = "../Reporting/ReportHistory.aspx?pettition=" + LicenseType_Select +
                       "&IDCard=" + txtIDCard.value +
                       "&FirstName=" + txtFirstName.value +
                       "&LastName=" + txtLastName.value;
            //var qstr = "../Reporting/ReportHistory.aspx";
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');

            }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

    <div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
               <asp:Label Text="แสดงประวัติการดาวน์โหลดใบเสร็จอิเล็กทรอนิกส์" BorderStyle="None" ID="lblHistoryDownload" runat="server" />
                </div>
            </div>






    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                <ContentTemplate>


                    <asp:Panel ID="PnlSearch" runat="server" Width="100%">

                        <div class="box_body_content">
            	        <div class="box_t_h">::ค้นหา</div>
                     
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">

                    
                           <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblTypeReceipt" runat="server" Text="ประเภทใบเสร็จ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; ">
                                        <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblIDCard" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; ">
                                        <asp:TextBox ID="txtIDCard" runat="server" MaxLength="13" CssClass="txt" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    </td>
                                    <td style="width: 20%;" rowspan="2">
                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" 
                                        Width="75px" OnClick="btnSearch_Click" Visible="false" />
                                        <input type="button" class="btn" value="ค้นหา" onclick="OpenPopup()" />
                                        <br /><br />
                                        <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                            OnClick="btnCancle_Click" Width="75px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; ">
                                        <asp:Label ID="lblFirstName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"></asp:TextBox>
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


                    <div class="box_body_content">
            	        <div class="box_t_h"></div>
                             <div style="float:right;padding-right:20px;">
                                    <asp:ImageButton ID="btnExportExcel" Visible="false" runat="server" ImageUrl="~/image/exel.png" onclick="btnExportExcel_Click" 
                                    ToolTip="ดาวน์โหลดข้อมูล"   />
                                </div>
            	        <div class="box_textbox_bg_table" runat="server" id="bluediv">
                	    <div class="box_texbox_add_box">

                    <table width="100%">
                        <tr>
                            <td style="text-align: center;">
                                <asp:GridView ID="gvSearch" runat="server" Width="100%"  CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" ShowHeaderWhenEmpty="true"
                                    EnableTheming="True" AutoGenerateColumns="False" EmptyDataText="ไม่มีข้อมูล...">
                                    <AlternatingRowStyle CssClass="altrow" />
                                    <HeaderStyle BorderStyle="None" />
                                    <PagerStyle HorizontalAlign="Right" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="5%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbllll" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ประเภทใบเสร็จ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseTypeGv" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเสร็จ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseNumberGv" runat="server" Text='<%# Bind("RECEIPT_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อ-นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirstLastNameGv" runat="server" Text='<%# Bind("FLNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สั่งจ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentDateGv" runat="server" Text='<%# Bind("PAYMENT_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่ชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblReceiptDateGv" runat="server" Text='<%# Bind("ORDER_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseNoGv" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="6%"  CssClass="td-center"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmountGv" runat="server" Text='<%# Bind("AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="รายละเอียด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkShowDetail" runat="server" Text="แสดง" CommandName="ShowDetail"
                                                   CommandArgument='<%# Bind("RECEIPT_NO") %>' OnCommand="lnkShowDetail_Command"></asp:LinkButton>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="จำนวนครั้งที่พิมพ์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" CssClass="td-center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmountPrintGv" runat="server" Text='<%# Bind("PRINT_TIMES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView><br />
                            </td>
                        </tr>
                    </table>

                        </div>  
                          <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false" Text="1"></asp:TextBox>
                                                <asp:Label ID="lblParaPage" runat="server" Text="/" Visible = "false"></asp:Label>
                                                <asp:Label ID="txtTotalPage" runat="server" Visible = "false"></asp:Label>
                                                <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;" Visible="true" />
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" MaxLength="4" Width="5%" onpaste="return false;" ondrop="return false;" style="text-align:center" onkeypress="return runScript(event)"></asp:TextBox>&nbsp;
                                                <asp:Button ID="pageGo" runat="server" Visible="false" text="GO" onclick="btnSearch_Click"/>
                                            </td>
                                            <td align="right">
                                                 จำนวน <b><asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label></b> รายการ
                                            </td>
                                        </tr>
                                   </table>

                                      <!--popup-->
                                <asp:Button ID="btn_pop2" runat="server" Style="display: none" />
                                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_pop2"
                                        PopupControlID="Div1"   BackgroundCssClass="popup-background-style"  >
                                    </ajaxToolkit:ModalPopupExtender>
                                      
                                       <div id ="Div1" class="popupContainer">
                                        <table width="100%"  style="margin-bottom: 0px;min-width:700px">
                                            <tr style="border-style: none; border-width: 0px; border-color: inherit; margin: 0px;
                                                padding: 0px;">
                                                <td bgcolor="#99CCFF" colspan="3" style="border: 0px;">
                                                     <table width="100%" style="margin-bottom: 0px">
                                                       <tr>
                                                            <td  bgcolor="#99CCFF" style="width: 100%" align="left">
                                                                <asp:Panel ID="pop_FROM_panel" runat="server" border="0" BorderStyle="None" BorderWidth="0px">
                                                                     <b><asp:Label ID="Label22" runat="server" CssClass="colum_head" Text="รายละเอียดการดาวน์โหลดใบเสร็จอีเล็กทรอนิกส์ เลขที่ใบเสร็จ" /> <u> <asp:Label ID="txtRcv" runat="server"></asp:Label></u></b>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCount" runat="server" Text=""></asp:Label>
                                                                </asp:Panel>
                                                            </td>
                                                            <td bgcolor="#99CCFF" style="width: 5%" class="aligncenter">
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Middle" ImageUrl="~/Images/clear_button.png"
                                                                    Width="10px" ToolTip="ปิดหน้านี้" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                   &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="width:100%"  colspan="3" >
                                                    <asp:GridView ID="pop_FROM_gv" runat="server" AutoGenerateColumns="false" EmptyDataText="ไม่พบข้อมูล"
                                                    HeaderStyle-CssClass="colum_head" RowStyle-CssClass="colum_detail" HorizontalAlign="Center"  
                                                    PagerSettings-Visible="true"  Width="100%">
                                                    <Columns>
                                                      <asp:TemplateField HeaderText="ลำดับ" ItemStyle-Width="30px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPopRunno" Text='<%# Bind("RUN_NO") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="หมายเลขบัตรประชาชน" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPopUserId" Text='<%# Bind("ID_CARD") %>' runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ชื่อ-นามสกุล" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPopUserName" Text='<%# Bind("USER_NAME") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-Width="100px"  Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPopEVENT" Text='<%# Bind("rcvevent") %>' runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="ประเภทสมาชิก" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPopMemberType" Text='<%# Bind("MEMBER_TYPE") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่ดำเนินการ" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPopMemberType" Text='<%# Bind("CREATE_DATE","{0:dd/MM/yyyy}") %>' runat="server" ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <br />
                                       </div>
                                <!--END popup-->
                        </div>
                         
                        </div>
                        
                        <uc2:UCModalError ID="UCModalError1" runat="server" /> 
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="btnExportExcel" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>



    </div>
    <div style="clear:left;"></div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="regApprovCompany.aspx.cs" Inherits="IAS.Register.regApprovCompany" %>
<%@ Register src="../UserControl/ucAttachFileControl.ascx" tagname="ucAttachFileControl" tagprefix="uc1" %>
<%@ Register src="../UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc2" %>
<%@ Register src="../UserControl/UCModalSuccess.ascx" tagname="UCModalSuccess" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Detail" runat="server">
  

<div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
           <asp:Label Text="ลงทะเบียน บริษัท (อนุมัติ)" BorderStyle="None" ID="lblRegister" runat="server" />
                </div>
            </div>

         
    <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
        <asp:UpdatePanel ID="udpDetailCompany" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                
                        <div class="box_body_content">
            	        <div class="box_t_h"><asp:Label ID="lblTitleType" runat="server"></asp:Label></div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">



                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4" align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblCompany" runat="server" Text="บริษัท" CssClass="lbl"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <asp:TextBox runat="server" ID="txtCompany" CssClass="txt" Width="83%" Enabled="false"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span><asp:Label ID="lblCompanyRegister" runat="server" CssClass="lbl" Text="ทะเบียนบริษัท"></asp:Label>
                                    </span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtCompanyRegister" runat="server" Width="230px" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblCompanyTel" runat="server" Text="เบอร์โทรศัพท์หน่วยงาน" CssClass="lbl"></asp:Label>
                                </span>:
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtCompanyTel" runat="server" Width="230px" CssClass="txt" Enabled="false"></asp:TextBox>
                                  <asp:Label ID="lblTelExt" runat="server" CssClass="lbl" Text="ต่อ"></asp:Label>
                                 <asp:TextBox ID="txtCompanyTelExt" runat="server" CssClass="txt-tel-ext" MaxLength="4" Enabled="false"></asp:TextBox>                              
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 30%;">
                                &nbsp;
                            </td>
                        </tr>


                        <tr>
                            <td align="right" rowspan="2" valign="top">
                            <span Class="lbl"> ที่อยู่ :</span>
                            </td>
                            <td align="left" rowspan="2" valign="top">
                                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Height="30px" Width="300px" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>

                             <td align="right">
                            <span Class="lbl">จังหวัด : </span>	
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtProvince" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>



                        <tr>
                            <td align="right">
                           <span Class="lbl">เขต/อำเภอ :</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtAmpher" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>                       
                        </tr>



                          <tr>
                            <td align="right">
                           <span Class="lbl">รหัสไปรษณีย์: </span>	
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPost" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>

                             <td align="right">
                           <span Class="lbl"> แขวง/ตำบล:	</span>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTumbol" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>


                    </table>
            </div>
            </div>
            </div>
            
            <div class="box_body_content">
            <div class="box_t_h"><asp:Label ID="lblTitletypePerson" runat="server"></asp:Label></div>
            <div class="box_textbox_bgadd">
            <div class="box_texbox_add_box">

      
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtMemberType" runat="server" Text="บริษัท" Enabled="false" CssClass="txtreadonly"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtTitleAfterReg" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" MaxLength="50" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="REV1" Display="Dynamic" runat="server" 
                                ControlToValidate="txtFirstName" ErrorMessage="รูปแบบชื่อไม่ถูกต้อง" CssClass="failureNotification"
                                ValidationGroup="CompanyValidationGroup" ValidationExpression="^[ก-๙|a-zA-Z|]{1,50}?$"><asp:Label ID="Label6" runat="server" visible="false">*</asp:Label>
                                </asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" MaxLength="70" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" 
                                ControlToValidate="txtLastName" ErrorMessage="รูปแบบนามสกุลไม่ถูกต้อง" CssClass="failureNotification"
                                ValidationGroup="CompanyValidationGroup" ValidationExpression="^[ก-๙|a-zA-Z|]{1,70}?$"><asp:Label ID="Label7" runat="server" visible="false">*</asp:Label>
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%; height: 17px;">
                                <asp:Label ID="lblTel" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%; height: 17px;">
                                  <asp:TextBox ID="txtTel" runat="server" CssClass="txt-tel" MaxLength="9" Enabled="false"></asp:TextBox>
                                <asp:Label ID="lblTelExt2" runat="server" CssClass="lbl" Text="ต่อ"></asp:Label>
                                <asp:TextBox ID="txtTelExt" runat="server" CssClass="txt-tel-ext" MaxLength="4" Enabled="false"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง" CssClass="failureNotification" ValidationGroup="CompanyValidationGroup" 
                                    ControlToValidate="txtTel" ValidationExpression="^0[0-9]{8}" ><asp:Label ID="Label3" runat="server" visible="false">*</asp:Label>
                                </asp:RegularExpressionValidator>
                   
                            </td>
                            <td style="text-align: right; width: 20%; height: 17px;">
                                <asp:Label ID="lblStartDate0" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%; height: 17px;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr style="text-align: left; width: 30%; height: 17px;">
                                        <td style="text-align: left; width: 35%; height: 17px;">
                                            <asp:RadioButtonList ID="rblSex" runat="server" Enabled="false" RepeatDirection="Horizontal" CssClass="rb">
                                                <asp:ListItem Text="ชาย" Value="M" ></asp:ListItem>
                                                <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="text-align: left; width: 65%; height: 17px;">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                <!--Email ValidationExpression Nattapong@22/8/2556-->
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server"
                                        ControlToValidate="txtEmail" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง" CssClass="failureNotification"
                                        ValidationGroup="CompanyValidationGroup" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><asp:Label ID="Label2" runat="server" visible="false">*</asp:Label>
                                 </asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblIDCard" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtIDNumber" runat="server" CssClass="txt txtidcard" MaxLength="13" Enabled="false"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="reqRegIDNumber" runat="server" ControlToValidate="txtIDNumber" ValidationExpression="^[0-9]{13}?$"
                                        ValidationGroup="CompanyValidationGroup" ErrorMessage="รูปแบบเลขบัตรประชาชนไม่ถูกต้อง" CssClass="failureNotification"><asp:Label id ="a" runat="server" visible="false">*</asp:Label>
                                    </asp:RegularExpressionValidator>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="4">
                                &nbsp;</td>
                        </tr>
                    </table>

                    
             
             </div>
             </div>
             </div>
             

      
                
        
         


            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


        <table style="width: 100%">
                                <tr>
                                    <td>
                                        
                                        <uc1:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
                                        
                                    </td>
                                </tr>
                    </table>


     <div class="box_body_content">
            <div class="box_t_h">
            </div>
            <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">
                    <table align="center" style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 25%;">
                                <div align="center">
                                <asp:Panel ID="PnlResultReg" runat="server" Visible="true">
                                <fieldset style="width:50%; text-align:left" class="fsFixIe" >
                                <legend>ผลการสมัคร</legend>
                                    <table align="center" style="width: 500px;">
                                        <tr>
                                            <td colspan="2" valign="top">
                                                <asp:Label ID="lblResultReg" runat="server" Text="ผลการสมัคร" CssClass="lbl" Visible="false"></asp:Label>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtResultReg" runat="server" CssClass="txt" Height="100px" Width="500px" TextMode="MultiLine" Enabled="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="update" runat="server">
        <ContentTemplate>
        
       

        <div class="box_body_content">
            <div class="box_t_h">
            </div>
            <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">
                    <%--<asp:Button Text="เจ้าหน้าที่ คปภ." BorderStyle="None" ID="Button2" CssClass="Initial"
                        runat="server" />--%><strong>เจ้าหน้าที่ คปภ.</strong>
                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                <asp:Label ID="Label5" runat="server" Text="ผู้อนุมัติ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="Label8" runat="server" Text="วันที่อนุมัติ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:TextBox ID="txtApproval" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                <asp:Label ID="Label9" runat="server" Text="สถานะ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:DropDownList ID="ddlStatusApproval" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" ValidationGroup="regGuestValidationGroup"
                                    OnClick="btnOk_Click" />
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <uc2:UCModalError ID="UCModalError1" runat="server" />
            <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
        </div>

         </ContentTemplate>        
        </asp:UpdatePanel>

    </div>

   



<div style="clear:left;"></div>


</asp:Content>


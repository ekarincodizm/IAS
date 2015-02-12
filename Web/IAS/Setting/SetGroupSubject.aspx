<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SetGroupSubject.aspx.cs" Inherits="IAS.Setting.SetExamGroup" %>
<%@ Register src="../UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc1" %>
<%@ Register src="../UserControl/UCModalSuccess.ascx" tagname="UCModalSuccess" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
     <script type="text/javascript">
         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             } else {
                 return true;
             }
         }
         function spacialCaractor(evt) {
             var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()";
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
         function charEngThai(evt) {
             var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()1234567890๑๒๓๔฿๕๖๗๘๙๐";
             var e = evt || window.event,
                    charCode = evt.charCode || evt.keyCode;
             var ch = String.fromCharCode(charCode);
             return (iChars.indexOf(ch) == -1);
         }         
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content" align="center">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
                      จัดการกลุ่มวิชาสอบ
                </div>
            </div>

            <div style="clear:left;"></div>

            <asp:UpdatePanel ID="UpdateWeb" runat="server">
                <ContentTemplate>
                    <table width="100%" cellpadding="5" cellspacing="5">
                <tr>
                    <td align="right" width="45%">
                        ชื่อกลุ่ม :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtGroupName" runat="server" CssClass="txt"></asp:TextBox><span style="color:Red;">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" Text="*" ControlToValidate="txtGroupName" ErrorMessage="กรุณาป้อนชื่อกลุ่ม"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td align="right">
                        คะแนนที่ผ่าน :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtExamPass" runat="server" MaxLength="2" Width="50px" CssClass="txt" onkeypress="return isNumberKey(event);"></asp:TextBox>%<span style="color:Red;">*</span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="a" Text="*" ControlToValidate="txtExamPass" ErrorMessage="กรุณาป้อนคะแนนที่ผ่าน"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>

            <div align="center">
                <asp:Button ID="btnSave" runat="server" Text="บันทึก" onclick="btnSave_Click" CssClass="btn" />
                <asp:Button ID="btnCancelSave" runat="server" Text="ยกเลิก" OnClick="btnCancelSave_Click" CssClass="btn" />
            </div>
            <br />
            <div align="center">
                <asp:GridView ID="GvExamGroup" runat="server" AutoGenerateColumns="false" Width="500px"
                BorderWidth="1" CssClass="datatable" CellSpacing="-1" GridLines="None">
                <AlternatingRowStyle CssClass="altrow" />
                    <Columns>

                        <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                            <div align="center">
                                <%#Container.DataItemIndex+1 %>
                            </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="รหัส" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="ชื่อกลุ่ม">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%#Bind("GROUP_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="คะแนนที่ผ่าน">
                            <ItemTemplate>
                             <div align="center">
                                <asp:Label ID="lblExamPass" runat="server" Text='<%#Bind("EXAM_PASS") %>'></asp:Label>
                            </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="แก้ไข">
                            <ItemTemplate>
                                 <div align="center">
                                <asp:LinkButton ID="lbtUpdate" runat="server" onclick="lbtUpdate_Click"> <img src="../Images/edit-icon.gif" /> </asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="ลบ">
                            <ItemTemplate>
                             <div align="center">
                               <asp:LinkButton ID="lbtDelete" runat="server" onclick="lbtDelete_Click"><img src="../Images/delete_icon.png" /> </asp:LinkButton>
                             <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" TargetControlID="lbtDelete">
                                        </ajaxToolkit:ConfirmButtonExtender>
                                        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="lbtDelete" Drag="true"
                                            OkControlID="btnYes" CancelControlID="btnNo" PopupDragHandleControlID="pnlPopup" BackgroundCssClass="modalbackground2">
                                        </ajaxToolkit:ModalPopupExtender>

                                        <asp:Panel ID="pnlPopup" runat="server" CssClass="popup_container" Wrap="true">                   
        
                                        <div class="popup_title_confirm">
                                            <asp:Label ID="lblTitle" runat="server" Text="แจ้งเตือน"></asp:Label>
                                        </div>

                                        <div style="padding:15px;">
                                        <table width="90%" cellpadding="3" cellspacing="3">
                                            <tr >
                                                <td align="center" width="20px" style="background:none;border:none;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/info.png" />
                                                </td>
                                                <td  align="center" style="background:none;border:none;">
                                                   คุณต้องการลบข้อมูลใช่หรือไม่
                                                </td>
                                            </tr>
                                        </table>

		    
                                         <div align="center">
                                            <asp:Button ID="btnYes" runat="server" CssClass="btn" Text="ใช่" />
                                            <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="ไม่" />
                                       </div>
          
                                    </div>    

                                </asp:Panel>
                            
                           </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>




                 <div id="divshow" runat="server"></div>
                               <ajaxToolkit:ModalPopupExtender ID="ModalPopupUpdate" runat="server" PopupDragHandleControlID = "pnUpdate" Drag="true" TargetControlID="divshow" 
                PopupControlID="pnUpdate" BackgroundCssClass="modalbackground2">
                </ajaxToolkit:ModalPopupExtender>


                <asp:Panel ID="pnUpdate" runat="server" CssClass="popup_container" Width="500px" Wrap="true">
                     <div class="popup_title_confirm">
                                            <asp:Label ID="lblTitle" runat="server" Text="แก้ไขข้อมูล"></asp:Label>
                                        </div>
                        <br />
                     <table width="100%" cellpadding="3" cellspacing="3">   

                     <tr>
                        <td align="right">
                        ชื่อกลุ่ม :
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtUpdateGroupName" runat="server" Width="200px" CssClass="txt"></asp:TextBox>                           
                            <asp:HiddenField ID="HID" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="b" Text="*" ControlToValidate="txtUpdateGroupName" ErrorMessage="กรุณาป้อนชื่อกลุ่ม"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td align="right">
                            คะแนนที่ผ่าน :
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtUpdatePass" runat="server" MaxLength="2" Width="50px" CssClass="txt" onkeypress="return isNumberKey(event);"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="b" Text="*" ControlToValidate="txtUpdatePass" ErrorMessage="กรุณาป้อนคะแนนที่ผ่าน"></asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="2">
                        
                            <asp:Button ID="btnUpdate" runat="server" Text="แก้ไข" CssClass="btn" 
                                onclick="btnUpdate_Click"  />
                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                onclick="btnCancel_Click" />

                        </td>
                    </tr>
                </table>
                <br />
                </asp:Panel>



                <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />
                <uc1:UCModalError ID="UCModalError1" runat="server" />
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
            

            <br />
            <br />
        <div style="clear:left">
        </div>
    </div>
</asp:Content>

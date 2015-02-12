<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SetSubject.aspx.cs" Inherits="IAS.Setting.SetSubject" %>
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
                      จัดการวิชาสอบ
                </div>
            </div>

            <div style="clear:left;"></div>

<div align="center" style="margin:auto;">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>   
    

<asp:Panel ID="pantype" runat="server" Width="80%">
<fieldset>
    <legend>ประเภทใบอนุญาต</legend>

<table width="100%" cellpadding="3" cellspacing="3">
    <tr>
        <td align="right" width="45%">
            ประเภทใบอนุญาต :
        </td>
        <td align="left" valign="top">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="ddl"
                DataValueField = "Id" DataTextField = "Name" 
                AutoPostBack="true" onselectedindexchanged="ddlType_SelectedIndexChanged">            
            </asp:DropDownList>
            <span style="color:Red;">*</span>
            <asp:RequiredFieldValidator ControlToValidate="ddlType" Text="" ID="RequiredFieldValidator1" ValidationGroup="a" runat="server" ErrorMessage="กรุณาเลือกประเภทใบอนุญาต"></asp:RequiredFieldValidator>
            <asp:Button ID="btnadd" runat="server" Text="เพิ่ม" CssClass="btn" onclick="btnadd_Click" />
        </td>
    </tr>
</table>

</fieldset>
</asp:Panel>

<asp:Panel ID="panSubject" runat="server" Visible="false" Width="80%">
<fieldset>
    <legend>ข้อมูลวิชา</legend>

<table width="100%" cellpadding="3" cellspacing="3">
        <tr id="txtsubjectId" runat="server">
            <td align="right" width="45%">
                รหัสวิชา :
            </td>
            <td align="left">
                <asp:TextBox ID="txtSubjectCode" runat="server" MaxLength="3" CssClass="txt" Width="50px" onkeypress="return isNumberKey(event);"></asp:TextBox>
                <span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ControlToValidate="txtSubjectCode" Text="*" ID="RequiredFieldValidator2" ValidationGroup="a" runat="server" ErrorMessage="กรุณาป้อนรหัสวิชา"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr id="lblSubjectId" runat="server" visible="false">
            <td align="right" width="45%">
                รหัสวิชา :
            </td>
            <td align="left">
               <asp:Label ID="LabelSubjectId" runat="server"></asp:Label>                
            </td>
        </tr>

    <tr>
        <td align="right">
            ชื่อวิชา :
        </td>
        <td align="left">
            <asp:TextBox ID="txtSubjectName" runat="server" Width="300px" CssClass="txt"></asp:TextBox>
            <span style="color:Red;">*</span>
            <asp:RequiredFieldValidator ControlToValidate="txtSubjectName" Text="*" ID="RequiredFieldValidator3" ValidationGroup="a" runat="server" ErrorMessage="กรุณาป้อนชื่อวิชา"></asp:RequiredFieldValidator>
        </td>
    </tr>

        <tr id="trgroup" runat="server">
            <td align="right">
                ชื่อกลุ่ม :
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="ddl" DataValueField = "Id" DataTextField = "Name" >
                </asp:DropDownList>
                <asp:HiddenField ID="hiGroup" runat="server" />
                <span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ControlToValidate="ddlGroup" Text="*" ID="RequiredFieldValidator5" ValidationGroup="a" runat="server" ErrorMessage="กรุณาเลือกกลุ่ม"></asp:RequiredFieldValidator>
            </td>
        </tr>

        <tr id="lableGroup" runat="server" visible="false">
            <td align="right">
                ชื่อกลุ่ม :
            </td>
            <td align="left">
                <asp:Label ID="lblgroup" runat="server"></asp:Label>
            </td>
        
        </tr>

        <tr>
            <td align="right">
                คะแนนสูงสุด :
            </td>
            <td align="left">
                <asp:TextBox ID="txtMaxScore" runat="server" MaxLength="3" CssClass="txt" Width="50px" onkeypress="return isNumberKey(event);"></asp:TextBox>
                <span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ControlToValidate="txtMaxScore" Text="*" ID="RequiredFieldValidator7" ValidationGroup="a" runat="server" ErrorMessage="กรุณาป้อนคะแนนสูงสุด"></asp:RequiredFieldValidator>
            </td>
        </tr>

    


    
    <tr>
        <td align="center" colspan="2">
            <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn" onclick="btnSave_Click"/>
            <asp:Button ID="btnCancelSubject" runat="server" Text="ยกเลิก" CssClass="btn" 
                onclick="btnCancelSubject_Click"/>
        </td>
    </tr>
</table>

</fieldset>
</asp:Panel>

<asp:Panel ID="panGrid" runat="server" Visible="false" Width="80%">
    <fieldset>
        <legend>รายการวิชา</legend>

         <asp:GridView ID="GvSubjectR" runat="server" AutoGenerateColumns="false" BorderWidth="1"
         CssClass="datatable" CellSpacing="-1" GridLines="None" Width="99%">
            <AlternatingRowStyle CssClass="altrow" />
            <Columns>
                <asp:TemplateField HeaderText = "ลำดับ">
                    <ItemTemplate>
                        <div align="center">
                       <%#Container.DataItemIndex+1 %>
                       </div>
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "รหัสวิชา">
                    <ItemTemplate>
                    <div align="center">
                        <asp:Label ID="lblSubjectCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                    </div>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText = "ประเภทใบอนุญาต">
                    <ItemTemplate>
                        <asp:Label ID="lblLicenseTypeName" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "ชื่อวิชา">
                    <ItemTemplate>
                        <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "กลุ่ม">
                    <ItemTemplate>
                        <div align="center">
                        <asp:Label ID="lblGRP_SUBJECT_NAME" runat="server" Text='<%# Bind("GROUP_NAME") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "กลุ่ม" Visible="false">
                    <ItemTemplate>
                        <div align="center">
                        <asp:Label ID="lblGRP_SUBJECT_CODE" runat="server" Text='<%# Bind("GROUP_ID") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "คะแนนที่ผ่าน" Visible="false">
                    <ItemTemplate>
                        <div align="center">
                        <asp:Label ID="lblEXAM_PASS" runat="server" Text='<%# Bind("EXAM_PASS") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText = "คะแนนสูงสุด">
                    <ItemTemplate>
                        <div align="center">
                        <asp:Label ID="lblMAX_SCORE" runat="server" Text='<%# Bind("MAX_SCORE") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText = "รหัสประเภท" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblLicenCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                    </ItemTemplate>
            
                </asp:TemplateField>

                <asp:TemplateField HeaderText="แก้ไข">
                    <ItemTemplate>
                       <div align="center"> <asp:LinkButton ID="lbtUpdate" runat="server" 
                               onclick="lbtUpdate_Click"><img src="../Images/edit-icon.gif" /></asp:LinkButton></div>
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ลบ">
                    <ItemTemplate>
                              <div align="center"> <asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click"><img src="../Images/delete_icon.png" /></asp:LinkButton></div>
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
                                                   คุณต้องการลบ รหัสวิชา  <asp:Label ID="lblCodeConfirm" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label> ชื่อวิชา <asp:Label ID="lblNameConfirm" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label> ใช่หรือไม่
                                                </td>
                                            </tr>
                                        </table>

		    
                                         <div align="center">
                                            <asp:Button ID="btnYes" runat="server" CssClass="btn" Text="ใช่" />
                                            <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="ไม่" />
                                       </div>
          
                                    </div>    

                                </asp:Panel>
                    
                    </ItemTemplate>
                    <ItemStyle Width="60px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblEmpty" runat="server" Text="ไม่พบข้อมูล"></asp:Label>
            </EmptyDataTemplate>
         </asp:GridView>
         <br />
    </fieldset>
</asp:Panel>

</ContentTemplate>
    </asp:UpdatePanel>

</div>

            <uc1:UCModalError ID="UCModalError1" runat="server" />
            <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />


    <br />
    <br />

    </div>
    <div style="clear:left;"></div>

</asp:Content>

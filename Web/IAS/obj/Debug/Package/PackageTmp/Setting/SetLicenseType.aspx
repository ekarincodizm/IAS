<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SetLicenseType.aspx.cs" Inherits="IAS.Setting.SetLicenseType" %>
<%@ Register src="../UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc1" %>
<%@ Register src="../UserControl/UCModalSuccess.ascx" tagname="UCModalSuccess" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">


 <div class="box_content" align="center">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
                      ประเภทใบอนุญาต
                </div>
            </div>

            <div style="clear:left;"></div>


            <table width="100%" cellpadding="5" cellspacing="5">
                <tr>
                    <td align="right">
                        ประเภทตัวแทน :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAgentype" runat="server" DataValueField="AGENT_TYPE" 
                            DataTextField="AGENT_TYPE_DESC" AutoPostBack="True" 
                            onselectedindexchanged="ddlAgentype_SelectedIndexChanged">                        
                        </asp:DropDownList>
                    </td>
                </tr>

                 <tr>
                    <td align="right">
                        ประเภทประกันภัย :
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlInsuran" runat="server">
                            <asp:ListItem Text="ประกันชีวิต" Value="1"></asp:ListItem>
                            <asp:ListItem Text="ประกันวินาศภัย" Value="2"></asp:ListItem>
                            <asp:ListItem Text="ประกันชีวิต/ประกันวินาศภัย" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td align="right" width="45%">
                        ชื่อใบอนุญาต :
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLicenseName" runat="server" Width="300px"></asp:TextBox>
                    </td>
                </tr>

                  <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn" 
                            onclick="btnSave_Click"/>
                    </td>
                </tr>

           
            </table>

    <asp:GridView ID="GvLicense" runat="server" AutoGenerateColumns="false" BorderWidth="1"
    CssClass="datatable" CellSpacing="-1" GridLines="None" Width="600px">
     <AlternatingRowStyle CssClass="altrow" />
        <Columns>
            <asp:TemplateField HeaderText = "ลำดับ">
               <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
               </ItemTemplate>
               <ItemStyle HorizontalAlign="Center" Width="80px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="รหัสประเภท">
                <ItemTemplate>
                    <asp:Label ID="lblTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                </ItemTemplate> 
                <ItemStyle HorizontalAlign="Center" Width="80px" />           
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ชื่อประเภทใบอนุญาต">
                <ItemTemplate>
                    <asp:Label ID="lblNameType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblAgenCode" runat="server" Text='<%# Bind("AGENT_TYPE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblInsuranCode" runat="server" Text='<%# Bind("INSURANCE_TYPE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="แก้ไข">
                <ItemTemplate>
                  <div align="center"> <asp:LinkButton ID="lbtUpdate" runat="server" 
                          onclick="lbtUpdate_Click"><img src="../Images/edit-icon.gif" /> </asp:LinkButton></div>
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="ลบ">
                <ItemTemplate>
                     <div align="center"><asp:LinkButton ID="lbtDelete" runat="server" OnClick="lbtDelete_Click"><img src="../Images/delete_icon.png" /> </asp:LinkButton></div>
                
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
                
                </ItemTemplate>
                <ItemStyle Width="80px" />
            </asp:TemplateField>

        </Columns>
        <EmptyDataTemplate>
            ไม่พบข้อมูล
        </EmptyDataTemplate>
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
                        ประเภทตัวแทน :
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlAgentTypeUpdate" runat="server" DataValueField="AGENT_TYPE" DataTextField="AGENT_TYPE_DESC">                        
                            </asp:DropDownList>                       
                        </td>
                    </tr>

                          <tr>
                        <td align="right">
                        ประเภทประกันภัย :
                        </td>
                        <td align="left">
                           <asp:DropDownList ID="ddlInsuranUpdate" runat="server">
                            <asp:ListItem Text="ประกันชีวิต" Value="1"></asp:ListItem>
                            <asp:ListItem Text="ประกันวินาศภัย" Value="2"></asp:ListItem>
                            <asp:ListItem Text="ประกันชีวิต/ประกันวินาศภัย" Value="3"></asp:ListItem>
                        </asp:DropDownList>                      
                        </td>
                    </tr>

                     <tr>
                        <td align="right">
                        ชื่อใบอนุญาต :
                        </td>
                        <td align="left">
                           <asp:TextBox ID="txtUpdatename" runat="server" Width="250px"></asp:TextBox>
                           <asp:HiddenField ID="hdTypeCode" runat="server" />
                         </td>
                    </tr>

                    <tr>
                        <td align="center" colspan="2">
                        
                            <asp:Button ID="btnUpdate" runat="server" Text="แก้ไข" CssClass="btn" 
                                onclick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                onclick="btnCancel_Click" />

                        </td>
                    </tr>
                </table>
                <br />
                </asp:Panel>

                <uc1:UCModalError ID="UCModalError1" runat="server" />
            <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />

                <br />
                <br />
                <br />
    

    </div>
    <div style="clear:left;"></div>

</asp:Content>

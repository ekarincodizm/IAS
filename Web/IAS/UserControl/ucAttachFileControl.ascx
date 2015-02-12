﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAttachFileControl.ascx.cs"
    Inherits="IAS.UserControl.ucAttachFileControl" %>
    <%@ Register src="UCLoadingProgress.ascx" tagname="UCLoadingProgress" tagprefix="uc1" %>

    <script type="text/javascript">
        function confirmDeleteAttachFile() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

    </script>
     <style type="text/css">
        select:focus, select.expand {
                width: auto;
            }
    </style>  
 <asp:UpdatePanel ID="udpAttachFiles" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <asp:Panel ID="pnlAttachFiles" runat="server" Visible="true" Width="100%">
          

                <div class="box_body_content">
                <div class="box_t_h">::เอกสารแนบ</div>
                <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">


                 <asp:Panel ID="pnlUpload" runat="server" >

           
                    <table style="width: 100%;" align="center">
                    <tr>
                        <td style="text-align: left; width: 20%;">
                            <asp:Label ID="lblDocumentType" runat="server" Text="ประเภทไฟล์แนบ" CssClass="lblbig"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:Label ID="lblAttachFile" runat="server" Text="ไฟล์แนบ" CssClass="lblbig"></asp:Label>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:Label ID="lblRemark" runat="server" Text="หมายเหตุ" CssClass="lblbig"></asp:Label>
                        </td>
                    </tr> 
                    <tr>
                        <td style="text-align: left; width: 20%;">
                        <div style="width:200px;overflow:hidden;">
                            <!-- ResetWidth : got some problem. It cant use on this page, Next times please test before use it. Thank u. -->
                            <%--<asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="ddl" Width="200px" onchange='ResetWidth(this)' onmousedown='SetWidthToAuto(this)' 
                            onblur = 'ResetWidth(this)' onmouseover='ResetWidth(this)'>--%>
                            <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="ddl" Width="100%">
                            </asp:DropDownList>
                        </div>
                        </td> 
                        <td style="text-align: left; width: 30%;">
                            <asp:FileUpload ID="fulAttachFile" runat="server" Width="300px" CssClass="fileUpload" />
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="txt" Width="350px" MaxLength="100"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 10%;">
                            <asp:Button ID="btnUploadFile" runat="server" Text="โหลดไฟล์" CssClass="btn" OnClick="btnUploadFile_Click" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="udpAttachFiles">
                                <ProgressTemplate>
                                    Please wait image uploaded....
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    </table>

                 </asp:Panel>

                 <asp:Panel ID="pnlGridView" runat="server" >


                    <table style="width: 100%;" align="center">
                    <tr>
                        <td style="text-align: center;">

                         <asp:GridView ID="gvAttachFiles" runat="server"  Width="100%" AllowPaging="True"
                                EnableTheming="True" AutoGenerateColumns="False" 
                                CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1"
                                DataKeyNames="ID" ShowHeader="true"
                                OnRowCancelingEdit="gvAttachFiles_RowCancelingEdit" OnRowEditing="gvAttachFiles_RowEditing"
                                OnRowUpdating="gvAttachFiles_RowUpdating" 
                                OnPreRender="gvAttachFiles_PreRender" OnPageIndexChanging="gvAttachFiles_PageIndexChanging"
                                OnRowDataBound="gvAttachFiles_RowDataBound" ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle CssClass="altrow" />

                                <HeaderStyle BorderStyle="None" />
                                <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />

                                <Columns>
                                    <asp:TemplateField  HeaderText="แก้ไข-ยกเลิก" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="lbnOkGv" runat="server" CausesValidation="True" CommandName="Update"
                                                Text="ตกลง"></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="lbnCancelGv" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="ยกเลิก"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbnEditGv" runat="server" CausesValidation="False" CommandName="Edit"
                                                ><img src="../Images/edit-icon.gif" title="แก้ไข" /></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" CssClass="td-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DOCTYPE" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="30%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentCode" runat="server" Text='<%# Bind("ATTACH_FILE_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ประเภทไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocumentName" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAttachFilePath" runat="server" Visible="true" Text='<%# Bind("ATTACH_FILE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หมายเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="30%" />
                                        <ItemTemplate>
                                            <%# Eval("REMARK")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDetailGv" runat="server" Text='<%# Bind("REMARK") %>' CssClass="txt"
                                                Width="350px" MaxLength="100"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="File_Status" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileStatus" runat="server" Visible="true" Text='<%# Bind("FILE_STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="30%" CssClass="td-center" />
                                        <ItemTemplate>
                                   
                                          <asp:LinkButton ID="hplView" runat="server" ><img style="margin-right:9px;" src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                           <asp:LinkButton ID="hplDelete" runat="server" OnClientClick="return confirmDeleteAttachFile();"
                                                OnClick="hplDelete_Click"><img style="margin-right:5px; margin-left:-5px; " src="../Images/delete_icon.png" title="ลบ" /></asp:LinkButton>
                                          <asp:LinkButton ID="hplCancel" runat="server"   OnClick="hplCancel_Click"><img style="margin-right:5px;   margin-left:-5px;  z-index:1000; position:relative;"  src="../Images/x-icon.png" title="ยกเลิก" /></asp:LinkButton>
                                   
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="lblNote" runat="server" ForeColor="Red" Text="หมายเหตุ : สามารถแนบไฟล์ที่มีนามสกุล BMP,GIF,JPG,PNG,TIF,PDF,WORD เท่านั้น "></asp:Label><br />
                             <asp:Label ID="lblNote0" runat="server" ForeColor="Red" Text="หมายเหตุ :  อัพโหลดไฟล์รูปภาพได้สูงสุด 1 MB "></asp:Label><br />
                            <asp:Label ID="lblNote1" runat="server" Text="เอกสารที่ต้องแนบ ได้แก่"></asp:Label><br />
                             <asp:Label ID="lblDocReq" runat="server" ForeColor="Red" ></asp:Label>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
         </div>
         </div>
         </div>
         
        </asp:Panel>
       <!--[if lte IE 8]>
        <script type='text/javascript'>

            function SetWidthToAuto(drpLst) {
                debugger;
            drpLst.style.width = 'auto';
        }

        function ResetWidth(drpLst) {
            debugger;
            drpLst.style.width = '200px';
        }
        </script>
      <![endif]-->
 
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUploadFile" />
    </Triggers>
 </asp:UpdatePanel>

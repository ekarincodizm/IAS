<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAttachFileLicense.ascx.cs" Inherits="IAS.UserControl.ucAttachFileLicense" %>
    <%@ Register src="UCLoadingProgress.ascx" tagname="UCLoadingProgress" tagprefix="uc1" %>
    <script type="text/javascript">
        function confirmDeleteAttachFile() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }
    </script>
    <asp:UpdatePanel ID="udpAttachFiles" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <asp:Panel ID="pnlAttachFiles" runat="server" Visible="true" Width="100%">
            <fieldset>
                <legend class="lbl">เอกสารแนบ</legend>
                 <asp:Panel ID="pnlUpload" runat="server" >
                    <table style="width: 90%;" align="center">
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
                            <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="ddl" Width="200px">
                            </asp:DropDownList>
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
                            <uc1:UCLoadingProgress ID="ucLoading" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="udpAttachFiles">
                                <ProgressTemplate>
                                    Please wait image uploaded...
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    </table>
                 </asp:Panel>
                 <asp:Panel ID="pnlGridView" runat="server" >
                    <table style="width: 90%;" align="center">
                    <tr>
                        <td style="text-align: center;">
                         <asp:GridView ID="gvAttachFiles" runat="server" GridLines="Vertical" Width="100%" AllowPaging="True"
                                BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False" 
                                DataKeyNames="ID" ShowHeader="true"
                                OnRowCancelingEdit="gvAttachFiles_RowCancelingEdit" OnRowEditing="gvAttachFiles_RowEditing"
                                OnRowUpdating="gvAttachFiles_RowUpdating" 
                                OnPreRender="gvAttachFiles_PreRender" OnPageIndexChanging="gvAttachFiles_PageIndexChanging"
                                OnRowDataBound="gvAttachFiles_RowDataBound" ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle CssClass="table_alter_row" />
                                <HeaderStyle BorderStyle="None" />
                                <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                <RowStyle CssClass="table_item_row" />
                                <Columns>
                                    <asp:TemplateField HeaderText="แก้ไข-ยกเลิก" Visible="true" ItemStyle-HorizontalAlign="Center">
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
                                        <ItemStyle Width="10%" />
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
                                        <ItemStyle Width="30%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hplView" runat="server" ><img style="margin-right:8px;" src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                            <asp:LinkButton ID="hplDelete" runat="server" OnClientClick="return confirmDeleteAttachFile();"
                                                OnClick="hplDelete_Click"><img style="margin-right:5px;" src="../Images/delete_icon.png" title="ลบ" /></asp:LinkButton>
                                            <asp:LinkButton ID="hplCancel" runat="server"  OnClick="hplCancel_Click"><img style="margin-right:5px; "  src="../Images/x-icon.png" title="ยกเลิก" /></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="หมายเหตุ : สามารถแนบไฟล์ที่มีนามสกุล BMP,GIF,JPG,PNG,TIF,PDF,WORD เท่านั้น "></asp:Label><br />
                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="กรณีไฟล์รูปภาพ สามารถแนบไฟล์ที่มีนามสกุล BMP,GIF,JPG,PNG,TIF เท่านั้น "></asp:Label><br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Label ID="lblNote0" runat="server" ForeColor="Red" Text="อัพโหลดไฟล์รูปภาพได้สูงสุด 1 MB "></asp:Label><br />
                            <asp:Label ID="lblNote" runat="server" ForeColor="Red" Text="*หลักฐานที่เป็นสำเนากรุณาลงนามรับรองสำเนาถูกต้องให้ครบถ้วน*"></asp:Label><br />
                            <asp:Label ID="lblNote1" runat="server" Text="เอกสารที่ต้องแนบ ได้แก่"></asp:Label><br />
                            <asp:Label ID="lblDocReq" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </td>
                    </tr>
                    </table>
                </asp:Panel>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUploadFile" />
    </Triggers>
 </asp:UpdatePanel>

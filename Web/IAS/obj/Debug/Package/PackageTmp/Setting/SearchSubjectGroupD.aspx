<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SearchSubjectGroupD.aspx.cs" Inherits="IAS.Setting.SearchSubjectGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function NumberOnly(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content" align="center">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
                      ประวัติหลักสูตร
                </div>
            </div>

            <div style="clear:left;"></div>

                <br />
                <br />
                <div align="center">

                <asp:UpdatePanel ID="updateWeb" runat="server">
                    <ContentTemplate>
                  

                     <table width="100%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td align="right" width="45%">
                                ประเภทใบอนุญาต :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="ddl"
                                DataValueField = "Id" DataTextField = "Name" 
                                AutoPostBack="true" onselectedindexchanged="ddlType_SelectedIndexChanged" >                           
                            </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="false" 
                    BorderWidth="1" CssClass="datatable" CellSpacing="-1" GridLines="None"
                         onrowdatabound="gvSearch_RowDataBound" Width="800px">
                         <AlternatingRowStyle CssClass="altrow" />
                        <Columns>

                        <asp:TemplateField HeaderText="ลำดับ">
                                <ItemTemplate>
                                    <asp:Label ID="lblNum" runat="server" Text='<%# Bind("Num") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="รหัสหลักสูตร">
                                <ItemTemplate>
                                    <asp:Label ID="lblCourseName" runat="server" Text='<%# Bind("COURSE_NUMBER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="LICENSE_TYPE_CODE" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                                <ItemTemplate>
                                    <asp:Label ID="lblLisenseTypeName" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                </ItemTemplate>                            
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="วันที่มีผลบังคับใช้">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("START_DATE", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ถึงวันที่">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("END_DATE", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="สถานะ">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="หมายเหตุ">
                                <ItemTemplate>
                                    <asp:Label ID="lblNote" runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="ดูข้อมูลวิชา">
                                <ItemTemplate>
                                    <div align="center">
                                   <asp:LinkButton ID="lbtView" runat="server" onclick="lbtView_Click"><img src ="../Images/search.png" /> </asp:LinkButton>
                                   </div>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="center">
                                ไม่พบข้อมูล
                            </div>
                        </EmptyDataTemplate>

                    </asp:GridView>

                   
               <div id="ShowmodalSubject" runat="server"></div>
              <ajaxToolkit:ModalPopupExtender ID="modalSubject" runat="server"
              PopupControlID="panelSubject" TargetControlID="ShowmodalSubject" BackgroundCssClass="modalbackground2" Drag="true" PopupDragHandleControlID="panelSubject">            
              </ajaxToolkit:ModalPopupExtender>

              <asp:Panel ID="panelSubject" runat="server" CssClass="popup_container" BackColor="White" Wrap="true" Width="700px">
                 <div class="popup_title_confirm">
                       <asp:Label ID="lblTitle" runat="server" Text="ข้อมูลวิชา"></asp:Label>
                 </div>

                 <table width ="100%" cellpadding="5" cellspacing="5">
                    <tr>
                        <td align="right">
                            ประเภทใบอนุญาต :
                        </td>

                        <td align="left">
                            <asp:Label ID="lblLicenseName" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td align="right">
                            วันที่เริ่ม :
                        </td>

                        <td align="left">
                            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td align="right">
                            ถึงวันที่ :
                        </td>

                        <td align="left">
                            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td align="right">
                            สถานะ :
                        </td>

                        <td align="left">
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td align="right">
                            หมายเหตุ :
                        </td>

                        <td align="left">
                            <asp:Label ID="lblNote" runat="server"></asp:Label>
                        </td>
                    </tr>

                    
                 </table>

                 <div align="center">
                <asp:GridView ID="GvSubject" runat="server" AutoGenerateColumns="false" Width="98%"
                BorderWidth="1" CssClass="datatable" CellSpacing="-1" GridLines="None">
                <AlternatingRowStyle CssClass="altrow" />
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText ="รหัสวิชา">
                            <ItemTemplate>             
                                <div align="center">                  
                                <asp:Label ID="lblSubjectCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>                             
                                </div> 
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText ="ชื่อวิชา">
                            <ItemTemplate>                                
                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>                             
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText ="คะแนนสูงสุด">
                            <ItemTemplate>             
                                <div align="center">                   
                                <asp:Label ID="lblMaxScore" runat="server" Text='<%# Bind("MAX_SCORE") %>'></asp:Label>     
                                </div>                        
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    
                </asp:GridView>
                </div>
                <div align="center">
                    <br />
                    <asp:Button ID="btnCancel" runat="server" Text="ปิด" CssClass="btn" />
                    <br />
                    <br />
                </div>
              </asp:Panel>



                    <br />
                    <br />



                    <table>
                        <tr>
                            <td>

                                <asp:Button ID="btn_first" runat="server" Text="<" onclick="btnFirst_Click" CssClass="btn" />
                                <asp:TextBox ID="txtPage" runat="server" Width="25px" Text="1" onkeypress="return NumberOnly(event);" Enabled ="false"></asp:TextBox>/
                                <asp:Label ID="lblCountPage" runat="server"></asp:Label>
                                <asp:Button ID="btn_last" runat ="server" Text=">" CssClass="btn" 
                                    onclick="btn_last_Click"/>
                                จำนวนรายการต่อหน้า
                                <asp:TextBox ID="txtCountRecord" runat="server" Width="25px" Text="20" onkeypress="return NumberOnly(event);"></asp:TextBox>
                                <asp:Button ID="btnShow" runat="server"  CssClass="btn" Text="Go" 
                            onclick="btnShow_Click"/>
                                
                            </td>
                            <td>
                            
                            </td>
                            <td>
                            
                            </td>
                            <td>
                                จำนวนรายการ <asp:Label ID="lblCountRecord" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>


                </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                
            
</div>
<div style="clear:left;"></div>
</asp:Content>

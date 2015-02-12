<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SetExamPlace.aspx.cs" Inherits="IAS.Setting.SetExamPlace" %>
<%@ Register src="../UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc1" %>
<%@ Register src="../UserControl/UCModalSuccess.ascx" tagname="UCModalSuccess" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
 function confirmDelete() {
            return confirm('คุณต้องการลบหรือไม่?');
        }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        #tblName
        {
            width: 97%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

 <asp:Panel ID="PnlMain" runat="server">
            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
 <div class="box_content" align="center">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
                      จัดการสนามสอบ
                </div>
            </div>

            <div style="clear:left;"></div>


            <table width="100%" cellpadding="5" cellspacing="5">
                <tr>
                    <td align="left">
                        <div class="box_t_h">::สนามสอบ</div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div class="box_textbox_bgadd">
                             <table width="100%">
                             <tr>
                                <td style="width:5px"></td>
                                <td><asp:Label ID="lblplaceG" runat="server" Text="สังกัด" ></asp:Label></td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlPlaceGroup" AutoPostBack="true"
                                        onselectedindexchanged="ddlPlaceGroup_SelectedIndexChanged">
                                        <asp:ListItem Text="เลือก" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="หน่วยงานจัดสอบ" Value="G"></asp:ListItem>
                                        <asp:ListItem Text="สมาคม" Value="A"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td><asp:Label runat="server" Text="หน่วยงานจัดสอบ/สมาคม"></asp:Label></td>
                                <td><asp:DropDownList runat="server" ID="ddlPlaceG" Width="183px"  
                                        AutoPostBack="true" onselectedindexchanged="ddlPlaceG_SelectedIndexChanged"
                                       ></asp:DropDownList>
                                </td>
                                <td style="width:5px"></td>
                                <td><asp:Label runat="server" Text="จังหวัด"></asp:Label></td>
                                <td><asp:DropDownList runat="server" ID="ddlProvince" Width="203px" AutoPostBack="true"
                                        onselectedindexchanged="ddlProvince_SelectedIndexChanged" ></asp:DropDownList>
                                </td>
                                <td >
                                    <asp:Button runat="server" ID="btn_Search" Text="ค้นหา" Width="80" 
                                        onclick="btn_Search_Click" />
                                 </td>
                                <td align="center">
                                    <asp:Button ID="btn_Clear" runat="server" onclick="btn_Clear_Click" 
                                        Text="ยกเลิก" Width="80" />
                                </td>
                                <td>
                                    <asp:Button ID="btn_Add" runat="server" onclick="btn_Add_Click" 
                                        Text="เพิ่มข้อมูล" Visible="false" Width="80" />
                                </td>
                                <td style="width:5px"></td>
                            </tr>
                            
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                    
                           
                       
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td >
                        <table width="100%">
                            <tr>
                                <td style="width:5px" rowspan="2"></td>
                                <td align="center">
                                    <asp:GridView ID="gvPlace" runat="server" GridLines="None" Width="80%" CssClass="datatable"
                                            EnableTheming="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" BorderWidth="1px"
                                            CellSpacing="-1" AllowSorting="True" PageSize="20" >
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>     
                                                            <div style="text-align:center"> ไม่พบข้อมูล...</div>
                                                            </EmptyDataTemplate>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                           
                                                           <%-- <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHeadRequestNO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRownum" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รหัสสนามสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("PLACECODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อสนามสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="20%"  />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTxtTime" runat="server" Text=' <%# Bind("PLACENAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รองรับผู้สมัครสอบได้สูงสุด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%"  CssClass="td-center"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEnd" runat="server" Text='<%# Bind("SEAT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สนามสอบสำหรับใช้รวม" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="20%"  CssClass="td-center"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStart" runat="server" Text='<%# Bind("FREE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>           
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                 <ItemTemplate> 
                                                                 <asp:LinkButton ID="lbtnEdit" runat="server"  
                                                                        onclick="lbtnEdit_Click"><img src="../Images/edit-icon.gif" alt="" /></asp:LinkButton>
                                                                    <asp:LinkButton ID="hplCancel" runat="server" OnClick="hplCancel_Click" OnClientClick="return confirmDelete();"><img src="../Images/delete_icon.png" title="" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                </td>
                                <td style="width:5px" rowspan="2"></td>
                            </tr>
                            <tr>
                                <td>
                             <%--     <table width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false"
                                                                    Text="1"></asp:TextBox>
                                                                <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                                <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;"
                                                                    Visible="true" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" Style="text-align: center"
                                                                    MaxLength="4" ondrop="return false;" AutoComplete="Off" onpaste="return false;"
                                                                    Width="5%" onkeypress="return runScript(event) "></asp:TextBox>&nbsp;
                                                                <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearch_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <div runat="server" id="div_totalPage">
                                                                    จำนวน <b>
                                                                        <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label></b> รายการ</div>
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                                    <asp:Panel ID="PanalPage" runat="server" Width="100%" Visible="false">
                                                    <table width="80%" align="center" >
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false"
                                                                    Text="1" Height="22px"></asp:TextBox>
                                                                <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                                <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;"
                                                                    Visible="true" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" Style="text-align: center"
                                                                    MaxLength="4" ondrop="return false;" AutoComplete="Off" onpaste="return false;"
                                                                    Width="5%" onkeypress="return runScript(event) "></asp:TextBox>&nbsp;
                                                                <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearchGo_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <div runat="server" id="div_totalPage">
                                                                    จำนวน <b>
                                                                        <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label></b> รายการ</div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    
                </tr>

               

           
            </table>

       
                <uc1:UCModalError ID="UCModalError1" runat="server" />
            <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />

            

                <br />
                <br />
                <br />
    

    </div>

            <asp:UpdatePanel ID="UplExamSchedule" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="display: none">
                    <asp:Button ID="btnExamSchedule" runat="server" Text="Open" />
                </div>
                <ajaxToolkit:ModalPopupExtender ID="ModalExamSchedule" runat="server" CancelControlID="btnExamScheduleCancel"
                    OkControlID="btnScheduleOkay" TargetControlID="btnExamSchedule" PopupControlID="pnlModalExamSchedule"
                    PopupDragHandleControlID="PopupHeader" Drag="true" BackgroundCssClass="modalbackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlModalExamSchedule" Style="display: block" Width="90%" runat="server"
                    CssClass="modalpopup" TabIndex="100">
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::เพิ่มข้อมูล</div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                        <table runat="server" id="tblName" visible="false" width="100%">
                           
                            <tr>
                                <td align="right"><asp:Label Id="lblCode" runat="server" Text="รหัสสนามสอบ"></asp:Label></td>
                                <td align ="left"><asp:TextBox runat="server" ID="txtCode"  onkeyup="this.value=this.value.replace(/[^0-9]/g,'');" MaxLength = "5"></asp:TextBox></td>
                                <td align="right"><asp:Label ID="Label1" runat="server" Text="ชื่อสนามสอบ"></asp:Label></td>
                                <td align="left" > 
                                    <asp:TextBox runat="server"  ID="txtPlace" Width="250px" MaxLength = "60"></asp:TextBox> 
                                </td>
                                
                                <td align="right"><asp:Label ID="seat" runat="server" Text="รองรับผู้สมัครสอบได้สูงสุด (คน)"></asp:Label></td>
                                <td align="left"><asp:TextBox runat="server" ID="txtSeat" Width="100px"  onkeyup="this.value=this.value.replace(/[^0-9]/g,'');" MaxLength = "5"></asp:TextBox></td>
                             
                                <td  align="center"><asp:CheckBox ID="chkFree" runat="server" Text="สนามสอบนี้สำหรับใช้รวม" Visible="false"/></td>
                                <td  align="center" >
                                    <asp:Button runat="server" ID="btn_Save" Text="บันทึก" Width="80" 
                                         onclick="btn_Save_Click"/>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btn_Cancle" Text="ยกเลิก" Width="80" 
                                         onclick="btn_Cancle_Click"/>
                                 </td>
                            </tr>
                                                        
                            </table>
                            </div>
                        </div>
                    </div>
                               </asp:Panel>
            </ContentTemplate>
            </asp:UpdatePanel>
            </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    <div style="clear:left;"></div>

</asp:Content>

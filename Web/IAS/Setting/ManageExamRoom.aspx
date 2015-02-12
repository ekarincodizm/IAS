<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="ManageExamRoom.aspx.cs" Inherits="IAS.Setting.ManageExamRoom" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagPrefix="uc" TagName="UCModalSuccess" %>
<%@ Register Src="~/UserControl/UCModalError.ascx" TagPrefix="uc" TagName="UCModalError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    function runScript(e) {
        var charCode = (e.which) ? e.which : e.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        else if (charCode == 13) {
            return false;
        }
        return true;
    }
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style type="text/css">
        .style3
        {
            width: 292px;
        }
        .style4
        {
            width: 278px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
       <asp:Panel ID="PnlMain" runat="server">

       <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
               <asp:UpdatePanel ID="upn" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="จัดการห้องสอบ" BorderStyle="None" ID="lblExamRoom" runat="server" />
            </div>
        </div>
           
       
            <center>
            <br />
                    <table width="100%">
                         <tr>
                            <td style="width:3%"></td>
                              <td align="center" style="width:94%" >
                                 <div class="box_textbox_bgadd" >
                                    <table width="100%">                                                                                                                                                                                                                                                     <table width="100%">
                                    <tr>
                                <td align="center" style="width:94%" >
                                 <div class="box_textbox_bgadd" >
                                    <table width="100%">                                                                                                                                                                                                                                                     <table width="100%">
                                    <tr>
                        
                        <td align="right">&nbsp;</td>
                         <td align="right">สังกัด</td>
                        <td>
                            <asp:DropDownList ID="ddlGroupType" runat="server" AutoPostBack="true" CssClass="ddl" 
                                onselectedindexchanged="ddlGroupType_SelectedIndexChanged" 
                                Width="190px">
                                      <asp:ListItem Text="เลือก" Selected="True" Value=""></asp:ListItem>
                                      <asp:ListItem Text="หน่วยงานจัดสอบ" Value="7" ></asp:ListItem>
                                      <asp:ListItem Text="สมาคม" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" 
                                ControlToValidate="ddlGroupType" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                                <td><asp:Label ID="Label1" runat="server" Text="หน่วยงานจัดสอบ/สมาคม"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlExamPlaceGroup" runat="server" AutoPostBack="true" CssClass="ddl"
                                Width="190px" 
                                onselectedindexchanged="ddlExamPlaceGroup_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlExamPlaceGroup" runat="server" ErrorMessage="*" 
                                ControlToValidate="ddlExamPlaceGroup" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    
                        <td align="right">สนามสอบ</td>
                        <td>
                            <asp:DropDownList ID="ddlExamPlace" runat="server" CssClass="ddl"
                               Width="188px" onselectedindexchanged="ddlExamPlace_SelectedIndexChanged"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlExamPlace" runat="server" ErrorMessage="*" 
                                ControlToValidate="ddlExamPlace" ValidationGroup="1"></asp:RequiredFieldValidator>

                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
                                 Text="ค้นหา" onclick="btnSearch_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btn_Cancle" runat="server" CssClass="btn" 
                                onclick="btn_Cancle_Click" Text="ยกเลิก" />
                        </td>
                        <td>
                             <asp:Button ID="btnPopUp" runat="server" CssClass="btn" 
                                 onclick="btnPopUp_Click" Text="เพิ่มห้องสอบ" Visible="false" />
                        </td>
                    </tr>
                                     </table>
                                </div>
                                </td>
                             <td style="width:3%"><asp:Label ID="lbltotalSeat" runat="server" Visible="false"></asp:Label></td>
                            </tr>
                  </table>
                    
                        </div>
                              </td>
                              </tr>
                         </table>
                         <asp:UpdatePanel ID="UpdatePanelGridviw" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                    <table width="100%">
                    <tr>
                        <td colspan="7" align="center">
                        
                            <asp:GridView ID="gvExamRoom" Width="80%" runat="server" AutoGenerateColumns="false" CssClass="datatable"  ShowHeaderWhenEmpty="True" AllowSorting="True" >
                              <AlternatingRowStyle CssClass="altrow" />
                                        <HeaderStyle BorderStyle="None" HorizontalAlign="Center" />
                             <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>     
                                            <div style="text-align:center"> ไม่พบข้อมูล...</div>
                              </EmptyDataTemplate>
                                <Columns>
                                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="5%" CssClass="td-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRownum" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                </asp:TemplateField>
                                    <%--<asp:TemplateField>
                                        <HeaderTemplate>ลำดับ</HeaderTemplate>
                                        <ItemStyle CssClass="td-center" />
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField Visible="false">
                                        <HeaderTemplate>หน่วยงานจัดสอบ</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlaceGroupCode" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_CODE") %>'></asp:Label>
                                            <asp:Label ID="lblPlaceGroupName" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>สถานที่สอบ</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                            <asp:Label ID="lblPlaceName" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>จำนวนที่นั่งทั้งหมด</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeatTotal" runat="server" Text='<%# Bind("SEAT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>รหัสห้องสอบ</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodeRoom" runat="server" Text='<%# Bind("EXAM_ROOM_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>ชื่อห้องสอบ</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameRoom" runat="server" Text='<%# Bind("EXAM_ROOM_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>จำนวนที่นั่ง</HeaderTemplate>
                                        <ItemStyle  CssClass="td-center"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeatAmount" runat="server" Text='<%# Bind("SEAT_AMOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>ดำเนินการ</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnEdit" runat="server" 
                                                onclick="lbtnEdit_Click"><img src="../Images/edit-icon.gif" alt="" /></asp:LinkButton>&nbsp;
                                            <asp:LinkButton ID="lbtnDel" runat="server" Text="Delete" 
                                                onclick="lbtnDel_Click" OnClientClick="return confirm('ยืนยันการลบข้อมูล?')"><img src="../Images/delete_icon.png" alt="" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                         <ItemStyle CssClass="td-center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                          
                        </td>
                    </tr>
                    <tr>
                        <td>
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
                </ContentTemplate>
                  </asp:UpdatePanel>
            </center>
        </ContentTemplate>
        </asp:UpdatePanel>
         <asp:UpdatePanel ID="uplPopUp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                    <div style="display: none">
                        <asp:Button ID="btnModal" runat="server" Text="Open" />
                    </div>
                    <ajaxToolkit:ModalPopupExtender ID="mpeExamRoom" runat="server" 
                            OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                            Drag="true" BackgroundCssClass="modalbackground">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlModal" Style="display: block; width:550px;" runat="server" CssClass="modalpopup">
                        <div class="box_body_content">
                            <div class="box_t_h">::ข้อมูลห้องสอบ</div>
                            <div class="box_textbox_bgadd">
                                <div class="box_texbox_add_box">
                                    <table>
                                        <tr>
                                            <td class="style4">จำนวนสูงสุดที่รองรับได้ในห้องสอบนี้</td>
                                            <td><asp:Label ID="lblSeatAmountPlace" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="style4">รหัสห้องสอบ</td>
                                            <td>
                                                <asp:TextBox ID="txtCodeRoom" runat="server" CssClass="textbox" onpaste="return false;" ondrop="return false;" AutoComplete="Off"  onkeypress="return runScript(event);" MaxLength="6"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtCodeRoom" runat="server" ErrorMessage="*" 
                                                    ControlToValidate="txtCodeRoom" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">ชื่อห้องสอบ</td>
                                            <td>
                                                <asp:TextBox ID="txtNameRoom" runat="server" CssClass="textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtNameRoom" runat="server" ErrorMessage="*" 
                                                    ControlToValidate="txtNameRoom" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style4">จำนวนที่นั่ง</td>
                                            <td>
                                                <asp:TextBox ID="txtSeatAmount" runat="server" onkeypress="return runScript(event);" onpaste="return false;" ondrop="return false;" AutoComplete="Off"  CssClass="textbox" MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtSeatAmount" runat="server" ErrorMessage="*" 
                                                    ControlToValidate="txtSeatAmount" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:Label ID="lblOldSeat" runat="server" Visible="false"></asp:Label>
                                            </td> 
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btnSave" runat="server" CssClass="btn" onclick="btnSave_Click" 
                                                    Text="บันทึก" ValidationGroup="1" />&nbsp;
                                                <asp:Button ID="btnUpdate" runat="server" Text="แก้ไข" CssClass="btn" ValidationGroup="1"
                                                    onclick="btnUpdate_Click" />&nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                                    onclick="btnCancel_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>   
                    <uc:UCModalError runat="server" ID="UCError" />
                    <uc:UCModalSuccess runat="server" ID="UCSuccess" />
            </ContentTemplate>
        </asp:UpdatePanel>
            
          
        </asp:Panel>
   </div>
</asp:Content>

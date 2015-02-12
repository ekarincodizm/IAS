<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SetExamPlaceGroup.aspx.cs" Inherits="IAS.Setting.SetExamPlaceGroup" %>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="จัดการข้อมูลหน่วยงานจัดสอบ" BorderStyle="None" ID="lblManageAssociation" runat="server" />
            </div>
        </div>

    <asp:UpdatePanel ID="uplAssociation" runat="server" UpdateMode="Conditional">
        <ContentTemplate><div class="clear"></div>
        <br /><br />
        <div class="box_body_content">
            <div class="box_t_h">
                                ::จัดการข้อมูลหน่วยงานจัดสอบ</div>
                     <table width="100%">
                         <tr>
                            <td style="width:3%"></td>
                            <td style="width:92%">
                                <div class="box_textbox_bgadd" style="width:100%">
                                    <fieldset> ค้นหาข้อมูล 
                                        <table width="100%">
                                            <tr><td colspan="4"></td></tr>
                                            <tr>
                                                <td align="right"> <asp:Label ID="lbl1" Text="รหัสหน่วยงานจัดสอบ" runat="server"></asp:Label></td>
                                                <td align="left"><asp:TextBox ID="txtIdAss" runat="server" onkeypress="return runScript(event);" CssClass="textbox" MaxLength="3"></asp:TextBox>  </td>
                                                 <td align="right"> <asp:Label ID="Label1" Text="ชื่อหน่วยงานจัดสอบ" runat="server"></asp:Label></td>
                                                <td align="left"><asp:TextBox ID="txtNameAss" runat="server" Width="288px" CssClass="textbox" ></asp:TextBox>  </td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td align="right"> <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" 
                                                        onclick="btnSearch_Click" /></td>
                                                <td align="center"><asp:Button ID="btnClear" runat="server" Text="ยกเลิก" 
                                                        CssClass="btn" onclick="btnClear_Click1" /></td>
                                                <td>
                                                    <asp:Button ID="btnPopUp" runat="server" Text="เพิ่มข้อมูล" CssClass="btn"  
                                                    onclick="btnPopUp_Click" />
                                                </td>
                                            </tr>
                                              
                                            
                                            <tr><td colspan="4"></td></tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </td>
                            <td style="width:5%"></td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center" style="width:100%">
                               <asp:GridView ID="gvAssociation" GridLines="None" runat="server" AutoGenerateColumns="false" 
                                 CssClass="datatable" Width="70%" ShowHeaderWhenEmpty="true"
                                  BorderWidth="1px" CellSpacing="-1" AllowSorting="True">
                                <AlternatingRowStyle CssClass="altrow" />
                                <HeaderStyle BorderStyle="None" />
                                 <EmptyDataTemplate>
                                        <center>ไม่พบข้อมูล...</center>
                                    </EmptyDataTemplate>
                                <PagerStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle Width="5%" CssClass="td-center" />
                        <ItemTemplate>
                            <asp:Label ID="lblRownum" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>รหัสหน่วยงานจัดสอบ</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAssociationCode" runat="server"  Text='<%# Bind("exam_place_group_code") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="20%" CssClass="td-center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>ชื่อหน่วยงานจัดสอบ</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblAssociationName" runat="server" Text='<%# Bind("exam_place_group_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>ดำเนินการ</HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEdit" runat="server"  
                            onclick="lbtnEdit_Click"><img src="../Images/edit-icon.gif" alt="" /></asp:LinkButton>
                        <asp:LinkButton ID="lbtnDelete" runat="server" Text="Delete" OnClientClick="return confirm('ยืนยันการลบข้อมูล?')"
                            onclick="lbtnDelete_Click"><img src="../Images/delete_icon.png" alt="" /></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="10%" CssClass="td-center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="70%" align="center">
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
                            </td>
                        </tr>
                        <tr><td></td></tr>
                        <tr><td></td></tr>
                    </table>
        </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    
    <asp:UpdatePanel ID="UplPopUp" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="display: none">
                <asp:Button ID="btnModal" runat="server" Text="Open" />
            </div>
            <ajaxToolkit:ModalPopupExtender ID="mpeAssociation" runat="server" CancelControlID="btnCancel"
                    OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                    Drag="true" BackgroundCssClass="modalbackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlModal" Style="display: block; width:550px;" runat="server" CssClass="modalpopup">
                <div class="box_body_content">
                    <div class="box_t_h">::ข้อมูลหน่วยงานจัดสอบ</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table>
                                <tr>
                                    <td width="150px">รหัสหน่วยงานจัดสอบ</td>
                                    <td>
                                        <asp:TextBox ID="txtAssociationCode" runat="server"  onkeypress="return runScript(event);" CssClass="textbox" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqCode" runat="server" ErrorMessage="*" 
                                            ControlToValidate="txtAssociationCode" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ชื่อหน่วยงานจัดสอบ</td>
                                    <td>
                                        <asp:TextBox ID="txtAssociationName" runat="server" CssClass="textbox" Width="350px" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reaName" runat="server" ErrorMessage="*" 
                                            ControlToValidate="txtAssociationName" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                               
                                
                            </table>

                            
                            <center>
                                <asp:Button ID="btnSave" runat="server" Text="บันทึก" onclick="btnSave_Click" ValidationGroup="1" CssClass="btn" />
                                <asp:Button ID="btnUpdate" runat="server" Text="แก้ไข" ValidationGroup="1" onclick="btnUpdate_Click" CssClass="btn" />
                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                    onclick="btnCancel_Click" />
                            </center>
                        </div>
                    </div>
                </div>
            </asp:Panel>
                <div align="center" style="width: 500px">
                   <%-- <asp:Button ID="btnNDel" runat="server" />
                    <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe"
                        TargetControlID="btnNDel">
                    </ajaxToolkit:ConfirmButtonExtender>--%>
                    <%--<ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
                        TargetControlID="btnNDel" Drag="true" OkControlID="btnYes" CancelControlID="btnNo"
                        PopupDragHandleControlID="pnlPopup" BackgroundCssClass="modalbackground2">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="popup_container" Wrap="true">
                        <div class="popup_title_confirm">
                            <asp:Label ID="lblTitle" runat="server" Text="แจ้งเตือน"></asp:Label>
                        </div>
                        <div style="padding: 15px;">
                            <table width="90%" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td align="center" width="20px" style="background: none; border: none;">
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/info.png" />
                                    </td>
                                    <td align="center" style="background: none; border: none;">
                                        รอบสอบนี้ตรงกับวันหยุด กดยืนยันเพื่อสร้าง
                                    </td>
                                </tr>
                            </table>
                            <div align="center">
                                <asp:Button ID="btnYes" runat="server" CssClass="btn" Text="ยืนยัน" 
                                    OnClick="btnYes_Click"  />
                                <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="ยกเลิก" />
                            </div>
                        </div>
                    </asp:Panel>--%>
                </div>


            <uc:UCModalError runat="server" ID="UCError" />
            <uc:UCModalSuccess runat="server" ID="UCSuccess" />


                <%--<div align="center" style="width: 500px">
                    <asp:Button ID="Button1" runat="server" />
                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" DisplayModalPopupID="mpe"
                        TargetControlID="btnNDel">
                    </ajaxToolkit:ConfirmButtonExtender>
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlPopup"
                        TargetControlID="btnNDel" Drag="true" OkControlID="btnYes" CancelControlID="btnNo"
                        PopupDragHandleControlID="pnlPopup" BackgroundCssClass="modalbackground2">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="Panel1" runat="server" CssClass="popup_container" Wrap="true">
                        <div class="popup_title_confirm">
                            <asp:Label ID="Label2" runat="server" Text="แจ้งเตือน"></asp:Label>
                        </div>
                        <div style="padding: 15px;">
                            <table width="90%" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td align="center" width="20px" style="background: none; border: none;">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/info.png" />
                                    </td>
                                    <td align="center" style="background: none; border: none;">
                                        รอบสอบนี้ตรงกับวันหยุด กดยืนยันเพื่อสร้าง
                                    </td>
                                </tr>
                            </table>
                            <div align="center">
                                <asp:Button ID="Button2" runat="server" CssClass="btn" Text="ยืนยัน" 
                                    OnClick="btnYes_Click"  />
                                <asp:Button ID="Button3" runat="server" CssClass="btn" Text="ยกเลิก" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>--%>

        </ContentTemplate>
    </asp:UpdatePanel>

  
   </div>
</asp:Content>

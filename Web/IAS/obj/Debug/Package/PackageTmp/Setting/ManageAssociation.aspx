<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="ManageAssociation.aspx.cs" Inherits="IAS.Setting.ManageAssociation" %>
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
    <style type="text/css">
        .modalpopupMessageSE {z-index:99994 !important;}
        .modalBackgroundSE {z-index:99995 !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="จัดการข้อมูลสมาคม" BorderStyle="None" ID="lblManageAssociation" runat="server" />
            </div>
        </div>

    <asp:UpdatePanel ID="uplAssociation" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <br /><br />
        <div class="box_body_content">
            <div class="box_t_h">
                                ::จัดการข้อมูลสมาคม</div>
                     <table width="100%">
                         <tr>
                            <td style="width:3%"></td>
                            <td style="width:92%">
                                <div class="box_textbox_bgadd" style="width:100%">
                                    <fieldset> ค้นหาข้อมูล 
                                        <table width="100%">
                                            <tr><td colspan="4"></td></tr>
                                            <tr>
                                                <td align="right"> <asp:Label ID="lbl1" Text="รหัสสมาคม" runat="server"></asp:Label></td>
                                                <td align="left"><asp:TextBox ID="txtIdAss" runat="server" onkeypress="return runScript(event);" CssClass="textbox" MaxLength="5"></asp:TextBox>  </td>
                                                 <td align="right"> <asp:Label ID="Label1" Text="ชื่อสมาคม" runat="server"></asp:Label></td>
                                                <td align="left"><asp:TextBox ID="txtNameAss" runat="server" Width="288px" CssClass="textbox" MaxLength="100" ></asp:TextBox>  </td>
                                                
                                            </tr>
                                              <tr>
                                                <td align="right">ประเภทประกอบธุรกิจ</td>
                                                 <td> 
                                        <asp:DropDownList ID="ddlCType" runat="server" CssClass="ddl" >
                                            <asp:ListItem Text="เลือก" Value=""></asp:ListItem>
                                            <asp:ListItem Text="ประกันชีวิต" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="ประกันวินาศภัย" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="ประกันชีวิตและวินาศภัย" Value="03"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                                <td align="right">ประเภทธุรกิจ</td>
                                                <td>
                                        <asp:DropDownList ID="ddlAtype" runat="server" CssClass="ddl" >
                                            <asp:ListItem Text="เลือก" Value=""></asp:ListItem>
                                            <asp:ListItem Text="ตัวแทน" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="นายหน้า" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="ตัวแทนและนายหน้า" Value="03"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                            </tr>
                                            <tr>
                                                <td align="right">สถานะ</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" >
                                                        <asp:ListItem Text="เลือก" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="ใช้งาน" Value="Y"></asp:ListItem>
                                                        <asp:ListItem Text="ไม่ใช้งาน" Value="N"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td align="right"> <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" 
                                                        onclick="btnSearch_Click" /></td>
                                                <td align="center"><asp:Button ID="btnClear" runat="server" Text="ยกเลิก" 
                                                        CssClass="btn" onclick="btnClear_Click" /></td>
                                                <td>
                                                    <asp:Button ID="btnPopUp" runat="server" Text="เพิ่มรายชื่อสมาคม" CssClass="btn"  
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
                            <td width="95%" align="center">
                               <asp:GridView ID="gvAssociation" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" 
                                    CssClass="datatable" Width="93%" onrowdatabound="gvAssociation_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>ลำดับ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNum" runat="server"  Text='<%# Bind("num") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" CssClass="td-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>รหัสสมาคม</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAssociationCode" runat="server"  Text='<%# Bind("ASSOCIATION_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" CssClass="td-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>ชื่อสมาคม</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAssociationName" runat="server" Text='<%# Bind("ASSOCIATION_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>ประเภทประกอบธุรกิจ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompType" runat="server" Text='<%# Bind("COMP_TYPE") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblCompTypeName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>ประเภทธุรกิจ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAgentType" runat="server" Text='<%# Bind("AGENT_TYPE") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblAgentTypeName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>สถานะ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAssocActive" runat="server" Text='<%# Bind("ACTIVE") %>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblAssocActiveName" runat="server"></asp:Label>
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
                                    <EmptyDataTemplate>
                                        <center>ไม่พบข้อมูล...</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <br/>
                                <div id="ctrPage" runat="server" style="text-align:left; width:93%;">
                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" 
                                            onclick="btnPreviousGvSearch_Click" />
                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false" Text="1"></asp:TextBox>
                                    <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="true"></asp:Label>
                                    <asp:Label ID="txtTotalPage" runat="server" Visible="true"></asp:Label>
                                    <asp:Button ID="btnNextGvSearch" runat="server" Text="&gt;" Visible="true" 
                                            onclick="btnNextGvSearch_Click" />
                                        &nbsp;&nbsp;&nbsp;
                        
                                    <span id="spanRowsPerPage" runat="server">
                                    <asp:Label ID="lblRowsPerPage" runat="server" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                    <asp:TextBox ID="txtRowsPerpage" runat="server" Style="text-align: center" CssClass="txt"
                                        MaxLength="4" onkeypress="return runScript(event)" onpaste="return false;" AutoComplete="Off"
                                        ondrop="return false;" Width="5%"></asp:TextBox>&nbsp;
                                    <asp:Button ID="pageGo" runat="server" Text="GO" 
                                        onclick="pageGo_Click" />
                                    </span>

                                    <span style="float:right; margin-right:20px;">
                                        <asp:Label ID="lblTotalRows" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="clear"></div>
                            </td>
                        </tr>
                    </table><br /><br />
                    
        </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    
    <asp:UpdatePanel ID="UplPopUp" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div style="display: none">
                <asp:Button ID="btnModal" runat="server" Text="Open" />
            </div>
            <ajaxToolkit:ModalPopupExtender ID="mpeAssociation" runat="server" CancelControlID="btnCancel"
                    OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                    Drag="true" BackgroundCssClass="modalbackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlModal" Style="display: block; width:550px; z-index:99990 !important;" runat="server" CssClass="modalpopup">
                <div class="box_body_content">
                    <div class="box_t_h">::ข้อมูลสมาคม</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table>
                                <tr>
                                    <td width="150px">รหัสสมาคม</td>
                                    <td>
                                        <asp:TextBox ID="txtAssociationCode" runat="server"  onkeypress="return runScript(event);" CssClass="textbox" MaxLength="3"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqCode" runat="server" ErrorMessage="*" CssClass="failureNotification"
                                            ControlToValidate="txtAssociationCode" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ชื่อสมาคม</td>
                                    <td>
                                        <asp:TextBox ID="txtAssociationName" runat="server" CssClass="textbox" Width="320px" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reaName" runat="server" ErrorMessage="*"  CssClass="failureNotification"
                                            ControlToValidate="txtAssociationName" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ประเภทประกอบธุรกิจ</td>
                                    <td>
                                        <asp:DropDownList ID="ddlCompType" runat="server" CssClass="ddl">
                                            <asp:ListItem Text="ประกันชีวิต" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="ประกันวินาศภัย" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="ประกันชีวิตและวินาศภัย" Value="03" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ประเภทธุรกิจ</td>
                                    <td>
                                        <asp:DropDownList ID="ddlAgentType" runat="server" CssClass="ddl">
                                            <asp:ListItem Text="ตัวแทน" Value="01"></asp:ListItem>
                                            <asp:ListItem Text="นายหน้า" Value="02"></asp:ListItem>
                                            <asp:ListItem Text="ตัวแทนและนายหน้า" Value="03" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                            <fieldset>
                                <legend>สิทธิ์ในการจัดสอบประเภทใบอนุญาต</legend>
                                <asp:CheckBoxList ID="chkLicense" runat="server"></asp:CheckBoxList>
                            </fieldset><br />
                            <center>
                                <asp:Button ID="btnSave" runat="server" Text="บันทึก" onclick="btnSave_Click" ValidationGroup="1" CssClass="btn" />
                                <asp:Button ID="btnUpdate" runat="server" Text="แก้ไข" ValidationGroup="1" onclick="btnUpdate_Click" CssClass="btn" />
                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" />
                            </center>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <uc:UCModalError runat="server" ID="UCError" />
            <uc:UCModalSuccess runat="server" ID="UCSuccess" />
        </ContentTemplate>
    </asp:UpdatePanel>

   </div>
   <div class="clear"></div>
</asp:Content>

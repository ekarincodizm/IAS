<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="AccountDetail.aspx.cs" Inherits="IAS.Account.AccountDetail" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagPrefix="uc" TagName="UCModalSuccess" %>
<%@ Register Src="~/UserControl/UCModalError.ascx" TagPrefix="uc" TagName="UCModalError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
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
    function OnAssocSelected(source, eventArgs) {
        var c = GetIDByString(eventArgs.get_value());
        if (c) {
            document.getElementById("<%=txtAssocId.ClientID %>").value = c;
        } else {
            document.getElementById("<%=txtAssocId.ClientID %>").value = '';
            document.getElementById("<%=txtAssoc.ClientID %>").value = ''
        }
    }
    function OnCompanySelected(source, eventArgs) {
        var c = GetIDByString(eventArgs.get_value());
        if (c) {
            document.getElementById("<%=txtCompanyId.ClientID %>").value = c;
        } else {
            document.getElementById("<%=txtCompanyId.ClientID %>").value = '';
            document.getElementById("<%=txtCompany.ClientID %>").value = ''
        }
        if (/Chrome/.test(navigator.userAgent)) {
            document.documentElement.style.overflow = 'auto';
        }
    }
    function GetIDByString(name) {
        return (name.split('[')[1]).split(']')[0];
    }

    function OnAutoCompleteShown(source, eventArgs) {
        if (/Chrome/.test(navigator.userAgent)) {
            document.documentElement.style.overflow = 'hidden';
        }
    }
    function OnAutoCompleteHidden(source, eventArgs) {
        if (/Chrome/.test(navigator.userAgent)) {
            document.documentElement.style.overflow = 'auto';
        }
    }
    </script>
    <style type="text/css">
        .modalpopupMessageSE {z-index:99994 !important;}
        .modalBackgroundSE {z-index:99995 !important;}
        table.icon-manage{margin:0 auto;}
        table.icon-manage td{border:none !important; width:23px; padding:0 2px !important;}
        .txtReadonly{ background-color:#f0f0f0 !important;}
        .autocomplete_completionListElement{z-index: 10000 !important;}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

<div class="box_content">
<div class="box_content_header">
<div class="box_content_header_text">
     <asp:Label Text="ข้อมูลผู้ใช้งาน" BorderStyle="None" ID="lblAccountPage" runat="server" /> 
</div>
</div>

    <div>
        <asp:Panel ID="PnlMain" runat="server" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelGv" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="box_body_content">
                    <div class="box_t_h">::ค้นหา</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table>
                                <tr>
                                    <td>ประเภทผู้ใช้งาน</td>
                                    <td><asp:DropDownList ID="ddlUserType" runat="server" CssClass="ddl" Width="170px"></asp:DropDownList></td>
                                    <td>ชื่อผู้ใช้งานระบบ</td>
                                    <td><asp:TextBox ID="txtUserName" runat="server" CssClass="txt"></asp:TextBox></td>
                                    <td>รหัสบัตรประชาชน</td>
                                    <td><asp:TextBox ID="txtIdCard" runat="server" CssClass="txt" Width="130px" onkeypress="return runScript(event)" MaxLength="13"></asp:TextBox></td>
                                    <td>อีเมล์</td>
                                    <td><asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox></td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" 
                                            onclick="btnSearch_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                            onclick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
   
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
                <div style="margin-left:10px">
                <asp:GridView ID="gvAccountDetail" runat="server" AutoGenerateColumns="false" 
                        CssClass="datatable" Width="99%" onrowdatabound="gvAccountDetail_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="ลำดับ" ItemStyle-CssClass="td-center">
                           <ItemTemplate>
                                <asp:Label ID="lblNum" runat="server" Text='<%# Bind("NUM") %>'></asp:Label>
                           </ItemTemplate>
                            <ItemStyle CssClass="td-center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสบัตรประชาชน" ItemStyle-CssClass="td-center">
                            <ItemTemplate>
                                <asp:Label ID="lblIdCardNo" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="td-center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="รหัสผู้ใช้" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblUserID" runat="server" Text='<%# Bind("USER_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อผู้ใช้งานระบบ">
                            <ItemTemplate>
                                <asp:Label ID="lblNames" runat="server" Text='<%# Bind("NAMES") %>'></asp:Label>
                                <asp:Label ID="lblLastname" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="อีเมล์">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("EMAIL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ประเภทผู้ใช้งาน" ItemStyle-CssClass="td-center">
                            <ItemTemplate>
                                <asp:Label ID="lblUserTypeCode" runat="server" Text='<%# Bind("MEMBER_TYPE") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblUserType" runat="server" Text='<%# Bind("MEMBER_NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="td-center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานะ" ItemStyle-CssClass="td-center">
                            <ItemTemplate>
                                <asp:Label ID="lblIsActive" runat="server" Text='<%# Bind("IS_ACTIVE") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblShowActive" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="td-center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="จัดการบัญชีผู้ใช้งาน" ItemStyle-CssClass="td-center">
                            <ItemTemplate>
                                <table class="icon-manage">
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="lbtnEditMemberType" runat="server" onclick="lbtnEditMemberType_Click"> 
                                            <img src="../Images/edit-account.png" alt="" title="การจัดการบัญชีผู้ใช้งาน" /></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtnEditPassword" runat="server" onclick="lbtnEditPassword_Click" >
                                            <img src="../Images/re-password.png" alt="" title="เปลี่ยนรหัสผ่าน" /></asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtnEditUserActive" runat="server"
                                            onclick="lbtnEditUserActive_Click"><img src="../Images/cancel-user.png" alt="" title="การยกเลิกบัญชีผู้ใช้งาน" /></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <ItemStyle CssClass="td-center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        ไม่พบข้อมูล
                    </EmptyDataTemplate>
                </asp:GridView>
                <br/>
                    <div id="ctrPage" runat="server">
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
                </div>
                <br/><br/>
         <%--       <uc:UCModalError runat="server" ID="UCModalError" />
                <uc:UCModalSuccess runat="server" ID="UCModalSuccess" />--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    
    <ajaxToolkit:ModalPopupExtender ID="PopUpEditTypeAccount" runat="server" CancelControlID="btnClosePopup" 
            TargetControlID="btnModal" PopupControlID="pnlModal" BackgroundCssClass="modalbackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Button ID="btnModal" runat="server" Text="Open" Style="display: none" />
        <asp:Panel ID="pnlModal" Style="display: none; width:650px;" runat="server" CssClass="modalpopup">
            <div class="box_body_content">
                <div class="box_t_h">::ข้อมูลผู้ใช้งาน</div>
                <div class="box_textbox_bgadd" style="padding-right:3px">
                    <div class="box_texbox_add_box">
                        <asp:UpdatePanel ID="UplPopUp" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblUserId" runat="server" Visible="false"></asp:Label>
                                <table>
                                    <tr>
                                        <td width="150">รหัสบัตรประชาชน</td>
                                        <td><asp:Label ID="lblIdCard" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>ชื่อผู้ใช้งาน</td>
                                        <td><asp:Label ID="lblNames" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>E-mail</td>
                                        <td><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>ประเภทผู้ใช้งาน</td>
                                        <td><asp:DropDownList ID="ddlMemberTypeEdit" runat="server" CssClass="ddl" 
                                                Enabled="false" AutoPostBack="true" 
                                                onselectedindexchanged="ddlMemberTypeEdit_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:CheckBox ID="chkEnableMemberType" runat="server" 
                                                Text="เปลี่ยนประเภทผู้ใช้งาน" AutoPostBack="true" 
                                                oncheckedchanged="chkEnableMemberType_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr id="trCompany" runat="server" visible="false">
                                        <td>บริษัท</td>
                                        <td><asp:TextBox ID="txtCompanyId" runat="server" CssClass="txt txtReadonly" Enabled="false" Width="50px">
                                            </asp:TextBox><asp:TextBox ID="txtCompany" runat="server" CssClass="txt" Width="380px"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtCompany"
                                                ServiceMethod="GetAutomCompleteCompany" ServicePath="~/Service/AutoComplete.asmx"
                                                MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
                                                ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="OnCompanySelected"
                                                OnClientShown="OnAutoCompleteShown" OnClientHidden="OnAutoCompleteHidden">
                                            </ajaxToolkit:AutoCompleteExtender>
                                            <asp:Image ID="imgS" runat="server" ImageUrl="~/Images/find_search.png"  ToolTip="พิมพ์ชื่อเพื่อทำการค้นหา"/>
                                            <asp:RequiredFieldValidator ID="ReqddlCompany" runat="server" ErrorMessage="*" 
                                                ControlToValidate="txtCompanyId" ValidationGroup="changeMT" CssClass="failureNotification"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trAssoc" runat="server"  visible="false">
                                        <td>สมาคม</td>
                                        <td><asp:TextBox ID="txtAssocId" runat="server" CssClass="txt txtReadonly" Enabled="false" Width="50px">
                                            </asp:TextBox><asp:TextBox ID="txtAssoc" runat="server" CssClass="txt" Width="250px"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txtAssoc"
                                                ServiceMethod="GetAutomCompleteAssociate" ServicePath="~/Service/AssociateAutoComplete.asmx"
                                                MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                                CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
                                                ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="OnAssocSelected"
                                                OnClientShown="OnAutoCompleteShown" OnClientHidden="OnAutoCompleteHidden">
                                            </ajaxToolkit:AutoCompleteExtender>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/find_search.png"  ToolTip="พิมพ์ชื่อเพื่อทำการค้นหา"/>
                                            <asp:RequiredFieldValidator ID="ReqddlAssoc" runat="server" ErrorMessage="*" 
                                                ControlToValidate="txtAssocId" ValidationGroup="changeMT" CssClass="failureNotification"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trExamPlaceGroup" runat="server" visible="false">
                                        <td>หน่วยงานจัดสอบ</td>
                                        <td><asp:DropDownList ID="ddlExamPlaceGroup" runat="server" CssClass="ddl"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ReqDdlExamPlaceGroup" runat="server" ErrorMessage="*" 
                                                ControlToValidate="ddlExamPlaceGroup" ValidationGroup="changeMT" CssClass="failureNotification"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>หมายเหตุ</td>
                                        <td><asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="txt" Height="50" Width="300" MaxLength="50" ></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnUpdate" runat="server" Text="บันทึก" CssClass="btn" OnClientClick="return confirmSave()"
                                                onclick="btnUpdate_Click" ValidationGroup="changeMT" />
                                            <asp:Button ID="btnClosePopup" runat="server" Text="ยกเลิก" CssClass="btn" 
                                                onclick="btnClosePopup_Click"/>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                                <script type="text/javascript">
                                    function confirmSave() {
                                        var changeType = document.getElementById('<%=chkEnableMemberType.ClientID %>');
                                        if (Page_ClientValidate('changeMT') && changeType.checked) {
                                            Page_BlockSubmit = false;
                                            return confirm("คุณแน่ใจที่จะเปลี่ยนประเภทผู้ใช้งาน ใช่ หรือ ไม่");
                                        } else {
                                            Page_BlockSubmit = false;
                                            return false;
                                        }
                                    }
                                </script>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="PopUpDisableUser" runat="server" CancelControlID="btnDisCancel"
            TargetControlID="Button1" PopupControlID="pnlModalDisableUser" BackgroundCssClass="modalbackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Button ID="Button1" runat="server" Text="Open" Style="display: none;" />
        <asp:Panel ID="pnlModalDisableUser" Style="display:none; z-index:99990 !important; width:550px;" runat="server" CssClass="modalpopup">
            <div class="box_body_content">
                <div class="box_t_h">::รายละเอียดข้อมูล</div>
                <div class="box_textbox_bgadd" style="padding-right:3px">
                    <div class="box_texbox_add_box">
                        <asp:UpdatePanel ID="UpdatePaneleDisableUser" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                            <table>
                                <tr>
                                    <td width="150">รหัสประชาชน</td>
                                    <td><asp:Label ID="lblDisIdCard" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>ชื่อผู้ใช้</td>
                                    <td><asp:Label ID="lblDisNames" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>E-mail</td>
                                    <td><asp:Label ID="lblDisEmail" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>ประเภทผู้ใช้งาน</td>
                                    <td><asp:Label ID="lblDisMemberType" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>สถานะ</td>
                                    <td>
                                        <asp:RadioButtonList ID="radIsActive" runat="server" 
                                            RepeatDirection="Horizontal" AutoPostBack="true" 
                                            onselectedindexchanged="radIsActive_SelectedIndexChanged">
                                            <asp:ListItem Value="A">ใช้งาน</asp:ListItem>
                                            <asp:ListItem Value="I">ไม่ใช้งาน</asp:ListItem>
                                            <asp:ListItem Value="D">ยกเลิกบัญชีผู้ใช้งาน</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr id="trCancelComment" runat="server">
                                    <td>เหตุผลการยกเลิก</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDisRemark" runat="server" 
                                            ValidationGroup="disableUser" CssClass="ddl" AutoPostBack="true" 
                                            onselectedindexchanged="ddlDisRemark_SelectedIndexChanged">
                                            <asp:ListItem Value="">---- เลือก ----</asp:ListItem>  
                                            <asp:ListItem>พฤติกรรมการแจ้งใช้งานไม่เหมาะสม</asp:ListItem>
                                            <asp:ListItem>ขอเปลี่ยนแปลงเจ้าหน้าที่</asp:ListItem>
                                            <asp:ListItem>ระบบอนุมัติผิดพลาด</asp:ListItem>
                                            <asp:ListItem>อื่น ๆ</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqddlDisRemark" runat="server" ErrorMessage="*" ControlToValidate="ddlDisRemark" ValidationGroup="DisableUser" CssClass="failureNotification"></asp:RequiredFieldValidator>
                                        &nbsp;&nbsp;
                                        <asp:TextBox ID="txtDisRemarkText" runat="server" CssClass="txt" Visible="false"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="ReqDisRemarkText" runat="server" ErrorMessage="*" ControlToValidate="txtDisRemarkText" ValidationGroup="DisableUser" CssClass="failureNotification"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><asp:Label ID="lblDisUserID" runat="server" Visible="false"></asp:Label><br />
                                        <asp:Button ID="btnDisSave" runat="server" Text="บันทึก" CssClass="btn"  ValidationGroup="DisableUser" OnClientClick="return ConfirmDisableUser()"
                                            onclick="btnDisSave_Click"/>
                                        <asp:Button ID="btnDisCancel" runat="server" Text="ยกเลิก"  CssClass="btn"
                                            onclick="btnDisCancel_Click" />
                                       <script type="text/javascript">
                                                function ConfirmDisableUser() {
                                                    if (Page_ClientValidate('DisableUser')) {
                                                        Page_BlockSubmit = false;
                                                        var sel = $('input[id^="<%=radIsActive.ClientID %>"]:checked'); 
                                                        if(sel.val() == 'D')
                                                            return confirm('คุณแน่ใจที่จะยกเลิกบัญชีผู้ใช้งานนี้ ใช่ หรือ ไม่?');
                                                        return true;
                                                    }
                                                    Page_BlockSubmit = false;
                                                    return false;
                                                }
                                        </script>
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="PopupEditPassword" runat="server" CancelControlID="btnPwdCancel"
            TargetControlID="Button2" PopupControlID="pnlModalPassword" BackgroundCssClass="modalbackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Button ID="Button2" runat="server" Text="Open" Style="display: none" />
        <asp:Panel ID="pnlModalPassword" Style="display: none;width:550px;" runat="server" CssClass="modalpopup">
            <div class="box_body_content">
                <div class="box_t_h">::ข้อมูลรหัสผ่าน</div>
                <div class="box_textbox_bgadd" style="padding-right:3px">
                    <div class="box_texbox_add_box">
                        <asp:UpdatePanel ID="UpdatePaneleEditPassword" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                            <table>
                                <tr>
                                    <td width="150">รหัสประชาชน</td>
                                    <td width="300">
                                        <asp:Label ID="lblPwdUserID" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblPwdIdCard" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ชื่อผู้ใช้</td>
                                    <td><asp:Label ID="lblPwdNames" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>E-mail</td>
                                    <td><asp:Label ID="lblPwdEmail" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>ประเภทผู้ใช้งาน</td>
                                    <td><asp:Label ID="lblPwdMemberType" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>รหัสผ่านใหม่</td>
                                    <td>
                                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txt" TextMode="Password" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqNewPW" runat="server" ControlToValidate="txtNewPassword" 
                                                ValidationGroup="ChangePW" ErrorMessage="กรุณาระบุรหัสผ่านใหม่" CssClass="failureNotification">*
                                        </asp:RequiredFieldValidator>
                                        <!--Password ValidationExpression 1=LowCase 1=UpperCase 1=Digi 1=Symbol[!@#$%^&*_+~] Length7-14 Nattapong@01/04/2557-->
                                        <asp:RegularExpressionValidator ID="reqRegNewPW" Display="Dynamic"
                                            runat="server" ControlToValidate="txtNewPassword" ErrorMessage="รหัสผ่านต้องมีความยาวอย่างน้อย 8 ตัวอักษร แต่ไม่เกิน 14 ตัวอักษร<br/>รหัสผ่านต้องผสมระหว่างตัวเลข ตัวอักษรพิมพ์ใหญ่ ตัวอักษรพิมพ์เล็ก และอักขระพิเศษ อย่างละ 1 ตัวขึ้นไป<br/>อักขระที่อนุญาตให้กำหนดรหัสผ่านได้คือ 0-9 A-Z a-z @ # $ % ^ & _ ( ) { } + ? และ , เท่านั้น" CssClass="failureNotification"
                                            ValidationGroup="ChangePW" ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$" ><asp:Label ID="Label3" runat="server" visible="false">*</asp:Label>
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ยืนยันรหัสผ่านใหม่</td>
                                    <td>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txt" TextMode="Password" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqConfirmPW" runat="server" ControlToValidate="txtConfirmPassword"
                                            ValidationGroup="ChangePW" ErrorMessage="กรุณายืนยันรหัสผ่าน" CssClass="failureNotification">*
                                        </asp:RequiredFieldValidator>
                                        <!--Password ValidationExpression 1=LowCase 1=UpperCase 1=Digi 1=Symbol[!@#$%^&*_+~] Length7-14 Nattapong@01/04/2557-->
                                        <asp:RegularExpressionValidator ID="reqRegConfirmPW" Display="Dynamic"
                                            runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="รหัสผ่านต้องมีความยาวอย่างน้อย 8 ตัวอักษร แต่ไม่เกิน 14 ตัวอักษร<br/>รหัสผ่านต้องผสมระหว่างตัวเลข ตัวอักษรพิมพ์ใหญ่ ตัวอักษรพิมพ์เล็ก และอักขระพิเศษ อย่างละ 1 ตัวขึ้นไป<br/>อักขระที่อนุญาตให้กำหนดรหัสผ่านได้คือ 0-9 A-Z a-z @ # $ % ^ & _ ( ) { } + ? และ , เท่านั้น" CssClass="failureNotification"
                                            ValidationGroup="ChangePW" ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$"><asp:Label ID="Label1" runat="server" visible="false">*</asp:Label>
                                        </asp:RegularExpressionValidator>
                                        <!--Password Compare-->
                                        <asp:CompareValidator ID="reqComparePW" runat="server" Display="Dynamic" ControlToValidate="txtNewPassword" 
                                            ControlToCompare="txtConfirmPassword" ValidationGroup="ChangePW"
                                            ErrorMessage="รหัสผ่านไม่ตรงกัน" CssClass="failureNotification"><asp:Label ID="Label2" runat="server" visible="false">*</asp:Label>
                                        </asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnPwdSave" runat="server" Text="บันทึก"
                                            onclick="btnPwdSave_Click" CssClass="btn" />
                                        <asp:Button ID="btnPwdCancel" runat="server" Text="ยกเลิก" 
                                            onclick="btnPwdCancel_Click" CssClass="btn"/>
                                    </td>
                                </tr>
                            </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
    </asp:Panel>

</div>
<div style="clear:left;"></div>
</asp:Content>

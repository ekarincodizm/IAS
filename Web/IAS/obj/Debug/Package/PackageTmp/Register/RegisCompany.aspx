<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterRegister.master" AutoEventWireup="true" CodeBehind="RegisCompany.aspx.cs" Inherits="IAS.Register.RegisCompany" %>
<%@ Register Src="~/UserControl/ucAddress.ascx" TagName="ucAddress" TagPrefix="ucAdd"%>
<%--<%@ Register Src="~/UserControl/ucCurrentLicense.ascx" TagName="UCLicense" TagPrefix="uclic" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Detail" runat="server">
    <script type="text/javascript">

        function CancelConfirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("คุณต้องการนำเข้าข้อมูลหรือไม่?")) {
                confirm_value.value = "Y";
            } else {
                confirm_value.value = "N";
            }
            document.forms[0].appendChild(confirm_value);
        }

    function confirmDelete() {
        return confirm('คุณต้องการลบข้อมูลหรือไม่?');
    }


    function confirmSave() {
        return confirm('คุณต้องการบันทึกข้อมูลหรือไม่');
    }

    function OnContactSelected(source, eventArgs) {

        var hdfValueID = "<%= hdf.ClientID %>";

        document.getElementById(hdfValueID).value = eventArgs.get_value();
        __doPostBack(hdfValueID, "");
    }

    function changeUrl() {
        var url = window.location.toString();
        if (url.indexOf('?') == -1) {
            window.location = url + "?Type=3";
        }
    }

    function Input_Eng(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode >= 3585) {
            return false;
        } else {
            var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?^()";
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
    }


    function spacialCaractor(evt) {
        var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()1234567890๑๒๓๔฿๕๖๗๘๙๐";
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
    </script>


    <% if(Request.QueryString["Mode"] == null)
       { %>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#Panel1").hide();
        });
    </script>

    <% } %>


<div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
           <asp:Label Text="ลงทะเบียน (บริษัท)" BorderStyle="None" ID="lblRegister" runat="server" />
                </div>
            </div>

         
    <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
        <asp:UpdatePanel ID="udpDetailCompany" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                        <div class="box_body_content">
            	        <div class="box_t_h">::ข้อมูลบริษัท</div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4" align="center">
                                <asp:Label ID="lblMessage" runat="server" CssClass="lbl" Font-Bold="true" ForeColor="Red" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblCompany" runat="server" Text="บริษัท" CssClass="lbl"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left;" colspan="3">
                                <asp:TextBox runat="server" ID="txtCompany" CssClass="txt" Width="83%"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txtCompany"
                                    ServiceMethod="GetAutomCompleteCompany" ServicePath="~/Service/AutoComplete.asmx"
                                    MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
                                    ShowOnlyCurrentWordInCompletionListItem="true" FirstRowSelected="true" onclientitemselected="OnContactSelected">
                                </ajaxToolkit:AutoCompleteExtender>
                                <asp:Image ID="imgS" runat="server" ImageUrl="~/Images/find_search.png"  ToolTip="พิมพ์ชื่อเพื่อทำการค้นหา"/>
                                <asp:RequiredFieldValidator ID="CompanyRequired" runat="server" ControlToValidate="txtCompany" ErrorMessage="พิพม์ชื่อบริษัทเพื่อทำการค้นหา"
                                    CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdf" runat="server" onvaluechanged="hdf_ValueChanged"  />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;<span><asp:Label ID="lblCompanyRegister" runat="server" CssClass="lbl" Text="ทะเบียนบริษัท"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtCompanyRegister" runat="server" Width="230px" CssClass="txt" Enabled="false"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="g" runat="server" ControlToValidate="txtCompanyRegister" ErrorMessage="ต้องเลือกบริษัทจึงจะมีข้อมูลทะเบียนบริษัท"
                                    CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblCompanyTel" runat="server" Text="เบอร์โทรศัพท์หน่วยงาน" CssClass="lbl"></asp:Label>
                                </span>:
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtCompanyTel" runat="server" Width="230px" CssClass="txt" MaxLength="9" ></asp:TextBox>
                                  <asp:Label ID="lblTelExt" runat="server" CssClass="lbl" Text="ต่อ"></asp:Label>
                                 <asp:TextBox ID="txtCompanyTelExt" runat="server" CssClass="txt-tel-ext" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CompanyTelRequired" runat="server" ControlToValidate="txtCompanyTel" ErrorMessage="กรุณากรอกเบอร์โทรศัพท์บริษัท"
                                    CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="failureNotification" ValidationGroup="CompanyValidationGroup" 
                                    ErrorMessage="รูปแบบเบอร์โทรศัพท์บริษัทไม่ถูกต้อง" ControlToValidate="txtCompanyTel" ValidationExpression="^0[0-9]{8}" ><asp:Label ID="Label4" runat="server" visible="false">*</asp:Label>
                                </asp:RegularExpressionValidator>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 30%;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <ucAdd:ucAddress ID="UcAddress" runat="server"  />
                            </td>
                        </tr>
                    </table>
            </div>
            </div>
            </div>
            
            <div class="box_body_content">
            <div class="box_t_h">::ข้อมูลพนักงานบริษัท</div>
            <div class="box_textbox_bgadd">
            <div class="box_texbox_add_box">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4">
                            <table style="width:100%;border-style:none;">
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <span>
                                                            <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                                            :</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtMemberType" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <span>
                                                            <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                            :</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ddl" Width="150px" AutoPostBack="true"
                                                            onselectedindexchanged="ddlTitle_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlTitle" ErrorMessage="กรุณาเลือกคำนำหน้าชื่อ"
                                                            CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" MaxLength="50" onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName" ErrorMessage="กรุณากรอกชื่อ"
                                                            CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REV1" Display="Dynamic" runat="server" 
                                                        ControlToValidate="txtFirstName" ErrorMessage="รูปแบบชื่อไม่ถูกต้อง" CssClass="failureNotification"
                                                        ValidationGroup="CompanyValidationGroup" ValidationExpression="^[ก-ฮ|a-zA-Z|\p{L}|\p{M}]{1,50}?$"><asp:Label ID="Label6" runat="server" visible="false">*</asp:Label>
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <span>
                                                            <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                                            :</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" MaxLength="70" onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName" ErrorMessage="กรุณากรอกนามสกุล"
                                                            CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" 
                                                        ControlToValidate="txtLastName" ErrorMessage="รูปแบบนามสกุลไม่ถูกต้อง" CssClass="failureNotification"
                                                        ValidationGroup="CompanyValidationGroup" ValidationExpression="^[ก-ฮ|a-zA-Z|\s|\p{L}|\p{M}]{1,70}?$"><asp:Label ID="Label7" runat="server" visible="false">*</asp:Label>
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%; height: 17px;">
                                                        <asp:Label ID="lblTel" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left; width: 30%; height: 17px;">
                                                          <asp:TextBox ID="txtTel" runat="server" CssClass="txt-tel" MaxLength="9" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                                                        <asp:Label ID="lblTelExt2" runat="server" CssClass="lbl" Text="ต่อ"></asp:Label>
                                                        <asp:TextBox ID="txtTelExt" runat="server" CssClass="txt-tel-ext" MaxLength="4" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');" onkeypress="fncInputNumericValuesOnly()"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="TelRequired" runat="server" ControlToValidate="txtTel" ErrorMessage="กรุณากรอกเบอร์โทรศัพท์"
                                                            CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="รูปแบบเบอร์โทรศัพท์ไม่ถูกต้อง" CssClass="failureNotification" ValidationGroup="CompanyValidationGroup" 
                                                            ControlToValidate="txtTel" ValidationExpression="^0[0-9]{8}" ><asp:Label ID="Label3" runat="server" visible="false">*</asp:Label>
                                                        </asp:RegularExpressionValidator>
                   
                                                    </td>
                                                    <td style="text-align: right; width: 20%; height: 17px;">
                                                        <asp:Label ID="lblStartDate0" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left; width: 30%; height: 17px;">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr style="text-align: left; width: 30%; height: 17px;">
                                                                <td style="text-align: left; width: 35%; height: 17px;">
                                                                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" Enabled="false" CssClass="rb">
                                                                        <asp:ListItem Text="ชาย" Value="M" ></asp:ListItem>
                                                                        <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                                <td style="text-align: left; width: 65%; height: 17px;">
                                                                    <asp:RequiredFieldValidator ID="SexRequired" runat="server" ControlToValidate="rblSex"  ErrorMessage="กรุณาเลือกเพศ"
                                                                    CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtEmail" runat="server" onkeypress="return Input_Eng(event);" CssClass="txt" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail" ErrorMessage="กรุณากรอกอีเมล"
                                                            CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                                        <!--Email ValidationExpression Nattapong@22/8/2556-->
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server"
                                                                ControlToValidate="txtEmail" ErrorMessage="รูปแบบอีเมลไม่ถูกต้อง" CssClass="failureNotification"
                                                                ValidationGroup="CompanyValidationGroup" ValidationExpression="^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"><asp:Label ID="Label2" runat="server" visible="false">*</asp:Label>
                                                         </asp:RegularExpressionValidator>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblIDCard" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                        :
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:TextBox ID="txtIDNumber" runat="server" CssClass="txt txtidcard" MaxLength="13" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="IDCardRequired" runat="server" ControlToValidate="txtIDNumber" ErrorMessage="กรุณากรอกเลขบัตรประชาชน"
                                                            CssClass="failureNotification" ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="reqRegIDNumber" runat="server" ControlToValidate="txtIDNumber" ValidationExpression="^[0-9]{13}?$"
                                                                ValidationGroup="CompanyValidationGroup" ErrorMessage="รูปแบบเลขบัตรประชาชนไม่ถูกต้อง" CssClass="failureNotification">
                                                                <asp:Label id ="a" runat="server" visible="false">*</asp:Label>
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>       
                                     </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left;" colspan="4">
                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="หมายเหตุ : Email ที่ใช้จะต้องเป็น Email ที่ใช้งานได้จริงเท่านั้น เพราะจะถูกใช้ในการติดต่อ"></asp:Label>
                            </td>
                        </tr>
                    </table>&nbsp;
                <%--<table style="width: 100%;" align="center">
                    <tr>
                        <td>
                            <ajaxToolkit:TabContainer ID="TabLicnese" runat="server" Width="100%" ActiveTabIndex="0" AutoPostBack="false" CssClass="ajax_tabs">
                                <ajaxToolkit:TabPanel ID="TabCurrentLicnese" runat="server">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblCurrentLicneseHeader" runat="server" ForeColor="Black" Text="ใบอนุญาตที่ยังมีผลอยู่"></asp:Label>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <uclic:UCLicense ID="ucCurrentLicense" runat="server"></uclic:UCLicense>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                </table>--%>
             </div>
             </div>
             </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>


    <% if (false)
       { %>
    </div>
    <%} %>
</asp:Content>

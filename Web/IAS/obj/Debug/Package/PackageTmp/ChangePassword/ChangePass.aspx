<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ChangePass.aspx.cs" Inherits="IAS.ChangePassword.ChangePass" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

    <div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
             <asp:Label Text="เปลี่ยนรหัสผ่าน" BorderStyle="None" ID="lblChanagePassword" runat="server" />
                </div>
            </div>

    <br />
    <br />

    <div>
      <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <asp:Panel ID="PnlMain" runat="server" Width="100%">
          
                    <asp:Panel ID="PnlSearch" runat="server">

                    <div class="box_body_content">
            	<div class="box_t_h"></div>
            	<div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">

                        <table style="width: 65%;" align="center">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20;">
                                            </td>
                                            <td style="width: 30;">
                                            </td>
                                            <td style="width: 20;">
                                            </td>
                                            <td style="width: 25;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20;">
                                            </td>
                                            <td style="width: 30; text-align: right;">
                                                <asp:Label ID="lblUserName" runat="server" Text="ชื่อผู้ใช้" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="width: 20; text-align: left;">
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                            <td style="width: 25;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20;">
                                            </td>
                                            <td style="width: 30; text-align: right;">
                                                <asp:Label ID="lblOldPassword" runat="server" Text="รหัสผ่านเดิม" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="width: 20; text-align: left;">
                                                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="txt" TextMode="Password" ValidationGroup="reqValidation"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqOldPW1" runat="server" ControlToValidate="txtOldPassword" Display="Dynamic" 
                                                                ValidationGroup="reqValidation" ErrorMessage="" CssClass="failureNotification"> </asp:RequiredFieldValidator>
                                                <!--Password ValidationExpression allow 0-9,a-z,A-Z Nattapong@1/11/2556-->
                                                <%--<asp:RegularExpressionValidator ID="regOldPW1" Display="Dynamic" 
                                                                runat="server" ControlToValidate="txtOldPassword" ErrorMessage="" CssClass="failureNotification" 
                                                                ValidationGroup="reqValidation" ValidationExpression="^([a-z]|[A-Z]|[!@&#]|[0-9]){6,14}$" ></asp:RegularExpressionValidator>--%>
                                            </td>
                                            <td style="width: 25;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20;">
                                            </td>
                                            <td style="width: 30; text-align: right;">
                                                <asp:Label ID="lblNewPassword" runat="server" Text="รหัสผ่านใหม่" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="width: 20; text-align: left;">
                                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txt" TextMode="Password" ValidationGroup="reqValidation"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqPW1" runat="server" ControlToValidate="txtNewPassword" Display="Dynamic"
                                                                ValidationGroup="reqValidation" ErrorMessage="" CssClass="failureNotification"></asp:RequiredFieldValidator>
                                                <!--Password ValidationExpression 1=LowCase 1=UpperCase 1=Digi 1=Symbol[!@#$%^&*_+~] Length7-14 Nattapong@01/04/2557-->
                                                <asp:RegularExpressionValidator ID="regPW1" Display="Dynamic" 
                                                                runat="server" ControlToValidate="txtNewPassword" ErrorMessage="" CssClass="failureNotification" 
                                                                ValidationGroup="reqValidation" ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$" ></asp:RegularExpressionValidator>   
                                            </td>
                                            <td style="width: 25;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20;">
                                            </td>
                                            <td style="width: 30; text-align: right;">
                                                <asp:Label ID="lblConfirmPassword" runat="server" Text="ยืนยันรหัสผ่าน" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="width: 20; text-align: left;">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txt" TextMode="Password" ValidationGroup="reqValidation"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqPW2" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" 
                                                                 ValidationGroup="reqValidation" ErrorMessage="" CssClass="failureNotification"> </asp:RequiredFieldValidator>
                                                <!--Password ValidationExpression 1=LowCase 1=UpperCase 1=Digi 1=Symbol[!@#$%^&*_+~] Length7-14 Nattapong@01/04/2557-->
                                                <asp:RegularExpressionValidator ID="regPW2" Display="Dynamic"
                                                                runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="" CssClass="failureNotification"
                                                                ValidationGroup="reqValidation" ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$"> </asp:RegularExpressionValidator>
                                                <!--Password Compare-->
                                                <asp:CompareValidator ID="reqComparePW" runat="server" Display="Dynamic" ControlToValidate="txtNewPassword"
                                                                ControlToCompare="txtConfirmPassword" 
                                                                ValidationGroup="reqValidation" ErrorMessage="" CssClass="failureNotification"> </asp:CompareValidator>
                                            </td>
                                            <td style="width: 25;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOk_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                </div>
                </div>
                </div>

                    </asp:Panel>
                    <uc2:UCModalError ID="UCModalError" runat="server" style="display:none" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" style="display:none"  />
         
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnOk" />
                </Triggers>
         
        </asp:Panel>
         
     </ContentTemplate>
   </asp:UpdatePanel>
    </div>

    </div>
    <div style="clear:left;"></div>

</asp:Content>

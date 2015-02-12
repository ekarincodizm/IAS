<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ForgetPass.aspx.cs" Inherits="IAS.ForgetPassword.ForgetPass" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
 <script type="text/javascript">
         $("#Panel1").hide();
</script>


<div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
         <asp:Label Text="ลืมรหัสผ่าน" BorderStyle="None" ID="lblChanagePassword" runat="server" />
                </div>
            </div>


    <br />
    <br />
    <div>
        <asp:Panel ID="PnlMain" runat="server" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelForgetPassword" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                                            <td style="width: 20%;">
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td style="width: 25%;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="">
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblUserName" runat="server" Text="ชื่อผู้ใช้" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                            <td style="">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="">
                                            </td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblConfirmEmail" runat="server" Text="ยืนยันอีเมล" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtConfirmEmail" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>&nbsp;&nbsp;
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

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnOk" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>

    </div>
    <div style="clear:left"></div>
                        <uc2:UCModalError ID="UCModalPopupError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalPopupSuccess" runat="server"  />

</asp:Content>

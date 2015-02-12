<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="IAS.home" %>
<%@ Register src="UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc1" %>
<%--<%@ Register Src="UserControl/UCExamSchedule.ascx" TagName="UCExamSchedule" TagPrefix="uc1" %>--%>
<%@ Register Src="~/UserControl/UCLoadingProgress.ascx" TagName="UCLoading" TagPrefix="ucload" %>
<%@ Import Namespace="System.Reflection" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= GetApplicationVersion() %>สำนักงานคณะกรรมการกำกับและส่งเสริมการประกอบธุรกิจประกันภัย</title>
    <link href="style/Home.css" rel="stylesheet" type="text/css" />
    <link href="style/css_login.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <!--[if IE 7]>
    <link href="style/ie7.css" rel="stylesheet" type="text/css" />
    <![endif]-->
    <script type="text/javascript">
        $(document).ready(function () {

            $("#<%=txtUserName.ClientID %>").attr('placeholder', 'Username');
            $("#<%=txtPassword.ClientID %>").attr('placeholder', 'Password');

            $(".modalpopupMessage").css("width", screen.width).css("height", screen.height);

        });
        
    </script>
    
    <style type="text/css">
        input[placeholder="Username"]{color:#000;}
        .modalpopupMessage
        {
            position:fixed;
            top:0px;
            left:0px;
            background-image:url('image/bgmessage.png');
        }
        
    </style>
</head>
<body>


    <form id="form2" runat="server">
   
    <div class="wrapper">
    <div class="logo_pagelogin"></div>
    <div class="bg_text">
    	<div class="text_head">ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ</div>
    </div>
    <div class="box_login">
    	<div class="h1_login">Login</div>
        <div class="boxusername">
        
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>

        </div>
        <div class="boxpassword">
        
         <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        
        </div>
        <div class="box_buttonlogin">        
        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="myButtonlogin" OnClick="btnLogin_Click" />     
        </div>
          
    	<div class="box_textlogin" runat = "server"  > 
        

        <center>
        	<div  id = "regisId" runat = "server" style="float:left;">
                <asp:LinkButton ID="lbtRegister" runat="server" Text="สมัครสมาชิก" onclick="lbtRegister_Click"></asp:LinkButton>
           </div>


            <div  id = "middle" runat = "server" style="float:left;padding:left;">&nbsp;|</div>
         
            </center>

           <center>
            <div  id = "forgetpass" runat = "server" style="text-align:center;">

            <asp:LinkButton ID="lbtForgetPassword" runat="server" Text="ลืมรหัสผ่าน" onclick="lbtForgetPassword_Click"></asp:LinkButton>        

            </div>
            </center>
        
        </div>
       
        
       
    </div>
    <div class="bg">
       
        </div>
    <div class="bg_header_login"></div>
    <div class="bg_center_login"></div>
    <div class="bg_footter_login">
    	<div class="box_footter">
    	<div class="box_footter_f1">สำนักงานคณะกรรมการกำกับและส่งเสริมการประกอบธุรกิจประกันภัย</div>
        <div class="box_footter_f2">22/79 ถนนรัชดาภิเษก แขวงจันทรเกษม เขตจตุจักร กรุงเทพมหานคร 10900 โทร.0-2515-3999 โทรสาร.0-2515-3970</div>
        <div class="box_footter_f3">webmaster@oic.or.th</div>
    </div>
    </div>
    </div>
    <div style="clear:both"></div>

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true"
        LoadScriptsBeforeUI="true" EnablePageMethods="true" ScriptMode="Release" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

 <asp:UpdatePanel ID="update" runat="server" UpdateMode="Always">
    <ContentTemplate>
 <asp:Panel ID="pnlMessageModalError" runat="server" CssClass="modalpopupMessage" Visible="false">
    <table  style="border: Solid 2px #ff0000; width:350px; height:200px; margin:200px auto ; padding:0px;background-color:#fff;"
        cellpadding="0" cellspacing="0">
        <tr style="background-color: Red">
            <td style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger;
                font-family: Calibri" align="left">
                <asp:Label ID="lblTitleMessage" runat="server" Font-Names="Arial" Text="ผิดพลาด" />
            </td>
            <td style="color: White; font-weight: bold; padding: 3px; font-size: larger" align="right">
                <asp:LinkButton ID="lbtnClose" runat="server" EnableTheming="True" 
                    ForeColor="White" onclick="btnClose_Click">X</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="padding: 5px; font-family: Calibri">
                <img src="Images/ico_error.png" width="40px" height="40px" />
                <div style="vertical-align: top;font-size:14px;">
                    <asp:Label ID="lblTextSesstion" runat="server" Text="session หมดอายุ กรุณาทำการล็อกอินใหม่"></asp:Label>
                    </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:Button ID="btnClose" runat="server" Text="ปิด" Height="25px" onclick="btnClose_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="PopUpEditPassword" runat="server" CancelControlID="btnCancel"
            TargetControlID="Button1" PopupControlID="pnlModalPassword" BackgroundCssClass="modalbackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Button ID="Button1" runat="server" Text="Open" Style="display: none" />

        <asp:Panel ID="pnlModalPassword" Style="display:none" runat="server" CssClass="modalpopup">
            <table  style="border: solid 2px #008000; width:450px; height:200px; margin:200px auto ; padding:0px;background-color:#fff;" cellpadding="0" cellspacing="0">
                <tr style="background-color: #008000">
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger; font-family: Calibri" align="left">
                    เปลี่ยนรหัสผ่าน
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding:10px;">รหัสผ่านเดิม</td>
                    <td>
                        <asp:TextBox ID="txtOldpassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqOldPW" runat="server" ControlToValidate="txtOldpassword" 
                            ValidationGroup="ChangePW" ErrorMessage="กรุณาระบุรหัสผ่านเดิม" CssClass="failureNotification">*
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding:10px;">รหัสผ่านใหม่</td>
                    <td>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
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
                    <td align="right" style="padding:10px;">ยืนยันรหัสผ่าน</td>
                    <td>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
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
                            CssClass="myButtonlogin"
                            onclick="btnPwdSave_Click" />
                        <asp:Button ID="btnPwdCancel" runat="server" Text="ยกเลิก" 
                            CssClass="myButtonlogin" onclick="btnPwdCancel_Click" />
                    </td>
                </tr>
        </table>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupError" runat="server" TargetControlID="divError" 
    PopupControlID="pnlModalError" BackgroundCssClass="modalbackground">
    </ajaxToolkit:ModalPopupExtender>
    <div id="divError" runat="server"></div>

        <asp:Panel ID="pnlModalError" runat="server" CssClass="modalpopup" style="display:none;">
            <table  style="border: Solid 2px #ff0000; width:450px; height:200px; margin:200px auto ; padding:0px;background-color:#fff;" cellpadding="0" cellspacing="0">
                <tr style="background-color: Red">
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger; font-family: Calibri" align="left">
                        <asp:Label ID="lblErrorTitle" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="padding: 5px; font-family: Calibri">
                        <img src="Images/ico_error.png" width="40px" height="40px" alt="" />
                        <div style="vertical-align: top;font-size:14px;">
                            <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center><asp:Button ID="btnCloseError" runat="server" Text="ปิด" 
                                CssClass="myButtonlogin" onclick="btnCloseError_Click" /></center>
                    </td>
                </tr>
        </table>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupSuccess" runat="server" CancelControlID="Button4"
            TargetControlID="Button3" PopupControlID="pnlModalSuccess" BackgroundCssClass="modalbackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Button ID="Button3" runat="server" Text="Open" Style="display: none" />
        <asp:Panel ID="pnlModalSuccess" Style="display: none" runat="server" CssClass="modalpopup">
            <table  style="border: Solid 2px #008000; width:450px; height:200px; margin:200px auto ; padding:0px;background-color:#fff;" cellpadding="0" cellspacing="0">
                <tr style="background-color: #008000">
                    <td colspan="2" style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger; font-family: Calibri" align="left">
                        <asp:Label ID="lblSuccessTitle" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" style="padding: 5px; font-family: Calibri">
                        <img src="Images/icon_Success.png" width="40px" height="40px" alt="" id="img_success" />
                        <div style="vertical-align: top;font-size:14px;">
                            <asp:Label ID="lblSuccessMsg" runat="server"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center>
                            <asp:Button ID="btnCloseSuccess" runat="server" Text="ตกลง" 
                                OnClientClick="lblSuccessMsg.innerHTML='กำลังไปหน้าหลัก..'; this.style.display='none';" 
                                CssClass="myButtonlogin" onclick="btnCloseSuccess_Click"/></center>
                    </td>
                </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
 </asp:UpdatePanel>
 <asp:UpdatePanel ID="udpLoading" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <ucload:UCLoading ID="ucLoading1" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

   

    </form>
</body>
</html>

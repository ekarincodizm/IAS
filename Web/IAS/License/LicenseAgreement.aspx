<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="LicenseAgreement.aspx.cs" Inherits="IAS.License.LicenseAgreement" %>
<%@ Register Src="~/UserControl/ucLicenseArgreement.ascx" TagName="ucLicenseArgeement" TagPrefix="ucArg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger  ControlID="btnNext"/>
        <asp:PostBackTrigger  ControlID="btnBack"/>
    </Triggers>
    <ContentTemplate>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         function ShowPopup(message) {
             $(function () {
                 $("#dialog").html(message);
                 $("#dialog").dialog({
                     title: "Result Message Popup",
                     buttons: {
                         Close: function () {
                             $(this).dialog('close');
                         }
                     },
                     modal: true
                 });
             });
         };
    </script>
        <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center" >
            <table  width="100%">
                <tr>
                    <td colspan="2" >
                        <table width="100%">
                            <tr>
                                <td>
                                    <%--<div id="DivEditWindow" runat="server" style="display: block">
                                    <iframe id="iframAgree" runat="server" width="70%" height="500px" scrolling="auto" frameborder="0" ></iframe>
                                    </div>--%>
                                     <div id="dialog" style="display: none; z-index:99999 !important"></div>
                                    <ucArg:ucLicenseArgeement ID="ucPDFArg" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:CheckBox ID="chkAgreement" runat="server" 
                            Text="ข้าพเจ้าขอรับรองว่าข้อความตามคำขอและเอกสารประกอบคำขอถูกต้องเป็นความจริงทุกประการ" Checked="false"
                            style="margin-left:200px;" oncheckedchanged="chkAgreement_CheckedChanged"  />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%;">
                                </td>
                                <td style="width: 25%;">
                                </td>
                                <td style="width: 25%;" align="right">
                                    <asp:ImageButton ID="btnBack" runat="server" 
                                        ImageUrl="~/Images/button_back.png" onclick="btnBack_Click" />
                                </td>
                                <td style="width: 25%;" align="left">
                                    <asp:ImageButton ID="btnNext" runat="server" 
                                        ImageUrl="~/Images/button_next.png" OnClick="btnNext_Click"  />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

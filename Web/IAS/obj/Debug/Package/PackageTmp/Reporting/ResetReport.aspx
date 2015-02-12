<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ResetReport.aspx.cs" Inherits="IAS.Reports.ResetReport" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function OpenPopup(para1, para2, para3) {
            var qstr = "../Reporting/Reset_Report.aspx?IDCard=" + para1 + "&FirstName=" + para2 + "&LastName=" + para3 + "&Click=Print";
            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            } else {
                return true;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

<div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
                <asp:Label Text="รายงานสถิติขอเปลี่ยนรหัสผ่าน" BorderStyle="None" ID="lblResult" runat="server" />
                </div>
            </div>


    <div>
        <asp:Panel ID="pnlMain" runat="server">
         

                        <div class="box_body_content">
            	        <div class="box_t_h">::<asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหา" /></div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">

                        <asp:UpdatePanel ID="update" runat="server">
                            <ContentTemplate>
                                                
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblIDCard" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label> :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt" MaxLength="13" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label> :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label> :
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"/>
                                </td>
                                <td style="width:10%;">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" OnClick="btnSearch_Click"
                                        Text="ค้นหา" /> 
                                   <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                        onclick="btnCancel_Click" />
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
    <asp:HiddenField ID="hdfSession" runat="server" />
    <div class="clear"></div>
     <div align="center" style="padding:10px;width:95%">
    <CR:CrystalReportViewer ID="CRYreset" runat="server" AutoDataBind="true" ToolPanelView="None"/>

    </div>
    </div>
    <div style="clear:left;"></div>
</asp:Content>

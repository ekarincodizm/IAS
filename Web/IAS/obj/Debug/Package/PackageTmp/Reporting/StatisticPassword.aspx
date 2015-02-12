<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="~/Reporting/StatisticPassword.aspx.cs" Inherits="IAS.Reporting.StatisticPassword" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenPopup() {
            var ddlLicenseType = document.getElementById('<%=ddlLicenseType.ClientID %>');
            var txtIDCard = document.getElementById('<%=txtIDCard.ClientID %>');
            var txtFirstName = document.getElementById('<%=txtFirstName.ClientID %>');
            var txtLastName = document.getElementById('<%=txtLastName.ClientID %>');


            var LicenseType_Select = ddlLicenseType.options[ddlLicenseType.selectedIndex].value;

            var qstr = "../Reporting/Rcp_Report.aspx?LicenseType=" + LicenseType_Select +
                       "&IDCard=" + txtIDCard.value +
                       "&FirstName=" + txtFirstName.value +
                       "&LastName=" + txtLastName.value +
                       "&Click=Print";

            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');

            //window.open(qstr, 'popupwindow', 'fullscreen=no,toolbar=0, menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            } else {
                return true;
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

                   <asp:Label Text="รายงานสถิติการขอใบเสร็จ" BorderStyle="None" ID="lblElectronicReceiptReport" runat="server" />

                </div>
            </div>  
        
        <br />
    <br />
    <div>



        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">         

                <div class="box_body_content">
            	        <div class="box_t_h">::ค้นหา</div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">

                        <asp:UpdatePanel ID="update" runat="server">
                            <ContentTemplate>         
                            
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right; width: 20%;">
                                    <asp:Label ID="lblTypeReceipt" runat="server" Text="ประเภทใบเสร็จ" CssClass="lbl"></asp:Label>
                                    <span>:</span>
                                </td>
                                <td style="text-align: left;">
                                    <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblIDCard" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                    <span>:</span>
                                </td>
                                <td style="text-align: left; ">
                                    <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                </td>
                                <td style=" width: 20%;" rowspan="2">
                                   <%-- <asp:Button ID="btnPrint" runat="server" Text="พิมพ์" CssClass="btn" OnClientClick="OpenPopup()" />--%>
                                    <input type="button" onclick="OpenPopup()" value="ค้นหา" class="btn"/>
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                        onclick="btnCancel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblFirstName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                                    <span>:</span>
                                </td>
                                <td style="text-align: left; ">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"></asp:TextBox>
                                </td>
                                <td style="text-align: right; ">
                                    <asp:Label ID="lblLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                                    <span>:</span>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" onkeypress="return spacialCaractor(event)"></asp:TextBox>
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
   
        <CR:CrystalReportViewer ID="CRVStatic" runat="server" AutoDataBind="true" 
            ToolPanelView="None" />
    
    </div>

    </div>
    <div style="clear:left;"></div>
</asp:Content>

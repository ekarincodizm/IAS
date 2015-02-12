<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="RequestEditApplicant.aspx.cs" Inherits="IAS.Applicant.RequestEditApplicant" %>
<%@ Register Src="~/UserControl/ucAttachFileControl.ascx" TagName="ucAttachFileControl" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmUpdate() {
            return confirm('คุณต้องการแก้ไขข้อมูลผู้สมัครสอบหรือไม่?');
        }

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
    <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
<ContentTemplate>
      <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลเดิมในระบบ" BorderStyle="None" ID="lblDetailPersonExam" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                        <asp:Panel ID="PnlSearch" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    :: ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">  
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblTestingNo" runat="server" Text="รหัสรอบสอบ" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtTestingNo" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"
                                                                                        MaxLength="6" CssClass="txt"></asp:TextBox>
                                                </td>
                                                 <td style="text-align: right;">
                                                    <asp:Label ID="lblIDCard" runat="server" Text="รหัสประจำตัวประชาชนเดิม" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtIdCard" runat="server" CssClass="txt" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"
                                                                                        autocomplete="off" MaxLength="13"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" 
                                                        onclick="btnSearch_Click"/>
                                                </td>
                                            </tr>
                                            <tr >
                                                <td style="text-align:right;">
                                                     <asp:Label ID="lblPreNameCode" runat="server" Text="คำนำหน้า:" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtPreNameCode" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="lblNames" runat="server" Text="ชื่อ:" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtNames" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="lblLastName" runat="server" Text="นามสกุล:" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                                        onclick="btnCancel_Click" /> 
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                
                                                <td>
                                                    <asp:Button ID="btnAddNew" runat="server" CssClass="btn" Visible="false" 
                                                        Text="ส่งเรื่องใหม่" onclick="btnAddNew_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Label ID="lblStatusName" runat="server" CssClass="lbl" Visible="False" 
                                        Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                <table width="100%">
                    <tr>
                        <td>
                        <asp:Panel ID="PanelEditContent" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    :: ข้อมูลที่ต้องการแก้ไข</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                            
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="text-align:right">
                                                <asp:Label ID="lblExamPlacecCode" runat="server" Text="สมาคมที่พิจารณา" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtExamPlaceCode" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblChangeIDVisibleF" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblTestingNoVisibleF" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewIDCard" runat="server" Text="รหัสประจำตัวประชาชน" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtNewIDCard" runat="server" CssClass="txt" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"
                                                                                        autocomplete="off" MaxLength="13"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewPreNameCode" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:DropDownList ID="ddlNewPreNameCode" runat="server" CssClass="ddl"></asp:DropDownList>
                                            </td>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewNames" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtNewNames" runat="server" CssClass="txt"  onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                            </td>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left">
                                                <asp:TextBox ID="txtNewLastName" runat="server" CssClass="txt"  onkeypress="return spacialCaractor(event);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblRemark" runat="server" Text="หมายเหตุ" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;" colspan="3">
                                                <asp:TextBox ID="txtRemark" runat="server" Width="400px" Height="100px"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                       <%-- <tr>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblAttachFile" runat="server" Text="แนบไฟล์ที่เกี่ยวข้อง" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;" colspan="4">
                                                <asp:DropDownList ID="ddlAttrachFile" runat="server" CssClass="ddl" >
                                                    <asp:ListItem Value="0">เลือก</asp:ListItem>
                                                    <asp:ListItem Value="1">บัตรประจำตัวชาชน</asp:ListItem>
                                                    <asp:ListItem Value="2">สำเนาบัตรประชาชนเปลี่ยนชื่อ</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtAttrachFile" runat="server" Width="300px" ></asp:TextBox>
                                                <asp:Button ID="btnSelect" runat="server" Text="เลือก..." CssClass="btn" 
                                                    onclick="btnSelect_Click" />
                                            </td>
                                            <td style="text-align:left;" >
                                                <asp:Button ID="btnAddAttachFile" runat="server" Text="แนบไฟล์" CssClass="btn" />
                                            </td>
                                        </tr>--%>
                                    </table>
                                    <table style="width:100%;">
                                        <tr>
                                            <td style="text-align:center">
                                                <%--<asp:GridView ID="gvbind" runat="server" CssClass="datatable" AutoGenerateColumns="false" >
                                                    <Columns>
                                                        <asp:BoundField HeaderText="ลำดับ" />
                                                    </Columns>
                                                </asp:GridView>--%>
                                                <asp:GridView ID="gvAttachFile" runat="server" GridLines="None" Width="80%" CssClass="datatable"
                                                EnableTheming="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" BorderWidth="1px"
                                                CellSpacing="-1" AllowSorting="True"  >
                                                <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                               <EmptyDataTemplate>
                                                                    <div style="text-align: center">ไม่พบข้อมูล...</div>
                                                               </EmptyDataTemplate>
                                                                    <PagerStyle HorizontalAlign="Left" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNO" runat="server" Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                </div>
                            </div>
                            <table style="width:100%;">
                            
                            <tr>
                                <td>
<%--                                <asp:UpdatePanel ID="udpAtt" runat="server" ClientIDMode="Static">
                                    <ContentTemplate>
                                        <uc4:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
                                        </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <%--<uc4:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />--%>
                                </td>
                            </tr>
                            </table>   
                        </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <asp:Panel ID="pnlAttachFileControl" runat="server" >
                                <uc4:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
                            </asp:Panel>
                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelSend" runat="server" Visible="false">
                                <table width="100%">
                                <tr>
                                <td ></td>
                                </tr>
                                <tr>
                                    <td align="center" style="width:100%">
                                        <asp:Button ID="btnSend" runat="server" Text="ส่งคำขอแก้ไข" CssClass="btn" 
                                            onclick="btnSend_Click" OnClientClick="return confirmUpdate();" />
                                   &nbsp;
                                        <asp:Button ID="btnCancleSend" runat="server" Text="ยกเลิก" CssClass="btn" 
                                            onclick="btnCancleSend_Click" /> 
                                    </td>
                                </tr>
                            </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>  
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

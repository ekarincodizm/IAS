<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="ApproveEditApplicant.aspx.cs" Inherits="IAS.Applicant.ApproveEditApplicant" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/ucAttachFileControl.ascx" TagName="ucAttachFileControl" tagprefix="uc4" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmUpdate() {
            return confirm('คุณต้องบันทึกผลการพิจารณาหรือไม่?');
        }

        function showload() {
            document.getElementById('Detail_UCLoadingProgress1_Image1').src = '../Images/newloading23.gif';
            $find('Detail_UCLoadingProgress1_mpeProgress').show();

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
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="พิจารณาการแก้ไขข้อมูลผู้สมัครสอบ" BorderStyle="None" ID="lblDetailPersonExam" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                         <asp:Panel ID="PnlSearch" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    :: ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">  
                                        <table style="width:100%;">
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="lblTestingNo" runat="server" Text="รหัสรอบสอบ" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtTestingNo" runat="server" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"
                                                                                        MaxLength="6" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="lblIDCard" runat="server" Text="รหัสประจำตัวประชาชนเดิม" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"
                                                                                        autocomplete="off" MaxLength="13"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" 
                                                        onclick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="lblStatusApprove" runat="server" Text="สถานะการพิจารณา" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align:left;">
                                                     <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl">
                                                    <asp:ListItem Text="เลือก" ></asp:ListItem>
                                                    <asp:ListItem Text="รอการพิจารณา(คปภ.)" Value="110"></asp:ListItem>
                                                    <asp:ListItem Text="อนุมัติ(คปภ.)" Value="211"></asp:ListItem>
                                                    <asp:ListItem Text="ไม่อนุมัติ(คปภ.)" Value="212"></asp:ListItem>
                                                    <asp:ListItem Text="ทั้งหมด" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align:right;">
                                                    
                                                </td>
                                                <td style="text-align:left;">
                                                   
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" 
                                                        onclick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                         <asp:Panel ID="PnlDetailSearchGridView" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                
                                <div class="box_textbox_bg_table" runat="server" id="bludDiv1">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvDetail" runat="server" GridLines="None" Width="100%" CssClass="datatable"
                                                        EnableTheming="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" BorderWidth="1px"
                                                        CellSpacing="-1" AllowSorting="True" PageSize="20" onrowcommand="gvSearch_RowCommand">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center">
                                                                ไม่พบข้อมูล...</div>
                                                        </EmptyDataTemplate>
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="changeid" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblchangeid" runat="server" Text='<%# Bind("CHANGE_ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamPlaceCode" runat="server" Text='<%# Bind("EXAM_OWNER") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRownum" runat="server" Text='<%# Bind("Rowno") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTestingNo" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เลขบัตรประชาชนเดิม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIDCard" runat="server" Text='<%# Bind("OLD_ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("OLD_FNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("OLD_LNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สมาคมที่พิจารณา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblExamPlaceName" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="สถานะการพิจารณา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUSNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplView" runat="server" CommandArgument="select" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="รหัสบัตรประชาชนใหม่" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewIDCARD" runat="server" Text='<%# Bind("NEW_ID_CARD_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="คำนำหน้าชื่อเก่า" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOLDPreFix" runat="server" Text='<%# Bind("OLD_PREFIX") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="คำนำหน้าชื่อ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldPreFixName" runat="server" Text='<%# Bind("OLD_PREFIX_NAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="คำนำหน้าชื่อใหม่" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewPreFix" runat="server" Text='<%# Bind("NEW_PREFIX") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อใหม่" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewFName" runat="server" Text='<%# Bind("NEW_FNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="นามสกุลใหม่" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNewLName" runat="server" Text='<%# Bind("NEW_LNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="หมายเหตุ" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("REMARK") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            
                                                            <asp:TemplateField HeaderText="compcode" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCompCode" runat="server" Text='<%# Bind("comp_code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="cancelreason" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCancelReason" runat="server" Text='<%# Bind("CANCEL_REASON") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="oicresult" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOICResult" runat="server" Text='<%# Bind("OIC_RESULT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="assoresult" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAssoResult" runat="server" Text='<%# Bind("ASSOCIATION_RESULT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="statusid" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatusID" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false"
                                                                    Text="1" Height="22px"></asp:TextBox>
                                                                <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                                <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;"
                                                                    Visible="true" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" Style="text-align: center"
                                                                    MaxLength="4" ondrop="return false;" AutoComplete="Off" onpaste="return false;"
                                                                    Width="5%" onkeypress="return runScript(event) "></asp:TextBox>&nbsp;
                                                                <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearchGo_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <div runat="server" id="div_totalPage">
                                                                    จำนวน <b>
                                                                        <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label></b> รายการ</div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
                                    <asp:Label ID="lblStatusNames" runat="server" CssClass="lbl" Visible="False" 
                                        Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                         
                         <asp:Panel ID="PanelOldApplicant" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    :: ข้อมูลเดิมในระบบ</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">  
                                        <table style="width: 100%;">
                                           <tr >
                                                <td style="text-align:right;">
                                                     <asp:Label ID="lblPreNameCode" runat="server" Text="คำนำหน้า" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtPreNameCode" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="lblNames" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtNames" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td style="text-align:right;">
                                                    <asp:Label ID="lblLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>:
                                                </td>
                                                <td style="text-align:left;">
                                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                         <asp:Panel ID="PanelEditContent" runat="server">
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
                                                <asp:Label ID="lblChangeid" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblStatusName" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblIDCARDVisibleF" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblTestingNOVisibleF" runat="server" CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblCompCodeVisibleF" runat="server"  CssClass="lbl" Visible="false"></asp:Label>

                                                <asp:Label ID="lblNewIDCardVisibleF" runat="server"  CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblNewPreName" runat="server"  CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblNewFirstName" runat="server"  CssClass="lbl" Visible="false"></asp:Label>
                                                <asp:Label ID="lblNewLastNames" runat="server"  CssClass="lbl" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewIDCard" runat="server" Text="รหัสประจำตัวประชาชน" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtNewIDCard" runat="server" CssClass="txt" Enabled="false" onkeyup="this.value=this.value.replace(/[^0-9]/g,'');"
                                                                                        autocomplete="off" MaxLength="13" ></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewPreNameCode" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:DropDownList ID="ddlNewPreNameCode" runat="server" CssClass="ddl" Enabled="false"></asp:DropDownList>
                                            </td>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewNames" runat="server" Text="ชื่อ" CssClass="lbl" ></asp:Label>:
                                            </td>
                                            <td style="text-align:left;">
                                                <asp:TextBox ID="txtNewNames" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblNewLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left">
                                                <asp:TextBox ID="txtNewLastName" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:right;">
                                                <asp:Label ID="lblRemark" runat="server" Text="หมายเหตุ" CssClass="lbl"></asp:Label>:
                                            </td>
                                            <td style="text-align:left;" colspan="3">
                                                <asp:TextBox ID="txtRemark" runat="server" Width="400px" Height="100px" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                       
                                    </table>

                                    </div>
                                </div>
                            </div>
                            <table style="width:100%;">
                            <tr>
                                <td>
                                        <uc4:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PanelApprove" runat="server">
                        <div class="DivApprove">
                        <table style="width:100%;">
                            <tr>
                                <td style="text-align:right;" width="305px">
                                      <asp:Label ID="lblApprove" runat="server" Text="ผลการพิจารณา" CssClass="lbl"></asp:Label>
                                </td>
                                <td style="text-align:left;" width="195px">
                                        <asp:DropDownList ID="ddlApprove" runat="server" CssClass="ddl" width="140px" 
                                            AutoPostBack="true" onselectedindexchanged="ddlApprove_SelectedIndexChanged">
                                            <asp:ListItem Text="เลือก"></asp:ListItem>
                                            <asp:ListItem Text="อนุมัติ" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="ไม่อนุมัติ" Value="2"></asp:ListItem>

                                        </asp:DropDownList>

                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblReason" runat="server" Text="เนื่องจาก :" CssClass="lbl" Visible="false"></asp:Label>

                                </td>
                                <td style="text-align:left" width="500px">
                                            <asp:TextBox ID="txtReason" runat="server" width="500px" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                            <table style="width:100%;">
                               
                                <tr>
                                    <td style="text-align:right;" width="50%">
                                        <asp:Button ID="btnSend" runat="server" Text="ส่งผลการพิจารณา" CssClass="btn" 
                                            onclick="btnSend_Click"  OnClientClick="return confirmUpdate();"/>
                                    </td>
                                    <td style="text-align:left;" width="50%">
                                        <asp:Button ID="btnCancleSend" runat="server" Text="ยกเลิก" CssClass="btn" 
                                            onclick="btnCancleSend_Click" /> 
                                    </td>
                                </tr>
                            </table>
                            </div>
                            </asp:Panel>
                            </asp:Panel>
                        <br />
                        
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>

                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>

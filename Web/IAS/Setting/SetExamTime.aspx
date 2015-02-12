<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SetExamTime.aspx.cs" Inherits="IAS.Setting.SetExamTime" %>
<%@ Register src="../UserControl/UCModalError.ascx" tagname="UCModalError" tagprefix="uc1" %>
<%@ Register src="../UserControl/UCModalSuccess.ascx" tagname="UCModalSuccess" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            return confirm('คุณต้องการลบหรือไม่?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">


 <div class="box_content" align="center">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
                      ตั้งค่าเวลาสอบ
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanelGridviewExamTime" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <div style="clear:left;"></div>
            <table width="100%" cellpadding="5" cellspacing="5">
                <tr>
                    <td align="left">
                        <div class="box_t_h">::เวลาสอบ</div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div class="box_textbox_bgadd">
                             <table width="100%">
                             <tr>
                                <td style="width:5px"></td>
                                <td><asp:Label runat="server" Text="เวลาสอบเริ่มต้น (ชั่วโมง : นาที)"></asp:Label></td>
                                <td><asp:DropDownList runat="server" ID="st_hr" Width="60px"  AutoPostBack="true"
                                        onselectedindexchanged="st_hr_SelectedIndexChanged"></asp:DropDownList>
                                    &nbsp;&nbsp;:&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="st_min" Width="60px" AutoPostBack="true" 
                                        onselectedindexchanged="st_min_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td style="width:5px"></td>
                                <td><asp:Label runat="server" Text="เวลาสอบสิ้นสุด (ชั่วโมง : นาที)"></asp:Label></td>
                                <td><asp:DropDownList runat="server" ID="en_hr" Width="60px"></asp:DropDownList>
                                    &nbsp;&nbsp;:&nbsp;&nbsp;
                                    <asp:DropDownList runat="server" ID="en_min" Width="60px"></asp:DropDownList>
                                </td>
                                <td style="width:5px"></td>
                                <td align="center">
                                    <asp:Button runat="server" ID="btn_Search" Text="ค้นหา" Width="80" 
                                        onclick="btn_Search_Click" />
                                    <asp:Button runat="server" ID="btn_Add" Text="เพิ่มข้อมูล" Width="80" 
                                        Visible="false" onclick="btn_Add_Click"/>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btn_Clear" Text="ยกเลิก" Width="80" 
                                        onclick="btn_Clear_Click" />
                                </td>
                                <td style="width:5px"></td>
                            </tr>
                            <tr>
                                <td colspan="10" style="height:30px" align="right" valign="bottom">
                                    <asp:Label runat="server" ID="lbl_txt_show" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td >
                        
                        <table width="100%">
                            <tr>
                                <td style="width:5px" rowspan="2"></td>
                                <td align="center">
                                    <asp:GridView ID="gvTime" runat="server" GridLines="None" Width="80%" CssClass="datatable"
                                            EnableTheming="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" BorderWidth="1px"
                                            CellSpacing="-1" AllowSorting="True" PageSize="20" EmptyDataText=" ไม่พบข้อมูล...">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                           
                                                           <%-- <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHeadRequestNO" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="timeCode" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="2%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTimeCode" runat="server" Text='<%# Bind("TIMECODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="5%" CssClass="td-center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRownum" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ช่วงเวลาที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="20%"  />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTxtTime" runat="server" Text=' <%# Bind("TXTTIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เวลาเริ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%"  CssClass="td-center"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStart" runat="server" Text='<%# Bind("STIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เวลาสิ้นสุด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%"  CssClass="td-center"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEnd" runat="server" Text='<%# Bind("ETIME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เวลาในการสอบ (นาที)" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("DIFFMIN") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ยกเลิกรายการนี้" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                 <ItemTemplate> 
                                                                    <asp:LinkButton ID="hplCancel" runat="server" OnClick="hplCancel_Click" OnClientClick="return confirmDelete();"><img src="../Images/x-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                </td>
                                <td style="width:5px" rowspan="2"></td>
                            </tr>
                            <tr>
                                <td>
                             <%--     <table width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                                                <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false"
                                                                    Text="1"></asp:TextBox>
                                                                <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="false"></asp:Label>
                                                                <asp:Label ID="txtTotalPage" runat="server" Visible="false"></asp:Label>
                                                                <asp:Button ID="btnNextGvSearch" runat="server" OnClick="btnNextGvSearch_Click" Text="&gt;"
                                                                    Visible="true" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="TXTrowperpage" runat="server" Visible="false" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                                                <asp:TextBox ID="rowPerpage" runat="server" Visible="false" Style="text-align: center"
                                                                    MaxLength="4" ondrop="return false;" AutoComplete="Off" onpaste="return false;"
                                                                    Width="5%" onkeypress="return runScript(event) "></asp:TextBox>&nbsp;
                                                                <asp:Button ID="pageGo" runat="server" Visible="false" Text="GO" OnClick="btnSearch_Click" />
                                                            </td>
                                                            <td align="right">
                                                                <div runat="server" id="div_totalPage">
                                                                    จำนวน <b>
                                                                        <asp:Label ID="lblTotal" runat="server" Visible="false"></asp:Label></b> รายการ</div>
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                                    <asp:Panel ID="PanalPage" runat="server" Width="100%" Visible="false">
                                                    <table width="80%" align="center" >
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
                                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                    
                </tr>

               

           
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>



                <uc1:UCModalError ID="UCModalError1" runat="server" />
            <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />

                <br />
                <br />
                <br />
    

    </div>
    <div style="clear:left;"></div>

</asp:Content>

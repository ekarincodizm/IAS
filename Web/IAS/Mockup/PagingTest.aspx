<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagingTest.aspx.cs" Inherits="IAS.Mockup.PagingTest" %>
<%-- <%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <div>

        
                        <div class="box_body_content">
            	        <div class="box_t_h">::ค้นหา</div>
            	        <div class="box_textbox_bgadd">
                	    <div class="box_texbox_add_box">


                 <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width:15%;">
                            <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label> :
                        </td>
                        <td style="text-align: left;width:25%;">
                            <asp:DropDownList ID="ddlSearchMemberType" runat="server" CssClass="ddl" 
                                Width="150px"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="SearchMemberTypeRequired" runat="server" ControlToValidate="ddlSearchMemberType"
                                CssClass="failureNotification" ErrorMessage="SearchMemberType is required." ToolTip="SearchMemberType is required."
                                ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="สถานะ"></asp:Label> :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="ddl" 
                                Width="150px" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="StatusRequired" runat="server" ControlToValidate="ddlSearchStatus"
                                CssClass="failureNotification" ErrorMessage="Status is required." ToolTip="Status is required." Display="Dynamic"
                                ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label> :
                        </td>
                        <td style="text-align: left; ">
                            <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td rowspan="2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label> :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label> :
                        </td>
                        <td style="text-align: left; ">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; ">
                            <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label> :
                        </td>
                        <td style="text-align: left; ">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                </table>

                    </div>
                    </div>
                    </div>
                    


              <asp:GridView ID="gvSearchOfficerOIC" runat="server"
                 AutoGenerateColumns="False"
            AllowSorting="True" BorderWidth="2px" BackColor="White" 
            GridLines="None" CellPadding="3"
            CellSpacing="1" BorderStyle="Ridge" BorderColor="White"
            AllowPaging="True" Width="100%" 
                            onpageindexchanged="gvSearchOfficerOIC_PageIndexChanged" 
                            onpageindexchanging="gvSearchOfficerOIC_PageIndexChanging" >
            <FooterStyle ForeColor="Black" 
               BackColor="#C6C3C6"></FooterStyle>
                  <PagerSettings Mode="NextPreviousFirstLast" />
            <PagerStyle ForeColor="Black" HorizontalAlign="Right" 
               BackColor="#C6C3C6"></PagerStyle>
            <HeaderStyle ForeColor="#E7E7FF" Font-Bold="True" 
               BackColor="#4A3C8C"></HeaderStyle>
     <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="CheckAllRecords(this,'chkSelect')" runat="server"
                                        type="checkbox" visible="true"/>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="false" AutoPostBack="false" OnCheckedChanged="chkSelect_CheckedChanged" visible="true" />
                                </ItemTemplate>
                                <ItemStyle CssClass="td-center"/>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่สมาชิก" Visible="false">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IDCard">
                                <HeaderTemplate>
                                    เลขบัตรประชาชน
                                </HeaderTemplate>
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIDCard" runat="server" Text='<%#Bind("ID_CARD_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <HeaderTemplate>
                                    อีเมล
                                </HeaderTemplate>
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%#Bind("EMAIL") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OICCODE">
                                <HeaderTemplate>
                                    รหัสเจ้าหน้าที่
                                </HeaderTemplate>
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblOicCode" runat="server" Text='<%#Bind("OIC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FirstName">
                                <HeaderTemplate>
                                    ชื่อ
                                </HeaderTemplate>
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="FirstName" runat="server" Text='<%#Bind("NAMES") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LastName">
                                <HeaderTemplate>
                                    นามสกุล
                                </HeaderTemplate>
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLastName" runat="server" Text='<%#Bind("LASTNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MemberType">
                                <HeaderTemplate>
                                    ประเภทสมาชิก
                                </HeaderTemplate>
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMemberType" runat="server" Text='<%#Bind("MEMBER_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TELEPHONE">
                                <HeaderTemplate>
                                    มือถือ
                                </HeaderTemplate>
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTel" runat="server" Text='<%#Bind("TELEPHONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ZipCode">
                                <HeaderTemplate>
                                    รหัสไปรษณีย์
                                </HeaderTemplate>
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblZipCode" runat="server" Text='<%#Bind("ZIP_CODE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <HeaderTemplate>
                                    สถานะ
                                </HeaderTemplate>
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("STATUS_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="URL" ItemStyle-CssClass="td-center">
                                <HeaderTemplate>
                                    ดำเนินการ
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>&nbsp;&nbsp;
                                    <asp:LinkButton ID="hplApprove" runat="server" OnClick="hplApprove_Click" Visible="false"><img src="../Images/approved-icon.gif" title="อนุมัติ" /></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>

<ItemStyle CssClass="td-center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
            <SelectedRowStyle ForeColor="White" Font-Bold="True" 
                 BackColor="#9471DE"></SelectedRowStyle>
            <RowStyle ForeColor="Black" BackColor="#DEDFDE"></RowStyle>
        
        
        
        </asp:GridView>

        <i>You are viewing page
        <%=gvSearchOfficerOIC.PageIndex + 1%>
        of
        <%=gvSearchOfficerOIC.PageCount%>
        </i>
    </div>

    </div>
    </form>
          <%--      <uc2:UCModalError ID="UCModalError" runat="server" />
            <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />--%>
</body>
</html>

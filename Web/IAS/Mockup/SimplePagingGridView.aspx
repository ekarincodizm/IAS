<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="SimplePagingGridView.aspx.cs" Inherits="IAS.Mockup.SimplePagingGridView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
            &nbsp;<fieldset style="width: 90%; margin: 0 auto;">
                <legend class="lbl">ค้นหา </legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <span>
                                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <span>
                                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <span>
                                <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <span>
                                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:DropDownList ID="ddlSearchMemberType" runat="server" CssClass="ddl" Width="150px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="SearchMemberTypeRequired" runat="server" ControlToValidate="ddlSearchMemberType"
                                CssClass="failureNotification" ErrorMessage="SearchMemberType is required." ToolTip="SearchMemberType is required."
                                ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <span>
                                <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <span>
                                <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="สถานะ"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="ddl" Width="150px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="StatusRequired" runat="server" ControlToValidate="ddlSearchStatus"
                                CssClass="failureNotification" ErrorMessage="Status is required." ToolTip="Status is required."
                                ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <div style="text-align: center">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" OnClick="btnSearch_Click" />   &nbsp; PageSize : <asp:TextBox runat="server" ID="txtPageSize" Width="30px" Text="10" ></asp:TextBox>
                </div>
            </fieldset>
            <asp:Panel runat="server" ID="pnlPagingGrid">
            <table>
            <tr>
                <td >
                  <asp:GridView ID="gvSearchOfficerOIC" runat="server" 
                GridLines="Vertical" Width="100%"  
                        BorderStyle="None" EnableTheming="True" 
                AutoGenerateColumns="False" AllowSorting="True"
                        OnRowDataBound="gvSearchOfficerOIC_RowDataBound" ShowFooter="True" >
                        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText=">" PreviousPageText="<"
                            Mode="NumericFirstLast" />
                        <AlternatingRowStyle CssClass="table_alter_row" />
                        <HeaderStyle BorderStyle="None" />
                        <PagerStyle CssClass="table_item_row" HorizontalAlign="Left" />
                        <RowStyle CssClass="table_item_row" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                        type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่สมาชิก" Visible="true">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IDCard">
                                <HeaderTemplate>
                                    เลขบัตรประชาชน
                                </HeaderTemplate>
                                <ItemStyle Width="10%" />
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
                                <ItemStyle Width="10%" />
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
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblMemberType" runat="server" Text='<%#Bind("MEMBER_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TELEPHONE">
                                <HeaderTemplate>
                                    มือถือ
                                </HeaderTemplate>
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTel" runat="server" Text='<%#Bind("TELEPHONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ZipCode">
                                <HeaderTemplate>
                                    รหัสไปรษณีย์
                                </HeaderTemplate>
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblZipCode" runat="server" Text='<%#Bind("ZIP_CODE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <HeaderTemplate>
                                    สถานะ
                                </HeaderTemplate>
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("STATUS_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="URL">
                                <HeaderTemplate>
                                    ดำเนินการ
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click">ดู</asp:LinkButton>
                                    <asp:LinkButton ID="hplApprove" runat="server" OnClick="hplApprove_Click" Visible="false">อนุมัติ</asp:LinkButton>
                                </ItemTemplate>
                    
                            </asp:TemplateField>
                        </Columns>
             
                    </asp:GridView>

                             </td>
                            </tr>  
                                <tr>
                                <td align="center">
                                    <table><tr>
                                    <td><asp:LinkButton ID="hplPrevious" runat="server" OnClick="hplPrevious_Click"><h3><</h3></asp:LinkButton></td>
                                    <td><asp:TextBox ID="txtCurrentPage" runat="server" OnTextChanged="txtCurrentPage_TextChanged"></asp:TextBox></td>
                                    <td><asp:LinkButton ID="hplNext" runat="server" OnClick="hplNext_Click"><h3>></h3></asp:LinkButton></td>
                                    </tr>
                                    </table> 
                                 </td>
                                 </tr>
                                
                        </table> 

            </asp:Panel>
                  
</asp:Content>

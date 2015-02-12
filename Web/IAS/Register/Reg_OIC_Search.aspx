<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Reg_OIC_Search.aspx.cs" Inherits="IAS.Register.Reg_OIC_Search" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">

        function SelectAllCheckboxes(spanChk) {

            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

<div class="box_content">
	<div class="box_content_header">
		<div class="box_content_header_text"></div>
	</div>

    <asp:UpdatePanel ID="upnSeachOIC" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:ValidationSummary ID="SearchOfficerOICValidationSummary" runat="server" CssClass="failureNotification"
                ValidationGroup="SearchOfficerOICValidationGroup" />
                
                <div class="box_body_content">
		        <div class="box_t_h">::ค้นหา</div>
		        <div class="box_textbox_bgadd">
			        <div class="box_texbox_add_box">
               <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right;">
                            <span>
                                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            <span>
                                <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td rowspan="3"><asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" OnClick="btnSearch_Click" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <span>
                                <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left; ">
                            <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right;">
                            <span>
                                <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlSearchMemberType" runat="server" CssClass="ddl" Width="150px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="SearchMemberTypeRequired" runat="server" ControlToValidate="ddlSearchMemberType"
                                CssClass="failureNotification" ErrorMessage="SearchMemberType is required." ToolTip="SearchMemberType is required."
                                ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <span>
                                <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; ">
                            <span>
                                <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="สถานะ"></asp:Label>
                                :</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="ddl" Width="150px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="StatusRequired" runat="server" ControlToValidate="ddlSearchStatus"
                                CssClass="failureNotification" ErrorMessage="Status is required." ToolTip="Status is required."
                                ValidationGroup="SearchOfficerOICValidationGroup">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                
                    </div>
		        </div>
	        </div>
            <br />

            <asp:Panel ID="pnlDetail" runat="server" Visible="true">
             
         
                    <asp:GridView ID="gvSearchOfficerOIC" runat="server" GridLines="None" Width="100%" BorderWidth="1"
                        EnableTheming="True" AutoGenerateColumns="False" AllowSorting="True" CellSpacing="-1"
                        OnRowDataBound="gvSearchOfficerOIC_RowDataBound" CssClass="datatable">
                        <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText=">" PreviousPageText="<"
                            Mode="NumericFirstLast" />
                        <AlternatingRowStyle CssClass="altrow" />
                        <HeaderStyle BorderStyle="None" />
                        <PagerStyle HorizontalAlign="Left" />
                        
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server"
                                        type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle CssClass="td-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่สมาชิก" Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
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
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                        <table>
                       <tr>
                                <td>
                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1" Enabled = "false"></asp:TextBox>
                                    <asp:Label ID="lblspace" runat="server" Text="/" Visible ="false"></asp:Label>
                                    <asp:Label ID="txtTotalPage" runat="server" Visible = "false"></asp:Label>
                                    <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                </td>
                            </tr>
                    </table>
                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnSeachOIC">
                        <ProgressTemplate>
                            Please wait image uploaded....
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Button runat="server" ID="btnGroupApprove" Text="อนุมัติกลุ่ม" OnClick="btnGroupApprove_Click"
                        Visible="false" />
             
            </asp:Panel>
            <uc2:UCModalError ID="UCModalError" runat="server" />
            <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    </div>
<div style="clear:left;"></div>
</asp:Content>

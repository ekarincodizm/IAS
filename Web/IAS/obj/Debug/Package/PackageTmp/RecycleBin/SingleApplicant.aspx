<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SingleApplicant.aspx.cs" Inherits="IAS.RecycleBin.SingleApplicant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:Label Text="ข้อมูลสมัครสอบ" BorderStyle="None" ID="lblExam" CssClass="Initial"
        runat="server" /><br />
    <br />
    <asp:UpdatePanel ID="upn" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlSearch" runat="server">
                <fieldset style="width: 90%; margin: 0px auto 10px auto;">
                    <legend>
                        <asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหารหัสสอบ" />
                    </legend>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblYard" runat="server" CssClass="lbl" Text="สถานที่สอบ"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlPlace" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblTypeLicense" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlTypeLicense" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblMonth" runat="server" CssClass="lbl" Text="เดือน"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblYear" runat="server" CssClass="lbl" Text="ปี"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtYear" runat="server" Width="120" Style="text-align: center" />
                                <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="txtYear"
                                    Width="120" RefValues="" ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID=""
                                    TargetButtonUpID="" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%; height: 17px;">
                                <asp:Label ID="lblTimeExam" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%; height: 17px;">
                                <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%; height: 17px;">
                                <asp:Label ID="lblDisplay" runat="server" CssClass="lbl" Text="แสดงผล"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%; height: 17px;">
                                <asp:RadioButtonList ID="rblDisplay" runat="server" CssClass="rb" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblDisplay_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="True" Text="ปฏิทิน" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="ตาราง" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 30%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" OnClick="btnSearch_Click" />
                            </td>
                            <td style="text-align: left; width: 30%;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="pnlCalendar" runat="server" Visible="true">
                <div style="width: 90%; margin: 0px auto 10px auto;">
                    <asp:Calendar ID="cld" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4"
                        DayNameFormat="Full" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="382px"
                        Width="100%" ShowGridLines="True">
                        <DayHeaderStyle BackColor="#CCCCCC" BorderColor="Black" Font-Bold="True" Height="50px"
                            HorizontalAlign="Left" />
                        <DayStyle BorderColor="Black" Height="50px" HorizontalAlign="Left" />
                        <TitleStyle BorderColor="Black" Height="15px" HorizontalAlign="Center" />
                        <TodayDayStyle BackColor="#FF9900" />
                    </asp:Calendar>
                    <br />
                    <asp:GridView ID="gvSingleApplication" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CaptionAlign="Top" CellPadding="3" DataKeyNames="ID" EnableTheming="True"
                        GridLines="Vertical" HeaderStyle-BackColor="#999999" PagerSettings-FirstPageText="First"
                        PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;"
                        PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager"
                        PagerStyle-HorizontalAlign="Center" PageSize="12" ssClass="gridtxlist" Width="100%">
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            NextPageText="&gt;" PreviousPageText="&lt;" />
                        <RowStyle CssClass="view" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                        <PagerStyle CssClass="viewpager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="viewheader" Font-Bold="True" />
                        <AlternatingRowStyle CssClass="view2" />
                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                        <Columns>
                            <asp:CommandField ButtonType="Link" CancelText="ยกเลิก" EditText="แก้ไข" ItemStyle-Width="10%"
                                ShowCancelButton="true" ShowEditButton="true" UpdateText="ตกลง" />
                            <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamNumber" runat="server" Text='<%# Bind("LicenseNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("ApproveDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="กลุ่มสนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamYard" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProvince" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("ExpireDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
            <asp:Button ID="btnModal" runat="server" Text="Open" OnClick="btnModal_Click" />
            <ajaxToolkit:ModalPopupExtender ID="ModSingleApplicant" runat="server" CancelControlID="btnCancel"
                OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                Drag="true" BackgroundCssClass="modalbackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlModal" Style="display: block" runat="server" CssClass="modalpopup">
                <fieldset style="width: 90%; margin: 0px auto 10px auto; background: white;">
                    <legend>
                        <asp:Label ID="lblModalExam" runat="server" BorderStyle="None" Text="สมัครสอบ" />
                    </legend>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 25%;">
                                <span>
                                    <asp:Label ID="lblModalExamCode" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 25%;">
                                <asp:TextBox ID="txtModalExamCode" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 26%;">
                                <span>
                                    <asp:Label ID="lblModalDateExam" runat="server" CssClass="lbl" Text="วันที่สมัครสอบ"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 24%;">
                                <asp:TextBox ID="txtModalExamCode0" runat="server" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CLDtxtModalExamCode0" runat="server" TargetControlID="txtModalExamCode0" Format = "dd/MM/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 25%;">
                                <asp:Label ID="lblModalCompanyCode" runat="server" CssClass="lbl" Text="รหัสบริษัทประกันภัย"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 25%;">
                                <asp:DropDownList ID="ddlModalCompanyCode" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
    
                            </td>
                            <td style="text-align: right; width: 26%;">
                                <span>
                                    <asp:Label ID="lblModalPersonalExam" runat="server" CssClass="lbl" Text="รหัสผู้สมัครสอบ"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 24%;">
                                <asp:TextBox ID="txtModalPersonalExam" runat="server" CssClass="txt" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 25%; height: 17px;">
                                <asp:Label ID="lblModalYardExam" runat="server" CssClass="lbl" Text="รหัสสนามสอบ"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 25%; height: 17px;">
                                <asp:TextBox ID="txtModalYardExam" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 26%; height: 17px;">
                                <asp:Label ID="lblModalOfficeExamCode" runat="server" CssClass="lbl" 
                                    Text="รหัส สนง. ประกันภัยที่สมัครสอบ"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 24%; height: 17px;">
                                <asp:TextBox ID="txtModalOfficeExamCode" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 25%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 25%;">
                                &nbsp;
                                <asp:Button ID="btnModalSubmit" runat="server" CssClass="btn" Text="ตกลง" />
                            </td>
                            <td style="text-align: left; width: 26%;">
                                <asp:Button ID="btnModalCancel" runat="server" CssClass="btn" Text="ยกเลิก" />
                            </td>
                            <td style="text-align: left; width: 24%;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

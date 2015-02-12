<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="YourApplicant.aspx.cs" Inherits="IAS.Applicant.YourApplicant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:Panel ID="pnlTable" runat="server" Visible="true">
        <div class="box_body_content">
            <div class="box_textbox_bg_table" id="Div1" runat="server">
                <div class="box_t_h">
                    ::ข้อมูลการสมัครสอบ</div>
                <div class="box_texbox_add_box">
                    <asp:GridView ID="gvExamSchedule" runat="server" AllowPaging="false" AllowSorting="True"
                        ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" CssClass="datatable" CellSpacing="-1"
                        GridLines="None" BorderWidth="1" CaptionAlign="Top" CellPadding="3" EnableTheming="True"
                        PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last" PagerSettings-Mode="NumericFirstLast"
                        PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10" PagerSettings-PreviousPageText="&lt;"
                        PagerStyle-CssClass="viewpager" PagerStyle-HorizontalAlign="Center" Width="100%"
                        OnRowDataBound="gvExamSchedule_RowDataBound" OnPageIndexChanging="gvExamSchedule_PageIndexChanging"
                        PageSize="20">
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            NextPageText="&gt;" PreviousPageText="&lt;" />
                        <SelectedRowStyle Font-Bold="True" BackColor="#008A8C" />
                        <PagerStyle HorizontalAlign="Center" />
                        <EmptyDataRowStyle HorizontalAlign="Center" BackColor="#FFFFFF" ForeColor="#000000" />
                        <EmptyDataTemplate>
                            <div style="text-align: center; background-color: White">
                                ไม่พบข้อมูล...</div>
                        </EmptyDataTemplate>
                        <HeaderStyle Font-Bold="True" />
                        <AlternatingRowStyle CssClass="altrow" />
                        <Columns>
                            <asp:TemplateField HeaderText="ลำดับที่">
                                <ItemTemplate>
                                    <asp:Label ID="ExamRUNNO" runat="server" Text='<%# Bind("RUN_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="td-center" Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="ExamNumberNo" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                    <asp:LinkButton ID="lnkExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                        Visible="false" OnClick="lnkExamNumber_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หน่วยงานจัดสอบ" ItemStyle-HorizontalAlign="Center"
                                Visible="true">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaceName" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัส" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemStyle Width="10%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="จังหวัด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblProvice" runat="server" Text='<%# Bind("PROVINCE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สมัคร/ที่นั่ง" ItemStyle-HorizontalAlign="Center"
                                Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamAdmission" runat="server" Text='<%# Bind("SEAT_AMOUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เลขที่ประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                Visible="false">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblLicenseTypeCodeNumber" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ค่าธรรมเนียม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="10%" CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamFee" runat="server" Text='<%# Bind("EXAM_FEE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ที่นั่ง" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalApply" runat="server" Text='<%# Bind("TOTAL_APPLY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ประเภทสมาชิก" Visible="true" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle />
                                <ItemTemplate>
                                    <asp:Label ID="lblAgentType" runat="server" Text='<%# Bind("AGENT_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ดำเนินการ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle CssClass="td-center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExamRegister" runat="server" Text='สมัครสอบ' OnClick="lnkExamNumber_Click"
                                        Visible="false"></asp:LinkButton>
                                    <asp:Label ID="lblExamRegister" runat="server" Text='สมัครสอบ' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btn_addtable" runat="server" CssClass="btn" OnClick="btnInsertExamSchedule_Click"
                        Text="เพิ่มการสอบ" vertical-align="middle" Visible="false" />
                    <asp:Button ID="btn_addtable0" runat="server" CssClass="btn" 
                        OnClick="btnInsertExamSchedule_Click" Text="ยืนยันการสอบ" 
                        vertical-align="middle" Visible="false" />
                    <br />
                </div>
            </div>
        </div>
        <div class="clear">
        </div>
    </asp:Panel>
</asp:Content>

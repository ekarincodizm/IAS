<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master"
    AutoEventWireup="true" CodeBehind="LicenseValidate.aspx.cs" Inherits="IAS.License.LicenseValidate" %>
<%@ Register Src="../UserControl/ucPersonalSkills.ascx" TagName="UserControlPersonalSkills" TagPrefix="ucPerSkills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnNext" />
            <asp:PostBackTrigger ControlID="btnBack" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="pnlChk" runat="server" HorizontalAlign="Left" Visible="false">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkRuleID7" runat="server" Text="ไม่เป็นคนวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ" Checked="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkRuleID8" runat="server" Text="ไม่เคยต้องโทษจำคุกโดยคำพิพากษาถึงที่สุดให้จำคุก ในความผิดเกี่ยวกับทรัพย์ที่กระกระทำโดยทุจริต เว้นแต่พ้นโทษมาแล้วไม่น้อยกว่าห้าปีก่อนวันขอรับใบอนุญาต" Checked="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkRuleID9" runat="server" Text="ไม่เป็นบุคคลล้มละลาย" Checked="false" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%;">
                                        </td>
                                        <td style="width: 25%;">
                                        </td>
                                        <td align="right" style="width: 25%;">
                                            <asp:ImageButton ID="ImageButton1" runat="server" 
                                                ImageUrl="~/Images/button_back.png" OnClick="btnBack_Click" />
                                        </td>
                                        <td align="left" style="width: 25%;">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:ImageButton ID="btnSubmit" runat="server" 
                                                    ImageUrl="~/Images/button_next.png" onclick="btnSubmit_Click"/>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center" Visible="true" >
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Panel ID="pnlRenewPetitionName" runat="server" Visible="false">
                            <fieldset style="width: 400px; margin: 0px auto 10px auto;">
                                <legend>
                                    <asp:Label ID="lblRenewPetitionNameHead" runat="server" CssClass="lbl" Text="ดำเนินการ"></asp:Label>
                                </legend>
                                <table width="100%">
                                     <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRenewPetitionName" runat="server" CssClass="lbl"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                             </fieldset>
                            </asp:Panel>
                        </td>
                    <tr>
                        <td aligh="center" colspan="2" style="text-align:left; width:100%;">
                            <ucPerSkills:UserControlPersonalSkills ID="ucPerSkills" runat="server"></ucPerSkills:UserControlPersonalSkills>
                        </td>
                        <%--<td>
                            <fieldset style="width: 400px; margin: 0px auto 10px auto;">
                                <legend>
                                    <asp:Label ID="lblHeadName1" runat="server" CssClass="lbl" Text="ผลการสอบ"></asp:Label>
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 135px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 135px">
                                            <asp:Label ID="lblResExamName" runat="server" CssClass="lbl" Text="ผล : "></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblResExam" runat="server" CssClass="lbl" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 135px">
                                            <asp:Label ID="lblDescExamName" runat="server" CssClass="lbl" 
                                                Text="คำอธิบาย : " Visible="false"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblDescExam" runat="server" CssClass="lbl" Text="" 
                                                Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width: 400px; margin: 0px auto 10px auto;">
                                <legend>
                                    <asp:Label ID="lblHeadName2" runat="server" CssClass="lbl" Text="ผลการอบรม"></asp:Label>
                                </legend>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 135px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 135px">
                                            <asp:Label ID="lblResTrainName" runat="server" CssClass="lbl" Text="ผล : "></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblResTrain" runat="server" CssClass="lbl" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width: 135px">
                                            <asp:Label ID="lblDesTrainName" runat="server" CssClass="lbl" 
                                                Text="คำอธิบาย : " Visible="false"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblDesTrain" runat="server" CssClass="lbl" Text="" 
                                                Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 135px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>--%>
                        <tr>
                            <td colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 25%;">
                                        </td>
                                        <td style="width: 25%;">
                                        </td>
                                        <td align="right" style="width: 25%;">
                                            <asp:ImageButton ID="btnBack" runat="server" 
                                                ImageUrl="~/Images/button_back.png" OnClick="btnBack_Click" />
                                        </td>
                                        <td align="left" style="width: 25%;">
                                            <asp:Panel ID="pnlNext" runat="server">
                                                <asp:ImageButton ID="btnNext" runat="server" 
                                                    ImageUrl="~/Images/button_next.png" OnClick="btnNext_Click" />
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblResult" runat="server" CssClass="lbl" Font-Bold="false" 
                                    ForeColor="Red" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <ajaxToolkit:TabContainer ID="TabDetail" runat="server" ActiveTabIndex="0" 
                                    AutoPostBack="true" OnActiveTabChanged="TabDetail_ActiveTabChanged" 
                                    Width="100%">
                                    <ajaxToolkit:TabPanel ID="TabHistoryExam" runat="server" Width="100%">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHistoryExam" runat="server" ForeColor="Black" 
                                                Text="ประวัติการสอบ"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvHistoryExam" runat="server" AutoGenerateColumns="False" 
                                                            BorderStyle="None" BorderWidth="1px" CellSpacing="-1" CssClass="datatable" 
                                                            GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Right" />
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                    ไม่พบข้อมูล</div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="รหัสประจำตัวผู้สอบ">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCertifiedIDGv" runat="server" 
                                                                            Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="รหัสสนามสอบ">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่สอบ">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExamDateGv" runat="server" 
                                                                            Text='<%# Bind("TESTING_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่หมดอายุ" Visible="False">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExamExpDateGv" runat="server" 
                                                                            Text='<%# Bind("EXPIRE_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เวลาที่สอบ">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblExamTimeGv" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeExamGv" runat="server" 
                                                                            Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="สนามสอบ">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFieldExamGv" runat="server" 
                                                                            Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="บริษัทประกันภัย">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCompanyGv" runat="server" 
                                                                            Text='<%# Bind("INSUR_COMP_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ผลสอบ">
                                                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblResultExamGv" runat="server" 
                                                                            Text='<%# Bind("EXAM_RESULT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabHistoryTraining" runat="server" Width="100%">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHistoryTraining" runat="server" ForeColor="Black" 
                                                Text="ประวัติการอบรม"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvHistoryTraining" runat="server" AllowPaging="false" 
                                                            AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1" CellSpacing="-1" 
                                                            CssClass="datatable" GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Right" />
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                    ไม่พบข้อมูล</div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="รหัสประเภท" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeIDGv" runat="server" 
                                                                            Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeExamGv" runat="server" 
                                                                            Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="อบรมครั้งที่" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="5%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTrainingTimeGv" runat="server" 
                                                                            Text='<%# Bind("TRAIN_TIMES") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ผลการอบรมเริ่มวันที่" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTrainingStartDateGv" runat="server" 
                                                                            Text='<%# Bind("TRAIN_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ผลอบรมสิ้นสุดวันที่" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTrainingEndDateGv" runat="server" 
                                                                            Text='<%# Bind("TRAIN_DATE_EXP", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="จำนวนชม." ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalHourGv" runat="server" Text='<%# Bind("HOURS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="สถานะ" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStatusGv" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabTraining" runat="server" Width="100%">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTraining" runat="server" ForeColor="Black" 
                                                Text="คุณสมบัติ 7(1)-(3)"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvTraining" runat="server" AllowPaging="false" 
                                                            AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1" CellSpacing="-1" 
                                                            CssClass="datatable" GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                                                            <AlternatingRowStyle CssClass="alter" />
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Right" />
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                    ไม่พบข้อมูล</div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="รหัสประเภท" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="5%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeIDGv" runat="server" 
                                                                            Text='<%# Bind("SPECIAL_TYPE_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ชื่อประเภท" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="5%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeNameGv" runat="server" 
                                                                            Text='<%# Bind("SPECIAL_TYPE_DESC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่สำเร็จคุณสมบัติ" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStartDateGv" runat="server" 
                                                                            Text='<%# Bind("START_DATE", "{0:dd/MM/yyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่คุณสมบัติหมดอายุ" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEndDateGv" runat="server" 
                                                                            Text='<%# Bind("END_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่ยื่นคุณสมบัติ" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSendDateGv" runat="server" 
                                                                            Text='<%# Bind("SEND_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ยื่นคุณสมบัติโดย" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSendByGv" runat="server" Text='<%# Bind("SEND_BY") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ID_CARD_NO" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="false">
                                                                    <ItemStyle Width="10%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabUnitLink" runat="server" Width="100%">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUnitLink" runat="server" ForeColor="Black" 
                                                Text="ประวัติการอบรม Unit Link"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="gvUnitLink" runat="server" AllowPaging="false" 
                                                            AutoGenerateColumns="false" BorderStyle="None" BorderWidth="1" CellSpacing="-1" 
                                                            CssClass="datatable" GridLines="None" ShowHeaderWhenEmpty="True" Width="100%">
                                                            <AlternatingRowStyle CssClass="alter" />
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Right" />
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                    ไม่พบข้อมูล</div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="รหัสประเภท" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="20%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeIDGv" runat="server" 
                                                                            Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต" 
                                                                    ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                    <ItemStyle Width="30%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTypeLicenseGv" runat="server" 
                                                                            Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="อบรมครั้งที่" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="25%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTrainingTimeGv" runat="server" 
                                                                            Text='<%# Bind("TRAIN_TIMES") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="วันที่อบรม" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="true">
                                                                    <ItemStyle Width="25%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTrainingDateGv" runat="server" 
                                                                            Text='<%# Bind("TRAIN_DATE", "{0:dd/MM/yyy}") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ID_CARD_NO" ItemStyle-HorizontalAlign="Center" 
                                                                    Visible="false">
                                                                    <ItemStyle Width="25%" />
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

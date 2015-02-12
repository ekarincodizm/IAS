<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLicenseDetail.ascx.cs" Inherits="IAS.UserControl.ucLicenseDetail" %>
<%@ Register Src="UCModalError.ascx" TagName="UCModalError" TagPrefix="ucLicenseDetailModelError" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="ucLicenseDetailModelSuccess" %>
<%@ Register Src="~/UserControl/UCModalWarning.ascx" TagName="UCModalWarning" TagPrefix="ucLiceneDetailWarning" %>
<%--<%@ Register Src="UCLoadingProgress.ascx" TagName="UCLoadingProgress" TagPrefix="ucLicenseLoading" %>--%>
<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
<%--<asp:UpdatePanel ID="udpLicenseMain" runat="server" UpdateMode="Conditional" >
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="TabDetail" EventName="ActiveTabChanged" />
</Triggers>
</asp:UpdatePanel>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Button ID="btnS" runat="server" Text="Open" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="PopupDetail" runat="server" CancelControlID="btnCloseX" TargetControlID="btnS" PopupControlID="pnlModal"
                   BackgroundCssClass="modalbackground" >
                </ajaxToolkit:ModalPopupExtender>
<script type="text/javascript">
//    function pageLoad(sender, e) {
//        $find('<%=TabDetail.ClientID%>').set_activeTabIndex(0);
//    }
</script>
<asp:Panel ID="pnlModal" runat="server" CssClass="modalpopup" Width="70%" >
 <div >
                            <div >
                                <div >
                                    <table style="width: 100%;min-height:500px;">
                                        <tr>
                                            <td style="width: 100%;">
                                                <%--   CssClass="ajax_tabs"--%>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                <ajaxToolkit:TabContainer ID="TabDetail" runat="server" ActiveTabIndex="0" 
                                                    AutoPostBack="true"  Width="100%" Height="500px" OnActiveTabChanged="TabDetail_ActiveTabChanged" >
                                                    <ajaxToolkit:TabPanel ID="TabProfile" runat="server" Height="500px" style="top">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblProfile" runat="server" ForeColor="Black" Text="ประวัติส่วนบุคคล"></asp:Label></HeaderTemplate>
                                                        <ContentTemplate>
                                                            <asp:UpdatePanel ID="udpFirstTab" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                            <table width="100%"  style="vertical-align:top" >
                                                                <tr>
                                                                    <td style="text-align: right; width: 15%;">
                                                                        <asp:Label ID="lblFirstNameLastName" runat="server" CssClass="lbl" 
                                                                            Text="ชื่อ-นามสกุล"></asp:Label><span>:</span>
                                                                    </td>
                                                                    <td style="text-align: left; width: 35%;">
                                                                        <asp:TextBox ID="txtFirstNameLastName" runat="server" CssClass="txt"
                                                                            Readonly="true"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: right; width: 22%;">
                                                                        <asp:Label ID="lblIdCard" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label><span>:</span>
                                                                    </td>
                                                                    <td style="text-align: left; width: 28%;">
                                                                        <asp:TextBox ID="txtIdCard" runat="server" CssClass="txt" Readonly="true"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right; width: 15%;">
                                                                        <asp:Label ID="lblSex" runat="server" CssClass="lbl" Text="เพศ"></asp:Label><span>:</span>
                                                                    </td>
                                                                    <td style="text-align: left; width: 35%;">
                                                                        <asp:TextBox ID="txtSex" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: right; width: 22%;">
                                                                        <asp:Label ID="lblNationality" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label><span>:</span>
                                                                    </td>
                                                                    <td style="text-align: left; width: 28%;">
                                                                        <asp:TextBox ID="txtNationality" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: right; width: 15%;">
                                                                        <asp:Label ID="lblEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label><span>:</span>
                                                                    </td>
                                                                    <td style="text-align: left; width: 35%;">
                                                                        <asp:TextBox ID="txtEducation" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                    <td style="text-align: right; width: 22%;">
                                                                        <asp:Label ID="lblBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label><span>:</span>
                                                                    </td>
                                                                    <td style="text-align: left; width: 28%;">
                                                                        <asp:TextBox ID="txtBirthDay" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <fieldset>
                                                                <legend class="lbl">ที่อยู่ปัจจุบัน</legend>
                                                                <table style="width: 100%; border-width: 1px;">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 15%;">
                                                                            <asp:Label ID="lblCurrentAddress" runat="server" CssClass="lbl" Text="ที่อยู่"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td rowspan="2" style="text-align: left; width: 25%;">
                                                                            <asp:TextBox ID="txtCurrentAddress" runat="server" CssClass="txt" Height="50px" MaxLength="200"
                                                                                TextMode="MultiLine" Width="250px" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblProvinceCurrentAddress" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:TextBox ID="txtProvinceCurrentAddress" runat="server" CssClass="txt" 
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 10%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblDistrictCurrentAddress" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:TextBox ID="txtDistrictCurrentAddress" runat="server" CssClass="txt" 
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 5%;">
                                                                            <asp:Label ID="lblPostcodeCurrentAddress" runat="server" CssClass="lbl" Text="รหัสไปรษณีย์"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 25%;">
                                                                            <asp:TextBox ID="txtPostcodeCurrentAddress" runat="server" CssClass="txt" MaxLength="5"
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblParishCurrentAddress" runat="server" CssClass="lbl" Text="แขวง/ตำบล"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:TextBox ID="txtParishCurrentAddress" runat="server" CssClass="txt" 
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                            <fieldset>
                                                                <legend class="lbl">ที่อยู่ตามทะเบียนบ้าน</legend>
                                                                <table style="width: 100%; border-width: 1px;">
                                                                    <tr>
                                                                        <td style="text-align: right; width: 15%;">
                                                                            <asp:Label ID="lblRegisterAddress" runat="server" CssClass="lbl" Text="ที่อยู่"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td rowspan="2" style="text-align: left; width: 25%;">
                                                                            <asp:TextBox ID="txtRegisterAddress" runat="server" CssClass="txt" Height="50px"
                                                                                MaxLength="100" TextMode="MultiLine" Width="250px" ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblProvinceRegisterAddress" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:TextBox ID="txtProvinceRegisterAddress" runat="server" CssClass="txt" 
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 10%;">
                                                                            &nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblDistrictRegisterAddress" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:TextBox ID="txtDistrictRegisterAddress" runat="server" CssClass="txt" 
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: right; width: 5%;">
                                                                            <asp:Label ID="lblPostcodeRegisterAddress" runat="server" CssClass="lbl" Text="รหัสไปรษณีย์"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 25%;">
                                                                            <asp:TextBox ID="txtPostcodeRegisterAddress" runat="server" CssClass="txt" MaxLength="5"
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                        <td style="text-align: right; width: 20%;">
                                                                            <asp:Label ID="lblParishRegisterAddress" runat="server" CssClass="lbl" Text="แขวง/ตำบล"></asp:Label><span>:</span>
                                                                        </td>
                                                                        <td style="text-align: left; width: 20%;">
                                                                            <asp:TextBox ID="txtParishRegisterAddress" runat="server" CssClass="txt" 
                                                                                ReadOnly="True"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </fieldset>
                                                            </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <ajaxToolkit:TabPanel ID="TabHistoryExam"  Height="500px" style="top" runat="server" >
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHistoryExam" runat="server" ForeColor="Black" Text="ประวัติการสอบ"></asp:Label></HeaderTemplate>
                                                        <ContentTemplate>
                                                        <div style="overflow:scroll; height:500px; ">
                                                               <table width="100%;" style="min-height:500px;vertical-align:top">
                                                                <tr>
                                                                    <td style="text-align: center;" valign="top">
                                                                        <asp:GridView ID="gvHistoryExam" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                            ShowHeaderWhenEmpty="True" EnableTheming="True" CssClass="datatable" GridLines="None"
                                                                            BorderWidth="1px" Width="100%">
                                                                            <AlternatingRowStyle CssClass="altrow" />
                                                                            <HeaderStyle BorderStyle="None" />
                                                                            <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                                    ไม่พบข้อมูล</div>
                                                                            </EmptyDataTemplate>
                                                            
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="รหัสผู้สอบ">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCertifiedIDGv" runat="server" Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="รหัสสอบ">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="วันที่สอบ">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblExamDateGv" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="เวลาที่สอบ">
                                                                                    <ItemStyle Width="15%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblExamTimeGv" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                                                                                    <ItemStyle Width="30%" HorizontalAlign="Center" CssClass="td-left" VerticalAlign="Top"  />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTypeExamGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="สนามสอบ">
                                                                                    <ItemStyle Width="20%" HorizontalAlign="Center" CssClass="td-left" VerticalAlign="Top"  />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFieldExamGv" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="บริษัทประกันภัย" Visible="false">
                                                                                    <ItemStyle Width="250px" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCompanyGv" runat="server" Text='<%# Bind("INSUR_COMP_NAME") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ผลสอบ">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblResultExamGv" runat="server" Text='<%# Bind("EXAM_RESULT") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="วันหมดอายุ" Visible="false">
                                                                                    <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("EXPIRE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                      </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <ajaxToolkit:TabPanel ID="TabHistoryTraining"  Height="500px" style="top" runat="server" Width="100%">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHistoryTraining" runat="server" ForeColor="Black" Text="ประวัติการอบรม"></asp:Label></HeaderTemplate>
                                                        <ContentTemplate>
                                                        <div style="overflow:scroll; height:500px; ">
                                                            <table width="100%;" style="min-height:500px;vertical-align:top">
                                                                <tr>
                                                                    <td style="text-align: center;" valign="top">
                                                                        <asp:GridView ID="gvHistoryTraining" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                            ShowHeaderWhenEmpty="True" EnableTheming="True" CssClass="datatable" CellSpacing="-1"
                                                                            GridLines="None" BorderWidth="1" Width="100%">
                                                                            <AlternatingRowStyle CssClass="altrow" />
                                                                            <HeaderStyle BorderStyle="None" />
                                                                            <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                                    ไม่พบข้อมูล</div>
                                                                            </EmptyDataTemplate>
                                                                          
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ประเภท">
                                                                                    <ItemStyle Width="30%" CssClass="td-left" VerticalAlign="Top"  />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTypeIDGv" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="อบรมครั้งที่">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTrainTime" runat="server" Text='<%# Bind("TRAIN_TIMES") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PILAR_1">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPliar1Gv" runat="server" Text='<%# Bind("PILAR_1") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PILAR_2">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPliar2Gv" runat="server" Text='<%# Bind("PILAR_2") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PILAR_3">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPliar3Gv" runat="server" Text='<%# Bind("PILAR_3") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="จำนวนชั่วโมงอบรม">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblHoursGv" runat="server" Text='<%# Bind("HOURS") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ประเภทการฝึกอบรม">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTraintypeGv" runat="server" Text='<%# Bind("TRAIN_TYPE") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ผลการอบรม">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblResultGv" runat="server" Text='<%# Bind("RESULT") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                    <%--<ajaxToolkit:TabPanel ID="TabTraining"  Height="500px" style="top" runat="server" >
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblTraining" runat="server" ForeColor="Black" Text="อบรม(1)-(4)"></asp:Label></HeaderTemplate>
                                                        <ContentTemplate>
                                                          <div style="overflow:scroll; height:500px; ">
                                                            <table width=" 100%;" style="min-height:500px;vertical-align:top">
                                                                <tr>
                                                                    <td style="text-align: center;"  valign="top">
                                                                        <asp:GridView ID="GvTraining" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                            ShowHeaderWhenEmpty="true" EnableTheming="True" CssClass="datatable" CellSpacing="-1"
                                                                            GridLines="None" BorderWidth="1" Width="100%">
                                                                            <AlternatingRowStyle CssClass="altrow" />
                                                                            <HeaderStyle BorderStyle="None" />
                                                                         
                                                                            <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                                    ไม่พบข้อมูล</div>
                                                                            </EmptyDataTemplate>
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="รหัสประเภท" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTypeIDGv" runat="server" Text='<%# Bind("SPECIAL_TYPE_CODE") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ชื่อประเภท" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTypeNameGv" runat="server" Text='<%# Bind("SPECIAL_TYPE_DESC") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="เริ่มวันที่" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblStartDateGv" runat="server" Text='<%# Bind("START_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="สิ้นสุดวันที่" ItemStyle-HorizontalAlign="Center"
                                                                                    Visible="true">
                                                                                    <ItemStyle Width="5%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEndDateGv" runat="server" Text='<%# Bind("END_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ส่งวันที่" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSendDateGv" runat="server" Text='<%# Bind("SEND_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ส่งโดย" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                    <ItemStyle Width="10%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSendByGv" runat="server" Text='<%# Bind("SEND_BY") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ID_CARD_NO" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                    <ItemStyle Width="10%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>--%>
                                                  <%--  <ajaxToolkit:TabPanel ID="TabUnitLink"   Height="500px" style="top" runat="server" >
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblUnitLink" runat="server" ForeColor="Black" Text="Unit link"></asp:Label></HeaderTemplate>
                                                        <ContentTemplate>
                                                        <div style="overflow:scroll; height:500px; ">
                                                            <table width=" 100%;" style="min-height:500px;vertical-align:top">
                                                                <tr>
                                                                    <td style="text-align: center;"  valign="top">
                                                                        <asp:GridView ID="gvUnitLink" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                            CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1px" ShowHeaderWhenEmpty="True"
                                                                            EnableTheming="True" Width="100%">
                                                                            <AlternatingRowStyle CssClass="altrow" />
                                                                            <HeaderStyle BorderStyle="None" />
                                                                            <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                                    ไม่พบข้อมูล</div>
                                                                            </EmptyDataTemplate>
                                                                           
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="รหัสประเภท">
                                                                                    <ItemStyle Width="20%" CssClass="td-center" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTypeCodeGv" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                                                                                    <ItemStyle Width="30%" CssClass="td-center" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLicensenameGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="อบรมครั้งที่">
                                                                                    <ItemStyle Width="25%" CssClass="td-center" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTrainingTimeGv" runat="server" Text='<%# Bind("TRAIN_TIMES") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="วันที่อบรม">
                                                                                    <ItemStyle Width="25%" CssClass="td-center" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTrainingDateGv" runat="server" Text='<%# Bind("TRAIN_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ID_CARD_NO" Visible="False">
                                                                                    <ItemStyle Width="25%" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>--%>
                                                    <ajaxToolkit:TabPanel ID="TabObtainLicense"  Height="500px" style="top" runat="server" >
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblObtainLicense" runat="server" ForeColor="Black" Text="ขอรับใบอนุญาต"></asp:Label></HeaderTemplate>
                                                        <ContentTemplate>
                                                        <div style="overflow:scroll; height:500px; ">
                                                            <table width="100%" style="min-height:500px;vertical-align:top">
                                                                <tr>
                                                                    <td style="text-align: center;"  valign="top">
                                                                        <asp:GridView ID="gvObtainLicense" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                            CssClass="datatable" EmptyDataText="ไม่พบข้อมูล" ShowHeaderWhenEmpty="True" BorderStyle="None"
                                                                            EnableTheming="True" GridLines="Vertical" Width="100%">
                                                                            <AlternatingRowStyle CssClass="altrow" />
                                                                            <HeaderStyle BorderStyle="None" />
                                                                            <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                                                            <EmptyDataTemplate>
                                                                                <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                                    ไม่พบข้อมูล</div>
                                                                            </EmptyDataTemplate>
                                                                       
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="เลขที่จ่าย" Visible="false">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                     <asp:Label ID="lblPayNumberGv" runat="server" Text=''></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="เลขใบอนุญาต">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLicenseNumberGv" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                                                                                    <ItemStyle Width="30%" HorizontalAlign="Center" CssClass="td-left" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbllicenseNameGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ครั้งที่ต่ออายุ">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRenewGv" runat="server" Text='<%# Bind("RENEW_TIME") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="วันที่ต่ออายุ">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRenewDateGv" runat="server" Text='<%# Bind("LICENSE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="วันที่หมดอายุ">
                                                                                    <ItemStyle Width="10%" HorizontalAlign="Center" CssClass="td-center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("LICENSE_EXPIRE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                             </div>
                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                </ajaxToolkit:TabContainer>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <br />
                                                <center>
                                                    <asp:Button ID="btnCloseX" runat="server" style="display:none;" CssClass="btn"  Text="ปิด" />
                                                    <asp:Button ID="Close_PopUp" runat="server" CssClass="btn"  Text="ปิด"
                                                        onclick="Close_PopUp_Click" />
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
</asp:Panel>
<div>
   <asp:UpdatePanel ID="udpLicenseDetail" runat="server" UpdateMode="Conditional" >
   <Triggers>
    <asp:PostBackTrigger ControlID="Close_PopUp" />
   </Triggers>
    <ContentTemplate>
        <%--<ucLicenseLoading:UCLoadingProgress ID="ucLicLoading" runat="server" />--%>
        <ucLicenseDetailModelError:UCModalError ID="ucLicDetailModelError" runat="server" />
        <ucLicenseDetailModelSuccess:UCModalSuccess ID="ucLicDetailModelSuccess" runat="server" />
        <ucLiceneDetailWarning:UCModalWarning ID="ucLicDetailModelWarning" runat="server" />
    </ContentTemplate>
   </asp:UpdatePanel>
</div>
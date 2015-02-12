<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPersonalSkills.ascx.cs" Inherits="IAS.UserControl.ucPersonalSkills" %>
<%@ Register Src="UCLoadingProgress.ascx" TagName="UCLoadingProgress" TagPrefix="ucLoading" %>
<asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <asp:Panel ID="pnlMain" runat="server"  Width ="100%">
        <div style="width: 100%;">
                                                        <div class="box_body_content">
                                                            <div class="box_t_h" style="width: 50%;">
                                                                ::ตรวจสอบคุณสมบัติ
                                                            </div>
                                                            <br />
                                                            <div class="box_t_h" style="text-align: right; float: right; padding-right: 30px;">
                                                                *หมายเหตุ
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                    Height="16px" />&nbsp ผ่าน&nbsp
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="../Images/discorrect.png" Width="15px"
                                                                    Height="16px" />&nbsp ไม่ผ่าน
                                                            </div>
                                                            <div class="box_textbox_bg_table">
                                                                <div class="box_texbox_add_box">
                                                                    <asp:GridView ID="gvGeneral" runat="server" EnableTheming="True" AutoGenerateColumns="False"
                                                                        CssClass="datatableRow" CellSpacing="-1" GridLines="None" BorderWidth="1" CaptionAlign="Top"
                                                                        ShowHeaderWhenEmpty="True" PagerStyle-HorizontalAlign="Center" OnRowCreated="Head_RowCreated" >
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="คุณสมบัติทั่วไป" HeaderStyle-Wrap="true">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                        Height="15px" Visible="false" />
                                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px" Visible="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle Wrap="true" />
                                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="gvExamResult" runat="server" Width="100%" CssClass="datatableRow"
                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ผลสอบ">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                        Height="15px" Visible="false" />
                                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px" Visible="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="gvEducation" runat="server" Width="100%" CssClass="datatableRow"
                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="คุณวุฒิการศึกษา">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                        Height="15px" Visible="false" />
                                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px" Visible="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="gvTrainResult" runat="server" Width="100%" CssClass="datatableRow"
                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ผลอบรม">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                        Height="15px" Visible="false" />
                                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px" Visible="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="gvOther" runat="server" Width="100%" CssClass="datatableRow" CellSpacing="-1"
                                                                        GridLines="None" BorderWidth="1" EnableTheming="True" ShowHeaderWhenEmpty="True"
                                                                        AutoGenerateColumns="False" OnRowCreated="Head_RowCreated" >
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="อื่น ๆ">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                                        Height="15px" Visible="false" />
                                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px" Visible="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLicenseDate" runat="server" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="gvExamSpecial" runat="server" Width="100%" CssClass="datatableRow"
                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                        OnRowCreated="Head_RowCreated">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="คุณสมบัติพิเศษลดชั่วโมงการสอบ" HeaderStyle-Wrap="true">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("DOCUMENT_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_ATTACH_FILE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                    </asp:GridView>
                                                                    <asp:GridView ID="gvTrainSpecial" runat="server" Width="100%" CssClass="datatableRow"
                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                                        OnRowCreated="Head_RowCreated">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="คุณสมบัติพิเศษลดชั่วโมงการอบรม" HeaderStyle-Wrap="true">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" VerticalAlign="Top" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="40%" CssClass="td-left" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("DOCUMENT_NAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ"></asp:Label>
                                                                                       <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                                        Width="15px" Height="15px" Visible="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID_ATTACH_FILE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
    </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
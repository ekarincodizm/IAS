<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="License.aspx.cs" Inherits="IAS.RecycleBin.License" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
    function visibleDiv(id) {
        if (document.getElementById(id).style.visibility == "hidden") {
            document.getElementById(id).style.visibility = "visible";
        }
        else {
            document.getElementById(id).style.visibility = "hidden";
        }
    }

//    function visible() {

//        divSubMain.style.visibility = "hidden";
//        divDetailExam.style.visibility = "hidden";
//    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:Label Text="ผู้ขอรับใบอนุญาต" BorderStyle="None" ID="lblTheLicensee" CssClass="Initial"
        runat="server" /><br />
    <br />
    <div>
        <asp:Panel ID="PnlMain" runat="server">
            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 40%;">
                                <asp:Label ID="lblFileName" runat="server" Text="ไฟล์ข้อมูล" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:FileUpload ID="FuFile" runat="server" Width="400px" CssClass="fileUpload" />
                            </td>
                            <td style="text-align: left; width: 60%;">
                                <asp:Button ID="btnLoadFile" runat="server" Text="โหลดข้อมูล" CssClass="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelUpload">
                                    <ProgressTemplate>
                                        <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                        Please wait image uploaded....
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <fieldset>
                        <legend class="lbl">ผลการนำเข้า:</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSubject" runat="server" GridLines="Vertical" Width="100%" AllowPaging="True"
                                        BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False">
                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                        <HeaderStyle BorderStyle="None" />
                                        <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                        <RowStyle CssClass="table_item_row" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="50%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("FileName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("ViewAll") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายการที่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("ItemsCorrect") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemsIncorrectGv" runat="server" Text='<%# Eval("ItemsIncorrect") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnLoadFile" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <br />
            <fieldset>
                <legend class="lbl">ตรวจสอบรายการ:</legend>
                <asp:UpdatePanel ID="UpdatePanelCheckList" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: center;" colspan="4">
                                    <asp:GridView ID="gvCheckList" runat="server" GridLines="Vertical" Width="100%" AllowPaging="True"
                                        BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False">
                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                        <HeaderStyle BorderStyle="None" />
                                        <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                        <RowStyle CssClass="table_item_row" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="20%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Eval("IDNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="เลขที่ใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLicenseNumberGv" runat="server" Text='<%# Eval("LicenseNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="วันที่อนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApproveDateGv" runat="server" Text='<%# Eval("ApproveDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="10%" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="hplview" runat="server">แสดง</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="15%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("Detail") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </asp:Panel>
        <asp:UpdatePanel ID="UpdatePanelResultExam" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button ID="btnSearch" runat="server" Text="นำเข้าระบบ" CssClass="btn" OnClientClick="visibleDiv('divResultExam'); return false;" />
                        </td>
                    </tr>
                </table>
                <div id="divResultExam" style="visibility: hidden">
                    <asp:Panel ID="PnlResultExam" runat="server" Width="100%">
                        <fieldset>
                            <legend class="lbl">ผลการสอบ:</legend>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblCompanyID" runat="server" Text="รหัสบริษัท" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtCompanyID" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblCompanyName" runat="server" Text="ชื่อบริษัท" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblDeliverDate" runat="server" Text="วันที่นำส่ง" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtDeliverDate" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblAmountPeople" runat="server" Text="จำนวนคน" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtAmountPeople" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblAmountMoney" runat="server" Text="จำนวนเงิน" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtAmountMoney" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblPermitNumber" runat="server" Text="เลขที่ใบอนุญาต" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtPermitNumber" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblCurrentLicenseDate" runat="server" Text="วันที่อนุญาตปัจจุบัน"
                                            CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtCurrentLicenseDate" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblExpireLicenseDate" runat="server" Text="วันที่หมดอายุปัจจุบัน"
                                            CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtExpireLicenseDate" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblFeeTransfer" runat="server" Text="ค่าธรรมเนียมในการย้าย" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtFeeTransfer" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblIDNumber" runat="server" Text="บัตรประชาชน" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtIDNumber" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblTimeMove" runat="server" Text="ครั้งที่ย้าย" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtTimeMove" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblTitleName" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtTitleName" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblAddress1" runat="server" Text="ที่อยู่ 1" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblAddress2" runat="server" Text="ที่อยู่ 2" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblAreaCode" runat="server" Text="รหัสพื้นที่" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtAreaCode" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblCurrentAddress1" runat="server" Text="ที่อยู่ปัจจุบัน 1" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtCurrentAddress1" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblCurrentAddress2" runat="server" Text="ที่อยู่ปัจจุบัน 2" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtCurrentAddress2" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblCurrentAreaCode" runat="server" Text="รหัสพื้นที่ปัจจุบัน" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtCurrentAreaCode" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblEmail" runat="server" Text="อีเมล" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 20%;">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;" colspan="4">
                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right;" colspan="2">
                                        <asp:Button runat="server" ID="btnSubmit" Text="ตกลง" CssClass="btn" />
                                    </td>
                                    <td style="text-align: left;" colspan="2">
                                        <asp:Button runat="server" ID="btnCacel" Text="ยกเลิก" CssClass="btn" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSearch" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

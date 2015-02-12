<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLicensePayment.ascx.cs" Inherits="IAS.UserControl.ucLicensePayment" %>
    <%@ Register src="UCLoadingProgress.ascx" tagname="UCLoadingProgress" tagprefix="uc1" %>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvPayment.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvPayment.ClientID %>');
            var TargetChildControl = "chkBxSelect";

            var gridview = document.getElementById("<%= gvPayment.ClientID %>");
            for (i = 1; i < gridview.rows.length; i++) {

                var labels = gridview.rows[i].cells[6].getElementsByTagName("span");
                var Inputs = gridview.rows[i].cells[0].getElementsByTagName("input");

                if (labels[0].innerHTML == 'อนุมัติ') {

                    //alert(labels[0].innerHTML + i.toString());

                    //get the reference of first column
                    cell = gridview.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = CheckBox.checked;

                        }
                    }

//                    //Get all the control of the type INPUT in the base control.
//                    var Inputs = TargetBaseControl.getElementsByTagName("input");
//                    //Checked/Unchecked all the checkBoxes in side the GridView.
//                    for (var n = 0; n < Inputs.length; ++n)
//                        if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
//                            Inputs[n].checked = CheckBox.checked;
//                    //Reset Counter
//                    Counter = CheckBox.checked ? TotalChkBx : 0;
                }
            }


        }

        function ChildClick(CheckBox, HCheckBox) {
            //get target base & child control.
            var HeaderCheckBox = document.getElementById(HCheckBox);

            //Modifiy Counter;            
            if (CheckBox.checked && Counter < TotalChkBx)
                Counter++;
            else if (Counter > 0)
                Counter--;

            //Change state of the header CheckBox.
            if (Counter < TotalChkBx)
                HeaderCheckBox.checked = false;
            else if (Counter == TotalChkBx)
                HeaderCheckBox.checked = true;
        }

        function confirmDeleteAttachFile() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

        //Renew Check All check box Natta@6-9-2556
        //Only Check Header Node
        function CheckAllRecords(headerChk, childId) {

            var IsChecked = headerChk.checked;
            var gv = document.getElementById('<%= gvPayment.ClientID %>');
            var cellindex = headerChk.parentNode.cellIndex;
            for (i = 0; i < gv.rows.length; i++) {
                var curTd = gv.rows[i].cells[cellindex];
                var item = curTd.getElementsByTagName('input');
                for (j = 0; j < item.length; j++) {
                    if (item[j].id != headerChk && item[j].type == "checkbox") 
                    {
                        if (item[j].checked != IsChecked && item[j]) 
                        {
                            item[j].click();
                        }
                    }
                }
            }
        }

        function OpenPopup(para) {

            //var qstr = "../Reporting/RptBillPayment.aspx?Invoice=" + para + "&Click=Print";
            var qstr = "../Reporting/RptBillPayment.aspx?Invoice=" + para + "&Click=PrintS";

            window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');

            //window.open(qstr, 'popupwindow', 'fullscreen=no,toolbar=0, menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        }

        function RadioCheck(rb) {
            var gv = document.getElementById("<%= gvPayment.ClientID %>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "checkbox") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        } 

    </script>
<asp:UpdatePanel ID="udpPayment" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" >
<Triggers>
    <%--<asp:PostBackTrigger ControlID="btnPayment" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="btnPayment" />--%>
</Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlPayment" runat="server" Visible="true" Width="100%">
                <fieldset >
                <legend><asp:Label ID="lblPaymentHead" runat="server" Text="เลือกรายการที่จะออกใบสั่งจ่าย" CssClass="lbl"></asp:Label></legend>
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="false" EmptyDataText="ไม่พบข้อมูล" ShowHeaderWhenEmpty="true" 
                            width="100%" BorderStyle="None" OnRowDataBound="gvPayment_RowDataBound" OnRowCreated="gvPayment_RowCreated" >
                                <AlternatingRowStyle CssClass="table_alter_row" />
                                <HeaderStyle BorderStyle="None" />
                                <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                <RowStyle CssClass="table_item_row" />
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBxSelect" runat="server" AutoPostBack="false"  />
                                           <%-- <asp:CheckBox ID="chkSelect" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" visible="true" onclick="RadioCheck(this);" />--%>
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value = '<%#Eval("UPLOAD_GROUP_NO")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายกลุ่ม" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupRequestNo" runat="server" Text='<%# Bind("GROUP_REQUEST_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายย่อย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblHeadRequestNo" runat="server" Text='<%# Bind("HEAD_REQUEST_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขที่นำส่ง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupNo" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ประเภทคำขอ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="17%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPetitionNo" runat="server" Text='<%# Bind("PETITION_TYPE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblMoney" runat="server" Text='<%# Bind("FEES","{0:n}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สถานะเอกสาร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblApproveDoc" runat="server" Text='<%# Bind("APPROVED_DOC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่สร้างเอกสาร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTranDate" runat="server" Text='<%# Bind("TRAN_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สร้างใบสั่งจ่าย" Visible="false" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="hplPrint" runat="server" OnClick="hplPrint_Click"><img src="../Images/print-icon.gif" title="พิมพ์" /></asp:LinkButton>--%>
                                            <%--<asp:ImageButton ID="btnPayment" runat="server" ImageUrl="~/Images/pdf.png" ToolTip="คลิกเพื่อพิมพ์ใบสั่งจ่าย" onclick="btnPayment_Click"  />--%>
                                            <asp:ImageButton ID="btnPayment" runat="server" ImageUrl="~/Images/pdf.png" ToolTip="คลิกเพื่อพิมพ์ใบสั่งจ่าย" onclick="btnPayment_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
                </fieldset>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
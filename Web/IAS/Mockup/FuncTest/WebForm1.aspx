<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="IAS.Mockup.FuncTest.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var TotalChkBx;
        var Counter;

        window.onload = function () {
            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvCheckboxes.Rows.Count %>');
            //Get total no. of checked CheckBoxes in side the GridView.
            Counter = 0;
        }

        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvCheckboxes.ClientID %>');
            var TargetChildControl = "chkBxSelect";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;
            //Reset Counter
            Counter = CheckBox.checked ? TotalChkBx : 0;
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="gvCheckboxes" runat="server" AutoGenerateColumns="False" OnRowCreated="gvCheckboxes_RowCreated">
            <Columns>
                <asp:BoundField HeaderText="S.N." DataField="sno">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBxSelect" runat="server" AutoPostBack="true"/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkBxHeader" onclick="javascript:HeaderClick(this);" runat="server" />
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBx" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                </asp:TemplateField>
            </Columns>
            <RowStyle BackColor="Moccasin" />
            <AlternatingRowStyle BackColor="NavajoWhite" />
            <HeaderStyle BackColor="DarkOrange" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        <br />
        <br />
        <asp:Button ID="ok" runat="server" Text="GetVal" onclick="ok_Click"  />
        </div>
    </form>
</body>
</html>

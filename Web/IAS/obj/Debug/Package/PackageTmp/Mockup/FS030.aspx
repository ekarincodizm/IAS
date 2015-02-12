<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    ResponseEncoding="utf-8" CodeBehind="FS030.aspx.cs"
    Inherits="IAS.Mockup.FS030" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--<script type="text/javascript">
     $(document).ready(function () {
         $(".ui-input-file").css("z-index", "0");
     });
   </script>--%>

    <div id="box-tabs" class="box">
        <div id="tab1">
            <div class="form">
     
                <asp:UpdatePanel runat="server" ID="upGrid" UpdateMode="Conditional">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                    <ContentTemplate>
                        <fieldset>
                            <asp:Panel ID="pnlFileupload" runat="server" Style="display: block;">
                                <div class="form">
                                    <div class="fields">
                                        <div class="field-first field" style="border-bottom: medium none;">
                                            <div class="input" style="padding-top:10px;">
                                                <div class="input input-file button highlight" style="margin: 0px;width:200%;padding-top:10px">
                                                     <label for="btnUpload" style="font-size:9pt"><strong>เลือกไฟล์ข้อมูลการชำระค่าธรรมเนียม :</strong></label>
                                                    <asp:FileUpload ID="FileUpload1" CssClass="file"   runat="server" />
                                                    <asp:Button ID="btnUpload" runat="server" Text="นำข้อมูลเข้า" Style="margin-left: 40px;" OnClick="btnUpload_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </fieldset>
                        <center>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="../Images/updateprogress.gif" Height="60px"
                                        Width="130px" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </center>
                        <asp:Panel ID="pnlMessageSusscess" runat="server" Visible="false">
                            <div id="Div2" class="ui-tabs-panel ui-widget-content ui-corner-bottom">
                                <div class="messages">
                                    <div id="message-success" class="message message-success">
                                        <div class="image">
                                            <img src="../Images/icons/success.png" alt="Notice" height="32" />
                                        </div>
                                        <div class="text">
                                            <h6>ผลการนำข้อมูลเข้า</h6>
                                            <span>นำข้อมูลเข้าระบบเรียบร้อยแล้ว</span>
                                        </div>
                                        <div class="dismiss">
                                            <a href="#message-notice"></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pGridValid" Visible="false">
                            <div class="table">
                                <fieldset>
                                    <legend>ผลการตรวจสอบ :&nbsp;
                                        <asp:Label Text="ข้อมูลถูกต้อง" runat="server" ID="lblResult" ForeColor="Green" />
                                    </legend>
                                    <asp:GridView runat="server" runat="server" ID="gvSummaryValid" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="FileDate" HeaderText="วันที่" DataFormatString="{0:dd MMM yyyy}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FileName" HeaderText="ชื่อแฟ้ม" />
                                            <asp:BoundField DataField="NumberOfItems" HeaderText="จำนวนรายการทั้งหมด" DataFormatString="{0:#,##0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumberOfValid" HeaderText="จำนวนรายการที่ถูกต้อง" DataFormatString="{0:#,##0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Total" HeaderText="ยอดรวม" DataFormatString="{0:#,##0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </fieldset>
                                <fieldset>
                                    <legend>รายละเอียด</legend>
                                    <asp:GridView ID="gvValidData" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ลำดับ">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="10px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SequenceNo" HeaderText="Sequence No.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" />
                                            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="Ref1" HeaderText="Ref1" />
                                            <asp:BoundField DataField="Ref2" HeaderText="Ref2" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:#,##0}">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BorderStyle="Outset" Font-Bold="False" />
                                        <RowStyle Font-Bold="False" />
                                    </asp:GridView>
                                </fieldset>
                                <div class="fields" style="text-align: right;">
                                    <div class ="buttons highlight">
                                    <asp:Label Text='*กรุณากดปุ่ม "ยืนยันการนำเข้า" เพื่อส่งข้อมูลเข้าสู่ระบบและรอการอนุมัติจากเจ้าหน้าที่   '
                                        ID="lblAlert" runat="server" ForeColor="Green" />
                                    <asp:Button class="ui-button ui-widget ui-state-default ui-corner-all" Text="ยืนยันการนำเข้า" runat="server" ID="btnSubmit" OnClick="btnSubmit_Click"
                                        OnClientClick="javascript:return confirm('โปรดยืนยันการนำข้อมูลเข้าระบบอีกครั้ง')" />
                                    <asp:Button class="ui-button ui-widget ui-state-default ui-corner-all" Text="ยกเลิก" runat="server" ID="btnCancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pGridInValid" Visible="false">
                            <div class="table">
                                <fieldset>
                                    <legend>ผลการตรวจสอบ :&nbsp;
                                        <asp:Label Text="ข้อมูลไม่ถูกต้อง" runat="server" ID="Label2" ForeColor="Red" />
                                    </legend>
                                    <asp:GridView runat="server" ID="gvSummaryInValid" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="FileDate" HeaderText="วันที่" DataFormatString="{0:dd MMM yyyy}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FileName" HeaderText="ชื่อแฟ้ม" />
                                            <asp:BoundField DataField="NumberOfItems" HeaderText="จำนวนรายการทั้งหมด" DataFormatString="{0:#,##0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumberOfValid" HeaderText="จำนวนรายการที่ถูกต้อง" DataFormatString="{0:#,##0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NumberOfInValid" HeaderText="ยอดรวม" DataFormatString="{0:#,##0}">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </fieldset>
                                <fieldset>
                                    <legend>รายละเอียด</legend>
                                    <asp:GridView ID="gvInValidData" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ลำดับ">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle Width="10px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SequenceNo" HeaderText="Sequence No.">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Remark" HeaderText="ผลการตรวจสอบ">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BorderStyle="Outset" Font-Bold="False" />
                                        <RowStyle Font-Bold="False" />
                                    </asp:GridView>
                                </fieldset>
                                <div style="text-align: right;">
                                    <asp:Label Text='กรุณาตรวจสอบข้อมูลให้ถูกต้องและนำเข้าใหม่อีกครั้ง' ID="Label3" runat="server"
                                        ForeColor="Red" />
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
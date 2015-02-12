<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="BankPage.aspx.cs" Inherits="IAS.BankPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function OpenPopupViewer(PetitionType, DateStart, DateEnd, CompCode, Days) {
        var qstr = "Reporting/QuickReportViewer.aspx?PetitionType=" + PetitionType + "&DateStart=" + DateStart + "&DateEnd=" + DateEnd + "&CompCode=" + CompCode + "&Days=" + Days;
        window.open(qstr, 'popupwindow', 'location=0,scrollbars,resizable=1');
    }
</script>
<style type="text/css">
    table.quickview{border:1px solid #eee;}
    table.quickview td{padding:5px;}
    table.quickview thead {font-weight:bold; background:#f8f8f8;}
    table.quickview tbody td{}
    div.subtab{margin-top:5px}
    div.box-admin-message{float:right; margin:0 10px 0 20px;}
    div.box-admin-message h2{color:#FF4500; margin-bottom: 2px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

    <div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
            <asp:Label Text="หน้าหลัก" BorderStyle="None" ID="lblCreatePayment" runat="server" />
                </div>
            </div>
            <div style="clear:left;"></div>
            <div align="center" id="divTitle" visible="false" runat="server" style="padding:10px;margin:10px;font-size:16px;font-family:Tahoma;background-color:#CBE5F3;">
      
            <asp:Label ID="lblText" runat="server" Text="การลงทะเบียนอยู่ในระหว่างการพิจารณาของเจ้าหน้าที่" ForeColor="Red"></asp:Label>
            
            </div>



  <div class="box_content_index_bgcolor">

    <div class="box_content_index_bg_admin" id="DashboardAdmin" runat="server" visible="false">
        <div class="box_content_index_bgtext"><br />
        <div class="box-admin-message">
            <h2>งานด่วน</h2>
            <table class="quickview">
                <thead>
                    <tr>
                        <td align="center" width="110"><strong>รายการ</strong></td>
                        <td align="center"><strong>จำนวนคำร้อง</strong></td>
                    </tr>
                </thead>
                <tbody>
                    <tr><td>ขอต่อใบอนุญาต 1 ปี</td>
                        <td align="center"><asp:Label ID="lblPetitionCode13" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <tr><td>ขอต่อใบอนุญาต 5 ปี</td>
                        <td align="center"><asp:Label ID="lblPetitionCode14" runat="server" Text="0"></asp:Label></td>
                    </tr>
                    <tr><td>ใบแทนใบอนุญาต</td>
                        <td align="center"><asp:Label ID="lblPetitionCode16" runat="server" Text="0"></asp:Label></td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr><td colspan="2" align="center">
                            <asp:Button ID="btnReportQuickReport" runat="server" Text="รายละเอียด" 
                                CssClass="btn" onclick="btnReportQuickReport_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>

        <div style="overflow:hidden">
            <ajaxToolkit:TabContainer ID="tabAdmin" runat="server" Visible="false" Width="100%"
                CssClass="ajax_tabs_info" ActiveTabIndex="0">
                <ajaxToolkit:TabPanel ID="TabPanel31" runat="server">
                    <HeaderTemplate>ขั้นตอนการลงทะเบียน</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer2" runat="server" Width="100%" CssClass="ajax_tabs_info">
                            <ajaxToolkit:TabPanel ID="TabPanel8" runat="server">
                                <HeaderTemplate>อนุมัติการสมัคร</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image1" ImageUrl="~/image/admin1.png" CssClass="imgflow" runat="server" /></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel9" runat="server">
                                <HeaderTemplate>ลงทะเบียนเจ้าหน้าที่กลุ่มสนามสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image2" ImageUrl="~/image/admin2.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel10" runat="server">
                                <HeaderTemplate>ลงทะเบียนเจ้าหน้าที่ คปภ.</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image3" ImageUrl="~/image/admin3.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel32" runat="server">
                    <HeaderTemplate>ขั้นตอนการสมัครสอบ</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer3" runat="server" Width="100%" CssClass="ajax_tabs_info">
                            <ajaxToolkit:TabPanel ID="TabPanel11" runat="server">
                                <HeaderTemplate>สร้างกำหนดการสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image4" ImageUrl="~/image/admin4.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel16" runat="server">
                                <HeaderTemplate>ยกเลิกผู้สมัครสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image9" ImageUrl="~/image/admin9.png" CssClass="imgflow" runat="server" /></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel30" runat="server">
                                <HeaderTemplate>นำเข้าข้อมูลผลการสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image23" ImageUrl="~/image/admin5.png" CssClass="imgflow" runat="server" /></ContentTemplate>
                            </ajaxToolkit:TabPanel> 
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel33" runat="server">
                    <HeaderTemplate>ขั้นตอนการขอรับใบอนุญาต</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer4" runat="server" Width="100%" CssClass="ajax_tabs_info">
                            <ajaxToolkit:TabPanel ID="TabPanel13" runat="server">
                                <HeaderTemplate>ตรวจสอบข้อมูลผู้ขอรับใบอนุญาต</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image6" ImageUrl="~/image/admin6.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel14" runat="server">
                                <HeaderTemplate>สร้างไฟล์ผู้ขอต่อใบอนุญาต</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image7" ImageUrl="~/image/admin7.png" CssClass="imgflow" runat="server" /></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel34" runat="server" Visible="false">
                    <HeaderTemplate>นำเข้าข้อมูล</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer5" runat="server" Width="100%" CssClass="ajax_tabs_info">
                           <ajaxToolkit:TabPanel ID="TabPanel12" runat="server">
                                <HeaderTemplate>นำเข้าข้อมูลผลการสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image5" ImageUrl="~/image/admin5.png" CssClass="imgflow" runat="server" /></ContentTemplate>
                            </ajaxToolkit:TabPanel> 
                            <ajaxToolkit:TabPanel ID="TabPanel15" runat="server">
                                <HeaderTemplate>นำเข้าข้อมูลการชำระเงิน</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image8" ImageUrl="~/image/admin8.png" CssClass="imgflow" runat="server" /></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel35" runat="server">
                    <HeaderTemplate>ขั้นตอนการตั้งค่าระบบ</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer6" runat="server" Width="100%" CssClass="ajax_tabs_info">
                            <ajaxToolkit:TabPanel ID="TabPanel18" runat="server">
                                <HeaderTemplate>ตั้งค่าเอกสาร</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image11" ImageUrl="~/image/admin11.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel17" runat="server">
                                <HeaderTemplate>ตั้งค่าระบบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image10" ImageUrl="~/image/admin10.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
        </div>
        <div style="clear:both;"></div><br />
    </div>
    
    <div class="box_content_index_bg" id="DashboardAll" runat="server" visible="false">
        <div class="box_content_index_bgtext"><br />

        <div class="dashboard">
        
        <ajaxToolkit:TabContainer ID="TabContainerGeneral"  runat="server" Visible="false" Width="100%" CssClass="ajax_tabs_info">
            <ajaxToolkit:TabPanel ID="TabPanel36" runat="server" Visible="true">
                <HeaderTemplate>ขั้นตอนการสมัครสอบ</HeaderTemplate>
                <ContentTemplate>
                    <asp:Image ID="Image1General" ImageUrl="~/image/a-1.png" CssClass="imgflow" runat="server" Visible="false" />
                    <asp:Image ID="Image1Company" ImageUrl="~/image/a-2.png" CssClass="imgflow" runat="server" Visible="false"/>
                    <asp:Image ID="Image1Association" ImageUrl="~/image/a-3.png" CssClass="imgflow" runat="server" Visible="false" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel37" runat="server" Visible="false">
                <HeaderTemplate>ขั้นตอนการขอรับใบอนุญาต</HeaderTemplate>
                <ContentTemplate>
                    <div class="subtab">
                    <ajaxToolkit:TabContainer ID="TabContainer1"  runat="server" Visible="true" Width="100%" 
                        CssClass="ajax_tabs_info">
                        <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" Visible="true">
                            <HeaderTemplate>ขอรับใบอนุญาต</HeaderTemplate>
                            <ContentTemplate>
                                <asp:Image ID="Image2General" ImageUrl="~/image/b-1.png" CssClass="imgflow" runat="server" Visible="false"/>
                                <asp:Image ID="Image2Company" ImageUrl="~/image/b-2.png" CssClass="imgflow" runat="server" Visible="false"/>  
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" Visible="true">
                            <HeaderTemplate>ขอต่ออายุใบอนุญาต</HeaderTemplate>
                            <ContentTemplate>
                                <asp:Image ID="Image3General" ImageUrl="~/image/c-1.png" CssClass="imgflow" runat="server" Visible="false"/>
                                <asp:Image ID="Image3Company" ImageUrl="~/image/c-2.png" CssClass="imgflow" runat="server" Visible="false"/>  
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" Visible="true">
                            <HeaderTemplate>ขอรับใบแทน</HeaderTemplate>
                            <ContentTemplate>
                                <asp:Image ID="Image4General" ImageUrl="~/image/d-1.png" CssClass="imgflow" runat="server" Visible="false"/>
                                <asp:Image ID="Image4Company" ImageUrl="~/image/d-2.png" CssClass="imgflow" runat="server" Visible="false"/>   
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" Visible="true">
                            <HeaderTemplate>ขอรับใบอนุญาต (ย้ายบริษัท)</HeaderTemplate>
                            <ContentTemplate>
                                <asp:Image ID="Image5General" ImageUrl="~/image/e-1.png" CssClass="imgflow" runat="server" Visible="false"/>
                                <asp:Image ID="Image5Company" ImageUrl="~/image/e-2.png" CssClass="imgflow" runat="server" Visible="false"/>  
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" Visible="true">
                            <HeaderTemplate>ขอรับใบอนุญาต (ใบที่ 2)</HeaderTemplate>
                            <ContentTemplate>
                                <asp:Image ID="Image6General" ImageUrl="~/image/f-1.png" CssClass="imgflow" runat="server" Visible="false"/>
                                <asp:Image ID="Image6Company" ImageUrl="~/image/f-2.png" CssClass="imgflow" runat="server" Visible="false"/> 
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="TabPanel7" runat="server" Visible="true">
                            <HeaderTemplate>ขอรับใบอนุญาต (ขาดต่อขอใหม่)</HeaderTemplate>
                            <ContentTemplate>
                                <asp:Image ID="Image7General" ImageUrl="~/image/g-1.png" CssClass="imgflow" runat="server" Visible="false"/>
                                <asp:Image ID="Image7Company" ImageUrl="~/image/g-2.png" CssClass="imgflow" runat="server" Visible="false"/>   
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>   
        

            <ajaxToolkit:TabContainer ID="tabAgent" runat="server" Visible="false" CssClass="ajax_tabs_info" Width="100%">
                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server">
                    <HeaderTemplate>ขั้นตอนการลงทะเบียน</HeaderTemplate>
                    <ContentTemplate>
                        <asp:Image ID="Image12" ImageUrl="~/image/agent1.png" CssClass="imgflow" runat="server"/>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel38" runat="server">
                    <HeaderTemplate>ขั้นตอนการสมัครสอบ</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer7" runat="server" CssClass="ajax_tabs_info" Width="100%">
                            <ajaxToolkit:TabPanel ID="TabPanel20" runat="server">
                                <HeaderTemplate>นำเข้าข้อมูลผลการสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image13" ImageUrl="~/image/agent2.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel23" runat="server">
                                <HeaderTemplate>ยกเลิกผู้สมัครสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image16" ImageUrl="~/image/agent5.png" CssClass="imgflow" runat="server" /></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel19" runat="server">
                    <HeaderTemplate>ขั้นตอนการขอรับใบอนุญาต</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer8" runat="server" CssClass="ajax_tabs_info" Width="100%">
                            <ajaxToolkit:TabPanel ID="TabPanel21" runat="server">
                                <HeaderTemplate>ตรวจสอบข้อมูลผู้ขอรับใบอนุญาต</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image14" ImageUrl="~/image/agent3.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel22" runat="server">
                                <HeaderTemplate>สร้างไฟล์ผู้ขอต่อใบอนุญาต</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image15" ImageUrl="~/image/agent4.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>

            <ajaxToolkit:TabContainer ID="tabFinance" runat="server" Visible="false" Width="100%"
                CssClass="ajax_tabs_info">
                <ajaxToolkit:TabPanel ID="TabPanel40" runat="server">
                    <HeaderTemplate>ขั้นตอนการสมัครสอบ</HeaderTemplate>
                    <ContentTemplate>
                        <asp:Image ID="Image19" ImageUrl="~/image/finance3.png" CssClass="imgflow" runat="server"/>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel39" runat="server">
                    <HeaderTemplate>ขั้นตอนการเงิน</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer9" runat="server" CssClass="ajax_tabs_info" Width="100%">
                            <ajaxToolkit:TabPanel ID="TabPanel24" runat="server">
                                <HeaderTemplate>สร้างใบเสร็จ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image17" ImageUrl="~/image/finance1.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel25" runat="server">
                                <HeaderTemplate>นำเข้าข้อมูลการชำระเงิน</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image18" ImageUrl="~/image/finance2.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>

            <ajaxToolkit:TabContainer ID="tabTestGroup" runat="server" Visible="false" CssClass="ajax_tabs_info" Width="100%">
                <ajaxToolkit:TabPanel ID="TabPanel26" runat="server">
                    <HeaderTemplate>ขั้นตอนการสมัครสอบ</HeaderTemplate>
                    <ContentTemplate>
                        <div class="subtab">
                        <ajaxToolkit:TabContainer ID="TabContainer10" runat="server" CssClass="ajax_tabs_info" Width="100%">
                            <ajaxToolkit:TabPanel ID="TabPanel27" runat="server">
                                <HeaderTemplate>สร้างกำหนดสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image20" ImageUrl="~/image/group1.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel28" runat="server">
                                <HeaderTemplate>นำเข้าข้อมูลผลการสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image21" ImageUrl="~/image/group2.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel ID="TabPanel29" runat="server">
                                <HeaderTemplate>ยกเลิกผู้สมัครสอบ</HeaderTemplate>
                                <ContentTemplate><asp:Image ID="Image22" ImageUrl="~/image/group3.png" CssClass="imgflow" runat="server"/></ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
 
        </div><!-- end:dashboard-->

 
        <div style="clear:both;"></div><br />
    
        </div>
    </div>  
</div>
 
 <script type="text/javascript">

     $(document).ready(function () {

         $(".tab_content").hide();
         $(".tab_content:first").show();

         $("ul.tabs li").click(function () {
             $("ul.tabs li").removeClass("active");
             $(this).addClass("active");
             $(".tab_content").hide();
             var activeTab = $(this).attr("rel");
             $("#" + activeTab).fadeIn();
         });

     });

</script> 
<script type="text/javascript">

    $(document).ready(function () {

        $(".tab_contentsmall").hide();
        $(".tab_contentsmall:first").show();

        $("ul.tabssmall li").click(function () {
            $("ul.tabssmall li").removeClass("active");
            $(this).addClass("active");
            $(".tab_contentsmall").hide();
            var activeTab = $(this).attr("rel");
            $("#" + activeTab).fadeIn();
        });
    });

</script>        


    </div>
    <div style="clear:left;"></div>
</asp:Content>

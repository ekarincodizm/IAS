// ใช้สำหรับให้ jquery กำหนดรูปแบบการ input ข้อมูลของผู้ใช้ด้วย textbox
// เช่น DateTime, Numeric
// โดยให้ใส่ชื่อ class ของ input control ตามรูปแบบที่ต้องการ

//<reference path="/Scripts/jquery/development-bundle/ui/ui.datepicker.js" />
//<reference path="/Scripts/jquery/development-bundle/ui/i18n/ui.datepicker-th.js" />

$(document).ready(function ($) {
    $.datepicker.setDefaults($.datepicker.regional['th']);
//    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
//    function BeginRequestHandler(sender, args) {
    //    }

    //window.history.forward(); // window.history.forward(1);

   Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler)
    
    // Support AJAX Update Panel End Request (Rebind Script)
    function EndRequestHandler(sender, args) {
        $(".datepicker").datepicker({
            dateFormat: 'dd/mm/yy'
                        , yearRange: "-20:+5"
                        , changeYear: true
                        , changeMonth: true
                        , showOn: "button"
                        , buttonImage: "/soria/images/calendar.gif"
                        , buttonImageOnly: true
                        , yearAddToDisplay: 543
                        , onSelect: function () { }
        });
        $(".datepicker").keypress(function (event) { event.preventDefault(); });
        $(".datepicker").attr('enabled', 'false');
        $(".datepicker").attr('ReadOnly', 'True');
        $(".toggle").toggle();
        $(".numeric").numeric();
        $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        $(".positive").numeric({ negative: false }, function () { alert("No negative values"); this.value = ""; this.focus(); });
        $(".positive-integer").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
        $(".money").maskMoney({ thousands: ',', decimal: '.' , allowZero: 1 });
        $(".money-positive").maskMoney({ thousands: ',', decimal: '.' , allowNegative: 0, allowZero: 1 });
        $(".money-positive2precision").maskMoney({ thousands: ',', decimal: '.' , allowNegative: 0, allowZero: 1 });
        $(".btnSubmit").click(function () {
            return confirm("ยืนยัน ?");
        });
    }

    $(".datepicker").datepicker({
        dateFormat: 'dd/mm/yy'
                        , yearRange: "-20:+5"
                        , changeYear: true
                        , changeMonth: true
                        , showOn: "button"
                        , buttonImage: "/soria/images/calendar.gif"
                        , buttonImageOnly: true
                        , yearAddToDisplay: 543
                        , onSelect: function () { }
    });
    $(".datepicker").keypress(function (event) { event.preventDefault(); });
    $(".datepicker").attr('enabled', 'false');
    $(".datepicker").attr('ReadOnly', 'True');
    $(".toggle").toggle();
    $(".numeric").numeric();
    $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
    $(".positive").numeric({ negative: false }, function () { alert("No negative values"); this.value = ""; this.focus(); });
    $(".positive-integer").numeric({ decimal: false, negative: false }, function () { alert("Positive integers only"); this.value = ""; this.focus(); });
    $(".money").maskMoney({ thousands: ',', decimal: '.' ,allowZero:1 });
    $(".money-positive").maskMoney({ thousands: ',', decimal: '.',  allowNegative: 0, allowZero: 1 });
    $(".money-positive2precision").maskMoney({ thousands: ',', decimal: '.' , allowNegative: 0, allowZero: 1 });
 
    $(".btnSubmit").click(function () {
       return  confirm("ยืนยัน ?");
    });
}
);
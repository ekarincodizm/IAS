/* Thai initialisation for the jQuery UI date picker plugin. */
/* Written by pipo (pipo@sixhead.com). */

jQuery(function ($) {
    var YearX = new Date().getFullYear();
    $.datepicker.regional['th'] = {
        closeText: 'ปิด',
        prevText: '&laquo;&nbsp;ย้อน',
        nextText: 'ถัดไป&nbsp;&raquo;',
        currentText: 'วันนี้',
        monthNames: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน',
		'กรกฏาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        monthNamesShort: ['มกราคม', 'กุมภาพันธ์', 'มีนาคม', 'เมษายน', 'พฤษภาคม', 'มิถุนายน',
		'กรกฏาคม', 'สิงหาคม', 'กันยายน', 'ตุลาคม', 'พฤศจิกายน', 'ธันวาคม'],
        dayNames: ['อาทิตย์', 'จันทร์', 'อังคาร', 'พุธ', 'พฤหัสบดี', 'ศุกร์', 'เสาร์'],
        dayNamesShort: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        dayNamesMin: ['อา.', 'จ.', 'อ.', 'พ.', 'พฤ.', 'ศ.', 'ส.'],
        dateFormat: 'dd/mm/yy', firstDay: 0,
        changeYear: true,
        changeMonth: true,
        yearRange: '1913:' + (YearX+10),
        buttonImage: "../images/calendar.gif",
        showOn: "button",
        buttonImageOnly: true,
        isRTL: false,
        yearAddToDisplay: 543,
        onClose: function (dateText, inst) {
            var day = String(dateText).split("/");
            var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            $(this).datepicker('setDate', new Date(year, month, day[0]));
        },
        defaultDate: new Date()
    };

    $.datepicker.setDefaults($.datepicker.regional['th']);
    $('.datepicker').datepicker();
    $('.ui-datepicker-trigger').css('cursor', 'pointer').attr({ 'title': 'ปฏิทิน', 'alt': 'ปฏิทิน' });
});

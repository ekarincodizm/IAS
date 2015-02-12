
        $(document).ready(function () {
            $('.txtidcard').focusout(function () {

                if ($('input.txtidcard').val().length < 13) {
                    if ($('input.txtidcard').val().length > 0)
                    {
                        alert("กรุณากรอกเลขบัตรประชาชนให้ครบ 13 หลัก");
                    }

                } else {
                    $.ajax({
                        url: "../Service/RegistrationWebService.asmx/VerifyIdCard",
                        data: "{ strIdCard : '" + $(this).val() + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data == null) {
                                alert("nothing.");
                            }
                            else {
                                if (data.d == true) {
                                }
                                else 
                                {
                                    alert("กรุณากรอกเลขบัตรประชาชนให้ถูกต้อง");
                                }
                            }
                        }
                    });
                }

            });
        });
        function fncInputNumericValuesOnly() {
            if (!(event.keyCode == 45 ||
                event.keyCode == 46 ||
                event.keyCode == 48 ||
                event.keyCode == 49 ||
                event.keyCode == 50 ||
                event.keyCode == 51 ||
                event.keyCode == 52 ||
                event.keyCode == 53 ||
                event.keyCode == 54 ||
                event.keyCode == 55 ||
                event.keyCode == 56 ||
                event.keyCode == 57 ||
                event.keyCode == 8 ||
                event.keyCode == 190 ||
                event.keyCode == 37 ||
                event.keyCode == 39 ||
                event.keyCode == 16)) {
                event.returnValue = false;
            }
        } 

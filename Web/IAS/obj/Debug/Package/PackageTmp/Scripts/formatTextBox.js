// JScript File

function ChkDbl(ctl) {
    zz = window.event.keyCode;

    if (zz < 48 || zz > 57) {
        if (zz == 46) {
            if (ctl.value.indexOf(".", 0) >= 0)
                window.event.keyCode = 0;
        }
        else window.event.keyCode = 0;
    }
}

function ChkEng(ctl) {
    //ป้อนได้เฉพาะตัวอักษร A-Z , a-z , ตัวเลข 0 - 9
    zz = window.event.keyCode;

    if ((zz >= 47 && zz <= 57) || (zz >= 65 && zz <= 90) || zz == 32 || zz == 45 || zz == 63 || zz == 58 || zz == 40 || zz == 41 || zz == 46 || zz == 44 || zz == 39 || zz == 43) {
        window.event.keyCode = zz;
    }
    else if (zz >= 97 && zz <= 122) {
        window.event.keyCode = zz;
    }
    else {
        window.event.keyCode = 0;
    }

}

//	sub txt2_onkeypress()
//window.event.returnValue = False
//if ((window.event.keycode>64) and (window.event.keycode<91)) or window.event.keyCode = 32 or window.event.keyCode = 46 then ' BIG Eng
//window.event.returnValue=True 
//elseif ((window.event.keycode>96) and (window.event.keycode<123)) or window.event.keyCode = 32 or window.event.keyCode = 46 then 'SMALL
//window.event.returnValue=True 
//end if 
//end sub
//}
function NoKey(ctl) {
    if (window.event.keyCode) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
        return false;
    }
}

function ChkInt(ctl) {
    zz = window.event.keyCode;

    if ((zz < 48 || zz > 57) || zz == 13) {
        window.event.keyCode = 0;
    }
}
function ChkDate(ctl) {
    zz = window.event.keyCode;

    if (zz < 47 || zz > 57) {
        window.event.keyCode = 0;
    }
}

function UpperCase(ctl) {
    zz = window.event.keyCode;

    if ((zz >= 47 && zz <= 57) || (zz >= 65 && zz <= 90) || zz == 32 || zz == 45 || zz == 63 || zz == 58 || zz == 40 || zz == 41 || zz == 46 || zz == 44 || zz == 39 || zz == 43) {
        window.event.keyCode = zz;
    }
    else if (zz >= 97 && zz <= 122) {
        window.event.keyCode = zz - 32;
    }
    else {
        window.event.keyCode = 0;
    }
}

function valDbl(ctlz) {
    ctlz.value = ClearMinus(ctlz.value);
    ctlz.value = AddFix(ctlz.value);
    ctlz.value = AddComma(ctlz.value, ctlz.value.length - 3);
}

function valDbl12(ctlz) {
    ctlz.value = ClearMinus(ctlz.value);
    ctlz.value = AddFix12(ctlz.value);
    ctlz.value = AddComma(ctlz.value, ctlz.value.length - 13);
}


function prepareNum(ctlz) {
    ctlz.value = ClearComma(ctlz.value);
    ctlz.select();
}
function ClearComma(valIn) {
    temp = valIn;
    while (temp.indexOf(",", 0) != -1)
        temp = temp.replace(",", "");

    return temp;

}
function formatDbl(valIn) {
    var temp = valIn;
    if (isNaN(parseFloat(temp))) {
        temp = 0;
    }

    var temp = "" + Math.round(parseFloat(temp) * 100);

    if (temp == 0)
        return '0.00';
    else {
        var i = temp.length;
        while (i < 3) {
            temp = "0" + temp;
            i = i + 1;
        }
        i = i - 2;
        temp = temp.substring(0, i) + "." + temp.substring(i, temp.length);

        return temp;
    }
}
function ClearMinus(valIn) {
    temp = valIn;
    while (temp.indexOf("-", 0) != -1)
        temp = temp.replace("-", "");

    return temp;
}

function AddFix(amount) {
    var i = parseFloat(amount);
    if (isNaN(i)) { i = 0.00; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .005) * 100);
    i = i / 100;
    s = new String(i);
    if (s.indexOf('.') < 0) { s += '.00'; }
    if (s.indexOf('.') == (s.length - 2)) { s += '0'; }
    s = minus + s;
    return s;
}

function AddFix12(amount) {
    var i = parseFloat(amount);
    if (isNaN(i)) { i = 0.000000000000; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    i = parseInt((i + .0000000000005) * 1000000000000);
    i = i / 1000000000000;
    s = new String(i);
    if (s.indexOf('.') < 0) { s += '.000000000000'; }
    for (var i2 = s.length; i2 < 15; i2++) {
        s += '0';
    }
    s = minus + s;
    return s;
}

function AddCommaFix12(valIn, posStart) {
    if (parseFloat(valIn) < 0.00)
        j = 4;
    else
        j = 3;

    var i = posStart;
    //i = 0; No bolck AddComma From YIM            // this line for block addcomma to not working...
    var temp = valIn;
    temp = temp.substring(0, temp.indexof('.'));
    while (i > j) {
        i = i - 3;
        temp = temp.substring(0, i) + "," + temp.substring(i, temp.length);
    }
    return temp;
}

function AddComma(valIn, posStart) {
    if (parseFloat(valIn) < 0.00)
        j = 4;
    else
        j = 3;

    var i = posStart;
    //i = 0; No bolck AddComma From YIM            // this line for block addcomma to not working...
    var temp = valIn;
    while (i > j) {
        i = i - 3;
        temp = temp.substring(0, i) + "," + temp.substring(i, temp.length);
    }
    return temp;
}
// By BALL 

function ChkEnter(ctl) {
    zz = window.event.keyCode;
    if (zz == 13) {
        window.event.keyCode = 0;
        window.event.returnValue = false;

        ctl.focus();
    }
}

function ChkSearchEnter(ctl) {
    zz = window.event.keyCode;
    if (zz == 13) {
        ctl.focus();
        window.event.keyCode = 13;
        window.event.returnValue = true;
    }
}


function DisableEnter(ctl) {
    zz = window.event.keyCode;
    if (zz == 13) {
        ctl.focus();
        window.event.returnValue = false;
    }
}

function DisableBackSpace(ctl) {
    zz = window.event.keyCode;
    if (zz == 8 || (zz > 43 && zz < 47)) {
        ctl.focus();
        window.event.returnValue = false;

        alert("This key is not use ! (" + keycode + ")");
    }
}

function sumDbl(rateId, settle, arrCtl, targetid) {
    var tmp = 0;
    //  var amt = formatDbl12(ClearComma(document.getElementById(rateId).value))*formatDbl12(ClearComma(document.getElementById(settle).value))
    var amt = ClearComma(document.getElementById(rateId).value) * ClearComma(document.getElementById(settle).value)
    for (i = 0; i < arrCtl.length; i++) {
        zz = formatDbl12(ClearComma(document.getElementById(arrCtl[i]).value));
        tmp = parseFloat(tmp) + parseFloat(zz);
    }
    tmp = amt + tmp
    ret = formatDbl(tmp);
    ret = AddComma(ret, ret.length - 3);
    document.getElementById(targetid).value = ret;

}

function formatDbl12(valIn) {
    var temp = valIn;
    if (isNaN(parseFloat(temp))) {
        temp = 0;
    }

    var temp = "" + Math.round(eval(temp) * 1000000000000);
    if (temp == 0)
        return '0.000000000000';
    else {
        var i = temp.length;
        while (i < 13) {
            temp = "0" + temp;
            i = i + 1;
        }
        i = i - 12;
        temp = temp.substring(0, i) + "." + temp.substring(i, temp.length);
        return temp;
    }
}


/* add by ar soft */
function checkKeyNumberFixPoint(e, obj, len) {
    var code = e.keyCode ? e.keyCode : (e.which ? e.which : window.event.keyCode);
    if (code == 190 || code == 110) {
        var str = obj.value.split(".");
        if (str.length > 1) {
            return false;
        }
    }
    else if (code >= 48 && code <= 57 || code >= 96 && code <= 105) {
        var str = obj.value.split(".");
        if (str.length > 1 && str[1].length > len) {
            var starPosi = obj.value.indexOf('.');
            var curPosi = getCaret(obj);
            return (curPosi <= starPosi)
        }
    }

    return (code >= 37 && code <= 40 || code >= 48 && code <= 57 || code >= 96 && code <= 105 || code == 8 || code == 9 || code == 110 || code == 190 || code == 46);
}

function checkKeyNumber(e, obj) {
    var code = e.keyCode ? e.keyCode : (e.which ? e.which : window.event.keyCode);
    if (code == 190) {
        var str = obj.value.split(".");
        if (str.length > 1) {
            return false;
        }
    }
    else if (code >= 48 && code <= 57) {
        var str = obj.value.split(".");
        if (str.length > 1 && str[1].length > 1) {
            var starPosi = obj.value.indexOf('.');
            var curPosi = getCaret(obj);
            return (curPosi <= starPosi)
        }
    }
    return (code >= 37 && code <= 40 || code >= 48 && code <= 57 || code == 8 || code == 110 || code == 190 || code == 8 || code == 46);

}


function getCaret(el) {
    if (el.selectionStart) {
        return el.selectionStart;
    } else if (document.selection) {
        el.focus();

        var r = document.selection.createRange();
        if (r == null) {
            return 0;
        }

        var re = el.createTextRange(),
        rc = re.duplicate();
        re.moveToBookmark(r.getBookmark());
        rc.setEndPoint('EndToStart', re);

        return rc.text.length;
    }
    return 0;
}



function checkKeyNumberKeyPress(e, obj) {
    var code = e.keyCode ? e.keyCode : (e.which ? e.which : window.event.keyCode);
    if (code == 46) {
        var str = obj.value.split(".");
        if (str.length > 1) {
            return false;
        }
    }
    else if (code >= 48 && code <= 57) {
        var str = obj.value.split(".");
        if (str.length > 1 && str[1].length > 1) {
            var starPosi = obj.value.indexOf('.');
            var curPosi = getCaret(obj);
            return (curPosi <= starPosi)
        }
    }
    return ((code >= 37 && code <= 40) || (code >= 48 && code <= 57) || code == 8 || code == 110 || code == 190 || code == 46);

}

function checkKeyNumberKeyPressFixPoint(e, obj, len) {
    //    var code = e.KeyCode ? e.keyCode : (e.which ? e.which : window.event.keyCode);
    var code = e.KeyCode ? e.keyCode : (e.which ? e.which : e.keyCode);
    if (code == 46 || code == 110) {
        var str = obj.value.split(".");
        if (str.length > 1) {
            return false;
        }
    }
    else if (code >= 48 && code <= 57 || code >= 96 && code <= 105) {
        var str = obj.value.split(".");
        if (str.length > 1 && str[1].length > len) {
            var starPosi = obj.value.indexOf('.');
            var curPosi = getCaret(obj);
            return (curPosi <= starPosi)
        }
    }
    return ((code >= 37 && code <= 40) || (code >= 48 && code <= 57) || code >= 96 && code <= 105 || code == 8 || code == 9 || code == 110 || code == 190 || code == 46);
}


/*------------------ */

function spacialCaractor(evt) {
    var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()";
    var e = evt || window.event,
                    charCode = evt.keyCode == 0 ? evt.charCode : null;
    var ch = charCode != null ? String.fromCharCode(charCode) : null;
    var CTRL = e.ctrlKey || e.metaKey;
    return (iChars.indexOf(ch) == -1 || ch == null) && !CTRL;
}
function charEngThai(evt) {
    var iChars = "~`!#$%^&*+=-[]\\\';,/{}|\":<>?@_^.()1234567890๑๒๓๔฿๕๖๗๘๙๐";
    var e = evt || window.event,
                    charCode = evt.keyCode == 0 ? evt.charCode : null;
    var ch = charCode != null ? String.fromCharCode(charCode) : null;
    var CTRL = e.ctrlKey || e.metaKey;
    return (iChars.indexOf(ch) == -1 || ch == null) && !CTRL;
}
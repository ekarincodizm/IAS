
var CommaFormatted =  function (amount) {
    var delimiter = ","; // replace comma if desired
    var a = amount.split('.', 2)
    var d = a[1];
    var i = parseInt(a[0]);
    if (isNaN(i)) { return ''; }
    var minus = '';
    if (i < 0) { minus = '-'; }
    i = Math.abs(i);
    var n = new String(i);
    var a = [];
    while (n.length > 3) {
        var nn = n.substr(n.length - 3);
        a.unshift(nn);
        n = n.substr(0, n.length - 3);
    }
    if (n.length > 0) { a.unshift(n); }
    n = a.join(delimiter);
    if (d.length < 1) { amount = n; }
    else { amount = n + '.' + d; }
    amount = minus + amount;
    return amount;
}
// end of function CommaFormatted()

function CommaDelimiter(amount) {
    var result = amount ;
    var a = amount.split('.',2)
    var amountInt = parseInt(a[0]).tostring(); // Integer for add delimiter

    delimeter = ",";
    loopForAddDelimiter = parseInt(amountInt) / 3; // เพื่อหาจำนวนรอบที่จะ add delimiter
    if ((parseInt(amountInt) % 3) == 0) {
        loopForAddDelimiter -= 1; // การ 3 ลงตัว แสดงว่าเป็นหลักร้อย หรือ หลักแสน ฯลฯ
    }

    var tmpLength = 0 ;
    var indexToAdd = 0;
    var tmpLeftStr = ""; // string ตัวเลขที่อยู่ฝังซ้าย หลังจากที่ตัด string เป็น 2 ชุดแล้ว
    var tmpRightStr = "" ;
    for (var i = 1; i <= loopForAddDelimiter; i++) {
        tmpLength = result.toString.length;
        indexToAdd = 3 * i;
        tmpLeftStr = result.substr(0, (tmpLength - indexToAdd - 1)); // หาตำแหน่งที่จะแทรกคอมมา
        tmpRightStr = result.substr( indexToAdd+1 , tmpLength )  ;

        result = tmpSplitedStr + delimeter + tmpRightStr;
    }
}
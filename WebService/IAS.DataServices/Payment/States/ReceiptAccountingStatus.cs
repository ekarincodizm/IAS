using System;
using System.Collections.Generic;
using System.Linq;

namespace IAS.DataServices.Payment.States
{
    public enum ReceiptAccountingStatus
    {
        /// <summary>
        /// W = wait สถานะเริ่มสร้างใบเสร็จ
        /// </summary>
        W = 0,
        /// <summary>
        /// X = ยกเลิกใบเสร็จ
        /// </summary>
        X = 1,
        /// <summary>
        ///  A = approve อนุมัติ สร้างใบเสร็จ
        /// </summary>
        A = 2,
        /// <summary>
        /// C = complete ใบเสร็จถูกสร้างเรียบร้อยแล้ว
        /// </summary>
        C = 3 
    }
}
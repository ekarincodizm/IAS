using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.States
{
    /// <summary>
    /// W = wait สถานะเริ่มสร้างกลุ่มย่อยใบสั่งจ่าย
    /// X = ยกเลิกไม่จ่ายเงิน
    /// A = approve อนุมัติ สร้างใบเสร็จ
    /// C = complete ใบเสร็จถูกสร้างเรียบร้อยแล้ว
    /// </summary>
    public enum PaymentRecordStatus
    {
        /// <summary>
        /// W = wait สถานะเริ่มสร้างกลุ่มย่อยใบสั่งจ่าย
        /// </summary>
        W = 0,
        /// <summary>
        /// P = (Paid) จ่ายแล้ว
        /// </summary>
        P = 1,
        /// <summary>
        /// S = (SubPaid) จ่ายไม่ครบ
        /// </summary>
        S = 2,
        /// <summary>
        /// X = ยกเลิกไม่จ่ายเงิน
        /// </summary>
        X = 3,
        /// <summary>
        ///  A = approve อนุมัติ สร้างใบเสร็จ
        /// </summary>
        A = 4,
        /// <summary>
        /// C = complete ใบเสร็จถูกสร้างเรียบร้อยแล้ว
        /// </summary>
        C = 5 
    }
}
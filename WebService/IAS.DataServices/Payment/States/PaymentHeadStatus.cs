using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.States
{
    /// <summary>
    /// W = (Wait) เริ่มต้นสร้าง
    /// P = (Paid) ชำระเงินแล้ว
    /// S = (Sub Paid) แบ่งจ่ายไม่เต็มจำนวน

    /// Z = แบ่งจ่ายจนครบจำนวนแล้ว
    /// </summary>
    public enum PaymentHeadStatus
    {
        /// <summary>
        /// W = (Wait) เริ่มต้นสร้าง
        /// </summary>
        W = 0,
        /// <summary>
        /// P = (Paid) ชำระเงินแล้ว 
        /// </summary>
        P = 1,
        /// <summary>
        /// S = (Sub Paid) แบ่งจ่ายไม่เต็มจำนวน
        /// </summary>
        S = 2,
        /// <summary>
        /// Z = แบ่งจ่ายจนครบจำนวนแล้ว
        /// </summary>
    }
}
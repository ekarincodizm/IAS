using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.States
{
    /// <summary>
    /// W = (Wait) เริ่มต้นสร้าง
    /// P = (Paid) จ่ายแล้ว
    /// S = (SubPaid) จ่ายไม่ครบ
    /// M = (Money) ทำการจ่ายเต็มยอด
    /// U = (Upper Money) ทำการแบบเกินยอด
    /// L = (Lower Money) ทำการจ่ายต่ำกว่ายอด
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// W = (Wait) เริ่มต้นสร้าง
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
        /// M = (Money) ทำการจ่ายเต็มยอด
        /// </summary>
        M = 3,
        /// <summary>
        /// U = (Upper Money) ทำการแบบเกินยอด
        /// </summary>
        U = 4,
        /// <summary>
        /// L = (Lower Money) ทำการจ่ายต่ำกว่ายอด
        /// </summary>
        L = 5
    }
}
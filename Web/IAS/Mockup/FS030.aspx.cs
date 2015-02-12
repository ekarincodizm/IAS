using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using TRS.Controller;
//using TRS.Model;
//using TRS.Model.Payment;
//using TRS.Util;
using IAS.DTO;
using IAS.Properties;

namespace IAS.Mockup
{
    public partial class FS030 : System.Web.UI.Page
    {
        //private Model.UserInfo GetUserInfo
        //{
        //    get
        //    {
        //        return (Model.UserInfo)Session["userInfo"];
        //    }
        //}

        private string UserId
        {
            get
            {
                return "50-1-355";
                //return this.GetUserInfo == null ? "" : this.GetUserInfo.TRS_RefID;
            }
        }

        private int UserIdInSystem
        {
            get
            {
                //return this.GetUserInfo == null ? 0 : Convert.ToInt32(this.GetUserInfo.TRS_UserID.ToString());
                return 0;
            }
        }

        private List<BankTransaction> TransList
        {
            get
            {
                return Session["FS030TransList"] == null ? new List<BankTransaction>() : (List<BankTransaction>)Session["FS030TransList"];
            }
            set
            {
                Session["FS030TransList"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //(new UserLogin()).CheckUserSession(this);
        }

        private void InitGrid(string fileName)
        {
            //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

            if (FileUpload1.PostedFile == null) return;
            if (FileUpload1.PostedFile.FileName == string.Empty) return;

            var transList = new List<BankTransaction>();
            int iNumberOfItem, iNumberOfValid, NumberOfInValid ;
            iNumberOfItem = iNumberOfValid = NumberOfInValid = 0;
            double iTotal = 0;

            try
            {
                try
                {
                    Stream rawData = FileUpload1.PostedFile.InputStream;
                    using (StreamReader sr = new StreamReader(rawData, System.Text.Encoding.GetEncoding("TIS-620")))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length > 0)
                            {
                                //D=Detail
                                if (line.Substring(0, 1) == "D")
                                {
                                    var trans = new BankTransaction
                                    {
                                        SequenceNo = line.Substring(1, 6),
                                        PaymentDate = line.Substring(20, 8),
                                        CustomerName = line.Substring(34, 50),
                                        Ref1 = line.Substring(84, 20).Trim(),
                                        Ref2 = line.Substring(104, 13).Trim(),		//เป็นรหัสบัตรประชาชน
                                        //Amount = Convert.ToDouble(line.Substring(163, 11) + "." + line.Substring(174, 2)) //เป็นจำนวนเงินรวมทศนิยมด้วย
                                    };

                                    iNumberOfItem += 1;

                                    //iTotal += trans.Amount;

                                    transList.Add(trans);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "Javascript", "<script type='text/javascript'>alert('" + Resources.errorFS030_001 + "')</script>");
                    return;
                }
                //ตรวจสอบกับข้อมูลการลงทะเบียนก่อน
                iNumberOfValid = BLL.PaymentBiz.ValidateDataOfKTB(transList);

                var summary = new SummaryBankTransaction
                {
                    
                    //FileDate = DateTime.Today,
                    FileName = fileName,
                    NumberOfItems = iNumberOfItem,
                    NumberOfValid = iNumberOfValid,
                    NumberOfInValid = iNumberOfItem - iNumberOfValid,
                    //Total = iTotal
                };

                var summaryList = new List<SummaryBankTransaction>();
                summaryList.Add(summary);

                //ถ้าข้อมูลถูกต้อง
                //if (summary.NumberOfInValid == 0)
                //{
                    gvSummaryValid.DataSource = summaryList;
                    gvSummaryValid.DataBind();

                    gvValidData.DataSource = transList;
                    gvValidData.DataBind();

                    pGridValid.Visible = true;
                    //pGridInValid.Visible = false;
                //}
                //else
                //{
                    gvSummaryInValid.DataSource = summaryList;
                    gvSummaryInValid.DataBind();

                    gvInValidData.DataSource = transList;
                    gvInValidData.DataBind();

                    //pGridValid.Visible = false;
                    pGridInValid.Visible = true;
                //}

                this.TransList = transList;

                //var dataIsValid = transList.Count == iNumberOfValid && summary.NumberOfItems == summary.NumberOfValid;
                //lblResult.Text = dataIsValid ? "ข้อมูลถูกต้อง" : "ข้อมูลไม่ถูกต้อง";
                //lblResult.ForeColor = (dataIsValid ? Color.Green : Color.Red);
                //btnSubmit.Enabled = dataIsValid;
                //lblAlert.Visible = dataIsValid;
                //pGridValid.Visible = true;

                upGrid.Update();
            }
            catch (IOException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
           // //เปลี่ยนสไตล์ของวันที่เป็นแบบอังกฤษเพื่อเก็บลงฐานข้อมูลซึ่งรับค่าที่เป็น ค.ศ.
           // System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
           // //ตรวจสอบข้อมูลก่อนบันทึกอีกครั้ง
           // var list = this.TransList;
           // var iNumberOfValid = BLL.PaymentBiz.ValidateDataOfKTB(list);

           // var hasInvalid = list.Where(l => !l.IsValid).Count() > 0;

           // if (hasInvalid)
           // {
           //     ClientScript.RegisterClientScriptBlock(GetType(), "JavascriptSuccess", "<script>alert('ช้อมูลบางรายการผิดพลาดไม่สามารถบันทึกได้ โปรดโหลดข้อมูลเพื่อตรวจสอบอีกครั้ง')</script>");
           //     this.upGrid.Update();
           //     return;
           // }

           // //หายอด Total
           // decimal total = this.TransList.Sum(s => (decimal)s.Amount);

           // //เตรียม List สำหรับเก็บ Bill
           // var billList = new List<FS091Bill>();

           // ////เตรียม Class สำหรับเก็บรายการรับชำระรวม
           // var payment = new Model.TRS_PAYMENT
           // {
           //     PAY_FN_ITEM_CODE = "AG0-33-0002",
           //     PAY_TOTAL_AMOUNT = total,
           //     PAY_DISCOUNT_AMOUNT = 0,
           //     PAY_NET_AMOUNT = total,
           //     PAY_CREATED_ON = DateTime.Now,
           //     PAY_UPDATED_ON = DateTime.Now,
           //     PAY_CREATED_BY = this.UserIdInSystem,
           //     PAY_UPDATED_BY = this.UserIdInSystem
           // };

           //// var regIdList = new List<int>();
           // int liRegisterId = 0;
           // foreach (var trans in this.TransList)
           // {
           //     var paymentDate = new DateTime(Convert.ToInt32(trans.PaymentDate.Substring(4, 4)),
           //                                    Convert.ToInt32(trans.PaymentDate.Substring(2, 2)),
           //                                    Convert.ToInt32(trans.PaymentDate.Substring(0, 2)));

           //     payment.TRS_PAYMENTS_DETAILs.Add(new Model.TRS_PAYMENTS_DETAIL
           //     {
           //         PYD_RECEIVE_TYPE = (byte)Controller.ReceiveType.KTB,
           //         PYD_RECEIVE_AMOUNT = (decimal)trans.Amount,
           //         PYD_CHANGE_AMOUNT = 0,
           //         PYD_AC_DATE = paymentDate
           //     });

           //     var ctrl = new PaymentController();
           //     var regDetailList = ctrl.GetRegDetailList(trans.RegisterId);

           //     //if (!regIdList.Where(id => id == trans.RegisterId).Any())
           //     //{
           //     //    regIdList.Add(trans.RegisterId);
           //     //}
           //     liRegisterId = trans.RegisterId;
           //     //เตรียมรายการเพื่อออกใบเสร็จ
           //     foreach (var regDetail in regDetailList)
           //     {
           //          var receipt = new Model.TRS_RECEIPT
           //             {
           //                 RCT_FN_DOC_TYPE = "e3",
           //                 RCT_DATE = paymentDate,
           //                 RCT_HEADER_1 = regDetail.BillHeader1,
           //                 RCT_HEADER_2 = regDetail.BillHeader2,
           //                 RCT_HEADER_3 = System.Configuration.ConfigurationManager.AppSettings["receipt_header3"],
           //                 RCT_AMOUNT = Convert.ToDecimal(regDetail.RGD_AMOUNT),
           //                 RCT_STATUS = "COMPLETED",
           //                 RCT_CREATED_ON = DateTime.Now,
           //                 RCT_CRAETED_BY = this.UserIdInSystem,
           //                 RCT_UPDATED_ON = DateTime.Now,
           //                 RCT_UPDATED_BY = this.UserIdInSystem,
           //                 //RCT_PYD_ID = payment.TRS_PAYMENTS_DETAILs.FirstOrDefault().PYD_REC_ID 
           //             };

           //         receipt.TRS_REGISTRATIONS_RECEIPTs.Add(new Model.TRS_REGISTRATIONS_RECEIPT
           //         {
           //             RGP_RGD_ID = regDetail.RGD_REC_ID,
           //             RGP_CREATED_BY = this.UserIdInSystem,
           //             RGP_UPDATED_BY = this.UserIdInSystem,
           //             RGP_CREATED_ON = DateTime.Now,
           //             RGP_UPDATED_ON = DateTime.Now
           //         });

           //         payment.TRS_RECEIPTs.Add(receipt);


           //         //เก็บรายการเพื่อออกใบเสร็จ
           //         billList.Add(new FS091Bill
           //         {
           //             RegDetailId = regDetail.RGD_REC_ID
           //         });
           //     }
           // }

           // var result = PaymentController.InsertPayment(payment, this.UserId, billList, null, liRegisterId);
            
           // if (result)
           // {
           //     //ClientScript.RegisterClientScriptBlock(GetType(), "JavascriptSuccess", "<script>alert('บันทึกข้อมูล เสร็จสมบูรณ์')</script>");
           //     ClearScreen();
           //     pnlMessageSusscess.Visible = true;
           // }
           // else
           // {
           //     ClientScript.RegisterClientScriptBlock(GetType(), "JavascriptError", "<script>alert('มีข้อผิดพลาด โปรดตรวจสอบข้อมูลอีกครั้ง')</script>");
           //     pnlMessageSusscess.Visible = false;
           // }
           // this.upGrid.Update();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void ClearScreen()
        {
            gvSummaryInValid.DataSource = gvSummaryValid.DataSource = null;
            gvInValidData.DataSource = gvValidData.DataSource = null;

            gvSummaryInValid.DataBind(); gvSummaryValid.DataBind();
            gvInValidData.DataBind(); gvValidData.DataBind();

            pGridValid.Visible = pGridInValid.Visible = false;
            pnlMessageSusscess.Visible = false;
            this.TransList = null;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ls_FileName = "";
            if (FileUpload1.PostedFile != null)
            {
                ls_FileName=FileUpload1.PostedFile.FileName ;
                if (ls_FileName.Length == 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "JavascriptError", "<script>alert('"+ Resources.errorFS030_002 +"')</script>");
                }
                else
                {
                    string Extension = Path.GetExtension(ls_FileName).ToLower();
                    if (Extension.Equals(".txt"))
                    {
                        InitGrid(FileUpload1.PostedFile.FileName);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "JavascriptError", "<script>alert('"+ Resources.errorFS030_003 +"')</script>");
                    }
                }
            }
        }
    }
}
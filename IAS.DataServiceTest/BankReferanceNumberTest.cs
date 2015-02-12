using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Payment;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class BankReferanceNumberTest
    {

        [TestMethod]
        public void GetReferanceNumberTest() 
        {
            ReferanceNumber referanceNumber1 = GenReferanceNumber.CreateReferanceNumber();

            Assert.IsNotNull(referanceNumber1);
            Assert.AreEqual(referanceNumber1.RunnigNumber, 82);
        }


        [TestMethod]
        public void The_BankReferanceNumber_Test_True_Formating() 
        {


            String oicCode = "xxxxxx";
            //test bankcode = "KTB";
           

            ReferanceNumber referanceNumber1 = GenReferanceNumber.CreateReferanceNumber();

            Assert.AreEqual(referanceNumber1.FirstNumber.Length, 20); // format '123456 1234 12345678'
            Assert.AreEqual(referanceNumber1.SecondNumber.Length, 8);
            Assert.AreEqual(referanceNumber1.RunnigNumber, 1);
            Assert.AreEqual(referanceNumber1.FirstNumber, genRefNo(oicCode, 1)); 

            ReferanceNumber referanceNumber2 = GenReferanceNumber.CreateReferanceNumber();
            Assert.AreEqual(referanceNumber2.RunnigNumber, 2);
            Assert.AreEqual(referanceNumber2.FirstNumber, genRefNo(oicCode, 2)); 
        }


        private String genRefNo(String oicnumber,Int64 runningNo){
            return  String.Format("{0} {1} {2}",
                    oicnumber,
                    DateTime.Now.ToString("yyMM", new System.Globalization.CultureInfo("th-TH")),
                    runningNo.ToString().PadLeft(8,'0'));
        }
    }
}

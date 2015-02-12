using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IAS.Utils;

namespace IAS.DataServices.Test.PaymentTest
{
    [TestClass]
    public class HashAlgorithmReceiptTest
    {
        [TestMethod]
        public void HashAlgorithm_Can_create_same_data_before_and_afterTest()
        {
            String at = @"D:\OIC\IAS\IAS.DataServices.Test\PaymentTest\FileTest";
            String result1 = "";
            //String filetest = Path.Combine(at, "3598253359853_12122e15613157.pdf");
            String filetest = Path.Combine(at, "NHibernate_3.pdf");
            FileInfo fileInfo = new FileInfo(filetest);
            if (fileInfo.Exists) {
                 result1 = FileObject.GetHashSHA1(fileInfo.FullName);
            }
            String newfile = Path.Combine(fileInfo.DirectoryName, String.Format("newfile{0}.pdf", DateTime.Now.ToString("yyyyMMddhhmmss")));
            File.Copy(fileInfo.FullName, newfile);

            FileInfo fileInfo2 = new FileInfo(newfile);
            String result2 = "";
            if (fileInfo2.Exists) {
                result2 = FileObject.GetHashSHA1(fileInfo2.FullName);
            }

            Assert.AreEqual(result1, result2);
        }

        [TestMethod]
        public void HashAlgorithm_Can_create_same_data_before_and_afterTest2()
        {
            String at = @"D:\OIC\IAS\IAS.DataServices.Test\PaymentTest\FileTest";
            String result1 = "";
            String filetest = Path.Combine(at, "NHibernate_3.pdf");
            FileInfo fileInfo = new FileInfo(filetest);
            if (fileInfo.Exists)
            {
                result1 = FileObject.GetHashSHA1(fileInfo.FullName);
            }
            String newfile = Path.Combine(fileInfo.DirectoryName, String.Format( "newfile{0}.pdf", DateTime.Now.ToString("yyyyMMddhhmmss")) );
            File.Copy(fileInfo.FullName, newfile);

            FileInfo fileInfo2 = new FileInfo(newfile);
            String result2 = "";
            if (fileInfo2.Exists)
            {
                result2 = FileObject.GetHashSHA1(fileInfo2.FullName);
            }

            Assert.AreEqual(result1, result2);
        }
    }
}


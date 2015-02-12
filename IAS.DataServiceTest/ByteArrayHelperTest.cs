using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using IAS.Utils;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class ByteArrayHelperTest
    {
        [TestMethod]
        public void TestEncodeAndDecode()
        {
            String filepath = @"D:\OIC\IAS\Web\IAS\image\a-1.png";
            byte[] img = FileToByteArray(filepath);

            String result = ByteArrayHelper.ConvertByteArrayToString(img);
            byte[] res = ByteArrayHelper.ConvertStringToByte(result);

            Assert.AreEqual(img, res);

        }

        public byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }
    }
}

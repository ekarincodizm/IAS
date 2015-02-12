using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.Utils;
using System.Data;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class MapperTableToObjectTest
    {


        public void TestMapper()
        {
            // Data Column 1=  Column1 , 2= Column2;
            DataTable dt = new DataTable();

            IList<TestClassMapper1> result = dt.MapToCollection<TestClassMapper1>();
        }


        public void ChangeIndex(IList<TestClassMapper1> list, TestClassMapper1 data, Boolean IsUp = true)
        {
            int index = list.IndexOf(data);
            if (IsUp)
            {
                list.Insert(index - 1, data);
                list.RemoveAt(index + 1);
            }
            else
            {

                list.Insert(index + 2, data);
                list.RemoveAt(index);
            }


        }

        [TestMethod]
        public void TestChangeIndexUpCollection()
        {
            IList<TestClassMapper1> list = new List<TestClassMapper1>();
            list.Add(new TestClassMapper1() { Column1 = "0", Column2 = "Row0" });
            list.Add(new TestClassMapper1() { Column1 = "1", Column2 = "Row1" });
            list.Add(new TestClassMapper1() { Column1 = "2", Column2 = "Row2" });
            list.Add(new TestClassMapper1() { Column1 = "3", Column2 = "Row3" });
            list.Add(new TestClassMapper1() { Column1 = "4", Column2 = "Row4" });
            list.Add(new TestClassMapper1() { Column1 = "5", Column2 = "Row5" });
            list.Add(new TestClassMapper1() { Column1 = "6", Column2 = "Row6" });
            list.Add(new TestClassMapper1() { Column1 = "7", Column2 = "Row7" });


            TestClassMapper1 find = list.SingleOrDefault(a => a.Column1 == "3");
            int oldindex = list.IndexOf(find);

            if (find != null)
            {
                ChangeIndex(list, find, true);
            }

            int index = list.IndexOf(find);


            Assert.AreEqual(2, index);
        }

        [TestMethod]
        public void TestChangeIndexDownCollection()
        {
            IList<TestClassMapper1> list = new List<TestClassMapper1>();
            list.Add(new TestClassMapper1() { Column1 = "0", Column2 = "Row0" });
            list.Add(new TestClassMapper1() { Column1 = "1", Column2 = "Row1" });
            list.Add(new TestClassMapper1() { Column1 = "2", Column2 = "Row2" });
            list.Add(new TestClassMapper1() { Column1 = "3", Column2 = "Row3" });
            list.Add(new TestClassMapper1() { Column1 = "4", Column2 = "Row4" });
            list.Add(new TestClassMapper1() { Column1 = "5", Column2 = "Row5" });
            list.Add(new TestClassMapper1() { Column1 = "6", Column2 = "Row6" });
            list.Add(new TestClassMapper1() { Column1 = "7", Column2 = "Row7" });


            TestClassMapper1 find = list.SingleOrDefault(a => a.Column1 == "3");
            int oldindex = list.IndexOf(find);

            if (find != null)
            {
                ChangeIndex(list, find, false);
            }

            int index = list.IndexOf(find);


            Assert.AreEqual(4, index);
        }
    }


    public class TestClassMapper1 {
        public String Column1 { get; set; }
        public String Column2 { get; set; }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class AssemblyInfoTest
    {
        [TestMethod]
        public void AssemblyInfo_showVersionTest()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;

            String vers = version.ToString();
        }
    }
}

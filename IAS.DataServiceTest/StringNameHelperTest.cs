using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IAS.DataServices.Applicant.ApplicantHelper;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class StringNameHelperTest
    {
        [TestMethod]
        public void Validate_Name_IsValid()
        {
            String name = "สมชาย";

            Assert.IsTrue(StringNameHelper.Validate(name));
        }

        [TestMethod]
        public void Validate_Name_IsInValid()
        {
            String name = "สมชาย%%";

            Assert.IsFalse(StringNameHelper.Validate(name));
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class ObjectClass_BindSting_Property_And_ValueTest
    {
        [TestMethod]
        public void TestObjectClassBindStringObjectClass()
        {
            DTO.LicenseAttatchFile entity = new DTO.LicenseAttatchFile();
            entity.ID = "111111111111111";
            entity.REGISTRATION_ID = "2222222222222222222";
            entity.CREATED_DATE = DateTime.Now;
            entity.IsImage = true;

            String result =  BindString(entity);

            Assert.AreNotEqual("", result);
        }

        private static String BindString(DTO.LicenseAttatchFile entity)
        {
            StringBuilder toString = new StringBuilder("");
            toString.AppendLine(entity.GetType().ToString());

            PropertyInfo[] properties = entity.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                String format = "";
                object value = property.GetValue(entity, null);
                if (value == null)
                    format = "{0}={1}";
                else {
                    switch (property.PropertyType.FullName)
                    {
                        case "System.String":
                        case "System.DateTime":  
                        case "System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]" :
                            format = "{0}='{1}'";
                            break;
                        default:
                            format = "{0}={1}";
                            break;
                    }
                }

                toString.AppendLine(String.Format(format,
                                                    property.Name,
                                                    (value == null) ? "NULL" : value.ToString()
                                    ));
            }

            return toString.ToString();
        }
    }
}

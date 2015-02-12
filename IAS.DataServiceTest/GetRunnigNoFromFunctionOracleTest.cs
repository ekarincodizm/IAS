using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IAS.DataServices;
using System.Data;
using Oracle.DataAccess.Client;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class GetRunnigNoFromFunctionOracleTest
    {
        [TestMethod]
        public void GetTestingNoGetTest()
        {
            String testingno = GetTestingNo("03");

            Assert.AreNotEqual(testingno, "");

        }
        private string GetTestingNo(string batchId)
        {

            string _testingNo = string.Empty;
            try
            {

                using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
                {

                    OracleCommand objCmd = new OracleCommand();

                    objCmd.Connection = objConn;

                    objCmd.CommandText = "AG_IAS_GET_TEST_RUN_NO";

                    objCmd.CommandType = CommandType.StoredProcedure;

                    //objCmd.Parameters.Add("I_Date", OracleDbType.Date).Value = DBNull.Value;
                    //objCmd.Parameters.Add("I_License_Type", OracleDbType.Varchar2).Value = "03";
                    //objCmd.Parameters.Add("I_Exam_Status", OracleDbType.Varchar2).Value = "E";

                    OracleParameter retval = new OracleParameter("myretval", OracleDbType.Varchar2);
                    retval.Direction = ParameterDirection.ReturnValue;
                    retval.Size = 4000;
                    retval.Value = "";
                    objCmd.Parameters.Add(retval);

                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    objConn.Close();

                    return retval.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
           
        }
    }
}

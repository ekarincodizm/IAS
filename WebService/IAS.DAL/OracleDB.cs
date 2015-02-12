using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Oracle.DataAccess.Client;
using System.Data;

namespace IAS.DAL
{
    public enum ConnectionFor
    {
        OraDB_Person,
        OraDB_Finance
    }

    public class OracleDB : IDisposable
    {
        private string ConnectionString;
        private ConnectionFor connectionFor;
        private OracleConnection Connection;

        public OracleDB() : this(ConnectionFor.OraDB_Person) { }

        public OracleDB(ConnectionFor value)
        {
            this.connectionFor = value;
            this.ConnectionString = ConfigurationManager
                                        .ConnectionStrings[value.ToString()]
                                        .ToString();
            this.Connection = new OracleConnection(this.ConnectionString);
        }

        //Initial Database Connection
        private void InitConnection()
        {
            if (this.Connection == null)
                this.Connection = new OracleConnection(this.ConnectionString);

            if (this.Connection.State == ConnectionState.Open)
                this.Connection.Close();

            this.Connection.Open();
        }

        //Get DataSet
        public DataSet GetDataSet(string sql)
        {
            DataSet ds = new DataSet();

            InitConnection();
            OracleDataAdapter da = new OracleDataAdapter(sql, this.Connection);
            da.Fill(ds);

            this.Connection.Close();
            //this.Connection.Dispose();

            return ds;
        }

        //Get DataSet
        public DataSet GetDataSet(string sql,ref OracleConnection conn)
        {
            DataSet ds = new DataSet();

            InitConnection();
            OracleDataAdapter da = new OracleDataAdapter(sql, conn);
            da.Fill(ds);

            this.Connection.Close();
            //this.Connection.Dispose();

            return ds;
        }
        //Get DataSet
        public DataSet GetDataSet(string sql, Int32 pageIndex, Int32 pageSize = 0 , String tableName="Table1")  
        {
            DataSet ds = new DataSet();

            pageSize = (pageSize != 0) ? pageSize : Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
            
            InitConnection();
            OracleDataAdapter da = new OracleDataAdapter(sql, this.Connection);
            Int32 currentIndex = (pageIndex*pageSize)-1;
            da.Fill(ds, currentIndex, pageSize, tableName);

            this.Connection.Close();
            //this.Connection.Dispose();

            return ds;
        }
        //Get DataTable
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();

            InitConnection();
            OracleDataAdapter da = new OracleDataAdapter(sql, this.Connection);
            da.Fill(dt);

            this.Connection.Close();
            //this.Connection.Dispose();

            return dt;
        }
      
        //Execute Command
        public string ExecuteCommand(string sql)
        {
            string err = string.Empty;
            try
            {
                InitConnection();
                OracleCommand command = new OracleCommand(sql, this.Connection);
                command.ExecuteNonQuery();

                this.Connection.Close();
                //this.Connection.Dispose();

                //return err;
            }
            catch (Exception ex)
            {
                err = ex.Message;
            }
            return err;
        }


        //For Check Open For Person or Finance
        public ConnectionFor ConnectionFor
        {
            get
            {
                return this.connectionFor;
            }
        }

        //Auto Generate Id
        public static string GetGenAutoId()
        {
            System.Threading.Thread.Sleep(5);
            string autoId = DateTime.Now.Year.ToString("0000").Substring(2, 2) +
                            DateTime.Now.ToString("MMddHHmmssfff");
            return autoId;
        }

        //Distroy Class
        public void Dispose()
        {
            if (this.Connection != null) this.Connection.Dispose();
            //GC.SuppressFinalize(this.Connection);
            GC.SuppressFinalize(this);
        }
    }
}
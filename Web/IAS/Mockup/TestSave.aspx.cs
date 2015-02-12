﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using System.Data;
using System.IO;
using Oracle.DataAccess.Types;

namespace IAS.Mockup
{
    public partial class TestSave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        protected void btnSave_Click(object sender, EventArgs e)
        {

            string constr = "User Id=agdoi;Password=password;Data Source=OIC";
            OracleConnection con = new OracleConnection(constr);
            con.Open();
            Response.Write("Connected to database!");

            // Step 2
            // Note: Modify the Source and Destination location
            // of the image as per your machine settings
            String SourceLoc = "D:/Images/index.jpg";
            String DestinationLoc = "D:/Images/Testindex.jpg";

            // provide read access to the file

            FileStream fs = new FileStream(SourceLoc, FileMode.Open, FileAccess.Read);

            // Create a byte array of file stream length
            byte[] ImageData = new byte[fs.Length];

            //Read block of bytes from stream into the byte array
            fs.Read(ImageData, 0, System.Convert.ToInt32(fs.Length));

            //Close the File Stream
            fs.Close();

            // Step 3
            // Create Anonymous PL/SQL block string
            String block = " BEGIN " +
                           " INSERT INTO test (id, photo) VALUES (100, :1); " +
                           " SELECT photo into :2 from test WHERE id = 100; " +
                           " END; ";

            // Set command to create Anonymous PL/SQL Block
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = block;
            cmd.Connection = con;


            // Since executing an anonymous PL/SQL block, setting the command type
            // as Text instead of StoredProcedure
            cmd.CommandType = CommandType.Text;

            // Step 4
            // Setting Oracle parameters

            // Bind the parameter as OracleDbType.Blob to command for inserting image
            OracleParameter param = cmd.Parameters.Add("blobtodb", OracleDbType.Blob);
            param.Direction = ParameterDirection.Input;


            // Assign Byte Array to Oracle Parameter
            param.Value = ImageData;

            // Bind the parameter as OracleDbType.Blob to command for retrieving the image
            OracleParameter param2 = cmd.Parameters.Add("blobfromdb", OracleDbType.Blob);
            param2.Direction = ParameterDirection.Output;

            // Step 5
            // Execute the Anonymous PL/SQL Block

            // The anonymous PL/SQL block inserts the image to the
            // database and then retrieves the images as an output parameter
            cmd.ExecuteNonQuery();
            Console.WriteLine("Image file inserted to database from " + SourceLoc);

            // Step 6
            // Save the retrieved image to the DestinationLoc in the file system

            // Create a byte array
            byte[] byteData = new byte[0];

            // fetch the value of Oracle parameter into the byte array
            byteData = (byte[])((OracleBlob)(cmd.Parameters[1].Value)).Value;

            // get the length of the byte array
            int ArraySize = new int();
            ArraySize = byteData.GetUpperBound(0);

            // Write the Blob data fetched from database to the filesystem at the
            // destination location
            FileStream fs1 = new FileStream(@DestinationLoc,
                                            FileMode.OpenOrCreate, FileAccess.Write);
            fs1.Write(byteData, 0, ArraySize);
            fs1.Close();

            Console.WriteLine("Image saved to " + DestinationLoc + " successfully !");
            Console.WriteLine("");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Before running this application again, execute 'Listing 1' ");

        }
    }
}
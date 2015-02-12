﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using System.Windows.Forms;
using IAS.Properties;

namespace TestBarCode
{
    public partial class Barcodes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the Requested code to be created.
            //string Code = Request["code"].ToString();
            string Code = "999999560900000013";
            if (Code != null)
            {
                // Multiply the lenght of the code by 40 (just to have enough width)
                int w = Code.Length * 40;

                // Create a bitmap object of the width that we calculated and height of 100
                Bitmap oBitmap = new Bitmap(w, 100);

                // then create a Graphic object for the bitmap we just created.
                Graphics oGraphics = Graphics.FromImage(oBitmap);

                // Now create a Font object for the Barcode Font
                // (in this case the IDAutomationHC39M) of 18 point size
                Font oFont = new Font("Free 3 of 9", 18);

                // Let's create the Point and Brushes for the barcode
                PointF oPoint = new PointF(2f, 2f);
                SolidBrush oBrushWrite = new SolidBrush(Color.Black);
                SolidBrush oBrush = new SolidBrush(Color.White);

                // Now lets create the actual barcode image
                // with a rectangle filled with white color
                oGraphics.FillRectangle(oBrush, 0, 0, w, 100);

                // We have to put prefix and sufix of an asterisk (*),
                // in order to be a valid barcode
                oGraphics.DrawString("*" + Code + "*", oFont, oBrushWrite, oPoint);


                // Then we send the Graphics with the actual barcode
                Response.ContentType = "image/jpeg";
                oBitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
               //  oBitmap.Dispose();
            }
            else
            {
                Response.Write(Resources.infoBarcodes_001);
            }
            

            

        }
        //private Bitmap CreateBarcode(string data)
        //{
        //    Bitmap barcode = new Bitmap(1, 1);
        //    Font threeofNine = new Font("Free 3 of 9", 60, System.Drawing.FontStyle.Regular,
        //        System.Drawing.GraphicsUnit.Point);
        //    Graphics Graphics = Graphics.FromImage(barcode);
        //    SizeF dataSize = Graphics.MeasureString(data, threeofNine);
        //    barcode = new Bitmap(barcode, dataSize.ToSize());
        //    Graphics = Graphics.FromImage(barcode);
        //    Graphics.Clear(Color.White);


        //}
    }
} 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GenCode128;
using System.IO;
//using Spire.Barcode;
using BarcodeLib;


namespace IAS.Utils
{
    public class BarCode
    {
        //public static byte[] GenBarCodeToImage(string code)
        //{
        //    return GenBarCodeToImage(code, 1);
        //}

        //public static byte[] GenBarCodeToImage(string code, int weight)
        //{
        //    try
        //    {
        //        Image img = Code128Rendering.MakeBarcodeImage(code, weight, true);
        //        MemoryStream ms = new MemoryStream();
        //        img.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
        //        return  ms.ToArray();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ArgumentException(ex.Message);
        //    }
        //}
        #region GenerateBarCode
        public static Byte[] GetBarCodeData(string argStrData, string argStrFontName, int argIntWidth, int argIntHeight)
        {
            BarcodeLib.Barcode bc = new BarcodeLib.Barcode();
            bc.BackColor = System.Drawing.Color.White;
            bc.ForeColor = System.Drawing.Color.Black;
            bc.IncludeLabel = false;
            bc.LabelFont = new System.Drawing.Font(argStrFontName, 10);
            System.Drawing.Image img = bc.Encode(BarcodeLib.TYPE.CODE128, argStrData, argIntWidth, argIntHeight);//350,80);
            byte[] bytes = bc.GetImageData(BarcodeLib.SaveTypes.PNG);
            return bytes;
        }

        #endregion GenerateBarCode
        //public static byte[] GenBarCodeToImage(string code) {
        //    //set the configuration of barcode
        //    BarcodeSettings settings = new BarcodeSettings();
        //    string data = "12345";
        //    string type = "Code128";
 
        //    if (code != null && code.Length > 0)
        //    {
        //        data = code;
        //    }

        //    settings.Data2D = data;
        //    settings.Data = code;


        //    settings.Type = (BarCodeType)Enum.Parse(typeof(BarCodeType), type);
        //    settings.HasBorder = false;
        //    short fontSize = 8;
        //    string font = "SimSun";
        //    settings.TextFont = new System.Drawing.Font(font, fontSize, FontStyle.Bold);
   

        //    short barHeight = 15;
        //    settings.BarHeight = barHeight;
        //    settings.ShowText = false;
        //    settings.ShowCheckSumChar = true;
        //    settings.ForeColor = Color.FromName("Black");


        //    //generate the barcode use the settings
        //    BarCodeGenerator generator = new BarCodeGenerator(settings);
        //    Image barcode = generator.GenerateImage();

        //    MemoryStream ms = new MemoryStream();
        //    barcode.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    return ms.ToArray();
        //}
    }

    

}

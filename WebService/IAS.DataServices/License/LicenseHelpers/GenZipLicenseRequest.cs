using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using Ionic.Zip;
using IAS.DataServices;
using IAS.DTO.FileService;
using IAS.DataServices.FileManager;

namespace IAS.DataServices.License.LicenseHelpers
{
    public class GenZipLicenseRequest
    {
        public static String StartCompressByPayment(IAS.DAL.Interfaces.IIASPersonEntities ctx, DateTime findDate, String userName, String zipName)
        {
            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            String _compressPath = ConfigurationManager.AppSettings["COMPRESS_FOLDER"].ToString();
            String imageTypeCode = ConfigurationManager.AppSettings["CODE_ATTACH_PHOTO"].ToString();
           
            NASDrive nasDrive;

            nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive);



            // กำหนด รหัส TypeImage สำหรับค้นหา
            

            Boolean IsNotCreateFolder = true;
            

            DirectoryInfo zipFolder = null;

            //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID
            IEnumerable<AG_IAS_PAYMENT_G_T> paymentGTs = GetPaymentGTs(ctx, findDate);

            foreach (AG_IAS_PAYMENT_G_T paymentGT in paymentGTs)
            {
                //หาข้อมูลที่ Sub Payment Head
                IEnumerable<AG_IAS_SUBPAYMENT_H_T> subPaymentHTs = GetSubPaymentHead(ctx, paymentGT);

                foreach (AG_IAS_SUBPAYMENT_H_T SubPaymentHT in subPaymentHTs)
                {

                    IEnumerable<AG_IAS_SUBPAYMENT_D_T> subPaymentDTs = GetSubPaymentDetails(ctx, SubPaymentHT);
                    

                    foreach (AG_IAS_SUBPAYMENT_D_T subPaymentDT in subPaymentDTs) // milk มาทำ เพิ่ม if else กันกรณีหลุด ไม่มีค่าแล้วเป็นข้อความ error 
                    {
                        if ((subPaymentDT.UPLOAD_GROUP_NO != null)&&(subPaymentDT.SEQ_NO != null))
                        {
                            AG_IAS_LICENSE_D licenD = ctx.AG_IAS_LICENSE_D.SingleOrDefault(w => w.UPLOAD_GROUP_NO == subPaymentDT.UPLOAD_GROUP_NO &&
                                                           w.SEQ_NO == subPaymentDT.SEQ_NO);
                            AG_IAS_LICENSE_H licenH = ctx.AG_IAS_LICENSE_H.Single(w => w.UPLOAD_GROUP_NO == licenD.UPLOAD_GROUP_NO);

                            if (subPaymentDT.LICENSE_TYPE_CODE != null )
                            {
                                if (licenD.OIC_APPROVED_BY != null) {
                                    String whereType = String.Format("_{0}.", imageTypeCode);
                                    AG_IAS_LICENSE_TYPE_R licenType = ctx.AG_IAS_LICENSE_TYPE_R.Single(l => l.LICENSE_TYPE_CODE == subPaymentDT.LICENSE_TYPE_CODE);
                                    AG_IAS_ATTACH_FILE_LICENSE attach = ctx.AG_IAS_ATTACH_FILE_LICENSE.SingleOrDefault(a => a.ID_CARD_NO == licenD.ID_CARD_NO
                                                      && a.GROUP_LICENSE_ID == licenD.UPLOAD_GROUP_NO
                                                      && a.ATTACH_FILE_PATH.Contains(whereType));

                                    if (attach == null)
                                    {
                                        nasDrive.Dispose();
                                        throw new ApplicationException(String.Format("ไม่พบ รูปสำหรับทำใบอนุญาติของ {0} {1} {2}", licenD.NAMES, licenD.LASTNAME, licenD.ID_CARD_NO));
                                    }



                                    if (IsNotCreateFolder)
                                    {
                                        zipFolder = CreateDirectory(Path.Combine(_netDrive, _compressPath),
                                                                    (String.IsNullOrEmpty(zipName)) ? findDate.ToString("yyyy-MM-dd-hhmmss") : zipName,
                                                                    0);
                                        IsNotCreateFolder = false;
                                    }
                                    AddLicenseRequest(ctx, _netDrive, zipFolder, SubPaymentHT, subPaymentDT, licenD, licenH, licenType, attach);
                                }

                            }
                            else
                            {
                                throw new ApplicationException(String.Format("ไม่พบประเภทใบอนุญาตของ {0}{1} {2} {3}", licenD.TITLE_NAME, licenD.NAMES, licenD.LASTNAME, licenD.ID_CARD_NO));
                            }

                        }
                        else
                        {
                            throw new ApplicationException(String.Format("ไม่พบเลขอ้างอิงในการอัพโหลดเอกสารของ หมายเลขบัตรประชาชน {0} ", subPaymentDT.ID_CARD_NO));
                        }
                      
                    }
                }
            }

            
            String zipfileName = "";
            if (!IsNotCreateFolder)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(zipFolder.FullName); // recurses subdirectories
                    zipfileName = zipFolder.FullName + ".zip";
                    zip.Save(zipfileName);
                    zipfileName = zipfileName.Replace(_netDrive, "");
                }
            }
            nasDrive.Dispose(); 
            return zipfileName;
        }
        public static String StartCompressByOicApprove(IAS.DAL.Interfaces.IIASPersonEntities ctx, DateTime findDate, String userName, String zipName)  
        {
            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            String _compressPath = ConfigurationManager.AppSettings["COMPRESS_FOLDER"].ToString();
            String imageTypeCode = ConfigurationManager.AppSettings["CODE_ATTACH_PHOTO"].ToString();

            NASDrive nasDrive;

            nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive);



            // กำหนด รหัส TypeImage สำหรับค้นหา


            Boolean IsNotCreateFolder = true;


            DirectoryInfo zipFolder = null;

            DateTime startDate = new DateTime(findDate.Year, findDate.Month, findDate.Day, 0, 0, 0);
            DateTime endDate = new DateTime(findDate.Year, findDate.Month, findDate.Day, 23, 59, 59);
            //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID
            IEnumerable<AG_IAS_LICENSE_D> licenseDs = ctx.AG_IAS_LICENSE_D.
                               Where(a => a.OIC_APPROVED_DATE >= startDate
                                                          && a.OIC_APPROVED_DATE <= endDate);

            foreach (AG_IAS_LICENSE_D licenD in licenseDs) // milk มาทำ เพิ่ม if else กันกรณีหลุด ไม่มีค่าแล้วเป็นข้อความ error 
            {


                AG_IAS_LICENSE_H licenH = ctx.AG_IAS_LICENSE_H.Single(w => w.UPLOAD_GROUP_NO == licenD.UPLOAD_GROUP_NO);

        

                String whereType = String.Format("_{0}.", imageTypeCode);
                AG_IAS_LICENSE_TYPE_R licenType = ctx.AG_IAS_LICENSE_TYPE_R.Single(l => l.LICENSE_TYPE_CODE == licenH.LICENSE_TYPE_CODE);
                //AG_IAS_ATTACH_FILE_LICENSE attach = ctx.AG_IAS_ATTACH_FILE_LICENSE.SingleOrDefault(a => a.ID_CARD_NO == licenD.ID_CARD_NO
                //                    && a.GROUP_LICENSE_ID == licenD.UPLOAD_GROUP_NO
                //                    && a.ATTACH_FILE_PATH.Contains(whereType));
                AG_IAS_ATTACH_FILE_LICENSE attach = Helpers.GetIASConfigHelper.GetAttachLicensePhoto(ctx, licenD.ID_CARD_NO, licenD.UPLOAD_GROUP_NO);

                if (attach == null)
                {
                    nasDrive.Dispose();
                    throw new ApplicationException(String.Format("ไม่พบ รูปสำหรับทำใบอนุญาติของ {0} {1} {2}", licenD.NAMES, licenD.LASTNAME, licenD.ID_CARD_NO));
                }



                if (IsNotCreateFolder)
                {
                    zipFolder = CreateDirectory(Path.Combine(_netDrive, _compressPath),
                                                (String.IsNullOrEmpty(zipName)) ? findDate.ToString("yyyy-MM-dd-hhmmss") : zipName,
                                                0);
                    IsNotCreateFolder = false;
                }
                AddLicenseRequest(ctx, _netDrive, zipFolder,  licenD, licenH, licenType, attach);
                

   



            }


            String zipfileName = "";
            if (!IsNotCreateFolder)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(zipFolder.FullName); // recurses subdirectories
                    zipfileName = zipFolder.FullName + ".zip";
                    zip.Save(zipfileName);
                    zipfileName = zipfileName.Replace(_netDrive, "");
                }
            }
            nasDrive.Dispose();
            return zipfileName;
        }
        #region Method Get File
        private static IEnumerable<AG_IAS_PAYMENT_G_T> GetPaymentGTs(IAS.DAL.Interfaces.IIASPersonEntities ctx, DateTime findDate)
        {
            IEnumerable<AG_IAS_PAYMENT_G_T> paymentGTs = ctx.AG_IAS_PAYMENT_G_T.Where(f => f.STATUS == "P" && f.PAYMENT_DATE==findDate).ToList();
            foreach (AG_IAS_PAYMENT_G_T payment in paymentGTs)
            {
                IEnumerable<AG_IAS_SUBPAYMENT_H_T> subHs = ctx.AG_IAS_SUBPAYMENT_H_T.Where(w => w.PETITION_TYPE_CODE == "11"
                                                                                            || w.PETITION_TYPE_CODE == "13"
                                                                                            || w.PETITION_TYPE_CODE == "14"
                                                                                            || w.PETITION_TYPE_CODE == "15"
                                                                                            || w.PETITION_TYPE_CODE == "16"
                                                                                            || w.PETITION_TYPE_CODE == "17"
                                                                                            || w.PETITION_TYPE_CODE == "18");
                if (subHs == null || subHs.Count() <= 0) {
                    paymentGTs.ToList().Remove(payment);
                }
                 
            }


            return paymentGTs;
        }

        private static void AddLicenseRequest(IAS.DAL.Interfaces.IIASPersonEntities ctx, String _netDrive,
            DirectoryInfo zipFolder, AG_IAS_LICENSE_D licenD, AG_IAS_LICENSE_H licenH, AG_IAS_LICENSE_TYPE_R licenType, AG_IAS_ATTACH_FILE_LICENSE attach)
        {


            String filePath = String.Format(@"{0}\{1}\{2}\{3}", zipFolder.FullName, ((String.IsNullOrEmpty(licenH.COMP_CODE)) ? "0000" : licenH.COMP_CODE), licenH.PETITION_TYPE_CODE, licenType.LICENSE_TYPE_CODE);
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(_netDrive, filePath));
            if (!dirInfo.Exists)
                dirInfo.Create();

            FileInfo fileInfo = new FileInfo(Path.Combine(dirInfo.FullName, "1.txt"));
            if (!fileInfo.Exists)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileInfo.FullName, true, Encoding.GetEncoding(874)))
                {
                    file.WriteLine("ชื่อรูป,เลขที่ใบอนุญาต,เลขบัตรประชาชน,ชื่อ,สกุล,วันที่ออกใบอนุญาต,วันที่หมดอายุ,บริษัท,ประเภทใบอนุญาต,");
                }
            }


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileInfo.FullName, true, Encoding.GetEncoding(874)))
            {
                file.WriteLine(String.Format("{0},{1},{2},{3},{4},\"{5}\",\"{6}\",{7},{8},", licenD.ID_CARD_NO,
                                                                                    WordSpacing(licenD.LICENSE_NO),
                                                                                    WordSpacing(licenD.ID_CARD_NO),
                                                                                    String.Format("{0} {1}", licenD.TITLE_NAME, licenD.NAMES),
                                                                                    licenD.LASTNAME,
                                                                                    ((DateTime)licenD.LICENSE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                    ((DateTime)licenD.LICENSE_EXPIRE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                    licenH.COMP_NAME,
                                                                                    licenType.LICENSE_TYPE_NAME));
            }



            Int32 start = attach.ATTACH_FILE_PATH.LastIndexOf('.');
            Int32 len = attach.ATTACH_FILE_PATH.Length;
            String extension = attach.ATTACH_FILE_PATH.Substring(attach.ATTACH_FILE_PATH.LastIndexOf('.'), len - start);

            MoveFileResponse response = FileManagerService.RemoteFileCommand(new MoveFileRequest()
            {
                CurrentContainer = "",
                CurrentFileName = attach.ATTACH_FILE_PATH,
                TargetContainer = String.Format(@"{0}\{1}", dirInfo.FullName.Replace(_netDrive, ""), "images"),
                TargetFileName = String.Format("{0}{1}", licenD.ID_CARD_NO, extension)
            }).Action();

            if (response.Code != "0000")
                throw new ApplicationException(response.Message);
        }

        private static void AddLicenseRequest(IAS.DAL.Interfaces.IIASPersonEntities ctx, String _netDrive,
                    DirectoryInfo zipFolder, AG_IAS_SUBPAYMENT_H_T SubPaymentHT, AG_IAS_SUBPAYMENT_D_T subPaymentDT,
                    AG_IAS_LICENSE_D licenD, AG_IAS_LICENSE_H licenH, AG_IAS_LICENSE_TYPE_R licenType, AG_IAS_ATTACH_FILE_LICENSE attach)
        {
           

            String filePath = String.Format(@"{0}\{1}\{2}\{3}", zipFolder.FullName,( (String.IsNullOrEmpty( licenH.COMP_CODE))?"0000": licenH.COMP_CODE ), SubPaymentHT.PETITION_TYPE_CODE, licenType.LICENSE_TYPE_CODE);
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(_netDrive, filePath));
            if (!dirInfo.Exists)
                dirInfo.Create();

            FileInfo fileInfo = new FileInfo(Path.Combine(dirInfo.FullName, "1.txt"));
         
            if (!fileInfo.Exists)
            {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileInfo.FullName, true, Encoding.GetEncoding(874)))
                {
                    file.WriteLine("ชื่อรูป,เลขที่ใบอนุญาต,เลขบัตรประชาชน,ชื่อ,สกุล,วันที่ออกใบอนุญาต,วันที่หมดอายุ,บริษัท,ประเภทใบอนุญาต,");
                }
            }


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileInfo.FullName, true, Encoding.GetEncoding(874)))
            {
                file.WriteLine(String.Format("{0},{1},{2},{3},{4},\"{5}\",\"{6}\",{7},{8},", subPaymentDT.ID_CARD_NO,
                                                                                    WordSpacing(subPaymentDT.LICENSE_NO),
                                                                                    WordSpacing(subPaymentDT.ID_CARD_NO),
                                                                                    String.Format("{0} {1}", licenD.TITLE_NAME, licenD.NAMES),
                                                                                    licenD.LASTNAME,
                                                                                    ((DateTime)licenD.LICENSE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                    ((DateTime)licenD.LICENSE_EXPIRE_DATE).ToString("dd/MM/yyy", CultureInfo.CreateSpecificCulture("th-TH")),
                                                                                    licenH.COMP_NAME,
                                                                                    licenType.LICENSE_TYPE_NAME));
            }



            Int32 start = attach.ATTACH_FILE_PATH.LastIndexOf('.');
            Int32 len = attach.ATTACH_FILE_PATH.Length;
            String extension = attach.ATTACH_FILE_PATH.Substring(attach.ATTACH_FILE_PATH.LastIndexOf('.'), len - start);

            MoveFileResponse response = FileManagerService.RemoteFileCommand(new MoveFileRequest()
            {
                CurrentContainer = "",
                CurrentFileName = attach.ATTACH_FILE_PATH,
                TargetContainer = String.Format(@"{0}\{1}", dirInfo.FullName.Replace(_netDrive, ""), "images"),
                TargetFileName = String.Format("{0}{1}", licenD.ID_CARD_NO, extension)
            }).Action();

            if (response.Code != "0000")
                throw new ApplicationException(response.Message);
        }

        private static IEnumerable<AG_IAS_SUBPAYMENT_D_T> GetSubPaymentDetails(IAS.DAL.Interfaces.IIASPersonEntities ctx, AG_IAS_SUBPAYMENT_H_T SubPaymentHT)
        {
            IEnumerable<AG_IAS_SUBPAYMENT_D_T> subPaymentDTs =
                ctx.AG_IAS_SUBPAYMENT_D_T.Where(w => w.HEAD_REQUEST_NO == SubPaymentHT.HEAD_REQUEST_NO &&
                                    !String.IsNullOrEmpty(w.RECEIPT_NO));
            return subPaymentDTs;
        }

        private static IEnumerable<AG_IAS_SUBPAYMENT_H_T> GetSubPaymentHead(IAS.DAL.Interfaces.IIASPersonEntities ctx, AG_IAS_PAYMENT_G_T paymentGT)
        {
            IEnumerable<AG_IAS_SUBPAYMENT_H_T> subPaymentHTs = ctx.AG_IAS_SUBPAYMENT_H_T
                                 .Where(w => w.GROUP_REQUEST_NO == paymentGT.GROUP_REQUEST_NO
                                        && (w.STATUS != null && w.STATUS == "P") &&
                                        (
                                             w.PETITION_TYPE_CODE == "11"
                                            || w.PETITION_TYPE_CODE == "13"
                                            || w.PETITION_TYPE_CODE == "14"
                                            || w.PETITION_TYPE_CODE == "15"
                                            || w.PETITION_TYPE_CODE == "16"
                                            || w.PETITION_TYPE_CODE == "17"
                                            || w.PETITION_TYPE_CODE == "18"
                                        )
                                    ).ToList();
            return subPaymentHTs;
        }

        public static  DirectoryInfo CreateDirectory(String path, String folderName, Int16 num)
        {
            DirectoryInfo dirInfo;
            if (num == 0)
            {
                dirInfo = new DirectoryInfo(Path.Combine(path, folderName));
            }
            else
            {
                dirInfo = new DirectoryInfo(Path.Combine(path, String.Format("{0} ({1})", folderName, num)));
            }

            if (dirInfo.Exists)
            {
                num++;
                dirInfo = CreateDirectory(path, folderName, num);
            }
            else
            {
                dirInfo.Create();
            }
            return dirInfo;
        }

        public static String WordSpacing(String word)
        {
            StringBuilder result = new StringBuilder("");
            if (word.Length > 0) {
                foreach (Char item in word.ToArray())
                {
                    result.Append(item.ToString() + " ");
                }
            }
            
            return result.ToString();
        }


        #endregion

        public static String StartCompressByOicApprove(IAS.DAL.Interfaces.IIASPersonEntities ctx, List<DTO.GenLicenseDetail> LicenseDetail, String userName, String zipName)
        {
            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            String _compressPath = ConfigurationManager.AppSettings["COMPRESS_FOLDER"].ToString();
            String imageTypeCode = ConfigurationManager.AppSettings["CODE_ATTACH_PHOTO"].ToString();

            NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive);
            // กำหนด รหัส TypeImage สำหรับค้นหา
            Boolean IsNotCreateFolder = true;
            DirectoryInfo zipFolder = null;

            foreach (var item in LicenseDetail)
            {
                AG_IAS_LICENSE_D LD = ctx.AG_IAS_LICENSE_D.FirstOrDefault(s => s.ID_CARD_NO == item.ID_CARD_NO && s.UPLOAD_GROUP_NO == item.UPLOAD_GROUP_NO);
                AG_IAS_LICENSE_H LH = ctx.AG_IAS_LICENSE_H.Single(w => w.UPLOAD_GROUP_NO == LD.UPLOAD_GROUP_NO);
                AG_IAS_LICENSE_TYPE_R licenType = ctx.AG_IAS_LICENSE_TYPE_R.Single(s => s.LICENSE_TYPE_CODE == LH.LICENSE_TYPE_CODE);
                AG_IAS_ATTACH_FILE_LICENSE attach = Helpers.GetIASConfigHelper.GetAttachLicensePhoto(ctx, LD.ID_CARD_NO, LD.UPLOAD_GROUP_NO);

                if (attach == null)
                {
                    nasDrive.Dispose();
                    throw new ApplicationException(String.Format("ไม่พบ รูปสำหรับทำใบอนุญาติของ {0} {1} {2}", LD.NAMES, LD.LASTNAME, LD.ID_CARD_NO));
                }

                if (IsNotCreateFolder)
                {
                    zipFolder = CreateDirectory(Path.Combine(_netDrive, _compressPath),
                                                (String.IsNullOrEmpty(zipName)) ? DateTime.Now.ToString("yyyy-MM-dd-hhmmss") : zipName,
                                                0);
                    IsNotCreateFolder = false;
                }
                AddLicenseRequest(ctx, _netDrive, zipFolder, LD, LH, licenType, attach);
            }
            
            String zipfileName = "";
            if (!IsNotCreateFolder)
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(zipFolder.FullName); // recurses subdirectories
                    zipfileName = zipFolder.FullName + ".zip";
                    zip.Save(zipfileName);
                    zipfileName = zipfileName.Replace(_netDrive, "");
                }

                if (Directory.Exists(zipFolder.FullName))
                {
                    Directory.Delete(zipFolder.FullName, true);
                }
            }
            nasDrive.Dispose();
            return zipfileName;
        }

    }
}
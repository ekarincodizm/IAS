using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zlib;
using SharpCompress.Reader;
using SharpCompress.Common;
using Ionic.Zip;
using SharpCompress.Reader.Rar;

namespace IAS.Utils
{
    [Serializable]
    public class ZipFileEntity
    {
        public string OriginalFilePath { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }

    public class CompressFile
    {
        public bool Zip(string file, string outputFile)
        {
            try
            {
                using (var inFile = File.OpenRead(file))
                {
                    using (var outFile = File.Create(outputFile))
                    {
                        using (var compress = new GZipStream(outFile, CompressionMode.Compress, false))
                        {
                            byte[] buffer = new byte[2048];

                            int read = inFile.Read(buffer, 0, buffer.Length);

                            while (read > 0)
                            {
                                compress.Write(buffer, 0, read);
                                read = inFile.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
                return true;
            }
            catch (IOException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Extract file from zip
        public bool ZipExtract(string source, string destFile)
        {
            try
            {
                var options = new ReadOptions
                {
                    StatusMessageWriter = System.Console.Out,
                    Encoding = System.Text.Encoding.GetEncoding(874)  // "932" for Shift-JIS
                };
                using (ZipFile zf = ZipFile.Read(source,options))
                {
                    foreach (ZipEntry z in zf)
                    {
                        z.Extract(destFile, ExtractExistingFileAction.OverwriteSilently);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Get List of File in Zip
        public List<string> GetFilesInZip(string source)
        {
            var list = new List<string>();

            try
            {
                var options = new ReadOptions
                {
                    StatusMessageWriter = System.Console.Out,
                    Encoding = System.Text.Encoding.GetEncoding(874)  // "932" for Shift-JIS
                };
                using (ZipFile zf = ZipFile.Read(source,options))
                {
                    foreach (ZipEntry z in zf)
                    {
                        if (z.Attributes == FileAttributes.Archive)
                        {
                            list.Add(z.FileName);
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Extract File in RAR
        public bool RarExtract(string source, string destPath)
        {
            try
            {
                using (Stream stream = File.OpenRead(source))
                {
                    var reader = RarReader.Open(stream);
                    while (reader.MoveToNextEntry())
                    {
                        if (reader.ArchiveType == ArchiveType.Rar)
                        {
                            reader.WriteEntryToDirectory(destPath, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Get List of File in RAR
        public List<string> GetFilesInRar(string source)
        {
            var list = new List<string>();
            try
            {
                using (Stream stream = File.OpenRead(source))
                {
                    var reader = RarReader.Open(stream);
                    while (reader.MoveToNextEntry())
                    {
                        if (reader.ArchiveType == ArchiveType.Rar)
                        {
                            list.Add(reader.Entry.FilePath);
                        }
                    }
                }

                

                return list;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}

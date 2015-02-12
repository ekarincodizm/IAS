using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Compression;
using Ionic.Zip;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class CompressFolderTest
    {
        [TestMethod]
        public void TestCompressFolder() {
            DirectoryInfo dir = new DirectoryInfo(@"D:\OIC\Branch\IAS-branch-finance\IAS.DataServiceTest\SimpleFile\TestZip");
            FileInfo targetFile = new FileInfo(@"D:\OIC\Branch\IAS-branch-finance\IAS.DataServiceTest\SimpleFile\TestZip.gz");
            CompressFolder(dir, targetFile);
            //foreach (DirectoryInfo subdir in dir.GetDirectories)
            //{
                
            //}
        }

        public void CompressFolder(DirectoryInfo dir, FileInfo targetFile) 
        {
            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                if (subdir.GetDirectories().Count() > 0)
                {
                    CompressFolder(subdir, targetFile);
                }
                else {
                    foreach (FileInfo fi in subdir.GetFiles()) {
                        Compress(fi, targetFile);
                    }
                }
            }
        }

        public static void Compress(FileInfo fi, FileInfo tarFile)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and 
                // already compressed files.
                if ((File.GetAttributes(fi.FullName)
                  & FileAttributes.Hidden)
                  != FileAttributes.Hidden & fi.Extension != ".gz")

                {
                    // Create the compressed file.
                    if (tarFile.Exists)
                    {
                        using (FileStream outFile = tarFile.OpenWrite())
                        {
                            using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                            {
                                // Copy the source file into 
                                // the compression stream.
                                inFile.CopyTo(Compress);

                                //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                                //    fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                            }
                        }
                    }
                    else {
                        using (FileStream outFile = File.Create(tarFile.FullName))
                        {
                            using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                            {
                                // Copy the source file into 
                                // the compression stream.
                                inFile.CopyTo(Compress);

                                //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                                //    fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                            }
                        }
                    }
                    
                }
            }
        }

    }
}

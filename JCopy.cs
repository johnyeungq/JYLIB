using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    internal class JCopy
    {

        internal string[] CopyCount(string org, string des)
        {
            int fileCount = 0;
            int folderCount = 0;

            try
            {
                // Ensure destination directory exists
                if (!Directory.Exists(des))
                {
                    Directory.CreateDirectory(des);
                }

                // Copy files in the source directory
                foreach (string file in Directory.GetFiles(org))
                {
                    string fileName = Path.GetFileName(file);
                    string destFile = Path.Combine(des, fileName);
                    File.Copy(file, destFile, true); // true allows overwriting
                    fileCount++;
                }

                // Copy directories recursively
                foreach (string dir in Directory.GetDirectories(org))
                {
                    string dirName = Path.GetFileName(dir);
                    string destDir = Path.Combine(des, dirName);
                    Directory.CreateDirectory(destDir);
                    folderCount++;

                    // Recursively copy subdirectory contents
                    string[] subCounts = CopyCount(dir, destDir);
                    fileCount += int.Parse(subCounts[0]);
                    folderCount += int.Parse(subCounts[1]);
                }

                return new string[] { fileCount.ToString(), folderCount.ToString() };
            }
            catch (Exception)
            {
                // Return 0 counts if an error occurs (e.g., path not found)
                return new string[] { "0", "0" };
            }
        }

    }
}

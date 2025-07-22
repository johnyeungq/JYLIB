using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    internal class Import
    {

        internal void ImportFileNameToLB(ListBox lb, string folderPath)
        {
            lb.Items.Clear(); // Clear existing items in the ListBox

            // Check if the file exists
            if (folderPath == null) {
                return;
            }
            foreach(string file in System.IO.Directory.GetFiles(folderPath))
            {
                // Get the file name without the path
                string fileName = System.IO.Path.GetFileName(file);

                // Add the file name to the ListBox
                lb.Items.Add(fileName);
            }       


        }








    }
}

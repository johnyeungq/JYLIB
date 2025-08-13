using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    internal class Logs
    {


        internal void Log(string message)
        {

            string logFilePath = @"C:\Reports\Log.txt";

            if (!System.IO.Directory.Exists(@"C:\Reports"))
            {
                System.IO.Directory.CreateDirectory(@"C:\Reports");
            }   
            if(!System.IO.File.Exists(logFilePath))
            {
                System.IO.File.Create(logFilePath).Close();
            }
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(logFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }





        }




    }
}

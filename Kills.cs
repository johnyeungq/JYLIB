using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    internal class Kills
    {
      
        internal void Kill(string exe)
        {
            Process[] processes = Process.GetProcessesByName(exe);
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }


    }
}

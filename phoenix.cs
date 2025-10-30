using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace JYLIB
{
    internal class phoenix
    {


        internal async Task Start()
        {
            var processNames = LoadProcesses();
            foreach (var processName in processNames)
            {
                Console.WriteLine(processName);
            }

            // Wait for 2 seconds
            await WaitNSeconds(2);

            // Stop processes and restart the computer
           await StopProcesses(processNames);
        }

        private static async Task WaitNSeconds(int n) => await Task.Delay(n * 1000);
        private static async Task StopProcesses(string[] processNames)
        {
            foreach (var processName in processNames)
            {
                try
                {
                    Process[] processesByName = Process.GetProcessesByName(processName);
                    foreach (var process in processesByName)
                    {
                        process.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error stopping process {processName}: {ex.Message}");
                }
            }
           await RestartComputer();
        }

        //create a fcunction to restart computer
        private static async Task RestartComputer()
        {
            await Task.Run(() =>
            {
                ProcessStartInfo psi = new ProcessStartInfo("shutdown", "/r /t 0")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                Process.Start(psi);
            });
        }
     



        private static string[] LoadProcesses()
        {
            // Get all processes except the current one and "devenv"
            var processList = Process.GetProcesses();
            var processNames = new List<string>();

            foreach (var process in processList)
            {
                if (process.ProcessName != nameof(process.ProcessName) && process.ProcessName != "devenv" && process.ProcessName != "TasksMaster")
                {
                    processNames.Add(process.ProcessName);
                }
            }
            return processNames.ToArray();
        }
       





    }

    



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace JYLIB
{
    internal class ExcelRefresh
    {
        private BackgroundWorker refreshWorker;
        private string excelFilePath;

        internal async Task RefreshingExcel(string path)
        {
            excelFilePath = path;

            MessageBox.Show($"Excel: {path} is now refreshing");
            SetupRefreshWorker();
    
            // Start refresh when form loads
            if (!refreshWorker.IsBusy)
            {
           
                refreshWorker.RunWorkerAsync();
            }
        }
     
        private void RefreshWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = null;
            Workbook workbook = null;

            try
            {
                excelApp = new Microsoft.Office.Interop.Excel.Application { Visible = false, DisplayAlerts = false };
                workbook = excelApp.Workbooks.Open(excelFilePath);

                // Get all connections
                Connections connections = workbook.Connections;
                int totalConnections = connections.Count > 0 ? connections.Count : 1; // Handle no connections
                int completedConnections = 0;

                // Refresh all connections
                refreshWorker.ReportProgress(0, "Refreshing all connections...");
                workbook.RefreshAll();

                // Poll connection status for progress
                while (completedConnections < totalConnections)
                {
                    if (refreshWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                    completedConnections = 0;
                    if (connections.Count > 0)
                    {
                        foreach (WorkbookConnection conn in connections)
                        {
                            if (!conn.OLEDBConnection.Refreshing)
                            {
                                completedConnections++;
                            }
                        }
                    }
                    else
                    {
                        // If no connections, assume refresh is done after a short delay
                        completedConnections = 1;
                        System.Threading.Thread.Sleep(5000); // Adjust delay as needed
                    }

                    int progressPercentage = (int)((double)completedConnections / totalConnections * 100);
                    refreshWorker.ReportProgress(progressPercentage, $"Refreshing {completedConnections}/{totalConnections} connections...");
                    System.Threading.Thread.Sleep(1000); // Poll every second
                }

                // Save and close
                workbook.Save();
                refreshWorker.ReportProgress(100, "Refresh completed.");
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
            finally
            {
                // Clean up Excel objects
                if (workbook != null)
                {
                    workbook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }
            }
        }

        private void RefreshWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        private void RefreshWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
              
            }
            else if (e.Error != null)
            {
             
                LogError($"Refresh error: {e.Error.Message}");
                MessageBox.Show($"Error: {e.Error.Message}");
            }
            else if (e.Result is string errorMessage)
            {
               
                LogError($"Refresh error: {errorMessage}");
                MessageBox.Show($"Error: {errorMessage}");
            }
            else
            {
                MessageBox.Show($"Refresh completed successfully. Last Modified: {LastModifiedDateTime(excelFilePath)}");
                
            }
        }

        private string LastModifiedDateTime(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    DateTime lastModified = File.GetLastWriteTime(file);
                    return $"Last Modified: {lastModified:yyyy-MM-dd HH:mm:ss}";
                }
                else
                {
                    return "Last Modified: File not found";
                }
            }
            catch (Exception ex)
            {
                LogError($"Error getting last modified: {ex.Message}");
                return $"Last Modified: Error: {ex.Message}";
            }
        }

        private void LogError(string message)
        {
            string logDir = @"C:\Reports";
            string logFile = System.IO.Path.Combine(logDir, "ErrorLog.txt");
            try
            {
                if (!System.IO.Directory.Exists(logDir))
                {
                    System.IO.Directory.CreateDirectory(logDir);
                }
                System.IO.File.AppendAllText(logFile, $"{DateTime.Now}: {message}\n");
            }
            catch
            {
                // Optionally handle logging failure (e.g., show a message or ignore)
            }
        }
        private void SetupRefreshWorker()
        {
            refreshWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            refreshWorker.DoWork += RefreshWorker_DoWork;
            refreshWorker.ProgressChanged += RefreshWorker_ProgressChanged;
            refreshWorker.RunWorkerCompleted += RefreshWorker_RunWorkerCompleted;
        }
      
    }
}

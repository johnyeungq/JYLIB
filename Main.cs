using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    public class Main
    {
        #region Classes

        private readonly Open _open;
        private readonly Excel _excel;
        private readonly Kills _kills;
        private readonly SQL _sql;
        private readonly Import _import;
        private readonly DGV _DGV;
        private readonly Start _start;
        private readonly SystemData _sysData;
        private readonly Logs _log;
        private readonly XML _xml;
        private readonly MSaccess _access;
        private readonly JCopy _copy;
        private readonly WindowsManagerJY _WMJY;
        private readonly phoenix _phoenix = new phoenix();  

        public Main()
        {

            _access = new MSaccess();
            _xml = new XML();
            _open = new Open();
            _excel = new Excel();
            _kills = new Kills();
            _sql = new SQL();
            _import = new Import();
            _DGV = new DGV();
            _sysData = new SystemData();
            _log = new Logs();  
            _copy = new JCopy();    
            _WMJY = new WindowsManagerJY(); 
            _phoenix = new phoenix();

        }
        #endregion

        #region Main
        public async Task StartTask(Task task)
        {
            await System.Threading.Tasks.Task.Run(() => task);

        }
        #endregion

        #region Start

        public void BrowseToTB(TextBox tb)
        {
            _start.Browse(tb);
        }
        public void LaunchEdge(string url)
        {
            _start.launchEdge(url);
        }
        #endregion

        #region Excel

        public void ExcelToDgv(string filePath, DataGridView dataGridView)
        {
            _excel.ExcelToDgv(filePath, dataGridView);
        }

        public void DgvToExcel(DataGridView dgv, string excelPath)
        {
            _excel.DGVtoExcel(dgv, excelPath);
        }

        public void KillExcel()
        {
            _excel.KillAllExcel();
        }

        public void ImportNewRowToExcel(string filePath, int SheetIndex, string[] data)
        {
            _excel.ImportNewRowToExcel(filePath, SheetIndex, data);
        }

        public void RefreshExcel(string filePath)
        {
            _excel.RefreshExcel(filePath);
        }
        #endregion

        #region Kill

        public void Kill(string exe)
        {
            _kills.Kill(exe);
        }

        #endregion

        #region Copy

        public string[] CopyCount(string org, string des)
        {
            string[] counts = _copy.CopyCount(org, des);
            return counts;


        }

        #endregion 



        #region SQL
        public void SqlToDgv(string server, string selectedDatabase, string selectedTable, DataGridView dgv)
        {
            _sql.SQLtoDGV(server, selectedDatabase, selectedTable, dgv);
        }

        public void SqlToDgvSelecting(string server, string selectedDatabase, string selectedTable, string value, string column, DataGridView dgv)
        {
            _sql.SQLtoDGVselecting(server, selectedDatabase, selectedTable, value, column, dgv);
        }

        public string SQLConnectionstring(string server, string DataBase)
        {

            return _sysData.SQLConnectionstring(server, DataBase);
        }

        public string AccessConnectionString(string filepath)
        {
            return _sysData.AccessConnectionString(filepath);
        }
        public string[] SQLDatabaseName(string connectionString)
        {
            return _sql.SQLDatabaseName(connectionString);
        }
        public string[] SQLTableName(string connectionString, string databaseName)
        {
            return _sql.SQLTables(connectionString, databaseName);
        }

        public void SQLfilterToDGV(string server, string selectedDatabase, string selectedTable, string value, DataGridView dgv)
        {
            _sql.SQLtoDGVFiltering(server, selectedDatabase, selectedTable, value, dgv);
        }

        #endregion



        #region Import
        public void ImportFileNameToLB(ListBox lb, string folderpath)
        {
            _import.ImportFileNameToLB(lb, folderpath);
        }
        public void ImportFileNameToCB(string folderPath, string filter, ComboBox cb)
        {
            _import.ImportFileNameToCB(folderPath, filter, cb);
        }
        #endregion


        #region DGV
        public string CellValue(DataGridView dgv, int row, int column)
        {
            return _DGV.CellVaule(dgv, row, column);
        }


        #endregion



        #region Logs

        public void Log(string message)
        {
            _log.Log(message);
        }


        #endregion


        #region System Data
        public string HostName()
        {
            return _sysData.HostName();
        }

        public string LocalIPaddress()
        {
            return _sysData.LocalIPaddress();
        }
        #endregion




        #region XML

        public List<string> XMLfiletoList(string xmlFilePath, string child, string Node)
        {
            return _xml.XMLfiletoList(xmlFilePath, child, Node);
        }

        public void CreateXmlFile(string xmlFilePath, List<string> values, string child, string node)
        {
            _xml.CreateXmlFile(xmlFilePath, values, child, node);
        }


        #endregion


        #region MSaccess
        public void AccessToCB(string connectionString, string table, string column, ComboBox cb)
        {
           _access.AccessToCB(connectionString, table, column, cb);
        }

        #endregion



        #region Checking 

        public static bool IsDarkMode()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if (key != null)
                {
                    var value = key.GetValue("AppsUseLightTheme");
                    return value is int lightValue && lightValue == 0;
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
            return false; 
        }

        #endregion


        #region Windows Control

       public async Task MakeProcessWindowTopMostAndMax(string processName)
        {
              await _WMJY.MakeProcessWindowTopMostAndMax(processName);
        }   

        public void MinimizeAllWindows()
        {
              _WMJY.MinimizeAllWindows();
        }


        #endregion


        #region Phoenix


        public async Task StartPhoenixProcedure()
        {
         await  _phoenix.Start();
        }


        #endregion 

    }
}
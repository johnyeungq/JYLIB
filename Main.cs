using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    public class Main
    {
        private readonly Open _open;
        private readonly Excel _excel;
        private readonly Kills _kills;
        private readonly SQL _sql;
        private readonly Import _import;
        private readonly DGV _DGV;
        private readonly Start _start;


        public Main()
        {
            _open = new Open();
            _excel = new Excel();
            _kills = new Kills();
            _sql = new SQL();
            _import = new Import();
            _DGV = new DGV();
        }

        public void Browse(TextBox tb)
        {
            _open.Browse(tb);
        }

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

        public void Kill(string exe)
        {
            _kills.Kill(exe);
        }

        public void SqlToDgv(string server, string selectedDatabase, string selectedTable, DataGridView dgv)
        {
            _sql.SQLtoDGV(server, selectedDatabase, selectedTable, dgv);
        }

        public void SqlToDgvSelecting(string server, string selectedDatabase, string selectedTable, string value, string column, DataGridView dgv)
        {
            _sql.SQLtoDGVselecting(server, selectedDatabase, selectedTable, value, column, dgv);
        }


        public void LaunchEdge(string url)
        {
            _open.launchEdge(url);
        }


        public void ImportFileNameToLB(ListBox lb, string folderpath)
        {
            _import.ImportFileNameToLB(lb, folderpath);
        }

        public string CellValue(DataGridView dgv, int row, int column)
        {
            return _DGV.CellVaule(dgv, row, column);
        }


        public async Task StartTask(Task task)
        {
            await System.Threading.Tasks.Task.Run(() => task);

        }
        public void ImportNewRowToExcel(string filePath,int SheetIndex, string[] data)
        {
            _excel.ImportNewRowToExcel(filePath, SheetIndex,data);
        }   

        public void RefreshExcel(string filePath)
        {
            _excel.RefreshExcel(filePath);
        }   
    }
}
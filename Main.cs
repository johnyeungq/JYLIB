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

        public Main()
        {
            _open = new Open();
            _excel = new Excel();
            _kills = new Kills();
            _sql = new SQL();
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


        public  void LaunchEdge(string url)
        {
            _open.launchEdge(url);
        }   





    }
}
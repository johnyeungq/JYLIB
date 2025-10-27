using System.Diagnostics;
using System.Runtime.InteropServices;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;




namespace JYLIB
{
    internal class Excel
    {
        internal void KillAllExcel()
        {
            Process[] processes = Process.GetProcessesByName("excel");
            foreach (Process process in processes)
            {
                process.Kill();
            }
           
        }

        internal DataTable ExcelToDataTable(string Excel)
        {
            _Excel.Application excelApp = new _Excel.Application();
            _Excel.Workbook workbook = null;
            _Excel.Worksheet worksheet = null;

            DataTable dataTable = new DataTable();

            try
            {
                workbook = excelApp.Workbooks.Open(Excel);
                worksheet = workbook.ActiveSheet as _Excel.Worksheet;

                _Excel.Range range = worksheet.UsedRange;

                object[,] values = (object[,])range.Value;

                int rowCount = values.GetLength(0);
                int columnCount = values.GetLength(1);

                for (int col = 1; col <= columnCount; col++)
                {
                    string columnName = values[1, col]?.ToString();
                    if (dataTable.Columns.Contains(columnName))
                    {
                        int count = 1;
                        string newColumnName = columnName + count.ToString();
                        while (dataTable.Columns.Contains(newColumnName))
                        {
                            count++;
                            newColumnName = columnName + count.ToString();
                        }
                        dataTable.Columns.Add(newColumnName);
                    }
                    else
                    {
                        dataTable.Columns.Add(columnName);
                    }
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int col = 1; col <= columnCount; col++)
                    {
                        dataRow[col - 1] = values[row, col];
                    }

                    dataTable.Rows.Add(dataRow);
                }


            }
            finally
            {
                if (worksheet != null)
                {
                    Marshal.ReleaseComObject(worksheet);
                }

                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

            }


            return dataTable;

        }
        internal async void RefreshExcel(string path)
        {

            ExcelRefresh ER = new ExcelRefresh();
            await ER.RefreshingExcel(path);



        }

        internal void ImportNewRowToExcel(string filePath,int ?sheetIndex ,string[] data)
        {
            KillAllExcel();
            if (data.Count() <= 0)
            {
                MessageBox.Show("No data to import.");
                return;
            }

         
            _Excel.Application excelApp = new _Excel.Application();


            _Excel.Workbook workbook = excelApp.Workbooks.Open(filePath);


            _Excel.Worksheet DataSheet = null;

            excelApp.DisplayAlerts = false;
            if (sheetIndex == null)
            {

                foreach (_Excel.Worksheet sheet in workbook.Sheets)
                {
                    if (sheet.Index >= 0)
                    {
                        DataSheet = sheet;

                        break;
                    }
                }
            }
            else { 
            DataSheet = workbook.Worksheets[sheetIndex] as _Excel.Worksheet;
            }


            if (DataSheet != null)
            {
                DataSheet.Unprotect();

                int nextRow = DataSheet.Cells.SpecialCells(_Excel.XlCellType.xlCellTypeLastCell).Row + 1;



                try
                {
                    for (int i = 0; i < data.Count(); i++)
                    {
                        _Excel.Range Cell = DataSheet.Cells[nextRow, i + 1];
                    
                        Cell.Value2 = data[i]?.ToString() ?? "";
                        
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                workbook.Save();
                MessageBox.Show(DataSheet.Name +" DataSheet Saved!");
                DataSheet.Protect();
            }
            else
            {
                MessageBox.Show("DataSheet Null");
                return;

            }


        
        
        

           

            // Close the workbook and release the resources
            workbook.Close();
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(DataSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        }


        internal void ExcelToDgv(string filePath, DataGridView dataGridView)
        {
            dataGridView.DataSource = null; // Clear the existing data source

           _Excel.Application excelApp = new _Excel.Application();
           _Excel.Workbook workbook = null;
            _Excel.Worksheet worksheet = null;

            try
            {
                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = workbook.ActiveSheet as _Excel.Worksheet;

                _Excel.Range range = worksheet.UsedRange;

                object[,] values = (object[,])range.Value;

                int rowCount = values.GetLength(0);
                int columnCount = values.GetLength(1);

                System.Data.DataTable dataTable = new System.Data.DataTable();

                for (int col = 1; col <= columnCount; col++)
                {
                    string columnName = values[1, col]?.ToString().Trim();
                    if (dataTable.Columns.Contains(columnName))
                    {
                        int count = 1;
                        string newColumnName = columnName + count.ToString();
                        while (dataTable.Columns.Contains(newColumnName))
                        {
                            count++;
                            newColumnName = columnName + count.ToString();
                        }
                        dataTable.Columns.Add(newColumnName);
                    }
                    else
                    {
                        dataTable.Columns.Add(columnName);
                    }
                }

                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int col = 1; col <= columnCount; col++)
                    {
                        dataRow[col - 1] = values[row, col];
                    }

                    dataTable.Rows.Add(dataRow);
                }

                dataGridView.DataSource = dataTable;
            }
            finally
            {
                if (worksheet != null)
                {
                    Marshal.ReleaseComObject(worksheet);
                }

                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
            }
        }
        internal void DGVtoExcel(DataGridView dgv, string excelPath)
        {
            _Excel.Application excelApp = null;
            _Excel.Workbook workbook = null;
            _Excel.Worksheet worksheet = null;

            try
            {
                // Create Excel application
                excelApp = new _Excel.Application();
                workbook = excelApp.Workbooks.Add();
                worksheet = workbook.ActiveSheet;

                // Export column headers
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
                }

                // Export data rows
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        if (dgv.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = dgv.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                // Auto-fit columns
                worksheet.Columns.AutoFit();

                // Save the workbook
                workbook.SaveAs(excelPath);

                MessageBox.Show("Data exported successfully to " + excelPath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close and clean up
                if (worksheet != null)
                {
                    Marshal.ReleaseComObject(worksheet);
                }
                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
            }
        }



     

    }
}
  


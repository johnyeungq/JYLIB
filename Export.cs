using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Excel = Microsoft.Office.Interop.Excel;
namespace JYLIB
{
    internal class Export
    {

        internal void ListBoxToExcel(string filepath, ListBox lb) {

            // Create a new Excel application
            _Excel.Application excelApp = new _Excel.Application();
            _Excel.Workbook workbook = null;
            _Excel.Worksheet worksheet = null;

            try
            {
                // Create a new workbook
                workbook = excelApp.Workbooks.Add();
                worksheet = workbook.ActiveSheet as _Excel.Worksheet;

                // Get the items from InputLB and add them to the first column of the worksheet
                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string item = lb.Items[i].ToString();
                    worksheet.Cells[i + 1, 1] = item;
                }

                // Save the workbook

                workbook.SaveAs(filepath);

                // Close the workbook and quit Excel application
                workbook.Close();
                excelApp.Quit();

                MessageBox.Show("Excel file created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating Excel file: " + ex.Message);
            }
            finally
            {
                // Release the COM objects
                if (worksheet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                }

                if (workbook != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }

            }
        }



    }
}

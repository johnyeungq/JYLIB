using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Office.Interop.Excel;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace JYLIB
{
    internal class SQL
    {

        internal void SQLtoDGV(string server ,string selectedDatabase, string selectedTable, DataGridView dgv)
        {
       
       
         
            string connectionString = $"Data Source={server};Integrated Security=True;Connect Timeout=30;Encrypt=False;Initial Catalog=" + selectedDatabase;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();


                string query = "SELECT * FROM " + selectedTable;
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                System.Data.DataTable dataTable = new System.Data.DataTable();
                adapter.Fill(dataTable);

                dgv.DataSource = dataTable;
                dgv.Sort(dgv.Columns[0], System.ComponentModel.ListSortDirection.Descending);







            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        
        
        }

        internal void SQLtoDGVselecting(string server,string selectedDatabase, string selectedTable, string value, string column, DataGridView dgv)
        {
            string connectionString = $"Data Source={server};Integrated Security=True;Connect Timeout=30;Encrypt=False;Initial Catalog={selectedDatabase}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM {selectedTable} WHERE [{column}] = @Value";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@Value", value);
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        adapter.Fill(dataTable);

                        dgv.DataSource = dataTable;
                        if (dgv.Columns.Count > 0)
                        {
                            dgv.Sort(dgv.Columns[0], System.ComponentModel.ListSortDirection.Descending);
                        }
                        else
                        {
                            MessageBox.Show("No columns returned from the query. Check the table or column name.");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data into DataGridView: {ex.Message}");
                }
            }
        }

    }
}

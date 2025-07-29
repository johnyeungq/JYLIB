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
using System.Buffers;

namespace JYLIB
{
    internal class SQL
    {

        internal string[] SQLDatabaseName(string Connectionstring)
        {
            List<string> names = new List<string>();
            string connectionString = Connectionstring;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT name FROM sys.databases";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string databaseName = reader["name"].ToString();
                           
                                names.Add(databaseName);
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving database names: {ex.Message}");
                    return new string[0]; // Return empty array on error
                }
            }

            return names.ToArray();
        }


        internal string[] SQLTables(string server,string Database) {
            List<string> Tables = new List<string>();
           
            string connectionString = $"Data Source={server};Integrated Security=True;Connect Timeout=30;Encrypt=False;Initial Catalog=" + Database;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                System.Data.DataTable tables = connection.GetSchema("Tables");
               
                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    Tables.Add(tableName);
                }
                return Tables.ToArray();
            }
            catch (Exception ex)
            {
                return new string[0]; // Return empty array on error
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            
        }

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

        internal void SQLtoDGVFiltering(string server,string selectedDatabase, string selectedTable, string value, DataGridView dgv)
        {
            string connectionString = $"Data Source={server};Integrated Security=True;Connect Timeout=30;Encrypt=False;Initial Catalog={selectedDatabase}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT * FROM {selectedTable} WHERE ";
                    SqlCommand command = new SqlCommand(query, connection);

                    System.Data.DataTable dataTable = new System.Data.DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);


                    System.Data.DataTable schemaTable = connection.GetSchema("Columns", new[] { null, null, selectedTable });
                    List<string> columnNames = schemaTable.AsEnumerable().Select(row => row.Field<string>("COLUMN_NAME")).ToList();



                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        string columnName = columnNames[i];
                        query += $"{columnName} LIKE @searchValue{i}";

                        if (i < columnNames.Count - 1)
                            query += " OR ";

                        command.Parameters.AddWithValue($"@searchValue{i}", "%" + value + "%");
                    }

                    adapter.SelectCommand.CommandText = query;
                    adapter.Fill(dataTable);

                    dgv.DataSource = dataTable;
                
                
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
        internal void SQLtoDGVselecting(string server, string selectedDatabase, string selectedTable, string value, string column, DataGridView dgv)
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

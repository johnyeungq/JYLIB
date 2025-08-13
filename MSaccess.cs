
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;



namespace JYLIB
{
    internal class MSaccess
    {

        internal void AccessToCB(string connectionString,string tabel ,string Column ,ComboBox CB)
        {


         

            string query = $"SELECT {Column} FROM {tabel}";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            CB.Items.Add(reader[Column].ToString());


                        }
                    }
                }
            }


        }

    }
}

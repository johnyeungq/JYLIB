using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace JYLIB
{
    internal class DGV
    {

        internal string CellVaule(DataGridView dgv, int row , int column)
        {

            string value = "?";
            foreach (DataGridViewRow dgvRow in dgv.Rows)
            {
                if (row > -1 && column > -1)
                {


                    value = dgv.Rows[row].Cells[column].Value.ToString();
                }
                else {

                    value = "!";
                }
            }


            return value;
            
        }





    }
}

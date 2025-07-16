using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace JYLIB
{
    public class Open
    {

        public void Browse(TextBox tb) {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
             
                openFileDialog.Title = "Select an File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                   tb.Text = openFileDialog.FileName;
                }
            }
        }

    }
}

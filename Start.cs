using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    internal class Start
    {


        internal async Task StartParellelTask(Task task) {

            await System.Threading.Tasks.Task.Run(() => task);

        }
        internal void Browse(TextBox tb)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {

                openFileDialog.Title = "Select an File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tb.Text = openFileDialog.FileName;
                }
            }
        }

        internal void launchEdge(string url)
        {
            string Edge = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";


            if (url.StartsWith("http") || url.StartsWith("www"))
            {

                System.Diagnostics.Process.Start(new ProcessStartInfo(Edge, url));

            }
            else
            {

                Process.Start("explorer.exe", url);

            }

        }
    }
}

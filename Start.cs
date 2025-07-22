using System;
using System.Collections.Generic;
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
    }
}

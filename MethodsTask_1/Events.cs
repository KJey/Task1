using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MethodsTask_1
{
    public class StartEventArgs : System.EventArgs
    {
    }

    public class FinishEventArgs : System.EventArgs
    {
    }

    public class FileFindedEventArgs<T> : System.EventArgs
    where T : FileSystemInfo

    {

        public T FindedItem { get; set; }
        //    public ActionType ActionType { get; set; }

    }

}

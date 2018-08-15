using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsTask_1.EventArgs
{
    public class FileFindedEventArgs<T> : System.EventArgs
         where T : FileSystemInfo
    {
        public T FindedItem { get; set; }
        public ActionType ActionType { get; set; }
    }
}

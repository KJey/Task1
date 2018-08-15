using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodsTask_1
{
    public class FoundedFileInfo
    {
        public string FullName { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }

        public FoundedFileInfo(string fullName, string name, string folder)
        {
            this.FullName = fullName;
            this.Name     = name;
            this.Folder   = folder;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MethodsTask_1
{
    class FileSystemVisitor
    {

         private readonly DirectoryInfo DefaultPath;


        public FileSystemVisitor(DirectoryInfo startDirectory)

        {

            DefaultPath = startDirectory;

        }

        public IEnumerable<FileSystemInfo> ShowFileSystemInfo()
         {
             foreach(var FileSystemInfo in ShowAllInside(DefaultPath))
             {
                yield return FileSystemInfo;
               
             }
         }


         public IEnumerable<FileSystemInfo> ShowAllInside(DirectoryInfo dirInf)
         {
             foreach (var FileSystemInfo in dirInf.EnumerateFileSystemInfos())
             {
                

                if (FileSystemInfo is FileSystemInfo)

                {

                    yield return FileSystemInfo;
                    

                }


                

                if (FileSystemInfo is DirectoryInfo)
                 {
                     
                     DirectoryInfo dir = new DirectoryInfo(FileSystemInfo.FullName.ToString());

                    foreach (var InsideFolderInfo in ShowAllInside(dir))
                     {
                         yield return InsideFolderInfo;
                     }
                     continue;
                 }
                 
                 
             }
         }


    }
}

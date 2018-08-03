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


        public event EventHandler<StartEventArgs> Start;

        public event EventHandler<FinishEventArgs> Finish;

        public event EventHandler<FileFindedEventArgs<FileInfo>> FileFinded;

        public event EventHandler<FileFindedEventArgs<FileInfo>> FilteredFileFinded;

        public event EventHandler<FileFindedEventArgs<DirectoryInfo>> DirectoryFinded;

        public event EventHandler<FileFindedEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        public FileSystemVisitor(DirectoryInfo startDirectory)

        {

            DefaultPath = startDirectory;

        }

        public IEnumerable<FileSystemInfo> ShowFileSystemInfo()
         {
            OnEvent(Start, new StartEventArgs());
             foreach(var FileSystemInfo in ShowAllInside(DefaultPath))
             {
                yield return FileSystemInfo;
               
             }
            OnEvent(Finish, new FinishEventArgs());
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

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)

        {

            someEvent?.Invoke(this, args);

        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MethodsTask_1.EventArgs;

namespace MethodsTask_1
{
    class FileSystemVisitor
    {
        //Parameters:
        private readonly DirectoryInfo DefaultPath;
        private readonly Func<FileSystemInfo, bool> filter;
        private readonly IFileSystemProcessingStrategy fileSystemProcessingStrategy;

        //Events:
        public event EventHandler<StartEventArgs> Start;
        public event EventHandler<FinishEventArgs> Finish;
        public event EventHandler<FileFindedEventArgs<FileInfo>> FileFinded;
        public event EventHandler<FileFindedEventArgs<FileInfo>> FilteredFileFinded;
        public event EventHandler<FileFindedEventArgs<DirectoryInfo>> DirectoryFinded;
        public event EventHandler<FileFindedEventArgs<DirectoryInfo>> FilteredDirectoryFinded;

        //Constructors:
        public FileSystemVisitor(DirectoryInfo startDirectory) => DefaultPath = startDirectory;
        public FileSystemVisitor(DirectoryInfo startDirectory, IFileSystemProcessingStrategy fileSPS, Func<FileSystemInfo, bool> default_filter = null)
        {
            DefaultPath                  = startDirectory;
            filter                       = default_filter;
            fileSystemProcessingStrategy = fileSPS;
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
                        DirectoryInfo dir = new DirectoryInfo(FileSystemInfo.FullName);
                        FileAttributes attr = (new FileInfo(dir.FullName)).Attributes;
                    var rules = dir.GetAccessControl();
                    if ((attr & FileAttributes.Hidden) > 0)
                            continue;
                        if ((attr & FileAttributes.ReadOnly) > 0)
                            Console.WriteLine("This file is read-only.");
                    //if (rules.AreAccessRulesProtected)
                      //  break;//Console.WriteLine("This file is protected.");
                    //if ((attr & FileAttributes.) > 0)
                    //   Console.WriteLine("This file is read-only.");
                    //var rules = di.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));

                    else
                    {
                        foreach (var InsideFolderInfo in ShowAllInside(dir))
                        {
                            //DirectoryInfo dir1 = new DirectoryInfo(InsideFolderInfo.FullName);
                            yield return InsideFolderInfo;
                            //ShowAllInside(dir1);
                        }
                        continue;
                        //yield return FileSystemInfo;
                    }
                    }
                    continue;
                }
            
                 
             
         }

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }

        private ActionType ProcessFile(FileInfo file)
        {
            return fileSystemProcessingStrategy
                .ProcessItemFinded(file, filter, FileFinded, FilteredFileFinded, OnEvent);
        }

        private ActionType ProcessDirectory(DirectoryInfo directory)
        {
            return fileSystemProcessingStrategy
                .ProcessItemFinded(directory, filter, DirectoryFinded, FilteredDirectoryFinded, OnEvent);
        }

        private class CurrentAction
        {
            public ActionType Action { get; set; }
            public static CurrentAction ContinueSearch => new CurrentAction { Action = ActionType.ContinueSearch };
        }

    }
}


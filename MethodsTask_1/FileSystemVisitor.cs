using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MethodsTask_1.EventArgs;

namespace MethodsTask_1
{
    public class FileSystemVisitor
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
        public FileSystemVisitor(DirectoryInfo startDirectory)
        {
            DefaultPath = startDirectory;
        }
        public FileSystemVisitor(DirectoryInfo startDirectory, IFileSystemProcessingStrategy fileSPS, Func<FileSystemInfo, bool> default_filter = null)
        {
            DefaultPath = startDirectory;
            filter = default_filter;
            fileSystemProcessingStrategy = fileSPS;
        }
        public FileSystemVisitor(string startDirectory, IFileSystemProcessingStrategy fileSPS, Func<FileSystemInfo, bool> default_filter = null)
        {
            DirectoryInfo DefPath = new DirectoryInfo(startDirectory);
            DefaultPath = DefPath;
            filter = default_filter;
            fileSystemProcessingStrategy = fileSPS;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FileSystemInfo> ShowFileSystemInfo()
        {
            OnEvent(Start, new StartEventArgs());
            foreach (var FileSystemInfo in ShowAllInside(DefaultPath, CurrentAction.ContinueSearch))
            {
                yield return FileSystemInfo;
            }
            OnEvent(Finish, new FinishEventArgs());
        }

        public IEnumerable<FoundedFileInfo> ShowFoundedFiles()
        {
            OnEvent(Start, new StartEventArgs());
            foreach (var FileSystemInfo in ShowAllInside(DefaultPath, CurrentAction.ContinueSearch))
            {
                FoundedFileInfo toReturn = new FoundedFileInfo(FileSystemInfo.FullName, FileSystemInfo.Name, null);
                yield return toReturn;
            }
            OnEvent(Finish, new FinishEventArgs());
        }

        //iterator:
        private IEnumerable<FileSystemInfo> ShowAllInside(DirectoryInfo dirInf,CurrentAction curentAction)
         {

                foreach (var FileSystemInfo in dirInf.EnumerateFileSystemInfos())
                {

                    //checking for file
                    if (FileSystemInfo is FileInfo)
                    {
                    FileInfo file = new FileInfo(FileSystemInfo.FullName);
                    curentAction.Action = ProcessFile(file);
                    }
                    //checking for folder
                    if (FileSystemInfo is DirectoryInfo)
                    {
                        DirectoryInfo dir = new DirectoryInfo(FileSystemInfo.FullName);
                        curentAction.Action = ProcessDirectory(dir);
                        FileAttributes attr = (new FileInfo(dir.FullName)).Attributes;
                    var rules = dir.GetAccessControl();
                        //checking for attributes
                        if ((attr & FileAttributes.Hidden) > 0)
                            continue;
                        if ((attr & FileAttributes.ReadOnly) > 0)
                            Console.WriteLine("This file is read-only.");
                        if (curentAction.Action == ActionType.StopSearch)
                            yield break;
                        else
                        {
                            foreach (var InsideFolderInfo in ShowAllInside(dir, curentAction))
                            {
                            yield return InsideFolderInfo;
                            }
                        continue;
                        }
                    }

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


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Moq;
using MethodsTask_1;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTests
    {

        DirectoryInfo Path = new DirectoryInfo(@"D:\\12");


        [TestMethod]
        public void ShowFoundedFiles()
        {
            int i = 0;
            List<string> result   = new List<string>();
            FileSystemVisitor FSV = new FileSystemVisitor(Path, new FileSystemProcessingStrategy(), filter => false);
            FSV.Start += (s, e) =>
            {
                i++;
            };

            FSV.Finish += (s, e) =>
            {
                i++;
            };

            FSV.FileFinded += (s, e) =>
            {
                result.Add(e.FindedItem.Name);
            };

            FSV.DirectoryFinded += (s, e) =>
            {
                result.Add(e.FindedItem.Name);
            };

            FSV.FilteredFileFinded += (s, e) =>
            {
                result.Add(e.FindedItem.Name);
            };

            FSV.FilteredDirectoryFinded += (s, e) =>
            {
                result.Add(e.FindedItem.Name);
            };

            foreach (var fileinfo in FSV.ShowFileSystemInfo())
            {

            }
            Assert.AreEqual(11, result.Count);

        }

        [TestMethod]
        public void StartEvent()
        {
            int i = 0;
            FileSystemVisitor FSV = new FileSystemVisitor(Path, new FileSystemProcessingStrategy(), filter => false);

            FSV.Start += (s, e) =>
            {
                i++;
            };

            foreach (var fileinfo in FSV.ShowFileSystemInfo())
            {

            }

            Assert.AreEqual(i, 1);

        }

        [TestMethod]
        public void FinishEvent()
        {
            int i = 0;
            FileSystemVisitor FSV = new FileSystemVisitor(Path, new FileSystemProcessingStrategy(), filter => false);

            FSV.Finish += (s, e) =>
            {
                i++;
            };

            foreach (var fileinfo in FSV.ShowFileSystemInfo())
            {

            }
            Assert.AreEqual(i, 1);

        }

        [TestMethod]
        public void FileFindedEvent()
        {
            int i = 0;
            bool check = false;
            FileSystemVisitor FSV = new FileSystemVisitor(Path, new FileSystemProcessingStrategy(), filter => false);

            FSV.FileFinded += (s, e) =>
            {
                i++;
            };

            foreach (var fileinfo in FSV.ShowFileSystemInfo())
            {

            }
            if (i > 0) check = true;
            Assert.IsTrue(check);

        }

        [TestMethod]
        public void DirectoryFindedEvent()
        {
            int i = 0;
            bool check = false;
            FileSystemVisitor FSV = new FileSystemVisitor(Path, new FileSystemProcessingStrategy(), filter => false);

            FSV.DirectoryFinded += (s, e) =>
            {
                i++;
            };

            foreach (var fileinfo in FSV.ShowFileSystemInfo())
            {

            }
            if (i > 0) check = true;
            Assert.IsTrue(check);

        }







        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }


    }
}

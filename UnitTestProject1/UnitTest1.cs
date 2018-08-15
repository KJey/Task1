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
        
        FileSystemVisitor FSV = new FileSystemVisitor((@"D:\\12"), new FileSystemProcessingStrategy(), filter => false);

        [TestMethod]
        public void ShowFoundedFiles()
        {

            FileSystemVisitor FSVin = new FileSystemVisitor((@"D:\\12"), new FileSystemProcessingStrategy(), filter => false);

            List<string> result = new List<string>();

            List<string> expectations = new List<string>();

            expectations.Add("14");
            expectations.Add("Mentoring.2017.Module.01 - master.zip");
            expectations.Add("15");
            expectations.Add("16");
            expectations.Add("Mentoring.2017.Module.01 - master.zip");
            expectations.Add("17");
            expectations.Add("18");
            expectations.Add("1A");
            expectations.Add("Mentoring.2017.Module.01 - master.zip");
            expectations.Add("Mentoring.2017.Module.01 - master.zip");
            expectations.Add("Task.docx");
            string vas = "";
            int i = 0;
            foreach (var filesinfo in FSVin.ShowFoundedFiles())
            {
                i++;
                vas = filesinfo.Name;
                result.Add(filesinfo.Name);
            }

            Assert.AreEqual(expectations, result);

        }

        [TestMethod]
        public void ShowFoundedFiles_vs_ShowFileSystemInfo_Methods()
        {
            List<string> result_SFF = new List<string>();

            List<string> result_FSI = new List<string>();

            foreach (var fileinfo in FSV.ShowFoundedFiles())
            {

                result_SFF.Add(fileinfo.FullName);
            }

            foreach (var fileinfo in FSV.ShowFileSystemInfo())
            {

                result_FSI.Add(fileinfo.FullName);
            }

            Assert.AreEqual(result_SFF, result_FSI);

        }


        //[TestMethod]
        //public void Test____()
        //{
        //    FileSystemVisitor FSVinside = FSV;
        //    int delegatesCallCount = 0;
        //    FSVinside.Start;
        //}






        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }


    }
}

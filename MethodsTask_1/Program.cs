using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MethodsTask_1;

namespace MethodsTask_1
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {

                DirectoryInfo DefaultPath = new DirectoryInfo(@"C:\Users\KJey\Desktop\WH ГОРОДА");//(@"D:\\12");
                FileSystemVisitor FSV     = new FileSystemVisitor(DefaultPath, new FileSystemProcessingStrategy(), (info) => true);

                FSV.Start += (s, e) =>
                {
                    Console.WriteLine("Iteration started");
                };

                FSV.Finish += (s, e) =>
                {
                    Console.WriteLine("Iteration finished");
                };

                FSV.FileFinded += (s, e) =>
                {
                    Console.WriteLine("\tFounded file: " + e.FindedItem.Name);
                };

                FSV.DirectoryFinded += (s, e) =>
                {
                    Console.WriteLine("\tFounded directory: " + e.FindedItem.Name);
                    if (e.FindedItem.Name.Length == 4)
                    {
                        e.ActionType = ActionType.StopSearch;
                    }
                };

                FSV.FilteredFileFinded += (s, e) =>
                {
                    Console.WriteLine("Founded filtered file: " + e.FindedItem.Name);
                };

                FSV.FilteredDirectoryFinded += (s, e) =>
                {
                    Console.WriteLine("Founded filtered directory: " + e.FindedItem.Name);
                    if (e.FindedItem.Name.Length == 4)
                        e.ActionType = ActionType.StopSearch;
                };




                Console.WriteLine("Folder " + DefaultPath.FullName.ToString() + " contains:");
                foreach (var fileinfo in FSV.ShowFileSystemInfo())
                {

                    Console.WriteLine(fileinfo.FullName);
                }
                Console.WriteLine("Operation successfully ended");

            }

            //catch (UnauthorizedAccessException exception)
            //{
            //    Console.WriteLine(exception.Message);
            //    Console.WriteLine("Canceled");
            //    return;                
            //}

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Search canceled");
                return;
            }
        }
    }
}

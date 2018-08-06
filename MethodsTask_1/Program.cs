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

                DirectoryInfo DefaultPath = new DirectoryInfo(@"C:\Users\KJey\Desktop");//(@"D:\\12");
                FileSystemVisitor FSV     = new FileSystemVisitor(DefaultPath);


                Console.WriteLine("Folder " + DefaultPath.FullName.ToString() + " contains:");

                
                foreach (var fileinfo in FSV.ShowFileSystemInfo())
                {

                    Console.WriteLine(fileinfo.FullName);
                }
                Console.WriteLine("Operation successfully ended");

            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message.ToString());
                Console.WriteLine("Canceled");
            }

            finally
            {
                Console.WriteLine("Search ended");
                //Console.ReadKey();
            }

        }
    }
}

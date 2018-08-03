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

                DirectoryInfo DefaultPath = new DirectoryInfo(@"D:\\12");
                FileSystemVisitor FSV     = new FileSystemVisitor(DefaultPath);


                Console.WriteLine("Folder " + DefaultPath.FullName.ToString() + " contains:");

                
                foreach (var fileinfo in FSV.ShowFileSystemInfo())
                {

                    Console.WriteLine(fileinfo.FullName);
                }

            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message.ToString());
            }

            finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }

    }
    }
}

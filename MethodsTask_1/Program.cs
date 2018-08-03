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


            DirectoryInfo DefaultPath = new DirectoryInfo(@"D:\\12");

            Console.WriteLine("Folder "+DefaultPath.FullName.ToString()+" contains:");

            FileSystemVisitor FSV = new FileSystemVisitor(DefaultPath);

            //var exit = FSV.ShowAllInside(DefaultPath); //ToString();
            //Console.WriteLine(exit);
            
            foreach (var fileinfo in FSV.ShowFileSystemInfo())
            {
                
                Console.WriteLine(fileinfo.FullName);
            }


            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}

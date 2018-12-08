using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronisingThreads
{
    class Program
    {
        private static char[] charArray1 = new char[1000];
        private static char[] charArray2 = new char[1000];

        static void Main(string[] args)
        {

            if (args[1] == null || args[1] == "")
            {
                Console.WriteLine("Please provide two filepaths as parameters. Exiting Application...");
                Console.ReadKey();
                System.Environment.Exit(1);
            }

            Console.WriteLine("Comparing the provided files");
            var t1 = new Thread(new ThreadStart(delegate {ReadFileToCharArray(charArray1, args[0]);}));
            var t2 = new Thread(new ThreadStart(delegate {ReadFileToCharArray(charArray2, args[1]);}));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            for (int i = 0; i < charArray1.Length; i++)
            {
                if (!charArray1[i].Equals(charArray2[i]))
                {
                    Console.WriteLine("The Files are not the same. Exiting Program.");
                    Console.ReadKey();
                    System.Environment.Exit(1);
                }
                
            }

            Console.WriteLine("The Files are identical. Exiting Program.");
            Console.ReadKey();

        }

        public static void ReadFileToCharArray(char[] chArr, string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    chArr[i] = (char)sr.Read();
                    i++;
                }
            }
        }

    }
}

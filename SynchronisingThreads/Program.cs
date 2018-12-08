using System;
using System.Collections.Generic;
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
        private static char ch;

        static void Main(string[] args)
        {
            Console.WriteLine("Press a Key to change the recurring character.");
            ch = 'A';
            var printer = new Thread(new ThreadStart(PrintCh));
            var reader = new Thread(new ThreadStart(ReadKey));

            printer.Start();
            reader.Start();
            
        }

        private static void PrintCh()
        {
            while (true)
            {
                Console.Write(ch);
                System.Threading.Thread.Sleep(500);
            }
        }

        private static void ReadKey()
        {
            while (true)
            {
                ch = Console.ReadKey().KeyChar;
            }
        }

    }
}

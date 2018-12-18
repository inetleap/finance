using System;

namespace FinanceCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            for( var i=0; i < args.Length; i++)
            {

                Console.WriteLine(args[i]);
            }
            Console.ReadLine();
        }
    }
}

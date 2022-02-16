using System;

namespace regto
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("regto");
            Console.WriteLine("————————————————————————————————————————————\n");
            
            while ( !Process.Ask() ) ;
        }
    }
}

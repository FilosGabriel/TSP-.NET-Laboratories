using System;
using Laborator1;

namespace Laborator1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ex1();
            Console.WriteLine("exrcitiul 2");
            Ex2.Run2(246527);
            Console.WriteLine();
            
            Console.WriteLine("exrcitiul 3");
            Ex2.Run3(246527);
            Console.WriteLine();


            Console.WriteLine("exrcitiul 4");
            Ex2.Run4(246527);
            Console.WriteLine();

        }

        public static void ex1()
        {
            var s = new Message();
            s.AskMessageChanged += SAskMessageChanged;
            s.AskMessage = 100;
        }

        static void SAskMessageChanged(object sender,
            AskMessageChangedEventArgs e)
        {
            Console.WriteLine("Stoc display: {0}", e.AskMessage);
            //throw new NotImplementedException();
        }
    }
}
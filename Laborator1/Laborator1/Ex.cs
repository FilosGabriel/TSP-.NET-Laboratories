using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Laborator1
{
    public class Ex2
    {
        public static void Prime1(object nr)
        {
            var nr1 = (int) nr;
            _list.Add($"Start fir: 1 Timestamp: {DateTime.Now} Nr: {nr1}");
            var i = 2;
            while (!IsPrime(i) || i < nr1)
            {
                i++;
            }

            _list.Add($"End fir: 1 Timestamp: {DateTime.Now} NrPrime: {i}");
        }

        public static void Prime2(object nr)
        {
            var nr1 = (int) nr;
            _list.Add($"Start fir: 2 Timestamp: {DateTime.Now} Nr: {nr1}");
            var i = nr1 + 1;
            while (!IsPrime(i))
            {
                i++;
            }

            _list.Add($"End fir: 2 Timestamp: {DateTime.Now} NrPrime: {i}");
        }

        private static bool IsPrime(int nr)
        {
            if (nr == 2 || nr == 3)
                return true;
            for (var i = 3; i < nr; i++)
                if (nr % i == 0)
                    return false;
            return true;
        }

        public static void Run2(int nr)
        {
            _list.Clear();
            var t1 = new Thread(Prime1);
            var t2 = new Thread(Prime2);
            t1.Start(nr);
            t2.Start(nr);
            t1.Join();
            t2.Join();
            foreach (var date in _list)
            {
                Console.WriteLine(date);
            }
        }

        public static void DoWork1(object sender, DoWorkEventArgs args)
        {
            Prime1(args.Argument);
        }

        public static void DoWork2(object sender, DoWorkEventArgs args)
        {
            Prime2(args.Argument);
        }

        public static void Run3(int nr)
        {
            _list.Clear();
            var b1 = new BackgroundWorker()
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = false
            };
            var b3 = new BackgroundWorker()
            {
                WorkerReportsProgress = false,
                WorkerSupportsCancellation = false
            };
            b1.DoWork += DoWork1;
            b3.DoWork += DoWork2;
            // b1.RunWorkerCompleted += RunWorkerCompleted;
            // b3.RunWorkerCompleted += RunWorkerCompleted;
            b1.RunWorkerAsync(nr);
            b3.RunWorkerAsync(nr);
            while (true)
                if (b1.IsBusy || b3.IsBusy)
                    Thread.Sleep(1000);
                else
                    break;
            foreach (var date in _list)
            {
                Console.WriteLine(date);
            }
        }

        static void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // if (_list.Count != 4) return;
            foreach (var date in _list)
            {
                Console.WriteLine(date);
            }
        }

        static Ex2()
        {
            _list = new List<string>();
        }


        public static void Run4(int nr)
        {
            _list.Clear();
            var t1 = new Task(() => Prime1(nr));
            var t3 = new Task(() => Prime2(nr));
            t1.Start();
            t3.Start();
            t1.Wait();
            t3.Wait();
            foreach (var date in _list)
            {
                Console.WriteLine(date);
            }
        }

        private static List<string> _list;
    }
}
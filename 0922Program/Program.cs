using System;
using System.Threading;

namespace ThreadExample
{
    public class Program
    {
        private static Mutex mutex = new Mutex();
        static int num1 = 1, num2 = 1;
        static object locker = new object();
        private void Th1()
        {
            for (int i = 0; i < 1000; i++)
            {
                num2 = 1;
                lock (locker)
                {
                    if (num2 != 0)
                        Console.WriteLine(num1 / num2);
                    num2 = 0;
                }
            }
        }
        public static void Main()
        {
            Program app = new Program();
            app.MultiThread();
        }
        private void MultiThread()
        {
            Thread[] multiThread =
            {
                new Thread(new ThreadStart(Th1)),
                new Thread(new ThreadStart(Th1)),
                new Thread(new ThreadStart(Th1))
            };
            foreach (Thread t in multiThread)
            {
                t.Start();
                t.Join();
            }
        }
    }

}
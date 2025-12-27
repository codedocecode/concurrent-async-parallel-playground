using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Parallel.For(0, 8, i =>
        {
            Console.WriteLine($"CPU {i} en hilo {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(500);
        });
    }
}

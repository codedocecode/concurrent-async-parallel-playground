using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var items = Enumerable.Range(1, 5);

        await Parallel.ForEachAsync(items, new ParallelOptions { MaxDegreeOfParallelism = 3 }, async (i, ct) =>
        {
            ProcesarCPU(i);
            await GuardarAsync(i);
            Console.WriteLine($"Item {i} en hilo {Environment.CurrentManagedThreadId}");
        });
    }

    static void ProcesarCPU(int i)
    {
        Task.Delay(300).Wait();
    }

    static async Task GuardarAsync(int i)
    {
        await Task.Delay(300);
    }
}

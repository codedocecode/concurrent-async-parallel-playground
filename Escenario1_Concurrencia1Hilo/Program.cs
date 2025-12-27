using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Escenario 1: Concurrencia 1 hilo");

        await OperacionAsync("Red", 1000);
        await OperacionAsync("Disco", 500);
    }

    static async Task OperacionAsync(string nombre, int delay)
    {
        Console.WriteLine($"Inicio {nombre}");
        await Task.Delay(delay);
        Console.WriteLine($"Fin {nombre}");
    }
}

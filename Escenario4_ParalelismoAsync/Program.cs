using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Escenario 4: Paralelismo + async (CPU + I/O)\n");

        string[] tareas = { "Tarea1", "Tarea2", "Tarea3", "Tarea4" };

        // -------------------------------------
        // Caso único: Parallel.ForEachAsync
        // -------------------------------------
        Console.WriteLine("--- Caso: Parallel.ForEachAsync ---");

        // Cada tarea realiza CPU-bound y luego I/O async
        // Varias tareas ejecutándose al mismo tiempo, hilos físicos distintos
        // Hilo lógico principal controla la secuencia general
        await Parallel.ForEachAsync(tareas, async (tarea, ct) =>
        {
            // Parte CPU
            int resultado = CalculoPesado(tarea);

            // Parte I/O asincrónica
            await OperacionAsync(tarea, 500);

            Console.WriteLine($"{tarea} completada (Thread {Thread.CurrentThread.ManagedThreadId})");
        });

        Console.WriteLine("Paralelismo + async completado");
    }

    static int CalculoPesado(string nombre)
    {
        // Simula cálculo intensivo de CPU
        Console.WriteLine($"CPU {nombre} inicio (Thread {Thread.CurrentThread.ManagedThreadId})");

        int suma = 0;
        for (int i = 0; i < 5000000; i++)
            suma += i % 10;

        Console.WriteLine($"CPU {nombre} fin (Thread {Thread.CurrentThread.ManagedThreadId})");
        return suma;
    }

    static async Task OperacionAsync(string nombre, int delay)
    {
        // Simula operación I/O asincrónica
        Console.WriteLine($"I/O {nombre} inicio (Thread {Thread.CurrentThread.ManagedThreadId})");
        await Task.Delay(delay);
        Console.WriteLine($"I/O {nombre} fin (Thread {Thread.CurrentThread.ManagedThreadId})");
    }
}

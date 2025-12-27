class Program
{
    static async Task Main()
    {
        Console.WriteLine("Escenario 3: Concurrencia multihilo (async + ThreadPool)\n");

        // -------------------------------------
        // Caso 1: CPU-bound con Task.Run
        // -------------------------------------
        Console.WriteLine("--- Caso 1: Task.Run CPU-bound ---");

        // Iniciamos tareas CPU-bound en hilos del ThreadPool
        // Hilo principal inicia las tareas y puede continuar con otras operaciones
        Task tarea1 = Task.Run(() => CalculoPesado("Tarea1"));
        Task tarea2 = Task.Run(() => CalculoPesado("Tarea2"));

        // Espera a que terminen todas las tareas
        await Task.WhenAll(tarea1, tarea2);
        Console.WriteLine("Task.Run completado\n");

        // -------------------------------------
        // Caso 2: I/O-bound con async
        // -------------------------------------
        Console.WriteLine("--- Caso 2: Tareas I/O async ---");

        // Ejecuta varias operaciones I/O simultáneamente
        // Hilo lógico principal sigue controlando la secuencia, pero cada operación puede
        // continuar en diferentes hilos cuando finaliza el await
        await Task.WhenAll(
            OperacionAsync("Tarea1", 1000),
            OperacionAsync("Tarea2", 500),
            OperacionAsync("Tarea3", 1500)
        );

        Console.WriteLine("Concurrencia multihilo completa");
    }

    static void CalculoPesado(string nombre)
    {
        // Tarea CPU-bound
        Console.WriteLine($"Inicio CPU {nombre} (Thread {Thread.CurrentThread.ManagedThreadId})");

        int suma = 0;
        for (int i = 0; i < 10000000; i++)
            suma += i % 10;

        Console.WriteLine($"Fin CPU {nombre} (Thread {Thread.CurrentThread.ManagedThreadId})");
    }

    static async Task OperacionAsync(string nombre, int delay)
    {
        // Tarea I/O simulado
        Console.WriteLine($"Inicio I/O {nombre} (Thread {Thread.CurrentThread.ManagedThreadId})");
        await Task.Delay(delay);
        Console.WriteLine($"Fin I/O {nombre} (Thread {Thread.CurrentThread.ManagedThreadId})");
    }
}

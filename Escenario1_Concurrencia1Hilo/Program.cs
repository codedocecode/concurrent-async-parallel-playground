class Program
{
    static async Task Main()
    {
        Console.WriteLine("Escenario 1: Concurrencia lógica con un hilo (flujo lógico principal)\n");

        // -------------------------------------
        // Caso 1: Secuencial con await inmediato
        // -------------------------------------
        Console.WriteLine("--- Caso 1: Secuencial (await inmediato) ---");
        Console.WriteLine($"Antes del await: Thread {Thread.CurrentThread.ManagedThreadId}");

        // Cada tarea se espera antes de iniciar la siguiente
        // Hilo lógico principal controla la secuencia, aunque ThreadId físico puede cambiar
        await OperacionAsync("Tarea1", 1000);
        await OperacionAsync("Tarea2", 500);

        Console.WriteLine("Secuencial completo\n");

        // -------------------------------------
        // Caso 2: Concurrencia real con Task.WhenAll
        // -------------------------------------
        Console.WriteLine("--- Caso 2: Concurrencia real (Task.WhenAll) ---");
        Console.WriteLine($"Antes de WhenAll: Thread {Thread.CurrentThread.ManagedThreadId}");

        // Ejecuta varias tareas al mismo tiempo
        // Hilo lógico principal sigue controlando la secuencia, pero cada tarea puede
        // continuar en diferentes hilos del ThreadPool cuando finaliza el await
        await Task.WhenAll(
            OperacionAsync("Tarea1", 1000),
            OperacionAsync("Tarea2", 500),
            OperacionAsync("Tarea3", 1500)
        );

        Console.WriteLine("Concurrencia real completada\n");

        // -------------------------------------
        // Caso 3: Await retrasado - iniciar varias tareas y esperar al final
        // -------------------------------------
        Console.WriteLine("--- Caso 3: Await retrasado: iniciar tareas y esperar al final ---");
        Console.WriteLine($"Antes de await retrasado: Thread {Thread.CurrentThread.ManagedThreadId}");

        // Inicia varias tareas async sin esperar inmediatamente
        // Hilo lógico principal controla la secuencia
        Task tarea1 = OperacionAsync("Tarea1", 1000);
        Task tarea2 = OperacionAsync("Tarea2", 500);

        // Aquí se podrían realizar otras operaciones mientras las tareas avanzan

        // Espera manualmente a que terminen
        await tarea1;
        await tarea2;

        Console.WriteLine("Await retrasado completado\n");

        // -------------------------------------
        // Caso 4: Await intercalado - iniciar una tarea, await otra, luego await la primera
        // -------------------------------------
        Console.WriteLine("--- Caso 4: Await intercalado ---");
        Console.WriteLine($"Antes de await intercalado: Thread {Thread.CurrentThread.ManagedThreadId}");

        // Inicia la primera tarea async
        Task tarea3 = OperacionAsync("Tarea1", 1000);

        // Lanza otra tarea async y la espera inmediatamente
        await OperacionAsync("Tarea2", 500);

        // Ahora espera la primera tarea
        await tarea3;

        Console.WriteLine("Await intercalado completado");
    }

    /// <summary>
    /// Simula una operación asincrónica
    /// </summary>
    /// <param name="nombre">Nombre de la tarea</param>
    /// <param name="delay">Tiempo simulado de espera (ms)</param>
    static async Task OperacionAsync(string nombre, int delay)
    {
        // Antes del await: hilo que ejecuta la tarea
        Console.WriteLine($"Inicio {nombre} (Thread {Thread.CurrentThread.ManagedThreadId})");

        // Simula operación asincrónica
        await Task.Delay(delay);

        // Después del await: puede ejecutarse en un hilo diferente
        // Hilo lógico sigue siendo el mismo flujo de la tarea
        Console.WriteLine($"Fin {nombre} (Thread {Thread.CurrentThread.ManagedThreadId})");
    }
}

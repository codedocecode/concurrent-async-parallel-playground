class Program
{
    static void Main()
    {
        Console.WriteLine("Escenario 2: Paralelismo puro (CPU-bound, varios hilos)\n");

        int[] numeros = Enumerable.Range(1, 8).ToArray();

        // -------------------------------------
        // Caso 1: Parallel.For
        // -------------------------------------
        Console.WriteLine("--- Caso 1: Parallel.For ---");

        // Ejecuta un cálculo pesado en paralelo
        // Cada iteración puede ejecutarse en un hilo físico diferente
        // Hilo principal no espera secuencialmente, todas las iteraciones avanzan al mismo tiempo
        Parallel.For(0, numeros.Length, i =>
        {
            int resultado = CalculoPesado(i + 1);
            Console.WriteLine($"Tarea{i + 1} (Thread {Thread.CurrentThread.ManagedThreadId}) resultado: {resultado}");
        });

        Console.WriteLine("Parallel.For completado\n");

        // -------------------------------------
        // Caso 2: Parallel.ForEach
        // -------------------------------------
        Console.WriteLine("--- Caso 2: Parallel.ForEach ---");

        // Similar a Parallel.For, pero usando colección
        Parallel.ForEach(numeros, numero =>
        {
            int resultado = CalculoPesado(numero);
            Console.WriteLine($"Tarea{numero} (Thread {Thread.CurrentThread.ManagedThreadId}) resultado: {resultado}");
        });

        Console.WriteLine("Parallel.ForEach completado");
    }

    static int CalculoPesado(int id)
    {
        // Simula tarea CPU-bound
        Console.WriteLine($"Inicio CPU Tarea{id} (Thread {Thread.CurrentThread.ManagedThreadId})");

        int suma = 0;
        for (int i = 0; i < 10000000; i++)
            suma += i % 10;

        Console.WriteLine($"Fin CPU Tarea{id} (Thread {Thread.CurrentThread.ManagedThreadId})");
        return suma;
    }
}

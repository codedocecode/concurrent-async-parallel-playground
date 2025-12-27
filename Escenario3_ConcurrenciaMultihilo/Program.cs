using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var urls = new[]
        {
            "https://jsonplaceholder.typicode.com/posts/1",
            "https://jsonplaceholder.typicode.com/posts/2",
            "https://jsonplaceholder.typicode.com/posts/3"
        };

        var tasks = urls.Select(DescargarAsync);
        await Task.WhenAll(tasks);
    }

    static async Task DescargarAsync(string url)
    {
        using var client = new HttpClient();
        var data = await client.GetStringAsync(url);
        Console.WriteLine($"Descargado {url[^1]} en hilo {Environment.CurrentManagedThreadId}");
    }
}

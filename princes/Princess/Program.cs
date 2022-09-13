using Nsu.Princess.Model;
using Nsu.Princess.Services;

namespace Nsu.Princess;

public class Program
{
    public static void Main(string[] args)
    {
        List<Contender> generatedContenders = new ContenderGenerator().GenerateContenders(100);
        foreach (var contender in generatedContenders)
        {
            Console.WriteLine(contender);
        }
        //CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Services.Princess>();
                services.AddScoped<Hall>();
                services.AddScoped<Friend>();
                services.AddScoped<ContenderGenerator>();
            });
    }
}
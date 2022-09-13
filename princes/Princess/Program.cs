using Nsu.Princess.Model;
using Nsu.Princess.Services;

namespace Nsu.Princess;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Services.Princess>();
                services.AddScoped<IHall, Hall>();
                services.AddScoped<IFriend, Friend>();
                services.AddSingleton<IContenderGenerator, ContenderGenerator>();
            });
    }
}
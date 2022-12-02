using Auto.Data.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace Auto.SignalRClient;

public static class Program
{
    private const string SIGNALR_HUB_URL = "http://localhost:5000/hub";
    private static HubConnection hub;

    public static async Task Main(string[] args)
    {
        hub = new HubConnectionBuilder().WithUrl(SIGNALR_HUB_URL).Build();
        await hub.StartAsync();
        Console.WriteLine("Hub started!");
        Console.WriteLine("Press any key to send a message (Ctrl-C to quit)");
        var i = 0;
        while (true)
        {
            var input = Console.ReadLine();
            var message = JsonConvert.SerializeObject(new Vehicle
            {
                Registration = "AB19NWG",
                ModelCode = "TestModel",
                Color = "TestColor",
                Year = 10,
                VehicleModel = null
            });
            await hub.SendAsync("NotifyWebUsers", "Auto.Notifier", message);
            Console.WriteLine($"Sent: {message}");
        }
    }
}
using System.Text.Json;
using System.Text;
using System.Runtime.InteropServices;

class Program
{
    public static async Task Main()
    {
        Config appConfiguration;
        Discord.Activity activity;

        long StartTimeUnix = DateTimeOffset.Now.ToUnixTimeSeconds();

        using (FileStream fs = new FileStream("config.json", FileMode.Open))
        {
            appConfiguration = await JsonSerializer.DeserializeAsync<Config>(fs);
        }
        var discord = new Discord.Discord(appConfiguration.appId, (UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();

        activity = new Discord.Activity
        {
            State = "(потратил 7 часов на статус)",
            Details = "прям ща - asp.net",
            Timestamps =
            {
                Start = StartTimeUnix,
            },
            Assets =
            {
                LargeImage = "dotnet", // Larger Image Asset Value
                LargeText = "powered by microsoft", // Large Image Tooltip
                SmallImage = "csharp", // Small Image Asset Value
                SmallText = "just C#", // Small Image Tooltip
            },

            Instance = true,
        };
        


        activityManager.UpdateActivity(activity, (result) =>
        {
            if (result == Discord.Result.Ok)
            {
                 Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        });

        while (true)
        {
            discord.RunCallbacks();
            Console.WriteLine("loop");
            Thread.Sleep(1000); 
        }
    }

    record struct Config(long appId);
}

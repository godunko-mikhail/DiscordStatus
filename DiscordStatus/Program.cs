using System.Text.Json;
using System.Text;
using System.Runtime.InteropServices;
class Program
{
    public static async Task Main()
    {
        Config appConfiguration;
        Discord.Activity activity1;
        Discord.Activity activity2;

        activity1 = await JsonActivityToDiscord("activity.json");

        long StartTimeUnix = DateTimeOffset.Now.ToUnixTimeSeconds();

        using (FileStream fs = new FileStream("config.json", FileMode.Open))
        {
            appConfiguration = await JsonSerializer.DeserializeAsync<Config>(fs);
        }

        var discord = new Discord.Discord(appConfiguration.appId, (UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();

        activityManager.UpdateActivity(activity1, (result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Console.WriteLine("Success!" + DateTimeOffset.Now.ToString());
            }
            else
            {
                Console.WriteLine("Failed");
            }
        });

        while (true)
        {
            activity2 = await JsonActivityToDiscord("activity.json");
            if(Equals(activity2, activity1))
            {
                
            }
            else
            {
                activityManager.UpdateActivity(activity2, (result) =>
                {
                    if (result == Discord.Result.Ok)
                    {
                        Console.WriteLine("Success!" + DateTimeOffset.Now.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Failed");
                    }
                });
                activity1 = activity2;
            }


            discord.RunCallbacks();
            Thread.Sleep(10000); 
        }

        async Task<Discord.Activity> JsonActivityToDiscord(string jsonPath)
        {
            Activity jsonActivity = new Activity();

            using (FileStream fs = new FileStream(jsonPath, FileMode.Open))
            {
                jsonActivity = await JsonSerializer.DeserializeAsync<Activity>(fs);
            }
            return new Discord.Activity
            {
                Type = (Discord.ActivityType)jsonActivity.Type,
                ApplicationId = jsonActivity.ApplicationId,
                Name = jsonActivity.Name,
                State = jsonActivity.State,
                Details = jsonActivity.Details,
                Timestamps =
                {
                    Start = jsonActivity.Timestamps.Start,
                    End = jsonActivity.Timestamps.End
                },
                Assets =
                {
                    LargeImage = jsonActivity.Assets.LargeImage,
                    LargeText = jsonActivity.Assets.LargeText,
                    SmallImage = jsonActivity.Assets.SmallImage,
                    SmallText = jsonActivity.Assets.SmallText,
                },
                Party =
                {
                    Id = jsonActivity.Party.Id,
                    Size =
                    {
                        CurrentSize = jsonActivity.Party.Size.CurrentSize,
                        MaxSize = jsonActivity.Party.Size.MaxSize
                    }
                },
                Secrets =
                {
                    Join = jsonActivity.Secrets.Join,
                    Match = jsonActivity.Secrets.Match,
                    Spectate = jsonActivity.Secrets.Spectate,
                },
                Instance = jsonActivity.Instance,

            };
        }
    }


    record struct Config(long appId);
}

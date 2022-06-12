using Newtonsoft.Json;

class Program
{
    public static async Task Main()
    {
        Config appConfiguration;
        Discord.Activity activity1;
        Discord.Activity activity2;

        activity1 = JsonActivityToDiscord("activity.json");

        long StartTimeUnix = DateTimeOffset.Now.ToUnixTimeSeconds();

        using (StreamReader fs = new StreamReader("config.json"))
        {
            var json = fs.ReadToEnd();
            appConfiguration = JsonConvert.DeserializeObject<Config>(json);
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
            activity2 = JsonActivityToDiscord("activity.json");
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

        Discord.Activity JsonActivityToDiscord(string jsonPath)
        {
            string jsonActivity;

            using (StreamReader fs = new StreamReader(jsonPath))
            {
                jsonActivity = fs.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<Discord.Activity>(jsonActivity);
        }
    }


    record struct Config(long appId);
}

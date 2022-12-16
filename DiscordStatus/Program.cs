using Newtonsoft.Json;

class Program
{
    public static async Task Main()
    {
        Config appConfiguration;
        Discord.Activity activity1;
        Discord.Activity activity2;
        Discord.Activity resultActivity;
        activity1 = JsonActivityToDiscord("activity.json");

        long StartTimeUnix = DateTimeOffset.Now.ToUnixTimeSeconds();

        var discord = new Discord.Discord(985112684771037236, (UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();

        resultActivity = activity1;
        resultActivity.Timestamps.Start = StartTimeUnix;

        activityManager.UpdateActivity(resultActivity, (result) =>
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
            if(!Equals(activity2, activity1))
            {
                resultActivity = activity2;
                resultActivity.Timestamps.Start = StartTimeUnix;
                activityManager.UpdateActivity(resultActivity, (result) =>
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

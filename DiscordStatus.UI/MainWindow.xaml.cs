﻿using Discord;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DiscordStatus.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InputModel? inputModel;

        private Discord.Discord? _discord;
        private Discord.Discord discord
        {
            get
            {
                return _discord ??= new Discord.Discord(long.Parse(inputModel.AppId ?? "0"), (UInt64)Discord.CreateFlags.Default);
            }
        }


        private DispatcherTimer timer;
        private string status;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
            Closed += stopTimer;
            Closed += saveData;
            try
            {
                inputModel = JsonConvert.DeserializeObject<InputModel?>(File.ReadAllText("backup.json"));
            }
            catch
            {
                inputModel = new InputModel();
            }

            this.DataContext = inputModel;
        }
        public void callback(object sender, EventArgs e)
        {
            try
            {
                discord.RunCallbacks();
                if (inputModel != null)
                {
                    inputModel.Status = "Done.";
                }
            }
            catch (Exception ex)
            {
                if (inputModel != null)
                {
                    inputModel.Status = ex.Message;
                }
            }
        }
        public void stopTimer(object sender, EventArgs e)
        {
            timer.Stop();
        }
        public void saveData(object sender, EventArgs e)
        {
            File.WriteAllText("backup.json", JsonConvert.SerializeObject(new InputModel(inputModel)));
        }

        private void Instruction_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://github.com/godunko-mikhail/DiscordStatus/blob/master/README.md");
        }

        private void SetStatus_Click(object sender, RoutedEventArgs e)
        {
            var activityManager = discord.GetActivityManager();

            var activity = new Activity()
            {
                ApplicationId = long.Parse(inputModel.AppId ?? "0"),
                Type = ActivityType.Playing,
                Name = "",
                State = inputModel.State ?? "",
                Details = inputModel.Details ?? "",
                Timestamps = new ActivityTimestamps()
                {
                    Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    End = 0
                },
                Assets = new ActivityAssets()
                {
                    LargeImage = inputModel.LargeImage ?? "",
                    LargeText = inputModel.LargeText ?? "",
                    SmallImage = inputModel.SmallImage ?? "",
                    SmallText = inputModel.SmallText ?? "",
                },
                Party = new ActivityParty()
                {
                    Id = "",
                    Size = new PartySize()
                    {
                        CurrentSize = 0,
                        MaxSize = 0
                    }
                },
                Secrets = new ActivitySecrets()
                {
                    Join = "",
                    Match = "",
                    Spectate = ""
                },
                Instance = true
            };

            activityManager.UpdateActivity(activity, (result) =>
            {
                inputModel.Status = result.ToString();
            });
            timer.Tick -= callback;
            timer.Tick += callback;
        }

        private void CustomAppId_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            inputModel.AppId = textBox.Text;
        }
        private void CustomState_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            inputModel.State = textBox.Text;
        }
        private void CustomDetails_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            inputModel.Details = textBox.Text;
        }
        private void LargeTextData_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            inputModel.LargeText = textBox.Text;
        }
        private void LargeImageData_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            inputModel.LargeImage = textBox.Text;
        }
        private void SmallImageData_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            inputModel.SmallImage = textBox.Text;
        }
        private void SmallTextData_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            inputModel.SmallText = textBox.Text;
        }
    }
}

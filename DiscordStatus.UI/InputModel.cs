using Discord;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DiscordStatus.UI
{
    public class InputModel : INotifyPropertyChanged
    {
        private string status = "";

        public InputModel(InputModel input)
        {
            AppId = input.AppId;
            LargeImage = input.LargeImage;
            LargeText = input.LargeText;
            SmallImage = input.SmallImage;
            SmallText = input.SmallText;
            Details = input.Details;
            State = input.State;
            Status = "";
        }
        public InputModel()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;


        public string? AppId { get; set; }

        public string? LargeImage { get; set; }
        public string? LargeText { get; set; }
        public string? SmallImage { get; set; }
        public string? SmallText { get; set; }

        public string? Details { get; set; }
        public string? State { get; set; }

        public string Status { 
            get { return status; } 
            set 
            {
                status = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

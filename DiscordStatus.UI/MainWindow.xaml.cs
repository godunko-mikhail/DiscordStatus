using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiscordStatus.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? AppId { get; set; }

        private string? LargeImage { get; set; }
        private string? LargeText { get; set; }
        private string? SmallImage { get; set; }
        private string? SmallText { get; set; }
        
        private string? Details { get; set; }
        private string? State { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Instruction_Click(object sender, RoutedEventArgs e)
        {
            //redirect to git
        }

        private void AppId_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}

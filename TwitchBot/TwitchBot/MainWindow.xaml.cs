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

namespace TwitchBot
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            var win = new TwitchBotWin();
            win.Show();
            this.Close();
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Login()
        {
            if (tb_ChannelName.Text.Length == 0)
                return;
            if (tb_Passwort.Text.Length == 0)
                return;
            if (tb_UserName.Text.Length == 0)
                return;

            IcrTwitch twitchAccount = new IcrTwitch("icr.twitch", 6667,
                                                    tb_UserName.Text, tb_Passwort.Text, tb_ChannelName.Text);

        }
    }
}

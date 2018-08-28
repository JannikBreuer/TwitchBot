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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TwitchBot
{
    /// <summary>
    /// Interaktionslogik für TwtichWin.xaml
    /// </summary>
    public partial class TwitchBotWin : Window
    {
        private string imagePfadSelectedBtn = @"D:\GitHub\TwitchBot\TwitchBot\UserListBackGroundImageSelected.jpg";
        private string imagePfadBtn = @"D:\GitHub\TwitchBot\TwitchBot\UserListBackroundNormal.jpg";

        public TwitchApi apiClass = new TwitchApi();
        public IcrTwitch twitchClient = new IcrTwitch("irc.twitch.tv", 6667,
                                                    "jnkstv", "oauth:9fxcnrikonai5w0vgt0ggaz6bj2pwy", "sentiolive");
        public static TwitchBotWin winRef;
        public TwitchBotWin()
        {
            InitializeComponent();
            winRef = this;
            grid_UserList.Visibility = Visibility.Visible;
            grid_Message.Visibility = Visibility.Collapsed;
            AddNewUserToStackPanel("Jannik", "Test");
        }
        private void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as Grid).Name == "grid_Button1")
            {
                img_UserList.Source = new BitmapImage(new Uri(imagePfadSelectedBtn));
                btn_UserList.Background = ConvertHexIntoBrush("#FF37568B");
                grid_Button1.Background = ConvertHexIntoBrush("#FF37568B");
            }
            else if ((sender as Grid).Name == "grid_Button2")
            {
                img_Messages.Source = new BitmapImage(new Uri(imagePfadSelectedBtn));
                btn_Messages.Background = ConvertHexIntoBrush("#FF37568B");
                grid_Button2.Background = ConvertHexIntoBrush("#FF37568B");
            }
            else if ((sender as Grid).Name == "grid_Button3")
            {
                img_Roulette.Source = new BitmapImage(new Uri(imagePfadSelectedBtn));
                btn_Roulette.Background = ConvertHexIntoBrush("#FF37568B");
                grid_Button3.Background = ConvertHexIntoBrush("#FF37568B");
            }
        }
        private void grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((sender as Grid).Name == "grid_Button1")
            {
                img_UserList.Source = new BitmapImage(new Uri(imagePfadBtn));
                btn_UserList.Background = ConvertHexIntoBrush("#FF3F3F46");
                grid_Button1.Background = ConvertHexIntoBrush("#FF3F3F46");
            }
            else if ((sender as Grid).Name == "grid_Button2")
            {
                img_Messages.Source = new BitmapImage(new Uri(imagePfadBtn));
                btn_Messages.Background = ConvertHexIntoBrush("#FF3F3F46");
                grid_Button2.Background = ConvertHexIntoBrush("#FF3F3F46");
            }
            else if ((sender as Grid).Name == "grid_Button3")
            {
                img_Roulette.Source = new BitmapImage(new Uri(imagePfadBtn));
                btn_Roulette.Background = ConvertHexIntoBrush("#FF3F3F46");
                grid_Button3.Background = ConvertHexIntoBrush("#FF3F3F46");
            }
        }
        private void btn_ClickShowUserListGrid(object sender, EventArgs e)
        {
            grid_Message.Visibility = Visibility.Collapsed;
            grid_UserList.Visibility = Visibility.Visible;
        }
        private void btn_ClickShowMessageGrid(object sender, EventArgs e)
        {
            grid_UserList.Visibility = Visibility.Collapsed;
            grid_Message.Visibility = Visibility.Visible;
        }
        private void btn_ClickShow(object sender, EventArgs e)
        {
            grid_Button3.Visibility = Visibility.Collapsed;
            grid_Button2.Visibility = Visibility.Collapsed;
            grid_Button1.Visibility = Visibility.Visible;
        }
        private void btn_ClickRefresh(object sender, EventArgs e)
        {
            //Refresh UserList
        }
        private Brush ConvertHexIntoBrush(string hexCode)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(hexCode);
            return brush;
        }
        /*
         * 
         *              <StackPanel Orientation="Horizontal" Height="30" Background="#FF035AA2" >
                            <Label Content="Name: Jannik"/>
                            <Label Margin="400,0,0,0" Content="Date: 21.08.2017"/>
                        </StackPanel>
                        <Label Margin="50,-1,0,0" Content="Message: Das ist eine Message." Background="#FF035AA2"/>
                        <StackPanel Orientation="Horizontal" Height="30" Background="CornflowerBlue" Margin="0,10,0,0" >
                            <Label Content="Jannik" Foreground="WhiteSmoke" />
                            <Label Margin="465,0,0,0" Content="21.08.2017" Foreground="WhiteSmoke"/>
                        </StackPanel>
                        <Label Margin="50,-1,0,0" Content="Das ist eine Message." Background="#FF035AA2" Foreground="WhiteSmoke"/>
        */

        public void AddNewMessageToStackPanel(string username, string message, string date)
        {
            Dispatcher.Invoke(() =>
            {
                StackPanel panel = new StackPanel();
                panel.Height = 30;
                panel.VerticalAlignment = VerticalAlignment.Top;
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(0, 1, 0, 0);
                panel.Background = new SolidColorBrush(Colors.CornflowerBlue);

                Label lb_Username = new Label();
                lb_Username.Content = username;
                lb_Username.Foreground = new SolidColorBrush(Colors.Red);
                Label lb_Date = new Label();
                lb_Date.Content = date;
                lb_Username.HorizontalAlignment = HorizontalAlignment.Right;
                lb_Date.HorizontalAlignment = HorizontalAlignment.Left;
                lb_Date.Margin = new Thickness(465, 0, 0, 0);
                lb_Date.Foreground = new SolidColorBrush(Colors.Red);

                panel.Children.Add(lb_Username);
                panel.Children.Add(lb_Date);

                Label lb_Message = new Label();
                lb_Message.Content = "Message: " + message;
                lb_Message.Margin = new Thickness(50, -1, 0, 0);
                lb_Message.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
                lb_Message.Background = new SolidColorBrush(Colors.CornflowerBlue);

                if (stackPanel_Message.Children.Count > 0)
                    panel.Margin = new Thickness(0, 10, 0, 0);

                stackPanel_Message.Children.Add(panel);
                stackPanel_Message.Children.Add(lb_Message);
            });
        }
        public void AddNewUserToStackPanel(string userName, string upTime)
        {
            /*
             *             <Border BorderThickness="2" BorderBrush="DarkBlue" Margin="1,3,0,0">
                        <StackPanel Orientation="Horizontal" Height="30"  >
                            <Label Content="Username"/>
                                <Label Margin="440,0,0,0" Content="OnlineTime"/>
                            </StackPanel>
                        </Border>
             */
            Dispatcher.Invoke(() =>
            {
                Border border = new Border();
                border.BorderThickness = new Thickness(1);
                border.BorderBrush = new SolidColorBrush(Colors.DarkBlue);
                border.Margin = new Thickness(1,3,0,0);
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.Height = 30;
                panel.Margin = new Thickness(1,1,0,0);
                Label lb_Username = new Label();
                lb_Username.Content = userName;
                Label lb_DateTime = new Label();
                lb_DateTime.Content = "10h";
                lb_DateTime.Margin = new Thickness(440,0,0,0);

                panel.Children.Add(lb_Username);
                panel.Children.Add(lb_DateTime);

                stackPanel_UserList.Children.Add(panel);
                stackPanel_UserList.Children.Add(border);
            });
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
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
        private string imagePfadSelectedBtn = @".\Resources\UserListBackGroundImageSelected.jpg";
        private string imagePfadBtn = @".\Resources\UserListBackGroundNormal.jpg";


        private UserListClass userListClass = new UserListClass();
        private TwitchApi apiClass = new TwitchApi();
        private TwitchBotClient twitchClient = new TwitchBotClient("jnkstv", File.ReadAllText(@"C:\Users\j.breuer\Documents\Pw\TwitchOAuthToken.txt") , "sentiolive");


        public static TwitchBotWin winRef;
        public TwitchBotWin()
        {
            InitializeComponent();
            winRef = this;
            grid_UserList.Visibility = Visibility.Visible;
            grid_Message.Visibility = Visibility.Collapsed;
        }
        #region get + setter
        public TwitchApi GetApiClass()
        {
            return this.apiClass;
        }
        public UserListClass GetUserListClass()
        {
            return this.userListClass;
        }
        public TwitchBotClient GetTwitchBotClient()
        {
            return this.twitchClient;
        }
        #endregion
        #region UiEvents

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
        #endregion
        private Brush ConvertHexIntoBrush(string hexCode)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(hexCode);
            return brush;
        }
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
        //Grapic Part
        public void AddNewUserToStackPanel(string userName) // vlt später noch die Punkte und ob er Sub oder Follower ist
        {
            Dispatcher.Invoke(() =>
            {
                ListBoxItem lb_ItemUserName = new ListBoxItem();
                lb_ItemUserName.Content = userName;

                lb_ItemUserName.BorderThickness = new Thickness(1);
                lb_ItemUserName.Height = 30;
                lb_ItemUserName.Foreground = new SolidColorBrush(Colors.WhiteSmoke);

                Image kickImage = new Image();
                kickImage.Source = new BitmapImage(new Uri(@".\Resource\KickUser.png"));
                kickImage.Height = 30;

                Image timeOutImage = new Image();
                timeOutImage.Source = new BitmapImage(new Uri(@".\Resource\timeOut.png"));
                timeOutImage.Height = 30;

                Image informationImage = new Image();
                timeOutImage.Source = new BitmapImage(new Uri(@".\Resource\timeOut.png"));
                timeOutImage.Height = 30;

                Image bannImage = new Image();
                bannImage.Source = new BitmapImage(new Uri(@".\Resource\timeOut.png"));
                bannImage.Height = 30;

                lbox_BannUser.Items.Add(bannImage);
                lbox_Information.Items.Add(informationImage);
                lbox_TimeOutUser.Items.Add(timeOutImage);
                lbox_KickUser.Items.Add(kickImage);
                lbox_Username.Items.Add(lbox_Username);
            });
        }
    }
}

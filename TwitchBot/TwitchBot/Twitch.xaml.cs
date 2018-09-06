using System;
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
        private string imagePfadSelectedBtn = @"D:\GitHub\TwitchBot\Resources\UserListBackGroundImageSelected.jpg";
        private string imagePfadBtn = @"D:\GitHub\TwitchBot\Resources\UserListBackroundNormal.jpg";


        private MediaElement loadingGifPlayer;

        private const string COLORSELECTEDBTN = "#FF37568B";
        private const string COLORUNSELECTEDBTN = "#FF3F3F46";

        private readonly StreamInformation infoAboutStreamClass;
        private readonly UserListClass userListClass;
        private readonly TwitchApi apiClass;
        private readonly TwitchBotClient twitchClient;

        private object lockObj = new object();

        public static TwitchBotWin WinRef { get; private set; }

        public TwitchBotWin()
        {
            InitializeComponent();
            WinRef = this;
            userListClass = new UserListClass();
            BindingOperations.EnableCollectionSynchronization(userListClass.UserList, lockObj);
            dataGrid_UserList.ItemsSource = userListClass.UserList;
            grid_UserList.Visibility = Visibility.Visible;
            grid_Message.Visibility = Visibility.Collapsed;
            apiClass = new TwitchApi();
            infoAboutStreamClass = new StreamInformation();
            twitchClient = new TwitchBotClient("jnkstv", File.ReadAllText(@"C:\Users\Janniks-Pc\Documents\Pw\TwitchOAuthToken.txt"), "SentioLIVE");
        }
        #region get + setter
        public StreamInformation GetStreamInformationClass()
        {
            return this.infoAboutStreamClass;
        }
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


        private UserEintrag GetDataClassOfSend(object sender)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                ContextMenu cm = mi.CommandParameter as ContextMenu;
                if (cm != null)
                {
                    DataGridRow gridRow = cm.PlacementTarget as DataGridRow;
                    if (gridRow != null)
                    {
                        if (gridRow.Item is UserEintrag)
                        {
                            UserEintrag user = gridRow.Item as UserEintrag;
                            return user;
                        }
                    }
                }
            }
            return null;
        }
        #region UiEvents
        private void MenuItemKickUser_Click(object sender, RoutedEventArgs e)
        {
           var user = GetDataClassOfSend(sender);
            if(user != null)
                Console.WriteLine("Kick user: " + user.UserName);
        }
        private void MenuItemTimeOutUser_Click(object sender, RoutedEventArgs e)
        {
            var user = GetDataClassOfSend(sender);
            if (user != null)
                Console.WriteLine("Time out user: " + user.UserName);
        }
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as Grid).Name == "grid_Button1")
            {
                img_UserList.Source = new BitmapImage(new Uri(imagePfadSelectedBtn));
                btn_UserList.Background = ConvertHexIntoBrush(COLORSELECTEDBTN);
                grid_Button1.Background = ConvertHexIntoBrush(COLORSELECTEDBTN);
            }
            else if ((sender as Grid).Name == "grid_Button2")
            {
                img_Messages.Source = new BitmapImage(new Uri(imagePfadSelectedBtn));
                btn_Messages.Background = ConvertHexIntoBrush(COLORSELECTEDBTN);
                grid_Button2.Background = ConvertHexIntoBrush(COLORSELECTEDBTN);
            }
            else if ((sender as Grid).Name == "grid_Button3")
            {
                img_Roulette.Source = new BitmapImage(new Uri(imagePfadSelectedBtn));
                btn_Roulette.Background = ConvertHexIntoBrush(COLORSELECTEDBTN);
                grid_Button3.Background = ConvertHexIntoBrush(COLORSELECTEDBTN);
            }
        }
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((sender as Grid).Name == "grid_Button1")
            {
                img_UserList.Source = new BitmapImage(new Uri(imagePfadBtn));
                btn_UserList.Background = ConvertHexIntoBrush(COLORUNSELECTEDBTN);
                grid_Button1.Background = ConvertHexIntoBrush(COLORUNSELECTEDBTN);
            }
            else if ((sender as Grid).Name == "grid_Button2")
            {
                img_Messages.Source = new BitmapImage(new Uri(imagePfadBtn));
                btn_Messages.Background = ConvertHexIntoBrush(COLORUNSELECTEDBTN);
                grid_Button2.Background = ConvertHexIntoBrush(COLORUNSELECTEDBTN);
            }
            else if ((sender as Grid).Name == "grid_Button3")
            {
                img_Roulette.Source = new BitmapImage(new Uri(imagePfadBtn));
                btn_Roulette.Background = ConvertHexIntoBrush(COLORUNSELECTEDBTN);
                grid_Button3.Background = ConvertHexIntoBrush(COLORUNSELECTEDBTN);
            }
        }
        private void Btn_ClickShowUserListGrid(object sender, EventArgs e)
        {
            grid_Message.Visibility = Visibility.Collapsed;
            grid_UserList.Visibility = Visibility.Visible;
        }
        private void Btn_ClickShowMessageGrid(object sender, EventArgs e)
        {
            grid_UserList.Visibility = Visibility.Collapsed;
            grid_Message.Visibility = Visibility.Visible;
        }
        private void Btn_ClickShow(object sender, EventArgs e)
        {
            grid_Button3.Visibility = Visibility.Collapsed;
            grid_Button2.Visibility = Visibility.Collapsed;
            grid_Button1.Visibility = Visibility.Visible;
        }
        private void Btn_ClickRefresh(object sender, EventArgs e)
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
        public void RefreshCountLabels()
        {
            Dispatcher.Invoke(() =>
            {
                lb_FollowerCount.Content = "Followercount: " + userListClass.GetFollowerCount(); ;
                lb_nonFollowerCount.Content = "Non Follower count: " + userListClass.GetCurrentNonFollowerInChat(); ;
                lb_SubsCount.Content = "Subcount: " + userListClass.GetSubCount();
                lb_ViewerCount.Content = "Viewercount: " + userListClass.GetCurrentViewerCount();
            });
        }
            public void AddNewMessageToStackPanel(string username, string message, string date, Color color)
        {
            Dispatcher.Invoke(() =>
            {
                StackPanel panel = new StackPanel();
                panel.Height = 30;
                panel.VerticalAlignment = VerticalAlignment.Top;
                panel.Orientation = Orientation.Horizontal;
                panel.Margin = new Thickness(0, 1, 0, 0);

                Label lb_Username = new Label();
                lb_Username.Content = username;
                lb_Username.Foreground = new SolidColorBrush(color);
                Label lb_Date = new Label();
                lb_Date.Content = date;
                lb_Username.HorizontalAlignment = HorizontalAlignment.Right;
                lb_Date.HorizontalAlignment = HorizontalAlignment.Left;
                lb_Date.Margin = new Thickness(465, 0, 0, 0);
                lb_Date.Foreground = new SolidColorBrush(color);

                panel.Children.Add(lb_Username);
                panel.Children.Add(lb_Date);

                Label lb_Message = new Label();
                lb_Message.Content = "Message: " + message;
                lb_Message.Margin = new Thickness(50, -1, 0, 0);
                lb_Message.Foreground = new SolidColorBrush(color);

                if (stackPanel_Message.Children.Count > 0)
                    panel.Margin = new Thickness(0, 10, 0, 0);

                stackPanel_Message.Children.Add(panel);
                stackPanel_Message.Children.Add(lb_Message);
            });
        }
        public void CreatLoadingAndAddItToUIserListGrid()
        {
            loadingGifPlayer = new MediaElement();
            loadingGifPlayer.Source = new Uri("");
            loadingGifPlayer.Height = 200;
            loadingGifPlayer.Width = 200;
            dataGrid_UserList.Items.Add(loadingGifPlayer);
        }
        public void DeletLoadingImage()
        {
            dataGrid_UserList.Items.Remove(loadingGifPlayer);
        }
        private void OnWinIsLoaded(object sender, RoutedEventArgs e)
        {
            dataGrid_UserList.Columns[0].Header = "Username";
            dataGrid_UserList.Columns[1].Header = "Userstate";
            dataGrid_UserList.Columns[2].Header = "Seit";
            dataGrid_UserList.Columns[3].Header = "Punkte";
            dataGrid_UserList.Columns[4].Header = "UserID";
            dataGrid_UserList.Columns[4].Header = "WatchTime";
        }
    }
}

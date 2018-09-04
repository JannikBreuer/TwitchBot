using System;
using System.Net.Sockets;
using System.IO;
using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;
using System.Threading;

namespace TwitchBot
{
    public class TwitchBotClient
    {

        private ConnectionCredentials credentials;

        private readonly TwitchClient client;
        private string _userName;
        private string _channelName;
        private Thread addUsersToListThread;
        public TwitchBotClient(string userName, string password, string channelName)
        {
            _userName = userName;
            _channelName = channelName;
            addUsersToListThread = new Thread(FillUserList);
            addUsersToListThread.Start();
            credentials = new ConnectionCredentials("218744916", "gjhi4j2o6npoab4saq4xe7yjhc08fp");
            client = new TwitchClient();
            
            client.Initialize(credentials, channelName);
            client.ChatThrottler = new TwitchLib.Client.Services.MessageThrottler(client, 20, TimeSpan.FromSeconds(30));
            client.WhisperThrottler = new TwitchLib.Client.Services.MessageThrottler(client, 20, TimeSpan.FromSeconds(30));
            client.ChatThrottler.StartQueue();
            client.OnJoinedChannel += OnJoinedChannel;
            client.OnMessageReceived += onMessageReceived;
            client.OnWhisperReceived += onWhisperReceived;
            client.OnNewSubscriber += onNewSubscriber;
            client.OnConnected += Client_OnConnected;
            client.OnBeingHosted += Client_OnBeingHosted;
            client.OnReSubscriber += Client_OnReSubscriber;
            client.OnUserJoined += Client_OnUserJoined;
            client.OnUserLeft += Client_OnUserLeft;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.AddChatCommandIdentifier('!');

            //irgendwie muss ich noch an die unfollower und unsubs dran kommen 
            try
            {
                client.Connect();
            }
            catch(Exception e)
            {
                Console.WriteLine("Verbindung konnte nicht hergestellt werden. Exception: " + e);
                //DO something (informate the user that he isnt connected with the channel ....)
            }
        }

        private void Client_OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            string parameter;

            parameter = e.Command.ArgumentsAsString;

            if (e.Command.ArgumentsAsString.Contains(" "))
            {
                var split = e.Command.ArgumentsAsString.Split(' ');
                parameter = split[0];
            }


            Commands.IdentifyCommand(e.Command.CommandText, parameter);
            Console.WriteLine("Recieved a command! "  + e.Command.CommandText + "  " + e.Command.ArgumentsAsString);

        }

        private void FillUserList()
        {
            //Play loading animation
            TwitchBotWin.winRef.CreatLoadingAndAddItToUIserListGrid();
            TwitchBotWin.winRef.GetUserListClass().AddUsersToList(TwitchBotWin.winRef.GetApiClass().GetFilledUserListWithAllInformation());
            var followerAndSubCount = TwitchBotWin.winRef.GetUserListClass().GetFollowerCount() + TwitchBotWin.winRef.GetUserListClass().GetSubCount();
            TwitchBotWin.winRef.GetUserListClass().SetCurrentNonFollowerViewerInChat(TwitchBotWin.winRef.GetUserListClass().GetCurrentViewerCount() - followerAndSubCount);
            TwitchBotWin.winRef.RefreshCountLabels();
            TwitchBotWin.winRef.DeletLoadingImage();
            //quit loading animatio
        }
        private void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            Console.WriteLine("The user " + e.Username + " left the room");
            TwitchBotWin.winRef.GetUserListClass().UserLefTheChannel(e.Username);
        }

        private void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            Console.WriteLine("The user " + e.Username + "  joined the room");
            TwitchBotWin.winRef.GetUserListClass();
        }

        private void Client_OnReSubscriber(object sender, OnReSubscriberArgs e)
        {
            //if he resub is he a new sub or is he still the same sub ?
        }
        private void Client_OnBeingHosted(object sender, OnBeingHostedArgs e)
        {
            Console.WriteLine("You got hosted by " + e.BeingHostedNotification.HostedByChannel + "  with  " + e.BeingHostedNotification.Viewers + "  viewers");
            //e.BeingHostedNotification.HostedByChannel
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
                // client.SendMessage(client.GetJoinedChannel(e.AutoJoinChannel), "Das ist ein Bot");
        }
        private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Joined channel " + e.Channel);
            Console.WriteLine(client.JoinedChannels.Count);
            //Add UserListCount to view count
           // SendMessageToChannel("The bot has joined the room!", e.Channel);
            //SendMessageToChannel("Im Chat sind gerade " + TwitchBotWin.winRef.GetUserListClass().GetFollowerCount() + " follower und  " + TwitchBotWin.winRef.GetUserListClass().GetSubCount() + " subs und " + TwitchBotWin.winRef.GetUserListClass().GetCurrentNonViewerInChat() + " Viewer die keine Follower sind!", e.Channel);
        }
        public void SendMessageToChannel(string message, string channelName)
        {
           client.SendMessage(channelName, message);
        }
        private void onMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            TwitchBotWin.winRef.AddNewMessageToStackPanel(e.ChatMessage.Username, e.ChatMessage.Message, DateTime.Now.ToString("HH:mm"), System.Windows.Media.Color.FromArgb(e.ChatMessage.Color.A, e.ChatMessage.Color.R, e.ChatMessage.Color.G, e.ChatMessage.Color.B));
        }
        private void onWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
        
        }
        private void onNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            Console.WriteLine("The user " + e.Subscriber.DisplayName + " subed!!!");
            TwitchBotWin.winRef.GetUserListClass().SetUserTypeOfUser(e.Subscriber.DisplayName, "Subscriber");
            TwitchBotWin.winRef.GetUserListClass().AddNewSubToCurrentSubsInChat();
            //Add new sub to count
            TwitchBotWin.winRef.RefreshCountLabels();
        }

    }
}

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
        private string userName;
        private string channelName;
        private Thread addUsersToListThread;

        public TwitchBotClient(string _userName, string password, string _channelName)
        {
            userName = _userName;
            channelName = _channelName;
            addUsersToListThread = new Thread(FillUserList);
            addUsersToListThread.Start();
            //Should be filled with oAuthToken and userId
            credentials = new ConnectionCredentials("", "");
            client = new TwitchClient();
            
            client.Initialize(credentials, channelName);
            client.ChatThrottler = new TwitchLib.Client.Services.MessageThrottler(client, 20, TimeSpan.FromSeconds(30));
            client.WhisperThrottler = new TwitchLib.Client.Services.MessageThrottler(client, 20, TimeSpan.FromSeconds(30));
            client.ChatThrottler.StartQueue();
            client.OnJoinedChannel += OnJoinedChannel;
            client.OnMessageReceived += OnMessageReceived;
            client.OnWhisperReceived += OnWhisperReceived;
            client.OnNewSubscriber += OnNewSubscriber;
            client.OnConnected += Client_OnConnected;
            client.OnBeingHosted += Client_OnBeingHosted;
            client.OnReSubscriber += Client_OnReSubscriber;
            client.OnUserJoined += Client_OnUserJoined;
            client.OnUserLeft += Client_OnUserLeft;
            client.OnChatCommandReceived += Client_OnChatCommandReceived;
            client.AddChatCommandIdentifier('!');

            //Unsub events will come late .... (nur zur anmerkung)
            try
            {
                client.Connect();
            }
            catch(Exception e)
            {
                Console.WriteLine("Verbindung konnte nicht hergestellt werden. Exception: " + e);
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
            TwitchBotWin.WinRef.CreatLoadingAndAddItToUIserListGrid();
            TwitchBotWin.WinRef.GetUserListClass().AddUsersToList(TwitchBotWin.WinRef.GetApiClass().GetFilledUserListWithAllInformation());
            var followerAndSubCount = TwitchBotWin.WinRef.GetUserListClass().GetFollowerCount() + TwitchBotWin.WinRef.GetUserListClass().GetSubCount();
            TwitchBotWin.WinRef.GetUserListClass().SetCurrentNonFollowerViewerInChat(TwitchBotWin.WinRef.GetUserListClass().GetCurrentViewerCount() - followerAndSubCount);
            TwitchBotWin.WinRef.RefreshCountLabels();
            TwitchBotWin.WinRef.DeletLoadingImage();
        }

        private void Client_OnUserLeft(object sender, OnUserLeftArgs e)
        {
            Console.WriteLine("The user " + e.Username + " left the room");
            TwitchBotWin.WinRef.GetUserListClass().UserLefTheChannel(e.Username);
        }

        private void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            Console.WriteLine("The user " + e.Username + "  joined the room");
            TwitchBotWin.WinRef.GetUserListClass();
        }

        private void Client_OnReSubscriber(object sender, OnReSubscriberArgs e)
        {
        }

        private void Client_OnBeingHosted(object sender, OnBeingHostedArgs e)
        {
            Console.WriteLine("You got hosted by " + e.BeingHostedNotification.HostedByChannel + "  with  " + e.BeingHostedNotification.Viewers + "  viewers");
 
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
               
        }

        private void OnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine("Joined channel " + e.Channel);
            Console.WriteLine(client.JoinedChannels.Count);
        }

        public void SendMessageToChannel(string message, string channelName)
        {
           client.SendMessage(channelName, message);
        }

        private void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            TwitchBotWin.WinRef.AddNewMessageToStackPanel(e.ChatMessage.Username, e.ChatMessage.Message, DateTime.Now.ToString("HH:mm"), System.Windows.Media.Color.FromArgb(e.ChatMessage.Color.A, e.ChatMessage.Color.R, e.ChatMessage.Color.G, e.ChatMessage.Color.B));
        }

        private void OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
        
        }

        private void OnNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            Console.WriteLine("The user " + e.Subscriber.DisplayName + " subed!!!");
            TwitchBotWin.WinRef.GetUserListClass().SetUserTypeOfUser(e.Subscriber.DisplayName, "Subscriber");
            TwitchBotWin.WinRef.GetUserListClass().AddNewSubToCurrentSubsInChat();
            TwitchBotWin.WinRef.RefreshCountLabels();
        }

    }
}

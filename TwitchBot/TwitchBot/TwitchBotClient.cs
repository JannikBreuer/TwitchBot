using System;
using System.Net.Sockets;
using System.IO;
using TwitchLib;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Client.Events;

namespace TwitchBot
{
    public class TwitchBotClient
    {

        private ConnectionCredentials credentials;


        private readonly TwitchClient client;
        private string _userName;
        private string _channelName;
        public TwitchBotClient(string userName, string password, string channelName)
        {
            _userName = userName;
            _channelName = channelName;

            credentials = new ConnectionCredentials("218744916", "oauth:9fxcnrikonai5w0vgt0ggaz6bj2pwy");
            client = new TwitchClient();
            TwitchBotWin.winRef.GetUserListClass().AddUsersToList(TwitchBotWin.winRef.GetApiClass().GetFilledUserListWithAllInformation());
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
            client.Connect();
        }

        private void Client_OnUserJoined(object sender, OnUserJoinedArgs e)
        {
            Console.WriteLine("The user " + e.Username + "  joined the room");
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
           // SendMessageToChannel("The bot has joined the room!", e.Channel);
          //  SendMessageToChannel("Im Chat sind gerade " + TwitchBotWin.winRef.GetUserListClass().GetFollowerCount() + " follower und  " + TwitchBotWin.winRef.GetUserListClass().GetSubCount() + " subs!", e.Channel);
        }
        public void SendMessageToChannel(string message, string channelName)
        {
           client.SendMessage(channelName, message);
        }
        private void onMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            TwitchBotWin.winRef.AddNewMessageToStackPanel(e.ChatMessage.Username, e.ChatMessage.Message, DateTime.Now.ToString("HH:mm"));
        }
        private void onWhisperReceived(object sender, OnWhisperReceivedArgs e)
        {
        
        }
        private void onNewSubscriber(object sender, OnNewSubscriberArgs e)
        {
            Console.WriteLine("The user " + e.Subscriber.DisplayName + " subed!!!");
            TwitchBotWin.winRef.GetUserListClass().SetTypeOfUser(e.Subscriber.DisplayName, "Subscriber");
        }

    }
}

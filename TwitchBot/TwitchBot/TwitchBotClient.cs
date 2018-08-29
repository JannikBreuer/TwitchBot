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


        private TwitchClient client;
        private string _userName;
        private string _channelName;

        public TwitchBotClient(string userName, string password, string channelName)
        {
            _userName = userName;
            _channelName = channelName;

            credentials = new ConnectionCredentials(userName,password);
            client = new TwitchClient();
            client.Initialize(credentials, channelName);
            client.Connect();

            client.OnJoinedChannel += onJoinedChannel;
            client.OnMessageReceived += onMessageReceived;
            client.OnWhisperReceived += onWhisperReceived;
            client.OnNewSubscriber += onNewSubscriber;
            client.OnConnected += Client_OnConnected;
            client.OnBeingHosted += Client_OnBeingHosted;
            client.OnReSubscriber += Client_OnReSubscriber;

            //TwitchLib.Client.Internal.Rfc2812.Kick() ;
        }

        private void Client_OnReSubscriber(object sender, OnReSubscriberArgs e)
        {

        }

        private void Client_OnBeingHosted(object sender, OnBeingHostedArgs e)
        {
            //e.BeingHostedNotification.HostedByChannel
        }

        private void Client_OnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");

            Console.WriteLine(client.JoinedChannels[0]);

            TwitchBotWin.winRef.GetApiClass().DisplayUserListOnScreen();

           // client.SendMessage(client.GetJoinedChannel(e.AutoJoinChannel), "Das ist ein Bot");
        }
        private void onJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
           
        }
        public void WriteMessage(string message)
        {
            client.SendMessage(client.GetJoinedChannel("SentioLIVE"), message);
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
     
        }

    }
}

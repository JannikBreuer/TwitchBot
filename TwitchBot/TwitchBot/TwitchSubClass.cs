using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.PubSub;
using System.IO;

namespace TwitchBot
{
    //Klasse wird benötigt um Follows mit zu verfolgen
    public class TwitchSubClass
    {
        private readonly TwitchPubSub client;

        public TwitchSubClass()
        {
            client = new TwitchPubSub();
            client.OnPubSubServiceConnected += Client_OnPubSubServiceConnected;
            client.OnFollow += Client_OnFollow;
        }

        private void Client_OnFollow(object sender, TwitchLib.PubSub.Events.OnFollowArgs e)
        {
            Console.WriteLine("The User: " + e.DisplayName + " has followed!");
            TwitchBotWin.WinRef.GetUserListClass().SetUserTypeOfUser(e.Username, "Follower");
            //Add new follower to Stream
            TwitchBotWin.WinRef.RefreshCountLabels();

        }
        private void Client_OnPubSubServiceConnected(object sender, EventArgs e)
        {
            client.SendTopics(File.ReadAllText(@"C:\Users\Janniks-Pc\Documents\Pw\TwitchOAuthToken.txt"));
        }
    }
}

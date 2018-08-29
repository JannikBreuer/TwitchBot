using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using TwitchLib.Api.Models.Undocumented;
using TwitchLib.Api;
using TwitchLib.Api.Extensions;
using TwitchLib.Api.Models;
using TwitchLib.Api.Models.v5.Channels;


namespace TwitchBot
{
    public class TwitchApi
    {
        private static TwitchAPI api;


        public TwitchApi()
        {
            api = new TwitchAPI();
            
        }
        public void DisplayUserListOnScreen()
        {
           var chatters =  Task.Run(() => api.Undocumented.GetChattersAsync("SentioLive"));
            TwitchBotWin.winRef.GetUserListClass().DisplayUserList(chatters.Result);
        }
        private void SetClientIDAndAcessToken()
        {
            api.Settings.ClientId = File.ReadAllText(@"C:\Users\j.breuer\Documents\Pw\TwtichClientId.txt");
            api.Settings.AccessToken = File.ReadAllText(@"C:\Users\j.breuer\Documents\Pw\TwitchOAuthToken.txt");
        }
    }
}

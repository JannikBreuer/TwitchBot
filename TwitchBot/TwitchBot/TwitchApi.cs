using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private UserListClass userListClass;
        private static TwitchAPI api;


        public TwitchApi()
        {
            userListClass = new UserListClass();
            api = new TwitchAPI();
            api.Settings.ClientId = "opktuhj4e5cyop3gycd0jwssv6evvz";
            api.Settings.AccessToken = "oauth: 9fxcnrikonai5w0vgt0ggaz6bj2pwy";
        }
        public void DisplayUserListOnScreen()
        {
           var chatters =  Task.Run(() => api.Undocumented.GetChattersAsync("SentioLive"));
            userListClass.DisplayUserList(chatters.Result);
        }
    }
}

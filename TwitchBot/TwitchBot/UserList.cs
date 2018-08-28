using System.Collections.Generic;
using System.Windows.Controls;

using TwitchLib.Api.Models.Undocumented;

namespace TwitchBot
{
    public class UserListClass
    {
        private List<string> userList;
        public UserListClass()
        {
            userList = new List<string>();

        }
        public void DisplayUserList(List<TwitchLib.Api.Models.Undocumented.Chatters.ChatterFormatted> list)
        {
            foreach (var user in list)
            {
                TwitchBotWin.winRef.AddNewUserToStackPanel(user.Username);
            }
            // TwitchBotWin.winRef.twitchClient.WriteMessage("Aktuell sind " + (list.Count + 1) + " User online!");
        }
    }

    public class UserEintrag
    {
        public string name;
    }
}

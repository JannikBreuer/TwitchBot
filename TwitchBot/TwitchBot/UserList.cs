using System.Collections.Generic;
using System.Windows.Controls;

using TwitchLib.Api.Models.Undocumented;

namespace TwitchBot
{
    public class UserList
    {
        //private List<>
        public void DisplayUserList(List<TwitchLib.Api.Models.Undocumented.Chatters.ChatterFormatted> list)
        {
            foreach (var user in list)
            {
               TwitchBotWin.winRef.AddNewUserToStackPanel(user.Username, "1");
            }
            TwitchBotWin.winRef.twitchClient.WriteMessage("Aktuell sind " + (list.Count + 1) + "User online!");
        }
    }

    public class UserEintrag
    {
        public StackPanel userPanel;
        public string name;
    }
}

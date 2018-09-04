using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    public class Commands
    {
        public static void IdentifyCommand(string command, string parameter)
        {
            switch(command)
            {
                case "upTime":
                    {
                        TwitchBotWin.winRef.GetTwitchBotClient().SendMessageToChannel("Sentio ist seid "  + TwitchBotWin.winRef.GetApiClass().GetUpTimeFromUser() + " am streamen", "SentioLIVE");
                    }break;
                case "uptime":
                    {
                        TwitchBotWin.winRef.GetTwitchBotClient().SendMessageToChannel("Sentio ist seid " + TwitchBotWin.winRef.GetApiClass().GetUpTimeFromUser() + " am streamen", "SentioLIVE");
                    }
                    break;
                case "viewerwatchtime":
                    {
                        if (parameter == null)
                            return;

                        
                        var userWatchTime = TwitchBotWin.winRef.GetUserListClass().GetViewerCurrentWatchTime(parameter);

                        if(userWatchTime == null)
                            TwitchBotWin.winRef.GetTwitchBotClient().SendMessageToChannel("Der User ist leider grade nicht im Chat.", "SentioLIVE");


                        TwitchBotWin.winRef.GetTwitchBotClient().SendMessageToChannel("Der User " + parameter + " guckt seit " + userWatchTime + " den Stream!", "SentioLIVE");
                    }
                    break;
            }
         //   if()
        }
    }
}

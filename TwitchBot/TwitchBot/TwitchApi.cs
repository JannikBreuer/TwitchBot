using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using TwitchLib.Api;
using TwitchLib.Api.Extensions;
using TwitchLib.Api.Models;
using TwitchLib.Api.Models.v5.Channels;
using TwitchLib.Api.Models.v5.Subscriptions;
using TwitchLib.PubSub.Events;
using TwitchLib.Api.Models.Undocumented.Chatters;
using TwitchLib.Api.Interfaces;

namespace TwitchBot
{
    public class TwitchApi
    {
        private readonly TwitchAPI api;
        //Ist das gleiche wie die UserId
        private string channelId = "95745125";

        public TwitchApi()
        {
            api = new TwitchAPI();
            SetClientIDAndAcessToken();
        }

        public bool CheckIfUserIsSubed(string userID)
        {
            var result = Task.Run(() => api.Users.v5.CheckUserSubscriptionByChannelAsync(userID, channelId).Result);
            if (result.Result == null)
                return false;
            return true;
        }

        public bool CheckIfUserFollowesTheChannel(string userID)
        {
            var result = Task.Run(() => api.Users.v5.CheckUserFollowsByChannelAsync(userID, channelId));
            if (result.Result == null)
                return false;
            return true;
        }

        //Gibt eine List mit Informationen über die User am Chat zurück
        public ObservableCollection<UserEintrag> GetFilledUserListWithAllInformation()          
        {
            ObservableCollection<UserEintrag> userList = new ObservableCollection<UserEintrag>();
            var usersInChat = GetAllUsersInTheCurrentChat("SentioLIVE");
            foreach (var user in usersInChat)
            {
                UserEintrag _user = new UserEintrag();
                _user.UserName = user.Username;
                userList.Add(_user);
            }
            userList = CheckIfUserIsSub(userList, GetSubsOfChannel());
            userList = CheckIfUserIsFollower(userList, GetFollowersOfChannel());
            return userList;
        }

        public List<ChatterFormatted> GetAllUsersInTheCurrentChat(string chatName)
        {
            var chatters = Task.Run(() => api.Undocumented.GetChattersAsync("SentioLIVE").Result);
            return chatters.Result;
        }

        public ObservableCollection<UserEintrag> CheckIfUserIsFollower(ObservableCollection<UserEintrag> userList, List<ChannelFollow> followerList)
        {
            int followerCounter = 0;
            foreach (var follower in followerList)
            {
                foreach (var userInChat in userList)
                {
                    if (follower.User.Name == userInChat.UserName)
                    {
                        if (userInChat.UserType == "Subscriber")
                            break;

                        userInChat.UserType = "Follower";
                        userInChat.Since = follower.CreatedAt.ToString("DD");
                        userInChat.UserId = follower.User.Id;
                        followerCounter++;
                        break;
                    }
                }
                if (followerCounter == userList.Count)
                {
                    TwitchBotWin.WinRef.GetUserListClass().SetFollowerCount(followerCounter);
                    return userList;
                }
            }
            TwitchBotWin.WinRef.GetUserListClass().SetFollowerCount(followerCounter);
            return userList;
        }

        //First find all Subs and then Find the Followers which arent Subs
        public ObservableCollection<UserEintrag> CheckIfUserIsSub(ObservableCollection<UserEintrag> userList, List<Subscription> subs)            
        {
            int subsCount = 0;
            foreach (var sub in subs)
            {
                foreach (var userInChat in userList)
                {
                    if (sub.User.Name == userInChat.UserName)
                    {
                        userInChat.UserType = "Subscriber";
                        subsCount++;
                        userInChat.UserId = sub.User.Id;
                        userInChat.Since = sub.CreatedAt.ToString();
                        break;
                    }
                }
                if (subsCount == userList.Count)
                {
                    TwitchBotWin.WinRef.GetUserListClass().SetSubCountInChat(subsCount);
                    return userList;
                }
            }
            TwitchBotWin.WinRef.GetUserListClass().SetSubCountInChat(subsCount);
            return userList;
        }

        public List<Subscription> GetSubsOfChannel()
        {
            var subsList = Task.Run(() => api.Channels.v5.GetAllSubscribersAsync(channelId)).Result;
            return subsList;
        }

        public List<ChannelFollow> GetFollowersOfChannel()
        {
            var followerList = Task.Run(() => api.Channels.v5.GetAllFollowersAsync(channelId).Result);
            return followerList.Result;
        }

        public void CheckIfUserIsFollowing(string userID)
        {
            var test = Task.Run(() => api.Users.helix.GetUsersFollowsAsync(toId: channelId, fromId: userID)).Result;
            
        }

        public string GetUpTimeFromUser()
        {
            var timeSpan =  Task.Run(() => api.Streams.v5.GetUptimeAsync(channelId)).Result.Value;
            return timeSpan.ToString(@"hh\:mm\:ss");
        }

        //Nur eine Testfunction um zu gucken was schneller geht
        public void Test2(string userID)
        {
            var test = Task.Run(() => api.Users.v5.CheckUserSubscriptionByChannelAsync(userID, channelId)).Result;
            if(test == null)
            {
                Console.WriteLine("Not subed");
            }
            else
            {
                Console.WriteLine("subed");
            }
        }

        public void SetClientIDAndAcessToken()
        {
            api.Settings.ClientId = File.ReadAllText(@"C:\Users\Janniks-Pc\Documents\Pw\TwtichClientId.txt");
            api.Settings.AccessToken = File.ReadAllText(@"C:\Users\Janniks-Pc\Documents\Pw\TwitchOAuthToken.txt");
        }
    }
}

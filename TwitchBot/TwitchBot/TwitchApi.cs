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
        private string channelId;

        public TwitchApi(string channelID)
        {
            api = new TwitchAPI();
            SetClientIDAndAcessToken();
            channelId = channelID;
        }
        public void GetIDOfChannel()
        {

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
        public ObservableCollection<UserEintrag> GetFilledUserListWithAllInformation()          //Gibt eine List mit Informationen über die User am Chat zurück
        {
            ObservableCollection<UserEintrag> userList = new ObservableCollection<UserEintrag>();
            var usersInChat = GetAllUsersInTheCurrentChat("SentioLIVE");
            foreach (var user in usersInChat)
            {
                UserEintrag _user = new UserEintrag();
                _user.userName = user.Username;
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
        public ObservableCollection<UserEintrag> CheckIfUserIsFollower(ObservableCollection<UserEintrag> userList, IFollow[] followerList)
        {
            int followerCounter = 0;
            foreach (var follower in followerList)
            {
                foreach (var userInChat in userList)
                {
                    if (follower.User.DisplayName == userInChat.userName)
                    {
                        if (userInChat.userType == "Subscriber")
                        {
                            followerCounter++;
                            break;
                        }
                        userInChat.userType = "Follower";
                        followerCounter++;
                    }
                }
                if (followerCounter == userList.Count)
                    return userList;
            }
            return userList;
        }
        public ObservableCollection<UserEintrag> CheckIfUserIsSub(ObservableCollection<UserEintrag> userList, Subscription[] subs)       //First find all Subs and then Find the Followers which arent Subs      
        {
            int subsCount = 0;
            foreach (var sub in subs)
            {
                foreach (var userInChat in userList)
                {
                    if (sub.User.DisplayName == userInChat.userName)
                    {
                        userInChat.userType = "Subscriber";
                        subsCount++;
                    }
                }
                if (subsCount == userList.Count)
                    return userList;
            }
            return userList;
        }
        public Subscription[] GetSubsOfChannel()
        {
            var subsList = Task.Run(() => api.Channels.v5.GetChannelSubscribersAsync("").Result);
            return subsList.Result.Subscriptions;
        }
        public IFollow[] GetFollowersOfChannel()
        {
            var followerList = Task.Run(() => api.Channels.v5.GetChannelFollowersAsync(channelId).Result);
            return followerList.Result.Follows;
        }
        public void SetClientIDAndAcessToken()
        {
            api.Settings.ClientId = File.ReadAllText(@"C:\Users\Janniks-Pc\Documents\Pw\TwtichClientId.txt");
            api.Settings.AccessToken = File.ReadAllText(@"C:\Users\Janniks-Pc\Documents\Pw\TwitchOAuthToken.txt");
        }
    }
}

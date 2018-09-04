using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Timers;

namespace TwitchBot
{
    public class UserListClass
    {
        public  ObservableCollection<UserEintrag> userList { get; set; }
        private int currenSubsInChat;
        private int currentFollowerInChat;
        private int currentNonFollowerViewer;           //And non sub
        private Timer timer;

        public UserListClass()
        {
            userList = new ObservableCollection<UserEintrag>();
            timer = new Timer();
            timer.Interval = 60000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (userList.Count == 0) return;
            for (int i = 0; i < userList.Count; i++)
            {
                var user = new UserEintrag();
                user.since = userList[i].since;
                user.userType = userList[i].userType;
                user.userName = userList[i].userName;
                user.userID = userList[i].userID;
                user.currentWatchTime = userList[i].currentWatchTime.Add(new TimeSpan(0, 1, 0));
                userList.RemoveAt(i);
                userList.Insert(i,user);
            }
        }
        #region getter#Setter
        public string GetViewerCurrentWatchTime(string userName)
        {
            foreach (var user in userList)
            {
                if (user.userName == userName)
                    return user.currentWatchTime.ToString(@"hh\:mm");
            }
            return null;
        }
        public int GetCurrentViewerCount()
        {
            return this.userList.Count;
        }
        public int GetCurrentNonFollowerInChat()
        {
            return this.currentNonFollowerViewer;
        }
        public int GetSubCount()
        {
            return this.currenSubsInChat;
        }
        public void SetUserTypeOfUser(string userName, string userType)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].userName == userName)
                {
                    userList[i].userType = userType;
                    return;
                }
            }
        }
        public int GetFollowerCount()
        {
            return this.currentFollowerInChat;
        }
        public void SetFollowerCount(int newFollowerCount)
        {
            this.currentFollowerInChat = newFollowerCount;
        }
        public void SetSubCountInChat(int newSubCount)
        {
            this.currenSubsInChat = newSubCount;
        }
        public void SetCurrentNonFollowerViewerInChat(int newNonFollowerViewerCount) 
        {
            this.currentNonFollowerViewer = newNonFollowerViewerCount;
        }
        #endregion
        public void AddNewFollowerToCurrentFollowerInChat()
        {
            this.currentFollowerInChat++;
        }
        public void AddNewNonFollowerToCurrentNonFollwerChat()
        {
            this.currentNonFollowerViewer++;
        }
        public void AddNewSubToCurrentSubsInChat()
        {
            this.currenSubsInChat++;
        }
        public void AddNewUserToCurrentChannel(UserEintrag user)
        {
            userList.Add(user);
        }
        public void UserLefTheChannel(string userName)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if(userList[i].userName == userName)
                {
                    if (userList[i].userType == "Viewer")
                        currentNonFollowerViewer--;
                    else if (userList[i].userType == "Follower")
                        currentFollowerInChat--;
                    else
                        currenSubsInChat--;

                    userList.RemoveAt(i);
                    break;
                }

            }
        }
        public void AddUsersToList(ObservableCollection<UserEintrag> _userList)
        {
            foreach (var item in _userList)
            {
                userList.Add(item);
            }
        }
        
    }
    public class UserEintrag
    {
        public string userName { get; set; }
        public string userType { get; set; } = "Viewer";        //is Follower ore Sub
        public string since { get; set; }
        public string userPoints { get; set; } = "0";      //muss später noch eingefügt werden
        public string userID { get; set; }
        public TimeSpan currentWatchTime { get; set; } = new TimeSpan(0);         //UserViewTime




    }
}

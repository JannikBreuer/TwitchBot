using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Timers;

namespace TwitchBot
{
    public class UserListClass
    {
        public  ObservableCollection<UserEintrag> UserList { get; set; }
        private int currenSubsInChat;
        private int currentFollowerInChat;
        private int currentNonFollowerViewer;
        private Timer timer;

        public UserListClass()
        {
            UserList = new ObservableCollection<UserEintrag>();
            timer = new Timer
            {
                Interval = 60000
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (UserList.Count == 0) return;
            for (int i = 0; i < UserList.Count; i++)
            {
                var user = new UserEintrag
                {
                    Since = UserList[i].Since,
                    UserType = UserList[i].UserType,
                    UserName = UserList[i].UserName,
                    UserId = UserList[i].UserId,
                    CurrentWatchTime = UserList[i].CurrentWatchTime.Add(new TimeSpan(0, 1, 0))
                };
                UserList.RemoveAt(i);
                UserList.Insert(i,user);
            }
        }
        #region getter#Setter

        public string GetViewerCurrentWatchTime(string userName)
        {
            foreach (var user in UserList)
            {
                if (user.UserName == userName)
                    return user.CurrentWatchTime.ToString(@"hh\:mm");
            }
            return null;
        }

        public int GetCurrentViewerCount()
        {
            return this.UserList.Count;
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
            for (int i = 0; i < UserList.Count; i++)
            {
                if (UserList[i].UserName == userName)
                {
                    UserList[i].UserType = userType;
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
            UserList.Add(user);
        }

        public void UserLefTheChannel(string userName)
        {
            for (int i = 0; i < UserList.Count; i++)
            {
                if(UserList[i].UserName == userName)
                {
                    if (UserList[i].UserType == "Viewer")
                    {
                        currentNonFollowerViewer--;
                    }
                    else if (UserList[i].UserType == "Follower")
                    {
                        currentFollowerInChat--;
                    }
                    else
                    {
                        currenSubsInChat--;
                    }

                    UserList.RemoveAt(i);
                    break;
                }

            }
        }

        public void AddUsersToList(ObservableCollection<UserEintrag> _userList)
        {
            foreach (var item in _userList)
            {
                UserList.Add(item);
            }
        }
        
    }
    public class UserEintrag
    {
        public string UserName { get; set; }
        public string UserType { get; set; } = "Viewer";       
        public string Since { get; set; }
        public string UserPoints { get; set; } = "0";     
        public string UserId { get; set; }
        public TimeSpan CurrentWatchTime { get; set; } = new TimeSpan(0);
    }
}

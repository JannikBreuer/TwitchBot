using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace TwitchBot
{
    public class UserListClass
    {
        public  ObservableCollection<UserEintrag> userList { get; set; }
        private int currenSubsInChat;
        private int currentFollowerInChat;


        #region getter#Setter
        public int GetSubCount()
        {
            return this.currenSubsInChat;
        }
        public int GetFollowerCount()
        {
            return this.currentFollowerInChat;
        }
        public void SetFollowerCount(int newFollowerCount)
        {
            currentFollowerInChat = newFollowerCount;
        }
        public void SetSubCountInChat(int newSubCount)
        {
            currenSubsInChat = newSubCount;
        }
        public void SetTypeOfUser(string userName, string userType)
        {
            foreach (var user in userList)
            {
                if (user.userName == userName)
                {
                    user.userType = userType;
                    return;
                }
            }
        }
        #endregion
        public UserListClass()
        {
            userList = new ObservableCollection<UserEintrag>();
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
        public string userPoints { get; set; } = "0";      //muss später noch eingefügt werden
    }
}

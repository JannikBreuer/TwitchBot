using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    public class StreamInformation
    {
        private int newFollowers;
        private int newSubs;
        private int views;



        #region Get + Setter
        public int GetNewFollowerCount()
        {
            return this.newFollowers;
        }
        public int GetNewSubCount()
        {
            return this.newSubs;
        }
        public int GetViewsCount()
        {
            return this.views;
        }
        public void SetNewViewerCount(int newViewerCount)
        {
            this.views = newViewerCount;
        }
        public void SetNewSubsCount(int newSubsCount)
        {
            this.newSubs = newSubsCount;
        }
        public void SetNewFollowerCOunt(int newFollowerCount)
        {
            this.newFollowers = newFollowerCount;
        }
        #endregion



    }
}

using MyHome.Helpers;
using RSSFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.ViewModels
{
   
    public class RSSFeedControlViewModel : Observable
    {

        private string title;
        private Uri myImagePath;
        private String content;
        private string date;
        private string source;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                Set(ref title, value);
            }
        }

        public Uri MyImagePath
        {
            get
            {
                return myImagePath;
            }
            set
            {
                Set(ref myImagePath, value);
            }
        }

        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                Set(ref content, value);
            }
        }





        public RSSFeedControlViewModel()
        {
            RSSFeedParser rSSFeedParser = new RSSFeedParser("https://www.tagesschau.de/xml/rss2/");
            rSSFeedParser.Parse();
        }
    }
}

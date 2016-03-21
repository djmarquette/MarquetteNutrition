using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace MNF4.ViewModels
{
    public class BlogViewModel
    {
        private string _wpUrl  = ConfigurationManager.AppSettings["wpFeed"];    // wordpress URL
        private string _bsUrl = ConfigurationManager.AppSettings["bsFeed"];     // blogspot URL

        public string wpUrl { get { return _wpUrl; } }
        public string bsUrl { get { return _bsUrl; } }
 
        public SyndicationFeed rssData = new SyndicationFeed();
        
        public BlogViewModel()
        {
            #region WordPress test
            //            if (!String.IsNullOrEmpty(_wpUrl))
//            {
//                try
//                {
//                    XmlReader wpReader = XmlReader.Create(_wpUrl);
//                    //wpReader.read
//                    rssData.Add(SyndicationFeed.Load(wpReader));
//                }
//                catch (Exception)
//                {
//                    ;           // ignore error, it just won't add to list
//                }
            //            }
            #endregion

            //string _bsUrl = "http://marquettenutrition.blogspot.com/feeds/posts/default";
            XmlReader reader = XmlReader.Create(_bsUrl);
            rssData = SyndicationFeed.Load(reader);
        }

    }
}
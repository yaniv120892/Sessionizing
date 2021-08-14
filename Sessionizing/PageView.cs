namespace Sessionizing
{
    public class PageView
    {
        public string VisitorId { get; }
        public string SiteUrl { get; }
        public string PageViewUrl { get; }
        public long TimeStamp { get; }

        
        public PageView(string visitorId, string siteUrl, string pageViewUrl, long timeStamp)
        {
            VisitorId = visitorId;
            SiteUrl = siteUrl;
            PageViewUrl = pageViewUrl;
            TimeStamp = timeStamp;
        }
    }
}
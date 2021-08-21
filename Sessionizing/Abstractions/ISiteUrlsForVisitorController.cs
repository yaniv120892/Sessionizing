using System.Collections.Generic;

namespace Sessionizing.Abstractions
{
    public interface ISiteUrlsForVisitorController
    {
        void Add(string visitorId, string siteUrl);
        IEnumerable<string> GetSites(string visitorId);
    }
}
using System.Collections.Generic;
using Sessionizing.Abstractions;
using Sessionizing.Exceptions;

namespace Sessionizing
{
    public class SiteUrlsForVisitorController : ISiteUrlsForVisitorController
    {
        private readonly IDictionary<string, HashSet<string>> m_visitorToSitesMapping =
            new Dictionary<string, HashSet<string>>();

        public void Add(string visitorId, string siteUrl)
        {
            if (!m_visitorToSitesMapping.ContainsKey(visitorId))
            {
                m_visitorToSitesMapping[visitorId] = new HashSet<string>();
            }

            m_visitorToSitesMapping[visitorId].Add(siteUrl);
        }
        
        public IEnumerable<string> GetSites(string visitorId)
        {
            if (m_visitorToSitesMapping.TryGetValue(visitorId, out HashSet<string> sites))
            {
                return sites;
            }
            
            throw new UnknownVisitorIdException($"No visited sites for {visitorId}");
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Sessionizing.Abstractions;

namespace Sessionizing.Providers
{
    internal class UniqueVisitedSitesProvider : IUniqueVisitedSitesProvider
    {
        private readonly ISiteUrlsForVisitorController m_siteUrlsForVisitorController;
        private readonly Dictionary<string, int> m_cacheSitesCount;

        public UniqueVisitedSitesProvider(ISiteUrlsForVisitorController siteUrlsForVisitorController)
        {
            m_siteUrlsForVisitorController = siteUrlsForVisitorController;
            m_cacheSitesCount = new Dictionary<string, int>();
        }

        public int Get(string visitorId)
        {
            if (!m_cacheSitesCount.ContainsKey(visitorId))
            {
                m_cacheSitesCount[visitorId] = m_siteUrlsForVisitorController.GetSites(visitorId).Count();
            }

            return m_cacheSitesCount[visitorId];
        }
    }
}
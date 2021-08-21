using System.Collections.Generic;
using Sessionizing.Abstractions;

namespace Sessionizing
{
    public class PageViewsInfoLoader : IPageViewsInfoLoader
    {
        private readonly IDictionary<string, Session> m_siteAndVisitorToSessionsMapping;
        private readonly ISiteUrlsForVisitorController m_siteUrlsForVisitorController;
        private readonly ISessionsForSiteUrlController m_sessionsForSiteUrlController;

        public PageViewsInfoLoader(ISiteUrlsForVisitorController siteUrlsForVisitorController, 
            ISessionsForSiteUrlController sessionsForSiteUrlController)
        {
            m_siteUrlsForVisitorController = siteUrlsForVisitorController;
            m_sessionsForSiteUrlController = sessionsForSiteUrlController;
            m_siteAndVisitorToSessionsMapping = new Dictionary<string, Session>();
        }
        
        public void Load(IPageViewReader pageViewProvider)
        {
            using IEnumerator<PageView> enumerator = pageViewProvider.GetEnumerator();
            enumerator.MoveNext();
            PageView pageView = enumerator.Current;

            while (pageView != null)
            {
                string sessionDictionaryKey = GetSessionAndVisitorDictionaryKey(pageView);
                m_siteUrlsForVisitorController.Add(pageView.VisitorId, pageView.SiteUrl);
                AggregateSessions(sessionDictionaryKey, pageView);
                pageView = enumerator.MoveNext() ? enumerator.Current : null;
            }

            foreach (var keyValuePair in m_siteAndVisitorToSessionsMapping)
            {
                string siteUrl = GetSiteUrlFromSessionAndVisitorDictionaryKey(keyValuePair.Key);
                m_sessionsForSiteUrlController.Add(siteUrl ,keyValuePair.Value);
            }
        }

        private void AggregateSessions(string sessionAndVisitorDictionaryKey, PageView pageView)
        {
            if (!m_siteAndVisitorToSessionsMapping.ContainsKey(sessionAndVisitorDictionaryKey))
            {
                m_siteAndVisitorToSessionsMapping[sessionAndVisitorDictionaryKey] = new Session(pageView);
                return;
            }

            Session session = m_siteAndVisitorToSessionsMapping[sessionAndVisitorDictionaryKey];
            if (session.IsBelongToSession(pageView))
            {
                session.AddPageView(pageView);
                return;
            }
            
            m_sessionsForSiteUrlController.Add(pageView.SiteUrl ,session);
            m_siteAndVisitorToSessionsMapping[sessionAndVisitorDictionaryKey] = new Session(pageView);
        }

        private static string GetSessionAndVisitorDictionaryKey(PageView pageView) => 
            $"{pageView.VisitorId}:{pageView.SiteUrl}";
        private static string GetSiteUrlFromSessionAndVisitorDictionaryKey(string sessionAndVisitorDictionaryKey) =>
            sessionAndVisitorDictionaryKey.Split(":")[1];
    }
}
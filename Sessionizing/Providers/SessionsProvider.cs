using System.Collections.Generic;
using System.Linq;
using Sessionizing.Abstractions;

namespace Sessionizing.Providers
{
    internal class SessionsProvider : ISessionCounter, ISessionMedianLengthCalculator
    {
        private readonly IPageViewsRepository m_pageViewsRepository;

        public SessionsProvider(IPageViewsRepository pageViewsRepository)
        {
            m_pageViewsRepository = pageViewsRepository;
        }
        
        public int Count(string siteUrl)
        {
            return GetSiteSessions(siteUrl).Count();
        }

        public double Calculate(string siteUrl)
        {
            List<long> sessionsLength = GetSiteSessions(siteUrl).Select(m=> m.SessionLength).ToList();
            sessionsLength.Sort();
            return sessionsLength[sessionsLength.Count / 2];
        }
        
        private IEnumerable<Session> GetSiteSessions(string siteUrl)
        {
            var pageViewsForSite = m_pageViewsRepository.GetPageViewForSite(siteUrl);
            return GetSessions(pageViewsForSite);
        }

        private static IEnumerable<Session> GetSessions(List<PageView> pageViewsForSite)
        {
            pageViewsForSite.Sort(CompareByTimestamp);
            var sessions = new List<Session>();
            var inProgressSessions = new Dictionary<string, Session>();
            
            foreach (PageView pageView in pageViewsForSite)
            {
                string dictionaryKey = GetDictionaryKey(pageView);
                if (inProgressSessions.TryGetValue(dictionaryKey, out Session session))
                {
                    if (session.IsBelongToSession(pageView))
                    {
                        session.AddPageView(pageView);
                        continue;
                    }

                    sessions.Add(session);
                }

                inProgressSessions[dictionaryKey] = new Session(pageView);
            }

            sessions.AddRange(inProgressSessions.Values);
            return sessions;
        }

        private static string GetDictionaryKey(PageView pageView)
        {
            return $"{pageView.VisitorId}_{pageView.SiteUrl}";
        }

        private static int CompareByTimestamp(PageView x1, PageView y1)
        {
            return (int) (x1.TimeStamp - y1.TimeStamp);
        }
    }
}
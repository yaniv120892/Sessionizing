using System.Collections.Generic;
using System.Linq;
using Sessionizing.Abstractions;

namespace Sessionizing.Providers
{
    internal class SessionsProvider : ISessionCounter, ISessionMedianLengthCalculator
    {
        private readonly ISessionsForSiteUrlController m_sessionsForSiteUrlController;
        private readonly Dictionary<string, int> m_cacheSessionCount;
        private readonly Dictionary<string, double> m_cacheSessionMedianLength;

        public SessionsProvider(ISessionsForSiteUrlController sessionsForSiteUrlController)
        {
            m_cacheSessionCount = new Dictionary<string, int>();
            m_cacheSessionMedianLength = new Dictionary<string, double>();
            m_sessionsForSiteUrlController = sessionsForSiteUrlController;
        }
        
        public int Count(string siteUrl)
        {
            if (!m_cacheSessionCount.ContainsKey(siteUrl))
            {
                m_cacheSessionCount[siteUrl] = m_sessionsForSiteUrlController.GetSessions(siteUrl).Count();
            }

            return m_cacheSessionCount[siteUrl];
        }
            

        public double Calculate(string siteUrl)
        {
            if (!m_cacheSessionMedianLength.ContainsKey(siteUrl))
            {
                List<long> sessionsLength = m_sessionsForSiteUrlController
                    .GetSessions(siteUrl)
                    .Select(m=> m.SessionLength)
                    .ToList();
                sessionsLength.Sort();
                m_cacheSessionMedianLength[siteUrl] = sessionsLength[sessionsLength.Count / 2];
            }
            
            return m_cacheSessionMedianLength[siteUrl];
        }
    }
}
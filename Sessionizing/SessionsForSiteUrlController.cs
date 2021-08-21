using System.Collections.Generic;
using Sessionizing.Abstractions;
using Sessionizing.Exceptions;

namespace Sessionizing
{
    public class SessionsForSiteUrlController : ISessionsForSiteUrlController
    {
        private readonly IDictionary<string, List<Session>> m_siteToSessionsMapping = new Dictionary<string, List<Session>>();

        public void Add(string siteUrl, Session session)
        {
            if (!m_siteToSessionsMapping.ContainsKey(siteUrl))
            {
                m_siteToSessionsMapping[siteUrl] = new List<Session>();
            }

            m_siteToSessionsMapping[siteUrl].Add(session);
        }

        public IEnumerable<Session> GetSessions(string siteUrl)
        {
            if (m_siteToSessionsMapping.TryGetValue(siteUrl, out List<Session> sessions))
            {
                return sessions;
            }
            
            throw new UnknownSiteUrlException($"No sessions info for {siteUrl}");
        }
    }
}
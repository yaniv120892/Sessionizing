using System.Collections.Generic;

namespace Sessionizing.Abstractions
{
    public interface ISessionsForSiteUrlController
    {
        void Add(string siteUrl, Session session);
        IEnumerable<Session> GetSessions(string siteUrl);
    }
}
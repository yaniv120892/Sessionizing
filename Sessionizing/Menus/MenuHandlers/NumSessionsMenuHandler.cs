using System;
using Sessionizing.Abstractions;
using Sessionizing.Exceptions;

namespace Sessionizing.Menus.MenuHandlers
{
    internal class NumSessionsMenuHandler : IMenuHandler
    {
        private readonly ISessionCounter m_sessionCounter;

        public NumSessionsMenuHandler(ISessionCounter sessionCounter)
        {
            m_sessionCounter = sessionCounter;
        }

        public void Handle()
        {
            Console.WriteLine("Enter site url");
            string siteUrl = Console.ReadLine();
            try
            {
                int numOfSessions = m_sessionCounter.Count(siteUrl);
                Console.WriteLine($"Num of sessions for {siteUrl} is {numOfSessions}");
            }
            catch (UnknownSiteUrlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
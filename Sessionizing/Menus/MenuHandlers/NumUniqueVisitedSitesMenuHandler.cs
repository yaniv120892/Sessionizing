using System;
using Sessionizing.Abstractions;

namespace Sessionizing.Menus.MenuHandlers
{
    internal class NumUniqueVisitedSitesMenuHandler : IMenuHandler
    {
        private readonly IUniqueVisitedSitesProvider m_uniqueVisitedSitesProvider;

        public NumUniqueVisitedSitesMenuHandler(IUniqueVisitedSitesProvider uniqueVisitedSitesProvider)
        {
            m_uniqueVisitedSitesProvider = uniqueVisitedSitesProvider;
        }

        public void Handle()
        {
            Console.WriteLine("Enter visitor id");
            string visitorId = Console.ReadLine();
            int numOfUniqueVisitedSites = m_uniqueVisitedSitesProvider.Get(visitorId);
            Console.WriteLine($"Num of unique visited sites for {visitorId} is {numOfUniqueVisitedSites}");
        }
    }
}
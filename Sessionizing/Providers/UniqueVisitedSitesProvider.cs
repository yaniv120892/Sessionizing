using System.Linq;
using Sessionizing.Abstractions;

namespace Sessionizing.Providers
{
    internal class UniqueVisitedSitesProvider : IUniqueVisitedSitesProvider
    {
        private readonly IPageViewsRepository m_pageViewsRepository;

        public UniqueVisitedSitesProvider(IPageViewsRepository pageViewsRepository)
        {
            m_pageViewsRepository = pageViewsRepository;
        }
        
        public int Get(string visitorId) => 
            m_pageViewsRepository.GetPageViewForVisitor(visitorId)
                .Select(m => m.SiteUrl)
                .Distinct()
                .Count();
    }
}
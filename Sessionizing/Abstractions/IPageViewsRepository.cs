using System.Collections.Generic;

namespace Sessionizing.Abstractions
{
    public interface IPageViewsRepository
    {
        List<PageView> GetPageViewForVisitor(string visitorId);
        List<PageView> GetPageViewForSite(string siteUrl);
    }
}
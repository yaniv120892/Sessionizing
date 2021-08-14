namespace Sessionizing.Abstractions
{
    internal interface IUniqueVisitedSitesProvider
    {
        int Get(string visitorId);
    }
}
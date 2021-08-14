namespace Sessionizing.Abstractions
{
    internal interface ISessionCounter
    {
        int Count(string siteUrl);
    }
}
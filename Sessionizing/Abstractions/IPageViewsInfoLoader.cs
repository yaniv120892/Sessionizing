namespace Sessionizing.Abstractions
{
    public interface IPageViewsInfoLoader
    {
        void Load(IPageViewReader pageViewReader);
    }
}
using System.Collections.Generic;

namespace Sessionizing
{
    public class Session
    {
        private const int c_maxPeriodBetweenPageViewsInSeconds = 1800;
        
        private readonly List<PageView> m_pageViews;
        private readonly PageView m_firstPageView;
        private PageView m_lastPageView;

        public Session(PageView pageView)
        {
            m_pageViews = new List<PageView>()
            {
                pageView
            };
            m_firstPageView = pageView;
            m_lastPageView = pageView;
        }

        public long SessionLength => m_lastPageView.TimeStamp - m_firstPageView.TimeStamp;

        public void AddPageView(PageView newPageView)
        {
            m_pageViews.Add(newPageView);
            m_lastPageView = newPageView;
        }

        public bool IsBelongToSession(PageView newPageView)
        {
            return newPageView.TimeStamp - m_lastPageView.TimeStamp <= c_maxPeriodBetweenPageViewsInSeconds;
        }
    }
}
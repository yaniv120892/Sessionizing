using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sessionizing.Abstractions;

namespace Sessionizing.PageViewReaders
{
    public class PageViewReadersController : IPageViewReader
    {
        private readonly IEnumerator<PageView>[] m_pageViewReaders;
        private readonly PageView[] m_currentPageViews;

        public PageViewReadersController(IPageViewReader[] pageViewReaders)
        {
            m_pageViewReaders = pageViewReaders.Select(m=> m.GetEnumerator()).ToArray();
            m_currentPageViews = new PageView[pageViewReaders.Length];
            for(int i = 0; i < m_pageViewReaders.Length; i++)
            {
                m_pageViewReaders[i].MoveNext();
                m_currentPageViews[i] = m_pageViewReaders[i].Current;
            }
        }
        
        public IEnumerator<PageView> GetEnumerator()
        {                
            int oldestTimeStampIndex = GetOldestTimeStampIndex();
            PageView pageViewToReturn = m_currentPageViews[oldestTimeStampIndex];
            LoadNextPageView(oldestTimeStampIndex);
            
            while (pageViewToReturn != null)
            {
                yield return pageViewToReturn;
                oldestTimeStampIndex = GetOldestTimeStampIndex();
                if (oldestTimeStampIndex == -1)
                {
                    pageViewToReturn = null;
                }
                else
                {
                    pageViewToReturn = m_currentPageViews[oldestTimeStampIndex];
                    LoadNextPageView(oldestTimeStampIndex); 
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private int GetOldestTimeStampIndex()
        {
            int oldestTimeStampIndex = -1;
            long oldestTimeStamp =  long.MaxValue;
            for (int i = 0; i < m_currentPageViews.Length; i++)
            {
                if (m_currentPageViews[i] == null)
                {
                    continue;
                }
                
                if (oldestTimeStamp > m_currentPageViews[i].TimeStamp)
                {
                    oldestTimeStampIndex = i;
                    oldestTimeStamp = m_currentPageViews[i].TimeStamp;
                }
            }

            return oldestTimeStampIndex;
        }

        private void LoadNextPageView(int index)
        {
            m_currentPageViews[index] = m_pageViewReaders[index].MoveNext() ? m_pageViewReaders[index].Current : null;
        }
    }
}
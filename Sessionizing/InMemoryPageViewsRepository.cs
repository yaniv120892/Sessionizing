using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using Sessionizing.Abstractions;

namespace Sessionizing
{
    internal class InMemoryPageViewsRepository : IPageViewsRepository
    {
        private readonly List<PageView> m_pageViews = new();
        
        public InMemoryPageViewsRepository(IReadOnlyList<string> filesToRead)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
            
            foreach (var fileToRead in filesToRead)
            {
                using var streamReader = File.OpenText(fileToRead);
                using var csvReader = new CsvReader(streamReader, csvConfig);
                IEnumerable<PageView> pageViewsFromFile = csvReader.GetRecords<PageView>();
                m_pageViews.AddRange(pageViewsFromFile);
            }
        }

        public List<PageView> GetPageViewForVisitor(string visitorId) => 
            m_pageViews.Where(m => m.VisitorId == visitorId).ToList();
        
        public List<PageView> GetPageViewForSite(string siteUrl) => 
            m_pageViews.Where(m => m.SiteUrl == siteUrl).ToList();
    }
}
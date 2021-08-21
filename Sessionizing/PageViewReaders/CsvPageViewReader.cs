using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using Sessionizing.Abstractions;

namespace Sessionizing.PageViewReaders
{
    internal class CsvPageViewReader : IPageViewReader
    {
        private static readonly CsvConfiguration s_csvConfig = new(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        
        private readonly string m_fileToRead;

        public CsvPageViewReader(string fileToRead)
        {
            m_fileToRead = fileToRead;
        }

        public IEnumerator<PageView> GetEnumerator()
        {
            using var streamReader = File.OpenText(m_fileToRead);
            using var csvReader = new CsvReader(streamReader, s_csvConfig);
            csvReader.Read();
            PageView pageViewsFromFile = csvReader.GetRecord<PageView>();
            while (pageViewsFromFile != null)
            {
                yield return pageViewsFromFile;
                pageViewsFromFile = csvReader.Read() ? csvReader.GetRecord<PageView>() : null;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
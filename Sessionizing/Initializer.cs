using System.Collections.Generic;
using System.Linq;
using NLog;
using Sessionizing.Abstractions;
using Sessionizing.PageViewReaders;
using Sessionizing.Providers;
using Unity;

namespace Sessionizing
{
    public class Initializer
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly UnityContainer m_unityContainer;

        public Initializer(UnityContainer unityContainer)
        {
            m_unityContainer = unityContainer;
        }

        public void Initialize(IReadOnlyCollection<string> filesToRead)
        {
            s_logger.Info($"Start initialize");
            m_unityContainer.RegisterInstance<ISessionsForSiteUrlController>(new SessionsForSiteUrlController());
            m_unityContainer.RegisterInstance<ISiteUrlsForVisitorController>(new SiteUrlsForVisitorController());
            m_unityContainer.RegisterInstance(new SessionsProvider(m_unityContainer.Resolve<ISessionsForSiteUrlController>()));
            m_unityContainer.RegisterType<ISessionCounter, SessionsProvider>();
            m_unityContainer.RegisterType<ISessionMedianLengthCalculator, SessionsProvider>();
            m_unityContainer.RegisterInstance<IUniqueVisitedSitesProvider>(
                new UniqueVisitedSitesProvider(m_unityContainer.Resolve<ISiteUrlsForVisitorController>()));
            m_unityContainer.RegisterType<IPageViewsInfoLoader, PageViewsInfoLoader>();

            IPageViewReader pageViewReader = CreatePageViewReadersController(filesToRead);
            m_unityContainer.Resolve<IPageViewsInfoLoader>().Load(pageViewReader);
            s_logger.Info($"Done initialize");
        }

        private static PageViewReadersController CreatePageViewReadersController(IReadOnlyCollection<string> filesToRead)
        {
            var pageViewReaders = new IPageViewReader[filesToRead.Count];
            for (int i = 0; i < filesToRead.Count; i++)
            {
                pageViewReaders[i] = new CsvPageViewReader(filesToRead.ElementAt(i));
            }

            return new PageViewReadersController(pageViewReaders);
        }
    }
}
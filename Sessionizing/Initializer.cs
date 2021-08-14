using System.Collections.Generic;
using NLog;
using Sessionizing.Abstractions;
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

        public void Initialize(IReadOnlyList<string> filesToRead)
        {
            s_logger.Info($"Start initialize");
            var pageViewsRepository = new InMemoryPageViewsRepository(filesToRead);
            m_unityContainer.RegisterInstance<IPageViewsRepository>(pageViewsRepository);
            m_unityContainer.RegisterType<ISessionCounter, SessionsProvider>();
            m_unityContainer.RegisterType<ISessionMedianLengthCalculator, SessionsProvider>();
            m_unityContainer.RegisterType<IUniqueVisitedSitesProvider, UniqueVisitedSitesProvider>();
            s_logger.Info($"Done initialize");
        }
    }
}
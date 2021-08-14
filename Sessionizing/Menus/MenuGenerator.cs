using System.Collections.Generic;
using Sessionizing.Abstractions;
using Sessionizing.Menus.MenuHandlers;
using Unity;

namespace Sessionizing.Menus
{
    public class MenuGenerator
    {
        private readonly UnityContainer m_unityContainer;

        public MenuGenerator(UnityContainer unityContainer)
        {
            m_unityContainer = unityContainer;
        }
        
        public Menu Generate()
        {
            IMenuHandler numSessionMenuHandler = new NumSessionsMenuHandler(m_unityContainer.Resolve<ISessionCounter>());
            IMenuHandler numUniqueVisitedSitesMenuHandler = new NumUniqueVisitedSitesMenuHandler(m_unityContainer.Resolve<IUniqueVisitedSitesProvider>());
            IMenuHandler sessionsMedianLengthMenuHandler = new SessionsMedianLengthMenuHandler(m_unityContainer.Resolve<ISessionMedianLengthCalculator>());

            Dictionary<MenuOption, IMenuHandler> menuHandlersMapping = new Dictionary<MenuOption, IMenuHandler>()
            {
                {MenuOption.NumSessions, numSessionMenuHandler},
                {MenuOption.NumUniqueVisitedSites, numUniqueVisitedSitesMenuHandler},
                {MenuOption.MedianSessionLength, sessionsMedianLengthMenuHandler},
            };

            return new Menu(menuHandlersMapping);
        }
    }
}
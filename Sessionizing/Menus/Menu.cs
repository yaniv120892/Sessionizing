using System;
using System.Collections.Generic;
using NLog;
using Sessionizing.Abstractions;

namespace Sessionizing.Menus
{
    public class Menu
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        private readonly Dictionary<MenuOption, IMenuHandler> m_menuStateHandlers;

        public Menu(Dictionary<MenuOption, IMenuHandler> menuStateHandlers)
        {
            m_menuStateHandlers = menuStateHandlers;
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    var menuOption = GetUserSelection();
                    if (menuOption == MenuOption.Exit)
                    {
                        return;
                    }
                    m_menuStateHandlers[menuOption].Handle();
                }
            }
            catch (Exception e)
            {
                s_logger.Error(e, "Got error");
            }
        }

        private static MenuOption GetUserSelection()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Sessionizing menu");
                Console.WriteLine("-------------------");

                MenuOption[] allMenuOptions = GetAllPossibleMenuOptions();
                foreach (MenuOption menuState in allMenuOptions)
                {
                    Console.WriteLine($"Press {(int) menuState} for {menuState}");
                }

                Console.WriteLine();
                Console.WriteLine("Enter your choice:");

                var input = Console.ReadLine();
                Console.WriteLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case (int) MenuOption.NumSessions:
                            return MenuOption.NumSessions;
                        case (int) MenuOption.MedianSessionLength:
                            return MenuOption.MedianSessionLength;
                        case (int) MenuOption.NumUniqueVisitedSites:
                            return MenuOption.NumUniqueVisitedSites;
                        case (int) MenuOption.Exit:
                            return MenuOption.Exit;
                    }
                }

                Console.WriteLine("Invalid Choice! - Please choose again");
            }
        }

        private static MenuOption[] GetAllPossibleMenuOptions()
        {
            return (MenuOption[]) Enum.GetValues(typeof(MenuOption));
        }
    }
}
using System;
using NLog;
using Sessionizing.Abstractions;
using Sessionizing.Exceptions;

namespace Sessionizing.Menus.MenuHandlers
{
    internal class SessionsMedianLengthMenuHandler : IMenuHandler
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        private readonly ISessionMedianLengthCalculator m_sessionMedianLengthCalculator;

        public SessionsMedianLengthMenuHandler(ISessionMedianLengthCalculator sessionMedianLengthCalculator)
        {
            m_sessionMedianLengthCalculator = sessionMedianLengthCalculator;
        }

        public void Handle()
        {
            Console.WriteLine("Enter site url");
            string siteUrl = Console.ReadLine();
            try
            {

                double medianSessionLengthInSeconds = m_sessionMedianLengthCalculator.Calculate(siteUrl);
                Console.WriteLine($"Median session length in seconds for {siteUrl} is {medianSessionLengthInSeconds}");
            }
            catch (UnknownSiteUrlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
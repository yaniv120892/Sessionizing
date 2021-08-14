using System;
using Sessionizing.Abstractions;

namespace Sessionizing.Menus.MenuHandlers
{
    internal class SessionsMedianLengthMenuHandler : IMenuHandler
    {
        private readonly ISessionMedianLengthCalculator m_sessionMedianLengthCalculator;

        public SessionsMedianLengthMenuHandler(ISessionMedianLengthCalculator sessionMedianLengthCalculator)
        {
            m_sessionMedianLengthCalculator = sessionMedianLengthCalculator;
        }

        public void Handle()
        {
            Console.WriteLine("Enter site url");
            string siteUrl = Console.ReadLine();
            double medianSessionLengthInSeconds = m_sessionMedianLengthCalculator.Calculate(siteUrl);
            Console.WriteLine($"Median session length in seconds for {siteUrl} is {medianSessionLengthInSeconds}");
        }
    }
}
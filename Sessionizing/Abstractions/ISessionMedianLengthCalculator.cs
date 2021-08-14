namespace Sessionizing.Abstractions
{
    internal interface ISessionMedianLengthCalculator
    {
        double Calculate(string siteUrl);
    }
}
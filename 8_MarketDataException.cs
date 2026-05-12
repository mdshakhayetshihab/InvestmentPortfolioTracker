public class MarketDataException : PortfolioException
{
  public string InvestmentName { get; }

  public MarketDataException(string investmentName, Exception innerException)
    : base($"Failed to fetch market data for '{investmentName}'", innerException)
  {
    InvestmentName = investmentName;
  }
}
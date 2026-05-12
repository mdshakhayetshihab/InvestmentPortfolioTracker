public class InsufficientFundsException : PortfolioException
{
  public double AvailableBalance { get; }
  public double RequiredAmount { get; }

  public InsufficientFundsException(double available, double required)
    : base($"Insufficient funds. Available: {available:N0}, Required: {required:N0}")
  {
    AvailableBalance = available;
    RequiredAmount = required;
  }
}

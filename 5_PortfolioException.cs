namespace Portfolio
{
  public class PortfolioException : Exception
  {
    public PortfolioException() : base("A Portfolio system error occurred.") {}
    public PortfolioException(string message) : base(message) {}
    public PortfolioException(string message, Exception innerException)
      : base(message, innerException) {}
  }
}

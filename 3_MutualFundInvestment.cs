class MutualFundInvestment : Investment
{
  private double _nav;
  private string _fundHouse;

  public double NAV
  {
    get { return _nav; }
    set
    {
      if(value <= 0)
        throw new InvalidInvestmentDataException("NAV", value);
      _nav = value;
    }
  }

  public string FundHouse
  {
    get { return _fundHouse; }
    set
    {
      if(string.IsNullOrWhiteSpace(value))
        throw new InvalidInvestmentDataException("FundHouse", value ?? "null");
      _fundHouse = value.Trim();
    }
  }

  public MutualFundInvestment(string name, double buyPrice, int quantity,
                               string fundHouse, double nav)
    : base(name, buyPrice, quantity)
  {
    FundHouse = fundHouse;
    NAV = nav;
  }

  public override double CalculateCurrentValue()
  {
    return NAV * Quantity;
  }

  public override void DisplayDetails()
  {
    Console.WriteLine($"[Mutual Fund] {InvestmentName} - {FundHouse}");
    Console.WriteLine($"  Buy Price : {BuyPrice:N0} x {Quantity} = {BuyPrice * Quantity:N0}");
    Console.WriteLine($"  NAV       : {NAV:N0} x {Quantity} = {CalculateCurrentValue():N0}");
    Console.WriteLine($"  Profit/Loss: {GetProfitLoss():N0}");
  }
}

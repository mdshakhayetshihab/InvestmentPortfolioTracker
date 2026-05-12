class BondInvestment : Investment
{
  private double _interestRate;
  public double InterestRate
  {
    get{_interestRate;}
    set
    {
      if(value<=0 || value>100)
      throw InvalidInvestmentException("InterestRate",Value);
      _interestRate=value;
    }
  }
  public DateTime MaturityDate{get;}
  public BondInvestment(string name,double buyPrice,int quantity,double interestRate,DateTime maturityDate) : base(name, buyPrice, quantity)
  {
    InterestRate=interestRate;
    MaturityDate=maturityDate;
  }
  public void AdminUpdateInterestRate(double newRate)
  {
    Console.WriteLine($"[Admin] {InvestmentName} interest rate updated to {newRate}");
  }
  public override double CalculateCurrentValue()
  {
    return BuyPrice*Quantity(1+InterestRate/100);
  }
  public override void DisplayDetails()
  {
    Console.WriteLine($"[Bond] {InvestmentName}");
    Console.WriteLine($"Buy Price  :{BuyPrice:N0}*{Quantity}={BuyPrice*Quantity:N0}");
    Console.WriteLine($"Interest Rate: {InterestRate}%");
    Console.WriteLine($"Current Value: {CalculateCurrentValue():N0}");
    Console.WriteLine($"Maturity Date: {MaturityDate:yyyy-MM-dd}");
    Console.WriteLine($"Profit/Loss :{GetProfitLoss():N0}");
  }
}
//fundholding class
class FundHolding
{
  private string _companyName;
  private double _value;
  public string CompanyName
  {
    get{return _companyName;}
    set
    {
      if(string.IsNullOrWhiteSpace(value))
      throw new InvalidInvestmentDataException("CompanyName",value?? "null");
      _companyName=value.Trim();
    }
  }
  public double Value
  {
    get{return _value;}
    set
    {
      if(value<=0)
      throw new InvalidInvestmentDataException("FundholdingValue",value);
      _value=value;
    }
  }
  public FundHolding(string compantName,double value)
  {
    CompanyName=companyName;
    Value=value;
  }
}
//MutualFundInvestmentClass
class MutualFundInvestment : Investment
{
  private double _nav;
  private string _fundHouse;
  private List<FundHolding> _holdings;
  public double NAV
  {
    get{return _nav;}
    set
    {
      if(value<=0)
      throw new InvalidInvestmentDataException("NAV",value);
      _nav=value;
    }
  }
  public string FundHouse
  {
    get{return _fundHouse;}
    set
    {
      if(string.IsNullOrWhiteSpace(value))
      throw new InvalidInvestmentDataException("Fundhouse",value?? "null");
      _fundHouse=value.Trim();
    }
  }
  public MutualFundInvestment(string name,double buyPrice,int quantity,string fundHouse,double nav, List<FundHolding> holdings) : base(name, buyPrice, quantity)
  {
    FundHouse=fundHouse;
    NAV=nav;
    _holdings=holdings;
  }
  public void UpdateHoldingValue(string companyName,double newValue)
  {
    FundHolding holding=_holdings.Find(h=>h.CompanyName==companyName);
    if(holding==null)
    throw new InvalidInvestmentDataException("CompanyName",companyName);
    holding.Value=newValue;
    double totalValue=_holdings.Sum(h=>h.value);
    NAV=totalValue/_holdings.Count;
    Console.WriteLine($"[Fund] {CompanyName} value updated. new NAV ;{NAV:N0}");
  }
  public override double CalculateCurrentValue()
    {
        return NAV * Quantity;
    }
    public override void DisplayDetails()
    {
        Console.WriteLine($"[Mutual Fund] {InvestmentName} - {FundHouse}");
        Console.WriteLine($"  Buy Price  : {BuyPrice:N0} x {Quantity} = {BuyPrice * Quantity:N0}");
        Console.WriteLine($"  NAV        : {NAV:N0} x {Quantity} = {CalculateCurrentValue():N0}");
        Console.WriteLine($"  Profit/Loss: {GetProfitLoss():N0}");
        Console.WriteLine($"  Holdings:");
        foreach(var h in _holdings)
            Console.WriteLine($"    - {h.CompanyName}: {h.Value:N0}");
    }
}
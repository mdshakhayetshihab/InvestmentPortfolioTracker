using System.Security.Cryptography.X509Certificates;

class StockInvestment
{
  private double _currentPrice;
  private string _tickerSymbol;
  public double CurrentPrice
  {
    get{return _currentPrice;}
    set
    {
      if(value<=0)
      throw new InvalidInvestmentDataException("CurrentPrice",value);
      _currentPrice=value;
    }
  }
  public string TickerSymbol
  {
    get{return _tickerSymbol;}
    set
    {
      if(string.IsNullOrWhiteSpace(value))
      throw new InvalidInvestmentDataException("TickerSymbol",value?? "null");
      _tickerSymbol=value.Trim();
    }
  }
  public StockInvestment(string name,double buyPrice,int quantity,string tickerSymbol,double CurrentPrice) : base(name, buyPrice, quantity)
  {
    TickerSymbol=tickerSymbol;
    CurrentPrice=currentPrice;
  }
  public void AdminUpdatePrice(double newPrice)
  {
    try
    {
      if(new Random().Next(0,4)==0)
      throw new Exception("Network timeout");
      CurrentPrice=newPrice;
      Console.WriteLine($"[Admin] {InvestmentName} price updated to {newPrice:N0}");
    }
    catch(Exception ex)
    {
      throw MarketDataException(InvestmentName,ex);
    }
  }
  public override double CalculateCurrentValue()
  {
    return CurrentPrice*Quantity;
  }
  public override void DisplayDetails()
  {
   Console.WriteLine($"[Stock] {InvestmentName} ({TickerSymbol})");
   Console.WriteLine($"Buy Price  :{BuyPrice:N0}*{Quantity}={BuyPrice*Quantity:N0}");
   Console.WriteLine($"Current Price: {CurrentPrice:N0}*{Quantity} = {CalculateCurrentValue():N0}");
   Console.WriteLine($" Profit/Loss : {GetProfitLoss():N0}"); 
  }
}
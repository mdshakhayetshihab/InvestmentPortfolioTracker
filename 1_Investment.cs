using System.Security.Cryptography.X509Certificates;

abstract class Investment
{
  private string _investmentName;
  private double _buyPrice;
  private int _quantity;
  public string InvestmentName
  {
    get{return _investmentName;}
    set
    {
      if(string.IsNullOrWhiteSpace(value))
      throw new InvalidInvestmentDataException("InvestmentName",value?? "null");
      _investmentName=value.Trim();
    }
  }
 public double BuyPrice
  {
    get{return _buyPrice;}
    set
    {
      if(value<=0)
      throw new InvalidInvestmentDataException("BuyPrice",value);
      _buyPrice=value;
    }
  }
public int Quantity
  {
    get{return _quantity;}
    set
    {
      if(value<=0)
      throw new InvalidInvestmentDataException("Quantity",value);
      _quantity=value;
    }
  }
  public DateTime PurchaseDate{get;}
  public Investment(string name,double buyPrice,int quantity)
  {
    InvestmentName=name;
    BuyPrice=buyPrice;
    Quantity=quantity;
    PurchaseDate=DateTime.Now;
  }
  public abstract double CalculateCurrentValue();
  public abstract void DisplayDetails();
  public double GetProfitLosss()
  {
    return CalculateCurrentValue() -(BuyPrice*Quantity);
  }
}
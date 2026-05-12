public class InvalidInvestmentDataException : PortfolioException
{
  public string FieldName { get; }
  public object InvalidValue { get; }

  public InvalidInvestmentDataException(string fieldName, object invalidValue)
    : base($"Invalid value for '{fieldName}' : '{invalidValue}'")
  {
    FieldName = fieldName;
    InvalidValue = invalidValue;
  }
}
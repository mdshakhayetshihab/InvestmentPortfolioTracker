using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Investment> investments = new List<Investment>();
    static double balance = 0;

    static void Main()
    {
        try
        {
            Console.Write("Enter your balance: ");
            balance = double.Parse(Console.ReadLine());
            if(balance <= 0)
                throw new InvalidInvestmentDataException("Balance", balance);
        }
        catch(InvalidInvestmentDataException ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
            return;
        }
        catch(Exception)
        {
            Console.WriteLine("[ERROR] Invalid balance!");
            return;
        }

        List<FundHolding> holdings = new List<FundHolding>
        {
            new FundHolding("Square Pharma", 15000),
            new FundHolding("BRAC Bank", 12000),
            new FundHolding("Walton", 18000)
        };

        try
        {
            AddToPortfolio(new StockInvestment("Grameenphone", 100, 200, "GP", 100));
            AddToPortfolio(new MutualFundInvestment("IDLC Growth Fund", 50, 300, "IDLC", 50, holdings));
            AddToPortfolio(new BondInvestment("Govt Bond 2025", 1000, 20, 8, new DateTime(2025, 12, 31)));
        }
        catch(InsufficientFundsException ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
            return;
        }

        bool running = true;
        while(running)
        {
            Console.WriteLine("\n=== Investment Portfolio Tracker ===");
            Console.WriteLine($"Available Balance: {balance:N0}");
            Console.WriteLine("1. Show all investments");
            Console.WriteLine("2. Show single investment");
            Console.WriteLine("3. Show multiple investments");
            Console.WriteLine("4. Add new investment");
            Console.WriteLine("5. Admin Panel (price update)");
            Console.WriteLine("6. Portfolio Summary");
            Console.WriteLine("7. Exit");
            Console.Write("\nEnter choice: ");

            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    ShowAll();
                    break;
                case "2":
                    ShowSingle();
                    break;
                case "3":
                    ShowMultiple();
                    break;
                case "4":
                    AddInvestment();
                    break;
                case "5":
                    AdminPanel();
                    break;
                case "6":
                    ShowSummary();
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("[ERROR] Invalid choice!");
                    break;
            }
        }
    }

    static void AddToPortfolio(Investment inv)
    {
        double cost = inv.BuyPrice * inv.Quantity;
        if(cost > balance)
            throw new InsufficientFundsException(balance, cost);
        balance -= cost;
        investments.Add(inv);
        Console.WriteLine($"[SUCCESS] {inv.InvestmentName} added! Remaining balance: {balance:N0}");
    }

    static void ShowAll()
    {
        Console.WriteLine("\n=== All Investments ===");
        Console.WriteLine(new string('=', 50));
        foreach(var inv in investments)
        {
            inv.DisplayDetails();
            Console.WriteLine(new string('-', 50));
        }
    }

    static void ShowSingle()
    {
        ShowInvestmentList();
        Console.Write("Enter number: ");
        if(int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= investments.Count)
        {
            Console.WriteLine(new string('=', 50));
            investments[index - 1].DisplayDetails();
        }
        else
        {
            Console.WriteLine("[ERROR] Invalid number!");
        }
    }

    static void ShowMultiple()
    {
        ShowInvestmentList();
        Console.WriteLine("\nTip: Enter numbers separated by comma");
        Console.WriteLine("Example: 1,3 will show investment 1 and 3");
        Console.Write("\nEnter numbers: ");
        string input = Console.ReadLine();
        string[] parts = input.Split(',');

        Console.WriteLine(new string('=', 50));
        foreach(var part in parts)
        {
            if(int.TryParse(part.Trim(), out int index) && index >= 1 && index <= investments.Count)
            {
                investments[index - 1].DisplayDetails();
                Console.WriteLine(new string('-', 50));
            }
            else
            {
                Console.WriteLine($"[ERROR] Invalid number: {part}");
            }
        }
    }

    static void AddInvestment()
    {
        Console.WriteLine("\n=== Add Investment ===");
        Console.WriteLine("1. Stock");
        Console.WriteLine("2. Mutual Fund");
        Console.WriteLine("3. Bond");
        Console.Write("Enter type: ");
        string type = Console.ReadLine();

        try
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Buy Price: ");
            double buyPrice = double.Parse(Console.ReadLine());

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            switch(type)
            {
                case "1":
                    Console.Write("Ticker Symbol: ");
                    string ticker = Console.ReadLine();
                    AddToPortfolio(new StockInvestment(name, buyPrice, quantity, ticker, buyPrice));
                    break;

                case "2":
                    Console.Write("Fund House: ");
                    string fundHouse = Console.ReadLine();
                    Console.Write("Number of holdings: ");
                    int holdingCount = int.Parse(Console.ReadLine());
                    List<FundHolding> holdings = new List<FundHolding>();
                    for(int i = 0; i < holdingCount; i++)
                    {
                        Console.Write($"  Company {i+1} name: ");
                        string company = Console.ReadLine();
                        Console.Write($"  Company {i+1} value: ");
                        double value = double.Parse(Console.ReadLine());
                        holdings.Add(new FundHolding(company, value));
                    }
                    AddToPortfolio(new MutualFundInvestment(name, buyPrice, quantity, fundHouse, buyPrice, holdings));
                    break;

                case "3":
                    Console.Write("Interest Rate (%): ");
                    double rate = double.Parse(Console.ReadLine());
                    Console.Write("Maturity Year: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Maturity Month: ");
                    int month = int.Parse(Console.ReadLine());
                    Console.Write("Maturity Day: ");
                    int day = int.Parse(Console.ReadLine());
                    AddToPortfolio(new BondInvestment(name, buyPrice, quantity, rate, new DateTime(year, month, day)));
                    break;

                default:
                    Console.WriteLine("[ERROR] Invalid type!");
                    break;
            }
        }
        catch(InsufficientFundsException ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
        catch(InvalidInvestmentDataException ex)
        {
            Console.WriteLine($"[ERROR] Invalid Data: {ex.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"[ERROR] {ex.Message}");
        }
    }

    static void AdminPanel()
    {
        Console.WriteLine("\n=== Admin Panel ===");
        ShowInvestmentList();
        Console.Write("Enter number: ");

        if(int.TryParse(Console.ReadLine(), out int index) && index >= 1 && index <= investments.Count)
        {
            var inv = investments[index - 1];
            try
            {
                if(inv is StockInvestment stock)
                {
                    Console.Write("New Current Price: ");
                    double newPrice = double.Parse(Console.ReadLine());
                    stock.AdminUpdatePrice(newPrice);
                }
                else if(inv is MutualFundInvestment mf)
                {
                    Console.Write("Company name to update: ");
                    string company = Console.ReadLine();
                    Console.Write("New value: ");
                    double newValue = double.Parse(Console.ReadLine());
                    mf.UpdateHoldingValue(company, newValue);
                }
                else if(inv is BondInvestment bond)
                {
                    Console.Write("New Interest Rate (%): ");
                    double newRate = double.Parse(Console.ReadLine());
                    bond.AdminUpdateInterestRate(newRate);
                }
            }
            catch(MarketDataException ex)
            {
                Console.WriteLine($"[ERROR] Market: {ex.Message}");
                Console.WriteLine($"  Caused by: {ex.InnerException?.Message}");
            }
            catch(InvalidInvestmentDataException ex)
            {
                Console.WriteLine($"[ERROR] Invalid Data: {ex.Message}");
            }
            catch(PortfolioException ex)
            {
                Console.WriteLine($"[ERROR] Portfolio: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("[ERROR] Invalid number!");
        }
    }

    static void ShowSummary()
    {
        double totalValue = investments.Sum(i => i.CalculateCurrentValue());
        double totalCost  = investments.Sum(i => i.BuyPrice * i.Quantity);
        double totalPL    = totalValue - totalCost;

        Console.WriteLine("\n=== Portfolio Summary ===");
        Console.WriteLine($"  Total Investments  : {investments.Count}");
        Console.WriteLine($"  Remaining Balance  : {balance:N0}");
        Console.WriteLine($"  Total Invested     : {totalCost:N0}");
        Console.WriteLine($"  Total Value        : {totalValue:N0}");
        Console.WriteLine($"  Total P/L          : {totalPL:N0}");
    }

    static void ShowInvestmentList()
    {
        Console.WriteLine("\nAvailable investments:");
        for(int i = 0; i < investments.Count; i++)
            Console.WriteLine($"  {i+1}. {investments[i].InvestmentName}");
    }
}
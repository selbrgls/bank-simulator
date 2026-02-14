using System;

// BUSINESS LOGIC
public class BankingService
{
    private double lastTransactionAmount = 0.0;

    // Pass-by-value
    public double CheckBalance(double balance)
    {
        return balance;
    }

    // ref
    public bool Deposit(ref double balance, double amount)
    {
        if (amount <= 0)
            return false;

        balance += amount;
        lastTransactionAmount = amount;
        return true;
    }

    // ref + out
    public void Withdraw(ref double balance, double amount, out bool success)
    {
        if (amount <= 0 || amount > balance)
        {
            success = false;
            return;
        }

        balance -= amount;
        lastTransactionAmount = amount;
        success = true;
    }

    // Pass-by-value
    public void GetMiniStatement(double balance, out double currentBalance, out double lastTransaction)
    {
        currentBalance = balance;
        lastTransaction = lastTransactionAmount;
    }
}


// USER INTERFACE
public class BankingView
{
    private static double balance = 1000.00;
    private static BankingService service = new BankingService();

    public static void Run()
    {
        Console.WriteLine("Hazel Brigoli");
        Console.WriteLine("Simple ATM System");
        Console.WriteLine();
        Console.WriteLine($"Initial Balance: PHP {balance:F2}");
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("1: Check Balance");
            Console.WriteLine("2: Deposit Money");
            Console.WriteLine("3: Withdraw Money");
            Console.WriteLine("4: Print Mini Statement");
            Console.WriteLine("5: Exit");
            Console.Write("Select an option: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Current Balance: PHP {service.CheckBalance(balance):F2}");
                    break;

                case 2:
                    Console.Write("Enter amount to deposit: ");
                    double depositAmount = Convert.ToDouble(Console.ReadLine());

                    if (!service.Deposit(ref balance, depositAmount))
                    {
                        Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
                        continue;
                    }

                    Console.WriteLine("Deposit successful.");
                    Console.WriteLine($"Updated Balance: PHP {balance:F2}");
                    break;

                case 3:
                    Console.Write("Enter amount to withdraw: ");
                    double withdrawAmount = Convert.ToDouble(Console.ReadLine());

                    bool success;
                    service.Withdraw(ref balance, withdrawAmount, out success);

                    if (!success)
                    {
                        if (withdrawAmount <= 0)
                            Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
                        else
                            Console.WriteLine("Withdrawal failed. Insufficient balance.");

                        continue;
                    }

                    Console.WriteLine("Withdrawal successful.");
                    Console.WriteLine($"Updated Balance: PHP {balance:F2}");
                    break;

                case 4:
                    double currentBalance, lastTransaction;
                    service.GetMiniStatement(balance, out currentBalance, out lastTransaction);

                    Console.WriteLine("--- Mini Statement ---");
                    Console.WriteLine($"Current Balance: PHP {currentBalance:F2}");
                    Console.WriteLine($"Last Transaction Amount: PHP {lastTransaction:F2}");
                    break;

                case 5:
                    Console.WriteLine("Thank you for using the ATM. Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option selected. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}


// PROGRAM ENTRY POINT
public class Program
{
    public static void Main(string[] args)
    {
        BankingView.Run();
    }
}

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

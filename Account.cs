using System;

public class Account
{
    private decimal _balance;
    private string _name;
    
    public string Name
    {
        get { return _name; }
    }

    public Account(string name, decimal startingBalance)
    {
        _name = name;
        _balance = startingBalance;
    }

    public Boolean Deposit(decimal amountToAdd)
    {
        if (amountToAdd > 0)
        {
            _balance = _balance + amountToAdd;
            return true;
        }
        else
            return false;
    }

    public Boolean Withdraw(decimal amountToWithdraw)
    {
        if ((_balance >= amountToWithdraw) && (amountToWithdraw > 0))
        {
            _balance = _balance - amountToWithdraw;
            return true;
        }
        else
            return false;
    }

    public void Print()
    {
        Console.WriteLine($"Your account: {_name} has a balance of ${_balance}.");
    }
}

using System;
using System.Collections.Generic;

public class Bank
{
    private List<Account> _accounts;
    private List<Transaction> _transactions;

    public Bank()
    {
        this._accounts = new List<Account>();
        this._transactions = new List<Transaction>();
    }
    
    public void AddAccount(Account account)
    {
        this._accounts.Add(account);
    }

    public Account? GetAccount(string name)  // all paths must return account
    {
        foreach(Account account in this._accounts)
        {
            if (account.Name.ToLower().Trim() == name.ToLower().Trim())
            {
                return account;
            }
        }
        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        transaction.Execute();
        this._transactions.Add(transaction);
    }
    
    public void PrintTransactionHistory()
    {
        foreach (Transaction transaction in this._transactions)
        {
            transaction.Print();
        }
    }

}
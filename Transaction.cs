using System;
using System.Collections.Generic;

public abstract class Transaction
{
    protected decimal _amount;
    protected bool _executed;
    protected bool _reversed;
    private DateTime _dateStamp;

    public bool Executed
    {
        get {return this._executed; }
    }
    public bool Reversed
    {
        get {return this._reversed; }
    }

    public DateTime DateStamp
    {
        get {return this._dateStamp;}
    }

    public abstract bool Succeeded
    {
        get;
    }

    public Transaction(decimal amount)
    {
        this._amount = amount;
    }

    public abstract void Print();
    
    public virtual void Execute()
    {
        if(this._executed)
        {
            throw new Exception("Cannot execute transaction as it has already been executed.");
        }
        this._executed = true;
        this._dateStamp = DateTime.Now;
    }

    public virtual void Rollback()
    {
        if (!this._executed)
        {
            throw new Exception("Cannot rollback this transaction as it has not been executed yet!");
        }
        
        if (this._reversed)
        {
            throw new Exception("Cannot rollback as transaction has already been rolled back!");
        }
    }
}
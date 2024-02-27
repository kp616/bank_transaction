using System;

public class DepositTransaction : Transaction
{
    private Account _account;
    private bool _succeeded = false;

    public override bool Succeeded
    {
        get 
        {
            return this._succeeded; //increases clarity, field is in the class
        }
    }

    public DepositTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }
    
    public override void Execute()
    {
        base.Execute();
        this._succeeded = _account.Deposit(this._amount);
    }

    public override void Rollback()
    {
        //Throw exception if trans has not been executed
        base.Rollback();

        if (this._account.Withdraw(this._amount))
        {
            this._reversed = true;
            this._executed = false;
            this._succeeded = false;
        }
        else
        {
            this._reversed = false;
            this._executed = true;
            this._succeeded = true;
        }
        //Throw exception if transaction has been reversed
    }

    public override void Print()
    {
        if(this._succeeded)
        {
            Console.WriteLine("You have deposited $" + this._amount + " into " + this._account.Name + "'s account! Deposit successful.");
        }
        else
        {
            Console.WriteLine("Deposit unsuccessful!");
            if (this._reversed)
            {
                Console.WriteLine("Deposit was reversed.");
            }
        }
    }

}
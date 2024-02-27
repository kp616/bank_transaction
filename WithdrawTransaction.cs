using System;

public class WithdrawTransaction : Transaction
{
    private Account _account;
    private bool _succeeded;

    public override bool Succeeded
    {
        get 
        {
            return this._succeeded; //increases clarity, field is in the class
        }
    }
    
    public WithdrawTransaction(Account account, decimal amount) : base(amount)
    {
        this._account = account;
    }
    
    public override void Execute()
    {
        base.Execute();
        this._succeeded = _account.Withdraw(this._amount);
    }

    public override void Rollback()
    {
        //Throw exception if trans has not been executed
        
       base.Rollback();
        
        if (this._account.Deposit(this._amount))
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
            Console.WriteLine("You have withdrawn $" + this._amount + " from " + this._account.Name + "'s account! Withdrawal successful.");
        }
        else
        {
            Console.WriteLine("Withdrawal unsuccessful!");
            if (this._reversed)
            {
                Console.WriteLine("Withdrawal was reversed.");
            }
        }
    }

}
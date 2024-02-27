using System;

public class TransferTransaction : Transaction
{
    private Account _fromAccount;
    private Account _toAccount;
    private WithdrawTransaction _theWithdraw;
    private DepositTransaction _theDeposit;

    public override bool Succeeded
    {
        get 
        {
            if (this._theWithdraw.Succeeded && this._theDeposit.Succeeded)
                return true;
            else
                return false;
        }
    }
    
    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        this._fromAccount = fromAccount;
        this._toAccount = toAccount;

        this._theWithdraw = new WithdrawTransaction(fromAccount, amount);
        this._theDeposit = new DepositTransaction(toAccount, amount);
    }
    
    public override void Execute()
    {
        base.Execute();

        this._theWithdraw.Execute();
        if (this._theWithdraw.Succeeded)
        {
            this._theDeposit.Execute();
            if (!this._theDeposit.Succeeded)
            {
                this._theWithdraw.Rollback();
            }
        

        }
        else
        {
            throw new Exception("Cannot process transaction. The withdraw transaction failed.");
        }
    }

    public override void Rollback()
    {
        base.Rollback();

        if (this._theWithdraw.Succeeded)
        {
            this._theWithdraw.Rollback();
        }
        
        if (this._theDeposit.Succeeded)
        {
            this._theDeposit.Rollback();
        }

        //to remember if transaction had been reversed
        if (this._theWithdraw.Reversed && this._theDeposit.Reversed)
        {
            this._reversed = true;
            this._executed = false;
        }
    }

    public override void Print()
    {
        if (this._theWithdraw.Succeeded && this._theDeposit.Succeeded)
        {
            Console.WriteLine("$" + this._amount + " has transfered from " + this._fromAccount + "'account to " + this._toAccount + "'s account. Transfer successful.");
            Console.WriteLine("     ");
            this._theWithdraw.Print();
            Console.WriteLine("     ");
            this._theDeposit.Print();
        }
        else
        {
            Console.WriteLine("Transfer was not successful.");
            if (this._reversed)
                Console.WriteLine("Transfer was reversed.");
        }
    }
}
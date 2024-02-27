using System;
using SplashKitSDK;

public enum MenuOption
{
    Add_Account,
    Withdraw,
    Deposit,
    Transfer,
    Print,
    Print_Transactions,
    Quit
}

public class Program
{
    public static void Main()
    {
        Bank bank = new Bank();
        MenuOption userSelection;
        
        do
        {
            userSelection = readUserOption();
            switch (userSelection)
            {
                case MenuOption.Add_Account:
                {
                    doAddAccount(bank);
                    break;
                }
                case MenuOption.Withdraw:
                {
                    dowithdraw(bank);
                    break;
                }
                case MenuOption.Deposit:
                {
                    doDeposit(bank);
                    break;
                }
                case MenuOption.Transfer:
                {
                    doTransfer(bank);
                    break;
                }
                case MenuOption.Print:
                {
                    doPrint(bank);
                    break;
                }
                case MenuOption.Print_Transactions:
                {
                    bank.PrintTransactionHistory();
                    break;
                }
                case MenuOption.Quit:
                {
                    Console.WriteLine("Quitting program.");
                    break;
                }
            }
        } while (userSelection != MenuOption.Quit);
    }

    private static void doAddAccount(Bank bank)
    {
        Console.Write("Please enter the account name: ");
        string accountName = Console.ReadLine();
        while (true)
        {
            try
            {
                Console.Write("Enter opening balance: ");
                decimal openingBalance;
                openingBalance = Convert.ToDecimal(Console.ReadLine());
                if (openingBalance < 0)
                {
                    throw new Exception();
                }
                bank.AddAccount(new Account(accountName, openingBalance));
                break;
            }
            catch
            {
                Console.WriteLine("Invalid opening balance. Please enter a valid balance.");
            }
        }
    }

    // readUserOption
    private static MenuOption readUserOption()
    {
        int option;
        Console.WriteLine("Please select from the following option [1-7]: ");
        Console.WriteLine("--------------------");
        Console.WriteLine("1: Add Account");
        Console.WriteLine("2: Withdraw");
        Console.WriteLine("3: Deposit");
        Console.WriteLine("4: Transfer");
        Console.WriteLine("5: Print");
        Console.WriteLine("6: Print Transaction History");
        Console.WriteLine("7: Quit");
        Console.WriteLine("--------------------");

        do
        {
            try
            {
                option = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("There was a problem parsing your selection. Try again.");
                option = -1;
            }
            if (option > 7 || option < 1)
            {
                Console.WriteLine("Please select a valid number between 1 and 7.");
            }
        } while (option > 7 || option < 1);
        return (MenuOption)(option - 1);
    }

    private static Account findAccount (Bank fromBank)
    {
        Console.Write("Enter account name: ");
        String name = Console.ReadLine();
        Account? result = fromBank.GetAccount(name);
        if (result == null)
        {
            Console.WriteLine($"No account found with name {name}.");
        }
        return result;
    }

    private static void dowithdraw(Bank bank)
    {
        Account account = findAccount(bank);
        if (account == null)
        {
            return;
        }
        decimal amount;
        Console.Write("How much would you like to withdraw from " + account.Name + "'s account? ");

        try
        {
            amount = Convert.ToDecimal(Console.ReadLine());
            WithdrawTransaction transaction = new WithdrawTransaction(account, amount);
            bank.ExecuteTransaction(transaction);
            
            if (!transaction.Succeeded)
            {
                throw new Exception("Withdraw was not successful.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void doDeposit(Bank bank)
    {
        Account account = findAccount(bank);
        
        if (account == null)
        {
            return;
        }

        decimal amount;
        Console.Write("How much would you like to deposit into " + account.Name + "'s account? ");

        try
        {
            amount = Convert.ToDecimal(Console.ReadLine());
            DepositTransaction transaction = new DepositTransaction(account, amount);
            //transaction.Execute();
            //transaction.Print();
            bank.ExecuteTransaction(transaction);
            if (!transaction.Succeeded)
            {
                throw new Exception("Deposit was not successful!!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void doTransfer(Bank bank)
    {
        try
        {
            Account fromAccount = findAccount(bank);
            if (fromAccount == null)
            {
                return;
            }
            Account toAccount = findAccount(bank);
            if (toAccount == null)
            {
                return;
            }
            if (toAccount == fromAccount)
            {
                throw new Exception("Same account!");
            }

            decimal amount;
            Console.Write("How much would you like to transfer into " + toAccount.Name + "'s account? ");
            
            try
            {
                amount = Convert.ToDecimal(Console.ReadLine());
                TransferTransaction transaction = new TransferTransaction(fromAccount, toAccount, amount);
                bank.ExecuteTransaction(transaction);
                if (!transaction.Succeeded)
                {
                    throw new Exception("Deposit was not successful!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }

    private static void doPrint(Bank bank)
    {
        Account account = findAccount(bank);
        
        if (account == null)
        {
            return;
        }
        account.Print();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

public class Bank
{
    private List<Client> clients;

    public Bank()
    {
        clients = new List<Client>();
    }

    public void AddClient(Client client)
    {
        clients.Add(client);
    }

    public void RemoveClient(Client client)
    {
        clients.Remove(client);
    }

    public void BlockAccount(Client client, int accountNumber)
    {
        Account account = client.GetAccount(accountNumber);
        if (account != null)
        {
            account.IsBlocked = true;
            Console.WriteLine($"Account {accountNumber} blocked.");
        }
        else
        {
            Console.WriteLine($"Account {accountNumber} not found.");
        }
    }

    public void UnblockAccount(Client client, int accountNumber)
    {
        Account account = client.GetAccount(accountNumber);
        if (account != null)
        {
            account.IsBlocked = false;
            Console.WriteLine($"Account {accountNumber} unblocked.");
        }
        else
        {
            Console.WriteLine($"Account {accountNumber} not found.");
        }
    }

    public List<Account> SearchAccounts(Client client, string keyword)
    {
        return client.Accounts.Where(account => account.AccountNumber.ToString().Contains(keyword)).ToList();
    }

    public List<Account> SortAccounts(Client client, bool ascending = true)
    {
        if (ascending)
        {
            return client.Accounts.OrderBy(account => account.AccountNumber).ToList();
        }
        else
        {
            return client.Accounts.OrderByDescending(account => account.AccountNumber).ToList();
        }
    }

    public decimal GetTotalBalance(Client client)
    {
        return client.Accounts.Sum(account => account.Balance);
    }

    public decimal GetPositiveBalanceTotal(Client client)
    {
        return client.Accounts.Where(account => account.Balance > 0).Sum(account => account.Balance);
    }

    public decimal GetNegativeBalanceTotal(Client client)
    {
        return client.Accounts.Where(account => account.Balance < 0).Sum(account => account.Balance);
    }
}

public class Client
{
    private List<Account> accounts;

    public string Name { get; set; }
    public List<Account> Accounts { get { return accounts; } }

    public Client(string name)
    {
        Name = name;
        accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public void RemoveAccount(Account account)
    {
        accounts.Remove(account);
    }

    public Account GetAccount(int accountNumber)
    {
        return accounts.FirstOrDefault(account => account.AccountNumber == accountNumber);
    }
}

public class Account
{
    public int AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }

    public Account(int accountNumber, decimal balance)
    {
        AccountNumber = accountNumber;
        Balance = balance;
        IsBlocked = false;
    }
}

public class Program
{
    public static void Main()
    {
        Bank bank = new Bank();

        // Create clients
        Client client1 = new Client("John");
        Client client2 = new Client("Alice");

        // Add accounts to clients
        client1.AddAccount(new Account(1, 1000));
        client1.AddAccount(new Account(2, -500));
        client2.AddAccount(new Account(3, 2000));
        client2.AddAccount(new Account(4, -1000));

        // Add clients to the bank
        bank.AddClient(client1);
        bank.AddClient(client2);

        // Block an account
        bank.BlockAccount(client1, 1);

        // Unblock an account
        bank.UnblockAccount(client2, 4);

        // Search accounts
        List<Account> searchResults = bank.SearchAccounts(client1, "2");
        Console.WriteLine("Search Results:");
        foreach (Account account in searchResults)
        {
            Console.WriteLine($"Account Number: {account.AccountNumber}, Balance: {account.Balance}");
        }

        // Sort accounts
        List<Account> sortedAccounts = bank.SortAccounts(client2, ascending: false);
        Console.WriteLine("Sorted Accounts:");
        foreach (Account account in sortedAccounts)
        {
            Console.WriteLine($"Account Number: {account.AccountNumber}, Balance: {account.Balance}");
        }

        // Calculate total balance
        decimal totalBalance = bank.GetTotalBalance(client1);
        Console.WriteLine($"Total Balance for {client1.Name}: {totalBalance}");

        // Calculate positive and negative balance totals
        decimal positiveBalanceTotal = bank.GetPositiveBalanceTotal(client2);
        decimal negativeBalanceTotal = bank.GetNegativeBalanceTotal(client2);
        Console.WriteLine($"Positive Balance Total for {client2.Name}: {positiveBalanceTotal}");
        Console.WriteLine($"Negative Balance Total for {client2.Name}: {negativeBalanceTotal}");
    }
}
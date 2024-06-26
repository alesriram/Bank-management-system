#BANK MANAGEMENT SYSTEM

using System;
using System.Collections.Generic;

namespace BankManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            while (true)
            {
                Console.WriteLine("\nBank Management System");
                Console.WriteLine("1. Add customer");
                Console.WriteLine("2. Open account");
                Console.WriteLine("3. Close account");
                Console.WriteLine("4. Deposit");
                Console.WriteLine("5. Withdraw");
                Console.WriteLine("6. Display customer information");
                Console.WriteLine("7. Display account transaction history");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        bank.AddCustomer();
                        break;
                    case "2":
                        bank.OpenAccount();
                        break;
                    case "3":
                        bank.CloseAccount();
                        break;
                    case "4":
                        bank.Deposit();
                        break;
                    case "5":
                        bank.Withdraw();
                        break;
                    case "6":
                        bank.DisplayCustomerInformation();
                        break;
                    case "7":
                        bank.DisplayAccountTransactionHistory();
                        break;
                    case "8":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                        break;
                }
            }
        }
    }

    class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
    }

    enum AccountType
    {
        Savings,
        Checking
    }

    class Account
    {
        public int AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();
    }

    class Transaction
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }

    class Bank
    {
        private List<Customer> customers = new List<Customer>();
        private int customerIdCounter = 1;
        private int accountNumberCounter = 1;

        public void AddCustomer()
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter customer address: ");
            string address = Console.ReadLine();

            Console.Write("Enter customer phone number: ");
            string phoneNumber = Console.ReadLine();

            customers.Add(new Customer
            {
                CustomerId = customerIdCounter++,
                Name = name,
                Address = address,
                PhoneNumber = phoneNumber
            });

            Console.WriteLine("Customer added successfully.");
        }

        public void OpenAccount()
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Customer customer = customers.Find(c => c.CustomerId == customerId);
            if (customer != null)
            {
                Console.WriteLine("Select account type:");
                Console.WriteLine("1. Savings");
                Console.WriteLine("2. Checking");
                Console.Write("Enter your choice: ");
                int accountTypeChoice = int.Parse(Console.ReadLine());

                AccountType accountType = (AccountType)(accountTypeChoice - 1);

                customer.Accounts.Add(new Account
                {
                    AccountNumber = accountNumberCounter++,
                    Type = accountType
                });
                Console.WriteLine("Account opened successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        public void CloseAccount()
        {
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            foreach (var customer in customers)
            {
                var accountToRemove = customer.Accounts.Find(a => a.AccountNumber == accountNumber);
                if (accountToRemove != null)
                {
                    customer.Accounts.Remove(accountToRemove);
                    Console.WriteLine("Account closed successfully.");
                    return;
                }
            }
            Console.WriteLine("Account not found.");
        }

        public void Deposit()
        {
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            foreach (var customer in customers)
            {
                var account = customer.Accounts.Find(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    Console.Write("Enter deposit amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());

                    account.Balance += amount;
                    account.TransactionHistory.Add(new Transaction
                    {
                        Date = DateTime.Now,
                        Description = "Deposit",
                        Amount = amount
                    });
                    Console.WriteLine("Deposit successful.");
                    return;
                }
            }
            Console.WriteLine("Account not found.");
        }

        public void Withdraw()
        {
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            foreach (var customer in customers)
            {
                var account = customer.Accounts.Find(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    Console.Write("Enter withdrawal amount: ");
                    decimal amount = decimal.Parse(Console.ReadLine());

                    if (amount > account.Balance)
                    {
                        Console.WriteLine("Insufficient funds.");
                        return;
                    }

                    account.Balance -= amount;
                    account.TransactionHistory.Add(new Transaction
                    {
                        Date = DateTime.Now,
                        Description = "Withdrawal",
                        Amount = amount
                    });
                    Console.WriteLine("Withdrawal successful.");
                    return;
                }
            }
            Console.WriteLine("Account not found.");
        }

        public void DisplayCustomerInformation()
        {
            Console.Write("Enter customer ID: ");
            int customerId = int.Parse(Console.ReadLine());

            Customer customer = customers.Find(c => c.CustomerId == customerId);
            if (customer != null)
            {
                Console.WriteLine($"Customer Name: {customer.Name}");
                Console.WriteLine($"Customer Address: {customer.Address}");
                Console.WriteLine($"Customer Phone Number: {customer.PhoneNumber}");
                Console.WriteLine("Accounts:");
                foreach (var account in customer.Accounts)
                {
                    Console.WriteLine($"Account Number: {account.AccountNumber}");
                    Console.WriteLine($"Type: {account.Type}");
                    Console.WriteLine($"Balance: {account.Balance:C}");
                }
            }
            else
            {
                Console.WriteLine("Customer not found.");
            }
        }

        public void DisplayAccountTransactionHistory()
        {
            Console.Write("Enter account number: ");
            int accountNumber = int.Parse(Console.ReadLine());

            foreach (var customer in customers)
            {
                var account = customer.Accounts.Find(a => a.AccountNumber == accountNumber);
                if (account != null)
                {
                    Console.WriteLine($"Transaction history for Account Number {accountNumber}:");
                    foreach (var transaction in account.TransactionHistory)
                    {
                        Console.WriteLine($"Date: {transaction.Date}, Description: {transaction.Description}, Amount: {transaction.Amount:C}");
                    }
                    return;
                }
            }
            Console.WriteLine("Account not found.");
        }
    }
}

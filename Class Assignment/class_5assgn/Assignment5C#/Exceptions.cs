using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5C_
{
    class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string message) : base(message) { }
    }

    class BankAccount
    {
        private int _accountNumber;
        private double _balance;

        public BankAccount(int accountNumber, double balance)
        {
            _accountNumber = accountNumber;
            _balance = balance;
        }

        public void FundTransfer(double amount)
        {
            if (amount <= _balance)
            {
                _balance = _balance - amount;
                Console.WriteLine("Transfer successfull! Balance: " + _balance);
            }
            else
            {
                throw new InsufficientBalanceException($"Entered Amount is Greater than Account Balance.Please Enter Amount Lesser than {_balance}");
            }
        }
    }
    internal class Exceptions
    {
        static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount(101, 20000);

            try
            {
                bankAccount.FundTransfer(21000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace CreditCardApp
{
    class CreditCard
    {
        private string cardNumber;
        private string ownerName;
        private string cvc;
        private DateTime expiryDate;
        private decimal balance;

        public string CardNumber
        {
            get => cardNumber;
            set
            {
                if (!Regex.IsMatch(value, @"^\d{16}$"))
                    throw new ArgumentException("Номер картки повинен містити 16 цифр.");

                cardNumber = value;
            }
        }

        public string OwnerName
        {
            get => ownerName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ПІБ власника не може бути порожнім.");

                ownerName = value;
            }
        }

        public string CVC
        {
            get => cvc;
            set
            {
                if (!Regex.IsMatch(value, @"^\d{3}$"))
                    throw new ArgumentException("CVC повинен містити 3 цифри.");

                cvc = value;
            }
        }

        public DateTime ExpiryDate
        {
            get => expiryDate;
            set
            {
                if (value < DateTime.Now)
                    throw new ArgumentException("Термін дії картки закінчився.");

                expiryDate = value;
            }
        }

        public decimal Balance
        {
            get => balance;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Баланс не може бути від’ємним.");

                balance = value;
            }
        }

        public CreditCard()
        {
            CardNumber = "1111222233334444";
            OwnerName = "Unknown";
            CVC = "000";
            ExpiryDate = DateTime.Now.AddYears(1);
            Balance = 0;
        }

        public CreditCard(string cardNumber,
                          string ownerName,
                          string cvc,
                          DateTime expiryDate,
                          decimal balance)
        {
            CardNumber = cardNumber;
            OwnerName = ownerName;
            CVC = cvc;
            ExpiryDate = expiryDate;
            Balance = balance;
        }

        public override string ToString()
        {
            return $"Номер картки: {CardNumber}\n" +
                   $"Власник: {OwnerName}\n" +
                   $"CVC: {CVC}\n" +
                   $"Термін дії: {ExpiryDate:MM/yyyy}\n" +
                   $"Баланс: {Balance} грн";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CreditCard card1 = new CreditCard(
                    "1234567812345678",
                    "Іван Петренко",
                    "123",
                    new DateTime(2028, 12, 31),
                    5000
                );

                Console.WriteLine(card1);

                Console.WriteLine("\n------------------\n");

                CreditCard card2 = new CreditCard(
                    "1234",  
                    "Олег Іванов",
                    "12",  
                    new DateTime(2020, 1, 1),
                    -100
                );

                Console.WriteLine(card2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }
}
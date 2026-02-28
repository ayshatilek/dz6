using System;

namespace StrategyPatternDemo
{]
    public interface IPaymentStrategy
    {
        void Pay(decimal amount);
    }

    public class CreditCardPayment : IPaymentStrategy
    {
        private string cardNumber;

        public CreditCardPayment(string cardNumber)
        {
            this.cardNumber = cardNumber;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата {amount} тг банковской картой {cardNumber}");
        }
    }

    public class PayPalPayment : IPaymentStrategy
    {
        private string email;

        public PayPalPayment(string email)
        {
            this.email = email;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата {amount} тг через PayPal аккаунт {email}");
        }
    }

    public class CryptoPayment : IPaymentStrategy
    {
        private string walletAddress;

        public CryptoPayment(string walletAddress)
        {
            this.walletAddress = walletAddress;
        }

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Оплата {amount} тг криптовалютой. Кошелек: {walletAddress}");
        }
    }

    public class PaymentContext
    {
        private IPaymentStrategy strategy;

        public void SetStrategy(IPaymentStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void ExecutePayment(decimal amount)
        {
            if (strategy == null)
            {
                Console.WriteLine("Стратегия оплаты не выбрана!");
                return;
            }

            strategy.Pay(amount);
        }
    }

    public class Program
    {
        public static void Main()
        {
            PaymentContext context = new PaymentContext();

            Console.WriteLine("Выберите способ оплаты:");
            Console.WriteLine("1 - Банковская карта");
            Console.WriteLine("2 - PayPal");
            Console.WriteLine("3 - Криптовалюта");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    context.SetStrategy(new CreditCardPayment("1234-5678-9999"));
                    break;
                case "2":
                    context.SetStrategy(new PayPalPayment("user@mail.com"));
                    break;
                case "3":
                    context.SetStrategy(new CryptoPayment("0xA1B2C3D4"));
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    return;
            }

            context.ExecutePayment(10000);
        }
    }
}

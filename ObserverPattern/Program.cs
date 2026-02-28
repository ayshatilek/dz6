using System;
using System.Collections.Generic;

namespace ObserverPatternDemo
{
    public interface IObserver
    {
        void Update(string currency, decimal rate);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public class CurrencyExchange : ISubject
    {
        private List<IObserver> observers = new List<IObserver>();
        private string currency;
        private decimal rate;

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void SetRate(string currency, decimal rate)
        {
            this.currency = currency;
            this.rate = rate;
            Notify();
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(currency, rate);
            }
        }
    }

    public class MobileApp : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"[Mobile] Новый курс {currency}: {rate}");
        }
    }

    public class WebApp : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            Console.WriteLine($"[Web] Обновление курса {currency}: {rate}");
        }
    }

    public class AnalyticsSystem : IObserver
    {
        public void Update(string currency, decimal rate)
        {
            if (rate > 500)
                Console.WriteLine($"[Analytics] Курс {currency} высокий! ({rate})");
            else
                Console.WriteLine($"[Analytics] Курс {currency} стабильный ({rate})");
        }
    }

    public class Program
    {
        public static void Main()
        {
            CurrencyExchange exchange = new CurrencyExchange();

            IObserver mobile = new MobileApp();
            IObserver web = new WebApp();
            IObserver analytics = new AnalyticsSystem();

            exchange.Attach(mobile);
            exchange.Attach(web);
            exchange.Attach(analytics);

            exchange.SetRate("USD", 480);
            Console.WriteLine();

            exchange.SetRate("EUR", 520);
            Console.WriteLine();

            exchange.Detach(web);

            exchange.SetRate("BTC", 30000000);
        }
    }
}

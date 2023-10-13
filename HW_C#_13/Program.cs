using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HW_C__13
{
    public delegate void Display(string mess);
    public class CreditCard
    {
        public event Display Notf;

        private string _number;
        private string _fullName;
        private DateTime _activeTo;
        private string _PIN;
        private int _limit;
        private int _credit;
        private int _cash;
        public bool credit;

        public CreditCard(string number, string fullName, DateTime activeTo, string PIN, int limit, int cash)
        {
            _number = number;
            _fullName = fullName;
            _activeTo = activeTo;
            _PIN = PIN;
            _limit = limit;
            _cash = cash;
            _credit = limit;
            credit = false;
        }
        public void add(int cash)
        {
            _cash += cash;
            Notf?.Invoke($"Cash added: {cash}");
        }
        public void remove(int cash)
        {
            
            if (credit)
            {
                if (cash > _credit)
                {
                    Notf?.Invoke($"Its below your credit limit!");
                }
                else
                {
                    _credit = cash;
                    Notf?.Invoke($"from your credit was removed: {cash}");
                }
            }
            else
            {
                if (cash > _cash)
                {
                    Notf?.Invoke($"You have not enough cash: {_cash}");
                }
                else
                {
                    _cash -= cash;
                    Notf?.Invoke($"from your cash was removed: {cash}");
                }
            }
        }
        public void creditStart()
        {
            credit = true;
            Notf?.Invoke($"Starting credit, limit: {_limit}");
        }
        public void changePIN(string PIN)
        {
            _PIN = PIN;
            Notf?.Invoke($"Your PIN was changed!");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            CreditCard test = new CreditCard("00000000", "Someone In Newada", new DateTime(2003, 01, 01), "0000", 10000, 1000);
            test.Notf += Current;

            test.add(1);
            test.remove(1);
            test.creditStart();
            test.changePIN("1111");
            test.remove(100001);

            void Current(string text)
            {
                Console.WriteLine(text);
            }
        }
    }
}

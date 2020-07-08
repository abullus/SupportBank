using System.Collections.Generic;
using NLog;

namespace SupportBank
{
    public class Person
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public float Balance;
        public readonly string Name;
        public readonly List<Transaction> Transactions;

        public Person(string name)
        {
            Name = name;
            Transactions = new List<Transaction>();
        }

        public void ChangeBalance(float value)
        {
            Balance += value;
            logger.Trace("Changed balance");
        }
    }
}
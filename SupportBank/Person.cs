using System.Collections.Generic;

namespace SupportBank
{
    public class Person
    {
        public float Balance;
        public string Name;
        public List<Transaction> Transactions;

        public Person(string name)
        {
            Name = name;
            Transactions = new List<Transaction>();
        }

        public void ChangeBalance(float value)
        {
            Balance += value;
        }
    }
}
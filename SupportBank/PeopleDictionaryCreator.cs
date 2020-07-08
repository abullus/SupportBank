using System;
using System.Collections.Generic;
using NLog;

namespace SupportBank
{
    public class PeopleDictionaryCreator
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public readonly Dictionary<string, Person> PeopleDictionary = new Dictionary<string, Person>();
        
        public void AddListToDict(List<Transaction> transactionList)
        {
            foreach (var thisTransaction in transactionList)
            {
                AddToDict(thisTransaction.From, -thisTransaction.Amount, thisTransaction);
                AddToDict(thisTransaction.To, thisTransaction.Amount, thisTransaction);
            }
        }
        
        private void AddToDict(string name, float amount, Transaction thisTransaction)
        {
            if (!PeopleDictionary.ContainsKey(name)) PeopleDictionary.Add(name, new Person(name));
            PeopleDictionary[name].ChangeBalance(amount);
            PeopleDictionary[name].Transactions.Add(thisTransaction);
        }
    }
}
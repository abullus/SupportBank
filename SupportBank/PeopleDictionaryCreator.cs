using System;
using System.Collections.Generic;
using NLog;

namespace SupportBank
{
    public class PeopleDictionaryCreator
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public static Dictionary<string, Person> PeopleDictionary;
        
        public PeopleDictionaryCreator()
        {
            var dict = new Dictionary<string, Person>();
            PeopleDictionary = dict;
            logger.Info("Created dictionary");
        }
        
        public static void AddToDict(string name, float amount, Transaction thisTransaction)
        {
            if (!PeopleDictionary.ContainsKey(name)) PeopleDictionary.Add(name, new Person(name));
            PeopleDictionary[name].ChangeBalance(amount);
            PeopleDictionary[name].Transactions.Add(thisTransaction);
        }
    }
}
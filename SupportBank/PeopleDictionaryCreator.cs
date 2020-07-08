using System;
using System.Collections.Generic;
using NLog;

namespace SupportBank
{
    public class PeopleDictionaryCreator
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public Dictionary<string, Person> PeopleDictionary;
        
        public PeopleDictionaryCreator()
        {
            var dict = new Dictionary<string, Person>();
            PeopleDictionary = dict;
            logger.Info("Created dictionary");
        }

        public void AddStringsToDictionary(string[] lines)
        {
            for (var i = 1; i < lines.Length; i++)
            {
                var lineArray = lines[i].Split(',');
                logger.Trace("Created an array for the line " + i);
                try
                {
                    var thisTransaction = new Transaction
                    {
                        Date = lineArray[0],
                        From = lineArray[1],
                        To = lineArray[2],
                        Narrative = lineArray[3],
                        Amount = float.Parse(lineArray[4])
                    };
                    logger.Trace("Created a transaction");
                    AddToDict(PeopleDictionary, thisTransaction.From, -thisTransaction.Amount, thisTransaction);
                    AddToDict(PeopleDictionary, thisTransaction.To, thisTransaction.Amount, thisTransaction);
                    logger.Debug("Line "+i+" added to dictionary");
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"\n\nError: {lineArray[4]} is not in a number format");
                    logger.Error($"Line {i} contains an amount that is not a number: \n{lineArray[4]}");
                }
            }
            logger.Info("All of CSV file added to dictionary");
        }

        private void AddToDict(Dictionary<string, Person> dict, string name, float amount, Transaction thisTransaction)
        {
            if (!dict.ContainsKey(name)) dict.Add(name, new Person(name));
            dict[name].ChangeBalance(amount);
            dict[name].Transactions.Add(thisTransaction);
        }
    }
}
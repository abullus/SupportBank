using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NLog;

namespace SupportBank
{
    public class JsonReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        private class JsonTransaction
        {
            public DateTime Date { get; set; }
            public string FromAccount { get; set; }
            public string ToAccount { get; set; }
            public string Narrative { get; set; }
            public float Amount { get; set; }
        }

        public List<Transaction> CreateTransactionList (string filename)
        {
            string jsonAsString = System.IO.File.ReadAllText(filename);
            var jsonTransactionList = JsonConvert.DeserializeObject<List<JsonTransaction>>(jsonAsString);
            List<Transaction> transactionList = new List<Transaction>();
            logger.Info($"Successfully read data from json file: {filename}");
            for (int i = 1; i < jsonTransactionList.Count; i++)
            {
                var thisTransaction = new Transaction
                {
                    Date = jsonTransactionList[i].Date.ToShortDateString(),
                    From = jsonTransactionList[i].FromAccount,
                    To = jsonTransactionList[i].ToAccount,
                    Narrative = jsonTransactionList[i].Narrative,
                    Amount = jsonTransactionList[i].Amount
                };
                
                transactionList.Add(thisTransaction);

            }
            logger.Info("All of JSON file added to list");
            return transactionList;
        }
    }
    
    
    
}
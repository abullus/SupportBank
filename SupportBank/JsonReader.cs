using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NLog;

namespace SupportBank
{
    public class JsonReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public DateTime Date { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public string Narrative { get; set; }
        public float Amount { get; set; }
        
        public void AddJSONFile (string filename)
        {
            string jsonAsString = System.IO.File.ReadAllText(filename);
            var jsonTransactionList = JsonConvert.DeserializeObject<List<JsonReader>>(jsonAsString);
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
                logger.Trace($"Created a transaction from data value {i} of json file");
                PeopleDictionaryCreator.AddToDict(thisTransaction.From, -thisTransaction.Amount, thisTransaction);
                PeopleDictionaryCreator.AddToDict(thisTransaction.To, thisTransaction.Amount, thisTransaction);
                logger.Debug($"Data value {i} added to dictionary");
            }
            logger.Info("All of JSON file added to dictionary");
        }
    }
    
    
    
}
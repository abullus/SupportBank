using System;
using System.Collections.Generic;
using System.Xml;
using NLog;

namespace SupportBank
{
    public class XmlReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();


        public List<Transaction> CreateTransactionList(string filename)
        {
            List<Transaction> transactionList = new List<Transaction>();
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes)
            {
                var thisTransaction = new Transaction
                {
                    Date = xmlNode.Value,
                    //From = jsonTransactionList[i].FromAccount,
                    To = jsonTransactionList[i].ToAccount,
                    Narrative = xmlNode.ChildNodes[0].InnerText,
                    Amount = jsonTransactionList[i].Amount
                };
                
                Console.WriteLine(xmlNode.ChildNodes[0].InnerText);
            }

            return transactionList;
        }

    }
}
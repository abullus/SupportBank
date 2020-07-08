using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace SupportBank
{
    internal class CsvReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public List<Transaction> CreateTransactionList (string filename)
        {
            string[] csvTransactionArray;
            try
            {
                csvTransactionArray = File.ReadAllLines(filename);
                logger.Info($"Succesfully read lines from csv file: {filename}");
            }
            catch (IOException)
            {
                csvTransactionArray = new string[0];
                Console.Write($"\n\nError: File could not be read:\n{filename}");
                logger.Error("File could not be read");
            }
            List<Transaction> transactionList = new List<Transaction>();
            for (var i = 1; i < csvTransactionArray.Length; i++)
            {
                var lineArray = csvTransactionArray[i].Split(',');
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
                    
                    transactionList.Add(thisTransaction);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"\n\nError: {lineArray[4]} is not in a number format");
                    logger.Error($"Line {i} contains an amount that is not a number: \n{lineArray[4]}");
                }
            }
            logger.Info("All of CSV file added to dictionary");
            return transactionList;
        }
    }
}
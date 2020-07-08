using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace SupportBank
{
    internal class CsvReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public void AddCSVFile (string filename)
        {
            string[] transactionList;
            try
            {
                transactionList = File.ReadAllLines(filename);
                logger.Info($"Succesfully read lines from csv file: {filename}");
            }
            catch (System.IO.IOException)
            {
                transactionList = new string[0];
                Console.Write($"\n\nError: File could not be read:\n{filename}");
                logger.Error("File could not be read");
            }

            for (var i = 1; i < transactionList.Length; i++)
            {
                var lineArray = transactionList[i].Split(',');
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
                    PeopleDictionaryCreator.AddToDict(thisTransaction.From, -thisTransaction.Amount, thisTransaction);
                    PeopleDictionaryCreator.AddToDict(thisTransaction.To, thisTransaction.Amount, thisTransaction);
                    logger.Debug("Line " + i + " added to dictionary");
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"\n\nError: {lineArray[4]} is not in a number format");
                    logger.Error($"Line {i} contains an amount that is not a number: \n{lineArray[4]}");
                }
            }

            logger.Info("All of CSV file added to dictionary");
        }
    }
}
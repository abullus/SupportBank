using System;
using System.Collections.Generic;
using System.IO;
using NLog;

namespace SupportBank
{
    internal class CsvReader
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public readonly string[] TransactionList;

        public CsvReader(string filename)
        {
            try
            {   
                TransactionList = File.ReadAllLines(filename);
                logger.Info($"Succesfully read lines from csv file: {filename}");
            }
            catch (System.IO.IOException)
            {
                TransactionList = new string[0];
                Console.Write($"\n\nError: File could not be read:\n{filename}");
                logger.Error("File could not be read");
            }
            
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    internal class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        private static void Main()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Training\SupportBank\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, target));
            LogManager.Configuration = config;
            logger.Info("Program Initiated");
            
            var peopleDictionaryCreator = new PeopleDictionaryCreator();
            var csvReader = new CsvReader();
            csvReader.AddCSVFile(@"C:\Work\Training\SupportBank\Transactions2014.csv");
            csvReader.AddCSVFile(@"C:\Work\Training\SupportBank\DodgyTransactions2015.csv");

            logger.Info("Added all data to dictionary");
            while (true)
            {
                Console.Write(
                    "\n\nType: \n'List All' To output all people and amounts \n'List [Account]' to to print all transactions \n");
                var input = Console.ReadLine().Split(" ").ToList();
                logger.Trace("Got user input: "+ input);
                if (input[0] == "List")
                {
                    input.Remove("List");
                    var inputName = string.Join(" ", input.ToArray());
                    
                    if (inputName == "All") PrintAllBalances(PeopleDictionaryCreator.PeopleDictionary);
                    else if (PeopleDictionaryCreator.PeopleDictionary.ContainsKey(inputName)) PrintTransactions(PeopleDictionaryCreator.PeopleDictionary[inputName]);
                    else Console.Write("\nPlease enter a valid name");
                }
                else Console.Write("\nPlease enter a valid command");
                logger.Info($"User input processed");
            }
        }

        private static void PrintAllBalances(Dictionary<string, Person> dict)
        {
            logger.Debug("PrintAllBalances called");
            Console.Write("Here are the balances for all people:");
            foreach (var i in dict)
            {
                var name = i.Key;
                var balance = i.Value.Balance;
                Console.Write($"\n \n{name}: £{balance}");
            }
        }

        private static void PrintTransactions(Person person)
        {
            logger.Debug($"PrintTransactions for: {person} called");
            Console.Write($"Here are the transactions for {person.Name}:");
            foreach (var transaction in person.Transactions)
                Console.Write("\n\nDate: " + transaction.Date +
                              "\nFrom: " + transaction.From +
                              "\nTo: " + transaction.To +
                              "\nNarrative: " + transaction.Narrative +
                              "\nValue: " + transaction.Amount);

            Console.Write("\n\nTheir current balance is £" + person.Balance);
        }
    }
}
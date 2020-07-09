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
            InitializeLogger();

            var peopleDictionaryCreator = new PeopleDictionaryCreator();
            var csvReader = new CsvReader();
            var jsonReader = new JsonReader();
            var xmlReader = new XmlReader();
            peopleDictionaryCreator.AddListToDict(jsonReader.CreateTransactionList(@"C:\Work\Training\SupportBank\Transactions2013.json"));
            peopleDictionaryCreator.AddListToDict(csvReader.CreateTransactionList(@"C:\Work\Training\SupportBank\Transactions2014.csv"));
            peopleDictionaryCreator.AddListToDict(csvReader.CreateTransactionList(@"C:\Work\Training\SupportBank\DodgyTransactions2015.csv"));
            peopleDictionaryCreator.AddListToDict(xmlReader.CreateTransactionList(@"C:\Work\Training\SupportBank\Transactions2012.xml"));
            
            logger.Info("Added all data to dictionary");
            
            while (true)
            {
                List<string> input =  GetUserInput();
                if (input[0] == "List")
                {
                    input.Remove("List");
                    var inputName = string.Join(" ", input.ToArray());
                    if (inputName == "All") PrintAllBalances(peopleDictionaryCreator.PeopleDictionary);
                    else if (peopleDictionaryCreator.PeopleDictionary.ContainsKey(inputName))
                        PrintTransactions(peopleDictionaryCreator.PeopleDictionary[inputName]);
                    else Console.Write("\nPlease enter a valid name");
                }
                else Console.Write("\nPlease enter a valid command");

                logger.Info("User input processed");
            }
        }

        private static List<string> GetUserInput()
        {
            Console.Write($"\n\nType: \n'List All' To output all people and amounts \n'List [Account]' to to print all transactions \n");
            var input = Console.ReadLine()?.Split(" ").ToList();
            logger.Trace("Got user input: " + input);
            return input;
        }

        private static void InitializeLogger()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget
            {
                FileName = @"C:\Work\Training\SupportBank\SupportBank.log",
                Layout = @"${longdate} ${level} - ${logger}: ${message}"
            };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, target));
            LogManager.Configuration = config;
            logger.Info("Program Initiated");
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
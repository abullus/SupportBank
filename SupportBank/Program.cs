using System;
using System.Collections.Generic;
using System.Linq;

namespace SupportBank
{
    internal class Program
    {
        private static void Main()
        {
            var filename = @"C:\Work\Training\SupportBank\Transactions2014.csv";
            var dict = new Csvreader(filename).DataFile;
            while (true)
            {
                Console.Write(
                    "\n\nType: \n'List All' To output all people and amounts \n'List [Account]' to to print all transactions \n");
                var input = Console.ReadLine().Split(" ").ToList();
                if (input[0] == "List")
                {
                    input.Remove("List");
                    var inputName = string.Join(" ", input.ToArray());
                    
                    if (inputName == "All") PrintAllBalances(dict);
                    else if (dict.ContainsKey(inputName)) PrintTransactions(dict[inputName]);
                    else Console.Write("\nPlease enter a valid name");
                }
                else Console.Write("\nPlease enter a valid command");
            }
        }

        private static void PrintAllBalances(Dictionary<string, Person> dict)
        {
            Console.Write("Here are the balances for all people:");
            foreach (var i in dict)
            {
                var name = i.Key;
                var balance = i.Value.Balance;
                Console.Write($"\n \n{name}: £{balance}");
            }
        }

        private static void PrintTransactions(Person name)
        {
            Console.Write($"Here are the transactions for {name.Name}:");
            foreach (var transaction in name.Transactions)
                Console.Write("\n\nDate: " + transaction.Date +
                              "\nFrom: " + transaction.From +
                              "\nTo: " + transaction.To +
                              "\nNarrative: " + transaction.Narrative +
                              "\nValue: " + transaction.Amount);

            Console.Write("\n\nTheir current balance is £" + name.Balance);
        }
    }
}
using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class Csvreader
    {
        public readonly Dictionary<string, Person> DataFile;

        public Csvreader(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var dict = new Dictionary<string, Person>();
            for (var i = 1; i < lines.Length; i++)
            {
                var lineArray = lines[i].Split(',');

                var thisTransaction = new Transaction
                {
                    Date = lineArray[0],
                    From = lineArray[1],
                    To = lineArray[2],
                    Narrative = lineArray[3],
                    Amount = float.Parse(lineArray[4])
                };
                AddToDict(dict, thisTransaction.From, -thisTransaction.Amount, thisTransaction);
                AddToDict(dict, thisTransaction.To, thisTransaction.Amount, thisTransaction);
            }

            DataFile = dict;
        }

        private void AddToDict(Dictionary<string, Person> dict, string name, float amount, Transaction thisTransaction)
        {
            if (!dict.ContainsKey(name)) dict.Add(name, new Person(name));
            dict[name].ChangeBalance(amount);
            dict[name].Transactions.Add(thisTransaction);
        }
    }
}
using System.Collections.Generic;

namespace SupportBank
{
    public class PeopleDictionaryCreator
    {
        public Dictionary<string, Person> PeopleDictionary;

        public PeopleDictionaryCreator()
        {
            var dict = new Dictionary<string, Person>();
            PeopleDictionary = dict;
        }

        public void AddStringsToDictionary(string[] lines)
        {
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
                AddToDict(PeopleDictionary, thisTransaction.From, -thisTransaction.Amount, thisTransaction);
                AddToDict(PeopleDictionary, thisTransaction.To, thisTransaction.Amount, thisTransaction);
            }
        }

        private void AddToDict(Dictionary<string, Person> dict, string name, float amount, Transaction thisTransaction)
        {
            if (!dict.ContainsKey(name)) dict.Add(name, new Person(name));
            dict[name].ChangeBalance(amount);
            dict[name].Transactions.Add(thisTransaction);
        }
    }
}
using System;

namespace SupportBank
{
    class Person
    {
        public string name;
        public void printTransactions(string[,] transactions)
        {
            for (int i = 1; i < transactions.GetLength(0); i++)
            {
                var transactionOccurred = false;
                string personMessage = "";
                if (transactions[i, 1] == name)
                {
                    transactionOccurred = true;
                    personMessage = $"\nRecipient {transactions[i, 2]}";
                }

                if (transactions[i, 2] == name)
                {
                    transactionOccurred = true;
                    personMessage = $"\nSender {transactions[i, 2]}";
                }
                if (transactionOccurred)
                {
                    string date = transactions[i, 0];
                    string narrative = transactions[i, 3];
                    string amount = transactions[i, 4];
                    Console.Write($"\n\nDate: {date}"+personMessage+$"\nAmount: {amount} \nNarrative: {narrative}");
                }
            }
        }
    }
}
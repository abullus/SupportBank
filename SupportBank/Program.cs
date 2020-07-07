﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = @"C:\Work\Training\SupportBank\Transactions2014.csv"; 
            string[,] transactions2014 = LoadCSV(filename);
            List<string> namesList = GenerateNameList(transactions2014);
            while (true)
            {
                Console.Write("\nType: \n'List All' To output all people and amounts \n'List [Account]' to to print all transactions \n");
                var input  = Console.ReadLine();
                string[] inputList = input.Split(" ");
                if (inputList[0] == $"List")
                    if (inputList[1] == "All")
                    {
                        PrintAccounts(transactions2014, namesList);
                    }

                if (inputList.Contains(inputList[1]))
                {
                    // Print their history
                }
                
                
            }
        }

        private static List<string> GenerateNameList(string[,] transactions)
        {
            List<string> namesList = new List<string>();
            for (int i = 1; i < transactions.GetLength(0); i++)
            {
                // Add names to list if not already present
                if (!namesList.Contains(transactions[i,1])) namesList.Add(transactions[i,1]);
            }
            return namesList;
        }

        private static void PrintAccounts(string[,] transactions, List<string> namesList)
        {
            // Initialize a blank dictionary
            Dictionary<string, float> accountsDict = new Dictionary<string, float>();
            foreach (var name in namesList)
            {
                accountsDict.Add(name,0);
            }
            // Iterate through all the transactions
            for (int i = 1; i < transactions.GetLength(0); i++)
            {
                // Initialize values for current transaction
                string fromPerson = transactions[i, 1];
                string toPerson = transactions[i, 2];
                float amount = float.Parse(transactions[i, 4]);
                // Adjust values in account according to transaction
                accountsDict[fromPerson] = accountsDict[fromPerson] - amount;
                accountsDict[toPerson] = accountsDict[toPerson] + amount;
            }

            foreach (var entry in accountsDict)
            {
                Console.Write("\n {0} has an account value of {1}",entry.Key,entry.Value);
            }
        }
        private static string[,] LoadCSV(string filename)
        {
            {
                // Get the data
                string[] lines = System.IO.File.ReadAllLines(filename);
                // See how many rows and columns there are.
                int numRows = lines.Length;
                int numCols = lines[0].Split(',').Length;

                // Allocate the data array.
                string[,] values = new string[numRows, numCols];

                // Load the array.
                for (int r = 0; r < numRows; r++)
                {
                    string[] lineR = lines[r].Split(',');
                    for (int c = 0; c < numCols; c++)
                    {
                        values[r, c] = lineR[c];
                    }
                }

                // Return the values.
                return values;
            }
        }
    }
}

using System.Collections.Generic;
using System.IO;

namespace SupportBank
{
    internal class CsvReader
    {
        public readonly string[] TransactionList;

        public CsvReader(string filename)
        {
            TransactionList = File.ReadAllLines(filename);
        }
    }
}
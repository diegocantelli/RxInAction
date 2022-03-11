using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.SystemReactive
{
    public class StockInfo
    {
        public StockInfo(string symbol, decimal price)
        {
            Symbol = symbol;
            Price = price;
        }

        public string Symbol { get; private set; }
        public decimal Price { get; private set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.SystemReactive
{
    public class StockMonitor
    {
        public StockMonitor(StockTicker ticker)
        {
            ticker.StockTick += OnStockTick;
        }

        private void OnStockTick(object sender, StockTick e)
        {
            Console.WriteLine("Executa alguma lógica");
        }
    }
}

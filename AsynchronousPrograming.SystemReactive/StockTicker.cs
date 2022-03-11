using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.SystemReactive
{
    public class StockTicker
    {
        public event EventHandler<StockTick> StockTick;
    }
}

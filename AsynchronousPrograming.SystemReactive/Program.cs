using System;
using System.Reactive.Linq;

namespace AsynchronousPrograming.SystemReactive
{
    class Program
    {
        static void Main(string[] args)
        {
            //StockTicker ticker = new StockTicker();

            //var ticks = Observable.FromEventPattern<EventHandler<StockTick>, StockTick>(
            //    h => ticker.StockTick += h,
            //    h => ticker.StockTick -= h)
            //    .Select(tickEvent => tickEvent.EventArgs);

            //from tick in ticks
            //group tick by tick.QuoteSymbol into company
            //from tickPair in company.Buffer(2, 1)
            //let changeRatio = Math.Abs((tickPair[1].Price - tickPair[0].Price));

            Console.ReadLine();
        }
    }
}

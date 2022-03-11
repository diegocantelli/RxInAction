using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxInAction
{
    public class UtilizandoOperadorDo
    {
        public IObservable<int> DoObservable()
        {
            return Observable.Range(1, 5)
                .Do(x => Console.WriteLine("Valores emitidos pelo operador Do", x))
                .Where(x => x % 2 == 0)
                .Do(x => Console.WriteLine("Valores filtrados pelo Where", x))
                .Select(x => x * 3);
        }
    }
}

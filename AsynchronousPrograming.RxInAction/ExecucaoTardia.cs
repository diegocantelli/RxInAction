using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxInAction
{
    public class ExecucaoTardia
    {
        public IObservable<int> ObservableExecucaoTardia()
        {
            return Observable.Range(1, 5)
                .DelaySubscription<int>(TimeSpan.FromSeconds(3)); //Atrasa o subscribe em 3 segundos
        }

        public IObservable<DateTimeOffset> ObservableEmissaoComFimProgramado()
        {
            return Observable.Timer(DateTimeOffset.Now, TimeSpan.FromSeconds(1))
                .Select(x => DateTimeOffset.Now)
                .TakeUntil(DateTimeOffset.Now.AddSeconds(5)); //irá pegar os valores até que passe 5 segundos, após isso o observable será completado
        }
    }
}

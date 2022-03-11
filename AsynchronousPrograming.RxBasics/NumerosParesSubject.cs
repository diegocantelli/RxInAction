using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxBasics
{
    public class NumerosParesSubject : IDisposable
    {
        //private readonly Subject<int> subject = new Subject<int>();

        //ReplaySubject -> Re-emite os valores emitidos para observer que tenham se cadastrado em um momento futuro
        //em que é executado o OnNext e finalizado a emissão através do OnComplete
        //Neste exemplo, ao usar Subject, só serão exibidos os valores no console caso o subscribe seja feito antes
        //do método Run, para que o subject/Observer possa emitir os valores através do OnNext
        //Com o uso do ReplaySubject, o subscribe pode ser feito em qualquer momento, mesmo após o Run, pois no momento
        // em que for realizado o subscribe, os valores serão reemitidos.
        //private readonly ReplaySubject<int> subject = new ReplaySubject<int>();

        //Tem o mesmo comportamento de ReplaySubject, mas emite/reemite apenas o último valor
        //O Parâmetro no construtor é o valor default que o Subject irá emitir caso nenhum outro valor tenha sido 
        //Emitido ainda
        private readonly BehaviorSubject<int> subject = new BehaviorSubject<int>(0);

        public readonly List<IDisposable> disposables = new List<IDisposable>();

        public void Run()
        {
            Enumerable
                .Range(1, 100)
                .Where(x => x % 2 == 0)
                .ToList()
                .ForEach(x => subject.OnNext(x));
        }
        public void Dispose()
        {
            subject?.Dispose();

            foreach (var item in disposables)
                item?.Dispose();
        }

        public void Subscribe(Action<int> action)
        {
            //subject.Subscribe(action) -> retorn um IDisposable
            disposables.Add(subject.Subscribe(action));
        }
    }
}

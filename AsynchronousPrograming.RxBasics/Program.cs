using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AsynchronousPrograming.RxBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            //UsingSubject();
            //ConvertingToObservable();
            //EmitindoValoresComSubject();
            //CriandoUmaObservableViaCreate();
            //CriandoUmaObservableComTimer();

            var numeroParSubject = new NumerosParesSubject();
            //Subscribe(Console.WriteLine) -> Console.WriteLine será passado como parâmetro da subject privada que 
            //exite dentro do método Subscribe, cujo o retorno de subject.Subscribe(action) será adicionado na lista
            //de disposables
            //numeroParSubject.Subscribe(Console.WriteLine);

            //será executada a action dentro de subject.subscribe(action), que é o console.writeline da linha acima
            //Neste método é executado o método OnNext, caso não exista nenhum observer cadastrado, ela irá executar
            //sem exibir nada, a menos que seja usado o tipo ReplaySubject
            numeroParSubject.Run();

            numeroParSubject.Subscribe(Console.WriteLine);


            Console.ReadLine();

            numeroParSubject.Dispose();
        }

        static void UsingSubject()
        {
            var subject = new UsingSubject()
                .GetNewSubject();

            var subscription = subject
                .Subscribe(
                    (int i) => Console.WriteLine("Subscription 1 "+i),
                    (error) => Console.WriteLine("Erro Subscription 1: " + error.Message),
                    () => Console.WriteLine("Fim do processamento Subscription 1.")
                );

            Console.WriteLine("---------------------------------------------------------");
            var subscription2 = subject
                .Subscribe(
                    (int i) => Console.WriteLine("Subscription 2 " + i),
                    (error) => Console.WriteLine("Erro Subscription 2: " + error.Message),
                    () => Console.WriteLine("Fim do processamento Subscription 2.")
                );

            //cada subscription irá emitir os valores emitidos pelo OnNext()
            subject.OnNext(1);
            subject.OnNext(4);
            subject.OnNext(5);

            Console.ReadLine();
            subscription.Dispose();
            subscription2.Dispose();
        }

        static void Observers()
        {
            var observable = Observable.Range(5, 8);

            //Passa para o subscribe o interessado em receber as notificações, o "Observador".
            //Este objeto deve implementar IObserver e seus métodos
            //Dentro de Observer está especificada a lógica que deverá ocorrer assim que ele for notificado
            //pela observable
            var subscription = observable.Subscribe(new CustomObserver());

            var observer2 = Observer.Create<int>(
                (int i) => Console.WriteLine("Segundo observer: " + i),
                (error) => Console.WriteLine("Erro no processamento." + error.Message),
                () => Console.WriteLine("Fim do processamento"));

            Console.WriteLine("------------------------------------------");

            var subscription2 = observable.Subscribe(observer2);

            Console.ReadLine();

            subscription.Dispose();
        }

        static void ConvertingToObservable()
        {
            //Agora é possível passar um observer como parâmetro do subscribe
            var observable = new[] { "Olá", "Mundo" }.ToObservable();

            var subs = observable.Subscribe(
                (string value) => Console.WriteLine(value),
                (error) => Console.WriteLine(error),
                () => Console.WriteLine("Fim do processamento"));

            Console.ReadLine();

            subs.Dispose();
        }

        static void EmitindoValoresComSubject()
        {
            var subject = new Subject<string>();
            //var observable = new[] { "Olá", "Mundo" }.ToObservable();

            var subs = subject.Subscribe(Console.WriteLine);

            subject.OnNext("Olá");
            subject.OnNext("Mundo");
            subject.OnNext("!!!");

            Console.ReadLine();

            subs.Dispose();
        }

        static void CriandoUmaObservableViaCreate()
        {
            var observable = Observable.Create<int>(observer =>
            {
                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        observer.OnNext(i);
                    }

                    observer.OnCompleted();
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }

                return Disposable.Empty;
            });

            var subs = observable.Subscribe(
                Console.WriteLine, 
                err => Console.WriteLine(err.Message), 
                () => Console.WriteLine("Sucesso!"));
        }
        static void CriandoUmaObservableComTimer()
        {
            var periodo = TimeSpan.FromMilliseconds(500);

            //cria uma observable que gera os valores de 1 a 4 de meio em meio segundo
            var observable = Observable
                .Timer(periodo, periodo)
                .Skip(1)
                .Take(4);

            var subs = observable.Subscribe(Console.WriteLine);

            Console.ReadLine();

            subs.Dispose();
        }
    }
}

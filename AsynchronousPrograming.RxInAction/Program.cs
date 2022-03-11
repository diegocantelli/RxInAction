using System;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxInAction
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var numbers = new NumbersObservable(5).SubscribeConsole<int>("numbers");
            //var observable = EnumerableToObservable<string>.AsObservable(new[] { "Olá", "Mundo" });
            //observable.SubscribeConsole<string>("names");

            //var readFileObervable = new ReadFileObservable().ReadFile("file.txt");

            //readFileObervable.SubscribeConsole("reading file");

            #region Uso do Observer.Create e inscrevendo um observer em mais de uma observable
            //var observer1 = new CreatingObservers().CreateObserver<string>(Console.WriteLine);
            //Observable
            //    .Interval(TimeSpan.FromSeconds(1))
            //    .Select(x => "Observer subscribe 1 -> " + x)
            //    .Subscribe(observer1);

            //Observable
            //    .Interval(TimeSpan.FromSeconds(2))
            //    .Select(x => "Observer subscribe 2 -> " + x)
            //    .Subscribe(observer1);

            #endregion

            #region Criando uma observble de execução tardia
            //var observer = new ExecucaoTardia()
            //    .ObservableExecucaoTardia()
            //    .SubscribeConsole("Execução tardia");
            #endregion

            #region Observable com fim de emissao programado
            //var observable = new ExecucaoTardia().ObservableEmissaoComFimProgramado()
            //    .SubscribeConsole("Emissao fim programado");
            #endregion

            #region TakeUntil com base nos valores de outra Observable
            //var observableMsgs = new[] { "mensagem 1", "2mensagem 2" }.ToObservable();
            //var observableMsgsControl = new[] { "mensagem 1", "Stop", "Stop" }.ToObservable();

            //observableMsgs
            //    .TakeUntil(observableMsgsControl.Where(x => x.Equals("Stop"))) //Irá emitir os valores da observableMsgs áté que na observableMsgsControl seja emitido o valor Stop
            //    .Subscribe(x => Console.WriteLine(x));
            #endregion

            #region SkipUntil: Emite valores só após que um determinado valor seja emitido
            //var observableMsgs = new[] { "mensagem 1", "2mensagem 2", "Msg após start" }.ToObservable();
            //var observableMsgsControl = new[] { "mensagem 1", "Start" }.ToObservable();

            //observableMsgs
            //    .SkipUntil(observableMsgsControl.Where(x => x.Equals("Start"))) //Irá emitir os valores da observableMsgs apenas após que a observableMsgsControl emita o valor Start
            //    .Subscribe(x => Console.WriteLine(x));
            #endregion

            #region Utilizando o operador Do
            //var observable = new UtilizandoOperadorDo()
            //    .DoObservable()
            //    .SubscribeConsole("Final Operador Do");
            #endregion

            #region Usando Subject
            //var sbj = new Subject<int>();
            //sbj.SubscribeConsole("First"); //observer 1
            //sbj.SubscribeConsole("Second"); //observer 2

            //sbj.OnNext(1);
            //sbj.OnNext(2);
            #endregion

            #region Inscrevendo o Subject em múltiplas observables
            //Subject<string> sbj = new Subject<string>();

            ////A primeira observable que completar, irá interromper a emissão das demais e finalizará a emissão de novos valores
            //Observable.Interval(TimeSpan.FromSeconds(1))
            //    .Select(x => "First: " + x)
            //    .Take(5)
            //    .Subscribe(sbj);

            //Observable.Interval(TimeSpan.FromSeconds(2))
            //    .Select(x => "Second: " + x)
            //    .Take(5)
            //    .Subscribe(sbj);

            //sbj.SubscribeConsole();

            #endregion

            #region Utilizando Behavior Subject
            //BehaviorSubject<int> behaviorSbj = new BehaviorSubject<int>(0); //Behavior Subject guarda em cache o último valor emitido, para que possa ser re-transmitido para os observers que se inscreveram após a sua emissão
            //behaviorSbj.SubscribeConsole("1 observer");
            //behaviorSbj.OnNext(1);
            //behaviorSbj.OnNext(2);
            //behaviorSbj.SubscribeConsole("2 observer"); //Este observable irá exibir apenas o último valor emitido pela observable caso não tenha sido emitido nenhum valor após o subscribe da mesma
            //Console.WriteLine("Último valor emitido: " + behaviorSbj.Value);//behaviorSbj.Value -> armazena o último valor emitido pelo behavior subject
            #endregion
            #region Utilizando ReplaySubject
            //ReplaySubject<int> replaySbj = new ReplaySubject<int>();
            //replaySbj.OnNext(1);
            //replaySbj.OnNext(2);
            //replaySbj.SubscribeConsole("Replay Subject"); //Serão exibidos inclusive os itens que foram emitidos antes do Subscribe
            //replaySbj.OnNext(6);
            #endregion

            #region ColdObservables -> Emitem os valores apenas quando é feito o subscribe
            //var coldObservable = Observable.Create<string>(async x => 
            //{
            //    x.OnNext("Olá");
            //    await Task.Delay(TimeSpan.FromSeconds(1));
            //    x.OnNext("Rx");
            //});
            //coldObservable.SubscribeConsole("Observer 1");
            //await Task.Delay(TimeSpan.FromSeconds(0.5));
            //coldObservable.SubscribeConsole("Observer 2");

            #endregion

            #region Convertendo uma cold observable em uma hot observable
            //var coldObservable = Observable.Interval(TimeSpan.FromSeconds(1)).Take(5);
            //var connectableObservable = coldObservable.Publish();
            //connectableObservable.SubscribeConsole("Primeira");
            //connectableObservable.SubscribeConsole("Segunda");
            //connectableObservable.Connect(); //conecta a coldObservable com um subject interno
            //Thread.Sleep(2000);
            //connectableObservable.SubscribeConsole("Terceira"); //Mesmo após 2 segundos, os mesmos valores emitidos via os subscribes anteriores, serão reemitidos para este subscribe
            #endregion

            #region Utilizando SelectMany com Observables
            //var obs = new UsingSelectMany()
            //    .GetTelefones()
            //                                 //x -> o tipo ClienteTelefone,         
            //                                     //y-> a propriedade que está sendo extraída 
            //    .SelectMany(x => x.Telefones, (x, y) => new 
            //    { 
            //        Nome = x.Nome, 
            //        Length = y.Length,
            //        Telefone = y //y-> o próprio telefone
            //    }) //retorna os elementos de uma lista interna a um objeto, se for uma lista de objetos que cada um possua uma lista, retornará essa lista interna de forma unificada "flattened"
            //    .Where(x => x.Length > 1)
            //    .Select(x => new { Nome = x.Nome, Telefone = x.Telefone })
            //    .Subscribe(Console.WriteLine);
            #endregion

            #region Utilizando Distinct para filtrar valores
            //var obs = new UsingSelectMany()
            //    .GetTelefones()
            //    .SelectMany(x => x.Telefones, (x, y) => new
            //    {
            //        Nome = x.Nome,
            //        Length = y.Length,
            //        Telefone = y //y-> o próprio telefone
            //    }) //retorna os elementos de uma lista interna a um objeto, se for uma lista de objetos que cada um possua uma lista, retornará essa lista interna de forma unificada "flattened"
            //    .Where(x => x.Telefone.Length > 1)
            //    .Distinct(x => x.Telefone)
            //    .Subscribe(Console.WriteLine);
            #endregion

            #region Utilizando DistinctUntilChanged
            //var TxtSearch = new object(); //representaria um campo de pesquisa em um formulário
            //Observable.FromEventPattern(TxtSearch, "TextChanged") //cria uma observable com base no evento TextChanged do textbox
            //    .Select(_ => TxtSearch) //retornaria o texto dentro do TxtSearch
            //    .Throttle(TimeSpan.FromMilliseconds(400)) //Aguarda 400ms até que seja enviado o termo para  o próximo item do pipeline
            //    .DistinctUntilChanged() //só irá avançar no pipeline caso o termo digitado seja diferente do termo anterior
            //    .Subscribe(x => Console.WriteLine(x)); //efetua alguma ação
            #endregion

            #region Utilizando MaxBy
            //var obs = new UsingSelectMany()
            //    .GetTelefones()
            //    .MaxBy(x => x.Salario) //Retorna uma lista com o objeto que possui o maior salário
            //    .SelectMany(x => x) //Remove o elemento com o maior salário da lista para exibição
            //    .Subscribe(Console.WriteLine);
            #endregion

            #region Utilizando operador Aggregate
            //Observable.Range(1, 5)
            //    //O acumulador será inicializado com 1
            //    .Aggregate(1, (acumulador, itemAtual) => itemAtual + acumulador)//só emitirá o valor após ter sido somado todos os valores
            //    .SubscribeConsole("Aggregate");
            #endregion

            #region Utilizando operador Scan
            //Observable.Range(1, 5)
            //    //O acumulador será inicializado com 0
            //    .Scan(0, (acumulador, itemAtual) => itemAtual + acumulador)//para cada iteração emitirá o valor da soma
            //    .SubscribeConsole("Scan");
            #endregion

            #region Zip -> COmbinando observables 
            //IObservable<int> obs1 = new[] { 1, 2, 3 }.ToObservable<int>();
            //IObservable<int> obs2 = new[] { 4, 5, 6 }.ToObservable<int>();

            //obs1
            //    .Zip(obs2, (itemObs1, itemObs2) => itemObs1 + itemObs2) //combina o resultado das 2 observables somando os itens de mesma posição/índice. Caso exista diferença ta taxa de emissão dos itens entre uma api e outra, o Zip irá aguardar a emissão da mais lenta para poder realizar a operação
            //    .SubscribeConsole("Zip");
            #endregion

            #region Utilizando CombineLatest
            //Subject<int> heartRate = new Subject<int>();
            //Subject<int> speed = new Subject<int>();

            ////Irá combinar os últimos elementos emitidos por cada observable, desde que as observables tenham emitido pelo menos um valor, caso contrário, aquelas que já emitiram ficarão no aguardo daquelas que não emitiram
            //speed
            //    .StartWith(0) //Força para que seja emitido pelo menos um valor inicial
            //    .CombineLatest(heartRate.StartWith(0),
            //    (s, h) => String.Format("Heart:{0} Speed:{1}", h, s))
            //    .SubscribeConsole("Batimentos cardíacos");
            //heartRate.OnNext(150);
            //heartRate.OnNext(151);
            //heartRate.OnNext(152);
            //speed.OnNext(30);
            //speed.OnNext(31);
            //heartRate.OnNext(153);
            //heartRate.OnNext(154);
            #endregion

            #region Utilizando o operador Concat
            //Task<string[]> facebookMessages =
            //    Task.Delay(1000)
            //        .ContinueWith(_ => new[] { "Facebook1", "Facebook2" });

            //Task<string[]> twitterStatuses =
            //    Task.FromResult(new[] { "Twitter1", "Twitter2" });

            ////Irá emitir primeiro os valores já emitidos, não importando a ordem de execução
            //Observable.Merge(
            //    facebookMessages.ToObservable(),
            //    twitterStatuses.ToObservable())
            //.SelectMany(messages => messages)
            //.SubscribeConsole("Merged Messages");
            #endregion

            #region Utilizando merge e controle de concorrência
            //IObservable<string> first =
            //    Observable.Interval(TimeSpan.FromSeconds(1))
            //    .Select(i => "First" + i)
            //    .Take(2);

            //IObservable<string> second =
            //     Observable.Interval(TimeSpan.FromSeconds(1))
            //    .Select(i => "Second" + i)
            //    .Take(2);

            //IObservable<string> third = Observable.Interval(TimeSpan.FromSeconds(1))
            //     .Select(i => "Third" + i)
            //     .Take(2);

            //new[] { first, second, third }.ToObservable()
            //    .Merge(2)//indica que no máximo 2 observables podem ser subscritas ao mesmo tempo. A primeira que finalizar, dará lugar para última se inscrever. Ao final serão exibidos os valores das três observables
            //    .SubscribeConsole("Merge com max de 2 subscrptions");
            #endregion

            #region Usando o operador switch
            //var textsSubject = new Subject<string>();
            //IObservable<string> texts = textsSubject.AsObservable();
            //texts
            //    .Select(txt => Observable.Return(txt + "-Result")
            //    .Delay(TimeSpan.FromMilliseconds(txt == "R1" ? 10 : 0)))
            //    .Switch()//Dentre todas as observables, irá retornar o resultado da que completar primeiro e depois a mais lenta
            //    .SubscribeConsole("Merging from observable");

            //textsSubject.OnNext("R1"); //irá demorar 10ms, enquanto isso a R2 será chamada e irá retornar seu valor imediatamente, tendo seu valor exibido no console
            //textsSubject.OnNext("R2");
            //Thread.Sleep(20);
            //textsSubject.OnNext("R3");

            #endregion

            #region Utilizando o operador Amb
            //var server1 =
            //    Observable.Interval(TimeSpan.FromSeconds(0.5))
            //        .Select(i => "Server1-" + i);

            //var server2 =
            //    Observable.Interval(TimeSpan.FromSeconds(1))
            //        .Select(i => "Server2-" + i);

            //Observable.Amb(server1, server2) //pega apenas o resultado da observable que retornou mais rápido e descarta as demais
            //    .Take(3)
            //    .SubscribeConsole("Amb");
            #endregion

            #region Agrupando dados
            //var dataset = new DadosExemplo().GetDadosPessoa();
            //var mediaGeneroIdade =
            //    from genero in dataset.GroupBy(x => x.Genero)
            //    from media in dataset.Average(x => x.Idade)
            //    select new { Genero = genero.Key, Media = media }
            //    ;

            //mediaGeneroIdade.SubscribeConsole("Média idade por gênero");
            #endregion

            #region Parametrizando a thread
            //Console.WriteLine("Before - Thread: {0}",
            //Thread.CurrentThread.ManagedThreadId);
            ////CurrentThreadScheduler.Instance -> executa a observable na mesma thread em que ela está sendo chamada, no mesmo contexto
            //Observable.Interval(TimeSpan.FromSeconds(1), CurrentThreadScheduler.Instance)
            //    .Take(3)
            //    .Subscribe(x => Console.WriteLine("Inside - Thread: {1}",
            //    x,
            //Thread.CurrentThread.ManagedThreadId));
            #endregion

            #region Usando Throttle
            //var observable = Observable
            //    .Return("Update A")
            //    .Concat(Observable.Timer(TimeSpan.FromSeconds(2)).Select(_ => "Update B"))
            //    .Concat(Observable.Timer(TimeSpan.FromSeconds(1)).Select(_ => "Update C"))
            //    .Concat(Observable.Timer(TimeSpan.FromSeconds(1)).Select(_ => "Update D"))
            //    .Concat(Observable.Timer(TimeSpan.FromSeconds(3)).Select(_ => "Update E"));

            ////Caso ó próximo valor seja emitido num intervalor menor que 2 segundos, o valor atual será ignorado
            ////No caso acima só serão exibidos os valores: Update A, D e E
            //observable.Throttle(TimeSpan.FromSeconds(2))
            //    .SubscribeConsole("Throttle");
            #endregion

            #region Controlando o contexto de execução da thread referente ao observer
            //Observable.FromEventPattern(TextBox, "TextChanged")
            //.Select(_ => TextBox.Text)
            //.Throttle(TimeSpan.FromMilliseconds(400))
            //.ObserveOn(DispatcherScheduler.Current)//Informa que o observer será executado na mesma thread na qual foi invocado. Na UI Thread por exemplo
            //.Subscribe(t => ThrottledResults.Items.Add(t)); //Como o observer está na mesma thread da UI, sera possível adicionar itens ao textbox
            #endregion

            #region Tratamento de erros 
            //IObservable<int> errorSimulation =
            //    Observable.Throw<int>(new OutOfMemoryException());

            //errorSimulation
            //    .Catch((OutOfMemoryException ex) =>
            //    {
            //        Console.WriteLine("Tratando exceção.");
            //        return Observable.Empty<int>();
            //    })
            //    .SubscribeConsole("Catch: ");
            #endregion

            #region Utilizando OnErrorResumeNext
            //IObservable<WeatherReport> weatherStationA =
            //Observable.Throw<WeatherReport>(new OutOfMemoryException());
            //IObservable<WeatherReport> weatherStationB =
            // Observable.Return<WeatherReport>(new WeatherReport()
            // {
            //     Station = "B",
            //     Temperature = 20.0
            // });
            //weatherStationA
            // .OnErrorResumeNext(weatherStationB) //caso a observable weatherStationA dê erro, é chamada a próxima observable
            // .SubscribeConsole("OnErrorResumeNext(source throws)");
            #endregion

            #region Utilizano o operador Retry
            IObservable<string> weatherStationA =
                Observable.Throw<string>(new OutOfMemoryException());

            weatherStationA
                .Do(x => Console.WriteLine("Tenta se conectar à API."))
                .Retry(3) //Em caso de API indisponível, será feita 3 novas tentativas além da primeira que originou o erro
                .SubscribeConsole("Retry");
            #endregion
            Console.ReadLine();

        }
    }
}

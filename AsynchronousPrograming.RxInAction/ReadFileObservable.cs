using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxInAction
{
    public class ReadFileObservable
    {
        public IObservable<string> ReadFile(string path)
        {
            return Observable.Using( //abre o arquivo, mas liberando-o após o seu uso
                () => File.OpenText(path), 
                stream =>
                    Observable.Generate(
                        stream, //Estado inicial, aponta para o próprio arquivo, para o início dele
                        s => !s.EndOfStream, //Condição para que o arquivo continue sendo lido
                        s => s, //retorna o estado atual da stream, no caso a linha que está sendo lida
                        s => s.ReadLine() //lê a linha
                     ));
        }
    }
}

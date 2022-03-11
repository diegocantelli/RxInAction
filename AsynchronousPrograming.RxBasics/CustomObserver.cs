using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxBasics
{
    public class CustomObserver : IObserver<int>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Completed!");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Ocorreu um erro: " + error.Message);
        }

        public void OnNext(int value)
        {
            Console.WriteLine(value);
        }

    }
}

using System;
using System.Reactive;

namespace AsynchronousPrograming.RxInAction
{
    public class CreatingObservers
    {
        public IObserver<T> CreateObserver<T>(Action<T> action)
        {
            return Observer.Create<T>(x => action(x));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxInAction
{
    public class EnumerableToObservable<T>
    {
        public static IObservable<T> AsObservable(IEnumerable<T> enumerable)
        {
            return enumerable.ToObservable();
        }
    }
}

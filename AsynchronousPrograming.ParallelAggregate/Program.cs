using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchronousPrograming.ParallelAggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> values = new List<int> { 1, 2, 3, 4 };
            var result = ParallelSum(values);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        static int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();
            int result = 0;
            Parallel.ForEach(source: values,
                localInit: () => 0,
                body: (item, state, localValue) => localValue + item,
                localFinally: localValue =>
                {
                    lock (mutex)
                    {
                        result += localValue;
                    }
                });
            return result;
        }

        static int ParallelSumWithLINQ(IEnumerable<int> values)
        {
            return values.AsParallel().Sum();
        }

        static int ParallelSumLINQAggregate(IEnumerable<int> values)
        {
            return values.AsParallel().Aggregate(
                seed: 0,
                func: (sum, item) => sum + item);
        }

        static IEnumerable<int> ParallelSumLINQ(IEnumerable<int> values)
        {
            return values.AsParallel().Select(x => x * 1);
        }

        //Irá executar na ordem em que se encontram na lista
        static IEnumerable<int> ParallelSumLINQOrdered(IEnumerable<int> values)
        {
            return values.AsParallel().AsOrdered().Select(x => x * 1);
        }
    }
}

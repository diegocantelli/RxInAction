using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchronousPrograming.AsyncIEnumerable
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var result = GetValuesAsync().ConfigureAwait(false);
            //await foreach (var item in result)
            //{
            //    Console.WriteLine("Item: " + item);
            //}

            var values = SlowRange().WhereAwait(async value => 
            {
                await Task.Delay(10);
                return value % 2 == 0;
            });

            await foreach (var item in values)
            {
                Console.WriteLine("Values: " + item);
            }
            Console.ReadLine();
        }

        static async IAsyncEnumerable<int> GetValuesAsync()
        {
            await Task.Delay(1000);
            yield return 10;

            await Task.Delay(1000);
            yield return 20;
        }

        static async IAsyncEnumerable<int> SlowRange()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(i * 10);
                yield return i;
            }
        }
    }
}

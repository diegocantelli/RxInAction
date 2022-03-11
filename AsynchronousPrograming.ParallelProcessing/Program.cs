using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsynchronousPrograming.ParallelProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Produto> produtos = new List<Produto>
            {
                new Produto { Nome = "prod 1", Preco = 1, Qtd = 2},
                new Produto { Nome = "prod 2", Preco = 5, Qtd = 5},
                new Produto { Nome = "prod 3", Preco = 7, Qtd = 3},
                new Produto { Nome = "prod 4", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 5", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 6", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 7", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 8", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 9", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 10", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 11", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 12", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 13", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 14", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 15", Preco = 10, Qtd = 2},
                new Produto { Nome = "prod 16", Preco = 10, Qtd = 2}
            };

            var prod = produtos.FirstOrDefault();

            Parallel.Invoke(prod.CalculaTotal, GeraBoletoPedido);
            //CalculaTotal(produtos);
            ParallelOptions op = new ParallelOptions();
            op.MaxDegreeOfParallelism = 4;

            Stopwatch timer = new Stopwatch();
            timer.Start();
            Parallel.ForEach(produtos, produto => produto.CalculaTotal());
            timer.Stop();
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Tempo total de execução: " + timer.Elapsed.TotalSeconds);

            Console.ReadLine();
        }

        static void GeraBoletoPedido()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Boleto gerado com sucesso!");
        }
    }

    class Produto
    {
        public string Nome { get; set; }
        public int Qtd { get; set; }
        public int Preco { get; set; }
        public int ValorTotal { get; set; }

        public void CalculaTotal()
        {
            Thread.Sleep(5000);
            ValorTotal = Preco * Qtd;
            Console.WriteLine(this.Nome + " " + this.ValorTotal + "Thread Id: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsynchronousPrograming.RxInAction
{
    public class DadosExemplo
    {
        public IObservable<ClienteTelefone> GetDadosPessoa()
        {
            var listTelefones = new List<ClienteTelefone>
            {
                new ClienteTelefone { Nome = "Diego", Idade = 20, Genero = "M", Telefones = new List<string>{"123", "2", "789"}, Salario = 100},
                new ClienteTelefone { Nome = "João", Idade = 30, Genero = "M", Telefones = new List<string>{"1", "789", "123"}, Salario = 200},
                new ClienteTelefone { Nome = "Maria", Idade = 30, Genero = "F", Telefones = new List<string>{"1", "789", "123"}, Salario = 200},
            }.ToObservable<ClienteTelefone>();

            return listTelefones;
        } 
    }

    public class ClienteTelefone
    {
        public string Nome { get; set; }
        public IEnumerable<string> Telefones { get; set; }
        public string Genero { get; set; }
        public int Idade { get; set; }
        public ClienteTelefone()
        {
            Telefones = new List<string>();
        }
        public decimal Salario { get; set; }

        public override string ToString()
        {
            return $"Nome: {Nome}, Salário: {Salario}";
        }
    }
}

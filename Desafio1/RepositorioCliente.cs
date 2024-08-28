using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class RepositorioCliente : IRepositorio<Cliente, string>
    {
        List<Cliente> clientes = new List<Cliente>();
        public Cliente Add(Cliente item)
        {
            clientes.Add(item);
            return item;
        }

        public Cliente Alterar(Cliente item)
        {
            clientes.RemoveAt(clientes.FindIndex(c => c.cpf == item.cpf));
            clientes.Add(item);
            return item;
        }

        public Cliente Consultar(string cpf)
        {
            return clientes.Find(item => item.cpf == cpf);
        }

        public IEnumerable<Cliente> Consultar()
        {
            return clientes;
        }

        public bool Remover(Cliente item)
        {
            clientes.RemoveAt(clientes.FindIndex(c => c.cpf == item.cpf));
            return true;
        }
    }
}

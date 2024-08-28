using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class RepositorioProduto : IRepositorio<Produto, int>
    {
        List<Produto> produtos = new List<Produto>();
        public Produto Add(Produto item)
        {
            item.Id = produtos.Count;
            produtos.Add(item);
            return item;
        }

        public Produto Alterar(Produto item)
        {
            throw new NotImplementedException();
        }

        public Produto Consultar(int id)
        {
            return produtos.Find(item => item.Id == id);
        }

        public IEnumerable<Produto> Consultar()
        {
            return produtos;
        }

        public bool Remover(Produto item)
        {
            produtos.RemoveAt(produtos.FindIndex(p => p.Id == item.Id));
            return true;
        }
    }
}

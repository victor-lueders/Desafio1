using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class RepositorioCompra : IRepositorio<Compra, int>
    {
        List<Compra> compraList = new List<Compra>();
        public Compra Add(Compra item)
        {
            item.Id = compraList.Count;
            compraList.Add(item);
            return item;
        }

        public Compra Alterar(Compra item)
        {
            throw new NotImplementedException();
        }

        public Compra Consultar(int id)
        {
            return compraList.Find(item => item.Id == id);
        }

        public IEnumerable<Compra> Consultar()
        {
            return compraList;
        }

        public bool Remover(Compra item)
        {
            compraList.RemoveAt(compraList.FindIndex(c => c.Id == item.Id));
            return true;
        }
    }
}

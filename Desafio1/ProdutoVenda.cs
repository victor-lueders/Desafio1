using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class ProdutoVenda
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int CompraId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public ProdutoVenda(Produto produto, int quantidade)
        {
            this.Produto = produto;
            this.Quantidade = quantidade;
        }

        public ProdutoVenda() { }
    }
}

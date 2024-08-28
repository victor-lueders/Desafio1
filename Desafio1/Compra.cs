using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class Compra
    {
        public int Id { get; set; }
        public IEnumerable<ProdutoVenda> Produtos { get; set; }

        public Cliente Cliente { get; set; }

        public FormaPagamento Pagamento { get; set; }
        public double ValorTotal { get; set; }

        public Compra() { }

        public Compra(Cliente cliente, FormaPagamento pagamento, IEnumerable<ProdutoVenda> produtos)
        {
            this.Cliente = cliente;
            this.Pagamento = pagamento;
            this.Produtos = produtos;
            this.ValorTotal = 0;

            foreach(var produto in produtos)
            {
                ValorTotal += (produto.Produto.Valor * produto.Quantidade);
            }
        }

        public Compra(FormaPagamento pagamento, IEnumerable<ProdutoVenda> produtos)
        {
            this.Pagamento = pagamento;
            this.Produtos = produtos;
            this.ValorTotal = 0;

            foreach (var produto in produtos)
            {
                ValorTotal += (produto.Produto.Valor * produto.Quantidade);
            }
        }

        public void Imprimir()
        {
            Console.WriteLine("A compra de id " + Id + " é a seguinte");
            Console.WriteLine();
            foreach(var produto in Produtos)
            {
                Console.WriteLine($"{produto.Quantidade} {produto.Produto.Descricao} - Preço unitário: R${produto.Produto.Valor}");
            }
            Console.WriteLine();
            Console.WriteLine("O valor total é R$" + ValorTotal);
            Console.WriteLine("Forma de pagamento: " + Pagamento.Descricao);
            Console.WriteLine($"Sua pontuacao é {Cliente.Pontuacao}. Restam {10 - Cliente.Pontuacao} para uma refeição livre.");
        }
    }
}

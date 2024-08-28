namespace Desafio1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioCliente clientes = new RepositorioCliente();
            RepositorioProduto produtos = new RepositorioProduto();
            RepositorioCompra compras = new RepositorioCompra();


            //Popular clientes
            try
            {
                clientes.Add(new Cliente("Victor", "Blumenau", "123", "12312312312", 0));
                clientes.Add(new Cliente("Lucas", "Blumenau", "321", "32132132132", 0));
                clientes.Add(new Cliente("Ranieri", "Blumenau", "213", "21321321321", 0));
            }
            catch (Exception ex)
            {
                if(ex is ArgumentException)
                {
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    Console.WriteLine("Ocorreu um erro");
                    Console.WriteLine(ex);
                }
            }

            //Popular produtos
            try
            {
                produtos.Add(new Produto("Refri", 5.00));
                produtos.Add(new Produto("Pastel", 12.00));
                produtos.Add(new Produto("Fatia de torta", 8.00));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro.");
                throw ex;
            }

            //Popular compras
            try
            {
                var itens = new List<ProdutoVenda>();
                /*var cliente = clientes.Consultar("12312312312");
                itens.Add(new ProdutoVenda(produtos.Consultar(0), 2));
                itens.Add(new ProdutoVenda(produtos.Consultar(1), 1));
                itens.Add(new ProdutoVenda(produtos.Consultar(2), 1));

                compras.Add(new Compra(cliente, new FormaPagamento("Cartão crédito"), itens));
                cliente.AddPontuacao(1);
                clientes.Alterar(cliente);*/

                itens.Clear();
                itens.Add(new ProdutoVenda(produtos.Consultar(0), 2));
                itens.Add(new ProdutoVenda(produtos.Consultar(1), 1));
                itens.Add(new ProdutoVenda(produtos.Consultar(2), 1));

                compras.Add(new Compra(new FormaPagamento("Cartão crédito"), itens));



                compras.Consultar(0).Imprimir();

            }
            catch (Exception)
            {
                Console.WriteLine("deu bosta");
            }
        }
    }
}

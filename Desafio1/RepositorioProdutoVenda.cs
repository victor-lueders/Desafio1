using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class RepositorioProdutoVendaVenda : IRepositorio<ProdutoVenda, int>
    {
        List<ProdutoVenda> produtos = new List<ProdutoVenda>();
        RepositorioProduto repositorioProduto = new RepositorioProduto();
        private string connectionString = """Server=localhost\SQLEXPRESS;Database=mouts;Trusted_Connection=True;Integrated Security=true;""";
        ProdutoVenda produto = null;
        public void Delete(ProdutoVenda entity)
        {
            string deleteQuery = "DELETE FROM produtoVendas WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.Id);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ex.Message);
                }
            }
        }

        public ProdutoVenda Get(int id)
        {

            var selectQuery = "SELECT * FROM produtoVendas WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        produto = new ProdutoVenda
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            CompraId = reader.GetInt32(reader.GetOrdinal("CompraId")),
                            Produto = repositorioProduto.Get(reader.GetInt32(reader.GetOrdinal("ProdutoId"))),
                            Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
                        };
                    }
                }
            }

            return produto;
        }

        public IEnumerable<ProdutoVenda> GetAll()
        {
            produtos.Clear();
            var selectQuery = "SELECT * FROM produtoVendas";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produtos.Add(new ProdutoVenda
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                CompraId = reader.GetInt32(reader.GetOrdinal("CompraId")),
                                Produto = repositorioProduto.Get(reader.GetInt32(reader.GetOrdinal("ProdutoId"))),
                                Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return produtos;
        }

        public ProdutoVenda Save(ProdutoVenda entity)
        {
            /*entity.Id = produtoVendas.Count + 1;
            produtoVendas.Add(entity);*/
            var insertQuery = "INSERT INTO produtoVendas (CompraId, ProdutoId, Quantidade) VALUES (@CompraId, @ProdutoId, @Quantidade); " +
                "SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@CompraId", entity.CompraId);
                cmd.Parameters.AddWithValue("@ProdutoId", entity.ProdutoId);
                cmd.Parameters.AddWithValue("@Quantidade", entity.Quantidade);

                conn.Open();
                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return entity;
        }

        public bool Update(ProdutoVenda entity)
        {
            var updateQuery = "UPDATE produtoVendas SET Quantidade = @Quantidade, ProdutoId = @ProdutoId WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Quantidade", entity.Quantidade);
                cmd.Parameters.AddWithValue("@ProdutoId", entity.ProdutoId);
                cmd.Parameters.AddWithValue("@Id", entity.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }

            return false;
        }

        public IEnumerable<ProdutoVenda> GetByCompra(int compraId)
        {
            produtos.Clear();
            var selectQuery = "SELECT * FROM produtoVendas WHERE CompraId = @CompraId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@CompraId", compraId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produtos.Add(new ProdutoVenda
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                CompraId = reader.GetInt32(reader.GetOrdinal("CompraId")),
                                Produto = repositorioProduto.Get(reader.GetInt32(reader.GetOrdinal("ProdutoId"))),
                                Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return produtos;
        }
    }
}

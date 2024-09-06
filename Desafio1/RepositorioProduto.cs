using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class RepositorioProduto : IRepositorio<Produto, int>
    {
        List<Produto> produtos = new List<Produto>();
        private string connectionString = """Server=localhost\SQLEXPRESS;Database=mouts;Trusted_Connection=True;Integrated Security=true;""";
        Produto produto = null;
        public void Delete(Produto entity)
        {
            string deleteQuery = "DELETE FROM produtos WHERE Id = @Id";
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

        public Produto Get(int id)
        {
            
            var selectQuery = "SELECT * FROM produtos WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        produto = new Produto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                            Valor = reader.GetDouble(reader.GetOrdinal("Valor"))
                        };
                    }
                }
            }

            return produto;
        }

        public IEnumerable<Produto> GetAll()
        {
            produtos.Clear();
            var selectQuery = "SELECT * FROM produtos";

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
                            produtos.Add(new Produto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                Valor = reader.GetDouble(reader.GetOrdinal("Valor"))
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

        public Produto Save(Produto entity)
        {
            /*entity.Id = produtos.Count + 1;
            produtos.Add(entity);*/
            var insertQuery = "INSERT INTO produtos (Descricao, Valor) VALUES (@Descricao, @Valor); " +
                "SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Descricao", entity.Descricao);
                cmd.Parameters.AddWithValue("@Valor", entity.Valor);

                conn.Open();
                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return entity;
        }

        public bool Update(Produto entity)
        {
            var updateQuery = "UPDATE produtos SET Descricao = @Descricao, Valor = @Valor WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Descricao", entity.Descricao);
                cmd.Parameters.AddWithValue("@Valor", entity.Valor);
                cmd.Parameters.AddWithValue("@Id", entity.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }

            return false;
        }
    }
}

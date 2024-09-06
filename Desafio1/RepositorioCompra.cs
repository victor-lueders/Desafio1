using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class RepositorioCompra : IRepositorio<Compra, int>
    {
        RepositorioProduto repositorioProduto = new RepositorioProduto();
        List<Compra> compraList = new List<Compra>();
        Compra compra = null;

        private string connectionString = """Server=localhost\SQLEXPRESS;Database=mouts;Trusted_Connection=True;Integrated Security=true;""";
        public void Delete(Compra entity)
        {
            string deleteQuery = "DELETE FROM compras WHERE Id = @Id";
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

        public Compra Get(int id)
        {
            var selectQuery = "SELECT * FROM compras WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        compra = new Compra
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                            ValorTotal = reader.GetDouble(reader.GetOrdinal("ValorTotal")),
                            Pagamento = reader.GetString(reader.GetOrdinal("Pagamento")),
                            Produtos = repositorioProduto
                        };
                    }
                }
            }

            return compra;
        }

        public IEnumerable<Compra> GetAll()
        {
            compraList.Clear();
            var selectQuery = "SELECT * FROM Compras";

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
                            compraList.Add(new Compra
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                                ValorTotal = reader.GetDouble(reader.GetOrdinal("ValorTotal")),
                                Pagamento = reader.GetString(reader.GetOrdinal("Pagamento")),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return compraList;
        }

        public Compra Save(Compra entity)
        {
            /*entity.Id = CompraList.Count + 1;
            CompraList.Add(entity);*/
            var insertQuery = "INSERT INTO compras (ClienteId, Pagamento, ValorTotal) VALUES (@ClienteId, @Pagamento, @ValorTotal); " +
                "SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@ClienteId", entity.ClienteId);
                cmd.Parameters.AddWithValue("@Pagamento", entity.Pagamento);
                cmd.Parameters.AddWithValue("@ValorTotal", entity.ValorTotal);

                conn.Open();
                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return entity;
        }

        public bool Update(Compra entity)
        {
            var updateQuery = "UPDATE compras SET Pagamento = @Pagamento, ValorTotal = @ValorTotal WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Pagamento", entity.Pagamento);
                cmd.Parameters.AddWithValue("@ValorTotal", entity.ValorTotal);
                cmd.Parameters.AddWithValue("@Id", entity.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }

            return false;
        }
    }
}

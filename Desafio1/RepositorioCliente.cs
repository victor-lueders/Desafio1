using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1
{
    internal class RepositorioCliente : IRepositorio<Cliente, int>
    {
        private List<Cliente> clienteList = new List<Cliente>();
        private string connectionString = """Server=localhost\SQLEXPRESS;Database=mouts;Trusted_Connection=True;Integrated Security=true;""";
        public void Delete(Cliente entity)
        {
            string deleteQuery = "DELETE FROM clientes WHERE Id = @Id";
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

        public Cliente Get(int id)
        {
            Cliente Cliente = null;
            var selectQuery = "SELECT * FROM clientes WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Cliente = new Cliente
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nome = reader.GetString(reader.GetOrdinal("Nome")),
                            cpf = reader.GetString(reader.GetOrdinal("Cpf")),
                            Telefone = reader.GetString(reader.GetOrdinal("Telefone")),
                            Endereco = reader.GetString(reader.GetOrdinal("Endereco")),
                            Pontuacao = reader.GetInt32(reader.GetOrdinal("Pontuacao"))
                        };
                    }
                }
            }

            return Cliente;
        }

        public IEnumerable<Cliente> GetAll()
        {
            clienteList.Clear();
            var selectQuery = "SELECT * FROM clientes";

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
                            clienteList.Add(new Cliente
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                cpf = reader.GetString(reader.GetOrdinal("Cpf")),
                                Telefone = reader.GetString(reader.GetOrdinal("Telefone")),
                                Endereco = reader.GetString(reader.GetOrdinal("Endereco")),
                                Pontuacao = reader.GetInt32(reader.GetOrdinal("Pontuacao"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return clienteList;
        }

        public Cliente Save(Cliente entity)
        {
            /*entity.Id = ClienteList.Count + 1;
            ClienteList.Add(entity);*/
            var insertQuery = "INSERT INTO clientes (Nome, Cpf, Telefone, Endereco, Pontuacao) VALUES (@Nome, @Cpf, @Telefone, @Endereco, @Pontuacao); " +
                "SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                cmd.Parameters.AddWithValue("@Cpf", entity.cpf);
                cmd.Parameters.AddWithValue("@Telefone", entity.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", entity.Endereco);
                cmd.Parameters.AddWithValue("@Pontuacao", entity.Pontuacao);

                conn.Open();
                entity.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return entity;
        }

        public bool Update(Cliente entity)
        {
            var updateQuery = "UPDATE clientes SET Nome = @Nome, Cpf = @cpf, Telefone = @Telefone, Endereco = @Endereco, Pontuacao = @Pontuacao WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                cmd.Parameters.AddWithValue("@Cpf", entity.cpf);
                cmd.Parameters.AddWithValue("@Telefone", entity.Telefone);
                cmd.Parameters.AddWithValue("@Endereco", entity.Endereco);
                cmd.Parameters.AddWithValue("@Pontuacao", entity.Pontuacao);
                cmd.Parameters.AddWithValue("@Id", entity.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }

            return false;
        }
    }
}

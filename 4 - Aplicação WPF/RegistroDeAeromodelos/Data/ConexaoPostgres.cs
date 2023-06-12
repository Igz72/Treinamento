using Npgsql;
using System;
using System.Data.Common;

namespace RegistroDeAeromodelos.Data
{
    public class ConexaoPostgres : IConexao
    {
        private readonly NpgsqlConnection connection;

        private const string user = "postgres";
        private const string password = "1234";
        private const string host = "localhost";
        private const string port = "5432";
        private const string database = "RegistroDeAeromodelosDB";

        public ConexaoPostgres()
        {
            const string connectionString = $"User id={user};" +
                                            $"Password={password};" +
                                            $"Host={host};" +
                                            $"Port={port};" +
                                            $"Database={database}";

            connection = new NpgsqlConnection(connectionString);
        }

        ~ConexaoPostgres()
        {
            connection.Dispose();
        }

        public void ExecutarComando(string sql)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = sql;

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public void LerTabela(string sql, Func<DbDataReader, bool> lambda)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = sql;

                using NpgsqlDataReader dataReader = command.ExecuteReader();

                while (lambda.Invoke(dataReader));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

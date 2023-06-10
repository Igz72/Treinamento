using Npgsql;
using RegistroDeAeromodelos.Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

/*
 * Tabelas criadas no Postgres:
 *
 *  CREATE TABLE fabricantes (
 *      id SERIAL PRIMARY KEY,
 *      nome VARCHAR(32) UNIQUE NOT NULL
 *  );
 *  
 *  CREATE TABLE aeromodelos (
 *      nome VARCHAR(32) NOT NULL,
 *      envergadura NUMERIC NOT NULL CHECK(envergadura > 0),
 *      categoria VARCHAR(32) NOT NULL CHECK(categoria IN ('Outro', 'Sport', 'Escala', 'F3A', 'Acrobacia3D')),
 *      fabricante VARCHAR(32) REFERENCES fabricantes(nome) ON DELETE CASCADE,
 *      PRIMARY KEY(nome, envergadura, fabricante)
 *  );
 */

namespace RegistroDeAeromodelos.Data
{
    public class PostgresRepository : IRepository
    {
        private readonly ObservableCollection<Fabricante> listaDeFabricantes;

        private readonly NpgsqlConnection connection;

        private const string user     = "postgres";
        private const string password = "1234";
        private const string host     = "localhost";
        private const string port     = "5432";
        private const string database = "RegistroDeAeromodelosDB";

        public PostgresRepository()
        {
            listaDeFabricantes = new ObservableCollection<Fabricante>();

            const string connectionString = $"User id={user};" +
                                            $"Password={password};" +
                                            $"Host={host};" +
                                            $"Port={port};" +
                                            $"Database={database}";

            connection = new NpgsqlConnection(connectionString);

            try
            {
                AtualizarListaDeFabricantes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ObservableCollection<Fabricante> ListaDeFabricantes {
            get { return listaDeFabricantes; }
        }

        public void AdicionarFabricante(Fabricante fabricante)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO fabricantes (nome) VALUES('{fabricante.Nome}')";

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

            try
            {
                AtualizarListaDeFabricantes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoverFabricante(Fabricante fabricante)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM fabricantes WHERE nome = '{fabricante.Nome}'";

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

            try
            {
                AtualizarListaDeFabricantes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarFabricante(Fabricante fabricante, Fabricante novoFabricante)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE fabricantes SET nome = '{novoFabricante.Nome}' WHERE nome = '{fabricante.Nome}'";
                
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

            try
            {
                AtualizarListaDeFabricantes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AdicionarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO aeromodelos VALUES(" +
                                      $"'{aeromodelo.Nome}', " +
                                      $"'{aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}', " +
                                      $"'{aeromodelo.Categoria}', " +
                                      $"(SELECT id FROM fabricantes WHERE nome = '{fabricante.Nome}')" +
                                      ")";

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

            try
            {
                AtualizarListaDeFabricantes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoverAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "DELETE FROM aeromodelos " +
                                      $"WHERE nome = '{aeromodelo.Nome}' AND " +
                                      $"envergadura = '{aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}' AND " +
                                      $"fabricante = (SELECT id FROM fabricantes WHERE nome = '{fabricante.Nome}')";

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

            try
            {
                AtualizarListaDeFabricantes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo, Aeromodelo novoAeromodelo)
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE aeromodelos SET " +
                                      $"nome = '{novoAeromodelo.Nome}', " +
                                      $"envergadura = '{novoAeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}', " +
                                      $"categoria = '{novoAeromodelo.Categoria}' " +
                                      $"WHERE " +
                                      $"nome = '{aeromodelo.Nome}' AND " +
                                      $"envergadura = '{aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}' AND " +
                                      $"fabricante = (SELECT id FROM fabricantes WHERE nome = '{fabricante.Nome}')";

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

            try
            {
                AtualizarListaDeFabricantes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void AtualizarListaDeFabricantes()
        {
            listaDeFabricantes.Clear();

            try
            {
                RecuperarFabricantes();
                RecuperarAeromodelos();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RecuperarFabricantes()
        {
            try
            {
                connection.Open();

                using NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM fabricantes";

                using NpgsqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    listaDeFabricantes.Add(new Fabricante(
                        nome: dataReader.GetString(1)
                    ));
                }
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

        private void RecuperarAeromodelos()
        {
            try
            {
                connection.Open();

                foreach (Fabricante fabricante in listaDeFabricantes)
                {
                    using NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT f.id, f.nome, a.nome, a.envergadura, a.categoria, a.fabricante " +
                                          "FROM fabricantes f INNER JOIN aeromodelos a ON f.id = a.fabricante " +
                                          $"WHERE f.nome = '{fabricante.Nome}'";

                    using NpgsqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Enum.TryParse(dataReader.GetString(4), out Aeromodelo.CategoriaDoAeromodelo categoria);

                        fabricante.AdicionarAeromodelo(new Aeromodelo(
                            nome: dataReader.GetString(2),
                            envergadura: dataReader.GetDouble(3),
                            categoria: categoria
                        ));
                    }
                }
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

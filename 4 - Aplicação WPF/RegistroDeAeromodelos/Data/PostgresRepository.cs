using Npgsql;
using RegistroDeAeromodelos.Model;
using System;
using System.Globalization;
using System.Threading.Tasks;

/*
 * Tabelas criadas no Postgres:
 *
 *  CREATE TABLE fabricantes (
 *      nome VARCHAR(32) PRIMARY KEY
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
    public class PostgresRepository : BaseRepository
    {
        private NpgsqlConnection connection;

        public PostgresRepository()
        {
            connection = new NpgsqlConnection(
                connectionString: "Server=localhost;Port=5432;User id=postgres;Password=1234;Database=RegistroDeAeromodelosDB");
            connection.Open();

            _ = RecuperarDados();
        }

        ~PostgresRepository()
        {
            connection.Close();
        }

        public async Task RecuperarDados()
        {
            Task task1 = RecuperarFabricantes();
            await task1;

            Task task2 = RecuperarAeromodelos();
            await task2;
        }

        private async Task RecuperarFabricantes()
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM fabricantes";

            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                ListaDeFabricantes.Add(new Fabricante(
                    nome: reader["nome"] as string
                ));
            }
        }

        private async Task RecuperarAeromodelos()
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            for (int i = 0; i < ListaDeFabricantes.Count; i++)
            {
                cmd.CommandText = $"SELECT fabricantes.nome, aeromodelos.nome as aeromodelo, envergadura, categoria, fabricante " +
                                  $"FROM fabricantes INNER JOIN aeromodelos ON fabricantes.nome=aeromodelos.fabricante " +
                                  $"WHERE fabricantes.nome='{ListaDeFabricantes[i].Nome}'";

                using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Aeromodelo.CategoriaDoAeromodelo categoria;
                    Enum.TryParse(reader["categoria"] as string, out categoria);

                    ListaDeFabricantes[i].AdicionarAeromodelo(new Aeromodelo(
                        nome: reader["aeromodelo"] as string,
                        envergadura: Convert.ToDouble(reader["envergadura"]),
                        categoria: categoria
                    ));
                }
            }

        }

        public override async void AdicionarFabricante(Fabricante fabricante)
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"INSERT INTO fabricantes VALUES('{fabricante.Nome}')";
            await cmd.ExecuteNonQueryAsync();

            ListaDeFabricantes.Add(fabricante);
        }

        public override async void RemoverFabricante(Fabricante fabricante)
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"DELETE FROM fabricantes WHERE nome='{fabricante.Nome}'";
            await cmd.ExecuteNonQueryAsync();

            ListaDeFabricantes.Remove(fabricante);
        }

        public override async void AtualizarFabricante(Fabricante fabricante, Fabricante novoFabricante)
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"UPDATE fabricantes SET nome='{novoFabricante.Nome}' WHERE nome='{fabricante.Nome}'";
            await cmd.ExecuteNonQueryAsync();

            fabricante.AtualizarNome(novoFabricante);
        }

        public override async void AdicionarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo)
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"INSERT INTO aeromodelos (nome, envergadura, categoria, fabricante) " +
                              $"VALUES(" +
                              $"'{aeromodelo.Nome}', " +
                              $"'{aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}', " +
                              $"'{aeromodelo.Categoria}', " +
                              $"'{fabricante.Nome}'" +
                              $")";
            await cmd.ExecuteNonQueryAsync();

            fabricante.AdicionarAeromodelo(aeromodelo);
        }

        public override async void RemoverAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo)
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"DELETE FROM aeromodelos WHERE nome='{aeromodelo.Nome}' AND fabricante='{fabricante.Nome}'";
            System.Diagnostics.Debug.WriteLine(cmd.CommandText);
            await cmd.ExecuteNonQueryAsync();

            fabricante.RemoverAeromodelo(aeromodelo);
        }

        public override async void AtualizarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo, Aeromodelo novoAeromodelo)
        {
            using NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"UPDATE aeromodelos SET " +
                              $"nome='{novoAeromodelo.Nome}', " +
                              $"envergadura='{novoAeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}', " +
                              $"categoria='{novoAeromodelo.Categoria}' " +
                              $"WHERE " +
                              $"nome='{aeromodelo.Nome}' AND " +
                              $"envergadura='{aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}' AND " +
                              $"fabricante='{fabricante.Nome}'";
            await cmd.ExecuteNonQueryAsync();

            aeromodelo.Atualizar(novoAeromodelo);
        }
    }
}

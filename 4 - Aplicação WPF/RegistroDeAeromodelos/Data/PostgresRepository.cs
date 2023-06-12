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
        private readonly IConexao conexao;
        
        private readonly ObservableCollection<Fabricante> listaDeFabricantes;

        public PostgresRepository(IConexao conexao)
        {
            this.conexao = conexao;

            listaDeFabricantes = new ObservableCollection<Fabricante>();

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
                string sql = $"INSERT INTO fabricantes (nome) VALUES('{fabricante.Nome}')";
                conexao.ExecutarComando(sql);
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
                string sql = $"DELETE FROM fabricantes WHERE nome = '{fabricante.Nome}'";
                conexao.ExecutarComando(sql);
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
                string sql = $"UPDATE fabricantes SET nome = '{novoFabricante.Nome}' WHERE nome = '{fabricante.Nome}'";
                conexao.ExecutarComando(sql);
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
                string sql = "INSERT INTO aeromodelos VALUES(" +
                             $"'{aeromodelo.Nome}', " +
                             $"{aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}, " +
                             $"'{aeromodelo.Categoria}', " +
                             $"(SELECT id FROM fabricantes WHERE nome = '{fabricante.Nome}')" +
                             ")";
                conexao.ExecutarComando(sql);
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
                string sql = "DELETE FROM aeromodelos " +
                             $"WHERE nome = '{aeromodelo.Nome}' AND " +
                             $"envergadura = {aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)} AND " +
                             $"fabricante = (SELECT id FROM fabricantes WHERE nome = '{fabricante.Nome}')";
                conexao.ExecutarComando(sql);
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
                string sql = "UPDATE aeromodelos SET " +
                             $"nome = '{novoAeromodelo.Nome}', " +
                             $"envergadura = {novoAeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)}, " +
                             $"categoria = '{novoAeromodelo.Categoria}' " +
                             $"WHERE " +
                             $"nome = '{aeromodelo.Nome}' AND " +
                             $"envergadura = {aeromodelo.Envergadura.ToString(CultureInfo.InvariantCulture)} AND " +
                             $"fabricante = (SELECT id FROM fabricantes WHERE nome = '{fabricante.Nome}')";
                conexao.ExecutarComando(sql);
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
                string sql = "SELECT * FROM fabricantes";

                conexao.LerTabela(sql, (dataReader) =>
                {
                    if (dataReader.Read())
                    {
                        listaDeFabricantes.Add(new Fabricante(
                            nome: dataReader.GetString(1)
                        ));

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RecuperarAeromodelos()
        {
            try
            {
                foreach (Fabricante fabricante in listaDeFabricantes)
                { 
                    string sql = "SELECT f.id, f.nome, a.nome, a.envergadura, a.categoria, a.fabricante " +
                                 "FROM fabricantes f INNER JOIN aeromodelos a ON f.id = a.fabricante " +
                                 $"WHERE f.nome = '{fabricante.Nome}'";

                    conexao.LerTabela(sql, (dataReader) =>
                    {
                        if (dataReader.Read())
                        {
                            Enum.TryParse(dataReader.GetString(4), out Aeromodelo.CategoriaDoAeromodelo categoria);

                            fabricante.AdicionarAeromodelo(new Aeromodelo(
                                nome: dataReader.GetString(2),
                                envergadura: dataReader.GetDouble(3),
                                categoria: categoria
                            ));

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

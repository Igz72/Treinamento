using Autofac.Extras.Moq;
using Moq;
using RegistroDeAeromodelos.Data;
using RegistroDeAeromodelos.Model;
using System.Data.Common;

namespace RegistroDeAeromodelosTests.Data
{
    [TestClass]
    public class DatabaseRepositoryTests
    {
        [TestMethod]
        public void TestarConstrutor()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();

            string sql = "SELECT * FROM fabricantes";
            mock.Mock<IConexao>().Setup(conexao => conexao.LerTabela(sql, It.IsAny<Func<DbDataReader, bool>>()));

            // Act
            DatabaseRepository repository = mock.Create<DatabaseRepository>();

            // Assert
            mock.Mock<IConexao>().Verify(x => x.LerTabela(sql, It.IsAny<Func<DbDataReader, bool>>()), Times.Exactly(1));
        }

        [TestMethod]
        public void TestarAdicionarFabricante()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();
            
            string sql = "INSERT INTO fabricantes (nome) VALUES('Nome')";
            mock.Mock<IConexao>().Setup(conexao => conexao.ExecutarComando(sql));

            DatabaseRepository repository = mock.Create<DatabaseRepository>();

            Fabricante fabricante = new Fabricante("Nome");

            // Act
            repository.AdicionarFabricante(fabricante);

            // Assert
            mock.Mock<IConexao>().Verify(x => x.ExecutarComando(sql), Times.Exactly(1));
        }

        [TestMethod]
        public void TestarRemoverFabricante()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();

            string sql = "DELETE FROM fabricantes WHERE nome = 'Nome'";
            mock.Mock<IConexao>().Setup(conexao => conexao.ExecutarComando(sql));

            DatabaseRepository repository = mock.Create<DatabaseRepository>();

            Fabricante fabricante = new Fabricante("Nome");

            // Act
            repository.RemoverFabricante(fabricante);

            // Assert
            mock.Mock<IConexao>().Verify(x => x.ExecutarComando(sql), Times.Exactly(1));
        }

        [TestMethod]
        public void TestarAtualizarFabricante()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();

            string sql = "UPDATE fabricantes SET nome = 'novoNome' WHERE nome = 'Nome'";
            mock.Mock<IConexao>().Setup(conexao => conexao.ExecutarComando(sql));

            DatabaseRepository repository = mock.Create<DatabaseRepository>();

            Fabricante fabricante = new Fabricante("Nome");
            Fabricante novoFabricante = new Fabricante("novoNome");

            // Act
            repository.AtualizarFabricante(fabricante, novoFabricante);

            // Assert
            mock.Mock<IConexao>().Verify(x => x.ExecutarComando(sql), Times.Exactly(1));
        }

        [TestMethod]
        public void TestarAdicionarAeromodelo()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();

            string sql = "INSERT INTO aeromodelos VALUES(" +
                         "'Nome do aeromodelo', " +
                         "123456.789, " +
                         "'Sport', " +
                         "(SELECT id FROM fabricantes WHERE nome = 'Nome do fabricante')" +
                         ")";
            mock.Mock<IConexao>().Setup(conexao => conexao.ExecutarComando(sql));

            Fabricante fabricante = new Fabricante("Nome do fabricante");
            Aeromodelo aeromodelo = new Aeromodelo("Nome do aeromodelo", 123456.789, Aeromodelo.CategoriaDoAeromodelo.Sport);

            DatabaseRepository repository = mock.Create<DatabaseRepository>();

            // Act
            repository.AdicionarAeromodelo(fabricante, aeromodelo);

            // Assert
            mock.Mock<IConexao>().Verify(x => x.ExecutarComando(sql), Times.Exactly(1));
        }

        [TestMethod]
        public void TestarRemoverAeromodelo()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();

            string sql = "DELETE FROM aeromodelos " +
                         "WHERE nome = 'Nome do aeromodelo' AND " +
                         "envergadura = 123456.789 AND " +
                         "fabricante = (SELECT id FROM fabricantes WHERE nome = 'Nome do fabricante')";
            mock.Mock<IConexao>().Setup(conexao => conexao.ExecutarComando(sql));

            Fabricante fabricante = new Fabricante("Nome do fabricante");
            Aeromodelo aeromodelo = new Aeromodelo("Nome do aeromodelo", 123456.789, Aeromodelo.CategoriaDoAeromodelo.Sport);

            DatabaseRepository repository = mock.Create<DatabaseRepository>();

            // Act
            repository.RemoverAeromodelo(fabricante, aeromodelo);

            // Assert
            mock.Mock<IConexao>().Verify(x => x.ExecutarComando(sql), Times.Exactly(1));
        }

        [TestMethod]
        public void TestarAtualizarAeromodelo()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();

            string sql = "UPDATE aeromodelos SET " +
                         "nome = 'Novo nome do aeromodelo', " +
                         "envergadura = 987.654321, " +
                         "categoria = 'Sport' " +
                         "WHERE " +
                         "nome = 'Nome do aeromodelo' AND " +
                         "envergadura = 123456.789 AND " +
                         "fabricante = (SELECT id FROM fabricantes WHERE nome = 'Nome do fabricante')";
            mock.Mock<IConexao>().Setup(conexao => conexao.ExecutarComando(sql));

            Fabricante fabricante = new Fabricante("Nome do fabricante");
            Aeromodelo aeromodelo = new Aeromodelo("Nome do aeromodelo", 123456.789, Aeromodelo.CategoriaDoAeromodelo.Outro);
            Aeromodelo novoAeromodelo = new Aeromodelo("Novo nome do aeromodelo", 987.654321, Aeromodelo.CategoriaDoAeromodelo.Sport);

            DatabaseRepository repository = mock.Create<DatabaseRepository>();

            // Act
            repository.AtualizarAeromodelo(fabricante, aeromodelo, novoAeromodelo);

            // Assert
            mock.Mock<IConexao>().Verify(x => x.ExecutarComando(sql), Times.Exactly(1));
        }
    }
}

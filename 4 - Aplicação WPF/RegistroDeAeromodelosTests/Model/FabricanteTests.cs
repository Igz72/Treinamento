using RegistroDeAeromodelos.Model;

namespace RegistroDeAeromodelosTests.Model
{
    [TestClass]
    public class FabricanteTests
    {
        [TestMethod]
        public void TestarConstrutorVazio()
        {
            // Arrange
            const string nomeEsperado = "";
            const int tamanhoDaListaEsperado = 0;

            // Act
            Fabricante fabricante = new Fabricante();

            // Assert
            string nomeRecebido = fabricante.Nome;
            int tamanhoDaListaRecebido = fabricante.ListaDeAeromodelos.Count;

            Assert.AreEqual(nomeEsperado, nomeRecebido);
            Assert.AreEqual(tamanhoDaListaEsperado, tamanhoDaListaRecebido);
        }

        [TestMethod]
        public void TestarConstrutorComNome()
        {
            // Arrange
            const string nomeAtribuido = "Nome";

            const string nomeEsperado = nomeAtribuido;
            const int tamanhoDaListaEsperado = 0;

            // Act
            Fabricante fabricante = new Fabricante(nomeAtribuido);

            // Assert
            string nomeRecebido = fabricante.Nome;
            int tamanhoDaListaRecebido = fabricante.ListaDeAeromodelos.Count;

            Assert.AreEqual(nomeEsperado, nomeRecebido);
            Assert.AreEqual(tamanhoDaListaEsperado, tamanhoDaListaRecebido);
        }

        [TestMethod]
        public void TestarConstrutorComNomeNull()
        {
            // Arrange
            const string? nomeAtribuido = null;

            const string nomeEsperado = "";
            const int tamanhoDaListaEsperado = 0;

            // Act
            Fabricante fabricante = new Fabricante(nomeAtribuido);

            // Assert
            string nomeRecebido = fabricante.Nome;
            int tamanhoDaListaRecebido = fabricante.ListaDeAeromodelos.Count;

            Assert.AreEqual(nomeEsperado, nomeRecebido);
            Assert.AreEqual(tamanhoDaListaEsperado, tamanhoDaListaRecebido);
        }

        [TestMethod]
        public void TestarSetNomeNull()
        {
            // Arrange
            const string? nomeAtribuido = null;
            const string nomeEsperado = "";

            Fabricante fabricante = new Fabricante();

            // Act
            fabricante.Nome = nomeAtribuido;

            // Assert
            string? nomeRecebido = fabricante.Nome;

            Assert.AreEqual(nomeEsperado, nomeRecebido);
        }

        [TestMethod]
        public void TestarShallowCopy()
        {
            // Arrange
            Fabricante fabricante = new Fabricante();

            // Act
            Fabricante novoFabricante = fabricante.ShallowCopy();

            fabricante.Nome = "Nome";

            // Assert
            Assert.AreNotEqual(fabricante.Nome, novoFabricante.Nome);
            Assert.AreEqual(fabricante.ListaDeAeromodelos, novoFabricante.ListaDeAeromodelos);
        }

        [TestMethod]
        public void TestarAtualizarNome()
        {
            // Arrange
            Fabricante fabricante = new Fabricante();
            Fabricante novoFabricante = new Fabricante("Nome");

            // Act
            fabricante.AtualizarNome(novoFabricante);

            // Assert
            Assert.AreEqual(fabricante.Nome, novoFabricante.Nome);
            Assert.AreNotEqual(fabricante.ListaDeAeromodelos, novoFabricante.ListaDeAeromodelos);
        }

        [TestMethod]
        public void TestarAdicionarAeromodelo()
        {
            // Arrange
            Fabricante fabricante = new Fabricante();
            Aeromodelo aeromodelo = new Aeromodelo();

            const int tamanhoDaListaEsperado = 1;

            // Act
            fabricante.AdicionarAeromodelo(aeromodelo);

            // Assert
            Assert.AreEqual(fabricante.ListaDeAeromodelos.Count, tamanhoDaListaEsperado);
            Assert.AreEqual(fabricante.ListaDeAeromodelos[0], aeromodelo);
        }

        [TestMethod]
        public void TestarRemoverAeromodelo()
        {
            // Arrange
            Fabricante fabricante = new Fabricante();
            Aeromodelo aeromodelo = new Aeromodelo();
            fabricante.AdicionarAeromodelo(aeromodelo);

            const int tamanhoDaListaEsperado = 0;

            // Act
            fabricante.RemoverAeromodelo(aeromodelo);

            // Assert
            Assert.AreEqual(fabricante.ListaDeAeromodelos.Count, tamanhoDaListaEsperado);
        }
    }
}

using RegistroDeAeromodelos.Model;

namespace RegistroDeAeromodelosTests.Model
{
    [TestClass]
    public class AeromodeloTests
    {
        [TestMethod]
        public void TestarConstrutorVazio()
        {
            // Arrange
            const string nomeEsperado = "";
            const double envergaduraEsperada = 0;
            const Aeromodelo.CategoriaDoAeromodelo categoriaEsperada = Aeromodelo.CategoriaDoAeromodelo.Outro;

            // Act
            Aeromodelo aeromodelo = new Aeromodelo();

            // Assert
            string nomeRecebido = aeromodelo.Nome;
            double envergaduraRecebida = aeromodelo.Envergadura;
            Aeromodelo.CategoriaDoAeromodelo categoriaRecebido = aeromodelo.Categoria;

            Assert.AreEqual(nomeEsperado, nomeRecebido);
            Assert.AreEqual(envergaduraEsperada, envergaduraRecebida);
            Assert.AreEqual(categoriaEsperada, categoriaRecebido);
        }

        [TestMethod]
        public void TestarConstrutorComNomeEnvergaduraCategoria()
        {
            // Arrange
            const string nomeAtribuido = "Nome";
            const double envergaduraAtribuida = 123456.7890;
            const Aeromodelo.CategoriaDoAeromodelo categoriaAtribuida = Aeromodelo.CategoriaDoAeromodelo.Sport;

            // Act
            Aeromodelo aeromodelo = new Aeromodelo(nomeAtribuido, envergaduraAtribuida, categoriaAtribuida);

            // Assert
            string nomeRecebido = aeromodelo.Nome;
            double envergaduraRecebida = aeromodelo.Envergadura;
            Aeromodelo.CategoriaDoAeromodelo categoriaRecebido = aeromodelo.Categoria;

            Assert.AreEqual(nomeAtribuido, nomeRecebido);
            Assert.AreEqual(envergaduraAtribuida, envergaduraRecebida);
            Assert.AreEqual(categoriaAtribuida, categoriaRecebido);
        }

        [TestMethod]
        public void TestarConstrutorComNomeNullEnvergaduraCategoria()
        {
            // Arrange
            const string? nomeAtribuido = null;
            const double envergaduraAtribuida = 123456.7890;
            const Aeromodelo.CategoriaDoAeromodelo categoriaAtribuida = Aeromodelo.CategoriaDoAeromodelo.Sport;

            const string nomeEsperado = "";
            const double envergaduraEsperada = envergaduraAtribuida;
            const Aeromodelo.CategoriaDoAeromodelo categoriaEsperada = categoriaAtribuida;

            // Act
            Aeromodelo aeromodelo = new Aeromodelo(nomeAtribuido, envergaduraAtribuida, categoriaAtribuida);

            // Assert
            string nomeRecebido = aeromodelo.Nome;
            double envergaduraRecebida = aeromodelo.Envergadura;
            Aeromodelo.CategoriaDoAeromodelo categoriaRecebido = aeromodelo.Categoria;

            Assert.AreEqual(nomeEsperado, nomeRecebido);
            Assert.AreEqual(envergaduraEsperada, envergaduraRecebida);
            Assert.AreEqual(categoriaEsperada, categoriaRecebido);
        }

        [TestMethod]
        public void TestarSetNomeNull()
        {
            // Arrange
            const string? nomeAtribuido = null;
            const string nomeEsperado = "";

            Aeromodelo aeromodelo = new Aeromodelo();

            // Act
            aeromodelo.Nome = nomeAtribuido;

            // Assert
            string? nomeRecebido = aeromodelo.Nome;

            Assert.AreEqual(nomeEsperado, nomeRecebido);
        }

        [TestMethod]
        public void TestarShallowCopy()
        {
            // Arrange
            Aeromodelo aeromodelo = new Aeromodelo();

            // Act
            Aeromodelo novoAeromodelo = aeromodelo.ShallowCopy();

            aeromodelo.Nome = "Nome";
            aeromodelo.Envergadura = 123456.789;
            aeromodelo.Categoria = Aeromodelo.CategoriaDoAeromodelo.Sport;

            // Assert
            Assert.AreNotEqual(aeromodelo.Nome, novoAeromodelo.Nome);
            Assert.AreNotEqual(aeromodelo.Envergadura, novoAeromodelo.Envergadura);
            Assert.AreNotEqual(aeromodelo.Categoria, novoAeromodelo.Categoria);
        }

        [TestMethod]
        public void TestarAtualizar()
        {
            // Arrange
            Aeromodelo aeromodelo = new Aeromodelo();
            Aeromodelo novoAeromodelo = new Aeromodelo("nome", 123456.789, Aeromodelo.CategoriaDoAeromodelo.Sport);

            // Act
            aeromodelo.Atualizar(novoAeromodelo);

            // Assert
            Assert.AreEqual(aeromodelo.Nome, novoAeromodelo.Nome);
            Assert.AreEqual(aeromodelo.Envergadura, novoAeromodelo.Envergadura);
            Assert.AreEqual(aeromodelo.Categoria, novoAeromodelo.Categoria);
        }
    }
}

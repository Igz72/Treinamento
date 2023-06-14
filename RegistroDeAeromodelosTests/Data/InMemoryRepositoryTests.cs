using RegistroDeAeromodelos.Data;
using RegistroDeAeromodelos.Model;

namespace RegistroDeAeromodelosTests.Data
{
    [TestClass]
    public class InMemoryRepositoryTests
    {
        [TestMethod]
        public void TestarAdicionarFabricante()
        {
            // Arrange
            InMemoryRepository repository = new InMemoryRepository();
            Fabricante fabricante = new Fabricante();

            // Act
            repository.AdicionarFabricante(fabricante);

            // Assert
            Assert.IsTrue(repository.ListaDeFabricantes.Contains(fabricante));
        }

        [TestMethod]
        public void TestarRemoverFabricante()
        {
            // Arrange
            InMemoryRepository repository = new InMemoryRepository();
            Fabricante fabricante = new Fabricante();
            repository.AdicionarFabricante(fabricante);

            // Act
            repository.RemoverFabricante(fabricante);

            // Assert
            Assert.IsFalse(repository.ListaDeFabricantes.Contains(fabricante));
        }

        [TestMethod]
        public void TestarAtualizarFabricante()
        {
            // Arrange
            const string nomeAtribuido = "Nome";

            InMemoryRepository repository = new InMemoryRepository();
            Fabricante fabricante = new Fabricante();
            Fabricante novoFabricante = new Fabricante(nomeAtribuido);
            repository.AdicionarFabricante(fabricante);

            // Act
            repository.AtualizarFabricante(fabricante, novoFabricante);

            // Assert
            int indiceDoFabricante = repository.ListaDeFabricantes.IndexOf(fabricante);
            Fabricante fabricanteRecebido = repository.ListaDeFabricantes[indiceDoFabricante];
            string? nomeRecebido = fabricanteRecebido.Nome;

            Assert.AreEqual(nomeAtribuido, nomeRecebido);
        }

        [TestMethod]
        public void TestarAdicionarAeromodelo()
        {
            // Arrange
            InMemoryRepository repository = new InMemoryRepository();
            Fabricante fabricante = new Fabricante();
            Aeromodelo aeromodelo = new Aeromodelo();
            repository.AdicionarFabricante(fabricante);

            // Act
            repository.AdicionarAeromodelo(fabricante, aeromodelo);

            // Assert
            int indiceDoFabricante = repository.ListaDeFabricantes.IndexOf(fabricante);
            Fabricante fabricanteRecebido = repository.ListaDeFabricantes[indiceDoFabricante];

            Assert.IsTrue(fabricanteRecebido.ListaDeAeromodelos.Contains(aeromodelo));
        }

        [TestMethod]
        public void TestarRemoverAeromodelo()
        {
            // Arrange
            InMemoryRepository repository = new InMemoryRepository();
            Fabricante fabricante = new Fabricante();
            Aeromodelo aeromodelo = new Aeromodelo();
            repository.AdicionarFabricante(fabricante);
            repository.AdicionarAeromodelo(fabricante, aeromodelo);

            // Act
            repository.RemoverAeromodelo(fabricante, aeromodelo);

            // Assert
            int indiceDoFabricante = repository.ListaDeFabricantes.IndexOf(fabricante);
            Fabricante fabricanteRecebido = repository.ListaDeFabricantes[indiceDoFabricante];

            Assert.IsFalse(fabricanteRecebido.ListaDeAeromodelos.Contains(aeromodelo));
        }

        [TestMethod]
        public void TestarAtualizarAeromodelo()
        {
            // Arrange
            InMemoryRepository repository = new InMemoryRepository();
            Fabricante fabricante = new Fabricante();
            Aeromodelo aeromodelo = new Aeromodelo();
            Aeromodelo novoAeromodelo = new Aeromodelo("nome", 123456.789, Aeromodelo.CategoriaDoAeromodelo.Sport);
            repository.AdicionarFabricante(fabricante);
            repository.AdicionarAeromodelo(fabricante, aeromodelo);

            // Act
            repository.AtualizarAeromodelo(fabricante, aeromodelo, novoAeromodelo);

            // Assert
            int indiceDoFabricante = repository.ListaDeFabricantes.IndexOf(fabricante);
            Fabricante fabricanteRecebido = repository.ListaDeFabricantes[indiceDoFabricante];
            int indiceDoAeromodelo = fabricanteRecebido.ListaDeAeromodelos.IndexOf(aeromodelo);
            Aeromodelo aeromodeloRecebido = fabricanteRecebido.ListaDeAeromodelos[indiceDoAeromodelo];

            Assert.AreEqual(aeromodeloRecebido.Nome, novoAeromodelo.Nome);
            Assert.AreEqual(aeromodeloRecebido.Envergadura, novoAeromodelo.Envergadura);
            Assert.AreEqual(aeromodeloRecebido.Categoria, novoAeromodelo.Categoria);
        }
    }
}

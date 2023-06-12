using Autofac.Extras.Moq;
using Moq;
using RegistroDeAeromodelos.Data;
using RegistroDeAeromodelos.Model;
using System.Data;

namespace RegistroDeAeromodelosTests.Data
{
    [TestClass]
    public class PostgresRepositoryTests
    {
        [TestMethod]
        public void TestarAdicionarFabricante()
        {
            // Arrange
            using AutoMock mock = AutoMock.GetLoose();

            mock.Mock<IDbConnection>()
                .Setup(x => x.Open());

            mock.Mock<IDbCommand>()
                .Setup(x => x.ExecuteNonQuery());

            mock.Mock<IDbCommand>()
                .SetupSequence(x => x.ExecuteReader().Read())
                .Returns(true)
                .Returns(false);

            mock.Mock<IDbCommand>()
                .Setup(x => x.ExecuteReader().GetString(1))
                .Returns("Nome");

            PostgresRepository postgreRepository = mock.Create<PostgresRepository>();
            Fabricante fabricante = new Fabricante();

            // Act
            postgreRepository.AdicionarFabricante(fabricante);

            // Assert
            mock.Mock<IDbCommand>().Verify(x => x.ExecuteNonQuery(), Times.Exactly(1));
            //Assert.AreEqual(postgreRepository.ListaDeFabricantes.Count, 1);
        }
    }
}

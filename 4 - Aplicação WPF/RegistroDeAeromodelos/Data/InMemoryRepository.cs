using RegistroDeAeromodelos.Model;

namespace RegistroDeAeromodelos.Data
{
    public class InMemoryRepository : BaseRepository
    {
        public InMemoryRepository()
        {
            InicializarDados();
        }

        public override void AdicionarFabricante(Fabricante fabricante)
        {
            ListaDeFabricantes.Add(fabricante);
        }

        public override void RemoverFabricante(Fabricante fabricante)
        {
            ListaDeFabricantes.Remove(fabricante);
        }

        public override void AtualizarFabricante(Fabricante fabricante, Fabricante novoFabricante)
        {
            fabricante.AtualizarNome(novoFabricante);
        }

        public override void AdicionarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo)
        {
            fabricante.AdicionarAeromodelo(aeromodelo);
        }

        public override void RemoverAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo)
        {
            fabricante.RemoverAeromodelo(aeromodelo);
        }

        public override void AtualizarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo, Aeromodelo novoAeromodelo)
        {
            aeromodelo.Atualizar(novoAeromodelo);
        }

        private void InicializarDados()
        {
            Fabricante hobbyking = new Fabricante("HobbyKing");
            hobbyking.AdicionarAeromodelo(new Aeromodelo("Sukoi", 2, Aeromodelo.CategoriaDoAeromodelo.Escala));
            ListaDeFabricantes.Add(hobbyking);

            Fabricante pilotrc = new Fabricante("Pilot-RC");
            pilotrc.AdicionarAeromodelo(new Aeromodelo("Slick", 1.52, Aeromodelo.CategoriaDoAeromodelo.Acrobacia3D));
            pilotrc.AdicionarAeromodelo(new Aeromodelo("Laser", 1.50, Aeromodelo.CategoriaDoAeromodelo.Acrobacia3D));
            pilotrc.AdicionarAeromodelo(new Aeromodelo("Extra NG", 2.92, Aeromodelo.CategoriaDoAeromodelo.Acrobacia3D));
            pilotrc.AdicionarAeromodelo(new Aeromodelo("Extra NG", 1.52, Aeromodelo.CategoriaDoAeromodelo.Acrobacia3D));
            pilotrc.AdicionarAeromodelo(new Aeromodelo("Slick", 2.26, Aeromodelo.CategoriaDoAeromodelo.Acrobacia3D));
            pilotrc.AdicionarAeromodelo(new Aeromodelo("P47D 1/5", 2.41, Aeromodelo.CategoriaDoAeromodelo.Escala));
            ListaDeFabricantes.Add(pilotrc);

            Fabricante krillaircraft = new Fabricante("Krill Aircraft");
            krillaircraft.AdicionarAeromodelo(new Aeromodelo("Lazer Z2300", 2.662, Aeromodelo.CategoriaDoAeromodelo.Acrobacia3D));
            krillaircraft.AdicionarAeromodelo(new Aeromodelo("Yak 55M", 2.600, Aeromodelo.CategoriaDoAeromodelo.Escala));
            ListaDeFabricantes.Add(krillaircraft);

            Fabricante towerhobbies = new Fabricante("Tower Hobbies");
            towerhobbies.AdicionarAeromodelo(new Aeromodelo("Ultar Stick", 2.05, Aeromodelo.CategoriaDoAeromodelo.Sport));
            towerhobbies.AdicionarAeromodelo(new Aeromodelo("Commander", 1.4, Aeromodelo.CategoriaDoAeromodelo.Sport));
            ListaDeFabricantes.Add(towerhobbies);

            Fabricante ckaero = new Fabricante("CKAero");
            ckaero.AdicionarAeromodelo(new Aeromodelo("Alchemy Pro", 2, Aeromodelo.CategoriaDoAeromodelo.F3A));
            ckaero.AdicionarAeromodelo(new Aeromodelo("Balance Bipe", 2, Aeromodelo.CategoriaDoAeromodelo.F3A));
            ListaDeFabricantes.Add(ckaero);
        }
    }
}

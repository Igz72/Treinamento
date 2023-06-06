using RegistroDeAeromodelos.Model;
using System.Collections.ObjectModel;

namespace RegistroDeAeromodelos.Data
{
    public abstract class BaseRepository
    {
        public ObservableCollection<Fabricante> ListaDeFabricantes { get; private set; }

        public BaseRepository()
        {
            ListaDeFabricantes = new ObservableCollection<Fabricante>();
        }

        public abstract void AdicionarFabricante(Fabricante fabricante);
        public abstract void RemoverFabricante(Fabricante fabricante);
        public abstract void AtualizarFabricante(Fabricante fabricante, Fabricante novoFabricante);
        public abstract void AdicionarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo);
        public abstract void RemoverAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo);
        public abstract void AtualizarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo, Aeromodelo novoAeromodelo);
    }
}

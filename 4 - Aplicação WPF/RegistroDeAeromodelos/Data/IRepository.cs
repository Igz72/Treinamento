using RegistroDeAeromodelos.Model;
using System.Collections.ObjectModel;

namespace RegistroDeAeromodelos.Data
{
    public interface IRepository
    {
        ObservableCollection<Fabricante> ListaDeFabricantes { get; }

        void AdicionarFabricante(Fabricante fabricante);
        void RemoverFabricante(Fabricante fabricante);
        void AtualizarFabricante(Fabricante fabricante, Fabricante novoFabricante);
        void AdicionarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo);
        void RemoverAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo);
        void AtualizarAeromodelo(Fabricante fabricante, Aeromodelo aeromodelo, Aeromodelo novoAeromodelo);
    }
}

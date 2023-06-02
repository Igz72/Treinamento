using RegistroDeAeromodelos.Model;
using RegistroDeAeromodelos.View;
using RegistroDeAeromodelos.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RegistroDeAeromodelos.Windows.MainWindow
{
    public class MainWindowVM
    {
        public ObservableCollection<Fabricante> ListaDeFabricantes { get; private set; }

        public Fabricante? FabricanteSelecionado { get; set; }

        public Aeromodelo? AeromodeloSelecionado { get; set; }

        public ICommand AdicionarFabricante { get; private set; }
        public ICommand RemoverFabricante { get; private set; }
        public ICommand AtualizarFabricante { get; private set; }
        public ICommand AdicionarAeromodelo { get; private set; }
        public ICommand RemoverAeromodelo { get; private set; }
        public ICommand AtualizarAeromodelo { get; private set; }

        public MainWindowVM()
        {
            ListaDeFabricantes = new ObservableCollection<Fabricante>();
            InicializarComandos();
            InicializarDados();
        }

        private void InicializarComandos()
        {
            AdicionarFabricante = new RelayCommand((object _) =>
            {
                Fabricante novoFabricante = new Fabricante();

                EditarFabricanteWindow tela = new EditarFabricanteWindow
                {
                    DataContext = novoFabricante
                };

                if (tela.ShowDialog().Equals(true))
                {
                    ListaDeFabricantes.Add(novoFabricante);
                }
            });

            RemoverFabricante = new RelayCommand((object _) =>
            {
                ListaDeFabricantes.Remove(FabricanteSelecionado!);
            }, (object _) => (FabricanteSelecionado != null));

            AtualizarFabricante = new RelayCommand((object _) =>
            {
                Fabricante fabricanteTemporario = FabricanteSelecionado!.ShallowCopy();

                EditarFabricanteWindow tela = new EditarFabricanteWindow
                {
                    DataContext = fabricanteTemporario
                };

                if (tela.ShowDialog().Equals(true))
                { 
                    FabricanteSelecionado.AtualizarNome(fabricanteTemporario);
                }
            },(object _) => FabricanteSelecionado != null);

            AdicionarAeromodelo = new RelayCommand((object _) =>
            {
                Aeromodelo novoAeromodelo = new Aeromodelo();

                EditarAeromodeloWindow tela = new EditarAeromodeloWindow
                {
                    DataContext = novoAeromodelo
                };

                if (tela.ShowDialog().Equals(true))
                {
                    FabricanteSelecionado!.AdicionarAeromodelo(novoAeromodelo);
                }
            }, (object _) => FabricanteSelecionado != null);

            RemoverAeromodelo = new RelayCommand((object _) =>
            {
                FabricanteSelecionado!.RemoverAeromodelo(AeromodeloSelecionado!);
            },
            (object _) => FabricanteSelecionado != null && AeromodeloSelecionado != null);

            AtualizarAeromodelo = new RelayCommand((object _) =>
            {
                if (AeromodeloSelecionado != null)
                {
                    Aeromodelo aeromodeloTemporario = AeromodeloSelecionado.ShallowCopy();

                    EditarAeromodeloWindow tela = new EditarAeromodeloWindow
                    {
                        DataContext = aeromodeloTemporario
                    };

                    if (tela.ShowDialog().Equals(true))
                    {
                        AeromodeloSelecionado.Atualizar(aeromodeloTemporario);
                    }
                }
            }, (object _) => AeromodeloSelecionado != null);
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

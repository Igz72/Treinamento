using RegistroDeAeromodelos.Data;
using RegistroDeAeromodelos.Model;
using RegistroDeAeromodelos.View;
using RegistroDeAeromodelos.ViewModel;
using System.Windows.Input;

namespace RegistroDeAeromodelos.Windows.MainWindow
{
    public class MainWindowVM
    {
        public IRepository Repository { get; private set; }

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
            Repository = new PostgresRepository();

            InicializarComandos();
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
                    Repository.AdicionarFabricante(novoFabricante);
                }
            });

            RemoverFabricante = new RelayCommand((object _) =>
            {
                Repository.RemoverFabricante(FabricanteSelecionado!);
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
                    Repository.AtualizarFabricante(FabricanteSelecionado, fabricanteTemporario);
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
                    Repository.AdicionarAeromodelo(FabricanteSelecionado!, novoAeromodelo);
                }
            }, (object _) => FabricanteSelecionado != null);

            RemoverAeromodelo = new RelayCommand((object _) =>
            {
                Repository.RemoverAeromodelo(FabricanteSelecionado!, AeromodeloSelecionado!);
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
                        Repository.AtualizarAeromodelo(FabricanteSelecionado!, AeromodeloSelecionado, aeromodeloTemporario);
                    }
                }
            }, (object _) => AeromodeloSelecionado != null);
        }
    }
}

using System.ComponentModel;

namespace RegistroDeAeromodelos.Model
{
    public class Aeromodelo : INotifyPropertyChanged
    {
        private string nome;
        private double envergadura;
        private CategoriaDoAeromodelo categoria;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Aeromodelo() 
        {
            this.nome = "";
            this.envergadura = 0;
            this.categoria = CategoriaDoAeromodelo.Outro;
        }
        public Aeromodelo(string nome, double envergadura, CategoriaDoAeromodelo categoria) 
        {
            this.nome = nome;
            this.envergadura = envergadura;
            this.categoria = categoria;
        }

        public Aeromodelo ShallowCopy()
        {
            return (Aeromodelo)this.MemberwiseClone();
        }

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; Notificar(nameof(Nome)); }
        }

        public double Envergadura
        {
            get { return this.envergadura; }
            set { this.envergadura = value; Notificar(nameof(Envergadura)); }
        }

        public CategoriaDoAeromodelo Categoria
        {
            get { return this.categoria; }
            set { this.categoria = value; Notificar(nameof(Categoria)); }
        }

        public void Atualizar(Aeromodelo aeromodelo)
        {
            this.Nome = aeromodelo.Nome;
            this.Envergadura = aeromodelo.Envergadura;
            this.Categoria = aeromodelo.Categoria;
        }

        private void Notificar(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public enum CategoriaDoAeromodelo
        {
            Outro = 0,
            Sport = 1,
            Escala = 2,
            F3A = 3,
            Acrobacia3D = 4
        }
    }
}

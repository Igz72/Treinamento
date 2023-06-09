﻿using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RegistroDeAeromodelos.Model
{
    public class Fabricante : INotifyPropertyChanged
    {
        private string nome;

        public ObservableCollection<Aeromodelo> ListaDeAeromodelos { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Fabricante(string? nome = "") 
        {
            nome ??= "";

            this.nome = nome;
            this.ListaDeAeromodelos = new ObservableCollection<Aeromodelo>();
        }

        public string? Nome
        {
            get { return this.nome; }
            set
            {
                value ??= "";
                this.nome = value;
                Notificar(nameof(Nome));
            }
        }

        public Fabricante ShallowCopy()
        {
            return (Fabricante)this.MemberwiseClone();
        }

        public void AtualizarNome(Fabricante fabricante)
        {
            this.Nome = fabricante.Nome;
        }

        public void AdicionarAeromodelo(Aeromodelo aeromodelo)
        {
            this.ListaDeAeromodelos.Add(aeromodelo);
        }

        public void RemoverAeromodelo(Aeromodelo aeromodelo)
        {
            this.ListaDeAeromodelos.Remove(aeromodelo);
        }

        private void Notificar(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

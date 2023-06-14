using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RegistroDeAeromodelos.View
{
    /// <summary>
    /// Lógica interna para EditarAeromodeloWindow.xaml
    /// </summary>
    public partial class EditarAeromodeloWindow : Window
    {
        public EditarAeromodeloWindow()
        {
            InitializeComponent();
            
        }

        public void BotaoSalvar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}

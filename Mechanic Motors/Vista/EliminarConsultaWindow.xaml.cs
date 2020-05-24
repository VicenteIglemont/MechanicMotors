using Mechanic_Motors.Modelo;
using Mechanic_Motors.ServiciosBD;
using Mechanic_Motors.VistaModelo;
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

namespace Mechanic_Motors.Vista
{
    /// <summary>
    /// Lógica de interacción para EliminarConsultaWindow.xaml
    /// </summary>
    public partial class EliminarConsultaWindow : Window
    {

        Consulta consultaEliminada;

        public EliminarConsultaWindow(Consulta consultaEliminada)
        {
            this.consultaEliminada = consultaEliminada;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();
        }

        // Cancelacion de eliminacion de consulta
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Confirmacion para eliminacion de consulta
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (BDServicios.DeleteConsulta(consultaEliminada) == 1)
            {
                System.Windows.MessageBox.Show("Consulta eliminada con éxito", "Eliminar Consulta", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("No se ha podido eliminar la consulta... Compruebe su conexión a internet", "Eliminar Consulta", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

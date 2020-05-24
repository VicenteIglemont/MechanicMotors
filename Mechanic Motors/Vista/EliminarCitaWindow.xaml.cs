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
    /// Lógica de interacción para EliminarCitaWindow.xaml
    /// </summary>
    public partial class EliminarCitaWindow : Window
    {

        Cita citaEliminada;

        public EliminarCitaWindow(Modelo.Cita citaElegida)
        {
            citaEliminada = citaElegida;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();

            MensajeTextBlock.Text = $"¿Quiere eliminar la cita programada para las {citaEliminada.HoraCita} del dia de hoy?";
        }

        // Cancelacion de la eliminacion de la cita
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Confirmacion para eliminar la cita
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if (BDServicios.DeleteCita(citaEliminada) == 1)
            {
                MessageBox.Show("Cita eliminada con éxito", "Eliminar cita", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar la cita... Compruebe su conexión a internet", "Eliminar cita", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

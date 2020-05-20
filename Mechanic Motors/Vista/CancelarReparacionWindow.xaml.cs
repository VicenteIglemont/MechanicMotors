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
    /// Lógica de interacción para CancelarReparacionWindow.xaml
    /// </summary>
    public partial class CancelarReparacionWindow : Window
    {

        Modelo.Reparacion reparacionCancelada;

        public CancelarReparacionWindow(Modelo.Reparacion reparacionCancelada)
        {
            this.reparacionCancelada = reparacionCancelada;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();

            IdClienteTextBlock.Text = reparacionCancelada.IdReparacion.ToString();
            NombreClienteTextBlock.Text = reparacionCancelada.NombreCliente;
            EmailClienteTextBlock.Text = reparacionCancelada.EmailCliente;
            TelefonoClienteTextBlock.Text = reparacionCancelada.TelefonoCliente;
            VehiculoTextBlock.Text = reparacionCancelada.Vehiculo;
            ProblemaTextBox.Text = reparacionCancelada.Descripcion;

        }

        // Cancelar eliminacion de reparacion
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Confirmar eliminacion de reparacion
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            if(BDServicios.DeleteReparacion(reparacionCancelada) == 1)
            {
                MessageBox.Show("Reparacion eliminada con exito", "Eliminar reparacion", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar la reparacion", "Eliminar reparacion", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            this.Close();
        }
    }
}

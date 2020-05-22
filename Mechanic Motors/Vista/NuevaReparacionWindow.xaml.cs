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
    public partial class NuevaReparacionWindow : Window
    {
        public NuevaReparacionWindow()
        {
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();
        }

        // Cancelamos el registro de la reparacion
        private void CancelarNuevaReparacion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Confirmamos el registro de la reparacion
        private void ConfirmarNuevaReparacion_Click(object sender, RoutedEventArgs e)
        {
            Reparacion reparacion = new Reparacion();

            reparacion.NombreCliente = FormularioUserControl.NombreClienteTextBox.Text.Trim();
            reparacion.TelefonoCliente = FormularioUserControl.TelefonoClienteTextBox.Text.Trim();
            reparacion.EmailCliente = FormularioUserControl.EmailClienteTextBox.Text.Trim();
            reparacion.Vehiculo = FormularioUserControl.VehiculoTextBox.Text.Trim();
            reparacion.Descripcion = FormularioUserControl.DescripcionTextBox.Text.Trim();
            reparacion.HoraEntrada = DateTime.Now;

            if(reparacion.NombreCliente != "" && reparacion.TelefonoCliente.Length == 9 && (reparacion.EmailCliente.Contains('@') || reparacion.EmailCliente != "") && reparacion.Vehiculo != "")
            {
                if(BDServicios.AddReparacion(reparacion) == 1)
                {
                    MessageBox.Show("Reparación añadida con exito", "Nueva reparación", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se ha podido añadir la reparacion...", "Nueva reparación", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Alguno de los campos es incorrecto... No se ha añadido la reparacion...", "Nueva reparación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

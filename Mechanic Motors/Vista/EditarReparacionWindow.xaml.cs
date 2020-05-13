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
    /// Lógica de interacción para EditarReparacionWindow.xaml
    /// </summary>
    public partial class EditarReparacionWindow : Window
    {

        Modelo.Reparacion reparacionElegida;

        public EditarReparacionWindow(Modelo.Reparacion reparacionElegida)
        {
            this.reparacionElegida = reparacionElegida;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();

            FormularioUserControl.IdReparacionTextBox.IsReadOnly = true;
            FormularioUserControl.NombreClienteTextBox.IsReadOnly = true;
            FormularioUserControl.VehiculoTextBox.IsReadOnly = true;

            FormularioUserControl.IdReparacionTextBox.Text = reparacionElegida.IdReparacion.ToString();
            FormularioUserControl.NombreClienteTextBox.Text = reparacionElegida.NombreCliente;
            FormularioUserControl.TelefonoClienteTextBox.Text = reparacionElegida.TelefonoCliente;
            FormularioUserControl.EmailClienteTextBox.Text = reparacionElegida.EmailCliente;
            FormularioUserControl.VehiculoTextBox.Text = reparacionElegida.Vehiculo;
            FormularioUserControl.DescripcionTextBox.Text = reparacionElegida.Descripcion;
        }

        private void CancelarModificacionReparacion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmarModificacionReparacion_Click(object sender, RoutedEventArgs e)
        {
            reparacionElegida.TelefonoCliente = FormularioUserControl.TelefonoClienteTextBox.Text.Trim();
            reparacionElegida.EmailCliente = FormularioUserControl.EmailClienteTextBox.Text.Trim();
            reparacionElegida.Descripcion = FormularioUserControl.DescripcionTextBox.Text.Trim();

            if(reparacionElegida.TelefonoCliente.Length == 9 && (reparacionElegida.EmailCliente.Contains('@') || reparacionElegida.EmailCliente != "") && reparacionElegida.Descripcion != "")
            {
                if (BDServicios.SaveReparacion(reparacionElegida) == 1)
                {
                    MessageBox.Show("Reparación editada con exito", "Editar reparación", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se ha podido editar la reparacion... Compruebe su conexión a internet", "Editar reparación", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Alguno de los campos es incorrecto... No se ha editado la reparacion...", "Editar reparación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

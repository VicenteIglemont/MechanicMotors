using Mechanic_Motors.Modelo;
using Mechanic_Motors.ServiciosBD;
using Mechanic_Motors.VistaModelo;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Lógica de interacción para NuevaPiezaWindow.xaml
    /// </summary>
    public partial class NuevaPiezaWindow : Window
    {
        public NuevaPiezaWindow()
        {
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();
        }

        // Cancelar registro de la pieza
        private void CancelarNuevaPieza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Confirmar registro de la pieza
        private void ConfirmarNuevaPieza_Click(object sender, RoutedEventArgs e)
        {
            Pieza pieza = new Pieza();

            FormularioUserControl.PrecioTextBox.Text.Replace('.', ',');

            pieza.NombrePieza = FormularioUserControl.NombrePiezaTextBox.Text.Trim();
            pieza.VehiculoPerteneciente = FormularioUserControl.VehiculoPertenecienteTextBox.Text.Trim();
            pieza.Color = FormularioUserControl.ColorTextBox.Text;
            pieza.PrecioUnitario = Convert.ToDouble(FormularioUserControl.PrecioTextBox.Text);
            pieza.PrecioUnitario = Math.Round(pieza.PrecioUnitario, 2);

            pieza.Cantidad = Convert.ToInt32(FormularioUserControl.CantidadTextBox.Text);

            if(pieza.NombrePieza != "" || pieza.PrecioUnitario != 0 || pieza.Cantidad != 0)
            {
                if (BDServicios.AddPieza(pieza) == 1)
                {
                    MessageBox.Show("Pieza añadida con exito", "Nueva pieza", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se ha podido añadir la pieza... Compruebe su conexión a internet y revise los campos", "Nueva pieza", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Alguno de los campos es incorrecto... No se ha añadido la pieza...", "Nueva pieza",MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}

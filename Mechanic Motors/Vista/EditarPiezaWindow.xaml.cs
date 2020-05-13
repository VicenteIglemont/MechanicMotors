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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mechanic_Motors.Vista
{
    /// <summary>
    /// Lógica de interacción para EditarPiezaWindow.xaml
    /// </summary>
    public partial class EditarPiezaWindow : Window
    {

        Modelo.Pieza piezaElegida;

        public EditarPiezaWindow(Modelo.Pieza piezaElegida)
        {
            this.piezaElegida = piezaElegida;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();

            FormularioUserControl.IdPiezaTextBox.IsReadOnly = true;
            FormularioUserControl.NombrePiezaTextBox.IsReadOnly = true;
            FormularioUserControl.VehiculoPertenecienteTextBox.IsReadOnly = true;

            FormularioUserControl.IdPiezaTextBox.Text = piezaElegida.IdPieza.ToString();
            FormularioUserControl.NombrePiezaTextBox.Text = piezaElegida.NombrePieza;
            FormularioUserControl.VehiculoPertenecienteTextBox.Text =piezaElegida.VehiculoPerteneciente;
            FormularioUserControl.ColorTextBox.Text = piezaElegida.Color;
            FormularioUserControl.PrecioTextBox.Text = piezaElegida.PrecioUnitario.ToString();
            FormularioUserControl.CantidadTextBox.Text = piezaElegida.Cantidad.ToString();

        }

        private void CancelarModificacionPieza_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConfirmarModificacionPieza_Click(object sender, RoutedEventArgs e)
        {
            FormularioUserControl.PrecioTextBox.Text.Replace('.', ',');

            piezaElegida.Color = FormularioUserControl.ColorTextBox.Text;
            piezaElegida.PrecioUnitario = Convert.ToDouble(FormularioUserControl.PrecioTextBox.Text);
            piezaElegida.PrecioUnitario = Math.Round(piezaElegida.PrecioUnitario, 2);
            piezaElegida.Cantidad = Convert.ToInt32(FormularioUserControl.CantidadTextBox.Text);
            
            if(piezaElegida.Cantidad >= 0 && piezaElegida.PrecioUnitario >= 0)
            {
                if(BDServicios.SavePieza(piezaElegida) == 1)
                {
                    System.Windows.MessageBox.Show("Pieza editada con exito", "Editar pieza", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("No se ha podido editar la pieza... Compruebe su conexión a internet", "Editar pieza", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Alguno de los campos es incorrecto... No se ha editado la informacion de la pieza...", "Editar pieza", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

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
    /// Lógica de interacción para EliminarPiezaWindow.xaml
    /// </summary>
    public partial class EliminarPiezaWindow : Window
    {

        Pieza piezaEliminada;

        public EliminarPiezaWindow(Pieza piezaEliminada)
        {
            this.piezaEliminada = piezaEliminada;
            this.DataContext = new MenuPrincipalViewModel();
            InitializeComponent();

            IdPiezaTextBlock.Text = piezaEliminada.IdPieza.ToString();
            NombrePiezaTextBlock.Text = piezaEliminada.NombrePieza;
            VehiculoTextBlock.Text = piezaEliminada.VehiculoPerteneciente;
            ColorTextBlock.Text = piezaEliminada.Color;
            PrecioTextBlock.Text = piezaEliminada.PrecioUnitario.ToString();
            CantidadTextBlock.Text = piezaEliminada.Cantidad.ToString();
        }

        // Cancela la eliminacion de la pieza
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Confirma la eliminacion de la pieza
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (BDServicios.DeletePieza(piezaEliminada) == 1)
            {
                MessageBox.Show("Pieza eliminada con éxito", "Eliminar pieza", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se ha podido eliminar la pieza... Compruebe su conexión a internet", "Eliminar pieza", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }

    

}
